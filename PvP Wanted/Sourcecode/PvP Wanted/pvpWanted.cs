using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using GTA;
using GTA.Native;
using System.Drawing;

namespace PvP_Wanted
{
    public class pvpWanted : Script
    {
        int pos;
        //blips
        Blip hitBlip = null;
        //players
        Player mpPlayer = null;
        Player mpGivePlayer = null;
        Player mpWarnPlayer = null;
        Player trashPlayer = null;

        // Weapon Storage for Drive By
        int mp5Ammo = 0;
        int uziAmmo = 0;
        int glockAmmo = 0;
        int deagleAmmo = 0;
        int grenadeAmmo = 0;
        int molotovAmmo = 0;
        //ints
        int copKillRewardLarge;
        int copKillRewardMedium;
        int copKillRewardSmall;
        int copArrestRewardLarge;
        int copArrestRewardMedium;
        int copArrestRewardSmall;
        int hitMissionReward;
        int copInnocentPenalty;
        int crookInnocentPenalty;
        int chaseCounter = 0;
        int posPed;
        int startMoney;
        int startAmmo;
        int firstTimeRobbing = 0;
        int wantedRobAmt0;
        int wantedRobAmt1;
        int wantedRobAmt2;
        int wantedRobAmt3;
        int wantedRobAmt4;
        int wantedRobAmt5;
        int wantedRobAmt6;
        int robStoreTimer;
        int timesPerRob;
        int timesPerRobCounter = 0;
        int firstLoadLocation = 0;
        int proxVoiceDist;
        int loseMoneyPercent;
        int moneyLoss;
        //Peds
        Ped innocentPed = null;
        Ped[] pedsInArea = null;
        Ped trashPed = null;
        //Keys 
        Keys givePlayerWanted;
        Keys givePlayerWarning;
        Keys robStore;
        Keys displayCash;
        MouseButtons rightC = MouseButtons.Right;
        //BOOOLS
        bool processOfArresting = false;
        bool refreshChaseWanted = false;
        bool processOfPenalyze = false;
        bool processOfPenalyzePed = false;
        bool processOfRewardPed = false;
        bool arrestMpCop;
        bool arrestMpCrook;
        bool playerHadWeapon;
        bool pedHadWeapon;
        bool rest = false;
        bool bar = false;
        bool bs = false;
        bool cb = false;
        bool hospital = false;
        bool robbingInProgress = false;
        bool hoveBeachSpawn = false;
        bool bohanSpawn = false;
        bool algSpawn = false;
        bool aldernySpawn = false;
        bool initSpawn = false;
        bool blindFire = false;
        bool driveFire = false;
        bool pvpFunctions = false;
        bool rewardSystem = false;
        bool proximitySystem = false;
        bool allowDriverShooting = false;
        bool passengerAimPenalty = false;
        bool blindFirePenalty = false;
        bool allowStoreRobbing = false;
        bool loseWeaponsDeath = false;
        bool loseMoneyDeath = false;
        bool allowGiveWanted = false;
        //fonts
        GTA.Font selectionFont;
        // Objects
        GTA.Object robObject;
        GTA.Object robInProgressObject;
        Vector3 HospitalCoord;
        float HospitalHeading,
              HospitalDistance;
              
        //strings
        string zoneArea = null;
        string zoneAreaCheck;
        List<string> zoneHbList = new List<string>();
        List<string> zoneBhList = new List<string>();
        List<string> zoneAlgList = new List<string>();
        List<string> zoneAcList = new List<string>();
        //Timers
        GTA.Timer chaseTimer_Tick;
        GTA.Timer wantedRefresh_Tick;
        int waitModifier;
        #region Places to Rob
        public static Vector3 RestHB = new Vector3(884.82f, -487.23f, 14.89f); // Heading 6.8
        public static Vector3 BarHB = new Vector3(936.04f, -490.30f, 14.49f); // Heading 186.03
        public static Vector3 BowlHB = new Vector3(1201.83f, -654.63f, 15.85f); // Heading 185.25
        public static Vector3 ComedyHB = new Vector3(947.95f, -270.76f, 17.28f); // Heading 258.92
        public static Vector3 eightBallHB = new Vector3(1466.76f, 58.70f, 24.19f); // Heading 273.87
        public static Vector3 BurgerHB = new Vector3(1637.76f, 224.94f, 24.22f); // Heading 273.16
        public static Vector3 DartsHB = new Vector3(1144.54f, 740.70f, 34.52f); // Heading 183.54
        public static Vector3 CluckinHB = new Vector3(1186.28f, 360.27f, 24.11f); // Heading 1.23
        // ---------- Hove Beach StoreClerks
        Checkpoint RestClerkHB;
        Checkpoint BarClerkHB;
        Checkpoint BowlClerkHB;
        Checkpoint ComedyClerkHB;
        Checkpoint eightBallClerkHB;
        Checkpoint BurgerClerkHB;
        Checkpoint DartsClerkHB;
        Checkpoint CluckinClerkHB;
        //----------- Hospitals Hove Beach
        public static Vector3 outHospitalHB = new Vector3(1248.64f, 484.78f, 28.54f); // Heading 20.63
        public static Vector3 inHospitalHB = new Vector3(1213.07f, 203.48f, 32.55f); // Heading 181.53
        // -----------Hove Beach Hospital Clerks
        Checkpoint outClerkHB;
        Checkpoint inClerkHB;
        // ----------Bohan, Food Shops
        public static Vector3 sClubBH = new Vector3(1189.81f, 1703.13f, 16.73f); // Heading 164.18
        public static Vector3 BurgerBH1 = new Vector3(1111.82f, 1585.12f, 15.96f); // Heading 46.88
        public static Vector3 BurgerBH2 = new Vector3(451.18f, 1503.71f, 15.32f); // Heading 31.79
        // -------- Bohan Store Clerks
        Checkpoint sClubClerkBH;
        Checkpoint BurgerClerkBH1;
        Checkpoint BurgerClerkBH2;
        // ---------Hospital Bohan
        public static Vector3 outHospitalBH = new Vector3(982.06f, 1833.77f, 22.90f); // Heading 104.29
        // --------Bohan Hospital Clerk
        Checkpoint outClerkBH;
        //----------- Algonquein Food Shops
        public static Vector3 BurgerALG1 = new Vector3(-426.45f, 1195.41f, 12.05f); // Heading 104.39
        public static Vector3 CafeALG1 = new Vector3(15.93f, 979.68f, 14.65f); // Heading 283.23
        public static Vector3 BurgerALG2 = new Vector3(-170.89f, 289.01f, 13.83f); // Heading 97.72
        public static Vector3 ClunkinALG = new Vector3(-122.12f, 71.49f, 13.81f); // Heading 135.01
        public static Vector3 DartsALG = new Vector3(-435.69f, 451.36f, 9.40f); // Heading 45.34
        public static Vector3 BurgerALG3 = new Vector3(-617.42f, 131.89f, 3.82f); // Heading 359.34
        public static Vector3 BowlALG = new Vector3(-597.43f, 81.18f, 4.22f); // Heading 289.34
        public static Vector3 CafeALG2 = new Vector3(-235.57f, 51.52f, 14.71f); // Heading 181.33
        //-----------Algonquein Store Clerks
        Checkpoint BurgerClerkALG1;
        Checkpoint CafeClerkALG1;
        Checkpoint BurgerClerkALG2;
        Checkpoint ClunkinClerkALG;
        Checkpoint DartsClerkALG;
        Checkpoint BurgerClerkALG3;
        Checkpoint BowlClerkALG;
        Checkpoint CafeClerkALG2;
        // ---------- Hospital Algonquein 
        public static Vector3 outHospitalALG1 = new Vector3(-395.01f, 1279.19f, 22.03f); // Heading 253.03
        public static Vector3 outHospitalALG2 = new Vector3(60.32f, 191.42f, 13.76f); // Heading 90.38
        public static Vector3 inHospitalALG = new Vector3(106.12f, -695.29f, 13.77f); // Heading 287.12
        // ---------- Algonquein Hospital Clerks
        Checkpoint outClerkALG1;
        Checkpoint outClerkALG2;
        //------------- Alderny City Food Shops
        public static Vector3 ChinaAC = new Vector3(-1240.21f, 1086.49f, 18.79f); // Heading 173.05
        public static Vector3 BurgerAC = new Vector3(-1005.82f, 1628.30f, 23.32f); // Heading 178.11
        public static Vector3 sClubAC = new Vector3(-1574.90f, 12.89f, 9.02f); // Heading 3.03
        // ------------ Alderny City Store Clerks
        Checkpoint ChinaClerkAC;
        Checkpoint BurgerClerkAC;
        Checkpoint sClubClerkAC;
        //------------- Hospital Alderny City
        public static Vector3 outHospitalAC = new Vector3(-1515.54f, 396.92f, 20.40f); // Heading 315.32
        public static Vector3 inHospitalAC = new Vector3(-1325.59f, 1262.53f, 22.38f); // Heading 49.95
        //------------- Alderny City Hospital Clerks
        Checkpoint outClerkAC;
        Checkpoint inClerkAC;
        bool hospitalSpawn;
        #endregion

