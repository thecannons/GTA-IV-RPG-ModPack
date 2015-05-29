using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using GTA;
using System.IO;
using GTA.Native;
namespace FoodMod
{
    public class FoodModCS : Script
    {
        #region Declaring
        bool lowHunger = true;
        bool lowThirst = true;
        Keys toggleAutoSave;
        bool autoSaveEnabled = false;
        // ----------Hove Beach, Food Shops
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
        // SafeHouse Vectors
        // Bohan
        public static Vector3 enterShBh = new Vector3(596.643f, 1400.75f, 11.308f);
        public static Vector3 exitShBh = new Vector3(598.712f, 1401.21f, 11.6097f); // heading 267.35
        Checkpoint entranceBh;
        Blip shBhBlip;
        //Algonquein
        public static Vector3 enterMidAlg = new Vector3(97.4179f, 840.378f, 15.99f);
        public static Vector3 exitMidAlg = new Vector3(95.8987f, 852.004f, 45.25f); //heading 185.03
        public static Vector3 enterNorthAlg = new Vector3(-429.417f, 1474.45f, 20.35f);
        public static Vector3 exitNorthAlg = new Vector3(-426.533f, 1467.37f, 39.16f); // heading 268.45
        Checkpoint entranceMidAlg;
        Checkpoint entranceNorthAlg;
        Blip midAlgBlip;
        Blip northAlgBlip;
        //Alderny City
        public static Vector3 enterAc = new Vector3(-963.042f, 894.175f, 14.00f);
        Checkpoint entranceAc;
        Blip shAcBlip;
        //Hove Beach
        public static Vector3 enterHb = new Vector3(896.975f, -504.954f, 15.30f);
        Checkpoint entranceHb;
        Blip shHbBlip;
        int autoSaveInterval;
        // FOOD TIMERS
        int mHungerNeeds;
        int mThirstNeeds;
        int countHunger = 0;
        int countThirst = 0;
        int inventorySpace = 0;
        int MaxInventorySpace;
        //Objects
        GTA.Object bsBurger;
        GTA.Object cbBurger;
        GTA.Object Drink;
        //Menu Fonts
        GTA.Font mainMenuFont;
        GTA.Font subMenuFont;
        GTA.Font selectionFont;
        int selection;
        int tHungerSubtract = 1,
            tThirstSubtract = 1;
        int tHungerDamageCounter = 0,
            tThirstDamageCounter = 0;
        //Menu On/off bools
        bool MainMenuOn;
        bool MainMenuFood;
        bool MainMenuDrink;
        bool MainMenuInventory;
        bool MainMenuFoodInventory;
        bool MainMenuDrinkInventory;
        bool MainMenuFA;
        bool MainMenuFaInventory;
        bool readyToExitSafeMidAlg = false;
        bool readyToExitSafeNorthAlg = false;
        bool readyToExitSafeBh = false;
        bool readyToEnterSafeMidAlg = true;
        bool readyToEnterSafeNorthAlg = true;
        bool readyToEnterSafeBh = true;
        int firstTimeVendors = 0;
        bool largeFaHeld = false;
        bool mediumFaHeld = false;
        bool smallFaHeld = false;
        // Menu Locations
        bool Bar = false;
        bool Bs = false;
        bool Cb = false;
        bool Rest = false;
        bool Hospital = false;
        bool Inventory = false;
        bool inVehicleStopped = false;
        bool enableFoodVendors = false;
        bool greyStars = false;
        bool inSafeHouseBh = false;
        bool inSafeHouseNorthAlg = false;
        bool inSafeHouseMidAlg = false;
        bool outSafeHouseBh = false;
        bool outSafeHouseNorthAlg = false;
        bool outSafeHouseMidAlg = false;
        bool safeHousesEnabled = false;
        // Keys
        Keys buyMenu;
        Keys Purchase;
        Keys goBack;
        Keys Select;
        Keys scrollUp;
        Keys scrollDown;
        Keys eatDrinkHeal;
        Keys inventory;
        Keys startBuy;
        Keys startFoodMod;
        Keys loadGame;
        string stringPrice = null;
        // food/drink/fa int prices
        int priceFood1;
        int priceFood2;
        int priceFood3;
        int priceDrink1;
        int priceDrink2;
        int priceDrink3;
        int priceFA1;
        int priceFA2;
        int priceFA3;
        //Individual Food and drinks for storage
        int heartyBurger = 0;
        int basicBurger = 0;
        int frenchFries = 0;
        int largeDrink = 0;
        int mediumDrink = 0;
        int smallDrink = 0;
        int largeFA = 0;
        int mediumFA = 0;
        int smallFA = 0;
        //Inventory delcarations
        int MaxFoodInventory;
        int MaxFirstAidInventory;
        int MaxDrinkInventory;
        int mMaxHunger;
        int mMaxThirst;
        int foodAdd1;
        int foodAdd2;
        int foodAdd3;
        int drinkAdd1;
        int drinkAdd2;
        int drinkAdd3;
        int faAdd1;
        int faAdd2;
        int faAdd3;
        int enableSafeHouses;
        int saveTime;
        int baseBarBalance;
        int tLowThirstCount = 0,
            tLowHungerCounter = 0,
            xHungerThirstUI = 0,
            yHungerThirstUI = 0,
            waitModifier;
        Keys forceSave;
        //timers 
        GTA.Timer needsTimer_Tick;
        GTA.Timer saveTimer_Tick;
        bool playerRunning = false;
        private int R(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }



        #endregion
        public FoodModCS()
        {
            needsTimer_Tick = new GTA.Timer(1000);
            needsTimer_Tick.Tick += new EventHandler(NeedsTimerTick);
            saveTimer_Tick = new GTA.Timer(1000);
            saveTimer_Tick.Tick += new EventHandler(SaveTimerTick);
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            //Set Timers
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 500);
            mMaxHunger = Settings.GetValueInteger("MAX_FOOD_TIME", "TIMERS", 7200);
            mMaxThirst = Settings.GetValueInteger("MAX_THIRST_TIME", "TIMERS", 7200);
            foodAdd1 = Settings.GetValueInteger("FOOD_REPLENISH1", "FOOD_VALUES", 7200);
            foodAdd2 = Settings.GetValueInteger("FOOD_REPLENISH2", "FOOD_VALUES", 3600);
            foodAdd3 = Settings.GetValueInteger("FOOD_REPLENISH3", "FOOD_VALUES", 1800);
            drinkAdd1 = Settings.GetValueInteger("DRINK_REPLENISH1", "DRINK_VALUES", 7200);
            drinkAdd2 = Settings.GetValueInteger("DRINK_REPLENISH2", "DRINK_VALUES", 3600);
            drinkAdd3 = Settings.GetValueInteger("DRINK_REPLENISH3", "DRINK_VALUES", 1800);
            faAdd1 = Settings.GetValueInteger("FIRSTAID_HEALTH1", "FIRSTAID_VALUES", 100);
            faAdd2 = Settings.GetValueInteger("FIRSTAID_HEALTH2", "FIRSTAID_VALUES", 50);
            faAdd3 = Settings.GetValueInteger("FIRSTAID_HEALTH3", "FIRSTAID_VALUES", 25);
            xHungerThirstUI = Settings.GetValueInteger("HUNGER_THIRST_UI_X_AXIS", "SETTINGS", 0);
            yHungerThirstUI = Settings.GetValueInteger("HUNGER_THIRST_UI_Y_AXIS", "SETTINGS", 0);
            mHungerNeeds = Settings.GetValueInteger("Hunger", "NEEDS", 7200);
            mThirstNeeds = Settings.GetValueInteger("Thirst", "NEEDS", 7200);
            autoSaveInterval = Settings.GetValueInteger("AUTOSAVE_INTERVAL", "SETTINGS", 300);
            MaxInventorySpace = Settings.GetValueInteger("MAX_INVENTORY", "INVENTORY", 9);
            this.Purchase = Settings.GetValueKey("PURCHASE", "CONTROLS", Keys.NumPad9);
            this.scrollUp = Settings.GetValueKey("SCROLLUP", "CONTROLS", Keys.NumPad8);
            this.scrollDown = Settings.GetValueKey("SCROLLDOWN", "CONTROLS", Keys.NumPad2);
            this.Select = Settings.GetValueKey("SELECT", "CONTROLS", Keys.NumPad5);
            this.goBack = Settings.GetValueKey("GOBACK", "CONTROLS", Keys.NumPad0);
            this.eatDrinkHeal = Settings.GetValueKey("USE_ITEM_IN_HAND", "CONTROLS", Keys.U);
            this.inventory = Settings.GetValueKey("OPEN_INVENTORY", "CONTROLS", Keys.I);
            this.startFoodMod = Settings.GetValueKey("START_FOOD_MOD", "CONTROLS", Keys.U);
            this.startBuy = Settings.GetValueKey("OPEN_BUY_MENU", "CONTROLS", Keys.B);
            this.forceSave = Settings.GetValueKey("FORCE_SAVE_KEY", "CONTROLS", Keys.S);
            loadGame = Settings.GetValueKey("LOAD_SAVED_DATA", "CONTROLS", Keys.L);
            bool autoLoad = Settings.GetValueBool("AUTO_LOAD_ENABLED", "SETTINGS", true);
            toggleAutoSave = Settings.GetValueKey("ENABLE_DISABLE_AUTOSAVE_FUNCTION", "CONTROLS", Keys.A);
            BindKey(loadGame, true, true, true, LoadFoodDat);
            PerFrameDrawing += new GraphicsEventHandler(FoodMenu_PerFrameDrawing);
            this.Tick += new EventHandler(Food_Tick);
            this.KeyDown += new GTA.KeyEventHandler(KeyPressEvent);
            //                  shift, ctrl, alt
            BindKey(forceSave, true, true, true, SaveFoodDat); // force save key
            BindKey(startBuy, true, false, false, ToggleMenu);
            BindKey(inventory, true, false, false, ToggleInventory);
            BindKey(startFoodMod, true, false, false, StartFoodMod);
            BindKey(toggleAutoSave, true, true, true, Toggle_AutoSave);
            this.priceFood1 = Settings.GetValueInteger("FOODPRICE1", "PRICES", 150);
            this.priceFood2 = Settings.GetValueInteger("FOODPRICE2", "PRICES", 100);
            this.priceFood3 = Settings.GetValueInteger("FOODPRICE3", "PRICES", 50);
            this.priceDrink1 = Settings.GetValueInteger("DRINKPRICE1", "PRICES", 150);
            this.priceDrink2 = Settings.GetValueInteger("DRINKPRICE2", "PRICES", 100);
            this.priceDrink3 = Settings.GetValueInteger("DRINKPRICE3", "PRICES", 50);
            this.priceFA1 = Settings.GetValueInteger("FIRSTAIDPRICE1", "PRICES", 150);
            this.priceFA2 = Settings.GetValueInteger("FIRSTAIDPRICE2", "PRICES", 100);
            this.priceFA3 = Settings.GetValueInteger("FIRSTAIDPRICE3", "PRICES", 50);
            enableSafeHouses = Settings.GetValueInteger("ENABLE_SAFEHOUSE", "SETTINGS", 1);
            autoSaveEnabled = Settings.GetValueBool("AUTO_SAVE_ENABLED", "SETTINGS", false);
            
