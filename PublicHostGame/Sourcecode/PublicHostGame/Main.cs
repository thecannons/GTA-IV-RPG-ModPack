using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTA;
using GTA.Native;
using System.Windows.Forms;
using System.Drawing;

namespace PublicHostGame
{
    public class Main : Script
    {
        #region Declaring
        List<String> PlayerStringList = new List<String>();
        int playerCount = 0,
            hostMsgCnt = 0,
            displayHostMsgInterval;
        bool listLoaded = false,
            playerSelected = false,
            dediMode = false,
            isFirstTimeHost = true,
            allowHostMsg,
            allowHostOnlyDelete,
            toggleControlMenu = false,
            useAutoKick;
        Keys manualKick,
            dediKey,
            cleanServer,
            showModPackControls,
            toggleAutoKick;
        public Texture controlsMenu;
        private string dataPath = (Game.InstallFolder + "/scripts/PublicHostGame_Menu/");
        //For Cleaning
        GTA.Object[] allObjects;
        GTA.Ped[] allPeds;
        List<Ped> allPedsList = new List<Ped>();
        GTA.Vehicle[] allVehs;

        GTA.Timer host_msg_timer;
        Player mpPlayer;
        GTA.Font mainText;
        int waitModifier;
        #endregion
        public Main()
        {
            host_msg_timer = new GTA.Timer(1000);
            host_msg_timer.Tick += new EventHandler(HostMessageTick);
            this.Tick += new EventHandler(MainTick);
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 1000);
            allowHostMsg = Settings.GetValueBool("ALLOW_RECURRING_HOST_MESSAGE", "SETTINGS", true);
            displayHostMsgInterval = Settings.GetValueInteger("HOST_MSG_INTERVAL", "SETTINGS", 1200);
            useAutoKick = Settings.GetValueBool("USE_AUTO_KICK", "SETTINGS", true);
            manualKick = Settings.GetValueKey("MANUAL_KICK", "CONTROLS", Keys.J);
            dediKey = Settings.GetValueKey("DEDICATED_MODE", "CONTROLS", Keys.D);
            cleanServer = Settings.GetValueKey("CLEAN_SERVER", "CONTROLS", Keys.K);
            showModPackControls = Settings.GetValueKey("MODPACK_CONTROLS_MENU", "SETTINGS", Keys.M);
            toggleAutoKick = Settings.GetValueKey("TOGGLE_AUTO_KICK", "SETTINGS", Keys.L);
            BindKey(dediKey, true, true, true, DedicatedMode);
            BindKey(manualKick, true, true, true, KickPlayer);
            BindKey(cleanServer, true, true, true, CleanServer);
            BindKey(showModPackControls, true, false, true, showControls);
            mainText = new GTA.Font(44.0F, FontScaling.Pixel);
            PerFrameDrawing += new GraphicsEventHandler(Dedicated_ScreenMode);
            LoadListData();
            controlsMenu = new Texture(this.StreamFile(dataPath + "ModPack_Controls.png"));
            //controlsMenu = Resources.GetTexture("ModPack_Controls.png");
            if (allowHostMsg)
            {
                host_msg_timer.Start();
            }
        }
        void showControls()
        {
            if (toggleControlMenu)
                toggleControlMenu = false;
            else
                toggleControlMenu = true;

        }
        private byte[] StreamFile(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            stream.Close();
            return buffer;
        }
        //This is where the host will announce that he is host if enabled
        void HostMessageTick(object sender, EventArgs e)
        {
            if (GTA.Multiplayer.Host.isHost)
            {
                if (hostMsgCnt >= displayHostMsgInterval && allowHostMsg)
                {
                    Game.SendChatMessage(Game.LocalPlayer.Name + " , IS CURRENT SERVER HOST");
                    Game.DisplayText("YOU ARE THE CURRENT HOST", 5000);
                    Text("YOU ARE THE CURRENT HOST", 5000);
                    hostMsgCnt = 0;
                    Wait(5000);
                }
                hostMsgCnt++;
            }
        }
        private void Text(string text, int duration)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, duration, 1);
        }
        void DedicatedMode()
        {
            if (dediMode)
            {
                dediMode = false;
                Game.DisplayText("Dedicated mode deactivated", 2000);
                Game.LocalPlayer.Character.Invincible = false;
                Game.LocalPlayer.Character.Visible = true;
                Game.Unpause();
            }
            else
            {
                dediMode = true;
                Game.DisplayText("Dedicated mode activated", 2000);
                Game.LocalPlayer.Character.Invincible = true;
                Game.LocalPlayer.Character.Visible = false;
                Game.Pause();
            }
        }
        void KickPlayer()
        {
            Player[] playerList = Game.PlayerList;
            if (playerList.Count() > 1)
            {
                foreach (Player mpPlayer in playerList)
                {
                    if (Game.Exists(mpPlayer.Character) && mpPlayer != Game.LocalPlayer)
                    {
                        if (GTA.Multiplayer.Host.isHost)
                        {
                            while (!playerSelected)
                            {
                                Game.DisplayText("Kick: " + mpPlayer.Name + "? HOLD RightShiftKey to Deny, HOLD Enter to Kick, Or HOLD Backspace to exit Kicking Function", 3000);
                                Text("Kick: " + mpPlayer.Name + "? HOLD RightShiftKey to Deny, HOLD Enter to Kick, HOLD Backspace to end this", 3000);
                                Wait(1000);
                                if (Game.isKeyPressed(Keys.RShiftKey))
                                    break;
                                else if (Game.isKeyPressed(Keys.Enter))
                                    playerSelected = true;
                                else if (Game.isKeyPressed(Keys.Back))
                                    return;
                            }
                            if (playerSelected)
                            {
                                Game.DisplayText("KICKING PLAYER: " + mpPlayer.Name, 5000);
                                Function.Call("NETWORK_KICK_PLAYER", mpPlayer, true);
                                break;
                            }
                        }
                    }
                    while (Game.isKeyPressed(Keys.RShiftKey) || Game.isKeyPressed(Keys.Enter))
                    {
                        Text("Let go of RSHIFTKEY OR ENTER", 3000);
                        Wait(1000);
                    }
                    Wait(1000);
                }
            }
        }
        void MainTick(object sender, EventArgs e)
        {
            if (GTA.Multiplayer.Host.isHost)
            {
                if (isFirstTimeHost)
                {
                    Game.SendChatMessage(Game.LocalPlayer.Name + " , IS THE NEW SERVER HOST");
                    Game.DisplayText("YOU ARE THE NEW HOST", 3000);
                    Text("YOU ARE THE NEW HOST", 3000);
                    Wait(3000);
                    Game.SendChatMessage(Game.LocalPlayer.Name + " , IS THE NEW SERVER HOST");
                    Game.DisplayText("YOU ARE THE NEW HOST", 10000);
                    Text("YOU ARE THE NEW HOST", 10000);
                    Wait(10000);
                    isFirstTimeHost = false;
                }
            }
            if (listLoaded && useAutoKick)
            {
                Player[] playerList = Game.PlayerList;
                if (playerList.Count() > 1)
                {
                    foreach (Player mpPlayer in playerList)
                    {
                        if (Game.Exists(mpPlayer.Character) && mpPlayer != Game.LocalPlayer && !(PlayerStringList.Contains(mpPlayer.Name)))
                        {
                            if (GTA.Multiplayer.Host.isHost)
                            {
                                Game.DisplayText("KICKING UNAUTHORIZED PLAYER: " + mpPlayer.Name, 5000);
                                Function.Call("NETWORK_KICK_PLAYER", mpPlayer, true);
                                // GTA.Multiplayer.Host.KickPlayer(mpPlayer);
                                break;
                            }
                        }
                    }
                }
            }           
        }
        void LoadListData()
        {
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "PublicHostPlayerList.ini"));
            ini.Load();
            Game.DisplayText("Loading Player List", 3000);
            Wait(3000);
            for (int i = 1; i <= 32; i++)
            {
                String selectedPlayer = "PLAYER_";
                String playerName = ini.GetValueString(selectedPlayer + i.ToString(), "ALLOWED_PLAYERS", "EMPTYSLOT");
                Game.DisplayText("Player Loaded: " + playerName, 5000);
                //Wait(500);
                if (playerName != "EMPTYSLOT")
                {
                    PlayerStringList.Add(playerName);
                }
            }
            Game.DisplayText("Player List Loaded", 3000);
            listLoaded = true; 
        }

        void Dedicated_ScreenMode(object sender, GraphicsEventArgs e)
        {
            int xPosSprite = (Game.Resolution.Width / 2);
            int yPosSprite = (Game.Resolution.Height / 2);
            int xPos = 0;
            int yPos = 0;
            int width = 5000;
            int height = 5000;
            Color backColor = Color.Black;
            RectangleF blackScreen = new RectangleF(xPos, yPos, width, height);
            RectangleF text1 = new RectangleF(xPos, yPos + 350, 1100, 50);
            if (dediMode)
            {
                e.Graphics.DrawRectangle(blackScreen, backColor);
                e.Graphics.DrawText("Dedicated Mode", text1, TextAlignment.Center, mainText);
            }
            if (toggleControlMenu)
            {
                e.Graphics.DrawSprite(controlsMenu, xPosSprite, yPosSprite, Game.Resolution.Width - (Game.Resolution.Width / 4), Game.Resolution.Height - (Game.Resolution.Height / 4), 0f);
            }
        }
        void CleanServer()
        {
            Game.DisplayText("Server Cleanup: Gathering Objects");
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Gathering Objects to Delete");
            Wait(3000);
            allObjects = World.GetAllObjects();
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Deleting Objects");
            try
            {
                foreach (var eachObj in allObjects)
                {
                    Game.DisplayText("Server Cleanup: Deleting Objects");
                    if (Game.Exists(eachObj))
                        eachObj.Delete();
                    Wait(20);
                }
            }
            catch (NonExistingObjectException ex)
            {
                Game.Console.Print(ex.Message);
                Game.DisplayText(ex.Message, 5000);
                return;
            }
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Gathering Vehicles to Delete");
            Game.DisplayText("Server Cleanup: Gathering Vehicles");
            Wait(3000);
            allVehs = World.GetAllVehicles();
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Deleting Vehicles");
            try
            {
                foreach (var eachVeh in allVehs)
                {
                    if ((Game.LocalPlayer.Character.CurrentVehicle != eachVeh) || (this.mpPlayer.Character.CurrentVehicle != eachVeh))
                    {
                        Game.DisplayText("Server Cleanup: Deleting Vehicles");
                        eachVeh.Delete();
                        Wait(20);
                    }
                }
            }
            catch (NonExistingObjectException ex)
            {
                Game.Console.Print(ex.Message);
                Game.DisplayText(ex.Message, 5000);
                return;
            }
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Gathering Peds to Delete");
            Game.DisplayText("Server Cleanup: Gathering Peds");
            Wait(3000);
            allPeds = World.GetAllPeds();
            foreach (var eachPed in allPeds)
            {
                allPedsList.Add(eachPed);
            }
            if (Game.isMultiplayer)
            {
                foreach (var eachPed in allPeds)
                {
                    Player[] playerList = Game.PlayerList;
                    foreach (var mpPlayer in playerList)
                    {
                        if (Game.Exists(mpPlayer.Character))
                        {
                            if (eachPed == mpPlayer)
                                allPedsList.Remove(eachPed);
                        }
                    }
                }
            }
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup: Deleting Peds");
            try
            {
                foreach (var eachPed in allPedsList)
                {
                    if ((Game.Exists(Game.LocalPlayer.Character) && Game.LocalPlayer.Character != eachPed))
                    {
                        Game.DisplayText("Server Cleanup: Deleting Peds");
                        if (Game.Exists(eachPed))
                            eachPed.Delete();
                        Wait(20);
                    }
                }
            }
            catch (NonExistingObjectException ex)
            {
                Game.Console.Print(ex.Message);
                Game.DisplayText(ex.Message, 5000);
                return;
            }
            Game.DisplayText("Server Cleanup Complete");
            if (Game.isMultiplayer)
                Game.SendChatMessage("Server Cleanup Complete");
        }
    }
}