        private int R(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
        public pvpWanted()
        {
            chaseTimer_Tick = new GTA.Timer(1000);
            chaseTimer_Tick.Tick += new EventHandler(Chase_Timer);
            wantedRefresh_Tick = new GTA.Timer(16000);
            wantedRefresh_Tick.Tick += new EventHandler(Refresh_Wanted);
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            initSpawn = Settings.GetValueBool("INIT_SPAWN", "SETTINGS", false);
            if (Game.isMultiplayer && initSpawn)
            {
                Game.LocalPlayer.CanControlCharacter = false;
                Game.LocalPlayer.Character.Invincible = true;
                Game.LocalPlayer.Character.Visible = false;
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Initializing nearest spawn point, please wait patiently, dont press anything!", 16000, 1 });
            }
            this.Tick += new EventHandler(pvpWanted_Tick);
            //this.KeyDown += new GTA.KeyEventHandler(GiveWantedKeyDown);
           // BindKey(Keys.H, true, false, false, handsUpSearch);
            copArrestRewardLarge = Settings.GetValueInteger("COP_LARGE_REWARD", "COP_REWARDS", 800);
            copArrestRewardMedium = Settings.GetValueInteger("COP_MEDIUM_REWARD", "COP_REWARDS", 600);
            copArrestRewardSmall = Settings.GetValueInteger("COP_SMALL_REWARD", "COP_REWARDS", 400);
            copKillRewardLarge = Settings.GetValueInteger("COP_KILL_LARGE_REWARD", "COP_REWARDS", 600);
            copKillRewardMedium = Settings.GetValueInteger("COP_KILL_MEDIUM_REWARD", "COP_REWARDS", 400);
            copKillRewardSmall = Settings.GetValueInteger("COP_KILL_SMALL_REWARD", "COP_REWARDS", 200);
            copInnocentPenalty = Settings.GetValueInteger("COP_INNOCENT_PENALTY", "PENALTIES", 200);
            crookInnocentPenalty = Settings.GetValueInteger("CROOK_INNOCENT_PENALTY", "PENALTIES", 200);
            givePlayerWanted = Settings.GetValueKey("GIVE_WANTED", "CONTROLS", Keys.OemSemicolon);
            //givePlayerWarning = Settings.GetValueKey("WARNING_WANTED", "CONTROLS", Keys.NumPad4);
            robStore = Settings.GetValueKey("ROB_STORE", "CONTROLS", Keys.R);
            displayCash = Settings.GetValueKey("DISPLAY_CASH_ON_CHARACTER", "CONTROLS", Keys.U);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 500);
            startMoney = Settings.GetValueInteger("START_MONEY", "SETTINGS", 0);
            startAmmo = Settings.GetValueInteger("START_AMMO", "SETTINGS", 0);
            wantedRobAmt0 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_0", "WANTED_ROB_AMTS", 25);
            wantedRobAmt1 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_1", "WANTED_ROB_AMTS", 50);
            wantedRobAmt2 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_2", "WANTED_ROB_AMTS", 75);
            wantedRobAmt3 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_3", "WANTED_ROB_AMTS", 100);
            wantedRobAmt4 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_4", "WANTED_ROB_AMTS", 200);
            wantedRobAmt5 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_5", "WANTED_ROB_AMTS", 300);
            wantedRobAmt6 = Settings.GetValueInteger("WANTED_ROB_AMOUNT_6", "WANTED_ROB_AMTS", 400);
            proxVoiceDist = Settings.GetValueInteger("PROXIMITY_VOICE_DISTANCE", "SETTINGS", 50);
            timesPerRob = Settings.GetValueInteger("TIMES_PER_ROB_LOOP", "WANTED_ROB_AMTS", 5);
            robStoreTimer = Settings.GetValueInteger("ROB_STORE_TIMER", "TIMERS", 2000);         
            pvpFunctions = Settings.GetValueBool("ALLOW_PVP_FUNCTIONS", "SETTINGS", false);
            hospitalSpawn = Settings.GetValueBool("HOSPITAL_RESPAWN", "SETTINGS", false);
            rewardSystem = Settings.GetValueBool("ALLOW_REWARD_SYSTEM", "SETTINGS", true);
            proximitySystem = Settings.GetValueBool("ALLOW_PROXIMITY_VOICE_SYSTEM", "SETTINGS", false);
            allowDriverShooting = Settings.GetValueBool("ALLOW_DRIVER_SHOOTING", "SETTINGS", false);
            passengerAimPenalty = Settings.GetValueBool("ENABLE_PASSENGER_AIM_PENALTY", "SETTINGS", true);
            blindFirePenalty = Settings.GetValueBool("ENABLE_BLIND_FIRE_PENALTY", "SETTINGS", true);
            allowStoreRobbing = Settings.GetValueBool("ALLOW_STORE_ROBBING", "SETTINGS", false);
            loseWeaponsDeath = Settings.GetValueBool("LOSE_WEAPONS_ON_DEATH", "SETTINGS", false);
            loseMoneyDeath = Settings.GetValueBool("LOSE_MONEY_ON_DEATH", "SETTINGS", false);
            loseMoneyPercent = Settings.GetValueInteger("LOSE_MONEY_PERCENTAGE", "SETTINGS", 2);
            allowGiveWanted = Settings.GetValueBool("ALLOW_GIVE_WANTED_LEVEL", "SETTINGS", false);
            #region Zone Areas Add List
            // HoveBeach
            zoneHbList.Add("ZHOVEB2"); zoneHbList.Add("ZHOVEB1"); zoneHbList.Add("ZFRIS2"); zoneHbList.Add("ZFRIS1"); zoneHbList.Add("ZBEG2"); zoneHbList.Add("ZBEG1"); zoneHbList.Add("ZFIEPR1"); zoneHbList.Add("ZFIEPR3"); zoneHbList.Add("ZFIEPR6"); zoneHbList.Add("ZFIEPR2"); zoneHbList.Add("ZHOVEB3"); zoneHbList.Add("ZEHOK1"); zoneHbList.Add("ZOUTLO"); zoneHbList.Add("ZFIEPR4"); zoneHbList.Add("ZFIEPR5"); zoneHbList.Add("ZEHOK9"); zoneHbList.Add("ZCOIS3"); zoneHbList.Add("ZCOIS4"); zoneHbList.Add("ZEHOK8"); zoneHbList.Add("ZEHOK7"); zoneHbList.Add("ZRHIL2"); zoneHbList.Add("ZBOAB1"); zoneHbList.Add("ZESTCT2"); zoneHbList.Add("ZSTEI1"); zoneHbList.Add("ZSTEI3"); zoneHbList.Add("ZRHIL10"); zoneHbList.Add("ZDOWNT"); zoneHbList.Add("ZRHIL1"); zoneHbList.Add("ZCERV1"); zoneHbList.Add("ZSTEI2"); zoneHbList.Add("ZESTCT1"); zoneHbList.Add("ZSHTLER"); zoneHbList.Add("ZESTCT3"); zoneHbList.Add("ZESTCT4"); zoneHbList.Add("ZESTCT9"); zoneHbList.Add("ZESTCT5");
            zoneHbList.Add("ZSTEI8"); zoneHbList.Add("ZSTEI7"); zoneHbList.Add("ZSTEI6"); zoneHbList.Add("ZSTEI5"); zoneHbList.Add("ZSTEI4"); zoneHbList.Add("ZSTEI11"); zoneHbList.Add("ZMPARK3"); zoneHbList.Add("ZMPARK4"); zoneHbList.Add("ZMPARK2"); zoneHbList.Add("ZAIRPT1"); zoneHbList.Add("ZMPARK1"); zoneHbList.Add("ZSTEI9"); zoneHbList.Add("ZMHILLS"); zoneHbList.Add("ZCERV2"); zoneHbList.Add("ZBECCT1"); zoneHbList.Add("ZBECCT2"); zoneHbList.Add("ZBECCT3"); zoneHbList.Add("ZBECCT4"); zoneHbList.Add("ZAIRPT2"); zoneHbList.Add("ZAIRPT3"); zoneHbList.Add("ZBECCT5"); zoneHbList.Add("ZCERV3"); zoneHbList.Add("ZRHIL3"); zoneHbList.Add("ZESTCT8"); zoneHbList.Add("ZESTCT7"); zoneHbList.Add("ZESTCT6"); zoneHbList.Add("ZWILIS1"); zoneHbList.Add("ZCERV4"); zoneHbList.Add("ZRHIL4"); zoneHbList.Add("ZRHIL5"); zoneHbList.Add("ZRHIL6"); zoneHbList.Add("ZEHOK4"); zoneHbList.Add("ZEHOK3"); zoneHbList.Add("ZEHOK2");
            // Bohan
            zoneBhList.Add("ZBOULE2"); zoneBhList.Add("ZBOULE4"); zoneBhList.Add("ZNRDNS1"); zoneBhList.Add("ZNRDNS2"); zoneBhList.Add("ZNRDNS3"); zoneBhList.Add("ZLBAY4"); zoneBhList.Add("ZLBAY5"); zoneBhList.Add("ZLBAY2"); zoneBhList.Add("ZINDUS3"); zoneBhList.Add("ZCHASE2"); zoneBhList.Add("ZSOHAN2"); zoneBhList.Add("ZSOHAN1"); zoneBhList.Add("ZFORT"); zoneBhList.Add("ZBOULE1"); zoneBhList.Add("ZINDUS6"); zoneBhList.Add("ZINDUS4"); zoneBhList.Add("ZBOULE3"); zoneBhList.Add("ZNRDNS5"); zoneBhList.Add("ZNRDNS6"); zoneBhList.Add("ZINDUS2"); zoneBhList.Add("ZNRDNS7"); zoneBhList.Add("ZINDUS1");
            // Alderny City
            zoneAcList.Add("ZACTI"); zoneAcList.Add("ZACTI1"); zoneAcList.Add("ZPENN3"); zoneAcList.Add("ZPENN1"); zoneAcList.Add("ZPENN4"); zoneAcList.Add("ZPENN5"); zoneAcList.Add("ZPORT1"); zoneAcList.Add("ZTUDO1"); zoneAcList.Add("ZPORT"); zoneAcList.Add("ZNORM"); zoneAcList.Add("ZALD4"); zoneAcList.Add("ZALD2"); zoneAcList.Add("ZLEFT1"); zoneAcList.Add("ZWEST1"); zoneAcList.Add("ZWEST2"); zoneAcList.Add("ZLEFT2"); zoneAcList.Add("ZALD3"); zoneAcList.Add("ZALD1"); zoneAcList.Add("ZBERC"); zoneAcList.Add("ZBERC1"); zoneAcList.Add("ZACT1"); zoneAcList.Add("ZACT2"); zoneAcList.Add("ZTUDO2"); zoneAcList.Add("ZPENN2");
            //Algonequen
            zoneAlgList.Add("ZEHOL1"); zoneAlgList.Add("ZLANC2"); zoneAlgList.Add("ZNORT1"); zoneAlgList.Add("ZNORT3"); zoneAlgList.Add("ZNORT2"); zoneAlgList.Add("ZEHOL2"); zoneAlgList.Add("ZLANC1"); zoneAlgList.Add("ZMIDE"); zoneAlgList.Add("ZHAT"); zoneAlgList.Add("ZEAST"); zoneAlgList.Add("ZPRES"); zoneAlgList.Add("ZTRI"); zoneAlgList.Add("ZFISN"); zoneAlgList.Add("ZLOWE2"); zoneAlgList.Add("ZLOWE1"); zoneAlgList.Add("ZFISS1"); zoneAlgList.Add("ZCHIN"); zoneAlgList.Add("ZFISS2"); zoneAlgList.Add("ZFISS3"); zoneAlgList.Add("ZLANCE"); zoneAlgList.Add("ZBRI21"); zoneAlgList.Add("ZFISS4"); zoneAlgList.Add("ZFISS5"); zoneAlgList.Add("ZEXC3"); zoneAlgList.Add("ZCGAR3"); zoneAlgList.Add("ZCGAR4"); zoneAlgList.Add("ZEXC4"); zoneAlgList.Add("ZCGAR2"); zoneAlgList.Add("ZCGAR1"); zoneAlgList.Add("ZEXC1"); zoneAlgList.Add("ZEXC2"); zoneAlgList.Add("ZSUFF1"); zoneAlgList.Add("ZSTAR"); zoneAlgList.Add("ZMIDPA"); zoneAlgList.Add("ZNORT5"); zoneAlgList.Add("ZNHOL2");
            zoneAlgList.Add("ZNHOL1"); zoneAlgList.Add("ZVARH"); zoneAlgList.Add("ZMDW3"); zoneAlgList.Add("ZMDW2"); zoneAlgList.Add("ZMDW1"); zoneAlgList.Add("ZPURG1"); zoneAlgList.Add("ZPURG2"); zoneAlgList.Add("ZPURG3"); zoneAlgList.Add("ZWESTM"); zoneAlgList.Add("ZMEAT"); zoneAlgList.Add("ZITAL"); zoneAlgList.Add("ZSUFF2"); zoneAlgList.Add("ZCGCI2"); zoneAlgList.Add("ZCITY2"); zoneAlgList.Add("ZCGCI1"); zoneAlgList.Add("ZCGCI4"); zoneAlgList.Add("ZCGCI3"); zoneAlgList.Add("ZNORT4"); 
            #endregion
        }
        void Text(string text, int duration)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, duration, 1);
        }
        void Text(string text)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, 2000, 1);
        }
        #region Kick player test
        /*void kickPlayer()
        {
            if (Game.Exists(this.mpPlayer.Character))
            {
                GTA.Multiplayer.Host.KickPlayer(this.mpPlayer);
            }
        }*/
        #endregion
        #region Hands Up or Search
        /*
        void handsUpSearch()
        {
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                this.mpPlayer = mpPlayer;
                pos = Array.IndexOf(playerList, mpPlayer);
                if (Game.LocalPlayer.Character != mpPlayer)
                {
                    if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                    {
                        //cops
                        if (Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 2 && this.mpPlayer.WantedLevel < 1 && this.mpPlayer.Character.isOnScreen)
                        {
                            string crookName = this.mpPlayer.Name.ToString();
                            AnimationSet copSearch;
                            AnimationFlags animflags = AnimationFlags.Unknown12 | AnimationFlags.Unknown11 | AnimationFlags.Unknown09;
                            Wait(1000);
                            copSearch = new AnimationSet("cop");
                            Wait(500);
                            Game.LocalPlayer.Character.Task.PlayAnimation(copSearch, "cop_search", 6.4F, animflags);
                            Game.DisplayText("Searching " + crookName + " for weapons..");
                            Wait(6000);
                            if (this.mpPlayer.Character.Weapons.AnyHandgun.Ammo > 0)
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You found guns on " + crookName + " Arrest him!", 3500, 1 });
                                Game.SendChatMessage(crookName + " , I found weapons on you, your under arrest!");
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", crookName + " is clean and free to go...", 3500, 1 });
                                Game.SendChatMessage(crookName + " , Your free to go!");
                            }
                        }
                    }
                    else
                    {
                        AnimationSet handsUp;
                        AnimationFlags animflags = AnimationFlags.Unknown12 | AnimationFlags.Unknown11 | AnimationFlags.Unknown09;
                        Wait(1000);
                        handsUp = new AnimationSet("busted");
                        Wait(500);
                        Game.LocalPlayer.Character.Task.PlayAnimation(handsUp, "idle_2_hands_up", 6.4F, animflags);
                    }
                }
            }
        }
         * */
        #endregion
        public void GiveWantedKeyDown(object sender, GTA.KeyEventArgs e)
        {
            #region Keydown WantedLevel event
            if (allowGiveWanted)
            {
                if (givePlayerWanted == e.Key)
                {
                    Player[] playerList = Game.PlayerList;
                    foreach (var mpGivePlayer in playerList)
                    {
                        this.mpGivePlayer = mpGivePlayer;
                        pos = Array.IndexOf(playerList, mpGivePlayer);

                        if (Game.LocalPlayer.Character != mpGivePlayer)
                        {
                            if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                            {
                                if (this.mpGivePlayer.Character.Model == "M_Y_COP" || this.mpGivePlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpGivePlayer.Character.Model == "CS_MITCHCOP" || this.mpGivePlayer.Character.Model == "M_M_FATCOP_01" || this.mpGivePlayer.Character.Model == "M_M_FBI" || this.mpGivePlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpGivePlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpGivePlayer.Character.Model == "M_M_ARMOURED" || this.mpGivePlayer.Character.Model == "IG_FRANCIS_MC" || this.mpGivePlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpGivePlayer.Character.Model == "M_Y_SWAT" || this.mpGivePlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                                {
                                }
                                else
                                {
                                    string crookName = this.mpGivePlayer.Name.ToString();
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 1 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 75 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 1; Game.SendChatMessage(crookName + " , this is the police! Stop right now, your under arrest!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop and be arrested", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 1 && this.mpGivePlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 75 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 1; Game.SendChatMessage(crookName + " , this is the police! Pull over! Your under arrest!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to pull over his vehicle to be arrested. (1 star)", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 2 && this.mpGivePlayer.Character.isInVehicle() && this.mpGivePlayer.Character.CurrentVehicle.Speed >= 60 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 100 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 2; this.mpGivePlayer.Character.WantedByPolice = true; Game.SendChatMessage(crookName + " , this is the police! Stop right now!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to pull over for speeding! (2 stars for speeding in a vehicle)", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 2 && this.mpGivePlayer.Character.Weapons.AnyMelee.isPresent && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 75 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 2; this.mpGivePlayer.Character.WantedByPolice = true; Game.SendChatMessage(crookName + " , this is the police! Toss the weapon and stop!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop and drop the melee weapon! (2 stars for possession of melee weapon)", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 3 && (this.mpGivePlayer.Character.Weapons.AnyHandgun.isPresent || this.mpGivePlayer.Character.Weapons.AnyAssaultRifle.isPresent || this.mpGivePlayer.Character.Weapons.AnyShotgun.isPresent || this.mpGivePlayer.Character.Weapons.AnySMG.isPresent || this.mpGivePlayer.Character.Weapons.AnySniperRifle.isPresent || this.mpGivePlayer.Character.Weapons.AnyHeavyWeapon.isPresent || this.mpGivePlayer.Character.Weapons.AnyThrown.isPresent)
                                            && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 75 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 3; this.mpGivePlayer.Character.WantedByPolice = true; Game.SendChatMessage(crookName + " , this is the police! Toss the weapon and stop!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop and throw down his gun! (3 stars for possession of gun)", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 4 && this.mpGivePlayer.Character.isShooting && (this.mpGivePlayer.Character.Weapons.AnyHandgun.isPresent || this.mpGivePlayer.Character.Weapons.AnyAssaultRifle.isPresent || this.mpGivePlayer.Character.Weapons.AnyShotgun.isPresent || this.mpGivePlayer.Character.Weapons.AnySMG.isPresent || this.mpGivePlayer.Character.Weapons.AnySniperRifle.isPresent || this.mpGivePlayer.Character.Weapons.AnyHeavyWeapon.isPresent || this.mpGivePlayer.Character.Weapons.AnyThrown.isPresent)
                                            && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 75 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 4; this.mpGivePlayer.Character.WantedByPolice = true; Game.SendChatMessage(crookName + " , this is the police! Toss the weapon and stop!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop and throw down his gun! (4 stars for possession of gun and shooting)", 10000, 1 }); }
                                    if (Game.Exists(this.mpGivePlayer.Character) && this.mpGivePlayer.WantedLevel < 5 && Game.LocalPlayer.Character.HasBeenDamagedBy(this.mpGivePlayer.Character) && Game.LocalPlayer.Character.Position.DistanceTo(this.mpGivePlayer.Character.Position) <= 100 && this.mpGivePlayer.Character.isOnScreen) { this.mpGivePlayer.WantedLevel = 5; this.mpGivePlayer.Character.WantedByPolice = true; Game.SendChatMessage(crookName + " , you have attacked an officer! Stop or I will kill you!"); Wait(1000); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop for attacking an officer! Lethal Force granted! (5 stars attacking an officer)", 10000, 1 }); Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", Game.LocalPlayer.Character); }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
           /* #region KeyDown Warning Event
            if (Keys.NumPad4 == e.Key)
            {
                Player[] playerList = Game.PlayerList;
                foreach (var mpWarnPlayer in playerList)
                {
                    if (Game.Exists(this.mpWarnPlayer.Character))
                    {
                        this.mpWarnPlayer = mpWarnPlayer;
                        pos = Array.IndexOf(playerList, mpWarnPlayer);

                        if (Game.LocalPlayer.Character != mpWarnPlayer)
                        {
                            if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                            {
                                if (this.mpWarnPlayer.Character.Model == "M_Y_COP" || this.mpWarnPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpWarnPlayer.Character.Model == "CS_MITCHCOP" || this.mpWarnPlayer.Character.Model == "M_M_FATCOP_01" || this.mpWarnPlayer.Character.Model == "M_M_FBI" || this.mpWarnPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpWarnPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpWarnPlayer.Character.Model == "M_M_ARMOURED" || this.mpWarnPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpWarnPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpWarnPlayer.Character.Model == "M_Y_SWAT" || this.mpWarnPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                                {
                                }
                                else
                                {
                                    string crookName = this.mpWarnPlayer.Name.ToString();
                                    if (Game.Exists(this.mpWarnPlayer.Character) && this.mpWarnPlayer.WantedLevel < 1 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpWarnPlayer.Character.Position) <= 75 && this.mpWarnPlayer.Character.isOnScreen) { Game.SendChatMessage(crookName + " , this is the police! Stop so I can search you!"); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stop to be searched!", 10000, 1 }); }
                                    if (Game.Exists(this.mpWarnPlayer.Character) && this.mpWarnPlayer.WantedLevel < 1 && this.mpWarnPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(this.mpWarnPlayer.Character.Position) <= 75 && this.mpWarnPlayer.Character.isOnScreen) { Game.SendChatMessage(crookName + " , this is the police! Pull over!  This is a traffic stop!"); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to pull over his vehicle for traffic stop.", 10000, 1 }); }
                                    if (Game.Exists(this.mpWarnPlayer.Character) && this.mpWarnPlayer.WantedLevel < 1 && this.mpWarnPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(this.mpWarnPlayer.Character.Position) <= 75 && this.mpWarnPlayer.Character.CurrentVehicle.Speed == 0 && this.mpWarnPlayer.Character.isOnScreen)
                                    {
                                        Game.SendChatMessage(crookName + " , please step out of the vehicle and stand still!"); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to step out of the vehicle", 10000, 1 });
                                    }
                                    else if (Game.Exists(this.mpWarnPlayer.Character) && this.mpWarnPlayer.WantedLevel < 1 && this.mpWarnPlayer.Character.isInVehicle() && this.mpWarnPlayer.Character.isOnScreen)
                                    {
                                        Game.SendChatMessage(crookName + " , please stand still so I can search you. No sudden movements."); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You order " + crookName + " to stand still so you can search him.  Watch for sudden moves.", 10000, 1 });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        #endregion
            #region fuel overwrite for GK mod
            if (e.Key == Keys.T)
            {
                Wait(50);
                Game.DisplayText("");
            }
            #endregion
        }*/
        #region tick settings
        void pvpWanted_Tick(object sender, EventArgs e)
        {
            #region Initialize spawn points
            if (firstLoadLocation == 0 && initSpawn)
            {
                Vector3 randomSpawn = Game.LocalPlayer.Character.Position.Around(150f);
                zoneArea = World.GetZoneName(Game.LocalPlayer.Character.Position);
                if (zoneHbList.Contains(zoneArea))
                {
                    hoveBeachSpawn = true;
                    firstLoadLocation++;
                    IsPlayerNearHospital();
                    Game.FadeScreenOut(100);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(HospitalCoord);
                    Game.LocalPlayer.Character.Heading = HospitalHeading;
                }
                else if (zoneBhList.Contains(zoneArea))
                {
                    bohanSpawn = true;
                    firstLoadLocation++;
                    IsPlayerNearHospital();
                    Game.FadeScreenOut(100);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(HospitalCoord);
                    Game.LocalPlayer.Character.Heading = HospitalHeading;
                }
                else if (zoneAlgList.Contains(zoneArea))
                {
                    algSpawn = true;
                    firstLoadLocation++;
                    IsPlayerNearHospital();
                    Game.FadeScreenOut(100);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(HospitalCoord);
                    Game.LocalPlayer.Character.Heading = HospitalHeading;

                }
                else if (zoneAcList.Contains(zoneArea))
                {
                    aldernySpawn = true;
                    firstLoadLocation++;
                    IsPlayerNearHospital();
                    Game.FadeScreenOut(100);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(HospitalCoord);
                    Game.LocalPlayer.Character.Heading = HospitalHeading;
                }
                else if (!algSpawn && !bohanSpawn && !aldernySpawn && !hoveBeachSpawn)
                {
                    Game.LocalPlayer.TeleportTo(randomSpawn);
                }
                if (algSpawn || bohanSpawn || aldernySpawn || hoveBeachSpawn)
                {
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    Game.LocalPlayer.Character.Visible = true;
                    Wait(500);
                    Game.FadeScreenIn(100);
                }
            }
            #endregion
            #region display cash
            if (isKeyPressed(displayCash))
            {
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You have $" + Game.LocalPlayer.Money.ToString() + " in your wallet.", 5000, 1 });
            }
            #endregion
            #region Cop or Crook SP settings
            if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER")
            {
                #region Cop Dies
                if (Game.LocalPlayer.Character.isDead && Game.isMultiplayer)
                {
                    
                    if (hospitalSpawn)
                    {
                        IsPlayerNearHospital();
                        while (Game.LocalPlayer.Character.isDead)
                        {
                            Wait(100);
                        }
                        Game.LocalPlayer.CanControlCharacter = false;
                        Game.LocalPlayer.Character.Invincible = true;
                        Game.LocalPlayer.Character.Visible = false;
                        Game.FadeScreenOut(100);
                        Wait(2000);
                        Game.LocalPlayer.TeleportTo(HospitalCoord);
                        Game.LocalPlayer.Character.Heading = HospitalHeading;
                        Wait(2000);
                        if (loseWeaponsDeath)
                            Game.LocalPlayer.Character.Weapons.RemoveAll();
                        Game.FadeScreenIn(3000);
                        Game.LocalPlayer.CanControlCharacter = true;
                        Game.LocalPlayer.Character.Invincible = false;
                        Game.LocalPlayer.Character.Visible = true;
                        Wait(2000);
                        if (loseMoneyDeath)
                        {
                            moneyLoss = (Game.LocalPlayer.Money / loseMoneyPercent);
                            Game.LocalPlayer.Money -= moneyLoss;
                        }
                        Game.DisplayText("You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000);
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000, 1 });
                        Wait(8000);
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    }
                    else
                    {
                        if (loseWeaponsDeath)
                            Game.LocalPlayer.Character.Weapons.RemoveAll();

                        if (loseMoneyDeath)
                        {
                            moneyLoss = (Game.LocalPlayer.Money / loseMoneyPercent);
                            Game.LocalPlayer.Money -= moneyLoss;
                        }
                        Game.DisplayText("You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000);
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000, 1 });
                        Wait(8000);
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    }
                }
                #endregion
            }
            else
            {
                #region Crook Dies
                if (Game.LocalPlayer.Character.isDead && Game.isMultiplayer)
                {
                    if (hospitalSpawn)
                    {
                        IsPlayerNearHospital();
                        while (Game.LocalPlayer.Character.isDead)
                        {
                            Wait(100);
                        }
                        Game.LocalPlayer.CanControlCharacter = false;
                        Game.LocalPlayer.Character.Invincible = true;
                        Game.LocalPlayer.Character.Visible = false;
                        Game.FadeScreenOut(100);
                        Wait(2000);
                        Game.LocalPlayer.TeleportTo(HospitalCoord);
                        Game.LocalPlayer.Character.Heading = HospitalHeading;
                        Wait(2000);
                        if (loseWeaponsDeath)
                            Game.LocalPlayer.Character.Weapons.RemoveAll();
                        Game.FadeScreenIn(3000);
                        Game.LocalPlayer.CanControlCharacter = true;
                        Game.LocalPlayer.Character.Invincible = false;
                        Game.LocalPlayer.Character.Visible = true;
                        Wait(2000);
                        timesPerRobCounter = 0;
                        if (loseMoneyDeath)
                        {
                            moneyLoss = (Game.LocalPlayer.Money / loseMoneyPercent);
                            Game.LocalPlayer.Money -= moneyLoss;
                        }
                        Game.DisplayText("You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000);
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000, 1 });
                        Wait(8000);
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    }
                    else
                    {
                        if (loseWeaponsDeath)
                            Game.LocalPlayer.Character.Weapons.RemoveAll();

                        timesPerRobCounter = 0;
                        if (loseMoneyDeath)
                        {
                            moneyLoss = (Game.LocalPlayer.Money / loseMoneyPercent);
                            Game.LocalPlayer.Money -= moneyLoss;
                        }
                        Game.DisplayText("You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000);
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You have died, all weapons lost and you lost $" + moneyLoss.ToString() + " dollars.", 5000, 1 });
                        Wait(8000);
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    }
                }
                #endregion
                #region Crook near robbing area?
                if (allowStoreRobbing)
                {
                    if (firstTimeRobbing == 1)
                    {
                        Wait(8000);
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "To rob this place, Hold down R, the longer R is held, the more money and wanted stars you will get", 25000, 1 });
                    }
                    if (!Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(RestHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(CafeALG1) <= 5 ||
                        Game.LocalPlayer.Character.Position.DistanceTo(CafeALG2) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(ChinaAC) <= 5)
                    {
                        rest = true;
                        firstTimeRobbing++;
                    }
                    else
                    {
                        rest = false;
                    }
                    if (!Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(BarHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(ComedyHB) <= 5 ||
                        Game.LocalPlayer.Character.Position.DistanceTo(eightBallHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(DartsHB) <= 5 ||
                        Game.LocalPlayer.Character.Position.DistanceTo(sClubBH) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(DartsALG) <= 5 ||
                        Game.LocalPlayer.Character.Position.DistanceTo(sClubAC) <= 5)
                    {
                        bar = true;
                        firstTimeRobbing++;
                    }
                    else
                    {
                        bar = false;
                    }
                    if (!Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(BowlHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerHB) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(BurgerBH1) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerBH2) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG1) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG2) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG3) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BowlALG) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(BurgerAC) <= 5)
                    {
                        bs = true;
                        firstTimeRobbing++;
                    }
                    else
                    {
                        bs = false;
                    }
                    if (!Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(ClunkinALG) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(CluckinHB) <= 5)
                    {
                        cb = true;
                        firstTimeRobbing++;
                    }
                    else
                    {
                        cb = false;
                    }
                    if (!Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(outHospitalHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(inHospitalHB) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(outHospitalBH) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(outHospitalALG1) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(outHospitalALG2) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(outHospitalAC) <= 5 ||
                     Game.LocalPlayer.Character.Position.DistanceTo(inHospitalAC) <= 5)
                    {
                        hospital = true;
                        firstTimeRobbing++;
                    }
                    else
                    {
                        hospital = false;
                    }
                }
                #endregion
            }
            #endregion
            #region Hard to Aim Passenger Shooting and In Cover 
            //Blind Fire penalty
            if (blindFirePenalty)
            {
                if (Game.Mouse.isButtonDown(MouseButtons.Left) && Function.Call<bool>("IS_PED_IN_COVER", Game.LocalPlayer.Character) && !Game.Mouse.isButtonDown(MouseButtons.Right))
                {
                    blindFire = true;
                    int randWait = R(1000, 3000);
                    Game.DisplayText("BLIND FIRE ACTIVE, HOLD RIGHT CLICK TO DISABLE!", 2500);
                    Game.CurrentCamera.DrunkEffectIntensity = 0.5f;
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                    blindFire = false;
                }
                else
                {
                    if (blindFire == false)
                    {
                        Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                        Game.CurrentCamera.DrunkEffectIntensity = 0;
                    }
                }
            }
            // Passenger Drive By
            if (passengerAimPenalty)
            {
                if (Game.LocalPlayer.Character.isInVehicle() && !(Game.LocalPlayer.Character == Game.LocalPlayer.Character.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver)) && (Game.Mouse.isButtonDown(MouseButtons.Left) || Game.Mouse.isButtonDown(MouseButtons.Right)))
                {
                    driveFire = true;
                    int randWait = R(1000, 3000);
                    Game.DisplayText("FIRING/AIMING IN MOVING VEHICLE ACTIVE", 2500);
                    Game.CurrentCamera.DrunkEffectIntensity = 0.5f;
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    driveFire = false;
                }
                else if (Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.Speed == 0 && !(Game.LocalPlayer.Character == Game.LocalPlayer.Character.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver)) && (Game.Mouse.isButtonDown(MouseButtons.Left) || Game.Mouse.isButtonDown(MouseButtons.Right)))
                {
                    driveFire = true;
                    int randWait = R(1000, 2500);
                    Game.DisplayText("FIRING/AIMING IN STILL VEHICLE ACTIVE", 2500);
                    Game.CurrentCamera.DrunkEffectIntensity = 0.5f;
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                    Wait(randWait);
                    Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 0);
                    Wait(randWait);
                    driveFire = false;
                }
                else
                {
                    if (blindFire == false && driveFire == false)
                    {
                        Function.Call("SET_GAME_CAMERA_CONTROLS_ACTIVE", 1);
                        Game.CurrentCamera.DrunkEffectIntensity = 0;
                    }
                }
            }
            #endregion
            #region Driver Driveby disabled
            if (!allowDriverShooting)
            {
                if (Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character == Game.LocalPlayer.Character.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver))
                {
                    if (Game.LocalPlayer.Character.Weapons.Glock.Ammo > 0) { glockAmmo = Game.LocalPlayer.Character.Weapons.Glock.Ammo; Game.LocalPlayer.Character.Weapons.Glock.Remove(); }
                    if (Game.LocalPlayer.Character.Weapons.DesertEagle.Ammo > 0) { deagleAmmo = Game.LocalPlayer.Character.Weapons.DesertEagle.Ammo; Game.LocalPlayer.Character.Weapons.DesertEagle.Remove(); }
                    if (Game.LocalPlayer.Character.Weapons.MP5.Ammo > 0) { mp5Ammo = Game.LocalPlayer.Character.Weapons.MP5.Ammo; Game.LocalPlayer.Character.Weapons.MP5.Remove(); }
                    if (Game.LocalPlayer.Character.Weapons.Uzi.Ammo > 0) { uziAmmo = Game.LocalPlayer.Character.Weapons.Uzi.Ammo; Game.LocalPlayer.Character.Weapons.Uzi.Remove(); }
                    if (Game.LocalPlayer.Character.Weapons.Grenades.Ammo > 0) { grenadeAmmo = Game.LocalPlayer.Character.Weapons.Grenades.Ammo; Game.LocalPlayer.Character.Weapons.Grenades.Remove(); }
                    if (Game.LocalPlayer.Character.Weapons.MolotovCocktails.Ammo > 0) { molotovAmmo = Game.LocalPlayer.Character.Weapons.MolotovCocktails.Ammo; Game.LocalPlayer.Character.Weapons.MolotovCocktails.Remove(); }
                }
                else
                {
                    Weapon switchMelee = Weapon.Misc_AnyMelee;
                    if (glockAmmo > 0) { Game.LocalPlayer.Character.Weapons.Glock.Ammo = glockAmmo; glockAmmo = 0; } else { glockAmmo = 0; }
                    if (deagleAmmo > 0) { Game.LocalPlayer.Character.Weapons.DesertEagle.Ammo = deagleAmmo; deagleAmmo = 0; } else { deagleAmmo = 0; }
                    if (mp5Ammo > 0) { Game.LocalPlayer.Character.Weapons.MP5.Ammo = mp5Ammo; mp5Ammo = 0; } else { mp5Ammo = 0; }
                    if (uziAmmo > 0) { Game.LocalPlayer.Character.Weapons.Uzi.Ammo = uziAmmo; uziAmmo = 0; } else { uziAmmo = 0; }
                    if (grenadeAmmo > 0) { Game.LocalPlayer.Character.Weapons.Grenades.Ammo = grenadeAmmo; grenadeAmmo = 0; } else { grenadeAmmo = 0; }
                    if (molotovAmmo > 0) { Game.LocalPlayer.Character.Weapons.MolotovCocktails.Ammo = molotovAmmo; molotovAmmo = 0; } else { molotovAmmo = 0; }
                }
            }
            #endregion
            
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {
                    this.mpPlayer = mpPlayer;
                    pos = Array.IndexOf(playerList, mpPlayer);
                    #region Proximity Voice Chat
                    if (proximitySystem)
                    {
                        if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) >= proxVoiceDist) //!Function.Call<bool>("NETWORK_IS_PLAYER_MUTED_BY_ME", this.mpPlayer) //NETWORK_IS_PLAYER_TALKING  - for cop radio sounds
                        {
                            if (Game.Exists(this.mpPlayer.Character))
                            {
                                Function.Call("NETWORK_SET_PLAYER_MUTED", this.mpPlayer, true);
                            }
                        }
                        else
                        {
                            if (Game.Exists(this.mpPlayer.Character))
                            {
                                Function.Call("NETWORK_SET_PLAYER_MUTED", this.mpPlayer, false);
                            }
                        }
                    }
                    #endregion
                    #region Cops and Crooks automated PvP Functionality
                    if (pvpFunctions)
                    {
                        if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.Character != this.mpPlayer.Character)
                        {
                            if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                            {
                                    copSettings();
                            }
                            else
                            {
                                    crookSettings();
                            }
                        }
                    }
                    #endregion
                }
            }
            #region Get peds around Cop
            if (rewardSystem)
            {
                pedsInArea = World.GetPeds(Game.LocalPlayer.Character.Position, 50f);
                foreach (var innocentPed in pedsInArea)
                {
                    if (Game.Exists(innocentPed))
                    {
                        this.innocentPed = innocentPed;
                        if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                        {
                            if (Game.Exists(this.mpPlayer.Character) && this.innocentPed != this.mpPlayer.Character)
                            {
                                if (Game.Exists(this.innocentPed) && this.innocentPed.PedType != PedType.Cop && (this.innocentPed.Weapons.AnyAssaultRifle.isPresent || this.innocentPed.Weapons.AnyHandgun.isPresent || this.innocentPed.Weapons.AnyHeavyWeapon.isPresent || this.innocentPed.Weapons.AnyShotgun.isPresent || this.innocentPed.Weapons.AnySMG.isPresent || this.innocentPed.Weapons.AnySniperRifle.isPresent || this.innocentPed.Weapons.AnyThrown.isPresent))
                                {
                                    pedHadWeapon = true;
                                    Reward_CopPed();
                                }
                                else
                                {
                                    if (Game.Exists(this.innocentPed))
                                    {
                                        Penalyze_CopPed();
                                    }
                                }
                            }
                            else
                            {
                                if (Game.Exists(this.innocentPed) && this.innocentPed.PedType != PedType.Cop && (this.innocentPed.Weapons.AnyAssaultRifle.isPresent || this.innocentPed.Weapons.AnyHandgun.isPresent || this.innocentPed.Weapons.AnyHeavyWeapon.isPresent || this.innocentPed.Weapons.AnyShotgun.isPresent || this.innocentPed.Weapons.AnySMG.isPresent || this.innocentPed.Weapons.AnySniperRifle.isPresent || this.innocentPed.Weapons.AnyThrown.isPresent))
                                {
                                    pedHadWeapon = true;
                                    Reward_CopPed();
                                }
                                else
                                {
                                    if (Game.Exists(this.innocentPed))
                                    {
                                        Penalyze_CopPed();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Reward Cop Ped
        void Reward_CopPed()
        {
            if (Game.Exists(this.innocentPed) && this.innocentPed.HasBeenDamagedBy(Game.LocalPlayer.Character))
            {
                if (pedHadWeapon && !this.innocentPed.isAliveAndWell && this.innocentPed != null)
                {
                    Game.LocalPlayer.Money = Game.LocalPlayer.Money += copKillRewardSmall;
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a small reward for killing or assiting in killing an armed suspect", 5000, 1 });
                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.innocentPed);
                    pedHadWeapon = false;
                }
            }
        }
        #endregion
        #region Penalyze Cop Ped
        void Penalyze_CopPed()
        {
            if (Game.Exists(this.innocentPed) && !Game.LocalPlayer.Character.HasBeenDamagedBy(this.innocentPed) && this.innocentPed.HasBeenDamagedBy(Game.LocalPlayer.Character) && !(this.innocentPed.Weapons.AnyAssaultRifle.isPresent || this.innocentPed.Weapons.AnyHandgun.isPresent || this.innocentPed.Weapons.AnyHeavyWeapon.isPresent || this.innocentPed.Weapons.AnyShotgun.isPresent || this.innocentPed.Weapons.AnySMG.isPresent || this.innocentPed.Weapons.AnySniperRifle.isPresent || this.innocentPed.Weapons.AnyThrown.isPresent))
            {
                if (!pedHadWeapon && !this.innocentPed.isAliveAndWell && this.innocentPed != null)
                {
                    Game.LocalPlayer.Money = Game.LocalPlayer.Money -= copInnocentPenalty;
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You were penalized for killing or assisting in killing an innocent unarmed pedestrian", 8000, 1 });
                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                    Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.innocentPed);
                    pedHadWeapon = false;
                }
            }
        }
        #endregion
        
        #region copsettings
        void copSettings()
        {
            #region Wantedlevel Chase settings as cops
            if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
            {
            }
            else
            {
                if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 0) { if (!chaseTimer_Tick.isRunning) { chaseTimer_Tick.Start(); } } else { if (chaseTimer_Tick.isRunning) { chaseTimer_Tick.Stop(); } }
                if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 0 && refreshChaseWanted)
                {
                    //Cop within distance of wanted player and on screen siren off (on/off foot)
                    if (Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 100 && this.mpPlayer.Character.isOnScreen)
                    {
                        switch (this.mpPlayer.WantedLevel)
                        {
                            case 1: this.mpPlayer.WantedLevel = 1; break;
                            case 2: this.mpPlayer.WantedLevel = 2; break;
                            case 3: this.mpPlayer.WantedLevel = 3; break;
                            case 4: this.mpPlayer.WantedLevel = 4; break;
                            case 5: this.mpPlayer.WantedLevel = 5; break;
                            case 6: this.mpPlayer.WantedLevel = 6; break;
                        }
                    }
                    //Cop in vehicle bonus distance wanted level
                    if (Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 150 && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.SirenActive && this.mpPlayer.Character.isOnScreen) 
                    {
                        switch (this.mpPlayer.WantedLevel)
                        {
                            case 1: this.mpPlayer.WantedLevel = 1; break;
                            case 2: this.mpPlayer.WantedLevel = 2; break;
                            case 3: this.mpPlayer.WantedLevel = 3; break;
                            case 4: this.mpPlayer.WantedLevel = 4; break;
                            case 5: this.mpPlayer.WantedLevel = 5; break;
                            case 6: this.mpPlayer.WantedLevel = 6; break;
                        }
                    }
                }
            #endregion
                #region Cop arresting player
                if (!processOfArresting)
                {
                    /*---busted
                    //idle_2_hands_up
                    //----cop
                    //cop_cuff
                       crim_cuffed*/
                    if (Game.Exists(this.mpPlayer.Character) && !Game.LocalPlayer.Character.isOnFire && !Game.LocalPlayer.Character.isRagdoll && !Game.LocalPlayer.Character.isInVehicle() && this.mpPlayer.Character.isOnScreen && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 3 && this.mpPlayer.WantedLevel > 0 && this.mpPlayer.Character.isOnFire && (!this.mpPlayer.Character.Weapons.AnyAssaultRifle.isPresent || !this.mpPlayer.Character.Weapons.AnyHandgun.isPresent || !this.mpPlayer.Character.Weapons.AnyHeavyWeapon.isPresent || !this.mpPlayer.Character.Weapons.AnyShotgun.isPresent || !this.mpPlayer.Character.Weapons.AnyMelee.isPresent || !this.mpPlayer.Character.Weapons.AnySMG.isPresent || !this.mpPlayer.Character.Weapons.AnyThrown.isPresent))
                    {
                        processOfArresting = true;
                        string crookName = this.mpPlayer.Name.ToString();
                        Game.LocalPlayer.CanControlCharacter = false;
                        Game.LocalPlayer.Character.Invincible = true;
                        Game.DisplayText("Process of Arresting " + crookName + "...");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "In Process of arresting " + crookName + "...", 3500, 1 });
                        while (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.isRagdoll) { Wait(10); }
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            this.mpPlayer.Character.Animation.Play(new AnimationSet("busted"), "idle_2_hands_up", 6.4f, AnimationFlags.Unknown09 | AnimationFlags.Unknown11 | AnimationFlags.Unknown12);
                        } else { return; }
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.DisplayText("Arresting " + crookName + "...");
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Arresting " + crookName + "...", 2500, 1 });
                            this.mpPlayer.Character.Animation.Play(new AnimationSet("cop"), "crim_cuffed", 6.4f, AnimationFlags.Unknown09 | AnimationFlags.Unknown11 | AnimationFlags.Unknown12);
                            Game.LocalPlayer.Character.Animation.Play(new AnimationSet("cop"), "cop_cuff", 6.4f, AnimationFlags.Unknown09 | AnimationFlags.Unknown11 | AnimationFlags.Unknown12);
                        } else { return; }
                        Wait(4000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.LocalPlayer.CanControlCharacter = true;
                            Game.LocalPlayer.Character.Invincible = false;
                            processOfArresting = false;
                            Game.DisplayText(crookName + " apprehended!");
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", crookName + " apprehended!", 2500, 1 });
                        } else { return; }
                        Wait(1000);
                        if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 0 && this.mpPlayer.WantedLevel < 3) { Game.LocalPlayer.Money = Game.LocalPlayer.Money += copArrestRewardSmall; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a small reward for arresting " + crookName, 5000, 1 }); Wait(3000); Game.LocalPlayer.Money = Game.LocalPlayer.Money += this.mpPlayer.Money; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You get " + crookName + "'s $" + this.mpPlayer.Money.ToString() + " .", 5000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 1 or 2
                        else if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 2 && this.mpPlayer.WantedLevel < 5) { Game.LocalPlayer.Money = Game.LocalPlayer.Money += copArrestRewardMedium; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a medium reward for arresting " + crookName, 5000, 1 }); Wait(3000); Game.LocalPlayer.Money = Game.LocalPlayer.Money += this.mpPlayer.Money; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You get " + crookName + "'s $" + this.mpPlayer.Money.ToString() + " .", 5000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 3 or 4
                        else if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 4) { Game.LocalPlayer.Money = Game.LocalPlayer.Money += copArrestRewardLarge; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a large reward for arresting " + crookName, 5000, 1 }); Wait(3000); Game.LocalPlayer.Money = Game.LocalPlayer.Money += this.mpPlayer.Money; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You get " + crookName + "'s $" + this.mpPlayer.Money.ToString() + " .", 5000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 5 or 6
                        Wait(1000);
                        processOfArresting = false;
                    }

                }

                #endregion
                //FIND_NETWORK_KILLER_OF_PLAYER
                #region Cop Reward for Killing Wanted Player
                if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 0 && this.mpPlayer.WantedLevel < 3 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character) && (this.mpPlayer.Character.Weapons.AnyHandgun.isPresent || this.mpPlayer.Character.Weapons.AnyAssaultRifle.isPresent || this.mpPlayer.Character.Weapons.AnyShotgun.isPresent || this.mpPlayer.Character.Weapons.AnySMG.isPresent || this.mpPlayer.Character.Weapons.AnySniperRifle.isPresent || this.mpPlayer.Character.Weapons.AnyHeavyWeapon.isPresent || this.mpPlayer.Character.Weapons.AnyThrown.isPresent))
                {
                    playerHadWeapon = true;
                    if (Game.Exists(this.mpPlayer.Character) && playerHadWeapon && this.mpPlayer.Character.isDead)
                    {
                        string crookName = this.mpPlayer.Name.ToString();
                        Game.LocalPlayer.Money = Game.LocalPlayer.Money += copKillRewardSmall;
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a small reward for killing or assiting to kill an armed suspect named " + crookName, 5000, 1 });
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character);
                        playerHadWeapon = false;
                        Wait(3000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.LocalPlayer.Money = Game.LocalPlayer.Money += this.mpPlayer.Money;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You get " + crookName + "'s $" + this.mpPlayer.Money.ToString() + " .", 5000, 1 });
                        } else { return; }
                    }
                }
                if (Game.Exists(this.mpPlayer.Character) && !playerHadWeapon && this.mpPlayer.WantedLevel > 2 && this.mpPlayer.WantedLevel < 5 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character) && (this.mpPlayer.Character.Weapons.AnyHandgun.isPresent || this.mpPlayer.Character.Weapons.AnyAssaultRifle.isPresent || this.mpPlayer.Character.Weapons.AnyShotgun.isPresent || this.mpPlayer.Character.Weapons.AnySMG.isPresent || this.mpPlayer.Character.Weapons.AnySniperRifle.isPresent || this.mpPlayer.Character.Weapons.AnyHeavyWeapon.isPresent || this.mpPlayer.Character.Weapons.AnyThrown.isPresent))
                {
                    if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.isDead)
                    {
                        string crookName = this.mpPlayer.Name.ToString();
                        Game.LocalPlayer.Money = Game.LocalPlayer.Money += copKillRewardMedium;
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a medium reward for killing or assiting to kill an armed suspect named " + crookName, 5000, 1 });
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character);
                        Wait(3000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.LocalPlayer.Money = Game.LocalPlayer.Money += this.mpPlayer.Money;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You get " + crookName + "'s $" + this.mpPlayer.Money.ToString() + " .", 5000, 1 });
                        } else { return; }
                    }
                }
                if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.WantedLevel > 4 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character) && (this.mpPlayer.Character.Weapons.AnyHandgun.isPresent || this.mpPlayer.Character.Weapons.AnyAssaultRifle.isPresent || this.mpPlayer.Character.Weapons.AnyShotgun.isPresent || this.mpPlayer.Character.Weapons.AnySMG.isPresent || this.mpPlayer.Character.Weapons.AnySniperRifle.isPresent || this.mpPlayer.Character.Weapons.AnyHeavyWeapon.isPresent || this.mpPlayer.Character.Weapons.AnyThrown.isPresent))
                {
                    if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.isDead)
                    {
                        string crookName = this.mpPlayer.Name.ToString();
                        Game.LocalPlayer.Money = Game.LocalPlayer.Money += copKillRewardLarge;
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You earned a large reward for killing or assiting to kill an armed suspect named " + crookName, 5000, 1 });
                        Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character);

                    }
                }
                #endregion
                #region cop penalty for killing unarmed suspect
                if (this.mpPlayer.Character.isAlive)
                {
                    if (this.mpPlayer.WantedLevel == 0 && !Game.LocalPlayer.Character.HasBeenDamagedBy(this.mpPlayer.Character) && (this.mpPlayer.Character.Weapons.AnyMelee.isPresent || this.mpPlayer.Character.Weapons.Unarmed.isPresent))
                    {
                        if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.isDead)
                        {
                            string crookName = this.mpPlayer.Name.ToString();
                            Game.LocalPlayer.Money = Game.LocalPlayer.Money -= copInnocentPenalty;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You were penalized for killing or assisting in killing innocent civilian " + crookName, 5000, 1 });
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                    }
                }
                #endregion
            }
        }
        #endregion
        #region Hospital Locator
        void IsPlayerNearHospital()
        {
            List<Vector3> HospitalCoordList = new List<Vector3>();
            List<float> HospitalHeadingList = new List<float>();
            FillHospitalLists(HospitalCoordList, HospitalHeadingList);
            HospitalDistance = float.MaxValue;
            float tempDistance;
            for (int i = 0; i < HospitalCoordList.Count; i++)
            {
                tempDistance = Game.LocalPlayer.Character.Position.DistanceTo(HospitalCoordList[i]);
                if (tempDistance < HospitalDistance)
                {
                    HospitalDistance = tempDistance;
                    HospitalCoord = HospitalCoordList[i];
                    HospitalHeading = HospitalHeadingList[i];
                }
            }
        } // end Hospital Locator
        #endregion
        #region Hospital Vectors
        void FillHospitalLists(List<Vector3> HospitalCoordList, List<float> HospitalHeadingList)
        {

            if (hospitalSpawn)
            { 
                //----------- Hospitals Hove Beach
                HospitalCoordList.Add(new Vector3(1250.64f, 486.78f, 28.54f)); // Heading 20.63
                HospitalHeadingList.Add(20.63F);
                HospitalCoordList.Add(new Vector3(1213.13f, 201.43f, 32.55f)); // Heading 181.53
                HospitalHeadingList.Add(181.53F);
                // ---------Hospital Bohan
                HospitalCoordList.Add(new Vector3(980.06f, 1831.77f, 22.90f)); // Heading 104.29
                HospitalHeadingList.Add(104.29F);
                // ---------- Hospital Algonquein 
                HospitalCoordList.Add(new Vector3(-393.01f, 1281.19f, 22.03f)); // Heading 253.03
                HospitalHeadingList.Add(253.03F);
                HospitalCoordList.Add(new Vector3(62.32f, 192.42f, 13.76f)); // Heading 90.38
                HospitalHeadingList.Add(90.38F);
                //------------- Hospital Alderny City
                HospitalCoordList.Add(new Vector3(-1514.54f, 398.92f, 20.40f)); // Heading 315.32
                HospitalHeadingList.Add(315.32F);
                HospitalCoordList.Add(new Vector3(-1327.80f, 1262.53f, 22.38f)); // Heading 49.95
                HospitalHeadingList.Add(49.95F);

            }
            if (hoveBeachSpawn == true)
            {
                //----------- Hospitals Hove Beach
                HospitalCoordList.Add(new Vector3(1250.64f, 486.78f, 28.54f)); // Heading 20.63
                HospitalHeadingList.Add(20.63F);
                HospitalCoordList.Add(new Vector3(1213.13f, 201.43f, 32.55f)); // Heading 181.53
                HospitalHeadingList.Add(181.53F);
            }
            else if (bohanSpawn == true)
            {
                // ---------Hospital Bohan
                HospitalCoordList.Add(new Vector3(980.06f, 1831.77f, 22.90f)); // Heading 104.29
                HospitalHeadingList.Add(104.29F);
            }
            else if (algSpawn == true)
            {
                // ---------- Hospital Algonquein 
                HospitalCoordList.Add(new Vector3(-393.01f, 1281.19f, 22.03f)); // Heading 253.03
                HospitalHeadingList.Add(253.03F);
                HospitalCoordList.Add(new Vector3(62.32f, 192.42f, 13.76f)); // Heading 90.38
                HospitalHeadingList.Add(90.38F);
            }
            else if (aldernySpawn == true)
            {
                //------------- Hospital Alderny City
                HospitalCoordList.Add(new Vector3(-1514.54f, 398.92f, 20.40f)); // Heading 315.32
                HospitalHeadingList.Add(315.32F);
                HospitalCoordList.Add(new Vector3(-1327.80f, 1262.53f, 22.38f)); // Heading 49.95
                HospitalHeadingList.Add(49.95F);
            }
            else { return; }
        }
        #endregion
        #region crook settings
        void crookSettings()
        {
            #region OutOfBoundsCheck
            zoneAreaCheck = World.GetZoneName(Game.LocalPlayer.Character.Position);
            if (bohanSpawn)
            {
                if (zoneAcList.Contains(zoneAreaCheck) || zoneAlgList.Contains(zoneAreaCheck) || zoneHbList.Contains(zoneAreaCheck))
                {
                    if (!wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Start();
                        
                }
                else
                    if (wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Stop();
            }
            if (hoveBeachSpawn)
            {
                if (zoneAcList.Contains(zoneAreaCheck) || zoneAlgList.Contains(zoneAreaCheck) || zoneBhList.Contains(zoneAreaCheck))
                {
                    if (!wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Start();

                }
                else
                    if (wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Stop();
            }
            if (algSpawn)
            {
                if (zoneAcList.Contains(zoneAreaCheck) || zoneBhList.Contains(zoneAreaCheck) || zoneHbList.Contains(zoneAreaCheck))
                {
                    if (!wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Start();

                }
                else
                    if (wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Stop();
            }
            if (aldernySpawn)
            {
                if (zoneBhList.Contains(zoneAreaCheck) || zoneAlgList.Contains(zoneAreaCheck) || zoneHbList.Contains(zoneAreaCheck))
                {
                    if (!wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Start();

                }
                else
                    if (wantedRefresh_Tick.isRunning)
                        wantedRefresh_Tick.Stop();
            }
            #endregion
            #region Crook Robbing Store
            if (isKeyPressed(robStore) && Game.LocalPlayer.Character.isAlive && (rest || cb || bs || hospital || bar))
            {
                robbingInProgress = true;
                timesPerRobCounter++;
                switch (Game.LocalPlayer.WantedLevel)
                {
                    case 0:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt0.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt0.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt0;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 1:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt1.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt1.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt1;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 2:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt2.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt2.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt2;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 3:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt3.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt3.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt3;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 4:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt4.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt4.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt4;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 5:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt5.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt5.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt5;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                    case 6:
                        Game.DisplayText("Robbing store, earning $" + wantedRobAmt6.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds");
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Robbing store, earning $" + wantedRobAmt6.ToString() + " every " + (robStoreTimer / 1000).ToString() + " seconds", 1000, 1 });
                        Game.LocalPlayer.Money += wantedRobAmt6;
                        if (timesPerRobCounter >= timesPerRob) { timesPerRobCounter = 0; Game.LocalPlayer.WantedLevel += 1; }
                        break;
                }
                Wait(robStoreTimer);
                if (!isKeyPressed(robStore) || (!rest && !cb && !bs && !hospital && !bar))
                { robbingInProgress = false; }
                if (!robbingInProgress) { Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You stopped the robbery, continue by pressing and holding " + robStore.ToString() + " key and being close to counter of store.", 8000, 1 }); }
            }
            else { robbingInProgress = false; }
            #endregion
            #region Crook getting arrested
            //---busted
            //idle_2_hands_up
            //----cop
            //cop_cuff
            //crim_cuffed
            if (!processOfArresting)
            {
                if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                {
                    if (!this.mpPlayer.Character.isOnFire && !this.mpPlayer.Character.isRagdoll && !this.mpPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 3 && Game.LocalPlayer.WantedLevel > 0 && Game.LocalPlayer.Character.isOnFire && (!Game.LocalPlayer.Character.Weapons.AnyAssaultRifle.isPresent || !Game.LocalPlayer.Character.Weapons.AnyHandgun.isPresent || !Game.LocalPlayer.Character.Weapons.AnyHeavyWeapon.isPresent || !Game.LocalPlayer.Character.Weapons.AnyShotgun.isPresent || !Game.LocalPlayer.Character.Weapons.AnyMelee.isPresent || !Game.LocalPlayer.Character.Weapons.AnySMG.isPresent || !Game.LocalPlayer.Character.Weapons.AnyThrown.isPresent))
                    {
                        IsPlayerNearHospital();
                        Game.LocalPlayer.CanControlCharacter = false;
                        Game.LocalPlayer.Character.Invincible = true;
                        string copName = this.mpPlayer.Name.ToString();
                        processOfArresting = true;
                        while (Game.LocalPlayer.Character.isRagdoll) { Wait(100); }
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Vector3 copAttach = this.mpPlayer.Character.GetOffsetPosition(new Vector3(0.0f, 0.8f, 0.4f));
                            Game.LocalPlayer.TeleportTo(copAttach);
                            float copHeading = this.mpPlayer.Character.Heading;
                            Game.LocalPlayer.Character.Heading = copHeading;
                        } else { return; }
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.LocalPlayer.Character.Animation.Play(new AnimationSet("busted"), "idle_2_hands_up", 6.4f, AnimationFlags.Unknown09 | AnimationFlags.Unknown11 | AnimationFlags.Unknown12);
                        } else { return; }
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Game.DisplayText("You are being arrested by officer " + copName + "...");
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your are being arrested by officer " + copName + "...", 2500, 1 });
                            Game.LocalPlayer.Character.Animation.Play(new AnimationSet("cop"), "crim_cuffed", 6.4f, AnimationFlags.Unknown09 | AnimationFlags.Unknown11 | AnimationFlags.Unknown12);
                       
                        } else { return; }
                        Wait(4000);
                        if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.WantedLevel > 0 && Game.LocalPlayer.WantedLevel < 3) { Game.LocalPlayer.Money = Game.LocalPlayer.Money / 25; Game.LocalPlayer.Character.Weapons.RemoveAll(); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Arrested by officer " + copName + ".  You lose all weapons and 25% of your money", 10000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 1 or 2
                        else if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.WantedLevel > 2 && Game.LocalPlayer.WantedLevel < 5) { Game.LocalPlayer.Money = Game.LocalPlayer.Money / 50; Game.LocalPlayer.Character.Weapons.RemoveAll(); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Arrested by officer " + copName + ".  You lose all weapons and 50% of your money", 10000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 3 or 4
                        else if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.WantedLevel > 5) { Game.LocalPlayer.Money = Game.LocalPlayer.Money / 75; Game.LocalPlayer.Character.Weapons.RemoveAll(); Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Arrested by officer " + copName + ".  You lose all weapons and 75% of your money", 10000, 1 }); Function.Call("DISPLAY_CASH", new Parameter[] { 1 }); } // wanted level 5 or 6
                        Game.LocalPlayer.WantedLevel = 0;
                        Wait(8000);
                        Game.FadeScreenOut(100);
                        Wait(2000);
                        Game.LocalPlayer.TeleportTo(HospitalCoord);
                        Game.LocalPlayer.Character.Heading = HospitalHeading;
                        Wait(2000);
                        Game.FadeScreenIn(3000);
                        Wait(2000);
                        if (Game.Exists(this.mpPlayer.Character))
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Arrested by officer " + copName + ".  You have been released at the nearest hospital and are free to go.", 5000, 1 });
                        }
                        Game.LocalPlayer.Character.Invincible = false;
                        Game.LocalPlayer.CanControlCharacter = true;
                        processOfArresting = false;

                    }
                }
            }
            #endregion
            #region Crook Auto Wanted for Player Officers if in distance
            if (Game.Exists(this.mpPlayer.Character) && (this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER")) //-- Is LocalPlayer a cop?
            {
                string officersNearBy = this.mpPlayer.Name.ToString();
                if (Game.LocalPlayer.WantedLevel < 3 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 75 && Game.LocalPlayer.Character.isShooting) { Game.LocalPlayer.WantedLevel = 3; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Officer " + officersNearBy + " heard you fire a weapon!", 5000, 1 }); }
                if (Game.LocalPlayer.WantedLevel < 1 && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 100 && Game.LocalPlayer.Character.CurrentVehicle.Speed > 60) { this.mpPlayer.WantedLevel = 2; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Officer " + officersNearBy + " saw you speeding!", 5000, 1 }); }
                if (Game.LocalPlayer.WantedLevel < 1 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 75 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character)) { Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character); Game.LocalPlayer.WantedLevel = 2; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Officer " + officersNearBy + " saw you attack a pedestrian!", 5000, 1 }); }
                if (Game.LocalPlayer.WantedLevel < 4 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 100 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character)) { Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character); Game.LocalPlayer.WantedLevel = 4; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You attacked Officer " + officersNearBy + ".", 5000, 1 }); }
                if (Game.LocalPlayer.WantedLevel < 4 && Game.LocalPlayer.Character.Position.DistanceTo(this.mpPlayer.Character.Position) <= 100 && this.mpPlayer.Character.HasBeenDamagedBy(Game.LocalPlayer.Character) && (this.mpPlayer.Character.Health <= 0 || !this.mpPlayer.Character.isAliveAndWell)) { Function.Call("CLEAR_CHAR_LAST_DAMAGE_ENTITY", this.mpPlayer.Character); Game.LocalPlayer.WantedLevel = 4; Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You killed or assisted in killing Officer " + officersNearBy + ".", 5000, 1 }); }
            }
            #endregion
        }
        #endregion
        #region Chase Timer for Cops
        void Chase_Timer(object sender, EventArgs e)
        {
            chaseCounter++;
            if (chaseCounter == 1)
            {
                chaseCounter = 0;
                refreshChaseWanted = true;
            }
            else
            {
                refreshChaseWanted = false;
            }
            if (!chaseTimer_Tick.isRunning)
                chaseCounter = 0;
        }
        #endregion
        #region Refresh Wanted Timer
        void Refresh_Wanted(object sender, EventArgs e)
        {
            if (bohanSpawn)
            Text("Your in another area other than Bohan.  Get back to Bohan! You are highly wanted!", 6000);
            else if (hoveBeachSpawn)
                Text("Your in another area other than Hove Beach.  Get back to Hove Beach! You are highly wanted!", 6000);
            else if (algSpawn)
                Text("Your in another area other than Algonuin.  Get back to Algonuin! You are highly wanted!", 6000);
            else if (aldernySpawn)
                Text("Your in another area other than Alderny City.  Get back to Alderny City! You are highly wanted!", 6000);
            Game.LocalPlayer.WantedLevel = 6;
        }
        #endregion
        /*#region Per Frame Drawing
        void Wanted_PerFrameDrawing(object sender, GraphicsEventArgs e)
        {
            selectionFont = new GTA.Font(34.0F, FontScaling.Pixel);
            selectionFont.Color = Color.OrangeRed;
            int xPosition = 200;
            int yPosition = 250;
            int width = 1100;
            int height = 50;
            RectangleF m0 = new RectangleF(xPosition, yPosition + 40, width, height);
            e.Graphics.Scaling = FontScaling.Pixel;
            e.Graphics.DrawText("ZONE NAME: " + World.GetZoneName(Game.LocalPlayer.Character.Position).ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
        }
        #endregion*/
    }

}