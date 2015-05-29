using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GTA;
using System.IO;
using GTA.Native;
//using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;

namespace PersistentPersons
{
    public class PersistentPersonsCS : Script
    {
        #region Declaring
        //Declarations
        List<int> mWepsOnPersons = new List<int>();// 0 = uzi, 1 = mp5, 2 = pump, 3 = baretta, 4 = ak, 5 = m4, 6 = bolt, 7 = auto, 8 = molotov, 9 = grenade; 10 = glock, 11 = deagle
        List<int> mAmmoOnPersons = new List<int>();
        Vector3 mPlayerPos;
        // Guns
        int r_ak = 0,
            r_m4 = 0,
            glock = 0,
            deagle = 0,
            baretta = 0,
            pump = 0,
            uzi = 0,
            mp5 = 0,
            snipeAuto = 0,
            snipeBolt = 0,
            molotov = 0,
            grenade = 0,
            meleeWep = 0;
        //Ammo
        int rifleAmmo = 0,
            pistolAmmo = 0,
            shottyAmmo = 0,
            smgAmmo = 0,
            sniperAmmo = 0,
            throwAmmo = 0;
        int mPlayerHealth;
        int mPlayerArmor;
        int mPlayerMoney,
            waitModifier;
        Player mLocalP;
        Model mPlayerModel;
        List<Weapon> mGTAWeps;
        String mMeleeWepName;
        Keys forceSaveKey,
            toggleAutoSave,
            clearSavedData,
            loadGame;
        GTA.Timer saveDat_Tick,
            rewardArrest_Tick;
        int saveTimer = 0;
        int mPlayerHeadIndex = 0;
        int mPlayerFaceIndex = 0;
        int mPlayerHairIndex = 0;
        bool autoSaveEnabled = true;
        bool autoLoad;
        bool rewardArrest;
        Keys instaCop;
        int autoSaveInterval = 300; // in seconds
        int arrestCoolDown = 0;
        bool validArrest = false;
        #endregion
        public PersistentPersonsCS()
        {

            rewardArrest_Tick = new GTA.Timer(500);
            rewardArrest_Tick.Tick += new EventHandler(RewardArrest_Tick);
            saveDat_Tick = new GTA.Timer(1000);
            saveDat_Tick.Tick += new EventHandler(SaveDat_Tick);
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 500);
            this.Tick += new EventHandler(PersistentPersonMain_Tick);
            forceSaveKey = Settings.GetValueKey("FORCE_SAVE_KEY", "CONTROLS", Keys.S);
            toggleAutoSave = Settings.GetValueKey("ENABLE_DISABLE_AUTOSAVE_FUNCTION", "CONTROLS", Keys.A);
            autoSaveInterval = Settings.GetValueInteger("AUTOSAVE_INTERVAL", "SETTINGS", 300);
            autoSaveEnabled = Settings.GetValueBool("AUTO_SAVE_ENABLED", "SETTINGS", true);
            clearSavedData = Settings.GetValueKey("CLEAR_SAVED_DATA", "CONTROLS", Keys.C);
            loadGame = Settings.GetValueKey("LOAD_SAVED_DATA", "CONTROLS", Keys.L);
            autoLoad = Settings.GetValueBool("AUTO_LOAD_ENABLED", "SETTINGS", true);
            instaCop = Settings.GetValueKey("CHOOSE_COP", "CONTROLS", Keys.I);
            rewardArrest = Settings.GetValueBool("REWARD_COP", "SETTINGS", true);
            autoSaveEnabled = Settings.GetValueBool("AUTO_SAVE_ENABLED", "SETTINGS", false);
            BindKey(instaCop, true, true, true, InstaCop);
            BindKey(loadGame, true, true, true, LoadPersonsData);
            BindKey(forceSaveKey, true, true, true, SavePersonsData);
            BindKey(toggleAutoSave, true, true, true, Toggle_AutoSave);
            //BindKey(clearSavedData, true, true, true, ClearSavedData);
            mLocalP = Game.LocalPlayer;
            if (autoLoad)
            LoadPersonsData();
            Game.DisplayText("PersistentPersons Mod By Daimyo loaded", 2000);
        }
        private int R(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
        private void Text(string text, int duration)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, duration, 1);
        }
        void InstaCop()
        {
            //mLocalP.Character
            bool tIsDone = false;
            String tChoice = "";
            String[] mCopArray = new String[] { "M_Y_COP", "M_Y_COP_TRAFFIC", "M_Y_SWAT", "M_Y_NHELIPILOT","M_Y_STROOPER", "M_M_FATCOP_01","M_M_FBI","M_Y_BOUNCER_01", "M_Y_BOUNCER_02", "M_M_ARMOURED", "IG_FRANCIS_MC" };
            Text("INSTA COP ACTIVATED!", 3000);
            Wait(2000);
            Text("LOOK AT YOUR CHARACTER NOW DURING SKIN CHANGE!", 3000);
            Wait(2000);
            Text("STEP 1 IS LOOKING CHOOSING SKIN, STEP 2 IS SKIN TYPE!", 3000);
            Wait(3000);
            Text("GET READY!", 1000);
            Wait(1000);
            while (tChoice == "")
            {
                for (int i = 0; i < mCopArray.Length; i++)
                {
                    mLocalP.Model = mCopArray[i];
                    //mLocalP.Character.Voice = mCopArray[i];
                    Text("PRESS AND HOLD ENTER TO SELECT CURRENT SKIN", 1500);
                    Wait(2000);
                    if (Game.isKeyPressed(Keys.Enter))
                    {
                        tChoice = mCopArray[i];
                        break;
                    }
                }
            }
            while (Game.isKeyPressed(Keys.Enter))
            {
                Text("LET GO OF ENTER", 1000);
                Wait(1000);
            }
            Text("NOW WERE GOING THROUGH THE SKIN VARIATIONS FOR THIS SKIN. PRESS AND HOLD ENTER TO SELECT ONE", 4000);
            Wait(4000);
            while (!tIsDone)
            {
                Function.Call("SET_CHAR_RANDOM_COMPONENT_VARIATION", new Parameter[] { mLocalP.Character });
                    Text("PRESS and HOLD 'ENTER' to select CURRENT SKIN TYPE", 2000);
                    Wait(2000);
                    if (Game.isKeyPressed(Keys.Enter))
                    {
                        tIsDone = true;
                        break;
                    }
            }
            mPlayerHeadIndex = mLocalP.Character.Skin.Component.Head.ModelIndex;
            mPlayerFaceIndex =  mLocalP.Character.Skin.Component.Face.ModelIndex;
            mPlayerHairIndex =  mLocalP.Character.Skin.Component.Hair.ModelIndex;

            Text("ALL DONE, MAKE SURE TO SAVE YOUR SKIN!", 4000);
           
        }
        void PersistentPersonMain_Tick(object sender, EventArgs e)
        {   //Main
            //Lets find out what the player has to assign values to them
            if (mLocalP.Character.isAlive)
            {
                GetPlayerInfo();
            }
            else
            {

            }
            if (autoSaveEnabled)
            {
                if (!saveDat_Tick.isRunning)
                    saveDat_Tick.Start();
            }
            else
            {
                if (saveDat_Tick.isRunning)
                {
                    saveDat_Tick.Stop();
                }
            }
            if (rewardArrest)
            {
                if (!rewardArrest_Tick.isRunning)
                    rewardArrest_Tick.Start();
            }
            else
            {
                if (rewardArrest_Tick.isRunning)
                {
                    rewardArrest_Tick.Stop();
                }
            }
        }
        void Toggle_AutoSave()
        {
            if (autoSaveEnabled)
            {
                autoSaveEnabled = false;
                Text("AUTOSAVE Disabled", 2000);
                Game.DisplayText("AUTOSAVE Disabled", 2000);
            }
            else
            {
                Text("AUTOSAVE Enabled", 2000);
                Game.DisplayText("AUTOSAVE Enabled", 2000);
                autoSaveEnabled = true;
            }
        }
        void RewardArrest_Tick(object sender, EventArgs e)
        {
            Ped[] tCriminals;
            if (arrestCoolDown <= 0 && Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
            {
                //policeVehicle = mLocalP.Character.CurrentVehicle;
                tCriminals = World.GetPeds(mLocalP.Character.Position, 10f);
                if (tCriminals.Count() > 0)
                {
                    foreach (var criminalPed in tCriminals)
                    {
                        if (Game.isMultiplayer)
                        {
                            Player[] playerList = Game.PlayerList;
                            if (playerList.Count() > 0)
                            {
                                foreach (var mpPlayer in playerList)
                                {
                                    if (Game.isGameKeyPressed(GameKey.Aim) && Game.isGameKeyPressed(GameKey.Action) && Game.Exists(criminalPed) && mLocalP.GetTargetedPed() == criminalPed && mLocalP.GetTargetedPed() != mpPlayer && criminalPed.Position.DistanceTo(mLocalP.Character.Position) <= 10)
                                    {
                                        if (Game.Exists(criminalPed) && criminalPed.isAlive && !criminalPed.isInVehicle())
                                        {
                                            validArrest = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Game.isGameKeyPressed(GameKey.Aim) && Game.isGameKeyPressed(GameKey.Action) && Game.Exists(criminalPed) && mLocalP.GetTargetedPed() == criminalPed && criminalPed.Position.DistanceTo(mLocalP.Character.Position) <= 10)
                                {
                                    if (Game.Exists(criminalPed) && criminalPed.isAlive && !criminalPed.isInVehicle())
                                    {
                                        validArrest = true;
                                        break;
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (Game.isGameKeyPressed(GameKey.Aim) && Game.isGameKeyPressed(GameKey.Action) && Game.Exists(criminalPed) && mLocalP.GetTargetedPed() == criminalPed && criminalPed.Position.DistanceTo(mLocalP.Character.Position) <= 10)
                            {
                                if (Game.Exists(criminalPed) && criminalPed.isAlive && !criminalPed.isInVehicle())
                                {
                                    validArrest = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            arrestCoolDown--;
            if (arrestCoolDown <= 0)
                arrestCoolDown = 0;
            if (validArrest && arrestCoolDown <= 0)
            {
                int rewardArrestMoney = R(25, 150);
                mLocalP.Money += rewardArrestMoney;
                Text("You were rewarded $" + rewardArrestMoney + " dollars for making the arrest", 5000);
                validArrest = false;
                arrestCoolDown = 600;
            }

            //policeVehicle.PassengerSeats.Equals(VehicleSeat.LeftRear);
            

        }
        void SaveDat_Tick(object sender, EventArgs e)
        {
            saveTimer++;
            //Game.DisplayText("Persistent: " + saveTimer.ToString(), 2000);
            if (saveTimer >= autoSaveInterval && autoSaveEnabled)
            {
                SavePersonsData();
                saveTimer = 0;
            }
        }
        void GetPlayerInfo()
        {
            #region Weapons and Ammo on Persons
            //Weapons and Ammo
            //if (Game.Exists(mLocalP.Character.Weapons.AssaultRifle_AK47)
            if (Exists(mLocalP.Character.Weapons.AssaultRifle_AK47))
            {
                //r_ak = 1;
                r_ak = mLocalP.Character.Weapons.AssaultRifle_AK47.Ammo;
            }
            else
            {
                r_ak = 0;
            }
            if (Exists(mLocalP.Character.Weapons.AssaultRifle_M4))
            {
               // r_m4 = 1;
                r_m4 = mLocalP.Character.Weapons.AssaultRifle_M4.Ammo;
            }
            else
            {
                //r_m4 = 0;
                r_m4 = 0;
            }
            if (Exists(mLocalP.Character.Weapons.Glock))
            {
                //glock = 1;
                glock = mLocalP.Character.Weapons.Glock.Ammo;
            }
            else
            {
                //glock = 0;
                glock = 0;
            }
            if (Exists(mLocalP.Character.Weapons.DesertEagle))
            {
                //deagle = 1;
                deagle = mLocalP.Character.Weapons.DesertEagle.Ammo;
            }
            else
            {
                deagle = 0;
               // pistolAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.BarettaShotgun))
            {
               // baretta = 1;
                baretta = mLocalP.Character.Weapons.BarettaShotgun.Ammo;
            }
            else
            {
                baretta = 0;
                //shottyAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.BasicShotgun))
            {
                //pump = 1;
                pump = mLocalP.Character.Weapons.BasicShotgun.Ammo;
            }
            else
            {
                pump = 0;
                //shottyAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.Uzi))
            {
                //uzi = 1;
                uzi = mLocalP.Character.Weapons.Uzi.Ammo;
            }
            else
            {
                uzi = 0;
                //smgAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.MP5))
            {
                //mp5 = 1;
                mp5 = mLocalP.Character.Weapons.MP5.Ammo;
            }
            else
            {
                mp5 = 0;
                //smgAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.SniperRifle_M40A1))
            {
                //snipeAuto = 1;
                snipeAuto = mLocalP.Character.Weapons.SniperRifle_M40A1.Ammo;
            }
            else
            {
                snipeAuto = 0;
                //sniperAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.BasicSniperRifle))
            {
                //snipeBolt = 1;
                snipeBolt = mLocalP.Character.Weapons.BasicSniperRifle.Ammo;
            }
            else
            {
                snipeBolt = 0;
                //sniperAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.MolotovCocktails))
            {
                //molotov = 1;
                molotov = mLocalP.Character.Weapons.MolotovCocktails.Ammo;
            }
            else
            {
                molotov = 0;
               // throwAmmo = 0;
            }
            if (Exists(mLocalP.Character.Weapons.Grenades))
            {
               //grenade = 1;
               grenade = mLocalP.Character.Weapons.Grenades.Ammo;
            }
            else
            {
                grenade = 0;
                throwAmmo = 0;
            }
            #endregion
            //Get Melee weapon
            if (Exists(mLocalP.Character.Weapons.AnyMelee))
            {
                meleeWep = 1;
                if (Exists(mLocalP.Character.Weapons.BaseballBat))
                    mMeleeWepName = mLocalP.Character.Weapons.BaseballBat.Type.ToString();
                else if (Exists(mLocalP.Character.Weapons.Knife))
                    mMeleeWepName = mLocalP.Character.Weapons.Knife.Type.ToString();
                else if (Exists(mLocalP.Character.Weapons.AnyMelee))
                    mMeleeWepName = mLocalP.Character.Weapons.AnyMelee.Type.ToString();
                else
                    mMeleeWepName = "EMPTY"; 
            }
            else
                meleeWep = 0;
            //
            // Get player skin
            mPlayerModel = mLocalP.Model;
            // Get Player head attributes
            mPlayerHeadIndex = mLocalP.Character.Skin.Component.Head.ModelIndex;
           // mPlayerFaceIndex = mLocalP.Character.Skin.Component.Face.ModelIndex;
           // mPlayerHairIndex = mLocalP.Character.Skin.Component.Hair.ModelIndex;
            //Get player health and Armor;
            mPlayerHealth = mLocalP.Character.Health;
            mPlayerArmor = mLocalP.Character.Armor;
            //Get player money
            mPlayerMoney = mLocalP.Money;
            //Get Player Pos
            mPlayerPos = mLocalP.Character.Position;
           
        }
        void SetPlayerPersonsGear()
        {
                // 0 = r_ak, 1 = r_m4, 2 = glock, 3 = deagle, 4 = baretta, 5 = pump, 6 = uzi, 7 = mp5, 8 = snipeAuto, 9 = snipeBolt; 10 = molotov, 11 = grenade 12 = meleeWep // 13 total
                // Lets set players melee if it exists
                if (meleeWep == 1)
                {
                    if (Weapon.Melee_BaseballBat.ToString() == mMeleeWepName)
                    {
                        mLocalP.Character.Weapons.BaseballBat.Ammo = 1;
                    }
                    else if (Weapon.Melee_Knife.ToString() == mMeleeWepName)
                    {
                        mLocalP.Character.Weapons.Knife.Ammo = 1;
                    }
                    else
                        mLocalP.Character.Weapons.AnyMelee.Ammo = 1;
                }
                //Set Skin
                mPlayerModel = ReturnModelName(mPlayerModel.ToString());
                mLocalP.Model = mPlayerModel;
                //Set Head attributes
                mLocalP.Character.Skin.Component.Head.ModelIndex = mPlayerHeadIndex;
                //mLocalP.Character.Skin.Component.Face.ModelIndex = mPlayerFaceIndex;
                //mLocalP.Character.Skin.Component.Hair.ModelIndex = mPlayerHairIndex;
                //Set Health and Armor
                mLocalP.Character.Health = mPlayerHealth;
                mLocalP.Character.Armor = mPlayerArmor;
                // Set Player money
                mLocalP.Money = mPlayerMoney;
                //Set Player pos
                mLocalP.TeleportTo(mPlayerPos);
                Wait(2000);
                #region Give Player Weapons and Ammo
                // Give player weapons and ammo
                if (r_ak >= 1)
                {
                    mLocalP.Character.Weapons.AssaultRifle_AK47.Ammo = r_ak;
                    Wait(100);
                }
                if (r_m4 >= 1)
                {
                    mLocalP.Character.Weapons.AssaultRifle_M4.Ammo = r_m4;
                    Wait(100);
                }
                if (glock >= 1)
                {
                    mLocalP.Character.Weapons.Glock.Ammo = glock;
                    Wait(100);
                }
                if (deagle >= 1)
                {
                    mLocalP.Character.Weapons.DesertEagle.Ammo = deagle;
                    Wait(100);
                }
                if (baretta >= 1)
                {
                    mLocalP.Character.Weapons.BarettaShotgun.Ammo = baretta;
                    Wait(100);
                }
                if (pump >= 1)
                {
                    mLocalP.Character.Weapons.BasicShotgun.Ammo = pump;
                    Wait(100);
                }
                if (uzi >= 1)
                {
                    mLocalP.Character.Weapons.Uzi.Ammo = uzi;
                    Wait(100);
                }
                if (mp5 >= 1)
                {
                    mLocalP.Character.Weapons.MP5.Ammo = mp5;
                    Wait(100);
                }
                if (snipeAuto >= 1)
                {
                    mLocalP.Character.Weapons.SniperRifle_M40A1.Ammo = snipeAuto;
                    Wait(100);
                }
                if (snipeBolt >= 1)
                {
                    mLocalP.Character.Weapons.BasicSniperRifle.Ammo = snipeBolt;
                    Wait(100);
                }
                if (molotov >= 1)
                {
                    mLocalP.Character.Weapons.MolotovCocktails.Ammo = molotov;
                    Wait(100);
                }
                if (grenade >= 1)
                {
                    mLocalP.Character.Weapons.Grenades.Ammo = grenade;
                    Wait(100);
                }
                #endregion

        }
        internal static string ReturnModelName(string hex)
        {
            if (hex == "0x6F0783F5") return "PLAYER";
            if (hex == "0x879495E2") return "M_Y_MULTIPLAYER";
            if (hex == "0xD9BDC03A") return "F_Y_MULTIPLAYER";
            if (hex == "0xAE4B15D6") return "SUPERLOD";
            if (hex == "0x6E7BF45F") return "IG_ANNA";
            if (hex == "0x9DD666EE") return "IG_ANTHONY";
            if (hex == "0x5927A320") return "IG_BADMAN";
            if (hex == "0x596FB508") return "IG_BERNIE_CRANE";
            if (hex == "0x6734C2C8") return "IG_BLEDAR";
            if (hex == "0x192BDD4A") return "IG_BRIAN";
            if (hex == "0x98E29920") return "IG_BRUCIE";
            if (hex == "0x0E28247F") return "IG_BULGARIN";
            if (hex == "0x0548F609") return "IG_CHARISE";
            if (hex == "0xB0D18783") return "IG_CHARLIEUC";
            if (hex == "0x500EC110") return "IG_CLARENCE";
            if (hex == "0x5786C78F") return "IG_DARDAN";
            if (hex == "0x1709B920") return "IG_DARKO";
            if (hex == "0x45B445F9") return "IG_DERRICK_MC";
            if (hex == "0x0E27ECC1") return "IG_DMITRI";
            if (hex == "0xDB354C19") return "IG_DWAYNE";
            if (hex == "0xA09901F1") return "IG_EDDIELOW";
            if (hex == "0x03691799") return "IG_FAUSTIN";
            if (hex == "0x65F4D88D") return "IG_FRANCIS_MC";
            if (hex == "0x54EABEE4") return "IG_FRENCH_TOM";
            if (hex == "0x7EED7363") return "IG_GORDON";
            if (hex == "0xEAAEA78E") return "IG_GRACIE";
            if (hex == "0x3A7556B2") return "IG_HOSSAN";
            if (hex == "0xCE3779DA") return "IG_ILYENA";
            if (hex == "0xE369F2A6") return "IG_ISAAC";
            if (hex == "0x458B61F3") return "IG_IVAN";
            if (hex == "0x15BCAD23") return "IG_JAY";
            if (hex == "0x0A2D8896") return "IG_JASON";
            if (hex == "0x17446345") return "IG_JEFF";
            if (hex == "0xEA28DB14") return "IG_JIMMY";
            if (hex == "0xC9AB7F1C") return "IG_JOHNNYBIKER";
            if (hex == "0xD1E17FCA") return "IG_KATEMC";
            if (hex == "0x3B574ABA") return "IG_KENNY";
            if (hex == "0x58A1E271") return "IG_LILJACOB";
            if (hex == "0xB4008E4D") return "IG_LILJACOBW";
            if (hex == "0xD75A60C8") return "IG_LUCA";
            if (hex == "0xE2A57E5E") return "IG_LUIS";
            if (hex == "0xC1FE7952") return "IG_MALLORIE";
            if (hex == "0xECC3FBA7") return "IG_MAMC";
            if (hex == "0x5629F011") return "IG_MANNY";
            if (hex == "0x188232D0") return "IG_MARNIE";
            if (hex == "0xCFE0FB92") return "IG_MEL";
            if (hex == "0x2BD27039") return "IG_MICHAEL";
            if (hex == "0xBF9672F4") return "IG_MICHELLE";
            if (hex == "0xDA0D3182") return "IG_MICKEY";
            if (hex == "0x64C74D3B") return "IG_PACKIE_MC";
            if (hex == "0xF6237664") return "IG_PATHOS";
            if (hex == "0x8BE8B7F2") return "IG_PETROVIC";
            if (hex == "0x932272CA") return "IG_PHIL_BELL";
            if (hex == "0x6AF081E8") return "IG_PLAYBOY_X";
            if (hex == "0x38E02AB6") return "IG_RAY_BOCCINO";
            if (hex == "0xDCFE251C") return "IG_RICKY";
            if (hex == "0x89395FC9") return "IG_ROMAN";
            if (hex == "0x2145C7A5") return "IG_ROMANW";
            if (hex == "0xFEF00775") return "IG_SARAH";
            if (hex == "0x528AE104") return "IG_TUNA";
            if (hex == "0xC380AE97") return "IG_VINNY_SPAZ";
            if (hex == "0x356E1C42") return "IG_VLAD";
            if (hex == "0x3977107D") return "CS_ANDREI";
            if (hex == "0xF866DC66") return "CS_ANGIE";
            if (hex == "0xFC012F67") return "CS_BADMAN";
            if (hex == "0xA2DDDBA7") return "CS_BLEDAR";
            if (hex == "0x009E4F3E") return "CS_BULGARIN";
            if (hex == "0x1F32DB93") return "CS_BULGARINHENCH";
            if (hex == "0x4B13F8D4") return "CS_CIA";
            if (hex == "0xF4386436") return "CS_DARDAN";
            if (hex == "0x1A5B22F0") return "CS_DAVETHEMATE";
            if (hex == "0x030B4624") return "CS_DMITRI";
            if (hex == "0xC74969B0") return "CS_EDTHEMATE";
            if (hex == "0xA776BDC7") return "CS_FAUSTIN";
            if (hex == "0x4AA2E9EA") return "CS_FRANCIS";
            if (hex == "0x2B578C90") return "CS_HOSSAN";
            if (hex == "0x2EB3F295") return "CS_ILYENA";
            if (hex == "0x4A85C1C4") return "CS_IVAN";
            if (hex == "0x96E9F99A") return "CS_JAY";
            if (hex == "0x7055C230") return "CS_JIMMY_PEGORINO";
            if (hex == "0x298ACEC3") return "CS_MEL";
            if (hex == "0x70AEB9C8") return "CS_MICHELLE";
            if (hex == "0xA1DFB431") return "CS_MICKEY";
            if (hex == "0x311DB819") return "CS_OFFICIAL";
            if (hex == "0xD09ECB11") return "CS_RAY_BOCCINO";
            if (hex == "0xDBAC6805") return "CS_SERGEI";
            if (hex == "0x7F5B9540") return "CS_VLAD";
            if (hex == "0x5A6C9C5F") return "CS_WHIPPINGGIRL";
            if (hex == "0xD0F8F893") return "CS_MANNY";
            if (hex == "0x6B941ABA") return "CS_ANTHONY";
            if (hex == "0x26C3D079") return "CS_ASHLEY";
            if (hex == "0x394C11AD") return "CS_ASSISTANT";
            if (hex == "0xE6829281") return "CS_CAPTAIN";
            if (hex == "0xEC96EE3A") return "CS_CHARLIEUC";
            if (hex == "0xC4B4204C") return "CS_DARKO";
            if (hex == "0xFB9190AC") return "CS_DWAYNE";
            if (hex == "0x3D47C135") return "CS_ELI_JESTER";
            if (hex == "0xAED416AF") return "CS_ELIZABETA";
            if (hex == "0x04F78844") return "CS_GAYTONY";
            if (hex == "0x26DE3A8A") return "CS_GERRYMC";
            if (hex == "0x49D3EAD3") return "CS_GORDON";
            if (hex == "0xB93A5686") return "CS_ISSAC";
            if (hex == "0x2E009A8D") return "CS_JOHNNYTHEBIKER";
            if (hex == "0xD7D47612") return "CS_JONGRAVELLI";
            if (hex == "0x5906B7A5") return "CS_JORGE";
            if (hex == "0x71A11E4C") return "CS_KAT";
            if (hex == "0xB4D0F581") return "CS_KILLER";
            if (hex == "0x5E730218") return "CS_LUIS";
            if (hex == "0x1B508682") return "CS_MAGICIAN";
            if (hex == "0xA17C3253") return "CS_MAMC";
            if (hex == "0xEA01EFDC") return "CS_MELODY";
            if (hex == "0xD8BA6C47") return "CS_MITCHCOP";
            if (hex == "0x9B333E73") return "CS_MORI";
            if (hex == "0xE9C3C332") return "CS_PBXGIRL2";
            if (hex == "0x5BEB1A2D") return "CS_PHILB";
            if (hex == "0xE9F368C6") return "CS_PLAYBOYX";
            if (hex == "0x4D6DE57E") return "CS_PRIEST";
            if (hex == "0x88F35A20") return "CS_RICKY";
            if (hex == "0x626C3F77") return "CS_TOMMY";
            if (hex == "0x553CBE07") return "CS_TRAMP";
            if (hex == "0x2AF6831D") return "CS_BRIAN";
            if (hex == "0x7AE0A064") return "CS_CHARISE";
            if (hex == "0xE7AC8418") return "CS_CLARENCE";
            if (hex == "0x6463855D") return "CS_EDDIELOW";
            if (hex == "0x999B9B33") return "CS_GRACIE";
            if (hex == "0x17C32FB4") return "CS_JEFF";
            if (hex == "0x574DE134") return "CS_MARNIE";
            if (hex == "0x8B0322AF") return "CS_MARSHAL";
            if (hex == "0xD77D71DF") return "CS_PATHOS";
            if (hex == "0xEFF3F84D") return "CS_SARAH";
            if (hex == "0x42F6375E") return "CS_ROMAN_D";
            if (hex == "0x6368F847") return "CS_ROMAN_T";
            if (hex == "0xE37B786A") return "CS_ROMAN_W";
            if (hex == "0x0E37C613") return "CS_BRUCIE_B";
            if (hex == "0x0E1B45E6") return "CS_BRUCIE_T";
            if (hex == "0x765C9667") return "CS_BRUCIE_W";
            if (hex == "0x7183C75F") return "CS_BERNIE_CRANEC";
            if (hex == "0x4231E7AC") return "CS_BERNIE_CRANET";
            if (hex == "0x1B4899DE") return "CS_BERNIE_CRANEW";
            if (hex == "0xB0B4BC37") return "CS_LILJACOB_B";
            if (hex == "0x7EF858B3") return "CS_LILJACOB_J";
            if (hex == "0x5DF63F45") return "CS_MALLORIE_D";
            if (hex == "0xCC381BCB") return "CS_MALLORIE_J";
            if (hex == "0x45768E2E") return "CS_MALLORIE_W";
            if (hex == "0x8469C377") return "CS_DERRICKMC_B";
            if (hex == "0x2FBC9A1E") return "CS_DERRICKMC_D";
            if (hex == "0x7D0BADD3") return "CS_MICHAEL_B";
            if (hex == "0xCF5FD27A") return "CS_MICHAEL_D";
            if (hex == "0x4DFB1B0C") return "CS_PACKIEMC_B";
            if (hex == "0x68EED0F3") return "CS_PACKIEMC_D";
            if (hex == "0xAF3F2AC0") return "CS_KATEMC_D";
            if (hex == "0x4ABDE1C7") return "CS_KATEMC_W";
            if (hex == "0xEE0BB2A4") return "M_Y_GAFR_LO_01";
            if (hex == "0xBBD14E30") return "M_Y_GAFR_LO_02";
            if (hex == "0x33D38899") return "M_Y_GAFR_HI_01";
            if (hex == "0x25B4EC5C") return "M_Y_GAFR_HI_02";
            if (hex == "0xE1F6A366") return "M_Y_GALB_LO_01";
            if (hex == "0xF1F54363") return "M_Y_GALB_LO_02";
            if (hex == "0x0C61783B") return "M_Y_GALB_LO_03";
            if (hex == "0x1EA71CCE") return "M_Y_GALB_LO_04";
            if (hex == "0x029035B4") return "M_M_GBIK_LO_03";
            if (hex == "0x5044865F") return "M_Y_GBIK_HI_01";
            if (hex == "0x9C071DE3") return "M_Y_GBIK_HI_02";
            if (hex == "0xA8E69DBF") return "M_Y_GBIK02_LO_02";
            if (hex == "0x5DDE4F9B") return "M_Y_GBIK_LO_01";
            if (hex == "0x8B932B00") return "M_Y_GBIK_LO_02";
            if (hex == "0x10B7B44B") return "M_Y_GIRI_LO_01";
            if (hex == "0xFEDA1090") return "M_Y_GIRI_LO_02";
            if (hex == "0x6DF3EEC6") return "M_Y_GIRI_LO_03";
            if (hex == "0x5FF2E9AF") return "M_M_GJAM_HI_01";
            if (hex == "0xEC4D0269") return "M_M_GJAM_HI_02";
            if (hex == "0x4295AEF5") return "M_M_GJAM_HI_03";
            if (hex == "0xA691BED3") return "M_Y_GJAM_LO_01";
            if (hex == "0xCB77889E") return "M_Y_GJAM_LO_02";
            if (hex == "0x5BD063B5") return "M_Y_GKOR_LO_01";
            if (hex == "0x2D8D8730") return "M_Y_GKOR_LO_02";
            if (hex == "0x1D55921C") return "M_Y_GLAT_LO_01";
            if (hex == "0x8D32F1D9") return "M_Y_GLAT_LO_02";
            if (hex == "0x45A43081") return "M_Y_GLAT_HI_01";
            if (hex == "0x97E25504") return "M_Y_GLAT_HI_02";
            if (hex == "0xEDFA50E3") return "M_Y_GMAF_HI_01";
            if (hex == "0x9FA03430") return "M_Y_GMAF_HI_02";
            if (hex == "0x03DBB737") return "M_Y_GMAF_LO_01";
            if (hex == "0x1E6BEC57") return "M_Y_GMAF_LO_02";
            if (hex == "0x9290C4A3") return "M_O_GRUS_HI_01";
            if (hex == "0x83892528") return "M_Y_GRUS_LO_01";
            if (hex == "0x75CF09B4") return "M_Y_GRUS_LO_02";
            if (hex == "0x5BFE7C54") return "M_Y_GRUS_HI_02";
            if (hex == "0x6F31C4B4") return "M_M_GRU2_HI_01";
            if (hex == "0x19BB19C8") return "M_M_GRU2_HI_02";
            if (hex == "0x66CB1E64") return "M_M_GRU2_LO_02";
            if (hex == "0xB9A05501") return "M_Y_GRU2_LO_01";
            if (hex == "0x33EEB47F") return "M_M_GTRI_HI_01";
            if (hex == "0x28C09E23") return "M_M_GTRI_HI_02";
            if (hex == "0xBF635A9F") return "M_Y_GTRI_LO_01";
            if (hex == "0xF62B4836") return "M_Y_GTRI_LO_02";
            if (hex == "0xD33B8FE9") return "F_O_MAID_01";
            if (hex == "0xF97D04E6") return "F_O_BINCO";
            if (hex == "0x516F7106") return "F_Y_BANK_01";
            if (hex == "0x14A4B50F") return "F_Y_DOCTOR_01";
            if (hex == "0x507AAC5B") return "F_Y_GYMGAL_01";
            if (hex == "0x37214098") return "F_Y_FF_BURGER_R";
            if (hex == "0xEB5AB08B") return "F_Y_FF_CLUCK_R";
            if (hex == "0x8292BFB5") return "F_Y_FF_RSCAFE";
            if (hex == "0x0CB09BED") return "F_Y_FF_TWCAFE";
            if (hex == "0xEEB5DE91") return "F_Y_FF_WSPIZZA_R";
            if (hex == "0x20EF1FEB") return "F_Y_HOOKER_01";
            if (hex == "0x3B61D4D0") return "F_Y_HOOKER_03";
            if (hex == "0xB8D8632B") return "F_Y_NURSE";
            if (hex == "0x42615D12") return "F_Y_STRIPPERC01";
            if (hex == "0x50AFF9AF") return "F_Y_STRIPPERC02";
            if (hex == "0x0171C5D1") return "F_Y_WAITRESS_01";
            if (hex == "0x97093869") return "M_M_ALCOHOLIC";
            if (hex == "0x401C1901") return "M_M_ARMOURED";
            if (hex == "0x07FDDC3F") return "M_M_BUSDRIVER";
            if (hex == "0x2D243DEF") return "M_M_CHINATOWN_01";
            if (hex == "0x9313C198") return "M_M_CRACKHEAD";
            if (hex == "0x0D13AEF5") return "M_M_DOC_SCRUBS_01";
            if (hex == "0xB940B896") return "M_M_DOCTOR_01";
            if (hex == "0x16653776") return "M_M_DODGYDOC";
            if (hex == "0x7D77FE8D") return "M_M_EECOOK";
            if (hex == "0xF410AB9B") return "M_M_ENFORCER";
            if (hex == "0x2FB107C1") return "M_M_FACTORY_01";
            if (hex == "0xE9EC3678") return "M_M_FATCOP_01";
            if (hex == "0xC46CBC16") return "M_M_FBI";
            if (hex == "0x89275CA8") return "M_M_FEDCO";
            if (hex == "0x24696C93") return "M_M_FIRECHIEF";
            if (hex == "0x1CFC648F") return "M_M_GUNNUT_01";
            if (hex == "0xD19BD6D0") return "M_M_HELIPILOT_01";
            if (hex == "0x2536480C") return "M_M_HPORTER_01";
            if (hex == "0x959D9B8A") return "M_M_KOREACOOK_01";
            if (hex == "0x918DD1CF") return "M_M_LAWYER_01";
            if (hex == "0xBC5DA76E") return "M_M_LAWYER_02";
            if (hex == "0x1699B3B8") return "M_M_LOONYBLACK";
            if (hex == "0x8C0F140E") return "M_M_PILOT";
            if (hex == "0x301D7295") return "M_M_PINDUS_01";
            if (hex == "0xEF0CF791") return "M_M_POSTAL_01";
            if (hex == "0xB92CCD03") return "M_M_SAXPLAYER_01";
            if (hex == "0x907AF88D") return "M_M_SECURITYMAN";
            if (hex == "0x1916A97C") return "M_M_SELLER_01";
            if (hex == "0x6FF14E0F") return "M_M_SHORTORDER";
            if (hex == "0x0881E67C") return "M_M_STREETFOOD_01";
            if (hex == "0xD6D5085C") return "M_M_SWEEPER";
            if (hex == "0x0085DCEE") return "M_M_TAXIDRIVER";
            if (hex == "0x46B50EAA") return "M_M_TELEPHONE";
            if (hex == "0xE96555E2") return "M_M_TENNIS";
            if (hex == "0x452086C4") return "M_M_TRAIN_01";
            if (hex == "0xF7835A1A") return "M_M_TRAMPBLACK";
            if (hex == "0xFD3979FD") return "M_M_TRUCKER_01";
            if (hex == "0xB376FD38") return "M_O_JANITOR";
            if (hex == "0x015E1A07") return "M_O_HOTEL_FOOT";
            if (hex == "0x463E4B5D") return "M_O_MPMOBBOSS";
            if (hex == "0xA8B24166") return "M_Y_AIRWORKER";
            if (hex == "0x80807842") return "M_Y_BARMAN_01";
            if (hex == "0x95DCB0F5") return "M_Y_BOUNCER_01";
            if (hex == "0xE79AD470") return "M_Y_BOUNCER_02";
            if (hex == "0xD05CB843") return "M_Y_BOWL_01";
            if (hex == "0xE61EE3C7") return "M_Y_BOWL_02";
            if (hex == "0x2DCD7F4C") return "M_Y_CHINVEND_01";
            if (hex == "0x2851C93C") return "M_Y_CLUBFIT";
            if (hex == "0xD4F6DA2A") return "M_Y_CONSTRUCT_01";
            if (hex == "0xC371B720") return "M_Y_CONSTRUCT_02";
            if (hex == "0xD56DDB14") return "M_Y_CONSTRUCT_03";
            if (hex == "0xF5148AB2") return "M_Y_COP";
            if (hex == "0xA576D885") return "M_Y_COP_TRAFFIC";
            if (hex == "0xAE46285D") return "M_Y_COURIER";
            if (hex == "0xDDCCAF85") return "M_Y_COWBOY_01";
            if (hex == "0xB380C536") return "M_Y_DEALER";
            if (hex == "0x565A4099") return "M_Y_DRUG_01";
            if (hex == "0x000F192D") return "M_Y_FF_BURGER_R";
            if (hex == "0xC3B54549") return "M_Y_FF_CLUCK_R";
            if (hex == "0x75FDB605") return "M_Y_FF_RSCAFE";
            if (hex == "0xD11FBA8B") return "M_Y_FF_TWCAFE";
            if (hex == "0x0C55ACF1") return "M_Y_FF_WSPIZZA_R";
            if (hex == "0xDBA0B619") return "M_Y_FIREMAN";
            if (hex == "0x43BD9C04") return "M_Y_GARBAGE";
            if (hex == "0x358464B5") return "M_Y_GOON_01";
            if (hex == "0x8E96352C") return "M_Y_GYMGUY_01";
            if (hex == "0xEABA11B9") return "M_Y_MECHANIC_02";
            if (hex == "0xC10A9D57") return "M_Y_MODO";
            if (hex == "0x479F2007") return "M_Y_NHELIPILOT";
            if (hex == "0xF6FFEBB2") return "M_Y_PERSEUS";
            if (hex == "0x1DDEBBCF") return "M_Y_PINDUS_01";
            if (hex == "0x0B1F9651") return "M_Y_PINDUS_02";
            if (hex == "0xF958F2C4") return "M_Y_PINDUS_03";
            if (hex == "0xB9F5BEA0") return "M_Y_PMEDIC";
            if (hex == "0x9C0BF5CC") return "M_Y_PRISON";
            if (hex == "0x0CD38A07") return "M_Y_PRISONAOM";
            if (hex == "0x5C907185") return "M_Y_ROMANCAB";
            if (hex == "0xA7ABA2BA") return "M_Y_RUNNER";
            if (hex == "0x15556BF3") return "M_Y_SHOPASST_01";
            if (hex == "0xFAAD5B99") return "M_Y_STROOPER";
            if (hex == "0xC41C88BE") return "M_Y_SWAT";
            if (hex == "0xFC2BE1B8") return "M_Y_SWORDSWALLOW";
            if (hex == "0xB2F9C1A1") return "M_Y_THIEF";
            if (hex == "0x102B77F0") return "M_Y_VALET";
            if (hex == "0xF4E8205B") return "M_Y_VENDOR";
            if (hex == "0x87DB1287") return "M_Y_FRENCHTOM";
            if (hex == "0x75E29A7D") return "M_Y_JIM_FITZ";
            if (hex == "0xF3D9C032") return "F_O_PEASTEURO_01";
            if (hex == "0x0B50EF20") return "F_O_PEASTEURO_02";
            if (hex == "0xEB320486") return "F_O_PHARBRON_01";
            if (hex == "0xF92630A4") return "F_O_PJERSEY_01";
            if (hex == "0x9AD4BE64") return "F_O_PORIENT_01";
            if (hex == "0x0600A909") return "F_O_RICH_01";
            if (hex == "0x093E163C") return "F_M_BUSINESS_01";
            if (hex == "0x1780B2C1") return "F_M_BUSINESS_02";
            if (hex == "0x51FFF4A5") return "F_M_CHINATOWN";
            if (hex == "0xEF0F006B") return "F_M_PBUSINESS";
            if (hex == "0x2864B0DC") return "F_M_PEASTEURO_01";
            if (hex == "0xB92CE9DD") return "F_M_PHARBRON_01";
            if (hex == "0x844EA438") return "F_M_PJERSEY_01";
            if (hex == "0xAF1EF9D8") return "F_M_PJERSEY_02";
            if (hex == "0x3067DA63") return "F_M_PLATIN_01";
            if (hex == "0xF84BEA2C") return "F_M_PLATIN_02";
            if (hex == "0x32CEF1D1") return "F_M_PMANHAT_01";
            if (hex == "0x04901554") return "F_M_PMANHAT_02";
            if (hex == "0x81BA39A8") return "F_M_PORIENT_01";
            if (hex == "0x605DF31F") return "F_M_PRICH_01";
            if (hex == "0x1B0DCC86") return "F_Y_BUSINESS_01";
            if (hex == "0x3120FC7F") return "F_Y_CDRESS_01";
            if (hex == "0xAECAC8C7") return "F_Y_PBRONX_01";
            if (hex == "0x9568444C") return "F_Y_PCOOL_01";
            if (hex == "0xA52AE3D1") return "F_Y_PCOOL_02";
            if (hex == "0xC760585B") return "F_Y_PEASTEURO_01";
            if (hex == "0x8D2AC355") return "F_Y_PHARBRON_01";
            if (hex == "0x0A047A8F") return "F_Y_PHARLEM_01";
            if (hex == "0x0006BC78") return "F_Y_PJERSEY_02";
            if (hex == "0x0339B6D8") return "F_Y_PLATIN_01";
            if (hex == "0xEE8D8D80") return "F_Y_PLATIN_02";
            if (hex == "0x67F08048") return "F_Y_PLATIN_03";
            if (hex == "0x6392D986") return "F_Y_PMANHAT_01";
            if (hex == "0x50B8B3D2") return "F_Y_PMANHAT_02";
            if (hex == "0x3EFE105D") return "F_Y_PMANHAT_03";
            if (hex == "0xB8DA98D7") return "F_Y_PORIENT_01";
            if (hex == "0x2A8A0FF0") return "F_Y_PQUEENS_01";
            if (hex == "0x95E177F9") return "F_Y_PRICH_01";
            if (hex == "0xC73ECED1") return "F_Y_PVILLBO_02";
            if (hex == "0x5E8CD2B8") return "F_Y_SHOP_03";
            if (hex == "0x6E2671EB") return "F_Y_SHOP_04";
            if (hex == "0x9A8CFCFD") return "F_Y_SHOPPER_05";
            if (hex == "0x4680C12E") return "F_Y_SOCIALITE";
            if (hex == "0xCA5194CB") return "F_Y_STREET_02";
            if (hex == "0x110C2243") return "F_Y_STREET_05";
            if (hex == "0x57D62FD6") return "F_Y_STREET_09";
            if (hex == "0x91AFE421") return "F_Y_STREET_12";
            if (hex == "0x4CEF5CF5") return "F_Y_STREET_30";
            if (hex == "0x6F96222E") return "F_Y_STREET_34";
            if (hex == "0x6892A334") return "F_Y_TOURIST_01";
            if (hex == "0x2D6795BA") return "F_Y_VILLBO_01";
            if (hex == "0xDA0E92D1") return "M_M_BUSINESS_02";
            if (hex == "0x976C0D95") return "M_M_BUSINESS_03";
            if (hex == "0xA59C6FD2") return "M_M_EE_HEAVY_01";
            if (hex == "0x9371CB7D") return "M_M_EE_HEAVY_02";
            if (hex == "0x74636532") return "M_M_FATMOB_01";
            if (hex == "0x894A8CB2") return "M_M_GAYMID";
            if (hex == "0xBF963CE7") return "M_M_GENBUM_01";
            if (hex == "0x1D88B92A") return "M_M_LOONYWHITE";
            if (hex == "0x89BC811F") return "M_M_MIDTOWN_01";
            if (hex == "0x3F688D84") return "M_M_PBUSINESS_01";
            if (hex == "0x0C717BCE") return "M_M_PEASTEURO_01";
            if (hex == "0xC3306A8C") return "M_M_PHARBRON_01";
            if (hex == "0x6A3B66CC") return "M_M_PINDUS_02";
            if (hex == "0xAC686EC9") return "M_M_PITALIAN_01";
            if (hex == "0x9EF053D9") return "M_M_PITALIAN_02";
            if (hex == "0x450E5DBF") return "M_M_PLATIN_01";
            if (hex == "0x75633E74") return "M_M_PLATIN_02";
            if (hex == "0x60AD1508") return "M_M_PLATIN_03";
            if (hex == "0xD8CF835D") return "M_M_PMANHAT_01";
            if (hex == "0xB217B5E2") return "M_M_PMANHAT_02";
            if (hex == "0x2BC50FD3") return "M_M_PORIENT_01";
            if (hex == "0x6F2AE4DB") return "M_M_PRICH_01";
            if (hex == "0xE6372469") return "M_O_EASTEURO_01";
            if (hex == "0x9E495AD7") return "M_O_HASID_01";
            if (hex == "0x62B5E24B") return "M_O_MOBSTER";
            if (hex == "0x793F36B1") return "M_O_PEASTEURO_02";
            if (hex == "0x4E76BDF6") return "M_O_PHARBRON_01";
            if (hex == "0x3A78BA45") return "M_O_PJERSEY_01";
            if (hex == "0xB29788AB") return "M_O_STREET_01";
            if (hex == "0x0E86251C") return "M_O_SUITED";
            if (hex == "0x7C54115F") return "M_Y_BOHO_01";
            if (hex == "0x0D2FF2BF") return "M_Y_BOHOGUY_01";
            if (hex == "0x031EE9E3") return "M_Y_BRONX_01";
            if (hex == "0x5B404032") return "M_Y_BUSINESS_01";
            if (hex == "0x2924DBD8") return "M_Y_BUSINESS_02";
            if (hex == "0xBB784DE6") return "M_Y_CHINATOWN_03";
            if (hex == "0xED4319C3") return "M_Y_CHOPSHOP_01";
            if (hex == "0xDF0C7D56") return "M_Y_CHOPSHOP_02";
            if (hex == "0xBE9A3CD6") return "M_Y_DODGY_01";
            if (hex == "0x962996E4") return "M_Y_DORK_02";
            if (hex == "0x47F77FC9") return "M_Y_DOWNTOWN_01";
            if (hex == "0x5971A2B9") return "M_Y_DOWNTOWN_02";
            if (hex == "0x236BB6B2") return "M_Y_DOWNTOWN_03";
            if (hex == "0xD36D1B5D") return "M_Y_GAYYOUNG";
            if (hex == "0xD7A357ED") return "M_Y_GENSTREET_11";
            if (hex == "0x9BF260A8") return "M_Y_GENSTREET_16";
            if (hex == "0x3AF39D6C") return "M_Y_GENSTREET_20";
            if (hex == "0x4658B34E") return "M_Y_GENSTREET_34";
            if (hex == "0xAB537AD4") return "M_Y_HARDMAN_01";
            if (hex == "0xB71B0F29") return "M_Y_HARLEM_01";
            if (hex == "0x97EBD0CB") return "M_Y_HARLEM_02";
            if (hex == "0x7D701BD4") return "M_Y_HARLEM_04";
            if (hex == "0x90442A67") return "M_Y_HASID_01";
            if (hex == "0xC1181556") return "M_Y_LEASTSIDE_01";
            if (hex == "0x22522444") return "M_Y_PBRONX_01";
            if (hex == "0xFBB5AA01") return "M_Y_PCOOL_01";
            if (hex == "0xF45E1B4E") return "M_Y_PCOOL_02";
            if (hex == "0x298F268A") return "M_Y_PEASTEURO_01";
            if (hex == "0x27F5967B") return "M_Y_PHARBRON_01";
            if (hex == "0x01961E02") return "M_Y_PHARLEM_01";
            if (hex == "0x5BF734C6") return "M_Y_PJERSEY_01";
            if (hex == "0x944D1A30") return "M_Y_PLATIN_01";
            if (hex == "0xC30777A4") return "M_Y_PLATIN_02";
            if (hex == "0xB0F0D377") return "M_Y_PLATIN_03";
            if (hex == "0x243BD606") return "M_Y_PMANHAT_01";
            if (hex == "0x7554785A") return "M_Y_PMANHAT_02";
            if (hex == "0xEB7CE59F") return "M_Y_PORIENT_01";
            if (hex == "0x21673B90") return "M_Y_PQUEENS_01";
            if (hex == "0x509627D1") return "M_Y_PRICH_01";
            if (hex == "0x0D55CAAC") return "M_Y_PVILLBO_01";
            if (hex == "0xB5559AAD") return "M_Y_PVILLBO_02";
            if (hex == "0xA2E575D9") return "M_Y_PVILLBO_03";
            if (hex == "0x48E8EE31") return "M_Y_QUEENSBRIDGE";
            if (hex == "0xB73D062F") return "M_Y_SHADY_02";
            if (hex == "0x68A019EE") return "M_Y_SKATEBIKE_01";
            if (hex == "0x170C6DAE") return "M_Y_SOHO_01";
            if (hex == "0x03B99DE1") return "M_Y_STREET_01";
            if (hex == "0x1F3854DE") return "M_Y_STREET_03";
            if (hex == "0x3082F773") return "M_Y_STREET_04";
            if (hex == "0xA37B1794") return "M_Y_STREETBLK_02";
            if (hex == "0xD939030F") return "M_Y_STREETBLK_03";
            if (hex == "0xD3E34ABA") return "M_Y_STREETPUNK_02";
            if (hex == "0x8D1CBD36") return "M_Y_STREETPUNK_04";
            if (hex == "0x51E946D0") return "M_Y_STREETPUNK_05";
            if (hex == "0xBC0DDE62") return "M_Y_TOUGH_05";
            if (hex == "0x303963D0") return "M_Y_TOURIST_02";
            return string.Empty;
        }
        void SavePersonsData()
        {
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "PersistentPersonsSaves.ini"));
            //Weapons
            ini.SetValue("AK47", "WEAPONS", r_ak);
            ini.SetValue("M4", "WEAPONS", r_m4);
            //ini.SetValue("RifleAmmo", "WEAPONS", rifleAmmo);
            ini.SetValue("GLOCK", "WEAPONS", glock);
            ini.SetValue("DEAGLE", "WEAPONS", deagle);
            //ini.SetValue("PistolAmmo", "WEAPONS", pistolAmmo);
            ini.SetValue("BARETTA", "WEAPONS", baretta);
            ini.SetValue("PUMP", "WEAPONS", pump);
           // ini.SetValue("ShottyAmmo", "WEAPONS", shottyAmmo);
            ini.SetValue("UZI", "WEAPONS", uzi);
            ini.SetValue("MP5", "WEAPONS", mp5);
            //ini.SetValue("SmgAmmo", "WEAPONS", smgAmmo);
            ini.SetValue("AUTO", "WEAPONS", snipeAuto);
            ini.SetValue("BOLT", "WEAPONS", snipeBolt);
           // ini.SetValue("SniperAmmo", "WEAPONS", sniperAmmo);
            ini.SetValue("MOLOTOV", "WEAPONS", molotov);
            ini.SetValue("GRENADE", "WEAPONS", grenade);
           // ini.SetValue("ThrowAmmo", "WEAPONS", throwAmmo);
            ini.SetValue("MELEE", "WEAPONS", meleeWep);

            //Persons
            ini.SetValue("MeleeType", "PERSONS", mMeleeWepName);
            ini.SetValue("PlayerModel", "PERSONS", mPlayerModel);
            ini.SetValue("PlayerHead", "PERSONS", mPlayerHeadIndex);
            //ini.SetValue("PlayerFace", "PERSONS", mPlayerFaceIndex);
            //ini.SetValue("PlayerHair", "PERSONS", mPlayerHairIndex);
            ini.SetValue("Health", "PERSONS", mPlayerHealth);
            ini.SetValue("Armor", "PERSONS", mPlayerArmor);
            ini.SetValue("Money", "PERSONS", mPlayerMoney);
            ini.SetValue("PlayerPos", "PERSONS", mPlayerPos);
            //Save and Close
            ini.Save();
            Game.Console.Print("PersistentPersons Data Saved");
            //Game.DisplayText("PersistentPersons Data Saved", 5000);
        }
        void LoadPersonsData()
        {
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "PersistentPersonsSaves.ini"));
            ini.Load();

            r_ak = ini.GetValueInteger("AK47", "WEAPONS", r_ak);
            r_m4 = ini.GetValueInteger("M4", "WEAPONS", r_m4);
           // rifleAmmo = ini.GetValueInteger("RifleAmmo", "WEAPONS", rifleAmmo);
            glock = ini.GetValueInteger("GLOCK", "WEAPONS", glock);
            deagle = ini.GetValueInteger("DEAGLE", "WEAPONS", deagle);
           // pistolAmmo = ini.GetValueInteger("PistolAmmo", "WEAPONS", pistolAmmo);
            baretta = ini.GetValueInteger("BARETTA", "WEAPONS", baretta);
            pump = ini.GetValueInteger("PUMP", "WEAPONS", pump);
           // shottyAmmo = ini.GetValueInteger("ShottyAmmo", "WEAPONS", shottyAmmo);
            uzi = ini.GetValueInteger("UZI", "WEAPONS", uzi);
            mp5 = ini.GetValueInteger("MP5", "WEAPONS", mp5);
           // smgAmmo = ini.GetValueInteger("SmgAmmo", "WEAPONS", smgAmmo);
            snipeAuto = ini.GetValueInteger("AUTO", "WEAPONS", snipeAuto);
            snipeBolt = ini.GetValueInteger("BOLT", "WEAPONS", snipeBolt);
           // sniperAmmo = ini.GetValueInteger("SniperAmmo", "WEAPONS", sniperAmmo);
            molotov = ini.GetValueInteger("MOLOTOV", "WEAPONS", molotov);
            grenade = ini.GetValueInteger("GRENADE", "WEAPONS", grenade);
          //  throwAmmo = ini.GetValueInteger("ThrowAmmo", "WEAPONS", throwAmmo);
            meleeWep = ini.GetValueInteger("MELEE", "WEAPONS", meleeWep);

            mMeleeWepName = ini.GetValueString("MeleeType", "PERSONS", mMeleeWepName);
            mPlayerModel = ini.GetValueModel("PlayerModel", "PERSONS", mPlayerModel);
            mPlayerHeadIndex = ini.GetValueInteger("PlayerHead", "PERSONS", mPlayerHeadIndex);
            //mPlayerFaceIndex = ini.GetValueInteger("PlayerFace", "PERSONS", mPlayerFaceIndex);
           // mPlayerHairIndex = ini.GetValueInteger("PlayerHair", "PERSONS", mPlayerHairIndex);
            mPlayerHealth = ini.GetValueInteger("Health", "PERSONS", mPlayerHealth);
            mPlayerArmor = ini.GetValueInteger("Armor", "PERSONS", mPlayerArmor);
            mPlayerMoney = ini.GetValueInteger("Money", "PERSONS", mPlayerMoney);
            mPlayerPos = ini.GetValueVector3("PlayerPos", "PERSONS", mPlayerPos);
                // Apply attributes
                SetPlayerPersonsGear();
            Text("PersistentPersons Saves Loaded", 2000);
            Game.DisplayText("PersistentPersons Saves Loaded", 5000);
        }
    }
}