            //Position in which objects spawn
            if (enableSafeHouses == 1) { safeHousesEnabled = true; } else safeHousesEnabled = false;

            // SafeHouse Entrances
            #region SafeHouse Blip Entrances
            if (safeHousesEnabled)
            {
                CreateSafeHouseBlips(shBhBlip, enterShBh);
                CreateSafeHouseBlips(shAcBlip, enterAc);
                CreateSafeHouseBlips(midAlgBlip, enterMidAlg);
                CreateSafeHouseBlips(northAlgBlip, enterNorthAlg);
                CreateSafeHouseBlips(shHbBlip, enterHb);
            }
            #endregion
            //Buy Locations
            #region First 5 Clerks and Blips
            //Spawn Clerks and Blips:
            
                Wait(500);

                Blip BlipShop1 = Blip.AddBlip(RestHB);
                BlipShop1.Icon = BlipIcon.Activity_Restaurant;
                BlipShop1.Name = "Restaurant";
                BlipShop1.ShowOnlyWhenNear = true;
                BlipShop1.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop2 = Blip.AddBlip(BarHB);
                BlipShop2.Icon = BlipIcon.Activity_Bar;
                BlipShop2.Name = "Happy Hour";
                BlipShop2.ShowOnlyWhenNear = true;
                BlipShop2.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop3 = Blip.AddBlip(BowlHB);
                BlipShop3.Icon = BlipIcon.Activity_Bowling;
                BlipShop3.Name = "Bowling FunZone";
                BlipShop3.ShowOnlyWhenNear = true;
                BlipShop3.Display = BlipDisplay.ArrowAndMap;
                
                Wait(500);

                Blip BlipShop4 = Blip.AddBlip(ComedyHB);
                BlipShop4.Icon = BlipIcon.Activity_ComedyClub;
                BlipShop4.Name = "Comedy Club";
                BlipShop4.ShowOnlyWhenNear = true;
                BlipShop4.Display = BlipDisplay.ArrowAndMap;

                Wait(500);
                Blip BlipShop5 = Blip.AddBlip(eightBallHB);
                BlipShop5.Icon = BlipIcon.Misc_8BALL;
                BlipShop5.Name = "Pool Hall";
                BlipShop5.ShowOnlyWhenNear = true;
                BlipShop5.Display = BlipDisplay.ArrowAndMap;
            #endregion
            #region 6 to 10 Clerks and Blips

                Wait(500);
                Blip BlipShop6 = Blip.AddBlip(BurgerHB);
                BlipShop6.Icon = BlipIcon.Building_BurgerShot;
                BlipShop6.Name = "Burger Shot";
                BlipShop6.ShowOnlyWhenNear = true;
                BlipShop6.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop7 = Blip.AddBlip(DartsHB);
                BlipShop7.Icon = BlipIcon.Activity_Darts;
                BlipShop7.Name = "Late Night Bar";
                BlipShop7.ShowOnlyWhenNear = true;
                BlipShop7.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop8 = Blip.AddBlip(outHospitalHB);
                BlipShop8.Icon = BlipIcon.Building_Hospital;
                BlipShop8.Name = "Hospital";
                BlipShop8.ShowOnlyWhenNear = true;
                BlipShop8.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop9 = Blip.AddBlip(inHospitalHB);
                BlipShop9.Icon = BlipIcon.Building_Hospital;
                BlipShop9.Name = "Hospital";
                BlipShop9.ShowOnlyWhenNear = true;
                BlipShop9.Display = BlipDisplay.ArrowAndMap;
  
                Wait(500);

                Blip BlipShop10 = Blip.AddBlip(sClubBH);
                BlipShop10.Icon = BlipIcon.Activity_StripClub;
                BlipShop10.Name = "Strip Club Buffet";
                BlipShop10.ShowOnlyWhenNear = true;
                BlipShop10.Display = BlipDisplay.ArrowAndMap;
            #endregion
            #region 11 to 15 Clerks and Blips
                Wait(500);

                Blip BlipShop11 = Blip.AddBlip(BurgerBH1);
                BlipShop11.Icon = BlipIcon.Building_BurgerShot;
                BlipShop11.Name = "Burger Shot";
                BlipShop11.ShowOnlyWhenNear = true;
                BlipShop11.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop12 = Blip.AddBlip(BurgerBH2);
                BlipShop12.Icon = BlipIcon.Building_BurgerShot;
                BlipShop12.Name = "Burger Shot";
                BlipShop12.ShowOnlyWhenNear = true;
                BlipShop12.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop13 = Blip.AddBlip(outHospitalBH);
                BlipShop13.Icon = BlipIcon.Building_Hospital;
                BlipShop13.Name = "Hospital";
                BlipShop13.ShowOnlyWhenNear = true;
                BlipShop13.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop14 = Blip.AddBlip(BurgerALG1);
                BlipShop14.Icon = BlipIcon.Building_BurgerShot;
                BlipShop14.Name = "Burger Shot";
                BlipShop14.ShowOnlyWhenNear = true;
                BlipShop14.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop15 = Blip.AddBlip(CafeALG1);
                BlipShop15.Icon = BlipIcon.Activity_Restaurant;
                BlipShop15.Name = "Celebrity Cafe";
                BlipShop15.ShowOnlyWhenNear = true;
                BlipShop15.Display = BlipDisplay.ArrowAndMap;
            #endregion
            #region 16 to 20 Clerks and Blips
                Wait(500);

                Blip BlipShop16 = Blip.AddBlip(BurgerALG2);
                BlipShop16.Icon = BlipIcon.Building_BurgerShot;
                BlipShop16.Name = "Burger Shot";
                BlipShop16.ShowOnlyWhenNear = true;
                BlipShop16.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop17 = Blip.AddBlip(ClunkinALG);
                BlipShop17.Icon = BlipIcon.Building_CluckinBell;
                BlipShop17.Name = "Cluckin Bell";
                BlipShop17.ShowOnlyWhenNear = true;
                BlipShop17.Display = BlipDisplay.ArrowAndMap;
                Wait(500);

                Blip BlipShop18 = Blip.AddBlip(DartsALG);
                BlipShop18.Icon = BlipIcon.Activity_Darts;
                BlipShop18.Name = "Late Night Bar";
                BlipShop18.ShowOnlyWhenNear = true;
                BlipShop18.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop19 = Blip.AddBlip(BurgerALG3);
                BlipShop19.Icon = BlipIcon.Building_BurgerShot;
                BlipShop19.Name = "Burger Shot";
                BlipShop19.ShowOnlyWhenNear = true;
                BlipShop19.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop20 = Blip.AddBlip(BowlALG);
                BlipShop20.Icon = BlipIcon.Activity_Bowling;
                BlipShop20.Name = "Concourse Bowling";
                BlipShop20.ShowOnlyWhenNear = true;
                BlipShop20.Display = BlipDisplay.ArrowAndMap;
            #endregion
            #region 21 to 25 Clerks and Blips

                Wait(500);

                Blip BlipShop21 = Blip.AddBlip(CafeALG2);
                BlipShop21.Icon = BlipIcon.Activity_Restaurant;
                BlipShop21.Name = "Celebrity Cafe";
                BlipShop21.ShowOnlyWhenNear = true;
                BlipShop21.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop22 = Blip.AddBlip(outHospitalALG1);
                BlipShop22.Icon = BlipIcon.Building_Hospital;
                BlipShop22.Name = "Hospital";
                BlipShop22.ShowOnlyWhenNear = true;
                BlipShop22.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop23 = Blip.AddBlip(outHospitalALG2);
                BlipShop23.Icon = BlipIcon.Building_Hospital;
                BlipShop23.Name = "Hospital";
                BlipShop23.ShowOnlyWhenNear = true;
                BlipShop23.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop25 = Blip.AddBlip(ChinaAC);
                BlipShop25.Icon = BlipIcon.Activity_Restaurant;
                BlipShop25.Name = "Chinese Cuisine";
                BlipShop25.ShowOnlyWhenNear = true;
                BlipShop25.Display = BlipDisplay.ArrowAndMap;
            #endregion
            #region 26 to 30 Clerks and Blips

                Wait(500);

                Blip BlipShop26 = Blip.AddBlip(BurgerAC);
                BlipShop26.Icon = BlipIcon.Building_BurgerShot;
                BlipShop26.Name = "Burger Shot";
                BlipShop26.ShowOnlyWhenNear = true;
                BlipShop26.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop27 = Blip.AddBlip(sClubAC);
                BlipShop27.Icon = BlipIcon.Activity_StripClub;
                BlipShop27.Name = "Strip Club Buffet";
                BlipShop27.ShowOnlyWhenNear = true;
                BlipShop27.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop28 = Blip.AddBlip(outHospitalAC);
                BlipShop28.Icon = BlipIcon.Building_Hospital;
                BlipShop28.Name = "Hospital";
                BlipShop28.ShowOnlyWhenNear = true;
                BlipShop28.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop29 = Blip.AddBlip(inHospitalAC);
                BlipShop29.Icon = BlipIcon.Building_Hospital;
                BlipShop29.Name = "Hospital";
                BlipShop29.ShowOnlyWhenNear = true;
                BlipShop29.Display = BlipDisplay.ArrowAndMap;

                Wait(500);

                Blip BlipShop30 = Blip.AddBlip(CluckinHB);
                BlipShop30.Icon = BlipIcon.Building_CluckinBell;
                BlipShop30.Name = "Cluckin Bell";
                BlipShop30.ShowOnlyWhenNear = true;
                BlipShop30.Display = BlipDisplay.ArrowAndMap;
            #endregion
            if (autoLoad)
            LoadFoodDat();
            Game.DisplayText("FoodMod by Daimyo Loaded", 2000);
        }
        #region LoadMenuFont
        void LoadMenuFont()
        {
            if (Bar)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.Firebrick;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }
            if (Cb)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.Yellow;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }
            if (Bs)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.CadetBlue;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }
            if (Rest)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.DarkRed;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }
            if (Hospital)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.SeaGreen;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }
            if (Inventory)
            {
                mainMenuFont = new GTA.Font(44.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.SaddleBrown;
                subMenuFont = new GTA.Font(28.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.Brown;
                selectionFont = new GTA.Font(28.0F, FontScaling.Pixel);
                selectionFont.Color = Color.OrangeRed;
            }

        }
        #endregion
        #region ToggleMenu
        void ToggleMenu()
        {
            if (enableFoodVendors)
            {
                if (Bar || Bs || Cb || Hospital || Rest)
                {
                    LoadMenuFont();
                    if (Bar)
                    {
                        if (Game.LocalPlayer.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out!", 5000, 1 });

                        }
                        else if (Game.LocalPlayer.WantedLevel < 1)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome, I'll be your Bartender today", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Game.LocalPlayer.CanControlCharacter = false;
                            Game.LocalPlayer.IgnoredByEveryone = true;
                            MainMenuOn = true;
                        }
                    }
                    if (Bs)
                    {
                        if (Game.LocalPlayer.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out!", 5000, 1 });

                        }
                        else if (Game.LocalPlayer.WantedLevel < 1)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome to Burger Shot!", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Game.LocalPlayer.CanControlCharacter = false;
                            Game.LocalPlayer.IgnoredByEveryone = true;
                            MainMenuOn = true;
                        }
                    }
                    if (Cb)
                    {
                        if (Game.LocalPlayer.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out!", 5000, 1 });

                        }
                        else if (Game.LocalPlayer.WantedLevel < 1)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome to Cluckin Bells!", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Game.LocalPlayer.CanControlCharacter = false;
                            Game.LocalPlayer.IgnoredByEveryone = true;
                            MainMenuOn = true;
                        }
                    }
                    if (Rest)
                    {
                        if (Game.LocalPlayer.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out!", 5000, 1 });

                        }
                        else if (Game.LocalPlayer.WantedLevel < 1)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome! What can I get for you!", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Game.LocalPlayer.CanControlCharacter = false;
                            Game.LocalPlayer.IgnoredByEveryone = true;
                            MainMenuOn = true;
                        }
                    }
                    if (Hospital)
                    {
                        if (Game.LocalPlayer.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Go Away!", 5000, 1 });

                        }
                        else if (Game.LocalPlayer.WantedLevel < 1)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Hello.  How can I help you today?", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Game.LocalPlayer.CanControlCharacter = false;
                            Game.LocalPlayer.IgnoredByEveryone = true;
                            MainMenuOn = true;
                        }
                    }
                }
            }
            else
            {
                if (Bar || Bs || Cb || Hospital || Rest)
                {
                    Game.DisplayText("You must enable vendors! Press SHIFT + U");
                }
            }

        }
        #endregion
        #region ToggleInventory
        void ToggleInventory()
        {
            if (Game.LocalPlayer.WantedLevel > 0 && !greyStars)
            {
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Cops see me!  Can't check inventory right now!", 6000, 1 });
            }
            else
            {
                if (Inventory)
                {
                    if (!MainMenuOn && !MainMenuInventory && !MainMenuFoodInventory && !MainMenuDrinkInventory && !MainMenuFaInventory)
                    {
                        if (Game.LocalPlayer.Character.isInVehicle())
                        {
                            if (inVehicleStopped)
                            {
                                Game.LocalPlayer.CanControlCharacter = false;
                                Function.Call("SET_CAMERA_CONTROLS_DISABLED_WITH_PLAYER_CONTROLS", 0);
                                Game.LocalPlayer.IgnoredByEveryone = false;
                                Game.LocalPlayer.Character.Invincible = false;
                                LoadMenuFont();
                                MainMenuOn = true;
                                MainMenuInventory = true;
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You open your bag", 1500, 1 });
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Vehicle must be at a complete stop to check inventory.", 5000, 1 });
                            }
                        }
                        else
                        {
                            Game.LocalPlayer.CanControlCharacter = false;
                            Function.Call("SET_CAMERA_CONTROLS_DISABLED_WITH_PLAYER_CONTROLS", 0);
                            Game.LocalPlayer.IgnoredByEveryone = false;
                            Game.LocalPlayer.Character.Invincible = false;
                            LoadMenuFont();
                            MainMenuOn = true;
                            MainMenuInventory = true;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You open your bag", 1500, 1 });

                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your already in your inventory!", 1500, 1 });
                    }
                }
                else
                {
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You need to be away from a vendor in order to open inventory", 5000, 1 });
                }
            }

        }
        #endregion
        #region Controls
        void KeyPressEvent(object sender, GTA.KeyEventArgs e)
        {
            #region Menu Controls
            if (MainMenuOn || MainMenuFood || MainMenuDrink || MainMenuFA || MainMenuInventory)
            {
                #region ScrollUp
                if (e.Key == scrollUp)
                {
                    #region ScrollUP BuyMenus
                    if (MainMenuOn && !MainMenuInventory && !MainMenuFoodInventory && !MainMenuDrinkInventory && !MainMenuFaInventory)
                    {
                        if (Hospital)
                        {
                            selection--;
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                            if (selection > 0)
                            {
                                selection = 0;
                            }
                        }
                        else
                        {
                            selection--;
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                            if (selection > 1)
                            {
                                selection = 1;
                            }
                        }
                    }
                        if (MainMenuFood)
                        {
                            selection--;
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                        }
                        if (MainMenuDrink)
                        {
                            selection--;
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                        }
                        if (MainMenuFA)
                        {
                            selection--;
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                        }

                    #endregion
                    #region ScrollUP Inventory Menus
                    if (MainMenuInventory)
                    {
                        selection--;
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                    }
                    if (MainMenuFoodInventory)
                    {
                        selection--;
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                    }
                    if (MainMenuDrinkInventory)
                    {
                        selection--;
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                    }
                    if (MainMenuFaInventory)
                    {
                        selection--;
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                    }
                    #endregion
                }
                #endregion
                #region ScrollDown
                if (e.Key == scrollDown)
                {
                    #region ScrollDown BuyMenus
                    if (MainMenuOn && !MainMenuInventory && !MainMenuFoodInventory && !MainMenuDrinkInventory && !MainMenuFaInventory)
                    {
                        if (Hospital)
                        {
                            selection++;
                            if (selection > 0)
                            {
                                selection = 0;
                            }
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                        }
                        else
                        {
                            selection++;
                            if (selection > 1)
                            {
                                selection = 1;
                            }
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                        }
                    }
                        if (MainMenuFood)
                        {
                            selection++;
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                        }
                        if (MainMenuDrink)
                        {
                            selection++;
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                        }
                        if (MainMenuFA)
                        {
                            selection++;
                            if (selection > 2)
                            {
                                selection = 2;
                            }
                            if (selection < 0)
                            {
                                selection = 0;
                            }
                        }

                    #endregion
                    #region ScrollDown Inventory Menu
                    if (MainMenuInventory)
                    {
                        selection++;
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                    }
                    if (MainMenuFoodInventory)
                    {
                        selection++;
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                    }
                    if (MainMenuDrinkInventory)
                    {
                        selection++;
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                    }
                    if (MainMenuFaInventory)
                    {
                        selection++;
                        if (selection > 2)
                        {
                            selection = 2;
                        }
                        if (selection < 0)
                        {
                            selection = 0;
                        }
                    }
                    #endregion
                }
                #endregion
                #region Select and Purchase Keys
                if (e.Key == Select)
                {
                    #region ShopMenu Select
                    if (MainMenuOn && !MainMenuInventory && !MainMenuFoodInventory && !MainMenuDrinkInventory && !MainMenuFaInventory)
                    {
                        if (!Hospital)
                        {
                            if (selection == 0)
                            {
                                selection = 0;
                                MainMenuOn = false;
                                MainMenuFood = true;
                                MainMenuDrink = false;
                                MainMenuFA = false;
                                return;
                            }
                            if (selection == 1)
                            {
                                selection = 0;
                                MainMenuOn = false;
                                MainMenuFood = false;
                                MainMenuDrink = true;
                                MainMenuFA = false;
                                return;
                            }
                        }
                        else
                        {
                            if (selection == 0)
                            {
                                selection = 0;
                                MainMenuOn = false;
                                MainMenuFA = true;
                                return;
                            }
                        }

                    }
                    #endregion
                    #region Inventory Select Menus
                    if (MainMenuInventory)
                    {
                        if (selection == 0)
                        {
                            selection = 0;
                            MainMenuInventory = false;
                            MainMenuFoodInventory = true;
                        }
                        if (selection == 1)
                        {
                            selection = 0;
                            MainMenuInventory = false;
                            MainMenuDrinkInventory = true;
                        }
                        if (selection == 2)
                        {
                            selection = 0;
                            MainMenuInventory = false;
                            MainMenuFaInventory = true;
                        }

                    }
                }
                    #endregion
                    #region ShopMenu Purchase
                if (e.Key == Purchase)
                {
                    if (MainMenuFood)
                    {
                        if (selection == 0)
                        {
                            if (Game.LocalPlayer.Money >= priceFood1)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    heartyBurger++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFood1;
                                    Wait(1000);
                                    Game.DisplayText("You have $" + Game.LocalPlayer.Money.ToString() + " left", 5000);
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Hearty Burger!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 1)
                        {
                            if (Game.LocalPlayer.Money >= priceFood2)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    basicBurger++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFood2;
                                    Wait(1000);
                                    Game.DisplayText("You have $" + Game.LocalPlayer.Money.ToString() + " left", 5000);
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Basic Burger!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 2)
                        {
                            if (Game.LocalPlayer.Money >= priceFood3)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    frenchFries++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFood3;
                                    Wait(1000);
                                    Game.DisplayText("You have $" + Game.LocalPlayer.Money.ToString() + " left", 5000);
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased French Fries!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                    }
                    if (MainMenuDrink)
                    {
                        if (selection == 0)
                        {
                            if (Game.LocalPlayer.Money >= priceDrink1)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    largeDrink++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceDrink1;
                                    Wait(1000);
                                    Game.DisplayText("You have $" + Game.LocalPlayer.Money.ToString() + " left", 5000);
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Large Drink!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 1)
                        {
                            if (Game.LocalPlayer.Money >= priceDrink2)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    mediumDrink++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceDrink2;
                                    Wait(1000);
                                    Game.DisplayText("You have $" + Game.LocalPlayer.Money.ToString() + " left", 5000);
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Medium Drink!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 2)
                        {
                            if (Game.LocalPlayer.Money >= priceDrink3)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    smallDrink++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceDrink3;
                                    Wait(1000);
                                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });

                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Small Drink!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                    }
                    if (MainMenuFA)
                    {
                        if (selection == 0)
                        {
                            if (Game.LocalPlayer.Money >= priceFA1)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    largeFA++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFA1;
                                    Wait(1000);
                                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });

                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Large FirstAid Kit!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 1)
                        {
                            if (Game.LocalPlayer.Money >= priceFA2)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    mediumFA++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFA2;
                                    Wait(1000);
                                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });

                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Medium FirstAid Kit!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                        if (selection == 2)
                        {
                            if (Game.LocalPlayer.Money >= priceFA3)
                            {
                                if (inventorySpace < MaxInventorySpace)
                                {
                                    smallFA++;
                                    inventorySpace++; if (inventorySpace >= MaxInventorySpace) { inventorySpace = MaxInventorySpace; }
                                    Game.LocalPlayer.Money -= priceFA3;
                                    Wait(1000);
                                    Function.Call("DISPLAY_CASH", new Parameter[] { 1 });

                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You purchased a Small FirstAid Kit!", 1500, 1 });
                                }
                                else
                                {
                                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your Inventory is full!", 1500, 1 });
                                }
                            }
                            else
                            {
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 1500, 1 });
                            }
                        }
                    }

                #endregion
                    #region Inventory Use (USES PURCHASE KEY)

                    if (MainMenuFoodInventory)
                    {
                        if (selection == 0)
                        {
                            SpawnObject();
                        }
                        if (selection == 1)
                        {
                            SpawnObject();
                        }
                        if (selection == 2)
                        {
                            SpawnObject();
                        }
                    }
                    if (MainMenuDrinkInventory)
                    {
                        if (selection == 0)
                        {
                            SpawnObject();
                        }
                        if (selection == 1)
                        {
                            SpawnObject();
                        }
                        if (selection == 2)
                        {
                            SpawnObject();
                        }
                    }
                    if (MainMenuFaInventory)
                    {
                        if (selection == 0)
                        {
                            SpawnObject();
                        }
                        if (selection == 1)
                        {
                            SpawnObject();
                        }
                        if (selection == 2)
                        {
                            SpawnObject();
                        }
                    }
                }
                    #endregion
                #endregion
                #region GoBack
                if (e.Key == goBack)
                {
                    #region GoBack ShopMenus
                    if (MainMenuOn && !MainMenuInventory && !MainMenuFoodInventory && !MainMenuDrinkInventory && !MainMenuFaInventory)
                    {
                        Game.FadeScreenOut(500);
                        Wait(1000);
                        MainMenuOn = false;
                        selection = 0;
                        Game.FadeScreenIn(500);
                        Game.LocalPlayer.CanControlCharacter = true;
                        Game.LocalPlayer.IgnoredByEveryone = false;
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Thanks for stopping by!", 5000, 1 });
                    }
                    else if (MainMenuFood)
                    {
                        MainMenuOn = true;
                        MainMenuFood = false;
                        selection = 0;
                    }
                    else if (MainMenuDrink)
                    {
                        MainMenuOn = true;
                        MainMenuDrink = false;
                        selection = 0;
                    }
                    else if (MainMenuFA)
                    {
                        MainMenuOn = true;
                        MainMenuFA = false;
                        selection = 0;
                    }
                    #endregion
                    #region GoBack Inventory Menu
                    if (MainMenuInventory)
                    {
                        MainMenuInventory = false;
                        MainMenuOn = false;
                        selection = 0;
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You close your bag", 2000, 1 });
                        Game.LocalPlayer.CanControlCharacter = true;
                    }
                    else if (MainMenuFoodInventory)
                    {
                        MainMenuInventory = true;
                        MainMenuFoodInventory = false;
                        selection = 0;
                    }
                    else if (MainMenuDrinkInventory)
                    {
                        MainMenuInventory = true;
                        MainMenuDrinkInventory = false;
                        selection = 0;
                    }
                    else if (MainMenuFaInventory)
                    {
                        MainMenuInventory = true;
                        MainMenuFaInventory = false;
                        selection = 0;
                    }
                    #endregion
                }
                #endregion
            }
        }
        #endregion
            #endregion

        #region Purchase Object, Spawn Object, Pickup Object
        void SpawnObject()
        {
            Vector3 objectSpawn = Game.LocalPlayer.Character.GetOffsetPosition(new Vector3(0.0f, 0.4f, 0.4f));
            AnimationSet animInventory = new AnimationSet("amb@atm");
            AnimationFlags animflags = AnimationFlags.Unknown12 | AnimationFlags.Unknown11 | AnimationFlags.Unknown09;

            #region Food Inventory Spawn
            if (MainMenuFoodInventory)
            {
                if (selection == 0 && heartyBurger > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object HeartyBurger = World.CreateObject("BM_burgershot_burg1", objectSpawn);
                        
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (HeartyBurger != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", HeartyBurger))
                            { Wait(0); }
                            HeartyBurger.NoLongerNeeded();
                            if (HeartyBurger != null && Game.Exists(HeartyBurger))
                            {
                                heartyBurger--;
                                if (heartyBurger <= 0) { heartyBurger = 0; }
                                inventorySpace--;
                                if (inventorySpace <= 0) { inventorySpace = 0; }
                                HeartyBurger.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", HeartyBurger, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", HeartyBurger, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", HeartyBurger);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, HeartyBurger, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 1 && basicBurger > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object BasicBurger = World.CreateObject("BM_cluckin_burg", objectSpawn);
                        
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (BasicBurger != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", BasicBurger))
                            { Wait(0); }
                            BasicBurger.NoLongerNeeded();
                            if (BasicBurger != null && Game.Exists(BasicBurger))
                            {
                                basicBurger--;
                                if (basicBurger <= 0) { basicBurger = 0; }
                                inventorySpace--;
                                if (inventorySpace <= 0) { inventorySpace = 0; }
                                BasicBurger.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", BasicBurger, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", BasicBurger, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", BasicBurger);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, BasicBurger, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 2 && frenchFries > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object FrenchFries = World.CreateObject("BM_burgershot_fries", objectSpawn);
                        
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (FrenchFries != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", FrenchFries))
                            { Wait(0); }
                            FrenchFries.NoLongerNeeded();
                            if (FrenchFries != null && Game.Exists(FrenchFries))
                            {
                                frenchFries--;
                                if (frenchFries <= 0) { frenchFries = 0; }
                                inventorySpace--;
                                if (inventorySpace <= 0) { inventorySpace = 0; }
                                FrenchFries.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", FrenchFries, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", FrenchFries, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", FrenchFries);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, FrenchFries, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
            }

            #endregion
            #region Drink Inventory Spawn
            if (MainMenuDrinkInventory)
            {
                if (selection == 0 && largeDrink > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object LargeDrink = World.CreateObject("BM_burgershot_cup", objectSpawn);
                        largeDrink--;
                        if (largeDrink <= 0) { largeDrink = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (LargeDrink != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", LargeDrink))
                            { Wait(0); }
                            LargeDrink.NoLongerNeeded();
                            if (LargeDrink != null && Game.Exists(LargeDrink))
                            {
                                LargeDrink.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", LargeDrink, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", LargeDrink, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", LargeDrink);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, LargeDrink, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 1 && mediumDrink > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object MediumDrink = World.CreateObject("BM_cluckin_cup", objectSpawn);
                        mediumDrink--;
                        if (mediumDrink <= 0) { mediumDrink = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (MediumDrink != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", MediumDrink))
                            { Wait(0); }
                            MediumDrink.NoLongerNeeded();
                            if (MediumDrink != null && Game.Exists(MediumDrink))
                            {
                                MediumDrink.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", MediumDrink, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", MediumDrink, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", MediumDrink);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, MediumDrink, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 2 && smallDrink > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object SmallDrink = World.CreateObject("CJ_COFFEE_CUP", objectSpawn); //0x7CC1EA0B 
                        smallDrink--;
                        if (smallDrink <= 0) { smallDrink = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (SmallDrink != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", SmallDrink))
                            { Wait(0); }
                            SmallDrink.NoLongerNeeded();
                            if (SmallDrink != null && Game.Exists(SmallDrink))
                            {
                                SmallDrink.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", SmallDrink, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", SmallDrink, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", SmallDrink);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, SmallDrink, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
            }
            #endregion
            #region FirstAid Inventory Spawn
            if (MainMenuFaInventory)
            {
                if (selection == 0 && largeFA > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object LargeFA = World.CreateObject("CJ_FIRST_AID_PICKUP", objectSpawn);
                        largeFA--;
                        if (largeFA <= 0) { largeFA = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        largeFaHeld = true;
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (LargeFA != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", LargeFA))
                            { Wait(0); }
                            LargeFA.NoLongerNeeded();
                            if (LargeFA != null && Game.Exists(LargeFA))
                            {
                                LargeFA.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", LargeFA, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", LargeFA, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", LargeFA);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, LargeFA, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 1 && mediumFA > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object MediumFA = World.CreateObject("CJ_FIRST_AID_PICKUP", objectSpawn);
                        mediumFA--;
                        if (mediumFA <= 0) { mediumFA = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        mediumFaHeld = true;
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (MediumFA != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", MediumFA))
                            { Wait(0); }
                            MediumFA.NoLongerNeeded();
                            if (MediumFA != null && Game.Exists(MediumFA))
                            {
                                MediumFA.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", MediumFA, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", MediumFA, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", MediumFA);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, MediumFA, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
                if (selection == 2 && smallFA > 0)
                {
                    if (!Game.LocalPlayer.Character.isSittingInVehicle() && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isRagdoll)
                    {
                        Game.LocalPlayer.Character.Task.PlayAnimation(animInventory, "m_getoutwallet_chest", 2.4F, animflags);
                        GTA.Object SmallFA = World.CreateObject("CJ_FIRST_AID_PICKUP", objectSpawn);
                        smallFA--;
                        if (smallFA <= 0) { smallFA = 0; }
                        inventorySpace--;
                        if (inventorySpace <= 0) { inventorySpace = 0; }
                        smallFaHeld = true;
                        //GTA.Native.Function.Call("ATTACH_OBJECT_TO_PED", HeartyBurger, Game.LocalPlayer.Character, 1232, 0.015f, -0.005f, -0.021f, 0.0f, 0.0f, 0.0f, 0);
                        if (SmallFA != null)
                        {
                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", SmallFA))
                            { Wait(0); }
                            SmallFA.NoLongerNeeded();
                            if (SmallFA != null && Game.Exists(SmallFA))
                            {
                                SmallFA.Visible = false; GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", SmallFA, 1);
                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", SmallFA, 1); GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", SmallFA);
                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Game.LocalPlayer.Character, SmallFA, 0.0f, 0.0f, 0.0f, 0);
                            }
                        }
                    }
                    else
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You cant do that right now", 1500, 1 });
                    }
                }
            }
            #endregion
        }
        #endregion
        #region Draw Menus && Hunger/Thirst Bars
        void FoodMenu_PerFrameDrawing(object sender, GraphicsEventArgs e)
        {
            int xPosition = (Game.Resolution.Width / 4);  //200;
            int yPosition = (Game.Resolution.Height / 4); //250;
            int width = 1100;
            int height = 50;
            int xPricePosition = (Game.Resolution.Width / 2); //700;
            int yPricePosition = (Game.Resolution.Height / 4); //250;
            int xBarPosition = (Game.Resolution.Width / 2) - (Game.Resolution.Width / 3) + xHungerThirstUI; //240;
            int yBarPosition = (Game.Resolution.Height / 2) + (Game.Resolution.Height / 3) + yHungerThirstUI; //680;
            int barWidth = 140;
            int barHeight = 25;

            Color baseBarFood = Color.Black;
            Color baseBarDrink = Color.Black; 
            Color foodBoxBar = Color.DarkRed;
            Color thirstBoxBar = Color.DarkBlue;
            string playerInventoryName = Game.LocalPlayer.Name.ToString();
            baseBarBalance = (mMaxHunger - mMaxThirst);
            //Rectangle
            RectangleF thirstBaseBar = new RectangleF(xBarPosition, yBarPosition + 30, mMaxHunger / 100, barHeight);
            RectangleF thirstBar = new RectangleF(xBarPosition + 5, yBarPosition + 35, ((mThirstNeeds + baseBarBalance) / 100 - 10), barHeight - 10);
            RectangleF foodBaseBar = new RectangleF(xBarPosition, yBarPosition, mMaxHunger / 100, barHeight);
            RectangleF foodBar = new RectangleF(xBarPosition + 5, yBarPosition + 5, ((mHungerNeeds / 100) - 10), barHeight - 10);
            
            if (!Game.LocalPlayer.Character.isInVehicle() && (Game.isGameKeyPressed(GameKey.Sprint) && (Game.isGameKeyPressed(GameKey.MoveLeft) || Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveBackward) || Game.isGameKeyPressed(GameKey.MoveForward)))) 
            {
                thirstBoxBar = Color.OrangeRed;
            }
            if (!Game.LocalPlayer.Character.isInVehicle() && (Game.isGameKeyPressed(GameKey.Sprint) && (Game.isGameKeyPressed(GameKey.MoveLeft) || Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveBackward) || Game.isGameKeyPressed(GameKey.MoveForward))))
            {
                foodBoxBar = Color.OrangeRed;
            }

            if (mThirstNeeds < 300 && !lowThirst)
            {
                thirstBoxBar = Color.Red;
                lowThirst = true;
            }
            if (mHungerNeeds < 300 && !lowHunger)
            {
                foodBoxBar = Color.Orange;
                lowHunger = true;
            }

            e.Graphics.DrawRectangle(foodBaseBar, baseBarFood);
            e.Graphics.DrawRectangle(foodBar, foodBoxBar);
            e.Graphics.DrawRectangle(thirstBaseBar, baseBarDrink);
            e.Graphics.DrawRectangle(thirstBar, thirstBoxBar);

           // e.Graphics.Scaling = FontScaling.Pixel; // fixed amount of pixels, size on screen will differ for each resolution
            

            //Create Hunger and Thirst bars
            //creating rectangles for Main Menu
            RectangleF title = new RectangleF(xPosition, yPosition, width, height);
            RectangleF m0 = new RectangleF(xPosition, yPosition + 40, width, height);
            RectangleF m1 = new RectangleF(xPosition, yPosition + 70, width, height);
            RectangleF m2 = new RectangleF(xPosition, yPosition + 100, width, height);
            RectangleF m3 = new RectangleF(xPosition, yPosition + 130, width, height);
            RectangleF m4 = new RectangleF(xPosition, yPosition + 160, width, height);
            RectangleF m5 = new RectangleF(xPosition, yPosition + 190, width, height);
            RectangleF m6 = new RectangleF(xPosition, yPosition + 220, width, height);
            RectangleF m7 = new RectangleF(xPosition, yPosition + 250, width, height);
            RectangleF m8 = new RectangleF(xPosition, yPosition + 280, width, height);
            RectangleF m9 = new RectangleF(xPosition, yPosition + 310, width, height);
            //Rectangles for Prices
            RectangleF p0 = new RectangleF(xPricePosition, yPricePosition + 40, width, height);
            RectangleF p1 = new RectangleF(xPricePosition, yPricePosition + 70, width, height);
            RectangleF p2 = new RectangleF(xPricePosition, yPricePosition + 100, width, height);
            RectangleF p3 = new RectangleF(xPricePosition, yPricePosition + 130, width, height);
            RectangleF p4 = new RectangleF(xPricePosition, yPricePosition + 160, width, height);
            RectangleF p5 = new RectangleF(xPricePosition, yPricePosition + 190, width, height);
            RectangleF p6 = new RectangleF(xPricePosition, yPricePosition + 220, width, height);
            RectangleF p7 = new RectangleF(xPricePosition, yPricePosition + 250, width, height);
            RectangleF p8 = new RectangleF(xPricePosition, yPricePosition + 280, width, height);
            RectangleF p9 = new RectangleF(xPricePosition, yPricePosition + 310, width, height);

            e.Graphics.Scaling = FontScaling.Pixel;
            if (MainMenuOn && Game.LocalPlayer.Character.isAlive)
            {
                if (Bar)
                {
                    e.Graphics.DrawText("Bar", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (Bs)
                {
                    e.Graphics.DrawText("Burger Shot", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (Cb)
                {
                    e.Graphics.DrawText("Cluckin Bell", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (Rest)
                {
                    e.Graphics.DrawText("Restaurant Menu", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("1) Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("2) Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (Hospital)
                {
                    e.Graphics.DrawText("Medical Center", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("1) Medical Supplies", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("1) Medical Supplies", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (MainMenuInventory)
                {
                    e.Graphics.DrawText(playerInventoryName + "'s Personal Inventory", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                    if (selection == 0)
                    {
                        e.Graphics.DrawText("Inventory Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Inventory Foods", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("Inventory Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Inventory Drinks", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 2)
                    {
                        e.Graphics.DrawText("Medical Supplies", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Medical Supplies", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (MainMenuFoodInventory)
                {
                    e.Graphics.DrawText("Food in Inventory", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("Hearty Hamburgers =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = heartyBurger.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Hearty Hamburgers =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = heartyBurger.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("Basic Hamburgers =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = basicBurger.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Basic Hamburgers =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = basicBurger.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 2)
                    {
                        e.Graphics.DrawText("French Fries =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = frenchFries.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("French Fries =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = frenchFries.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (MainMenuDrinkInventory)
                {
                    e.Graphics.DrawText("Drinks in Inventory", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("Large Drinks =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = largeDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Large Drinks =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = largeDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("Medium Drinks =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = mediumDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Medium Drinks =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = mediumDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 2)
                    {
                        e.Graphics.DrawText("Small Drinks =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = smallDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Small Drinks =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = smallDrink.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                }
                else if (MainMenuFaInventory)
                {
                    e.Graphics.DrawText("Medical Supplies in Inventory", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                    if (selection == 0)
                    {
                        e.Graphics.DrawText("Large FirstAid Kit =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = largeFA.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Large FirstAid Kit =", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = largeFA.ToString();
                        e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 1)
                    {
                        e.Graphics.DrawText("Medium FirstAid Kit =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = mediumFA.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Medium FirstAid Kit =", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = mediumFA.ToString();
                        e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    if (selection == 2)
                    {
                        e.Graphics.DrawText("Small FirstAid Kit =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                        stringPrice = smallFA.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                    {
                        e.Graphics.DrawText("Small FirstAid Kit =", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                        stringPrice = smallFA.ToString();
                        e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }

                }
            }
            else if (MainMenuFood)
            {
                e.Graphics.DrawText("Food Menu", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                if (selection == 0)
                {
                    e.Graphics.DrawText("1) Hearty Hamburger", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFood1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("1) Hearty Hamburger", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFood1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 1)
                {
                    e.Graphics.DrawText("2) Basic Hamburger", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFood2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("2) Basic Hamburger", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFood2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 2)
                {
                    e.Graphics.DrawText("3) French Fries", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFood3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("3) French Fries", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFood3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            else if (MainMenuDrink)
            {
                e.Graphics.DrawText("Drink Menu", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                if (selection == 0)
                {
                    e.Graphics.DrawText("1) Large Drink", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceDrink1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("1) Large Drink", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceDrink1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 1)
                {
                    e.Graphics.DrawText("2) Medium Drink", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceDrink2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("2) Medium Drink", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceDrink2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 2)
                {
                    e.Graphics.DrawText("3) Small Drink", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceDrink3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("3) Small Drink", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceDrink3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            else if (MainMenuFA)
            {
                e.Graphics.DrawText("Medical Supplies", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);

                if (selection == 0)
                {
                    e.Graphics.DrawText("1) Large FirstAid Kit", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFA1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("1) Large FirstAid Kit", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFA1.ToString();
                    e.Graphics.DrawText(stringPrice, p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 1)
                {
                    e.Graphics.DrawText("2) Medium FirstAid Kit", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFA2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("2) Medium FirstAid Kit", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFA2.ToString();
                    e.Graphics.DrawText(stringPrice, p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selection == 2)
                {
                    e.Graphics.DrawText("3) Small FirstAid Kit", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    stringPrice = "$" + priceFA3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("3) Small FirstAid Kit", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    stringPrice = "$" + priceFA3.ToString();
                    e.Graphics.DrawText(stringPrice, p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
        }
        #endregion
        void CreateSafeHouseBlips(Blip shBlip, Vector3 location)
        {
            shBlip = Blip.AddBlip(location);
            shBlip.Icon = BlipIcon.Building_Safehouse;
            shBlip.Scale = 1.0f;
            shBlip.ShowOnlyWhenNear = true;
            shBlip.Display = BlipDisplay.ArrowAndMap;

        }
        /*BM_burgershot_burg1 	2568528009 	0x9918A089
        BM_burgershot_burg2 	2388036357 	0x8E568B05 
        BM_burgershot_cup 	1324098947 	0x4EEC2583 
        BM_burgershot_fries 	870516668 	0x33E307BC 
        BM_cluckin_burg 	4131702693 	0xF644C7A5 
        BM_cluckin_burg02 	3773306759 	0xE0E81787 
        BM_cluckin_cup 	1369653748 	0x51A341F4 
        BM_cluckin_fries 	3213563275 	0xBF8B158B 
        CJ_J_CAN1 	895164698 	0x355B211A
        CJ_J_CAN2 	3096684429 	0xB893A78D
        CJ_J_CAN3 	3791977071 	0xE204FA6F
        CJ_J_CAN4 	1932696776 	0x73329CC8
        CJ_J_CAN5 	2122363748 	0x7E80B364
        CJ_J_CAN6 	1277447876 	0x4C244EC4
        CJ_J_CAN7 	1442210408 	0x55F66268 
        CJ_FIRST_AID_PICKUP 	1069950328 	0x3FC62578 
         */

        void NeedsTimerTick(object sender, EventArgs e)
        {
                Food_Count();
                Thirst_Count();

            // Show low food/water

                tLowThirstCount++;
                if (lowThirst && tLowThirstCount >= 1)
                {
                    tLowThirstCount = 0;
                    lowThirst = false;
                }
                tLowHungerCounter++;
                if (lowHunger && tLowHungerCounter >= 1)
                {
                    tLowHungerCounter = 0;
                    lowHunger = false;
                }
        }
        void Food_Tick(object sender, EventArgs e)
        {
            #region Player Clerk Vehicle Checks

            if (firstTimeVendors == 1)
            {
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "To enable vendors for the first time, press SHIFT + U", 25000, 1 });
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(RestHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(CafeALG1) <= 5 ||
                Game.LocalPlayer.Character.Position.DistanceTo(CafeALG2) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(ChinaAC) <= 5)
            {
                Rest = true;
                firstTimeVendors++;
            }
            else
            {
                Rest = false;
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(BarHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(ComedyHB) <= 5 ||
                Game.LocalPlayer.Character.Position.DistanceTo(eightBallHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(DartsHB) <= 5 ||
                Game.LocalPlayer.Character.Position.DistanceTo(sClubBH) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(DartsALG) <= 5 ||
                Game.LocalPlayer.Character.Position.DistanceTo(sClubAC) <= 5)
            {
                Bar = true;
                firstTimeVendors++;
            }
            else
            {
                Bar = false;
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(BowlHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerHB) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(BurgerBH1) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerBH2) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG1) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG2) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(BurgerALG3) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(BowlALG) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(BurgerAC) <= 5)
            {
                Bs = true;
                firstTimeVendors++;
            }
            else
            {
                Bs = false;
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(ClunkinALG) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(CluckinHB) <= 5)
            {
                Cb = true;
                firstTimeVendors++;
            }
            else
            {
                Cb = false;
            }
            if (Game.LocalPlayer.Character.Position.DistanceTo(outHospitalHB) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(inHospitalHB) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(outHospitalBH) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(outHospitalALG1) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(outHospitalALG2) <= 5 || Game.LocalPlayer.Character.Position.DistanceTo(outHospitalAC) <= 5 ||
             Game.LocalPlayer.Character.Position.DistanceTo(inHospitalAC) <= 5)
            {
                Hospital = true;
                firstTimeVendors++;
            }
            else
            {
                Hospital = false;
            }
            if (!Hospital && !Rest && !Bar && !Bs && !Cb)
            {
                Inventory = true;
            }
            else
            {
                Inventory = false;
            }
            if (Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.Speed == 0)
            {
                inVehicleStopped = true;
            }
            else
            {
                inVehicleStopped = false;
            }
            #endregion
            #region KeyPress Eat Drink Heal Event

            if (isKeyPressed(eatDrinkHeal) && Game.LocalPlayer.Character.isAlive && !Game.LocalPlayer.Character.isGettingUp && !Game.LocalPlayer.Character.isRagdoll)
            {
                
                AnimationSet healAnims;
                AnimationSet anims;
                AnimationFlags animflags = AnimationFlags.Unknown12 | AnimationFlags.Unknown11 | AnimationFlags.Unknown09;
                if (GTA.Native.Function.Call<bool>("IS_PED_HOLDING_AN_OBJECT", Game.LocalPlayer.Character) && Game.LocalPlayer.Character.Weapons.CurrentType.ToString() == "Misc_Object")
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    GTA.Object heldObject = GTA.Native.Function.Call<GTA.Object>("GET_OBJECT_PED_IS_HOLDING", Game.LocalPlayer.Character);
                    if (heldObject != null && Game.Exists(heldObject))
                    {
                        if (heldObject.Model.ToString() == "0x9918A089" || heldObject.Model.ToString() == "0xF644C7A5" || heldObject.Model.ToString() == "0x33E307BC") // BS Burg, CB Burg, BS Fries
                        {
                            GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", heldObject, 1);
                            anims = new AnimationSet("amb@hotdog_idle");
                            Game.LocalPlayer.Character.Task.PlayAnimation(anims, "eat_stand", 6.4F, animflags);
                            Wait(3000);
                            if (heldObject.Model.ToString() == "0x9918A089") // Bs Burg
                            {
                                mHungerNeeds = mHungerNeeds + foodAdd1;
                                if (mHungerNeeds >= mMaxHunger) { mHungerNeeds = mMaxHunger; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That hit the spot.", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Player.Character); Wait(1000);
                            }
                            else if (heldObject.Model.ToString() == "0xF644C7A5") // CB burg
                            {
                                mHungerNeeds = mHungerNeeds + foodAdd2;
                                if (mHungerNeeds >= mMaxHunger) { mHungerNeeds = mMaxHunger; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That was tasty!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Player.Character); Wait(1000);
                            }
                            else if (heldObject.Model.ToString() == "0x33E307BC") // BS fries
                            {
                                mHungerNeeds = mHungerNeeds + foodAdd3;
                                if (mHungerNeeds >= mMaxHunger) { mHungerNeeds = mMaxHunger; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Mmmmm, that was great!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Player.Character); Wait(1000);
                            }
                        }
                        else if (heldObject.Model.ToString() == "0x4EEC2583" || heldObject.Model.ToString() == "0x51A341F4" || heldObject.Model.ToString() == "0x7E5379BC") // bs cup, cb cup, cj_can3
                        {
                            GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", heldObject, 1);
                            anims = new AnimationSet("amb@bottle_idle");
                            Wait(100);
                            Game.LocalPlayer.Character.Task.PlayAnimation(anims, "drink_walk", 6.4F, animflags);
                            Wait(1200);
                            if (heldObject.Model.ToString() == "0x4EEC2583") // BS cup
                            {
                                mThirstNeeds = mThirstNeeds + drinkAdd1;
                                if (mThirstNeeds >= mMaxThirst) { mThirstNeeds = mMaxThirst; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Quenched my thirst!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                            }
                            else if (heldObject.Model.ToString() == "0x51A341F4") // cb cup
                            {
                                mThirstNeeds = mThirstNeeds + drinkAdd2;
                                if (mThirstNeeds >= mMaxThirst) { mThirstNeeds = mMaxThirst; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Ahhhh refreshing!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                            }
                            else if (heldObject.Model.ToString() == "0x7E5379BC") // cj can3 
                            {
                                mThirstNeeds = mThirstNeeds + drinkAdd3;
                                if (mThirstNeeds >= mMaxThirst) { mThirstNeeds = mMaxThirst; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Buuuurp!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                            }

                        }
                        else if (heldObject.Model.ToString() == "0x3FC62578" && largeFaHeld == true)
                        {
                            if (heldObject.Model.ToString() == "0x3FC62578" && largeFaHeld == true) // CJ_FIRST_AID_PICKUP 	1069950328 	0x3FC62578 
                            {
                                GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", heldObject, 1);
                                healAnims = new AnimationSet("missemergencycall");
                                Game.LocalPlayer.Character.Task.PlayAnimation(healAnims, "player_health_recieve", 6.4F, animflags);
                                Wait(3000);
                            }
                            if (heldObject.Model.ToString() == "0x3FC62578" && largeFaHeld == true)
                            {
                                Game.LocalPlayer.Character.Health = Game.LocalPlayer.Character.Health + faAdd1;
                                if (Game.LocalPlayer.Character.Health >= 100) { Game.LocalPlayer.Character.Health = 100; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You heal yourself with a large firstaid kit!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                                largeFaHeld = false;
                            }
                        } // Heal anims missemergencycall player_health_recieve
                        else if (heldObject.Model.ToString() == "0x3FC62578" && mediumFaHeld == true)
                        {
                            if (heldObject.Model.ToString() == "0x3FC62578" && mediumFaHeld == true) // CJ_FIRST_AID_PICKUP 	1069950328 	0x3FC62578 
                            {
                                GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", heldObject, 1);
                                healAnims = new AnimationSet("missemergencycall");
                                Game.LocalPlayer.Character.Task.PlayAnimation(healAnims, "medic_health_inject", 6.4F, animflags);
                                Wait(3000);
                            }
                            if (heldObject.Model.ToString() == "0x3FC62578" && mediumFaHeld == true)
                            {
                                Game.LocalPlayer.Character.Health = Game.LocalPlayer.Character.Health + faAdd2;
                                if (Game.LocalPlayer.Character.Health >= 100) { Game.LocalPlayer.Character.Health = 100; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You heal yourself with a medium firstaid kit!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                                mediumFaHeld = false;
                            }
                        } // Heal anims missemergencycall player_health_recieve
                        else if (heldObject.Model.ToString() == "0x3FC62578" && smallFaHeld == true)
                        {
                            if (heldObject.Model.ToString() == "0x3FC62578" && smallFaHeld == true) // CJ_FIRST_AID_PICKUP 	1069950328 	0x3FC62578 
                            {
                                GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", heldObject, 1);
                                healAnims = new AnimationSet("missemergencycall");
                                Game.LocalPlayer.Character.Task.PlayAnimation(healAnims, "medic_health_inject", 6.4F, animflags);
                                Wait(3000);
                            }
                            if (heldObject.Model.ToString() == "0x3FC62578" && smallFaHeld == true)
                            {
                                Game.LocalPlayer.Character.Health = Game.LocalPlayer.Character.Health + faAdd3;
                                if (Game.LocalPlayer.Character.Health >= 100) { Game.LocalPlayer.Character.Health = 100; }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You heal yourself with a small firstaid kit!", 1500, 1 });
                                GTA.Native.Function.Call("DETACH_OBJECT", heldObject, Game.LocalPlayer.Character); Wait(1000);
                                smallFaHeld = false;
                            }
                        }
                        else if (heldObject.Model.ToString() != "0x3FC62578")
                        {
                            largeFaHeld = false;
                            mediumFaHeld = false;
                            smallFaHeld = false;
                        }// Heal anims missemergencycall player_health_recieve
                    }
                    Game.LocalPlayer.CanControlCharacter = true;
                }
            }
            #endregion
            #region GreyStars Bool
            if (Game.LocalPlayer.WantedLevel > 0 && Function.Call<bool>("PLAYER_HAS_GREYED_OUT_STARS", Game.LocalPlayer))
            {
                greyStars = true;
            }
            else
            {
                greyStars = false;
            }
            if (Game.LocalPlayer.WantedLevel > 0 && !greyStars && (MainMenuInventory || MainMenuDrinkInventory || MainMenuFoodInventory || MainMenuFaInventory))
            {
                MainMenuInventory = false;
                MainMenuDrinkInventory = false;
                MainMenuFoodInventory = false;
                MainMenuFaInventory = false;
                Game.LocalPlayer.CanControlCharacter = true;
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Cops see me!  Can't check inventory right now!", 6000, 1 });
            }
            else if (Game.LocalPlayer.WantedLevel > 0 && !greyStars && (MainMenuOn || MainMenuDrink || MainMenuFood || MainMenuFA))
            {
                MainMenuOn = false;
                MainMenuDrink = false;
                MainMenuFood = false;
                MainMenuFA = false;
                Game.LocalPlayer.CanControlCharacter = true;
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Cops see me!  Can't check inventory right now!", 6000, 1 });
            }
            #endregion
            #region SafeHouse Enter Exit
            if (safeHousesEnabled)
            {
                if (Game.LocalPlayer.Character.Position.DistanceTo(enterShBh) <= 0.4f && readyToEnterSafeBh)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(exitShBh);
                    Game.LocalPlayer.Character.Heading = 267.35f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    inSafeHouseBh = true;
                    readyToEnterSafeBh = false;
                }
                else if (Game.LocalPlayer.Character.Position.DistanceTo(enterNorthAlg) <= 0.4f && readyToEnterSafeNorthAlg)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(exitNorthAlg);
                    Game.LocalPlayer.Character.Heading = 268.45f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    inSafeHouseNorthAlg = true;
                    readyToEnterSafeNorthAlg = false;
                }
                else if (Game.LocalPlayer.Character.Position.DistanceTo(enterMidAlg) <= 0.4f && readyToEnterSafeMidAlg)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(exitMidAlg);
                    Game.LocalPlayer.Character.Heading = 186.03f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    inSafeHouseMidAlg = true;
                    readyToEnterSafeMidAlg = false;
                }
                // EXIT----------
                if (Game.LocalPlayer.Character.Position.DistanceTo(exitShBh) <= 0.4f && readyToExitSafeBh)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(enterShBh);
                    Game.LocalPlayer.Character.Heading = 90.00f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    outSafeHouseBh = true;
                    readyToExitSafeBh = false;
                }
                else if (Game.LocalPlayer.Character.Position.DistanceTo(exitNorthAlg) <= 0.4 && readyToExitSafeNorthAlg)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(enterNorthAlg);
                    Game.LocalPlayer.Character.Heading = 90.00f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    outSafeHouseNorthAlg = true;
                    readyToExitSafeNorthAlg = false;
                }
                else if (Game.LocalPlayer.Character.Position.DistanceTo(exitMidAlg) <= 0.4 && readyToExitSafeMidAlg)
                {
                    Game.LocalPlayer.CanControlCharacter = false;
                    Game.LocalPlayer.Character.Invincible = true;
                    Game.FadeScreenOut(500);
                    Wait(2000);
                    Game.LocalPlayer.TeleportTo(enterMidAlg);
                    Game.LocalPlayer.Character.Heading = 6.00f;
                    Wait(3000);
                    Game.FadeScreenIn(500);
                    Game.LocalPlayer.CanControlCharacter = true;
                    Game.LocalPlayer.Character.Invincible = false;
                    outSafeHouseMidAlg = true;
                    readyToExitSafeMidAlg = false;
                }
                if (inSafeHouseMidAlg && Game.LocalPlayer.Character.Position.DistanceTo(exitMidAlg) > 3) { readyToExitSafeMidAlg = true; inSafeHouseMidAlg = false; }
                if (inSafeHouseNorthAlg && Game.LocalPlayer.Character.Position.DistanceTo(exitNorthAlg) > 3) { readyToExitSafeNorthAlg = true; inSafeHouseNorthAlg = false; }
                if (inSafeHouseBh && Game.LocalPlayer.Character.Position.DistanceTo(exitShBh) > 3) { readyToExitSafeBh = true; inSafeHouseBh = false; }

                if (outSafeHouseMidAlg && Game.LocalPlayer.Character.Position.DistanceTo(enterMidAlg) > 3) { outSafeHouseMidAlg = false; readyToEnterSafeMidAlg = true; }
                if (outSafeHouseNorthAlg && Game.LocalPlayer.Character.Position.DistanceTo(enterNorthAlg) > 3) { outSafeHouseNorthAlg = false; readyToEnterSafeNorthAlg = true; }
                if (outSafeHouseBh && Game.LocalPlayer.Character.Position.DistanceTo(enterShBh) > 3) { outSafeHouseBh = false; readyToEnterSafeBh = true; }
            }
            #endregion
            #region Start/Stop needs timer on death
            if (Game.LocalPlayer.Character.isDead)
            {
                if (needsTimer_Tick.isRunning)
                {
                    needsTimer_Tick.Stop();
                }
                if (saveTimer_Tick.isRunning)
                {
                    saveTimer_Tick.Stop();
                }
                mHungerNeeds = mMaxHunger;
                mThirstNeeds = mMaxThirst;
                heartyBurger = 0;
                basicBurger = 0;
                basicBurger = 0;
                frenchFries = 0;
                largeDrink = 0;
                mediumDrink = 0;
                smallDrink = 0;
                largeFA = 0;
                mediumFA = 0;
                smallFA = 0;
            }
            else
            { 
                if (!needsTimer_Tick.isRunning)
                    needsTimer_Tick.Start();
                if (!saveTimer_Tick.isRunning)
                    saveTimer_Tick.Start();
            }
            #endregion
            #region AutoSaveEnabled?
            if (autoSaveEnabled)
            {
                if (!saveTimer_Tick.isRunning)
                    saveTimer_Tick.Start();
            }
            else
            {
                if (saveTimer_Tick.isRunning)
                {
                    saveTimer_Tick.Stop();
                }
            }
            #endregion
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
        private void Text(string text, int duration)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, duration, 1);
        }
        void Food_Count()
        {
            if (Game.LocalPlayer.Character.isAlive)
            {
                if (!Game.LocalPlayer.Character.isInVehicle() && (Game.isGameKeyPressed(GameKey.Sprint) && (Game.isGameKeyPressed(GameKey.MoveLeft) || Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveBackward) || Game.isGameKeyPressed(GameKey.MoveForward))))
                {
                    tHungerSubtract = 2;
                }
                else
                    tHungerSubtract = 1;

                mHungerNeeds = mHungerNeeds - tHungerSubtract;
                if (mHungerNeeds >= mMaxHunger)
                {
                    mHungerNeeds = mMaxHunger;
                }
                if (mHungerNeeds < 300 && mHungerNeeds > 0)
                {
                    Game.DisplayText("Your very hungry, go buy something to eat!");
                }
                if (mHungerNeeds < 0)
                {
                    mHungerNeeds = 0;
                    tHungerDamageCounter++;
                    if (tHungerDamageCounter >= 10)
                    {
                        tHungerDamageCounter = 0;
                        Game.LocalPlayer.Character.Health--;
                        Game.DisplayText("Damage due to starvation! Eat something!");
                    }
                }
            }
        }
        void Thirst_Count()
        {
            if (!Game.LocalPlayer.Character.isInVehicle() && (Game.isGameKeyPressed(GameKey.Sprint) && (Game.isGameKeyPressed(GameKey.MoveLeft) || Game.isGameKeyPressed(GameKey.MoveRight) || Game.isGameKeyPressed(GameKey.MoveBackward) || Game.isGameKeyPressed(GameKey.MoveForward))))
            {
                tThirstSubtract = 3;
            }
            else
                tThirstSubtract = 1;

            mThirstNeeds = mThirstNeeds - tThirstSubtract;
            if (mThirstNeeds >= mMaxThirst)
            {
                mThirstNeeds = mMaxThirst;
            }
            if (mThirstNeeds < 300 && mThirstNeeds > 0)
            {
                Game.DisplayText("Your very thirsty, go buy something to drink!");
            }
            if (mThirstNeeds < 0)
            {
                mThirstNeeds = 0;
                tThirstDamageCounter++;
                if (tThirstDamageCounter >= 10)
                {
                    Game.LocalPlayer.Character.Health--;
                    Game.DisplayText("Damage due to dehydration! Drink something!");
                }
            }

        }
        void checkpointBuild(Checkpoint check1, Vector3 target, float checkDiameter, Color color, bool visible)
        {
            check1 = new Checkpoint();
            check1.Position = target;
            check1.Diameter = checkDiameter;
            check1.Color = color;
            check1.Visible = visible;
        }
        void StartFoodMod()
        {
            if (!enableFoodVendors)
            {
                enableFoodVendors = true;
                checkpointBuild(RestClerkHB, RestHB, 3.0f, Color.Red, true);
                checkpointBuild(BarClerkHB, BarHB, 3.0f, Color.Brown, true);
                checkpointBuild(BowlClerkHB, BowlHB, 3.0f, Color.Blue, true);
                checkpointBuild(ComedyClerkHB, ComedyHB, 3.0f, Color.Brown, true);
                checkpointBuild(eightBallClerkHB, eightBallHB, 3.0f, Color.Brown, true);
                checkpointBuild(BurgerClerkHB, BurgerHB, 3.0f, Color.Blue, true);
                checkpointBuild(DartsClerkHB, DartsHB, 3.0f, Color.Brown, true);
                checkpointBuild(CluckinClerkHB, CluckinHB, 3.0f, Color.Yellow, true);
                checkpointBuild(outClerkHB, outHospitalHB, 3.0f, Color.Green, true);
                checkpointBuild(inClerkHB, inHospitalHB, 3.0f, Color.Green, true);
                checkpointBuild(sClubClerkBH, sClubBH, 3.0f, Color.Brown, true);
                checkpointBuild(BurgerClerkBH1, BurgerBH1, 3.0f, Color.Blue, true);
                checkpointBuild(BurgerClerkBH2, BurgerBH2, 3.0f, Color.Blue, true);
                checkpointBuild(outClerkBH, outHospitalBH, 3.0f, Color.Green, true);
                checkpointBuild(BurgerClerkALG1, BurgerALG1, 3.0f, Color.Blue, true);
                checkpointBuild(CafeClerkALG1, CafeALG1, 3.0f, Color.Red, true);
                checkpointBuild(BurgerClerkALG2, BurgerALG2, 3.0f, Color.Blue, true);
                checkpointBuild(ClunkinClerkALG, ClunkinALG, 3.0f, Color.Yellow, true);
                checkpointBuild(DartsClerkALG, DartsALG, 3.0f, Color.Brown, true);
                checkpointBuild(BurgerClerkALG3, BurgerALG3, 3.0f, Color.Blue, true);
                checkpointBuild(BowlClerkALG, BowlALG, 3.0f, Color.Blue, true);
                checkpointBuild(CafeClerkALG2, CafeALG2, 3.0f, Color.Red, true);
                checkpointBuild(outClerkALG1, outHospitalALG1, 3.0f, Color.Green, true);
                checkpointBuild(outClerkALG2, outHospitalALG2, 3.0f, Color.Green, true);
                checkpointBuild(ChinaClerkAC, ChinaAC, 3.0f, Color.Red, true);
                checkpointBuild(BurgerClerkAC, BurgerAC, 3.0f, Color.Blue, true);
                checkpointBuild(sClubClerkAC, sClubAC, 3.0f, Color.Brown, true);
                checkpointBuild(outClerkAC, outHospitalAC, 3.0f, Color.Green, true);
                checkpointBuild(inClerkAC, inHospitalAC, 3.0f, Color.Green, true);
                if (safeHousesEnabled)
                {
                    checkpointBuild(entranceBh, enterShBh, 3.0f, Color.Blue, true);
                    checkpointBuild(entranceHb, enterHb, 3.0f, Color.Blue, true);
                    checkpointBuild(entranceMidAlg, enterMidAlg, 3.0f, Color.Blue, true);
                    checkpointBuild(entranceNorthAlg, enterNorthAlg, 3.0f, Color.Blue, true);
                    checkpointBuild(entranceAc, enterAc, 3.0f, Color.Blue, true);
                }
            }
            else
            {
                Game.DisplayText("Vendors already enabled! Use SHIFT+B to start buying when near vendors!");
            }
        }
        void SaveTimerTick(object sender, EventArgs e)
        {
            #region Save Timer
            saveTime++;
            //Game.DisplayText("Food Save: " + saveTime.ToString(), 2000);
            if (saveTime >= autoSaveInterval && autoSaveEnabled)
            {
                saveTime = 0;
                SaveFoodDat();
            }
            #endregion
        }
        #region Load/Save Function
        void SaveFoodDat()
        {
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "FoodModSaves.ini"));
            ini.SetValue("HeartyBurger", "FOOD", heartyBurger);
            ini.SetValue("BasicBurger", "FOOD", basicBurger);
            ini.SetValue("FrencFries", "FOOD", frenchFries);
            ini.SetValue("LargeDrink", "DRINK", largeDrink);
            ini.SetValue("MediumDrink", "DRINK", mediumDrink);
            ini.SetValue("SmallDrink", "DRINK", smallDrink);
            ini.SetValue("LargeFirstAid", "FIRSTAID", largeFA);
            ini.SetValue("MediumFirstAid", "FIRSTAID", mediumFA);
            ini.SetValue("SmallFirstAid", "FIRSTAID", smallFA);
            ini.SetValue("Hunger", "NEEDS", mHungerNeeds);
            ini.SetValue("Thirst", "NEEDS", mThirstNeeds);
            ini.Save();
            Game.Console.Print("FoodMod Data Saved");
            //Game.DisplayText("FoodMod Data Saved", 5000);
        } // end save()
        void LoadFoodDat()
        {
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "FoodModSaves.ini"));
            ini.Load();
            heartyBurger = ini.GetValueInteger("HeartyBurger", "FOOD", heartyBurger);
            basicBurger = ini.GetValueInteger("BasicBurger", "FOOD", basicBurger);
            frenchFries = ini.GetValueInteger("FrencFries", "FOOD", frenchFries);
            largeDrink = ini.GetValueInteger("LargeDrink", "DRINK", largeDrink);
            mediumDrink = ini.GetValueInteger("MediumDrink", "DRINK", mediumDrink);
            smallDrink = ini.GetValueInteger("SmallDrink", "DRINK", smallDrink);
            largeFA = ini.GetValueInteger("LargeFirstAid", "FIRSTAID", largeFA);
            mediumFA = ini.GetValueInteger("MediumFirstAid", "FIRSTAID", mediumFA);
            smallFA = ini.GetValueInteger("SmallFirstAid", "FIRSTAID", smallFA);
            mHungerNeeds =  ini.GetValueInteger("Hunger", "NEEDS", mHungerNeeds);
            mThirstNeeds = ini.GetValueInteger("Thirst", "NEEDS", mThirstNeeds);
            Game.DisplayText("FoodMod Saves Loaded", 5000);
        } // end load()
        #endregion
    }
}