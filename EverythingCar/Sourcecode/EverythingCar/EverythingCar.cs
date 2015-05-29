using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GTA;
using System.IO;
using GTA.Native;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;

namespace Everything_Car
{
    public class EverythingCar : Script
    {
        #region declaring
        //Floats
        float vehDistance,
            tempDistance,
            vehHead1,
            vehHead2,
            vehHead3,
            vehHead4,
             vehEngHealth1,
            vehEngHealth2,
            vehEngHealth3,
            vehEngHealth4;
        int tVehicleDamageCost;
        int lockCheck;
        int saveTimer = 0;
        //Weapon ints--------
        // Weapons
        int maxVehStorage,
            transferAmt = 0;
        int[] vA1 = new int[12]; // 0 = uzi, 1 = mp5, 2 = pump, 3 = baretta, 4 = ak, 5 = m4, 6 = bolt, 7 = auto, 8 = molotov, 9 = grenade; 10 = glock, 11 = deagle
        int[] vA2 = new int[12];
        int[] vA3 = new int[12];
        int[] vA4 = new int[12];
        int[] vehWeps1 = new int[12];
        int[] vehWeps2 = new int[12];
        int[] vehWeps3 = new int[12];
        int[] vehWeps4 = new int[12];
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
            grenade = 0;

        // Current Amount of guns in vehicle Int
        int akGuns = 0,
            m4Guns = 0,
            glockGuns = 0,
            deagleGuns = 0,
            mp5Guns = 0,
            uziGuns = 0,
            autoGuns = 0,
            boltGuns = 0,
            barettaGuns = 0,
            pumpGuns = 0,
            molGuns = 0,
            grenGuns = 0;
        //String array to fill empty slots
        string[] sFillSlots = new string[14];
        //Strings
        string sAk,
             sM4,
             sGlock,
             sDeagle,
             sUzi,
             sMp5,
             sAuto,
             sBolt,
             sBaretta,
             sPump,
             sMolotov,
             sGrenade,
             sRocket,
             sBat,
             sKnife;
        //string[] vehWeps1 = new string[0];
        //string[] vehWeps2 = new string[0];
        //string[] vehWeps3 = new string[0];
        //string[] vehWeps4 = new string[0];
        //List<string> vehWeps1 = new List<string>();
        //List<string> vehWeps2 = new List<string>();
        //List<string> vehWeps3 = new List<string>();
       // List<string> vehWeps4 = new List<string>();
        BlipColor tBlipColor,
                   mBlipColor1 = BlipColor.Green,
                   mBlipColor2 = BlipColor.Yellow,
                   mBlipColor3 = BlipColor.Purple,
                   mBlipColor4 = BlipColor.Orange;
        // Each Vehicle
        //------------
        int vehAmt = 0,
            totalAmmo = 0,
            totalWeps = 0,
            progBarBase,
            countProgBar,
            vehSave1,
            vehSave2,
            vehSave3,
            vehSave4,
            selectionVeh = 0,
            countAirTime = 0,
            maxAirTime,
            maxAmmo,
            vehCount = 0,
            pos,
            posBlip,
            posBlip1 = 1,
            posBlip2 = 2,
            posBlip3 = 3,
            posBlip4 = 4,
            progBar,
            progCount = 0,
            tireCounter = 0,
            progCd,
            vehHealth1,
            vehHealth2,
            vehHealth3,
            vehHealth4,
            vehLifeTimer,
            xScrPos1,
            yScrPos1,
            xScrPos2,
            yScrPos2;

        Vehicle currentVeh,
            vehTemp1,
            vehTemp2,
            vehTemp3,
            vehTemp4,
            closestMpVeh,
            closestMpCopVeh,
            closestVeh;
        Weapon wepChoice;
        Player lp;
        GTA.Pickup[] pickupArray = new Pickup[12];
        Vehicle[] vehArray = new Vehicle[0];
        Vector3[] vecArray = new Vector3[0];
        Blip[] vehBlipArray = new Blip[0];
        Blip currentBlip;
        GTA.Ped[] genericPedArray;
        GTA.Ped newPed,
            genericPed;
        Player mpPlayer;
        GTA.Object genWep;
        int tMinTireCost,
            tMaxTireCost,
            tMinBodyCostLow,
            tMaxBodyCostLow,
            tMinBodyCostMed,
            tMaxBodyCostMed,
            tMinBodyCostHigh,
            tMaxBodyCostHigh,
            tMinEngCostLow,
            tMaxEngCostLow,
            tMinEngCostMed,
            tMaxEngCostMed,
            tMinEngCostHigh,
            tMaxEngCostHigh;
        // BOOLS
        bool lookingInTrunk1 = false,
            lookingInTrunk2 = false,
            hasWeapons,
            lookingInTrunk3 = false,
            lookingInTrunk4 = false,
            taserEnabled = false,
            repairingEngine = false,
            repairingTires = false,
            repairingBody = false,
            seatBeltOn = false,
            autoUnlockVeh = false,
            menuSelectVeh = false,
            menuVeh1 = false,
            menuVeh2 = false,
            menuVeh3 = false,
            menuVeh4 = false,
            vehClosest1 = false,
            vehClosest2 = false,
            vehClosest3 = false,
            vehClosest4 = false,
            vehLocked1 = false,
            vehLocked2 = false,
            vehLocked3 = false,
            vehLocked4 = false,
            searchCopAuth = false,
            searchAuth = false,
            localPlayerInRadius = false,
            bVehVec1 = false,
            bVehVec2 = false,
            bVehVec3 = false,
            bVehVec4 = false,
            vehStored1 = false,
            vehStored2 = false,
            vehStored3 = false,
            vehStored4 = false;
        //Vehicle Door Damage
        #region Doors
        bool vtFL1 = false,
            vtFR1 = false,
            vtBL1 = false,
            vtBR1 = false,
            vtT1 = false,
            vtH1 = false,
            vtFL2,
            vtFR2,
            vtBL2,
            vtBR2,
            vtT2,
            vtH2,
            vtFL3,
            vtFR3,
            vtBL3,
            vtBR3,
            vtT3,
            vtH3,
            vtFL4,
            vtFR4,
            vtBL4,
            vtBR4,
            vtT4,
            vtH4,
            vEng1,
            vEng2,
            vEng3,
            vEng4;
        #endregion
        //Objects
        GTA.Object[] allObjects;
        GTA.Ped[] allPeds;
        List<Ped> allPedsList = new List<Ped>();
        GTA.Vehicle[] allVehs;
        //Vectors
        Vector3 vehCoords,
               vehCoords1,
               vehCoords2,
               vehCoords3,
               vehCoords4,
               tempVehCoords1,
               tempVehCoords2,
               tempVehCoords3,
               tempVehCoords4,
               vehTrunk,
               vehSideR,
               vehSideL,
               vehHood,
               vehMpHood,
               vehMpTrunk,
               vehMpCopHood,
               vehMpCopTrunk,
                    dG,
                    vehVec1,
                    vehVec2,
                    vehVec3,
                    vehVec4;
        List<Vector3> vehDistinct = new List<Vector3>();
        List<Vector3> vehList = new List<Vector3>();
        // Models
        Model vehModel1,
            vehModel2,
            vehModel3,
            vehModel4;
        //Timers
        GTA.Timer airTime_Tick,
                    progTimer_Tick,
                    showWeps_Tick,
                    syncTimer_Tick,
                    vehToVec_Tick,
                    saveDat_Tick;
        // Veh Colors
        ColorIndex vehColor1,
            vehColor2,
            vehColor3,
            vehColor4;
        //Fonts
        GTA.Font fontSeatBeltOn;
        GTA.Font fontSeatBeltOff;
        GTA.Font mainMenuFont;
        GTA.Font subMenuFont;
        GTA.Font selectionFont;
        GTA.Font fontProgTitle;
        GTA.Object[] genWeps;
        bool autoLoad;
        // ### SAVE/LOAD DECLARATIONS ###
        Vehicle tTempVehicle = null;
        Blip tTempBlip = null;
        Keys toggleAutoSave;
        bool autoSaveEnabled;
        // Individual Vector Floats
        float vV1_X = 0.0f,
              vV1_Y = 0.0f,
              vV1_Z = 0.0f,
              vV2_X = 0.0f,
              vV2_Y = 0.0f,
              vV2_Z = 0.0f,
              vV3_X = 0.0f,
              vV3_Y = 0.0f,
              vV3_Z = 0.0f,
              vV4_X = 0.0f,
              vV4_Y = 0.0f,
              vV4_Z = 0.0f;
        // Individual Vehicle Colors
        int indexVehColor1,
                    indexVehColor2,
                    indexVehColor3,
                    indexVehColor4;
        List<object> vehInfo1 = new List<object>();
        List<object> vehInfo2 = new List<object>();
        List<object> vehInfo3 = new List<object>();
        List<object> vehInfo4 = new List<object>();
        List<int> vehWepInfo1 = new List<int>();
        List<int> vehWepInfo2 = new List<int>();
        List<int> vehWepInfo3 = new List<int>();
        List<int> vehWepInfo4 = new List<int>();
        List<int> vehAmmoInfo1 = new List<int>();
        List<int> vehAmmoInfo2 = new List<int>();
        List<int> vehAmmoInfo3 = new List<int>();
        List<int> vehAmmoInfo4 = new List<int>();
        int waitModifier;
        bool freshLoad = true;
        bool searchFunc = false;
        #endregion
        int autoSaveInterval;
        private void Text(string text, int duration)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, duration, 1);
        }
        private void Text(string text)
        {
            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", "STRING", text, 2000, 1);
        }
        private int R(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
        public static double RoundToNearest(double Amount, double RoundTo)
        {
            double ExcessAmount = Amount % RoundTo;
            if (ExcessAmount < (RoundTo / 2))
            {
                Amount -= ExcessAmount;
            }
            else
            {
                Amount += (RoundTo - ExcessAmount);
            }

            return Amount;
        }
        public EverythingCar()
        {
            saveDat_Tick = new GTA.Timer(1000);
            saveDat_Tick.Tick += new EventHandler(SaveDat_Tick);
            vehToVec_Tick = new GTA.Timer(50);
            vehToVec_Tick.Tick += new EventHandler(VehToVec_Tick);
            airTime_Tick = new GTA.Timer(1000);
            airTime_Tick.Tick += new EventHandler(AirTime_Tick);
            syncTimer_Tick = new GTA.Timer(100);
            syncTimer_Tick.Tick += new EventHandler(SyncTimer_Tick);
            showWeps_Tick = new GTA.Timer(100);
            showWeps_Tick.Tick += new EventHandler(ShowWeps_Tick);
            progTimer_Tick = new GTA.Timer(1000);
            progTimer_Tick.Tick += new EventHandler(ProgTimer_Tick);
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 100);
            this.KeyDown += new GTA.KeyEventHandler(EverythingCar_KeyDown);
            this.Tick += new EventHandler(Car_MainTick);
            tMinTireCost = Settings.GetValueInteger("tMinTireCost", "VEHICLE_SETTINGS", 10);
            tMaxTireCost = Settings.GetValueInteger("tMaxTireCost", "VEHICLE_SETTINGS", 50);
            tMinBodyCostLow = Settings.GetValueInteger("tMinBodyCostLow", "VEHICLE_SETTINGS", 10);
            tMaxBodyCostLow = Settings.GetValueInteger("tMaxBodyCostLow", "VEHICLE_SETTINGS", 50);
            tMinBodyCostMed = Settings.GetValueInteger("tMinBodyCostMed", "VEHICLE_SETTINGS", 25);
            tMaxBodyCostMed = Settings.GetValueInteger("tMaxBodyCostMed", "VEHICLE_SETTINGS", 75);
            tMinBodyCostHigh = Settings.GetValueInteger("tMinBodyCostHigh", "VEHICLE_SETTINGS", 25);
            tMaxBodyCostHigh = Settings.GetValueInteger("tMaxBodyCostHigh", "VEHICLE_SETTINGS", 150);
            tMinEngCostLow = Settings.GetValueInteger("tMinEngCostLow", "VEHICLE_SETTINGS", 10);
            tMaxEngCostLow = Settings.GetValueInteger("tMaxEngCostLow", "VEHICLE_SETTINGS", 30);
            tMinEngCostMed = Settings.GetValueInteger("tMinEngCostMed", "VEHICLE_SETTINGS", 20);
            tMaxEngCostMed = Settings.GetValueInteger("tMaxEngCostMed", "VEHICLE_SETTINGS", 50);
            tMinEngCostHigh = Settings.GetValueInteger("tMinEngCostHigh", "VEHICLE_SETTINGS", 25);
            tMaxEngCostHigh = Settings.GetValueInteger("tMaxEngCostHigh", "VEHICLE_SETTINGS", 70);
            xScrPos1 = Settings.GetValueInteger("SEATBELT_LOCK_UI_X_AXIS", "SETTINGS", 0);
            yScrPos1 = Settings.GetValueInteger("SEATBELT_LOCK_UI_Y_AXIS", "SETTINGS", 0);
            vehAmt = Settings.GetValueInteger("VEHICLES_ALLOWED", "SETTINGS", 4);
            maxAirTime = Settings.GetValueInteger("MAX_AIR_TIME", "SETTINGS", 3);
            maxVehStorage = Settings.GetValueInteger("MAX_VEH_STORAGE_PER_WEP", "VEHICLE_SETTINGS", 5);
            maxAmmo = Settings.GetValueInteger("MAX_VEH_AMMO", "VEHICLE_SETTINGS", 600);
            transferAmt = Settings.GetValueInteger("AMMO_TRANSFER_AMMOUNT", "VEHICLE_SETTINGS", 10);
            taserEnabled = Settings.GetValueBool("TASER_ENABLED", "SETTINGS", true);
            autoSaveInterval = Settings.GetValueInteger("AUTOSAVE_INTERVAL", "SETTINGS", 300);
            Vehicle[] vehArray = new Vehicle[vehAmt];
            vehBlipArray = new Blip[vehAmt];
            this.vehArray = vehArray;
            Keys saveCurrentVeh,
                seatBeltToggle,
                lockDoors,
                repairVehicle,
                vehicleStatusMenu,
                popTrunk,
                popHood,
                placeAmmoVeh,
                placeWepVeh,
                openCloseTrunkHood,
                spawnPickup,
                cleanServer,
                attachVehicle,
                searchVehicle,
                instaCop,
                forceSave,
                loadGame;
            saveCurrentVeh = Settings.GetValueKey("SAVE_CURRENT_VEHICLE", "CONTROLS", Keys.S);
            seatBeltToggle = Settings.GetValueKey("SEAT_BELT_TOGGLE", "CONTROLS", Keys.L);
            lockDoors = Settings.GetValueKey("LOCK_DOORS", "CONTROLS", Keys.L);
            repairVehicle = Settings.GetValueKey("REPAIR_VEHICLE", "CONTROLS", Keys.R);
            vehicleStatusMenu = Settings.GetValueKey("VEHICLE_STATUS_MENU", "CONTROLS", Keys.N);
            popTrunk = Settings.GetValueKey("POP_TRUNK", "CONTROLS", Keys.Q);
            popHood = Settings.GetValueKey("POP_HOOD", "CONTROLS", Keys.Q);
            placeAmmoVeh = Settings.GetValueKey("PLACE_AMMO_VEHICLE", "CONTROLS", Keys.T);
            placeWepVeh = Settings.GetValueKey("PLACE_WEAPON_IN_VEHICLE", "CONTROLS", Keys.T);
            openCloseTrunkHood = Settings.GetValueKey("OPEN_CLOSE_TRUNK_HOOD", "CONTROLS", Keys.H);
            //cleanServer = Settings.GetValueKey("CLEAN_SERVER", "CONTROLS", Keys.K);
            attachVehicle = Settings.GetValueKey("ATTACH_VEHICLE", "CONTROLS", Keys.A);
            searchVehicle = Settings.GetValueKey("SEARCH_VEHICLE", "CONTROLS", Keys.H);
            //instaCop = Settings.GetValueKey("INSTA_COP", "CONTROLS", Keys.P);
            forceSave = Settings.GetValueKey("FORCE_SAVE_KEY", "CONTROLS", Keys.S);
            loadGame = Settings.GetValueKey("LOAD_SAVED_DATA", "CONTROLS", Keys.L);
            autoLoad = Settings.GetValueBool("AUTO_LOAD_ENABLED", "SETTINGS", true);
            searchFunc = Settings.GetValueBool("SEARCH_FUNC_ENABLED", "SETTINGS", false);
            toggleAutoSave = Settings.GetValueKey("ENABLE_DISABLE_AUTOSAVE_FUNCTION", "CONTROLS", Keys.A);
            autoSaveEnabled = Settings.GetValueBool("AUTO_SAVE_ENABLED", "SETTINGS", false);
            BindKey(loadGame, true, true, true, LoadDat);
                    // shift ctrl alt
            BindKey(forceSave, true, true, true, SaveDat);
            BindKey(saveCurrentVeh, false, false, true, SaveCurrentVeh);
            BindKey(seatBeltToggle, false, false, false, SeatBeltToggle);
            BindKey(lockDoors, true, false, false, LockDoors);
            BindKey(repairVehicle, true, false, false, RepairVehicle);
            BindKey(vehicleStatusMenu, false, true, false, VehicleStatusMenu);
            BindKey(popTrunk, true, false, false, PopTrunk);
            BindKey(popHood, true, true, false, PopHood);
            BindKey(placeAmmoVeh, false, false, false, PlaceAmmoVeh);
            BindKey(placeWepVeh, true, false, false, PlaceWepVeh);
            BindKey(openCloseTrunkHood, true, false, false, OpenCloseTrunkHood);
            BindKey(toggleAutoSave, true, true, true, Toggle_AutoSave);
            //BindKey(Keys.S, false, true, false, SpawnPed);
            //BindKey(Keys.G, true, false, false, SpawnPickup);
            //BindKey(cleanServer, true, true, true, CleanServer);
            BindKey(Keys.A, true, false, false, AttachVehicle);
            BindKey(Keys.H, true, false, false, SearchVehicle);
            lp = Game.LocalPlayer;
            LoadMenuFont();
            PerFrameDrawing += new GraphicsEventHandler(EverythingCar_PerFrameDraw);
            AssignAmmoArrays();
            List<Vector3> vehDistinct = vehList.Distinct().ToList();       
            saveDat_Tick.Start();
            if (autoLoad)
            LoadDat();
        }
        void SaveDat_Tick(object sender, EventArgs e)
        {
            saveTimer++;
            //Game.DisplayText("Vehicle Save in: " + saveTimer.ToString(), 2000);
            if (saveTimer >= autoSaveInterval && autoSaveEnabled)
            {
                SaveDat();
                saveTimer = 0;
            }
        }
        void EverythingCar_KeyDown(object sender, GTA.KeyEventArgs e)
        {
            #region Is Car Locked Warning
            if (isKeyPressed(Keys.F) && lp.Character.isInVehicle() && lp.Character.CurrentVehicle.DoorLock == DoorLock.ImpossibleToOpen)
            {
                Text("Your car doors are locked!  To unlock press LSHIFT + L", 5000);
            }

            #endregion
            #region Adjust Transfer Amt
            if (isKeyPressed(Keys.RShiftKey) && isKeyPressed(Keys.Left))
            {
                transferAmt -= 10;
                if (transferAmt <= 1)
                    transferAmt = 1;
                Game.DisplayText("Ammo Transfer Amount is at: " + transferAmt.ToString(), 3000);
            }
            if (isKeyPressed(Keys.RShiftKey) && isKeyPressed(Keys.Right))
            {
                transferAmt += 10;
                if (transferAmt >= 100)
                    transferAmt = 100;
                Game.DisplayText("Ammo Transfer Amount is at: " + transferAmt.ToString(), 3000);
            }
            #endregion
            #region MenuSelect Keys
            #region Scroll up and Down
            if (e.Key == Keys.NumPad8)
            {
                if (menuSelectVeh)
                {
                    selectionVeh--;
                    if (selectionVeh <= 1)
                        selectionVeh = 1;
                }
                if (menuVeh1 || menuVeh2 || menuVeh3 || menuVeh4)
                {
                    selectionVeh--;
                    if (selectionVeh <= 1)
                        selectionVeh = 1;
                }

            }
            if (e.Key == Keys.NumPad2)
            {
                if (menuSelectVeh)
                {
                    selectionVeh++;
                    if (selectionVeh >= vehAmt)
                        selectionVeh = vehAmt;
                }
                if (menuVeh1 || menuVeh2 || menuVeh3 || menuVeh4)
                {
                    selectionVeh++;
                    if (selectionVeh >= 12)
                        selectionVeh = 12;
                }
            }
            #endregion
            #region Select/Delete
            if (e.Key == Keys.NumPad5)
            {
                // Take Weapons if near trunk
                if (menuVeh1)
                {
                    if (lookingInTrunk1)
                    {
                        GrabGun(vehWeps1, vA1);
                        CalcWeaponStorage(vehWeps1);
                    }
                    else Text("Your not close enough to the trunk, make sure your near the vehicle with GREEN blip.", 6000);
                }
                else if (menuVeh2)
                {
                    if (lookingInTrunk2)
                    {
                        GrabGun(vehWeps2, vA2);
                        CalcWeaponStorage(vehWeps2);
                    }
                    else Text("Your not close enough to the trunk, make sure your near the vehicle with YELLOW blip.", 6000);
                }
                else if (menuVeh3)
                {
                    if (lookingInTrunk3)
                    {
                        GrabGun(vehWeps3, vA3);
                        CalcWeaponStorage(vehWeps3);
                    }
                    else Text("Your not close enough to the trunk, make sure your near the vehicle with PURPLE blip.", 6000);
                }
                else if (menuVeh4)
                {

                    if (lookingInTrunk4)
                    {
                        GrabGun(vehWeps4, vA4);
                        CalcWeaponStorage(vehWeps4);
                    }
                    else Text("Your not close enough to the trunk, make sure your near the vehicle with ORANGE blip.", 6000);
                
                }
                // Main Vehicle Menu----
                if (menuSelectVeh == true && selectionVeh == 1 && vehSave1 == 1)
                {
                    CalcWeaponStorage(vehWeps1);
                    menuSelectVeh = false;
                    menuVeh1 = true;
                }
                if (menuSelectVeh == true && selectionVeh == 2 && vehSave2 == 1)
                {
                    CalcWeaponStorage(vehWeps2);
                    menuSelectVeh = false;
                    menuVeh2 = true;
                }
                if (menuSelectVeh == true && selectionVeh == 3 && vehSave3 == 1)
                {
                    CalcWeaponStorage(vehWeps3);
                    menuSelectVeh = false;
                    menuVeh3 = true;
                }
                if (menuSelectVeh == true && selectionVeh == 4 && vehSave4 == 1)
                {
                    CalcWeaponStorage(vehWeps4);
                    menuSelectVeh = false;
                    menuVeh4 = true;
                }
            }
            if (e.Key == Keys.NumPad9)
            {
                UnSaveVehicle();
            }
            #endregion
            #region Exit Menu
            if (e.Key == Keys.NumPad0)
            {
                if (menuVeh1 || menuVeh2 || menuVeh3 || menuVeh4)
                {
                    ClearWeaponStorage();
                    selectionVeh = 1;
                    menuVeh1 = false;
                    menuVeh2 = false;
                    menuVeh3 = false;
                    menuVeh4 = false;
                    menuSelectVeh = true;
                    return;
                }
                if (menuSelectVeh == true)
                    menuSelectVeh = false;
            }
            #endregion
            #endregion
        }
        void ProgTimer_Tick(object sender, EventArgs e)
        {
            progCount = progCount + 1000;
            progCd = progCd - 1000;
            Game.DisplayText("Repair Time Left (seconds): ");
            Game.DisplayText("Repair Time Left (seconds): " + (progCd / 1000).ToString());
            Text("Repair Time Left (seconds): " + (progCd / 1000).ToString());
            if (progCount >= progBar)
            {
                if (repairingEngine)
                {
                    if (closestVeh.Door(VehicleDoor.Hood).isOpen && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 4, true))
                    {
                        lp.Character.Animation.Play(new AnimationSet("missgun_car"), "shut_trunk", 8f, AnimationFlags.Unknown05);
                        Wait(500);
                        closestVeh.Door(VehicleDoor.Hood).Close();
                        Wait(1000);
                    }
                    lp.Character.Task.ClearAllImmediately();
                    closestVeh.EngineHealth = 1000;
                    repairingEngine = false;
                    progCount = 0;
                    Text("Engine Repair Complete, COSTS: $" + tVehicleDamageCost + ".00");
                    lp.Money -= tVehicleDamageCost;
                    if (progTimer_Tick.isRunning)
                        progTimer_Tick.Stop();
                }
                else if (repairingTires)
                {
                    if (tireCounter > 1)
                    Text("Tire Repair Complete, COSTS: $" + tVehicleDamageCost + ".00 for all tires");
                    else
                        Text("Tire Repair Complete, COSTS: $" + tVehicleDamageCost + ".00 for the tire");
                    lp.Money -= tVehicleDamageCost;
                    lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "getofftrolley", 1.5f, AnimationFlags.Unknown04);
                    Wait(1500);
                    lp.Character.Task.ClearAllImmediately();
                    closestVeh.FixTire(VehicleWheel.CenterLeft); closestVeh.FixTire(VehicleWheel.CenterRight); closestVeh.FixTire(VehicleWheel.FrontLeft); closestVeh.FixTire(VehicleWheel.FrontRight); closestVeh.FixTire(VehicleWheel.RearLeft); closestVeh.FixTire(VehicleWheel.RearRight);
                    repairingTires = false;
                    tireCounter = 0;
                    progCount = 0;
                    if (progTimer_Tick.isRunning)
                        progTimer_Tick.Stop();
                }
                else if (repairingBody)
                {
                    Text("Body Repair Complete, COSTS: $" + tVehicleDamageCost + ".00");
                    lp.Money -= tVehicleDamageCost;
                    lp.Character.Animation.Play(new AnimationSet("missgun_car"), "shut_trunk", 8f, AnimationFlags.Unknown05);
                    Wait(1500);
                    lp.Character.Task.ClearAllImmediately();
                    if (closestVeh.EngineHealth < 1000)
                    {
                        float tTempEngH = 1000;
                        tTempEngH = closestVeh.EngineHealth;
                        closestVeh.Repair();
                        closestVeh.EngineHealth = tTempEngH;
                    }
                    else
                        closestVeh.Repair();
                    repairingBody = false;
                    progCount = 0;
                    if (progTimer_Tick.isRunning)
                        progTimer_Tick.Stop();
                }
            }
        }
        void ShowWeps_Tick(object sender, EventArgs e)
        {
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {
                    this.mpPlayer = mpPlayer;
                    if (Game.Exists(this.mpPlayer.Character) && lp.Character != this.mpPlayer.Character)
                    {
                        if (this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                        {
                            if (Game.Exists(this.mpPlayer.Character) && !this.mpPlayer.Character.isInVehicle() && ((Game.Exists(vehArray[0]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[0].Position) <= 15) || (Game.Exists(vehArray[1]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[1].Position) <= 15) || (Game.Exists(vehArray[2]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[2].Position) <= 15) || (Game.Exists(vehArray[3]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[3].Position) <= 15)))
                            {
                                if (vehSave1 == 1 && CheckWepValues(vehWeps1) == true)
                                {
                                    ShowWepsBeingSearched(vehWeps1, vA1, vehArray[0], this.mpPlayer);
                                }
                            }
                        }
                        else
                        {
                            if (Game.Exists(this.mpPlayer.Character) && !this.mpPlayer.Character.isInVehicle() && ((Game.Exists(vehArray[0]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[0].Position) <= 15) || (Game.Exists(vehArray[1]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[1].Position) <= 15) || (Game.Exists(vehArray[2]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[2].Position) <= 15) || (Game.Exists(vehArray[3]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[3].Position) <= 15)))
                            {
                                if (vehSave1 == 1 && CheckWepValues(vehWeps1) == true)
                                {
                                    ShowWepsBeingSearched(vehWeps1, vA1, vehArray[0], this.mpPlayer);
                                }
                            }
                        }
                    }
                }
            }
        }
        void SyncTimer_Tick(object sender, EventArgs e)
        {
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {
                    this.mpPlayer = mpPlayer;
                    if (Game.Exists(this.mpPlayer.Character) && lp.Character != this.mpPlayer.Character)
                    {
                        if (this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                        {
                            #region Cop sync Open/Close trunk and Locking
                            closestMpCopVeh = World.GetClosestVehicle(this.mpPlayer.Character.Position, 10);
                            if (Game.Exists(closestMpCopVeh))
                            {
                                vehMpCopHood = closestMpCopVeh.GetOffsetPosition(new Vector3(0.0f, +2.8f, 0.0f));
                                vehMpCopTrunk = closestMpCopVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                                IsMpPlayerOpenCloseTrunk(this.mpPlayer, closestMpCopVeh, vehMpCopTrunk, vehMpCopHood);
                            }
                            #endregion
                        }
                        else
                        {
                            #region Crook Sync Open/Close Trunk And Locking
                            closestMpVeh = World.GetClosestVehicle(this.mpPlayer.Character.Position, 10);
                            if (Game.Exists(closestMpVeh))
                            {
                                vehMpHood = closestMpVeh.GetOffsetPosition(new Vector3(0.0f, +2.8f, 0.0f));
                                vehMpTrunk = closestMpVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                                IsMpPlayerOpenCloseTrunk(this.mpPlayer, closestMpVeh, vehMpTrunk, vehMpHood);
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        void VehToVec_Tick(object sender, EventArgs e)
        {
            if (Game.isMultiplayer && !freshLoad)
            {
                if (vehSave1 == 1 && bVehVec1 == true)
                {
                    if (lp.Character.Position.DistanceTo(vehVec1) < 200)
                    {
                        CreateNewLoadedVehicle(ref vehArray[0], ref vehTemp1, ref vehModel1, ref vehVec1, ref vehColor1, ref vehBlipArray[0], ref vehHead1, ref vehEngHealth1, ref vehHealth1, ref vehSave1, ref posBlip1, ref vEng1, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1, ref vehLocked1, ref mBlipColor1, ref vehStored1);
                        bVehVec1 = false;
                    }
                }
                if (vehSave2 == 1 && bVehVec2 == true)
                {
                    if (lp.Character.Position.DistanceTo(vehVec2) < 200)
                    {
                        CreateNewLoadedVehicle(ref vehArray[1], ref vehTemp2, ref vehModel2, ref vehVec2, ref vehColor2, ref vehBlipArray[1], ref vehHead2, ref vehEngHealth2, ref vehHealth2, ref vehSave2, ref posBlip2, ref vEng2, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2, ref vehLocked2, ref mBlipColor2, ref vehStored2);
                        bVehVec2 = false;
                    }
                }
                if (vehSave3 == 1 && bVehVec3 == true)
                {
                    if (lp.Character.Position.DistanceTo(vehVec3) < 200)
                    {
                        CreateNewLoadedVehicle(ref vehArray[2], ref vehTemp3, ref vehModel3, ref vehVec3, ref vehColor3, ref vehBlipArray[2], ref vehHead3, ref vehEngHealth3, ref vehHealth3, ref vehSave3, ref posBlip3, ref vEng3, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3, ref vehLocked3, ref mBlipColor3, ref vehStored3);
                        bVehVec3 = false;
                    }
                }
                if (vehSave4 == 1 && bVehVec4 == true)
                {
                    if (lp.Character.Position.DistanceTo(vehVec4) < 200)
                    {
                        CreateNewLoadedVehicle(ref vehArray[3], ref vehTemp4, ref vehModel4, ref vehVec4, ref vehColor4, ref vehBlipArray[3], ref vehHead4, ref vehEngHealth4, ref vehHealth4, ref vehSave4, ref posBlip4, ref vEng4, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4, ref vehLocked4, ref mBlipColor4, ref vehStored4);
                        bVehVec4 = false;
                    }
                }
            }
        }
        #region Air Time Tick
        void AirTime_Tick(object sender, EventArgs e)
        {
            countAirTime++;
            Vector3 nG = lp.Character.Position.ToGround();
            if (Game.Exists(lp.Character.CurrentVehicle) && countAirTime >= maxAirTime && lp.Character.CurrentVehicle.Position.DistanceTo(nG) <= 3.5)
            {
                Wait(1000);
                if (Game.Exists(lp.Character.CurrentVehicle) && lp.Character.isInVehicle() && lp.Character.CurrentVehicle.isUpright && lp.Character.CurrentVehicle.isOnAllWheels)
                {
                    lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontRight);
                    lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterRight);
                    lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearRight);
                    if (airTime_Tick.isRunning)
                        airTime_Tick.Stop();
                    return;
                }
            }
            if (Game.Exists(lp.Character.CurrentVehicle) && countAirTime >= (maxAirTime - 1) && lp.Character.CurrentVehicle.Position.DistanceTo(nG) <= 2.5)
            {
                if (Game.Exists(lp.Character.CurrentVehicle) && lp.Character.isInVehicle() && lp.Character.CurrentVehicle.isUpright)
                {
                    int randomTires = R(1, 10);
                    if (randomTires < 4)
                    {
                        if (randomTires == 1)
                        lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontLeft);
                        if (randomTires == 2)
                        lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearRight);
                        if (randomTires == 3)
                        lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterLeft);
                    }
                    else if (randomTires >= 4 && randomTires < 7)
                    {
                        if (randomTires == 4)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontRight);
                        if (randomTires == 5)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearLeft);
                        if (randomTires == 6)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterRight);
                    }
                    else if (randomTires >= 7 && randomTires < 10)
                    {
                        if (randomTires == 7)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontRight);
                        if (randomTires == 8)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.CenterRight);
                        if (randomTires == 9)
                            lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearLeft); lp.Character.CurrentVehicle.BurstTire(VehicleWheel.RearRight);
                    }
                    if (airTime_Tick.isRunning)
                        airTime_Tick.Stop();
                    return;
                }
            }
            if (!lp.Character.isInVehicle())
            {
                Vector3 vG = lp.LastVehicle.Position.ToGround();
                if (Game.Exists(lp.LastVehicle) && countAirTime >= maxAirTime && lp.LastVehicle.Position.DistanceTo(vG) <= 3.5)
                {
                    Wait(1000);
                    if (Game.Exists(lp.LastVehicle) && lp.LastVehicle.isUpright && lp.LastVehicle.isOnAllWheels)
                    {
                        lp.LastVehicle.BurstTire(VehicleWheel.FrontLeft); lp.LastVehicle.BurstTire(VehicleWheel.FrontRight);
                        lp.LastVehicle.BurstTire(VehicleWheel.CenterLeft); lp.LastVehicle.BurstTire(VehicleWheel.CenterRight);
                        lp.LastVehicle.BurstTire(VehicleWheel.RearLeft); lp.LastVehicle.BurstTire(VehicleWheel.RearRight);
                        if (airTime_Tick.isRunning)
                            airTime_Tick.Stop();
                        return;
                    }
                    if (airTime_Tick.isRunning)
                        airTime_Tick.Stop();
                    return;
                }
            }
        }
        #endregion
        #region main tick
        void Car_MainTick(object sender, EventArgs e)
        {

            #region Criminal or Cop player
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {
                    this.mpPlayer = mpPlayer;
                    if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.Character != this.mpPlayer.Character)
                    {
                        if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                        {
                            CopSettings();
                        }
                        else
                        {
                            CrookSettings();
                        }
                    }
                }
            }

            #endregion
            #region Auto Unlock Veh on exit
            if (lp.Character.isInVehicle() && vehSave1 == 1 && Game.Exists(vehArray[0]) && lp.Character.Position.DistanceTo(vehArray[0].Position) < 200 && vehClosest1 && vehLocked1 || lp.Character.isInVehicle() && vehSave2 == 1 && Game.Exists(vehArray[1]) && lp.Character.Position.DistanceTo(vehArray[1].Position) < 200 && vehClosest2 && vehLocked2 || lp.Character.isInVehicle() && vehSave3 == 1 && Game.Exists(vehArray[2]) && lp.Character.Position.DistanceTo(vehArray[2].Position) < 200 && vehClosest3 && vehLocked3 || lp.Character.isInVehicle() && vehSave4 == 1 && Game.Exists(vehArray[3]) && lp.Character.Position.DistanceTo(vehArray[3].Position) < 200 && vehClosest4 && vehLocked4)
                autoUnlockVeh = true;

            if (autoUnlockVeh == true && !lp.Character.isInVehicle() && vehClosest1 && vehLocked1)
            {
                autoUnlockVeh = false;
                vehLocked1 = false;
            }
            if (autoUnlockVeh == true && !lp.Character.isInVehicle() && vehClosest2 && vehLocked2)
            {
                autoUnlockVeh = false;
                vehLocked2 = false;
            }
            if (autoUnlockVeh == true && !lp.Character.isInVehicle() && vehClosest3 && vehLocked3)
            {
                autoUnlockVeh = false;
                vehLocked3 = false;
            }
            if (autoUnlockVeh == true && !lp.Character.isInVehicle() && vehClosest4 && vehLocked4)
            {
                autoUnlockVeh = false;
                vehLocked4 = false;
            }
            #endregion
            #region Veh Location Check
            List<Vector3> vehDistinct = vehList.Distinct().ToList();
            if (Game.Exists(vehArray[0]) && lp.Character.Position.DistanceTo(vehArray[0].Position) < 200)
            {
                vehCoords1 = vehArray[0].Position;
                if (vehDistinct.Contains(vehCoords1))
                {
                    vehDistinct.Remove(vehCoords1);
                    vehDistinct.Add(vehCoords1);
                }
                else
                    vehDistinct.Add(vehCoords1);
            }
            if (Game.Exists(vehArray[1]) && lp.Character.Position.DistanceTo(vehArray[1].Position) < 200)
            {
                vehCoords2 = vehArray[1].Position;
                if (vehDistinct.Contains(vehCoords2))
                {
                    vehDistinct.Remove(vehCoords2);
                    vehDistinct.Add(vehCoords2);
                }
                else
                    vehDistinct.Add(vehCoords2);
            }
            if (Game.Exists(vehArray[2]) && lp.Character.Position.DistanceTo(vehArray[2].Position) < 200)
            {
                vehCoords3 = vehArray[2].Position;
                if (vehDistinct.Contains(vehCoords3))
                {
                    vehDistinct.Remove(vehCoords3);
                    vehDistinct.Add(vehCoords3);
                }
                else
                    vehDistinct.Add(vehCoords3);
            }
            if (Game.Exists(vehArray[3]) && lp.Character.Position.DistanceTo(vehArray[3].Position) < 200)
            {
                vehCoords4 = vehArray[3].Position;
                if (vehDistinct.Contains(vehCoords4))
                {
                    vehDistinct.Remove(vehCoords4);
                    vehDistinct.Add(vehCoords4);
                }
                else
                    vehDistinct.Add(vehCoords4);
            }

            #endregion
            #region Vehicle Closest
            // NEEDS FIX

            if (vehSave1 == 1 || vehSave2 == 1 || vehSave3 == 1 || vehSave4 == 1)
            {
                vehDistance = float.MaxValue;
                for (int i = 0; i < vehDistinct.Count; i++)
                {
                    tempDistance = lp.Character.Position.DistanceTo(vehDistinct[i]);

                    if (tempDistance < vehDistance)
                    {
                        vehDistance = tempDistance;
                        if (Game.Exists(vehArray[0]) && lp.Character.Position.DistanceTo(vehArray[0].Position) < 200 && vehDistinct[i].DistanceTo(vehArray[0].Position) <= 0.4)
                        {
                            vehClosest1 = true;
                            vehClosest2 = false;
                            vehClosest3 = false;
                            vehClosest4 = false;
                        }
                        if (Game.Exists(vehArray[1]) && lp.Character.Position.DistanceTo(vehArray[1].Position) < 200 && vehDistinct[i].DistanceTo(vehArray[1].Position) <= 0.4)
                        {
                            vehClosest1 = false;
                            vehClosest2 = true;
                            vehClosest3 = false;
                            vehClosest4 = false;
                        }
                        if (Game.Exists(vehArray[2]) && lp.Character.Position.DistanceTo(vehArray[2].Position) < 200 && vehDistinct[i].DistanceTo(vehArray[2].Position) <= 0.4)
                        {
                            vehClosest1 = false;
                            vehClosest2 = false;
                            vehClosest3 = true;
                            vehClosest4 = false;
                        }
                        if (Game.Exists(vehArray[3]) && lp.Character.Position.DistanceTo(vehArray[3].Position) < 200 && vehDistinct[i].DistanceTo(vehArray[3].Position) <= 0.4)
                        {
                            vehClosest1 = false;
                            vehClosest2 = false;
                            vehClosest3 = false;
                            vehClosest4 = true;
                        }
                    }
                }
            }

            #endregion
            #region SeatBelt Check
            if (!lp.Character.isInVehicle())
                seatBeltOn = false;
            if (seatBeltOn == false)
                lp.Character.WillFlyThroughWindscreen = true;
            else
                lp.Character.WillFlyThroughWindscreen = false;
            #endregion
            #region Vehicle Status Check not working
            /* #region Check Saved Vehicles Status NOT WORKING YET
        if (vehSave1 == 1)
        {
            if (Game.Exists(vehArray[0]) && lp.Character.Position.DistanceTo(vehArray[0].Position) < 200 && !vehStored1)
            {
                if (!vehArray[0].isAlive)
                {
                    if (Game.Exists(vehBlipArray[0]))
                    {
                        vehBlipArray[0].Delete();
                    }
                    vehArray[0].Delete();
                    vehSave1 = 0;
                    vehCount--;
                }
            }
        }
        if (vehSave2 == 1)
        {
            if (Game.Exists(vehArray[1]) && lp.Character.Position.DistanceTo(vehArray[1].Position) < 200 && !vehStored2)
            {
                if (!vehArray[1].isAlive)
                {
                    if (Game.Exists(vehBlipArray[1]))
                    {
                        vehBlipArray[1].Delete();
                    }
                    vehArray[1].Delete();
                    vehSave2 = 0;
                    vehCount--;
                }
            }
            else
            {
                vehSave2 = 0;
                vehCount--;
            }
        }
        if (vehSave3 == 1)
        {
            if (Game.Exists(vehArray[2]) && lp.Character.Position.DistanceTo(vehArray[2].Position) < 200 && !vehStored3)
            {
                if (!vehArray[2].isAlive)
                {
                    if (Game.Exists(vehBlipArray[2]))
                    {
                        vehBlipArray[2].Delete();
                    }
                    vehArray[2].Delete();
                    vehSave3 = 0;
                    vehCount--;
                }
            }
            else
            {
                vehSave3 = 0;
                vehCount--;
            }
        }
        if (vehSave4 == 1)
        {
            if (Game.Exists(vehArray[3]) && lp.Character.Position.DistanceTo(vehArray[3].Position) < 200 && !vehStored4)
            {
                if (!vehArray[3].isAlive)
                {
                    if (Game.Exists(vehBlipArray[3]))
                    {
                        vehBlipArray[3].Delete();
                    }
                    vehArray[3].Delete();
                    vehSave4 = 0;
                    vehCount--;
                }
            }
            else
            {
                vehSave4 = 0;
                vehCount--;
            }
        }
        #endregion*/
    #endregion
            #region Pop tires on high jump
            if (lp.Character.isInVehicle())
            {
                Vector3 dG = lp.Character.Position.ToGround();
                this.dG = dG;
            }
            if (lp.Character.isInVehicle() && Game.Exists(lp.Character.CurrentVehicle) && lp.Character.CurrentVehicle.isOnAllWheels == false && lp.Character.CurrentVehicle.Position.DistanceTo(this.dG) >= 6f)
            {
                if (!airTime_Tick.isRunning)
                {
                    airTime_Tick.Start();
                }
            }
            #endregion
            #region menu open death
            if (!lp.Character.isAlive && (menuVeh1 || menuVeh2 || menuVeh3 || menuVeh4))
            {
                ClearWeaponStorage();
                selectionVeh = 1;
                menuVeh1 = false;
                menuVeh2 = false;
                menuVeh3 = false;
                menuVeh4 = false;
            }
            #endregion
            #region Player at saved trunk
            if (vehClosest1 == true && vehSave1 == 1 && Game.Exists(vehArray[0]) && lp.Character.Position.DistanceTo(vehArray[0].Position) <= 10)
            {
                TrunkBool(vehArray[0]);
                if (TrunkBool(vehArray[0]) == true)
                    lookingInTrunk1 = true;
                else { lookingInTrunk1 = false; }
            }
            if (vehClosest2 == true && vehSave2 == 1 && Game.Exists(vehArray[1]) && lp.Character.Position.DistanceTo(vehArray[1].Position) <= 10)
            {
                TrunkBool(vehArray[1]);
                if (TrunkBool(vehArray[1]) == true)
                    lookingInTrunk2 = true;
                else { lookingInTrunk2 = false; }
            }
            if (vehClosest3 == true && vehSave3 == 1 && Game.Exists(vehArray[2]) && lp.Character.Position.DistanceTo(vehArray[2].Position) <= 10)
            {
                TrunkBool(vehArray[2]);
                if (TrunkBool(vehArray[2]) == true)
                    lookingInTrunk3 = true;
                else { lookingInTrunk3 = false; }
            }
            if (vehClosest4 == true && vehSave4 == 1 && Game.Exists(vehArray[3]) && lp.Character.Position.DistanceTo(vehArray[3].Position) <= 10)
            {
                TrunkBool(vehArray[3]);
                if (TrunkBool(vehArray[3]) == true)
                    lookingInTrunk4 = true;
                else { lookingInTrunk4 = false; }
            }
            #endregion
            #region MULTIPLAYER DESPAWN VEHICLE WHEN > 200 FROM VEHICLE
            if (Game.isMultiplayer && !freshLoad)
            {
                if (vehSave1 == 1 && Game.Exists(vehArray[0]) && vehArray[0].Position.DistanceTo(lp.Character.Position) > 200)
                {
                    bVehVec1 = true;
                    posBlip1 = 1;
                    SetTempVehPos(ref vehArray[0], ref vehTemp1, ref vehModel1, ref vehVec1, ref vehColor1, ref vehBlipArray[0], ref vehHead1, ref vehEngHealth1, ref vehHealth1, ref vehSave1, ref posBlip1, ref vEng1, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1, ref vehLocked1, ref indexVehColor1, ref mBlipColor1, ref vehStored1);
                    if (!vehToVec_Tick.isRunning)
                        vehToVec_Tick.Start();
                    if (Game.Exists(vehArray[0]))
                    {
                        vehArray[0].isRequiredForMission = false;
                        vehArray[0].Delete();
                    }
                }
                if (vehSave2 == 1 && Game.Exists(vehArray[1]) && vehArray[1].Position.DistanceTo(lp.Character.Position) > 200)
                {
                    bVehVec2 = true;
                    posBlip2 = 2;
                    SetTempVehPos(ref vehArray[1], ref vehTemp2, ref vehModel2, ref vehVec2, ref vehColor2, ref vehBlipArray[1], ref vehHead2, ref vehEngHealth2, ref vehHealth2, ref vehSave2, ref posBlip2, ref vEng2, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2, ref vehLocked2, ref indexVehColor2, ref mBlipColor2, ref vehStored2);
                    if (!vehToVec_Tick.isRunning)
                        vehToVec_Tick.Start();
                    if (Game.Exists(vehArray[1]))
                    {
                        vehArray[1].isRequiredForMission = false;
                        vehArray[1].Delete();
                    }
                }
                if (vehSave3 == 1 && Game.Exists(vehArray[2]) && vehArray[2].Position.DistanceTo(lp.Character.Position) > 200)
                {
                    bVehVec3 = true;
                    posBlip3 = 3;
                    SetTempVehPos(ref vehArray[2], ref vehTemp3, ref vehModel3, ref vehVec3, ref vehColor3, ref vehBlipArray[2], ref vehHead3, ref vehEngHealth3, ref vehHealth3, ref vehSave3, ref posBlip3, ref vEng3, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3, ref vehLocked3, ref indexVehColor3, ref mBlipColor3, ref vehStored3);
                    if (!vehToVec_Tick.isRunning)
                        vehToVec_Tick.Start();
                    if (Game.Exists(vehArray[2]))
                    {
                        vehArray[2].isRequiredForMission = false;
                        vehArray[2].Delete();
                    }
                }
                if (vehSave4 == 1 && Game.Exists(vehArray[3]) && vehArray[3].Position.DistanceTo(lp.Character.Position) > 200)
                {
                    bVehVec4 = true;
                    posBlip4 = 4;
                    SetTempVehPos(ref vehArray[3], ref vehTemp4, ref vehModel4, ref vehVec4, ref vehColor4, ref vehBlipArray[3], ref vehHead4, ref vehEngHealth4, ref vehHealth4, ref vehSave4, ref posBlip4, ref vEng4, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4, ref vehLocked4, ref indexVehColor4, ref mBlipColor4, ref vehStored4);
                    if (!vehToVec_Tick.isRunning)
                        vehToVec_Tick.Start();
                    if (Game.Exists(vehArray[3]))
                    {
                        vehArray[3].isRequiredForMission = false;
                        vehArray[3].Delete();
                    }
                }
            }
            #endregion
            #region AutoSaveEnabled?
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
            #endregion
        }
        #endregion
        #region Methods
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
        bool CheckWepValues(int[] vehWeps)
        {
            for (int i = 0; i < vehWeps.Length; i++)
            {
                if (vehWeps[i] > 0)
                {
                    hasWeapons = true;
                }
            }
            if (hasWeapons == true)
                return true;
            else 
                return false;
        }
        void PrototypeWeaponsAmmo(ref Vehicle vehArrayPos, ref int[] wepArray, ref bool vehClosest) 
        {
            if (Game.Exists(vehArrayPos) && vehClosest == true)
            {
                vehTrunk = vehArrayPos.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                if (lp.Character.Position.DistanceTo(vehTrunk) <= 1.6 && lp.Character.isTouching(vehArrayPos) && (vehArrayPos.Door(VehicleDoor.Trunk).isOpen || Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehArrayPos, 5, true)))
                {
                    CountAmmo(wepArray);
                    if (totalAmmo <= maxAmmo)
                    {
                        GetPlayerWeapons();
                        if (lp.Character.Weapons.Current.isPresent && (lp.Character.Weapons.Current == Weapon.Rifle_M4 || lp.Character.Weapons.Current == Weapon.Rifle_AK47 || lp.Character.Weapons.Current == Weapon.SMG_Uzi || lp.Character.Weapons.Current == Weapon.SMG_MP5 || lp.Character.Weapons.Current == Weapon.Handgun_DesertEagle || lp.Character.Weapons.Current == Weapon.Handgun_Glock || lp.Character.Weapons.Current == Weapon.Shotgun_Baretta || lp.Character.Weapons.Current == Weapon.Shotgun_Basic || lp.Character.Weapons.Current == Weapon.SniperRifle_M40A1 || lp.Character.Weapons.Current == Weapon.SniperRifle_Basic || lp.Character.Weapons.Current == Weapon.Thrown_Grenade || lp.Character.Weapons.Current == Weapon.Thrown_Molotov))
                        {
                            if (lp.Character.Weapons.Current == Weapon.Rifle_M4)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[5] += transferAmt;
                                    r_m4 -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Rifle_AK47)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[4] += transferAmt;
                                    r_ak -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Shotgun_Baretta)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[3] += transferAmt;
                                    baretta -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Shotgun_Basic)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[2] += transferAmt;
                                    pump -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.SMG_MP5)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    if (taserEnabled && lp.Character.Weapons.Current == Weapon.SMG_MP5)
                                    {
                                        wepArray[1] += transferAmt;
                                        mp5 -= transferAmt;
                                        lp.Character.Weapons.Current.Ammo -= transferAmt;
                                        Game.DisplayText(transferAmt.ToString() + " rounds of Taser ammo added", 3000);
                                        Text(transferAmt.ToString() + " rounds of Taser ammo added", 3000);
                                    }
                                    else
                                    {
                                        wepArray[1] += transferAmt;
                                        mp5 -= transferAmt;
                                        lp.Character.Weapons.Current.Ammo -= transferAmt;
                                        Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                        Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    }
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.SMG_Uzi)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[0] += transferAmt;
                                    uzi -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.SniperRifle_Basic)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[6] += transferAmt;
                                    snipeBolt -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.SniperRifle_M40A1)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[7] += transferAmt;
                                    snipeAuto -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Thrown_Molotov)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 1)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[8] += transferAmt;
                                    molotov -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Thrown_Grenade)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 1)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[9] += transferAmt;
                                    grenade -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Handgun_Glock)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[10] += transferAmt;
                                    grenade -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                            else if (lp.Character.Weapons.Current == Weapon.Handgun_DesertEagle)
                            {
                                if (lp.Character.Weapons.Current.Ammo <= 11)
                                    Text("You cannot transfer any more ammo or else you will lose your weapon", 5000);
                                else
                                {
                                    wepArray[11] += transferAmt;
                                    grenade -= transferAmt;
                                    lp.Character.Weapons.Current.Ammo -= transferAmt;
                                    Game.DisplayText(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                    Text(transferAmt.ToString() + " rounds of " + lp.Character.Weapons.Current.Type.ToString() + " ammo added", 3000);
                                }
                            }
                        }
                        else
                            Text("You cannot store a " + lp.Character.Weapons.Current.Type.ToString() + " weapon in the vehicle.", 3000);
                    }
                    else
                        Text("You can only fit a maximum of " + maxAmmo.ToString() + " ammo in this vehicle.", 8000);
                }
            }
        }
        void PrototypeWeapons(ref Vehicle vehArrayPos, ref int[] vehWeps, ref int[] wepArray, ref bool vehClosest)
        {
            if (Game.Exists(vehArrayPos) && vehClosest == true)
            {
                vehTrunk = vehArrayPos.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                if (lp.Character.Position.DistanceTo(vehTrunk) <= 1.6 && lp.Character.isTouching(vehArrayPos) && (vehArrayPos.Door(VehicleDoor.Trunk).isOpen || Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehArrayPos, 5, true)))
                {
                    GetPlayerWeapons();
                    if (lp.Character.Weapons.Current.isPresent && (lp.Character.Weapons.Current == Weapon.Rifle_M4 || lp.Character.Weapons.Current == Weapon.Rifle_AK47 || lp.Character.Weapons.Current == Weapon.SMG_Uzi || lp.Character.Weapons.Current == Weapon.SMG_MP5 || lp.Character.Weapons.Current == Weapon.Handgun_DesertEagle || lp.Character.Weapons.Current == Weapon.Handgun_Glock || lp.Character.Weapons.Current == Weapon.Shotgun_Baretta || lp.Character.Weapons.Current == Weapon.Shotgun_Basic || lp.Character.Weapons.Current == Weapon.SniperRifle_M40A1 || lp.Character.Weapons.Current == Weapon.SniperRifle_Basic || lp.Character.Weapons.Current == Weapon.Thrown_Grenade || lp.Character.Weapons.Current == Weapon.Thrown_Molotov))
                    {
                        CountWeps(vehWeps);
                        CountAmmo(wepArray);
                        if (totalAmmo <= maxAmmo)
                        {
                            if (totalWeps <= maxVehStorage)// 0 = uzi, 1 = mp5, 2 = pump, 3 = baretta, 4 = ak, 5 = m4, 6 = bolt, 7 = auto, 8 = molotov, 9 = grenade;
                            {
                                if (lp.Character.Weapons.Current.Type == Weapon.Rifle_M4)
                                {
                                    vehWeps[5] += 1;
                                    wepArray[5] += r_m4;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Rifle_AK47)
                                {
                                    vehWeps[4] += 1;
                                    wepArray[4] += r_ak;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Shotgun_Baretta)
                                {
                                    vehWeps[3] += 1;
                                    wepArray[3] += baretta;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Shotgun_Basic)
                                {
                                    vehWeps[2] += 1;
                                    wepArray[2] += pump;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.SMG_MP5)
                                {
                                    vehWeps[1] += 1;
                                    wepArray[1] += mp5;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.SMG_Uzi)
                                {
                                    vehWeps[0] += 1;
                                    wepArray[0] += uzi;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.SniperRifle_Basic)
                                {
                                    vehWeps[6] += 1;
                                    wepArray[6] += snipeBolt;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.SniperRifle_M40A1)
                                {
                                    vehWeps[7] += 1;
                                    wepArray[7] += snipeAuto;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Thrown_Molotov)
                                {
                                    vehWeps[8] += 1;
                                    wepArray[8] += molotov;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Thrown_Grenade)
                                {
                                    vehWeps[9] += 1;
                                    wepArray[9] += grenade;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Handgun_Glock)
                                {
                                    vehWeps[10] += 1;
                                    wepArray[10] += glock;
                                }
                                else if (lp.Character.Weapons.Current.Type == Weapon.Handgun_DesertEagle)
                                {
                                    vehWeps[11] += 1;
                                    wepArray[11] += deagle;
                                }
                                if (taserEnabled && lp.Character.Weapons.Current.Type == Weapon.SMG_MP5)
                                {
                                    Text("Taser added to vehicle " + lp.Character.Weapons.Current.Ammo.ToString() + "  rounds of ammo.", 5000);
                                }
                                else
                                {
                                    if (lp.Character.Weapons.Current == Weapon.Thrown_Grenade || lp.Character.Weapons.Current == Weapon.Thrown_Molotov)
                                        Text(lp.Character.Weapons.Current.Type.ToString() + " belt added to vehicle with " + lp.Character.Weapons.Current.Ammo.ToString() + " of " + lp.Character.Weapons.Current.Type.ToString() + "(s) rounds along with it.", 5000);
                                    else
                                    Text(lp.Character.Weapons.Current.Type.ToString() + " added to vehicle with " + lp.Character.Weapons.Current.Ammo.ToString() + " rounds of ammo.", 5000);
                                }
                                lp.Character.Weapons.Current.Remove();
                                CalcWeaponStorage(vehWeps);
                            }
                            else
                                Text("You cannot fit any more weapons in this vehicle, only ammo.  To do this, dont hold LSHIFT.", 8000);
                        }
                        else
                            Text("You can only fit a maximum of " + maxAmmo.ToString() + " rounds of ammo in this vehicle.", 8000);

                    }
                    else
                        Text("You cannot store a " + lp.Character.Weapons.Current.Type.ToString() + " weapon in the vehicle.", 3000);
                }
            }
        }
        void PopTrunk()
        {
            if (lp.Character.isInVehicle())
            {
                if (!lp.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).isOpen || lp.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).isDamaged)
                {
                    lp.Character.Animation.Play(new AnimationSet("amb@car_cell_dsty_ds"), "cell_destroy", 8f, AnimationFlags.Unknown04);
                    Wait(1500);
                    if (Game.Exists(lp.Character.CurrentVehicle))
                        lp.Character.CurrentVehicle.Door(VehicleDoor.Trunk).Open();
                    else
                        if (Game.Exists(lp.LastVehicle))
                            lp.LastVehicle.Door(VehicleDoor.Trunk).Open();
                }
            }
        }
        void PopHood()
        {
            if (lp.Character.isInVehicle())
            {
                if (!lp.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).isOpen || lp.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).isDamaged)
                {
                    lp.Character.Animation.Play(new AnimationSet("amb@car_low_ps_loops"), "alt_sit_ps_a", 8f, AnimationFlags.Unknown04);
                    Wait(1500);
                    if (Game.Exists(lp.Character.CurrentVehicle))
                        lp.Character.CurrentVehicle.Door(VehicleDoor.Hood).Open();
                    else
                        if (Game.Exists(lp.LastVehicle))
                            lp.LastVehicle.Door(VehicleDoor.Hood).Open();
                }
                else Text("Your too busy.");
            }

        }
        void LoadMenuFont()
        {
            fontProgTitle = new GTA.Font(16.0F, FontScaling.Pixel);
            fontProgTitle.Color = Color.Green;
            fontSeatBeltOn = new GTA.Font(22.0F, FontScaling.Pixel);
            fontSeatBeltOn.Color = Color.Green;
            fontSeatBeltOff = new GTA.Font(22.0F, FontScaling.Pixel);
            fontSeatBeltOff.Color = Color.Red;
                mainMenuFont = new GTA.Font(32.0F, FontScaling.Pixel);
                mainMenuFont.Color = Color.OldLace;
                subMenuFont = new GTA.Font(22.0F, FontScaling.Pixel);
                subMenuFont.Color = Color.WhiteSmoke;
                selectionFont = new GTA.Font(22.0F, FontScaling.Pixel);
                selectionFont.Color = Color.DarkRed;
        }
        void RepairVehicle()
        {
            if (lp.Character.Animation.isPlaying(new AnimationSet("misstaxidepot"), "workunderbonnet"))
            {
                lp.Character.Task.ClearAllImmediately();
                Game.DisplayText("Repair Canceled");
                Text("Repair Canceled");
                progCount = 0;
                if (progTimer_Tick.isRunning)
                    progTimer_Tick.Stop();
                return;
            }
            else if (lp.Character.Animation.isPlaying(new AnimationSet("misstaxidepot"), "fixcar"))
            {
                lp.Character.Task.ClearAllImmediately();
                Game.DisplayText("Repair Canceled");
                Text("Repair Canceled");
                progCount = 0;
                if (progTimer_Tick.isRunning)
                    progTimer_Tick.Stop();
                return;
            }
            closestVeh = World.GetClosestVehicle(lp.Character.Position, 5.5f);
            if (Game.Exists(closestVeh))
            {
                vehHood = closestVeh.GetOffsetPosition(new Vector3(0.0f, +2.8f, 0.0f));
            }
            if (Game.Exists(closestVeh) && lp.Character.isTouching(closestVeh) && closestVeh.EngineHealth < 1000 && !lp.Character.isInVehicle() && closestVeh.isAlive && lp.Character.Position.DistanceTo(vehHood) <= 1.6 && !closestVeh.isOnFire && (Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 4, true) || closestVeh.Door(VehicleDoor.Hood).isOpen))
            {
                if (closestVeh.EngineHealth < 200)
                {
                    repairingEngine = true;
                    progBar = R(14000, 18000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinEngCostHigh, tMaxEngCostHigh);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing highly damaged engine... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + (tVehicleDamageCost) + " to repair the high damaged engine!  You only have $" + lp.Money + " dollars", 5000);
                }
                else if (closestVeh.EngineHealth < 500)
                {
                    repairingEngine = true;
                    progBar = R(8000, 16000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinEngCostMed, tMaxEngCostMed);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing medium damaged engine... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + tVehicleDamageCost + " to repair the medium damaged engine!  You only have $" + lp.Money + " dollars", 5000);
                }
                else if (closestVeh.EngineHealth > 500)
                {
                    repairingEngine = true;
                    progBar = R(4000, 8000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinEngCostLow, tMaxEngCostLow);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing lightly damaged engine... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + (tVehicleDamageCost) + " to repair the low damaged engine!  You only have $" + lp.Money + " dollars", 5000);
                }
            }
            else if (Game.Exists(closestVeh) && !lp.Character.isInVehicle() && closestVeh.isAlive && !closestVeh.isOnFire && (closestVeh.IsTireBurst(VehicleWheel.CenterLeft) || closestVeh.IsTireBurst(VehicleWheel.CenterRight) || closestVeh.IsTireBurst(VehicleWheel.FrontLeft) || closestVeh.IsTireBurst(VehicleWheel.FrontRight) || closestVeh.IsTireBurst(VehicleWheel.RearLeft) || closestVeh.IsTireBurst(VehicleWheel.RearRight)))
            {
                vehSideR = closestVeh.GetOffsetPosition(new Vector3(1.0f, 0.0f, 0.0f));
                vehSideL = closestVeh.GetOffsetPosition(new Vector3(-1.0f, 0.0f, 0.0f));
                if (lp.Character.isTouching(closestVeh) && (lp.Character.Position.DistanceTo(vehSideR) <= 1.2 || lp.Character.Position.DistanceTo(vehSideL) <= 1.2))
                {
                    if (closestVeh.IsTireBurst(VehicleWheel.CenterLeft))
                        tireCounter++;
                    if (closestVeh.IsTireBurst(VehicleWheel.CenterRight))
                        tireCounter++;
                    if (closestVeh.IsTireBurst(VehicleWheel.FrontLeft))
                        tireCounter++;
                    if (closestVeh.IsTireBurst(VehicleWheel.FrontRight))
                        tireCounter++;
                    if (closestVeh.IsTireBurst(VehicleWheel.RearLeft))
                        tireCounter++;
                    if (closestVeh.IsTireBurst(VehicleWheel.RearRight))
                        tireCounter++;

                    if (tireCounter > 2)
                    {
                        repairingTires = true;
                        progBar = R(14000, 18000);
                        RoundToNearest(progBar, 1000);
                        tVehicleDamageCost = R((tMinTireCost * tireCounter), (tMaxTireCost * tireCounter));
                        if (lp.Money >= tVehicleDamageCost)
                        {
                            Text("You have 3 or more tires popped... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                            lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "fixcar", 8f, AnimationFlags.Unknown05);
                            progCd = progBar;
                            if (!progTimer_Tick.isRunning)
                                progTimer_Tick.Start();
                        }
                        else
                        {                         
                            Text("You need $" + (tVehicleDamageCost) + " to repair the " + tireCounter + " tires!  You only have $" + lp.Money + " dollars", 5000);
                            tireCounter = 0;
                        }
                    }
                    else if (tireCounter > 1 && tireCounter < 3)
                    {
                        repairingTires = true;
                        progBar = R(8000, 16000);
                        RoundToNearest(progBar, 1000);
                        tVehicleDamageCost = R((tMinTireCost * tireCounter), (tMaxTireCost * tireCounter));
                        if (lp.Money >= tVehicleDamageCost)
                        {
                            Text("You have 2 tires popped... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                            lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "fixcar", 8f, AnimationFlags.Unknown05);
                            progCd = progBar;
                            if (!progTimer_Tick.isRunning)
                                progTimer_Tick.Start();
                        }
                        else
                        {
                            Text("You need $" + (tVehicleDamageCost) + " to repair the " + tireCounter + " tires!  You only have $" + lp.Money + " dollars", 5000);
                            tireCounter = 0;
                        }
                    }
                    else if (tireCounter > 0 && tireCounter < 2)
                    {
                        repairingTires = true;
                        progBar = R(4000, 6000);
                        RoundToNearest(progBar, 1000);
                        tVehicleDamageCost = R((tMinTireCost * tireCounter), (tMaxTireCost * tireCounter));
                        if (lp.Money >= tVehicleDamageCost)
                        {
                            Text("You have 1 tire popped... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                            lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "fixcar", 8f, AnimationFlags.Unknown05);
                            progCd = progBar;
                            if (!progTimer_Tick.isRunning)
                                progTimer_Tick.Start();
                        }
                        else
                        {
                            Text("You need $" + (tVehicleDamageCost) + " to repair the " + tireCounter + " tires!  You only have $" + lp.Money + " dollars", 5000);
                            tireCounter = 0;
                        }
                    }
                }
            }
            else if (Game.Exists(closestVeh) && !lp.Character.isInVehicle() && closestVeh.isAlive && !closestVeh.isOnFire && closestVeh.Health < 1000)
            {
                if (closestVeh.Health < 200)
                {
                    repairingBody = true;
                    progBar = R(14000, 18000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinBodyCostHigh, tMaxBodyCostHigh);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing highly damaged body... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + (tVehicleDamageCost) + " to repair the highly damaged body!  You only have $" + lp.Money + " dollars", 5000);
                }
                else if (closestVeh.Health < 500)
                {
                    repairingBody = true;
                    progBar = R(8000, 16000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinBodyCostMed, tMaxBodyCostMed);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing medium damaged body... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + (tVehicleDamageCost) + " to repair the medium damaged body!  You only have $" + lp.Money + " dollars", 5000);
               
                }
                else if (closestVeh.Health > 500)
                {
                    repairingBody = true;
                    progBar = R(4000, 8000);
                    RoundToNearest(progBar, 1000);
                    tVehicleDamageCost = R(tMinBodyCostLow, tMaxBodyCostLow);
                    if (lp.Money >= tVehicleDamageCost)
                    {
                        Text("Repairing lightly damaged body... This will take " + (progBar / 1000).ToString() + " seconds.", progBar);
                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "workunderbonnet", 8f, AnimationFlags.Unknown05);
                        progCd = progBar;
                        if (!progTimer_Tick.isRunning)
                            progTimer_Tick.Start();
                    }
                    else
                        Text("You need $" + (tVehicleDamageCost) + " to repair the low damaged body!  You only have $" + lp.Money + " dollars", 5000);
               
                }
            }
            else
            {
                if (Game.Exists(closestVeh) && lp.Character.isTouching(closestVeh) && closestVeh.EngineHealth < 1000 && !lp.Character.isInVehicle() && closestVeh.isAlive && lp.Character.Position.DistanceTo(vehHood) <= 1.6 && (!Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 4, true) || !closestVeh.Door(VehicleDoor.Hood).isOpen))
                {
                    Text("To repair Engine, make sure the hood is opened and the engine is damaged/not on fire.", 5000);
                }
                else
                    Text("To repair tire or body, tire must be bursted/body damaged and car is not on fire, be at center side of car.", 6000);

            }
        }
        void SeatBeltToggle()
        {
            if (lp.Character.isInVehicle())
            {
                if (seatBeltOn == false)
                {
                    Wait(1000);
                    seatBeltOn = true;
                    lp.Character.WillFlyThroughWindscreen = false;
                }
                else
                {
                    Wait(1000);
                    seatBeltOn = false;
                    lp.Character.WillFlyThroughWindscreen = true;
                }
            }
        }
        void SaveCurrentVeh()
        {
            if (lp.Character.isInVehicle())
            {
                currentVeh = lp.Character.CurrentVehicle;
            }
            else
            {
                Game.DisplayText("You must be in a vehicle to save one!");
                return;
            }
            if (vehSave1 == 1)
            {
                if (vehCount >= vehAmt)
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            else
            {
                if (currentVeh.isRequiredForMission == true)
                {
                    Game.DisplayText("This vehicle has already beed saved!");
                    return;
                }
                vehCount++;
                if (vehCount <= vehAmt)
                {
                    vehArray[0] = currentVeh;
                    vehArray[0].isRequiredForMission = true;
                    posBlip = 0;
                    vehBlipArray[0] = vehArray[0].AttachBlip();
                    vehBlipArray[0].Display = BlipDisplay.ArrowAndMap;
                    vehBlipArray[0].Icon = BlipIcon.Building_Garage;
                    vehBlipArray[0].Color = BlipColor.Green;
                    vehBlipArray[0].Scale = 1.0f;
                    vehBlipArray[0].ShowOnlyWhenNear = true;
                    vehBlipArray[0].Name = "Saved Vehicle " + (posBlip += 1).ToString();
                    vehSave1 = 1;
                    vehTemp1 = vehArray[0];
                    // Empty Ammo for fresh car
                    for (int i = 0; i < vehWeps1.Length; i++)
                    {
                        vehWeps1[i] = 0;
                    }
                    for (int i = 0; i < vA1.Length; i++)
                    {
                        vA1[i] = 0;
                    }
                    Game.DisplayText("Vehicle Saved", 3000);
                    return;
                }
                else
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            if (vehSave2 == 1)
            {
                if (vehCount >= vehAmt)
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            else
            {
                if (currentVeh.isRequiredForMission == true)
                {
                    Game.DisplayText("This vehicle has already beed saved!");
                    return;
                }
                vehCount++;
                if (vehCount <= vehAmt)
                {
                    vehArray[1] = currentVeh;
                    vehArray[1].isRequiredForMission = true;
                    posBlip = 1;
                    vehBlipArray[1] = vehArray[1].AttachBlip();
                    vehBlipArray[1].Display = BlipDisplay.ArrowAndMap;
                    vehBlipArray[1].Icon = BlipIcon.Building_Garage;
                    vehBlipArray[1].Color = BlipColor.Yellow;
                    vehBlipArray[1].Scale = 1.0f;
                    vehBlipArray[1].ShowOnlyWhenNear = true;
                    vehBlipArray[1].Name = "Saved Vehicle " + (posBlip += 1).ToString();
                    vehSave2 = 1;
                    vehTemp2 = vehArray[1];
                    // Empty Ammo for fresh car
                    for (int i = 0; i < vehWeps2.Length; i++)
                    {
                        vehWeps1[i] = 0;
                    }
                    for (int i = 0; i < vA2.Length; i++)
                    {
                        vA2[i] = 0;
                    }
                    Game.DisplayText("Vehicle Saved", 3000);
                    return;
                }
                else
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            if (vehSave3 == 1)
            {
                if (vehCount >= vehAmt)
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            else
            {
                if (currentVeh.isRequiredForMission == true)
                {
                    Game.DisplayText("This vehicle has already beed saved or belongs to someone else", 5000);
                    return;
                }
                vehCount++;
                if (vehCount <= vehAmt)
                {
                    vehArray[2] = currentVeh;
                    vehArray[2].isRequiredForMission = true;
                    posBlip = 2;
                    vehBlipArray[2] = vehArray[2].AttachBlip();
                    vehBlipArray[2].Display = BlipDisplay.ArrowAndMap;
                    vehBlipArray[2].Icon = BlipIcon.Building_Garage;
                    vehBlipArray[2].Color = BlipColor.Purple;
                    vehBlipArray[2].Scale = 1.0f;
                    vehBlipArray[2].ShowOnlyWhenNear = true;
                    vehBlipArray[2].Name = "Saved Vehicle " + (posBlip += 1).ToString();
                    vehSave3 = 1;
                    vehTemp3 = vehArray[2];
                    // Empty Ammo for fresh car
                    for (int i = 0; i < vehWeps3.Length; i++)
                    {
                        vehWeps3[i] = 0;
                    }
                    for (int i = 0; i < vA3.Length; i++)
                    {
                        vA3[i] = 0;
                    }
                    Game.DisplayText("Vehicle Saved", 3000);
                    return;
                }
                else
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            if (vehSave4 == 1)
            {
                if (vehCount >= vehAmt)
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            else
            {
                if (currentVeh.isRequiredForMission == true)
                {
                    Game.DisplayText("This vehicle has already beed saved!");
                    return;
                }
                vehCount++;
                if (vehCount <= vehAmt)
                {
                    vehArray[3] = currentVeh;
                    vehArray[3].isRequiredForMission = true;
                    posBlip = 3;
                    vehBlipArray[3] = vehArray[3].AttachBlip();
                    vehBlipArray[3].Display = BlipDisplay.ArrowAndMap;
                    vehBlipArray[3].Icon = BlipIcon.Building_Garage;
                    vehBlipArray[3].Color = BlipColor.Orange;
                    vehBlipArray[3].Scale = 1.0f;
                    vehBlipArray[3].ShowOnlyWhenNear = true;
                    vehBlipArray[3].Name = "Saved Vehicle " + (posBlip += 1).ToString();
                    vehSave4 = 1;
                    vehTemp4 = vehArray[3];
                    // Empty Ammo for fresh car
                    for (int i = 0; i < vehWeps4.Length; i++)
                    {
                        vehWeps4[i] = 0;
                    }
                    for (int i = 0; i < vA4.Length; i++)
                    {
                        vA4[i] = 0;
                    }
                    Game.DisplayText("Vehicle Saved", 3000);
                    return;
                }
                else
                {
                    Game.DisplayText("Max Vehicles Reached.  Your allowed a maximum of " + vehAmt.ToString() + ".", 5000);
                }
            }
            Wait(1000);
        }
        void UnSaveVehicle()
        {
            if (menuSelectVeh == true)
            {
                switch (selectionVeh)
                {
                    case 1:
                        if (vehSave1 == 1)
                        {
                            vehSave1 = 0;
                            if (Game.Exists(vehArray[0]))
                            vehArray[0].isRequiredForMission = false;
                            if (Game.Exists(vehBlipArray[0]))
                            vehBlipArray[0].Delete();
                            vehCount--;
                            vehTemp1 = null;
                            Text("Vehicle 1 deleted", 3000);
                        }
                        break;
                    case 2:
                        if (vehSave2 == 1)
                        {
                            vehSave2 = 0;
                            if (Game.Exists(vehArray[1]))
                                vehArray[1].isRequiredForMission = false;
                            if (Game.Exists(vehBlipArray[1]))
                                vehBlipArray[1].Delete();
                            vehCount--;
                            vehTemp2 = null;
                            Text("Vehicle 2 deleted", 3000);
                        }
                        break;
                    case 3:
                        if (vehSave3 == 1)
                        {
                            vehSave3 = 0;
                            if (Game.Exists(vehArray[2]))
                                vehArray[2].isRequiredForMission = false;
                            if (Game.Exists(vehBlipArray[2]))
                                vehBlipArray[2].Delete();
                            vehCount--;
                            vehTemp3 = null;
                            Text("Vehicle 3 deleted", 3000);
                        }
                        break;
                    case 4:
                        if (vehSave4 == 1)
                        {
                            vehSave4 = 0;
                            if (Game.Exists(vehArray[3]))
                                vehArray[3].isRequiredForMission = false;
                            if (Game.Exists(vehBlipArray[3]))
                                vehBlipArray[3].Delete();
                            vehCount--;
                            vehTemp4 = null;
                            Text("Vehicle 4 deleted", 3000);
                        }
                        break;
                }
            }
            Wait(1000);
        }
        void VehicleStatusMenu()
        {
            if (!menuSelectVeh && (!menuVeh1 && !menuVeh2 && !menuVeh3 && !menuVeh4))
            {
                menuSelectVeh = true;
                selectionVeh = 1;
            }
        }
        void GetPlayerWeapons()
        {
            #region Get Players Current Weapons
            if (lp.Character.Weapons.Glock.Ammo > 0)
            {
                glock = lp.Character.Weapons.Glock.Ammo;
            }
            if (lp.Character.Weapons.DesertEagle.Ammo > 0)
            {
                deagle = lp.Character.Weapons.DesertEagle.Ammo;
            }
            if (lp.Character.Weapons.AssaultRifle_M4.Ammo > 0)
            {
                r_m4 = lp.Character.Weapons.AssaultRifle_M4.Ammo;
            }
            if (lp.Character.Weapons.AssaultRifle_AK47.Ammo > 0)
            {
                r_ak = lp.Character.Weapons.AssaultRifle_AK47.Ammo;
            }
            if (lp.Character.Weapons.Uzi.Ammo > 0)
            {
                uzi = lp.Character.Weapons.Uzi.Ammo;
            }
            if (lp.Character.Weapons.MP5.Ammo > 0)
            {
                mp5 = lp.Character.Weapons.MP5.Ammo;
            }
            if (lp.Character.Weapons.SniperRifle_M40A1.Ammo > 0)
            {
                snipeAuto = lp.Character.Weapons.SniperRifle_M40A1.Ammo;
            }
            if (lp.Character.Weapons.BasicSniperRifle.Ammo > 0)
            {
                snipeBolt = lp.Character.Weapons.BasicSniperRifle.Ammo;
            }
            if (lp.Character.Weapons.BarettaShotgun.Ammo > 0)
            {
                baretta = lp.Character.Weapons.BarettaShotgun.Ammo;
            }
            if (lp.Character.Weapons.BasicShotgun.Ammo > 0)
            {
                pump = lp.Character.Weapons.BasicShotgun.Ammo;
            }
            if (lp.Character.Weapons.MolotovCocktails.Ammo > 0)
            {
                molotov = lp.Character.Weapons.MolotovCocktails.Ammo;
            }
            if (lp.Character.Weapons.Grenades.Ammo > 0)
            {
                grenade = lp.Character.Weapons.Grenades.Ammo;
            }
            #endregion
        }
        void LockDoors()
        {
            if (lp.Character.isInVehicle())
            {
                if (vehSave1 == 1 && Game.Exists(vehArray[0]) && vehClosest1 && vehArray[0].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked1 == false)
                    {
                        if (!vehArray[0].Door(VehicleDoor.Trunk).isOpen && !vehArray[0].Door(VehicleDoor.LeftFront).isOpen && !vehArray[0].Door(VehicleDoor.RightFront).isOpen && !vehArray[0].Door(VehicleDoor.LeftRear).isOpen && !vehArray[0].Door(VehicleDoor.RightRear).isOpen && !(vehArray[0].Door(VehicleDoor.Trunk).isDamaged || vehArray[0].Door(VehicleDoor.LeftFront).isDamaged || vehArray[0].Door(VehicleDoor.RightFront).isDamaged || vehArray[0].Door(VehicleDoor.LeftRear).isDamaged || vehArray[0].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_a"), "change_radio", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[0].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[0], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[0]), new Parameter(1) });
                            vehLocked1 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[0].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[0].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[0].Door(VehicleDoor.Trunk).isDamaged || vehArray[0].Door(VehicleDoor.LeftFront).isDamaged || vehArray[0].Door(VehicleDoor.RightFront).isDamaged || vehArray[0].Door(VehicleDoor.LeftRear).isDamaged || vehArray[0].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_a"), "change_radio", 8f, AnimationFlags.Unknown04);
                        Wait(500);
                        vehArray[0].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[0], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[0]), new Parameter(0) });
                        vehLocked1 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[0].HazardLightsOn = true;
                        Wait(500);
                        vehArray[0].HazardLightsOn = false;
                    }
                }
                if (vehSave2 == 1 && Game.Exists(vehArray[1]) && vehClosest2 && vehArray[1].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked2 == false)
                    {
                        if (!vehArray[1].Door(VehicleDoor.Trunk).isOpen && !vehArray[1].Door(VehicleDoor.LeftFront).isOpen && !vehArray[1].Door(VehicleDoor.RightFront).isOpen && !vehArray[1].Door(VehicleDoor.LeftRear).isOpen && !vehArray[1].Door(VehicleDoor.RightRear).isOpen && !(vehArray[1].Door(VehicleDoor.Trunk).isDamaged || vehArray[1].Door(VehicleDoor.LeftFront).isDamaged || vehArray[1].Door(VehicleDoor.RightFront).isDamaged || vehArray[1].Door(VehicleDoor.LeftRear).isDamaged || vehArray[1].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_a"), "change_radio", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[1].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[1], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[1]), new Parameter(1) });
                            vehLocked2 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[1].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[1].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[1].Door(VehicleDoor.Trunk).isDamaged || vehArray[1].Door(VehicleDoor.LeftFront).isDamaged || vehArray[1].Door(VehicleDoor.RightFront).isDamaged || vehArray[1].Door(VehicleDoor.LeftRear).isDamaged || vehArray[1].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[1].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[1], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[1]), new Parameter(0) });
                        vehLocked2 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[1].HazardLightsOn = true;
                        Wait(500);
                        vehArray[1].HazardLightsOn = false;
                    }
                }
                if (vehSave3 == 1 && Game.Exists(vehArray[2]) && vehClosest3 && vehArray[2].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked3 == false)
                    {
                        if (!vehArray[2].Door(VehicleDoor.Trunk).isOpen && !vehArray[2].Door(VehicleDoor.LeftFront).isOpen && !vehArray[2].Door(VehicleDoor.RightFront).isOpen && !vehArray[2].Door(VehicleDoor.LeftRear).isOpen && !vehArray[2].Door(VehicleDoor.RightRear).isOpen && !(vehArray[2].Door(VehicleDoor.Trunk).isDamaged || vehArray[2].Door(VehicleDoor.LeftFront).isDamaged || vehArray[2].Door(VehicleDoor.RightFront).isDamaged || vehArray[2].Door(VehicleDoor.LeftRear).isDamaged || vehArray[2].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_a"), "change_radio", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[2].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[2], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[2]), new Parameter(1) });
                            vehLocked3 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[2].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[2].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[2].Door(VehicleDoor.Trunk).isDamaged || vehArray[2].Door(VehicleDoor.LeftFront).isDamaged || vehArray[2].Door(VehicleDoor.RightFront).isDamaged || vehArray[2].Door(VehicleDoor.LeftRear).isDamaged || vehArray[2].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[2].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[2], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[2]), new Parameter(0) });
                        vehLocked3 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[2].HazardLightsOn = true;
                        Wait(500);
                        vehArray[2].HazardLightsOn = false;
                    }
                }
                if (vehSave4 == 1 && Game.Exists(vehArray[3]) && vehClosest4 && vehArray[3].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked4 == false)
                    {
                        if (!vehArray[3].Door(VehicleDoor.Trunk).isOpen && !vehArray[3].Door(VehicleDoor.LeftFront).isOpen && !vehArray[3].Door(VehicleDoor.RightFront).isOpen && !vehArray[3].Door(VehicleDoor.LeftRear).isOpen && !vehArray[3].Door(VehicleDoor.RightRear).isOpen && !(vehArray[3].Door(VehicleDoor.Trunk).isDamaged || vehArray[3].Door(VehicleDoor.LeftFront).isDamaged || vehArray[3].Door(VehicleDoor.RightFront).isDamaged || vehArray[3].Door(VehicleDoor.LeftRear).isDamaged || vehArray[3].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_a"), "change_radio", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[3].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[3], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[3]), new Parameter(1) });
                            vehLocked4 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[3].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[3].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[3].Door(VehicleDoor.Trunk).isDamaged || vehArray[3].Door(VehicleDoor.LeftFront).isDamaged || vehArray[3].Door(VehicleDoor.RightFront).isDamaged || vehArray[3].Door(VehicleDoor.LeftRear).isDamaged || vehArray[3].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[3].DoorLock = DoorLock.None;
                        Function.Call("LOCK_CAR_DOORS", vehArray[3], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[3]), new Parameter(0) });
                        vehLocked4 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[3].HazardLightsOn = true;
                        Wait(500);
                        vehArray[3].HazardLightsOn = false;
                    }
                }
            }
            else
            {
                if (vehSave1 == 1 && Game.Exists(vehArray[0]) && vehClosest1 && vehArray[0].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked1 == false)
                    {
                        if (!vehArray[0].Door(VehicleDoor.Trunk).isOpen && !vehArray[0].Door(VehicleDoor.LeftFront).isOpen && !vehArray[0].Door(VehicleDoor.RightFront).isOpen && !vehArray[0].Door(VehicleDoor.LeftRear).isOpen && !vehArray[0].Door(VehicleDoor.RightRear).isOpen && !(vehArray[0].Door(VehicleDoor.Trunk).isDamaged || vehArray[0].Door(VehicleDoor.LeftFront).isDamaged || vehArray[0].Door(VehicleDoor.RightFront).isDamaged || vehArray[0].Door(VehicleDoor.LeftRear).isDamaged || vehArray[0].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("missvlad1"), "lockandunlock_door", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[0].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[0], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[0]), new Parameter(1) });
                            vehLocked1 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[0].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[0].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[0].Door(VehicleDoor.Trunk).isDamaged || vehArray[0].Door(VehicleDoor.LeftFront).isDamaged || vehArray[0].Door(VehicleDoor.RightFront).isDamaged || vehArray[0].Door(VehicleDoor.LeftRear).isDamaged || vehArray[0].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        lp.Character.Animation.Play(new AnimationSet("missvlad1"), "lockandunlock_door", 8f, AnimationFlags.Unknown04);
                        Wait(500);
                        vehArray[0].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[0], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[0]), new Parameter(0) });
                        vehLocked1 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[0].HazardLightsOn = true;
                        Wait(500);
                        vehArray[0].HazardLightsOn = false;
                    }
                }
                if (vehSave2 == 1 && Game.Exists(vehArray[1]) && vehClosest2 && vehArray[1].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked2 == false)
                    {
                        if (!vehArray[1].Door(VehicleDoor.Trunk).isOpen && !vehArray[1].Door(VehicleDoor.LeftFront).isOpen && !vehArray[1].Door(VehicleDoor.RightFront).isOpen && !vehArray[1].Door(VehicleDoor.LeftRear).isOpen && !vehArray[1].Door(VehicleDoor.RightRear).isOpen && !(vehArray[1].Door(VehicleDoor.Trunk).isDamaged || vehArray[1].Door(VehicleDoor.LeftFront).isDamaged || vehArray[1].Door(VehicleDoor.RightFront).isDamaged || vehArray[1].Door(VehicleDoor.LeftRear).isDamaged || vehArray[1].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("missvlad1"), "lockandunlock_door", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[1].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[1], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[1]), new Parameter(1) });
                            vehLocked2 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[1].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[1].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[1].Door(VehicleDoor.Trunk).isDamaged || vehArray[1].Door(VehicleDoor.LeftFront).isDamaged || vehArray[1].Door(VehicleDoor.RightFront).isDamaged || vehArray[1].Door(VehicleDoor.LeftRear).isDamaged || vehArray[1].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[1].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[1], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[1]), new Parameter(0) });
                        vehLocked2 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[1].HazardLightsOn = true;
                        Wait(500);
                        vehArray[1].HazardLightsOn = false;
                    }
                }

                if (vehSave3 == 1 && Game.Exists(vehArray[2]) && vehClosest3 && vehArray[2].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked3 == false)
                    {
                        if (!vehArray[2].Door(VehicleDoor.Trunk).isOpen && !vehArray[2].Door(VehicleDoor.LeftFront).isOpen && !vehArray[2].Door(VehicleDoor.RightFront).isOpen && !vehArray[2].Door(VehicleDoor.LeftRear).isOpen && !vehArray[2].Door(VehicleDoor.RightRear).isOpen && !(vehArray[2].Door(VehicleDoor.Trunk).isDamaged || vehArray[2].Door(VehicleDoor.LeftFront).isDamaged || vehArray[2].Door(VehicleDoor.RightFront).isDamaged || vehArray[2].Door(VehicleDoor.LeftRear).isDamaged || vehArray[2].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("missvlad1"), "lockandunlock_door", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[2].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[2], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[2]), new Parameter(1) });
                            vehLocked3 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[2].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[2].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[2].Door(VehicleDoor.Trunk).isDamaged || vehArray[2].Door(VehicleDoor.LeftFront).isDamaged || vehArray[2].Door(VehicleDoor.RightFront).isDamaged || vehArray[2].Door(VehicleDoor.LeftRear).isDamaged || vehArray[2].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[2].DoorLock = DoorLock.None;
                        //Function.Call("LOCK_CAR_DOORS", vehArray[2], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[2]), new Parameter(0) });
                        vehLocked3 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[2].HazardLightsOn = true;
                        Wait(500);
                        vehArray[2].HazardLightsOn = false;
                    }
                }
                if (vehSave4 == 1 && Game.Exists(vehArray[3]) && vehClosest4 && vehArray[3].Position.DistanceTo(lp.Character.Position) <= 15)
                {
                    if (vehLocked4 == false)
                    {
                        if (!vehArray[3].Door(VehicleDoor.Trunk).isOpen && !vehArray[3].Door(VehicleDoor.LeftFront).isOpen && !vehArray[3].Door(VehicleDoor.RightFront).isOpen && !vehArray[3].Door(VehicleDoor.LeftRear).isOpen && !vehArray[3].Door(VehicleDoor.RightRear).isOpen && !(vehArray[3].Door(VehicleDoor.Trunk).isDamaged || vehArray[3].Door(VehicleDoor.LeftFront).isDamaged || vehArray[3].Door(VehicleDoor.RightFront).isDamaged || vehArray[3].Door(VehicleDoor.LeftRear).isDamaged || vehArray[3].Door(VehicleDoor.RightRear).isDamaged))
                        {
                            lp.Character.Animation.Play(new AnimationSet("missvlad1"), "lockandunlock_door", 8f, AnimationFlags.Unknown04);
                            Wait(500);
                            vehArray[3].DoorLock = DoorLock.ImpossibleToOpen;
                            //Function.Call("LOCK_CAR_DOORS", vehArray[3], 2);
                            Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[3]), new Parameter(1) });
                            vehLocked4 = true;
                            Text("Vehicle locked and alarm set");
                            vehArray[3].HazardLightsOn = true;
                            Wait(1500);
                            vehArray[3].HazardLightsOn = false;
                        }
                        else Text("Make sure all vehicle doors are closed including the trunk in order to lock vehicle", 5000);
                        if (vehArray[3].Door(VehicleDoor.Trunk).isDamaged || vehArray[3].Door(VehicleDoor.LeftFront).isDamaged || vehArray[3].Door(VehicleDoor.RightFront).isDamaged || vehArray[3].Door(VehicleDoor.LeftRear).isDamaged || vehArray[3].Door(VehicleDoor.RightRear).isDamaged)
                        {
                            Text("One or more doors is damaged, you cannot lock the doors or set alarm", 5000);
                            return;
                        }
                    }
                    else
                    {
                        vehArray[3].DoorLock = DoorLock.None;
                        Function.Call("LOCK_CAR_DOORS", vehArray[3], 1);
                        Function.Call("SET_VEH_ALARM", new Parameter[] { new Parameter(vehArray[3]), new Parameter(0) });
                        vehLocked4 = false;
                        Text("Vehicle unlocked and alarm off");
                        vehArray[3].HazardLightsOn = true;
                        Wait(500);
                        vehArray[3].HazardLightsOn = false;
                    }
                }
            }
        }
        void PlaceAmmoVeh()
        {
            if (Game.Exists(vehArray[0]) && vehClosest1 && !lp.Character.isInVehicle())
                if (lookingInTrunk1)
                    PrototypeWeaponsAmmo(ref vehArray[0], ref vA1, ref vehClosest1);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[1]) && vehClosest2 && !lp.Character.isInVehicle())
                if (lookingInTrunk2)
                    PrototypeWeaponsAmmo(ref vehArray[1], ref vA2, ref vehClosest2);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[2]) && vehClosest3 && !lp.Character.isInVehicle())
                if (lookingInTrunk3)
                    PrototypeWeaponsAmmo(ref vehArray[2], ref vA3, ref vehClosest3);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[3]) && vehClosest4 && !lp.Character.isInVehicle())
                if (lookingInTrunk4)
                    PrototypeWeaponsAmmo(ref vehArray[3], ref vA4, ref vehClosest4);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
        }
        void PlaceWepVeh()
        {
            if (Game.Exists(vehArray[0]) && vehClosest1 && !lp.Character.isInVehicle())
                if (lookingInTrunk1)
                    PrototypeWeapons(ref vehArray[0], ref vehWeps1, ref vA1, ref vehClosest1);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[1]) && vehClosest2 && !lp.Character.isInVehicle())
                if (lookingInTrunk2)
                    PrototypeWeapons(ref vehArray[1], ref vehWeps2, ref vA2, ref vehClosest2);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[2]) && vehClosest3 && !lp.Character.isInVehicle())
                if (lookingInTrunk3)
                    PrototypeWeapons(ref vehArray[2], ref vehWeps3, ref vA3, ref vehClosest3);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
            else if (Game.Exists(vehArray[3]) && vehClosest4 && !lp.Character.isInVehicle())
                if(lookingInTrunk4)
                    PrototypeWeapons(ref vehArray[3], ref vehWeps4, ref vA4, ref vehClosest4);
                else Text("Your not close enough to trunk, make sure your touching the vehicle and your at the center of the trunk to access it", 6000);
        }
        void CalcWeaponStorage(int[] vehWeps)
        {
            CountWeps(vehWeps);
            if (totalWeps <= maxVehStorage)
            {
                if (vehWeps[0] >= 0)
                    uziGuns = vehWeps[0];
                if (vehWeps[1] >= 0)
                    mp5Guns = vehWeps[1];
                if (vehWeps[2] >= 0)
                    pumpGuns = vehWeps[2];
                if (vehWeps[3] >= 0)
                    barettaGuns = vehWeps[3];
                if (vehWeps[4] >= 0)
                    akGuns = vehWeps[4];
                if (vehWeps[5] >= 0)
                    m4Guns = vehWeps[5];
                if (vehWeps[6] >= 0)
                    boltGuns = vehWeps[6];
                if (vehWeps[7] >= 0)
                    autoGuns = vehWeps[7];
                if (vehWeps[8] >= 0)
                    molGuns = vehWeps[8];
                if (vehWeps[9] >= 0)
                    grenGuns = vehWeps[9];
                if (vehWeps[10] >= 0)
                    glockGuns = vehWeps[10];
                if (vehWeps[11] >= 0)
                    deagleGuns = vehWeps[11];
            }
        }
        void ClearWeaponStorage()
        {
            akGuns = 0;
            m4Guns = 0;
            glockGuns = 0;
            deagleGuns = 0;
            mp5Guns = 0;
            uziGuns = 0;
            autoGuns = 0;
            boltGuns = 0;
            barettaGuns = 0;
            pumpGuns = 0;
            molGuns = 0;
            grenGuns = 0;

        }
        bool TrunkBool(Vehicle vehArrayPos)
        {
            if (Game.Exists(vehArrayPos))
                vehTrunk = vehArrayPos.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                if (lp.Character.Position.DistanceTo(vehTrunk) <= 1.6 && lp.Character.isTouching(vehArrayPos) && (vehArrayPos.Door(VehicleDoor.Trunk).isOpen || Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehArrayPos, 5, true)))
                {
                    return true;
                }
                else return false;
        }
        void AssignAmmoArrays()
        {
            /*for (int i = 0; i < vA1.Length; i++)
            {
                vA1[i] = 0;
            }
            */
            foreach (int i in vehWeps1)
            {
                vehWeps1[i] = 0;
            }
            foreach (int i in vehWeps2)
            {
                vehWeps2[i] = 0;
            }
            foreach (int i in vehWeps3)
            {
                vehWeps3[i] = 0;
            }
            foreach (int i in vehWeps4)
            {
                vehWeps4[i] = 0;
            }
            foreach (int i in vA1)
            {
                vA1[i] = 0;
            }
            foreach (int i in vA2)
            {
                vA2[i] = 0;
            }
            foreach (int i in vA3)
            {
                vA3[i] = 0;
            }
            foreach (int i in vA4)
            {
                vA4[i] = 0;
            }
            /*vA1[0] = 0;
            vA1[1] = 0;
            vA1[2] = 0;
            vA1[3] = 0;
            vA1[4] = 0;
            vA1[5] = 0;
            vA1[6] = 0;
            vA1[7] = 0;
            vA1[8] = 0;
            vA1[9] = 0;
            vA1[10] = 0;
            vA1[11] = 0;
            */
        }
        void CountWeps(int[] vehWeps)
        {
            totalWeps = vehWeps[0] + vehWeps[1] + vehWeps[2] + vehWeps[3] + vehWeps[4] + vehWeps[5] + vehWeps[6] + vehWeps[7] + vehWeps[8] + vehWeps[9] + vehWeps[10] + vehWeps[11];
        }
        void CountAmmo(int[] vehAmmo)
        {
            totalAmmo = vehAmmo[0] + vehAmmo[1] + vehAmmo[2] + vehAmmo[3] + vehAmmo[4] + vehAmmo[5] + vehAmmo[6] + vehAmmo[7] + vehAmmo[8] + vehAmmo[9] + vehAmmo[10] + vehAmmo[11];
        }
        void GrabGun(int[] vehWeps, int[] wepArray)
        {
            if (selectionVeh == 1 || selectionVeh == 2)
            {
                if (lp.Character.Weapons.AnyHandgun.Ammo < lp.Character.Weapons.AnyHandgun.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnyHandgun.MaxAmmo - lp.Character.Weapons.AnyHandgun.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 1 && lp.Character.Weapons.Glock.Ammo >= 0) // grab ammo only
                        {
                            if (wepArray[10] < transferAmt && wepArray[10] > 0 && lp.Character.Weapons.Glock.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[10];
                                wepArray[10] -= wepArray[10];
                                lp.Character.Weapons.Glock.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Glock.", 5000);
                            }
                            else if (wepArray[10] >= transferAmt && lp.Character.Weapons.Glock.Ammo >= 0)
                            {
                                wepArray[10] -= transferAmt;
                                lp.Character.Weapons.Glock.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Glock.", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 2 && lp.Character.Weapons.DesertEagle.Ammo >= 0)
                        {
                            if (wepArray[11] < transferAmt && wepArray[11] > 0 && lp.Character.Weapons.DesertEagle.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[11];
                                wepArray[11] -= wepArray[11];
                                lp.Character.Weapons.Glock.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Desert Eagle.", 5000);
                            }
                            else if (wepArray[11] >= transferAmt && lp.Character.Weapons.DesertEagle.Ammo >= 0)
                            {
                                wepArray[11] -= transferAmt;
                                lp.Character.Weapons.DesertEagle.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Desert Eagle", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on ammo for this weapon", 5000);

                if (lp.Character.Weapons.Glock.Ammo <= 0 && lp.Character.Weapons.DesertEagle.Ammo <= 0) // grab gun with ammo
                {
                    if (selectionVeh == 1)
                    {
                        if (wepArray [10] < transferAmt && wepArray[10] > 0 && vehWeps[10] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[10];
                            wepArray[10] -= wepArray[10];
                            vehWeps[10] -= 1;
                            lp.Character.Weapons.Glock.Ammo = tempTransfer;
                            Text("You grab a " + "Glock" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[10] >= transferAmt && vehWeps[10] > 0)
                        {
                            vehWeps[10] -= 1;
                            wepArray[10] -= transferAmt;
                            lp.Character.Weapons.Glock.Ammo = transferAmt;
                            Text("You grab a " + "Glock" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[10] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[10] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 2)
                    {

                        if (wepArray[11] < transferAmt && wepArray[11] > 0 && vehWeps[11] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[11];
                            wepArray[11] -= wepArray[11];
                            vehWeps[11] -= 1;
                            lp.Character.Weapons.DesertEagle.Ammo = tempTransfer;
                            Text("You grab a " + "Desert Eagle" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[11] >= transferAmt && vehWeps[11] > 0)
                        {
                            vehWeps[11] -= 1;
                            wepArray[11] -= transferAmt;
                            lp.Character.Weapons.DesertEagle.Ammo = transferAmt;
                            Text("You grab a " + "Desert Eagle" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[11] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[11] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
            if (selectionVeh == 3 || selectionVeh == 4)
            {
                if (lp.Character.Weapons.AnyAssaultRifle.Ammo < lp.Character.Weapons.AnyAssaultRifle.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnyAssaultRifle.MaxAmmo - lp.Character.Weapons.AnyAssaultRifle.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 3)
                        {
                            if (wepArray[5] < transferAmt && wepArray[5] > 0 && lp.Character.Weapons.AssaultRifle_M4.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[5];
                                wepArray[5] -= wepArray[5];
                                lp.Character.Weapons.AssaultRifle_M4.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your M4.", 5000);
                            }
                            else if (wepArray[5] >= transferAmt && lp.Character.Weapons.AssaultRifle_M4.Ammo >= 0)
                            {
                                wepArray[5] -= transferAmt;
                                lp.Character.Weapons.AssaultRifle_M4.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your M4.", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 4)
                        {
                            if (wepArray[4] < transferAmt && wepArray[4] > 0 && lp.Character.Weapons.AssaultRifle_AK47.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[4];
                                wepArray[4] -= wepArray[4];
                                lp.Character.Weapons.AssaultRifle_M4.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your AK47.", 5000);
                            }
                            else if (wepArray[4] >= transferAmt && lp.Character.Weapons.AssaultRifle_AK47.Ammo >= 0)
                            {
                                wepArray[4] -= transferAmt;
                                lp.Character.Weapons.AssaultRifle_AK47.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your AK47", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on ammo for this weapon", 5000);

                if (lp.Character.Weapons.AssaultRifle_M4.Ammo <= 0 && lp.Character.Weapons.AssaultRifle_AK47.Ammo <= 0)
                {
                    if (selectionVeh == 3)
                    {
                        if (wepArray[5] < transferAmt && wepArray[5] > 0 && vehWeps[5] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[5];
                            wepArray[5] -= wepArray[5];
                            vehWeps[5] -= 1;
                            lp.Character.Weapons.AssaultRifle_M4.Ammo = tempTransfer;
                            Text("You grab a " + "M4" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[5] >= transferAmt && vehWeps[5] > 0)
                        {
                            vehWeps[5] -= 1;
                            wepArray[5] -= transferAmt;
                            lp.Character.Weapons.AssaultRifle_M4.Ammo = transferAmt;
                            Text("You grab an " + "M4" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[5] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[5] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 4)
                    {
                        if (wepArray[4] < transferAmt && wepArray[4] > 0 && vehWeps[4] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[4];
                            wepArray[4] -= wepArray[4];
                            vehWeps[4] -= 1;
                            lp.Character.Weapons.AssaultRifle_AK47.Ammo = tempTransfer;
                            Text("You grab a " + "AK47" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[4] >= transferAmt && vehWeps[4] > 0)
                        {
                            vehWeps[4] -= 1;
                            wepArray[4] -= transferAmt;
                            lp.Character.Weapons.AssaultRifle_AK47.Ammo = transferAmt;
                            Text("You grab an " + "AK47" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[4] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[4] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
            if (selectionVeh == 5 || selectionVeh == 6)
            {
                if (lp.Character.Weapons.AnySMG.Ammo < lp.Character.Weapons.AnySMG.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnySMG.MaxAmmo - lp.Character.Weapons.AnySMG.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 5)
                        {
                            if (wepArray[0] < transferAmt && wepArray[0] > 0 && lp.Character.Weapons.Uzi.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[0];
                                wepArray[0] -= wepArray[0];
                                lp.Character.Weapons.Uzi.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Uzi.", 5000);
                            }
                            else if (wepArray[0] >= transferAmt && lp.Character.Weapons.Uzi.Ammo >= 0)
                            {
                                wepArray[0] -= transferAmt;
                                lp.Character.Weapons.Uzi.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Uzi.", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 6)
                        {
                            if (wepArray[1] < transferAmt && wepArray[1] > 0 && lp.Character.Weapons.MP5.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[1];
                                wepArray[1] -= wepArray[1];
                                lp.Character.Weapons.MP5.Ammo += tempTransfer;
                                if (taserEnabled)
                                    Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Taser.", 5000);
                                else
                                    Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Mp5.", 5000);

                            }
                            else if (wepArray[1] >= transferAmt && lp.Character.Weapons.MP5.Ammo >= 0)
                            {
                                wepArray[1] -= transferAmt;
                                lp.Character.Weapons.MP5.Ammo += transferAmt;
                                if (taserEnabled)
                                    Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Taser", 5000);
                                else
                                    Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Mp5", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on ammo for this weapon", 5000);

                if (lp.Character.Weapons.Uzi.Ammo <= 0 && lp.Character.Weapons.MP5.Ammo <= 0)
                {
                    if (selectionVeh == 5)
                    {
                        if (wepArray[0] < transferAmt && wepArray[0] > 0 && vehWeps[0] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[0];
                            wepArray[0] -= wepArray[0];
                            vehWeps[0] -= 1;
                            lp.Character.Weapons.Uzi.Ammo = tempTransfer;
                            Text("You grab a " + "Uzi" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[0] >= transferAmt && vehWeps[0] > 0)
                        {
                            vehWeps[0] -= 1;
                            wepArray[0] -= transferAmt;
                            lp.Character.Weapons.Uzi.Ammo = transferAmt;
                            Text("You grab an " + "Uzi" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[0] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[0] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 6)
                    {
                        if (wepArray[1] < transferAmt && wepArray[1] > 0 && vehWeps[1] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[1];
                            wepArray[1] -= wepArray[1];
                            vehWeps[1] -= 1;
                            lp.Character.Weapons.MP5.Ammo = tempTransfer;
                            if (taserEnabled)
                                Text("You grab a " + "Taser" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                            else
                                Text("You grab a " + "Mp5" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                            
                        }
                        else if (wepArray[1] >= transferAmt && vehWeps[1] > 0)
                        {
                            vehWeps[1] -= 1;
                            wepArray[1] -= transferAmt;
                            lp.Character.Weapons.MP5.Ammo = transferAmt;
                            if (taserEnabled)
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Taser", 5000);
                            else
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Mp5", 5000);
                        }
                        else
                        {
                            if (vehWeps[1] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[1] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
            if (selectionVeh == 7 || selectionVeh == 8)
            {
                if (lp.Character.Weapons.AnyShotgun.Ammo < lp.Character.Weapons.AnyShotgun.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnyShotgun.MaxAmmo - lp.Character.Weapons.AnyShotgun.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 7)
                        {
                            if (wepArray[2] < transferAmt && wepArray[7] > 0 && lp.Character.Weapons.BasicShotgun.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[2];
                                wepArray[2] -= wepArray[2];
                                lp.Character.Weapons.BasicShotgun.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Pump Shotgun.", 5000);
                            }
                            else if (wepArray[2] >= transferAmt && lp.Character.Weapons.BasicShotgun.Ammo >= 0)
                            {
                                wepArray[2] -= transferAmt;
                                lp.Character.Weapons.BasicShotgun.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Pump Shotgun.", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 8)
                        {
                            if (wepArray[3] < transferAmt && wepArray[8] > 0 && lp.Character.Weapons.BarettaShotgun.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[3];
                                wepArray[3] -= wepArray[3];
                                lp.Character.Weapons.BarettaShotgun.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Baretta Shotgun.", 5000);
                            }
                            else if (wepArray[3] >= transferAmt && lp.Character.Weapons.BarettaShotgun.Ammo >= 0)
                            {
                                wepArray[3] -= transferAmt;
                                lp.Character.Weapons.BarettaShotgun.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Baretta Shotgun", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on ammo for this weapon", 5000);

                if (lp.Character.Weapons.BasicShotgun.Ammo <= 0 && lp.Character.Weapons.BarettaShotgun.Ammo <= 0)
                {
                    if (selectionVeh == 7)
                    {
                        if (wepArray[2] < transferAmt && wepArray[2] > 0 && vehWeps[2] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[2];
                            wepArray[2] -= wepArray[2];
                            vehWeps[2] -= 1;
                            lp.Character.Weapons.BasicShotgun.Ammo = tempTransfer;
                            Text("You grab a " + "Pump Shotgun" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[2] >= transferAmt && vehWeps[2] > 0)
                        {
                            vehWeps[2] -= 1;
                            wepArray[2] -= transferAmt;
                            lp.Character.Weapons.BasicShotgun.Ammo = transferAmt;
                            Text("You grab a " + "Pump Shotgun" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[2] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[2] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 8)
                    {
                        if (wepArray[3] < transferAmt && wepArray[3] > 0 && vehWeps[3] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[3];
                            wepArray[3] -= wepArray[3];
                            vehWeps[3] -= 1;
                            lp.Character.Weapons.BarettaShotgun.Ammo = tempTransfer;
                            Text("You grab a " + "Baretta Shotgun" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[3] >= transferAmt && vehWeps[3] > 0)
                        {
                            vehWeps[3] -= 1;
                            wepArray[3] -= transferAmt;
                            lp.Character.Weapons.BarettaShotgun.Ammo = transferAmt;
                            Text("You grab a " + "Baretta Shotgun" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[3] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[3] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
            if (selectionVeh == 9 || selectionVeh == 10)
            {
                if (lp.Character.Weapons.AnySniperRifle.Ammo < lp.Character.Weapons.AnySniperRifle.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnySniperRifle.MaxAmmo - lp.Character.Weapons.AnySniperRifle.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 9)
                        {
                            if (wepArray[7] < transferAmt && wepArray[9] > 0 && lp.Character.Weapons.SniperRifle_M40A1.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[7];
                                wepArray[7] -= wepArray[7];
                                lp.Character.Weapons.SniperRifle_M40A1.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Auto Sniper.", 5000);
                            }
                            else if (wepArray[7] >= transferAmt && lp.Character.Weapons.SniperRifle_M40A1.Ammo >= 0)
                            {
                                wepArray[7] -= transferAmt;
                                lp.Character.Weapons.SniperRifle_M40A1.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Auto Sniper.", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 10)
                        {
                            if (wepArray[6] < transferAmt && wepArray[10] > 0 && lp.Character.Weapons.BasicSniperRifle.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[6];
                                wepArray[6] -= wepArray[6];
                                lp.Character.Weapons.BasicSniperRifle.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Bolt Sniper.", 5000);
                            }
                            else if (wepArray[6] >= transferAmt && lp.Character.Weapons.BasicSniperRifle.Ammo >= 0)
                            {
                                wepArray[6] -= transferAmt;
                                lp.Character.Weapons.BasicSniperRifle.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of ammo for your Bolt Sniper", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on ammo for this weapon", 5000);

                if (lp.Character.Weapons.SniperRifle_M40A1.Ammo <= 0 && lp.Character.Weapons.BasicSniperRifle.Ammo <= 0)
                {
                    if (selectionVeh == 9)
                    {
                        if (wepArray[7] < transferAmt && wepArray[7] > 0 && vehWeps[7] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[7];
                            wepArray[7] -= wepArray[7];
                            vehWeps[7] -= 1;
                            lp.Character.Weapons.SniperRifle_M40A1.Ammo = tempTransfer;
                            Text("You grab a " + "Auto Sniper" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[7] >= transferAmt && vehWeps[7] > 0)
                        {
                            vehWeps[7] -= 1;
                            wepArray[7] -= transferAmt;
                            lp.Character.Weapons.SniperRifle_M40A1.Ammo = transferAmt;
                            Text("You grab an " + "Auto Sniper" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[7] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[7] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 10)
                    {
                        if (wepArray[6] < transferAmt && wepArray[6] > 0 && vehWeps[6] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[6];
                            wepArray[6] -= wepArray[6];
                            vehWeps[6] -= 1;
                            lp.Character.Weapons.BasicSniperRifle.Ammo = tempTransfer;
                            Text("You grab a " + "Bolt Sniper" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[6] >= transferAmt && vehWeps[6] > 0)
                        {
                            vehWeps[6] -= 1;
                            wepArray[6] -= transferAmt;
                            lp.Character.Weapons.BasicSniperRifle.Ammo = transferAmt;
                            Text("You grab a " + "Bolt Sniper" + " with " + transferAmt.ToString() + " rounds of ammo.", 5000);
                        }
                        else
                        {
                            if (vehWeps[6] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[6] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
            if (selectionVeh == 11 || selectionVeh == 12)
            {
                if (lp.Character.Weapons.AnyThrown.Ammo < lp.Character.Weapons.AnyThrown.MaxAmmo)
                {
                    int checkMax;
                    checkMax = lp.Character.Weapons.AnyThrown.MaxAmmo - lp.Character.Weapons.AnyThrown.Ammo;
                    if (checkMax >= transferAmt)
                    {
                        if (selectionVeh == 11)
                        {
                            if (wepArray[8] < transferAmt && wepArray[11] > 0 && lp.Character.Weapons.MolotovCocktails.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[8];
                                wepArray[8] -= wepArray[8];
                                lp.Character.Weapons.MolotovCocktails.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Molotov Belt.", 5000);
                            }
                            else if (wepArray[8] >= transferAmt && lp.Character.Weapons.MolotovCocktails.Ammo >= 0)
                            {
                                wepArray[8] -= transferAmt;
                                lp.Character.Weapons.MolotovCocktails.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of Molotovs for your Molotov Belt", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                        else if (selectionVeh == 12)
                        {
                            if (wepArray[9] < transferAmt && wepArray[12] > 0 && lp.Character.Weapons.Grenades.Ammo >= 0)
                            {
                                int tempTransfer;
                                tempTransfer = wepArray[9];
                                wepArray[9] -= wepArray[9];
                                lp.Character.Weapons.Grenades.Ammo += tempTransfer;
                                Text("You grab " + tempTransfer.ToString() + " rounds of ammo for your Grenades.", 5000);
                            }
                            else if (wepArray[9] >= transferAmt && lp.Character.Weapons.Grenades.Ammo >= 0)
                            {
                                wepArray[9] -= transferAmt;
                                lp.Character.Weapons.Grenades.Ammo += transferAmt;
                                Text("You grab " + transferAmt.ToString() + " rounds of Grenades for your Grenade Belt", 5000);
                            }
                            else Text("There is no ammo for this slot", 5000);
                        }
                    }
                    else Text("Your transfer amount of " + transferAmt.ToString() + " exceeds max ammo, lower transfer amount with RSHIFT + <--", 5000);
                }
                else Text("Your maxed out on rounds for this weapon", 5000);

                if (lp.Character.Weapons.MolotovCocktails.Ammo <= 0 && lp.Character.Weapons.Grenades.Ammo <= 0)
                {
                    if (selectionVeh == 11)
                    {
                        if (wepArray[8] < transferAmt && wepArray[8] > 0 && vehWeps[8] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[8];
                            wepArray[8] -= wepArray[8];
                            vehWeps[8] -= 1;
                            lp.Character.Weapons.MolotovCocktails.Ammo = tempTransfer;
                            Text("You grab a " + "Molotov belt" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[8] >= transferAmt && vehWeps[8] > 0)
                        {
                            vehWeps[8] -= 1;
                            wepArray[8] -= transferAmt;
                            lp.Character.Weapons.MolotovCocktails.Ammo = transferAmt;
                            Text("You grab an " + "Molotov belt" + " with " + transferAmt.ToString() + " moltovs on it.", 5000);
                        }
                        else 
                        { 
                            if (vehWeps[8] == 0)
                            Text("There is no weapons for this slot", 5000); 
                            else if (wepArray[8] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else 
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                    else if (selectionVeh == 12)
                    {
                        if (wepArray[9] < transferAmt && wepArray[9] > 0 && vehWeps[9] > 0)
                        {
                            int tempTransfer;
                            tempTransfer = wepArray[9];
                            wepArray[9] -= wepArray[9];
                            vehWeps[9] -= 1;
                            lp.Character.Weapons.Grenades.Ammo = tempTransfer;
                            Text("You grab a " + "Grenade belt" + " with " + tempTransfer.ToString() + " rounds of ammo.", 5000);
                        }
                        else if (wepArray[9] >= transferAmt && vehWeps[9] > 0)
                        {
                            vehWeps[9] -= 1;
                            wepArray[9] -= transferAmt;
                            lp.Character.Weapons.Grenades.Ammo = transferAmt;
                            Text("You grab a " + "Grenade belt" + " with " + transferAmt.ToString() + " grenades on it.", 5000);
                        }
                        else
                        {
                            if (vehWeps[9] == 0)
                                Text("There is no weapons for this slot", 5000);
                            else if (wepArray[9] == 0)
                                Text("There is no ammo for this slot", 5000);
                            else 
                                Text("There is no ammo or weapons for this slot", 5000);
                        }
                    }
                }
            }
        }
        void OpenCloseTrunkHood()
        {
            closestVeh = World.GetClosestVehicle(lp.Character.Position, 5f);
            if (Game.Exists(closestVeh))
            {
                vehHood = closestVeh.GetOffsetPosition(new Vector3(0.0f, +2.8f, 0.0f));
                vehTrunk = closestVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
            }
            if (Game.Exists(closestVeh) && closestVeh.Door(VehicleDoor.Trunk).isOpen && lp.Character.Position.DistanceTo(vehTrunk) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 5, true))
            {
                lp.Character.Animation.Play(new AnimationSet("missgun_car"), "shut_trunk", 8f, AnimationFlags.Unknown04);
                Wait(500);
                closestVeh.Door(VehicleDoor.Trunk).Close();
                Wait(1000);
                lp.Character.Task.ClearAllImmediately();
            }
            else if (Game.Exists(closestVeh) && !closestVeh.Door(VehicleDoor.Trunk).isOpen && lp.Character.Position.DistanceTo(vehTrunk) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 5, true))
            {
                if ((Game.Exists(vehArray[0]) && vehClosest1 && vehLocked1) || (Game.Exists(vehArray[1]) && vehClosest2 && vehLocked2) || (Game.Exists(vehArray[2]) && vehClosest3 && vehLocked3) || (Game.Exists(vehArray[3]) && vehClosest4 && vehLocked4))
                {
                    lp.Character.Animation.Play(new AnimationSet("missray1"), "d_locked_ds", 8f, AnimationFlags.Unknown04);
                    Wait(500);
                    Text("Vehicle is locked");
                }
                else
                {
                   // GetCarDoorLockStatus(Vehicle vehicle, eVehicleDoorLock *pValue) { NativeInvoke::Invoke<NATIVE_GET_CAR_DOOR_LOCK_STATUS, ScriptVoid>(vehicle, pValue); }
                    if (closestVeh.isRequiredForMission == true && closestVeh.DoorLock == DoorLock.ImpossibleToOpen)
                    {
                        lp.Character.Animation.Play(new AnimationSet("missray1"), "d_locked_ds", 8f, AnimationFlags.Unknown04);
                        Wait(500);
                        Text("Vehicle is locked");
                    }
                    else
                    {
                        lp.Character.Animation.Play(new AnimationSet("amb@bridgecops"), "open_boot", 8f, AnimationFlags.Unknown04);
                        Wait(500);
                        closestVeh.Door(VehicleDoor.Trunk).Open();
                        Wait(1000);
                        lp.Character.Task.ClearAllImmediately();
                    }
                }
            }
            if (Game.Exists(closestVeh) && closestVeh.Door(VehicleDoor.Hood).isOpen && lp.Character.Position.DistanceTo(vehHood) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 4, true))
            {
                lp.Character.Animation.Play(new AnimationSet("missgun_car"), "shut_trunk", 8f, AnimationFlags.Unknown04);
                Wait(500);
                closestVeh.Door(VehicleDoor.Hood).Close();
                Wait(1000);
                lp.Character.Task.ClearAllImmediately();
            }
            else if (Game.Exists(closestVeh) && !closestVeh.Door(VehicleDoor.Hood).isOpen && lp.Character.Position.DistanceTo(vehHood) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", closestVeh, 4, true))
            {
                lp.Character.Animation.Play(new AnimationSet("amb@bridgecops"), "open_boot", 8f, AnimationFlags.Unknown04);
                Wait(500);
                closestVeh.Door(VehicleDoor.Hood).Open();
                Wait(1000);
                lp.Character.Task.ClearAllImmediately();
            }
        }
        void ShowWepsBeingSearched(int[] wepArray, int[] wepAmmo, Vehicle vehArrayPos, Player mpPlayer)
        {
            for (int i = 0; i < wepArray.Length; i++)
            {
                if (Game.Exists(vehArrayPos))
                {
                    vehTrunk = vehArrayPos.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                    /*switch (i)
                    {
                        case 0:
                            wepChoice = Weapon.SMG_Uzi;
                            break;
                        case 1:
                            wepChoice = Weapon.SMG_MP5;
                            break;
                        case 2:
                            wepChoice = Weapon.Shotgun_Basic;
                            break;
                        case 3:
                            wepChoice = Weapon.Shotgun_Baretta;
                            break;
                        case 4:
                            wepChoice = Weapon.Rifle_AK47;
                            break;
                        case 5:
                            wepChoice = Weapon.Rifle_M4;
                            break;
                        case 6:
                            wepChoice = Weapon.SniperRifle_Basic;
                            break;
                        case 7:
                            wepChoice = Weapon.SniperRifle_M40A1;
                            break;
                        case 8:
                            wepChoice = Weapon.Thrown_Molotov;
                            break;
                        case 9:
                            wepChoice = Weapon.Thrown_Grenade;
                            break;
                        case 10:
                            wepChoice = Weapon.Handgun_Glock;
                            break;
                        case 11:
                            wepChoice = Weapon.Handgun_DesertEagle;
                            break;
                    }*/
                    if (mpPlayer.Character.Model == "M_Y_COP" || mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || mpPlayer.Character.Model == "CS_MITCHCOP" || mpPlayer.Character.Model == "M_M_FATCOP_01" || mpPlayer.Character.Model == "M_M_FBI" || mpPlayer.Character.Model == "M_Y_BOUNCER_01" || mpPlayer.Character.Model == "M_Y_BOUNCER_02" || mpPlayer.Character.Model == "M_M_ARMOURED" || mpPlayer.Character.Model == "IG_FRANCIS_MC" || mpPlayer.Character.Model == "M_Y_NHELIPILOT" || mpPlayer.Character.Model == "M_Y_SWAT" || mpPlayer.Character.Model == "M_Y_STROOPER")
                    {
                        if (wepArray[i] > 0 && Game.Exists(mpPlayer.Character) && mpPlayer.Character.Position.DistanceTo(vehTrunk) < 5 && mpPlayer.Character.Animation.isPlaying(new AnimationSet("misstaxidepot"), "copm_searchboot"))
                        {
                            Text("Officer " + mpPlayer.Name.ToString() + " has found weapons in your vehicle", 5000);
                            Wait(2000);
                            if (lp.Character.isInVehicle() && lp.Character.isAlive)
                                lp.Character.Animation.Play(new AnimationSet("missjeff2"), "jeff_upset", 8f, AnimationFlags.Unknown09 | AnimationFlags.Unknown01 | AnimationFlags.Unknown02 | AnimationFlags.Unknown11);
                            else
                                lp.Character.Animation.Play(new AnimationSet("gestures@car_f"), "upset", 8f, AnimationFlags.Unknown09 | AnimationFlags.Unknown01 | AnimationFlags.Unknown02 | AnimationFlags.Unknown11);
                            //lp.Character.SayAmbientSpeech("GENERIC_UPSET");
                            break;
                        }
                    }
                    else
                    {
                        if (wepArray[i] > 0 && Game.Exists(mpPlayer.Character) && mpPlayer.Character.Position.DistanceTo(vehTrunk) < 5 && mpPlayer.Character.Animation.isPlaying(new AnimationSet("misstaxidepot"), "copm_searchboot"))
                        {
                            Text("Criminal " + mpPlayer.Name.ToString() + " has found weapons in your vehicle", 5000);
                            Wait(2000);
                            if (lp.Character.isInVehicle() && lp.Character.isAlive)
                                lp.Character.Animation.Play(new AnimationSet("missjeff2"), "jeff_upset", 8f, AnimationFlags.Unknown09 | AnimationFlags.Unknown01 | AnimationFlags.Unknown02 | AnimationFlags.Unknown03);
                            else
                                lp.Character.Animation.Play(new AnimationSet("gestures@car_f"), "upset", 8f, AnimationFlags.Unknown09 | AnimationFlags.Unknown01 | AnimationFlags.Unknown02 | AnimationFlags.Unknown03);
                            //lp.Character.SayAmbientSpeech("GENERIC_UPSET");
                            break;
                        }
                    }
                }
            }
        }
        void AttachVehicle()
        {

            if (lp.Character.isInVehicle())
            {
                Vehicle towVeh;
                towVeh = lp.Character.CurrentVehicle;
                vehTrunk = towVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                //closestVeh = World.GetClosestVehicle(lp.Character.Position, 5f);
                Vehicle[] closeVeh;
                closeVeh = World.GetVehicles(lp.Character.CurrentVehicle.Position, 10);
                for (int i = 0; i < closeVeh.Length; i++)
                {
                    if (Game.Exists(closeVeh[i]) && closeVeh[i] != lp.Character.CurrentVehicle && closeVeh[i].Position.DistanceTo(vehTrunk) <= 5)
                    {
                        if (Game.Exists(closeVeh[i]) && closeVeh[i].Position.DistanceTo(vehTrunk) <= 5 && Function.Call<bool>("IS_CAR_ATTACHED", closeVeh[i]))
                        {
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_b"), "mirror_c", 8f, AnimationFlags.Unknown04);
                            Wait(1000);
                            Function.Call("DETACH_CAR", closeVeh[i]);
                            Game.DisplayText("Vehicle Detached");
                            break;
                        }
                        else if (closeVeh[i].Position.DistanceTo(vehTrunk) <= 5)
                        {
                            //Function.Call("ATTACH_CAR_TO_CAR_PHYSICALLY", towVeh, closeVeh[i], false, 0, 0.0F, 6.0F, -0.01F, 0.001F, 0.1F, 0.1F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F);
                            lp.Character.Animation.Play(new AnimationSet("amb@car_std_ds_b"), "mirror_c", 8f, AnimationFlags.Unknown04);
                            Wait(1000);
                            Function.Call("ATTACH_CAR_TO_CAR", closeVeh[i], towVeh, false, 0f, -5.4f, 0f, 0f, 0f, 0f);
                            Game.DisplayText("Vehicle Attached");
                            break;
                        }
                    }
                }
            }
            
        }
        void CopSettings()
        {
            if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
            {
                #region Cop sync Open/Close trunk and Locking
                if (Game.Exists(this.mpPlayer.Character))
                {
                    if (!syncTimer_Tick.isRunning)
                        syncTimer_Tick.Start();
                }
                if (!Game.Exists(this.mpPlayer.Character) && !Game.Exists(this.mpPlayer.Character))
                {
                    if (syncTimer_Tick.isRunning)
                        syncTimer_Tick.Stop();
                }
                #endregion
                #region Show Weps In Saved Vehicle For Cops
                if (searchFunc)
                {
                    if (vehSave1 == 1 || vehSave2 == 1 || vehSave3 == 1 || vehSave4 == 1)
                    {
                        if (CheckWepValues(vehWeps1) == true || CheckWepValues(vehWeps2) == true || CheckWepValues(vehWeps3) == true || CheckWepValues(vehWeps4) == true)
                        {
                            if (Game.Exists(this.mpPlayer.Character) && !this.mpPlayer.Character.isInVehicle() && ((Game.Exists(vehArray[0]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[0].Position) < 15) || (Game.Exists(vehArray[1]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[1].Position) < 15 || (Game.Exists(vehArray[2]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[2].Position) < 15)) || (Game.Exists(vehArray[3]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[3].Position) < 15)))
                            {
                                if (!showWeps_Tick.isRunning)
                                    showWeps_Tick.Start();
                            }
                        }
                        else if (CheckWepValues(vehWeps1) == false && CheckWepValues(vehWeps2) == false && CheckWepValues(vehWeps3) == false && CheckWepValues(vehWeps4) == false)
                        {
                            if (showWeps_Tick.isRunning)
                                showWeps_Tick.Stop();
                        }
                    }
                }
                #endregion
            }
            else
            {
               
            }
        }
        void CrookSettings()
        {
            if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Model == "M_Y_COP" || this.mpPlayer.Character.Model == "M_Y_COP_TRAFFIC" || this.mpPlayer.Character.Model == "CS_MITCHCOP" || this.mpPlayer.Character.Model == "M_M_FATCOP_01" || this.mpPlayer.Character.Model == "M_M_FBI" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_01" || this.mpPlayer.Character.Model == "M_Y_BOUNCER_02" || this.mpPlayer.Character.Model == "M_M_ARMOURED" || this.mpPlayer.Character.Model == "IG_FRANCIS_MC" || this.mpPlayer.Character.Model == "M_Y_NHELIPILOT" || this.mpPlayer.Character.Model == "M_Y_SWAT" || this.mpPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
            {
                
            }
            else
            {
                #region Crook Sync Open/Close Trunk And Locking
                if (Game.Exists(this.mpPlayer.Character))
                {
                    if (!syncTimer_Tick.isRunning)
                    {
                        syncTimer_Tick.Start();
                    }
                }
                if (!Game.Exists(this.mpPlayer.Character) && !Game.Exists(this.mpPlayer.Character))
                {
                    if (syncTimer_Tick.isRunning)
                        syncTimer_Tick.Stop();
                }
                #endregion
                #region Show Weps In Saved Vehicle For Crooks
                if (searchFunc)
                {
                    if (vehSave1 == 1 || vehSave2 == 1 || vehSave3 == 1 || vehSave4 == 1)
                    {
                        if (CheckWepValues(vehWeps1) == true || CheckWepValues(vehWeps2) == true || CheckWepValues(vehWeps3) == true || CheckWepValues(vehWeps4) == true)
                        {
                            if (Game.Exists(this.mpPlayer.Character) && !this.mpPlayer.Character.isInVehicle() && ((Game.Exists(vehArray[0]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[0].Position) < 15) || (Game.Exists(vehArray[1]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[1].Position) < 15 || (Game.Exists(vehArray[2]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[2].Position) < 15)) || (Game.Exists(vehArray[3]) && this.mpPlayer.Character.Position.DistanceTo(vehArray[3].Position) < 15)))
                            {
                                if (!showWeps_Tick.isRunning)
                                    showWeps_Tick.Start();
                            }
                        }
                        else if (CheckWepValues(vehWeps1) == false && CheckWepValues(vehWeps2) == false && CheckWepValues(vehWeps3) == false && CheckWepValues(vehWeps4) == false)
                        {
                            if (showWeps_Tick.isRunning)
                                showWeps_Tick.Stop();
                        }
                    }
                }
                #endregion
            }
        }
        void IsMpPlayerOpenCloseTrunk(Player player, Vehicle vehicle, Vector3 trunk, Vector3 hood)
        {
            if (Game.Exists(vehicle) && Game.Exists(player.Character) && player.Character.Position.DistanceTo(trunk) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehicle, 5, true))
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@bridgecops"), "open_boot"))
                {
                    vehicle.Door(VehicleDoor.Trunk).Open();
                }
            }
            if (Game.Exists(vehicle) && Game.Exists(player.Character) && player.Character.Position.DistanceTo(trunk) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehicle, 5, true))
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("missgun_car"), "shut_trunk"))
                {
                    vehicle.Door(VehicleDoor.Trunk).Close();
                }
            }
            if (Game.Exists(vehicle) && Game.Exists(player.Character) && player.Character.Position.DistanceTo(hood) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehicle, 4, true))
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("missgun_car"), "shut_trunk"))
                {
                    vehicle.Door(VehicleDoor.Hood).Close();
                }
            }
            if (Game.Exists(vehicle) && Game.Exists(player.Character) && player.Character.Position.DistanceTo(hood) <= 1.6 && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", vehicle, 4, true))
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@bridgecops"), "open_boot"))
                {
                    vehicle.Door(VehicleDoor.Hood).Open();
                }
            }
            if (Game.Exists(player.Character) && player.Character.isInVehicle() && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", player.Character.CurrentVehicle, 4, true) && (Game.Exists(player.Character.CurrentVehicle) || Game.Exists(player.LastVehicle))) // hood
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@car_low_ps_loops"), "alt_sit_ps_a"))
                {
                    if (Game.Exists(player.Character.CurrentVehicle))
                    player.Character.CurrentVehicle.Door(VehicleDoor.Hood).Open();
                    else 
                        if(Game.Exists(player.LastVehicle))
                            player.LastVehicle.Door(VehicleDoor.Hood).Open();
                }
            }
            if (Game.Exists(player.Character) && player.Character.isInVehicle() && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", player.Character.CurrentVehicle, 5, true) && (Game.Exists(player.Character.CurrentVehicle) || Game.Exists(player.LastVehicle))) // Trunk
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@car_cell_dsty_ds"), "cell_destroy"))
                {
                    if (Game.Exists(player.Character.CurrentVehicle))
                        player.Character.CurrentVehicle.Door(VehicleDoor.Trunk).Open();
                    else
                        if (Game.Exists(player.LastVehicle))
                            player.LastVehicle.Door(VehicleDoor.Trunk).Open();
                }
            }
            if (Game.Exists(player.Character) && player.Character.isInVehicle() && !Function.Call<bool>("IS_CAR_DOOR_DAMAGED", player.Character.CurrentVehicle, 5, true) && (Game.Exists(player.Character.CurrentVehicle) || Game.Exists(player.LastVehicle))) // Trunk
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@car_std_ds_a"), "change_radio"))
                {
                    if (Game.Exists(player.Character.CurrentVehicle))
                    {
                        player.Character.CurrentVehicle.HazardLightsOn = true;
                        player.Character.CurrentVehicle.HazardLightsOn = false;
                    }
                    else
                    {
                        if (Game.Exists(player.LastVehicle))
                        {
                            player.LastVehicle.HazardLightsOn = true;
                            player.LastVehicle.HazardLightsOn = false;
                        }
                    }
                }
            }
            if (Game.Exists(player.Character) && player.Character.isInVehicle())
            {
                if (player.Character.Animation.isPlaying(new AnimationSet("amb@car_std_ds_b"), "mirror_c"))
                {
                    if (Game.Exists(vehicle) && vehicle.Position.DistanceTo(vehTrunk) <= 5 && Function.Call<bool>("IS_CAR_ATTACHED", vehicle))
                    {
                        Function.Call("DETACH_CAR", vehicle);
                        Game.DisplayText("Vehicle Detached");
                    }
                    else if (vehicle.Position.DistanceTo(vehTrunk) <= 5)
                    {
                        Vehicle towVeh;
                        towVeh = player.Character.CurrentVehicle;
                        //Function.Call("ATTACH_CAR_TO_CAR_PHYSICALLY", towVeh, closeVeh[i], false, 0, 0.0F, 6.0F, -0.01F, 0.001F, 0.1F, 0.1F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F);
                        Function.Call("ATTACH_CAR_TO_CAR", vehicle, towVeh, false, 0f, -5.4f, 0f, 0f, 0f, 0f);
                        Game.DisplayText("Vehicle Attached");
                    }
                }
            }
        }
        void SearchVehicle()
        {
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {
                    this.mpPlayer = mpPlayer;
                    if (Game.Exists(this.mpPlayer.Character) && Game.LocalPlayer.Character != this.mpPlayer.Character)
                    {
                        if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER")
                        {
                            closestVeh = World.GetClosestVehicle(lp.Character.Position, 5.5f);
                            if (Game.Exists(closestVeh))
                            {
                                vehTrunk = closestVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                                if (lp.Character.isTouching(closestVeh) && lp.Character.Position.DistanceTo(vehTrunk) < 1.6 && closestVeh.Door(VehicleDoor.Trunk).isOpen)
                                {
                                    if (closestVeh.isRequiredForMission)
                                    {
                                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "copm_searchboot", 8f, AnimationFlags.Unknown05);
                                        Text("Searching vehicle...");
                                        Wait(3000);
                                        if (this.mpPlayer.Character.isRagdoll)
                                        {
                                            Text("Searching vehicle delayed...");
                                            Wait(3000);
                                        }
                                        lp.Character.Task.ClearAllImmediately();
                                        if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Animation.isPlaying(new AnimationSet("missjeff2"), "jeff_upset"))
                                            Text("You found weapons in the vehicle, impound vehicle for money.", 5000);
                                        else if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Animation.isPlaying(new AnimationSet("gestures@car_f"), "upset"))
                                            Text("You found weapons in the vehicle, impound vehicle for money.", 5000);
                                        else Text("You found nothing during the search, make sure suspect is alive when searching", 5000);
                                        searchCopAuth = false;
                                        //GTA.Pickup searchPick;
                                        //searchPick = Pickup.CreatePickup(closestVeh.Position, 0x7E5379BC, GTA.PickupType.Weapon);
                                    }
                                    else
                                    {
                                        Text("No player owns this vehicle, you wont find anything in it.", 5000);
                                    }
                                }
                                else
                                    Text("Make sure you are near and touching the trunk.  The trunk must be open in order to search!", 5000);
                            }
                        }
                        else
                        {
                            closestVeh = World.GetClosestVehicle(lp.Character.Position, 5.5f);
                            if (Game.Exists(closestVeh))
                            {
                                vehTrunk = closestVeh.GetOffsetPosition(new Vector3(0.0f, -2.8f, 0.0f));
                                if (lp.Character.isTouching(closestVeh) && lp.Character.Position.DistanceTo(vehTrunk) < 1.6 && closestVeh.Door(VehicleDoor.Trunk).isOpen)
                                {
                                    if (closestVeh.isRequiredForMission)
                                    {
                                        lp.Character.Animation.Play(new AnimationSet("misstaxidepot"), "copm_searchboot", 8f, AnimationFlags.Unknown05);
                                        Text("Searching vehicle...");
                                        Wait(3000);
                                        if (this.mpPlayer.Character.isRagdoll)
                                        {
                                            Text("Searching vehicle delayed...");
                                            Wait(3000);
                                        }
                                        lp.Character.Task.ClearAllImmediately();
                                        if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Animation.isPlaying(new AnimationSet("missjeff2"), "jeff_upset"))
                                            Text("You found weapons in the vehicle, impound vehicle for money.", 5000);
                                        else if (Game.Exists(this.mpPlayer.Character) && this.mpPlayer.Character.Animation.isPlaying(new AnimationSet("gestures@car_f"), "upset"))
                                            Text("You found weapons in the vehicle, impound vehicle for money.", 5000);
                                        else Text("You found nothing during the search, make sure owner of vehicle is alive when searching", 5000);
                                        searchAuth = false;
                                        //GTA.Pickup searchPick;
                                        //searchPick = Pickup.CreatePickup(closestVeh.Position, 0x7E5379BC, GTA.PickupType.Weapon);
                                    }
                                    else
                                    {
                                        Text("No player owns this vehicle, you wont find anything in it.", 5000);
                                    }
                                }
                                else
                                    Text("Make sure you are near and touching the trunk.  The trunk must be open in order to search!", 5000);
                            }
                        }
                    }
                }
            }
        }
        void SetGhostTempVehPos(ref Vehicle vehicle, ref Vehicle vehTemp, ref Model vehModel, ref Vector3 vehVec, ref ColorIndex vehColor, ref Blip vehBlip, ref float vehHead, ref float vehEngHealth, ref int vehHealth, ref int vehSave, ref int posBlip, ref bool vEng, ref bool vtFL, ref bool vtFR, ref bool vtBL, ref bool vtBR, ref bool vehLocked, ref int indexVehColor, ref BlipColor blipColor, ref bool vehStored, ref bool bVehVec)
        {
            if (Game.Exists(vehBlip))
            {
                vehBlip.Delete();
            }
            vehBlip = Blip.AddBlip(vehVec);
            vehBlip.Display = BlipDisplay.ArrowAndMap;
            vehBlip.Icon = BlipIcon.Building_Garage;
            vehBlip.Color = blipColor;
            vehBlip.Scale = 1.0f;
            vehBlip.ShowOnlyWhenNear = true;
            vehBlip.Name = "Saved Vehicle " + (posBlip).ToString();
            vehSave = 1;
            vehStored = true;
            bVehVec = true;
            if (!vehToVec_Tick.isRunning)
                vehToVec_Tick.Start();
        }
        void SetTempVehPos(ref Vehicle vehicle, ref Vehicle vehTemp, ref Model vehModel, ref Vector3 vehVec, ref ColorIndex vehColor, ref Blip vehBlip, ref float vehHead, ref float vehEngHealth, ref int vehHealth, ref int vehSave, ref int posBlip, ref bool vEng, ref bool vtFL, ref bool vtFR, ref bool vtBL, ref bool vtBR, ref bool vehLocked, ref int indexVehColor, ref BlipColor blipColor, ref bool vehStored)
        {
            vehVec = vehicle.Position;
            vehHead = vehicle.Heading;
            vehColor = vehicle.Color;
            vehModel = vehicle.Model;
            vehHealth = vehicle.Health;
            vehEngHealth = vehicle.EngineHealth;
            if (vehicle.EngineRunning == true)
                vEng = true;
            else
                vEng = false;
            if (vehicle.DoorLock == DoorLock.ImpossibleToOpen)
                vehLocked = true;
            else
                vehLocked = false;
            CheckTireDamage(ref vehicle, ref vtFL, ref vtFR, ref vtBL, ref vtBR);
            if (Game.Exists(vehBlip))
            {
                vehBlip.Delete();
            }
            vehBlip = Blip.AddBlip(vehVec);
            vehBlip.Display = BlipDisplay.ArrowAndMap;
            vehBlip.Icon = BlipIcon.Building_Garage;
            vehBlip.Color = blipColor;
            vehBlip.Scale = 1.0f;
            vehBlip.ShowOnlyWhenNear = true;
            vehBlip.Name = "Saved Vehicle " + (posBlip).ToString();
            vehSave = 1;
            vehTemp = vehicle;
            indexVehColor = vehicle.Color.Index;
            vehStored = true;
        }
        void CreateNewLoadedVehicle(ref Vehicle vehicle, ref Vehicle vehTemp, ref Model vehModel, ref Vector3 vehVec, ref ColorIndex vehColor, ref Blip vehBlip, ref float vehHead, ref float vehEngHealth, ref int vehHealth, ref int vehSave, ref int posBlip, ref bool vEng, ref bool vtFL, ref bool vtFR, ref bool vtBL, ref bool vtBR, ref bool vehLocked, ref BlipColor blipColor, ref bool vehStored)
        {
            vehicle = World.CreateVehicle(vehModel, vehVec);
            //Wait(2000);
            vehicle.Color = vehColor;
            vehicle.Heading = vehHead;
            vehicle.Health = vehHealth;
            vehicle.EngineHealth = vehEngHealth;
            if (vEng == true)
                vehicle.EngineRunning = true;
            else
                vehicle.EngineRunning = false;
            vehicle.isRequiredForMission = true;
            if (vehLocked)
                vehicle.DoorLock = DoorLock.ImpossibleToOpen;
            if (Game.Exists(vehBlip))
            {
                vehBlip.Delete();
            }
            vehBlip = vehicle.AttachBlip();
            vehBlip.Display = BlipDisplay.ArrowAndMap;
            vehBlip.Icon = BlipIcon.Building_Garage;
            vehBlip.Color = blipColor;
            vehBlip.Scale = 1.0f;
            vehBlip.ShowOnlyWhenNear = true;
            vehBlip.Name = "Saved Vehicle " + (posBlip).ToString();
            vehSave = 1;
            SetTireDamage(ref vehicle, ref vtFL, ref vtFR, ref vtBL, ref vtBR);
            vehTemp = vehicle;
            //vehCount++;
            vehStored = false;
        }
        void CheckTireDamage(ref Vehicle vehicle, ref bool vtFL, ref bool vtFR, ref bool vtBL, ref bool vtBR)
        {
            if (vehicle.IsTireBurst(VehicleWheel.FrontLeft))
                vtFL = true;
            if (vehicle.IsTireBurst(VehicleWheel.FrontRight))
                vtFR = true;
            if (vehicle.IsTireBurst(VehicleWheel.RearLeft))
                vtBL = true;
            if (vehicle.IsTireBurst(VehicleWheel.RearRight))
                vtBR = true;
        }
        void SetTireDamage(ref Vehicle vehicle, ref bool vtFL, ref bool vtFR, ref bool vtBL, ref bool vtBR)
        {
            if (vtFL == true)
            {
                vehicle.BurstTire(VehicleWheel.FrontLeft);
                vtFL = false;
            }
            if (vtFR == true)
            {
                vehicle.BurstTire(VehicleWheel.FrontRight);
                vtFR = false;
            }
            if (vtBL == true)
            {
                vehicle.BurstTire(VehicleWheel.RearLeft);
                vtBL = false;
            }
            if (vtBR == true)
            {
                vehicle.BurstTire(VehicleWheel.RearRight);
                vtBR = false;
            }
        }
        void SaveDat()
        {
            // Check if multiplayer game
            #region Multiplayer Save Type
            if (Game.isMultiplayer)
            {
                // Vehicle exists, lets save everything about it
                if (vehSave1 == 1 && Game.Exists(vehArray[0]) && vehArray[0].Position.DistanceTo(lp.Character.Position) < 200)
                {
                    tTempVehicle = vehArray[0];
                    tTempBlip = vehBlipArray[0];
                    vehVec1 = tTempVehicle.Position;
                    vehHead1 = tTempVehicle.Heading;
                    indexVehColor1 = tTempVehicle.Color.Index;
                    vehModel1 = tTempVehicle.Model;
                    vehHealth1 = tTempVehicle.Health;
                    vehEngHealth1 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng1 = true;
                    else
                        vEng1 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1);
                    vehSave1 = 1;
                }
                else if (vehSave1 == 1 && vehStored1 && vehVec1.DistanceTo(lp.Character.Position) > 200)
                {
                    // Vehicle is stored in memory but not created due to multiplayer workaround                  
                    vehSave1 = 1;
                }
                else
                {
                    // Its empty
                    vehSave1 = 0;
                }
                if (vehSave2 == 1 && Game.Exists(vehArray[1]) && vehArray[1].Position.DistanceTo(lp.Character.Position) < 200)
                {
                    tTempVehicle = vehArray[1];
                    tTempBlip = vehBlipArray[1];
                    vehVec2 = tTempVehicle.Position;
                    vehHead2 = tTempVehicle.Heading;
                    indexVehColor2 = tTempVehicle.Color.Index;
                    vehModel2 = tTempVehicle.Model;
                    vehHealth2 = tTempVehicle.Health;
                    vehEngHealth2 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng2 = true;
                    else
                        vEng2 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2);
                    vehSave2 = 1;
                }
                else if (vehSave2 == 1 && vehStored2 && vehVec2.DistanceTo(lp.Character.Position) > 200)
                {
                    // Vehicle is stored in memory but not created due to multiplayer workaround
                    vehSave2 = 1;
                }
                else
                {
                    // Its empty
                    vehSave2 = 0;
                }
                if (vehSave3 == 1 && Game.Exists(vehArray[2]) && vehArray[2].Position.DistanceTo(lp.Character.Position) < 200)
                {
                    tTempVehicle = vehArray[2];
                    tTempBlip = vehBlipArray[2];
                    vehVec3 = tTempVehicle.Position;
                    vehHead3 = tTempVehicle.Heading;
                    indexVehColor3 = tTempVehicle.Color.Index;
                    vehModel3 = tTempVehicle.Model;
                    vehHealth3 = tTempVehicle.Health;
                    vehEngHealth3 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng3 = true;
                    else
                        vEng3 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3);
                    vehSave3 = 1;
                }
                else if (vehSave3 == 1 && vehStored3 && vehVec3.DistanceTo(lp.Character.Position) > 200)
                {
                    // Vehicle is stored in memory but not created due to multiplayer workaround
                    vehSave3 = 1;
                }
                else
                {
                    // Its empty
                    vehSave3 = 0;
                }
                if (vehSave4 == 1 && Game.Exists(vehArray[3]) && vehArray[3].Position.DistanceTo(lp.Character.Position) < 200)
                {
                    tTempVehicle = vehArray[3];
                    tTempBlip = vehBlipArray[3];
                    vehVec4 = tTempVehicle.Position;
                    vehHead4 = tTempVehicle.Heading;
                    indexVehColor4 = tTempVehicle.Color.Index;
                    vehModel4 = tTempVehicle.Model;
                    vehHealth4 = tTempVehicle.Health;
                    vehEngHealth4 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng4 = true;
                    else
                        vEng4 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4);
                    vehSave4 = 1;
                }
                else if (vehSave4 == 1 && vehStored4 && vehVec4.DistanceTo(lp.Character.Position) > 200)
                {
                    // Vehicle is stored in memory but not created due to multiplayer workaround
                    vehSave4 = 1;
                }
                else
                {
                    // Its empty
                    vehSave4 = 0;
                }
            }
            #endregion
            #region SinglePlayer Save Type
            else // If game is SinglePlayer
            {
                // Vehicle exists, lets save everything about it
                if (vehSave1 == 1 && Game.Exists(vehArray[0]))
                {
                    tTempVehicle = vehArray[0];
                    tTempBlip = vehBlipArray[0];
                    vehVec1 = tTempVehicle.Position;
                    vehHead1 = tTempVehicle.Heading;
                    indexVehColor1 = tTempVehicle.Color.Index;
                    vehModel1 = tTempVehicle.Model;
                    vehHealth1 = tTempVehicle.Health;
                    vehEngHealth1 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng1 = true;
                    else
                        vEng1 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1);
                    vehSave1 = 1;
                }
                else
                {
                    // Its empty
                    vehSave1 = 0;
                }
                if (vehSave2 == 1 && Game.Exists(vehArray[1]))
                {
                    tTempVehicle = vehArray[1];
                    tTempBlip = vehBlipArray[1];
                    vehVec2 = tTempVehicle.Position;
                    vehHead2 = tTempVehicle.Heading;
                    indexVehColor2 = tTempVehicle.Color.Index;
                    vehModel2 = tTempVehicle.Model;
                    vehHealth2 = tTempVehicle.Health;
                    vehEngHealth2 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng2 = true;
                    else
                        vEng2 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2);
                    vehSave2 = 1;
                }
                else
                {
                    // Its empty
                    vehSave2 = 0;
                }
                if (vehSave3 == 1 && Game.Exists(vehArray[2]))
                {
                    tTempVehicle = vehArray[2];
                    tTempBlip = vehBlipArray[2];
                    vehVec3 = tTempVehicle.Position;
                    vehHead3 = tTempVehicle.Heading;
                    indexVehColor3 = tTempVehicle.Color.Index;
                    vehModel3 = tTempVehicle.Model;
                    vehHealth3 = tTempVehicle.Health;
                    vehEngHealth3 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng3 = true;
                    else
                        vEng3 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3);
                    vehSave3 = 1;
                }
                else
                {
                    // Its empty
                    vehSave3 = 0;
                }
                if (vehSave4 == 1 && Game.Exists(vehArray[3]))
                {
                    tTempVehicle = vehArray[3];
                    tTempBlip = vehBlipArray[3];
                    vehVec4 = tTempVehicle.Position;
                    vehHead4 = tTempVehicle.Heading;
                    indexVehColor4 = tTempVehicle.Color.Index;
                    vehModel4 = tTempVehicle.Model;
                    vehHealth4 = tTempVehicle.Health;
                    vehEngHealth4 = tTempVehicle.EngineHealth;
                    if (tTempVehicle.EngineRunning == true)
                        vEng4 = true;
                    else
                        vEng4 = false;
                    CheckTireDamage(ref tTempVehicle, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4);
                    vehSave4 = 1;
                }
                else
                {
                    // Its empty
                    vehSave4 = 0;
                }
            }
            #endregion
            #region Fileout
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "EverythingCarSaves.ini"));

            // 0 = uzi, 1 = mp5, 2 = pump, 3 = baretta, 4 = ak, 5 = m4, 6 = bolt, 7 = auto, 8 = molotov, 9 = grenade; 10 = glock, 11 = deagle

                //Vehicle 1
                ini.SetValue("VehSaved1", "VEHICLE1", vehSave1);
                ini.SetValue("VehPos1", "VEHICLE1", vehVec1);
                ini.SetValue("VehHead1", "VEHICLE1", vehHead1);
                ini.SetValue("VehColor1", "VEHICLE1", indexVehColor1);
                ini.SetValue("VehModel1", "VEHICLE1", vehModel1);
                ini.SetValue("VehHealth1", "VEHICLE1", vehHealth1);
                ini.SetValue("VehEngHealth1", "VEHICLE1", vehEngHealth1);
                ini.SetValue("EngOn1", "VEHICLE1", vEng1);
                ini.SetValue("TireFrontLeftFlat1", "VEHICLE1", vtFL1);
                ini.SetValue("TireFrontRightFlat1", "VEHICLE1", vtFR1);
                ini.SetValue("TireBackLeftFlat1", "VEHICLE1", vtBL1);
                ini.SetValue("TireBackRightFlat1", "VEHICLE1", vtBR1);
                ini.SetValue("VehLocked1", "VEHICLE1", vehLocked1);

                //Veh Weps 1
                ini.SetValue("akGuns1", "VEHICLEWEAPONS1", vehWeps1[0]);
                ini.SetValue("m4Guns1", "VEHICLEWEAPONS1", vehWeps1[1]);
                ini.SetValue("glockGuns1", "VEHICLEWEAPONS1", vehWeps1[2]);
                ini.SetValue("deagleGuns1", "VEHICLEWEAPONS1", vehWeps1[3]);
                ini.SetValue("mp5Guns1", "VEHICLEWEAPONS1", vehWeps1[4]);
                ini.SetValue("uziGuns1", "VEHICLEWEAPONS1", vehWeps1[5]);
                ini.SetValue("autoGuns1", "VEHICLEWEAPONS1", vehWeps1[6]);
                ini.SetValue("boltGuns1", "VEHICLEWEAPONS1", vehWeps1[7]);
                ini.SetValue("barettaGuns1", "VEHICLEWEAPONS1", vehWeps1[8]);
                ini.SetValue("pumpGuns1", "VEHICLEWEAPONS1", vehWeps1[9]);
                ini.SetValue("molGuns1", "VEHICLEWEAPONS1", vehWeps1[10]);
                ini.SetValue("grenGuns1", "VEHICLEWEAPONS1", vehWeps1[11]);
                //Veh Ammo1
                ini.SetValue("akAmmo1", "VEHICLEAMMO1", vA1[0]);
                ini.SetValue("m4Ammo1", "VEHICLEAMMO1", vA1[1]);
                ini.SetValue("glockAmmo1", "VEHICLEAMMO1", vA1[2]);
                ini.SetValue("deagleAmmo1", "VEHICLEAMMO1", vA1[3]);
                ini.SetValue("mp5Ammo1", "VEHICLEAMMO1", vA1[4]);
                ini.SetValue("uziAmmo1", "VEHICLEAMMO1", vA1[5]);
                ini.SetValue("autoAmmo1", "VEHICLEAMMO1", vA1[6]);
                ini.SetValue("boltAmmo1", "VEHICLEAMMO1", vA1[7]);
                ini.SetValue("barettaAmmo1", "VEHICLEAMMO1", vA1[8]);
                ini.SetValue("pumpAmmo1", "VEHICLEAMMO1", vA1[9]);
                ini.SetValue("molAmmo1", "VEHICLEAMMO1", vA1[10]);
                ini.SetValue("grenAmmo1", "VEHICLEAMMO1", vA1[11]);
                 //Vehicle 2
                ini.SetValue("VehSaved2", "VEHICLE2", vehSave2);
                ini.SetValue("VehPos2", "VEHICLE2", vehVec2);
                ini.SetValue("VehHead2", "VEHICLE2", vehHead2);
                ini.SetValue("VehColor2", "VEHICLE2", indexVehColor2);
                ini.SetValue("VehModel2", "VEHICLE2", vehModel2);
                ini.SetValue("VehHealth2", "VEHICLE2", vehHealth2);
                ini.SetValue("VehEngHealth2", "VEHICLE2", vehEngHealth2);
                ini.SetValue("EngOn2", "VEHICLE2", vEng2);
                ini.SetValue("TireFrontLeftFlat2", "VEHICLE2", vtFL2);
                ini.SetValue("TireFrontRightFlat2", "VEHICLE2", vtFR2);
                ini.SetValue("TireBackLeftFlat2", "VEHICLE2", vtBL2);
                ini.SetValue("TireBackRightFlat2", "VEHICLE2", vtBR2);
                ini.SetValue("VehLocked2", "VEHICLE2", vehLocked2);

                //Veh weps 2
                ini.SetValue("akGuns2", "VEHICLEWEAPONS2", vehWeps2[0]);
                ini.SetValue("m4Guns2", "VEHICLEWEAPONS2", vehWeps2[1]);
                ini.SetValue("glockGuns2", "VEHICLEWEAPONS2", vehWeps2[2]);
                ini.SetValue("deagleGuns2", "VEHICLEWEAPONS2", vehWeps2[3]);
                ini.SetValue("mp5Guns2", "VEHICLEWEAPONS2", vehWeps2[4]);
                ini.SetValue("uziGuns2", "VEHICLEWEAPONS2", vehWeps2[5]);
                ini.SetValue("autoGuns2", "VEHICLEWEAPONS2", vehWeps2[6]);
                ini.SetValue("boltGuns2", "VEHICLEWEAPONS2", vehWeps2[7]);
                ini.SetValue("barettaGuns2", "VEHICLEWEAPONS2", vehWeps2[8]);
                ini.SetValue("pumpGuns2", "VEHICLEWEAPONS2", vehWeps2[9]);
                ini.SetValue("molGuns2", "VEHICLEWEAPONS2", vehWeps2[10]);
                ini.SetValue("grenGuns2", "VEHICLEWEAPONS2", vehWeps2[11]);

                //Veh Ammo 2
                ini.SetValue("akAmmo2", "VEHICLEAMMO2", vA2[0]);
                ini.SetValue("m4Ammo2", "VEHICLEAMMO2", vA2[1]);
                ini.SetValue("glockAmmo2", "VEHICLEAMMO2", vA2[2]);
                ini.SetValue("deagleAmmo2", "VEHICLEAMMO2", vA2[3]);
                ini.SetValue("mp5Ammo2", "VEHICLEAMMO2", vA2[4]);
                ini.SetValue("uziAmmo2", "VEHICLEAMMO2", vA2[5]);
                ini.SetValue("autoAmmo2", "VEHICLEAMMO2", vA2[6]);
                ini.SetValue("boltAmmo2", "VEHICLEAMMO2", vA2[7]);
                ini.SetValue("barettaAmmo2", "VEHICLEAMMO2", vA2[8]);
                ini.SetValue("pumpAmmo2", "VEHICLEAMMO2", vA2[9]);
                ini.SetValue("molAmmo2", "VEHICLEAMMO2", vA2[10]);
                ini.SetValue("grenAmmo2", "VEHICLEAMMO2", vA2[11]);

                //Vehicle 3
                ini.SetValue("VehSaved3", "VEHICLE3", vehSave3);
                ini.SetValue("VehPos3", "VEHICLE3", vehVec3);
                ini.SetValue("VehHead3", "VEHICLE3", vehHead3);
                ini.SetValue("VehColor3", "VEHICLE3", indexVehColor3);
                ini.SetValue("VehModel3", "VEHICLE3", vehModel3);
                ini.SetValue("VehHealth3", "VEHICLE3", vehHealth3);
                ini.SetValue("VehEngHealth3", "VEHICLE3", vehEngHealth3);
                ini.SetValue("EngOn3", "VEHICLE3", vEng3);
                ini.SetValue("TireFrontLeftFlat3", "VEHICLE3", vtFL3);
                ini.SetValue("TireFrontRightFlat3", "VEHICLE3", vtFR3);
                ini.SetValue("TireBackLeftFlat3", "VEHICLE3", vtBL3);
                ini.SetValue("TireBackRightFlat3", "VEHICLE3", vtBR3);
                ini.SetValue("VehLocked3", "VEHICLE3", vehLocked3);

                //Veh weps 3
                ini.SetValue("akGuns3", "VEHICLEWEAPONS3", vehWeps3[0]);
                ini.SetValue("m4Guns3", "VEHICLEWEAPONS3", vehWeps3[1]);
                ini.SetValue("glockGuns3", "VEHICLEWEAPONS3", vehWeps3[2]);
                ini.SetValue("deagleGuns3", "VEHICLEWEAPONS3", vehWeps3[3]);
                ini.SetValue("mp5Guns3", "VEHICLEWEAPONS3", vehWeps3[4]);
                ini.SetValue("uziGuns3", "VEHICLEWEAPONS3", vehWeps3[5]);
                ini.SetValue("autoGuns3", "VEHICLEWEAPONS3", vehWeps3[6]);
                ini.SetValue("boltGuns3", "VEHICLEWEAPONS3", vehWeps3[7]);
                ini.SetValue("barettaGuns3", "VEHICLEWEAPONS3", vehWeps3[8]);
                ini.SetValue("pumpGuns3", "VEHICLEWEAPONS3", vehWeps3[9]);
                ini.SetValue("molGuns3", "VEHICLEWEAPONS3", vehWeps3[10]);
                ini.SetValue("grenGuns3", "VEHICLEWEAPONS3", vehWeps3[11]);

                //Veh Ammo 3
                ini.SetValue("akAmmo3", "VEHICLEAMMO3", vA3[0]);
                ini.SetValue("m4Ammo3", "VEHICLEAMMO3", vA3[1]);
                ini.SetValue("glockAmmo3", "VEHICLEAMMO3", vA3[2]);
                ini.SetValue("deagleAmmo3", "VEHICLEAMMO3", vA3[3]);
                ini.SetValue("mp5Ammo3", "VEHICLEAMMO3", vA3[4]);
                ini.SetValue("uziAmmo3", "VEHICLEAMMO3", vA3[5]);
                ini.SetValue("autoAmmo3", "VEHICLEAMMO3", vA3[6]);
                ini.SetValue("boltAmmo3", "VEHICLEAMMO3", vA3[7]);
                ini.SetValue("barettaAmmo3", "VEHICLEAMMO3", vA3[8]);
                ini.SetValue("pumpAmmo3", "VEHICLEAMMO3", vA3[9]);
                ini.SetValue("molAmmo3", "VEHICLEAMMO3", vA3[10]);
                ini.SetValue("grenAmmo3", "VEHICLEAMMO3", vA3[11]);

                //Vehicle 4
                ini.SetValue("VehSaved4", "VEHICLE4", vehSave4);
                ini.SetValue("VehPos4", "VEHICLE4", vehVec4);
                ini.SetValue("VehHead4", "VEHICLE4", vehHead4);
                ini.SetValue("VehColor4", "VEHICLE4", indexVehColor4);
                ini.SetValue("VehModel4", "VEHICLE4", vehModel4);
                ini.SetValue("VehHealth4", "VEHICLE4", vehHealth4);
                ini.SetValue("VehEngHealth4", "VEHICLE4", vehEngHealth4);
                ini.SetValue("EngOn4", "VEHICLE4", vEng4);
                ini.SetValue("TireFrontLeftFlat4", "VEHICLE4", vtFL4);
                ini.SetValue("TireFrontRightFlat4", "VEHICLE4", vtFR4);
                ini.SetValue("TireBackLeftFlat4", "VEHICLE4", vtBL4);
                ini.SetValue("TireBackRightFlat4", "VEHICLE4", vtBR4);
                ini.SetValue("VehLocked4", "VEHICLE4", vehLocked4);

                //Veh weps 4
                ini.SetValue("akGuns4", "VEHICLEWEAPONS4", vehWeps4[0]);
                ini.SetValue("m4Guns4", "VEHICLEWEAPONS4", vehWeps4[1]);
                ini.SetValue("glockGuns4", "VEHICLEWEAPONS4", vehWeps4[2]);
                ini.SetValue("deagleGuns4", "VEHICLEWEAPONS4", vehWeps4[3]);
                ini.SetValue("mp5Guns4", "VEHICLEWEAPONS4", vehWeps4[4]);
                ini.SetValue("uziGuns4", "VEHICLEWEAPONS4", vehWeps4[5]);
                ini.SetValue("autoGuns4", "VEHICLEWEAPONS4", vehWeps4[6]);
                ini.SetValue("boltGuns4", "VEHICLEWEAPONS4", vehWeps4[7]);
                ini.SetValue("barettaGuns4", "VEHICLEWEAPONS4", vehWeps4[8]);
                ini.SetValue("pumpGuns4", "VEHICLEWEAPONS4", vehWeps4[9]);
                ini.SetValue("molGuns4", "VEHICLEWEAPONS4", vehWeps4[10]);
                ini.SetValue("grenGuns4", "VEHICLEWEAPONS4", vehWeps4[11]);

                //Veh Ammo 4
                ini.SetValue("akAmmo4", "VEHICLEAMMO4", vA4[0]);
                ini.SetValue("m4Ammo4", "VEHICLEAMMO4", vA4[1]);
                ini.SetValue("glockAmmo4", "VEHICLEAMMO4", vA4[2]);
                ini.SetValue("deagleAmmo4", "VEHICLEAMMO4", vA4[3]);
                ini.SetValue("mp5Ammo4", "VEHICLEAMMO4", vA4[4]);
                ini.SetValue("uziAmmo4", "VEHICLEAMMO4", vA4[5]);
                ini.SetValue("autoAmmo4", "VEHICLEAMMO4", vA4[6]);
                ini.SetValue("boltAmmo4", "VEHICLEAMMO4", vA4[7]);
                ini.SetValue("barettaAmmo4", "VEHICLEAMMO4", vA4[8]);
                ini.SetValue("pumpAmmo4", "VEHICLEAMMO4", vA4[9]);
                ini.SetValue("molAmmo4", "VEHICLEAMMO4", vA4[10]);
                ini.SetValue("grenAmmo4", "VEHICLEAMMO4", vA4[11]);
            ini.Save();
            Game.Console.Print("EverythingCar Data Saved");
            //Game.DisplayText("Vehicles Saved", 5000);
            #endregion
        }
        void LoadDat()
        {
            #region FileLoad
            SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "EverythingCarSaves.ini"));
            ini.Load();
            vehSave1 = ini.GetValueInteger("VehSaved1", "VEHICLE1", vehSave1);
            if (vehSave1 == 1)
            {
                //Vehicle 1
                vehVec1 = ini.GetValueVector3("VehPos1", "VEHICLE1", vehVec1);
                vehHead1 = ini.GetValueFloat("VehHead1", "VEHICLE1", vehHead1);
                indexVehColor1 = ini.GetValueInteger("VehColor1", "VEHICLE1", indexVehColor1);
                vehModel1 = ini.GetValueModel("VehModel1", "VEHICLE1", vehModel1);
                vehHealth1 = ini.GetValueInteger("VehHealth1", "VEHICLE1", vehHealth1);
                vehEngHealth1 = ini.GetValueFloat("VehEngHealth1", "VEHICLE1", vehEngHealth1);
                vEng1 = ini.GetValueBool("EngOn1", "VEHICLE1", vEng1);
                vtFL1 = ini.GetValueBool("TireFrontLeftFlat1", "VEHICLE1", vtFL1);
                vtFR1 = ini.GetValueBool("TireFrontRightFlat1", "VEHICLE1", vtFR1);
                vtBL1 = ini.GetValueBool("TireBackLeftFlat1", "VEHICLE1", vtBL1);
                vtBR1 = ini.GetValueBool("TireBackRightFlat1", "VEHICLE1", vtBR1);
                vehLocked1 = ini.GetValueBool("VehLocked1", "VEHICLE1", vehLocked1);
                //Veh Weps 1
                vehWeps1[0] = ini.GetValueInteger("akGuns1", "VEHICLEWEAPONS1", vehWeps1[0]);
                vehWeps1[1] = ini.GetValueInteger("m4Guns1", "VEHICLEWEAPONS1", vehWeps1[1]);
                vehWeps1[2] = ini.GetValueInteger("glockGuns1", "VEHICLEWEAPONS1", vehWeps1[2]);
                vehWeps1[3] = ini.GetValueInteger("deagleGuns1", "VEHICLEWEAPONS1", vehWeps1[3]);
                vehWeps1[4] = ini.GetValueInteger("mp5Guns1", "VEHICLEWEAPONS1", vehWeps1[4]);
                vehWeps1[5] = ini.GetValueInteger("uziGuns1", "VEHICLEWEAPONS1", vehWeps1[5]);
                vehWeps1[6] = ini.GetValueInteger("autoGuns1", "VEHICLEWEAPONS1", vehWeps1[6]);
                vehWeps1[7] = ini.GetValueInteger("boltGuns1", "VEHICLEWEAPONS1", vehWeps1[7]);
                vehWeps1[8] = ini.GetValueInteger("barettaGuns1", "VEHICLEWEAPONS1", vehWeps1[8]);
                vehWeps1[9] = ini.GetValueInteger("pumpGuns1", "VEHICLEWEAPONS1", vehWeps1[9]);
                vehWeps1[10] = ini.GetValueInteger("molGuns1", "VEHICLEWEAPONS1", vehWeps1[10]);
                vehWeps1[11] = ini.GetValueInteger("grenGuns1", "VEHICLEWEAPONS1", vehWeps1[11]);
                //Veh Ammo1
                vA1[0] = ini.GetValueInteger("akAmmo1", "VEHICLEAMMO1", vA1[0]);
                vA1[1] = ini.GetValueInteger("m4Ammo1", "VEHICLEAMMO1", vA1[1]);
                vA1[2] = ini.GetValueInteger("glockAmmo1", "VEHICLEAMMO1", vA1[2]);
                vA1[3] = ini.GetValueInteger("deagleAmmo1", "VEHICLEAMMO1", vA1[3]);
                vA1[4] = ini.GetValueInteger("mp5Ammo1", "VEHICLEAMMO1", vA1[4]);
                vA1[5] = ini.GetValueInteger("uziAmmo1", "VEHICLEAMMO1", vA1[5]);
                vA1[6] = ini.GetValueInteger("autoAmmo1", "VEHICLEAMMO1", vA1[6]);
                vA1[7] = ini.GetValueInteger("boltAmmo1", "VEHICLEAMMO1", vA1[7]);
                vA1[8] = ini.GetValueInteger("barettaAmmo1", "VEHICLEAMMO1", vA1[8]);
                vA1[9] = ini.GetValueInteger("pumpAmmo1", "VEHICLEAMMO1", vA1[9]);
                vA1[10] = ini.GetValueInteger("molAmmo1", "VEHICLEAMMO1", vA1[10]);
                vA1[11] = ini.GetValueInteger("grenAmmo1", "VEHICLEAMMO1", vA1[11]);
            }
            vehSave2 = ini.GetValueInteger("VehSaved2", "VEHICLE2", vehSave2);
            if (vehSave2 == 1)
            {
                //Vehicle 2
                vehVec2 = ini.GetValueVector3("VehPos2", "VEHICLE2", vehVec2);
                vehHead2 = ini.GetValueFloat("VehHead2", "VEHICLE2", vehHead2);
                indexVehColor2 = ini.GetValueInteger("VehColor2", "VEHICLE2", indexVehColor2);
                vehModel2 = ini.GetValueModel("VehModel2", "VEHICLE2", vehModel2);
                vehHealth2 = ini.GetValueInteger("VehHealth2", "VEHICLE2", vehHealth2);
                vehEngHealth2 = ini.GetValueFloat("VehEngHealth2", "VEHICLE2", vehEngHealth2);
                vEng2 = ini.GetValueBool("EngOn2", "VEHICLE2", vEng2);
                vtFL2 = ini.GetValueBool("TireFrontLeftFlat2", "VEHICLE2", vtFL2);
                vtFR2 = ini.GetValueBool("TireFrontRightFlat2", "VEHICLE2", vtFR2);
                vtBL2 = ini.GetValueBool("TireBackLeftFlat2", "VEHICLE2", vtBL2);
                vtBR2 = ini.GetValueBool("TireBackRightFlat2", "VEHICLE2", vtBR2);
                vehLocked2 = ini.GetValueBool("VehLocked2", "VEHICLE2", vehLocked2);
                //Veh Weps 2
                vehWeps2[0] = ini.GetValueInteger("akGuns2", "VEHICLEWEAPONS2", vehWeps2[0]);
                vehWeps2[1] = ini.GetValueInteger("m4Guns2", "VEHICLEWEAPONS2", vehWeps2[1]);
                vehWeps2[2] = ini.GetValueInteger("glockGuns2", "VEHICLEWEAPONS2", vehWeps2[2]);
                vehWeps2[3] = ini.GetValueInteger("deagleGuns2", "VEHICLEWEAPONS2", vehWeps2[3]);
                vehWeps2[4] = ini.GetValueInteger("mp5Guns2", "VEHICLEWEAPONS2", vehWeps2[4]);
                vehWeps2[5] = ini.GetValueInteger("uziGuns2", "VEHICLEWEAPONS2", vehWeps2[5]);
                vehWeps2[6] = ini.GetValueInteger("autoGuns2", "VEHICLEWEAPONS2", vehWeps2[6]);
                vehWeps2[7] = ini.GetValueInteger("boltGuns2", "VEHICLEWEAPONS2", vehWeps2[7]);
                vehWeps2[8] = ini.GetValueInteger("barettaGuns2", "VEHICLEWEAPONS2", vehWeps2[8]);
                vehWeps2[9] = ini.GetValueInteger("pumpGuns2", "VEHICLEWEAPONS2", vehWeps2[9]);
                vehWeps2[10] = ini.GetValueInteger("molGuns2", "VEHICLEWEAPONS2", vehWeps2[10]);
                vehWeps2[11] = ini.GetValueInteger("grenGuns2", "VEHICLEWEAPONS2", vehWeps2[11]);
                //Veh Ammo2
                vA2[0] = ini.GetValueInteger("akAmmo2", "VEHICLEAMMO2", vA2[0]);
                vA2[1] = ini.GetValueInteger("m4Ammo2", "VEHICLEAMMO2", vA2[1]);
                vA2[2] = ini.GetValueInteger("glockAmmo2", "VEHICLEAMMO2", vA2[2]);
                vA2[3] = ini.GetValueInteger("deagleAmmo2", "VEHICLEAMMO2", vA2[3]);
                vA2[4] = ini.GetValueInteger("mp5Ammo2", "VEHICLEAMMO2", vA2[4]);
                vA2[5] = ini.GetValueInteger("uziAmmo2", "VEHICLEAMMO2", vA2[5]);
                vA2[6] = ini.GetValueInteger("autoAmmo2", "VEHICLEAMMO2", vA2[6]);
                vA2[7] = ini.GetValueInteger("boltAmmo2", "VEHICLEAMMO2", vA2[7]);
                vA2[8] = ini.GetValueInteger("barettaAmmo2", "VEHICLEAMMO2", vA2[8]);
                vA2[9] = ini.GetValueInteger("pumpAmmo2", "VEHICLEAMMO2", vA2[9]);
                vA2[10] = ini.GetValueInteger("molAmmo2", "VEHICLEAMMO2", vA2[10]);
                vA2[11] = ini.GetValueInteger("grenAmmo2", "VEHICLEAMMO2", vA2[11]);
            }
            vehSave3 = ini.GetValueInteger("VehSaved3", "VEHICLE3", vehSave3);
            if (vehSave3 == 1)
            {
                //Vehicle 3
                vehVec3 = ini.GetValueVector3("VehPos3", "VEHICLE3", vehVec3);
                vehHead3 = ini.GetValueFloat("VehHead3", "VEHICLE3", vehHead3);
                indexVehColor3 = ini.GetValueInteger("VehColor3", "VEHICLE3", indexVehColor3);
                vehModel3 = ini.GetValueModel("VehModel3", "VEHICLE3", vehModel3);
                vehHealth3 = ini.GetValueInteger("VehHealth3", "VEHICLE3", vehHealth3);
                vehEngHealth3 = ini.GetValueFloat("VehEngHealth3", "VEHICLE3", vehEngHealth3);
                vEng3 = ini.GetValueBool("EngOn3", "VEHICLE3", vEng3);
                vtFL3 = ini.GetValueBool("TireFrontLeftFlat3", "VEHICLE3", vtFL3);
                vtFR3 = ini.GetValueBool("TireFrontRightFlat3", "VEHICLE3", vtFR3);
                vtBL3 = ini.GetValueBool("TireBackLeftFlat3", "VEHICLE3", vtBL3);
                vtBR3 = ini.GetValueBool("TireBackRightFlat3", "VEHICLE3", vtBR3);
                vehLocked3 = ini.GetValueBool("VehLocked3", "VEHICLE3", vehLocked3);
                //Veh Weps 3
                vehWeps3[0] = ini.GetValueInteger("akGuns3", "VEHICLEWEAPONS3", vehWeps3[0]);
                vehWeps3[1] = ini.GetValueInteger("m4Guns3", "VEHICLEWEAPONS3", vehWeps3[1]);
                vehWeps3[2] = ini.GetValueInteger("glockGuns3", "VEHICLEWEAPONS3", vehWeps3[2]);
                vehWeps3[3] = ini.GetValueInteger("deagleGuns3", "VEHICLEWEAPONS3", vehWeps3[3]);
                vehWeps3[4] = ini.GetValueInteger("mp5Guns3", "VEHICLEWEAPONS3", vehWeps3[4]);
                vehWeps3[5] = ini.GetValueInteger("uziGuns3", "VEHICLEWEAPONS3", vehWeps3[5]);
                vehWeps3[6] = ini.GetValueInteger("autoGuns3", "VEHICLEWEAPONS3", vehWeps3[6]);
                vehWeps3[7] = ini.GetValueInteger("boltGuns3", "VEHICLEWEAPONS3", vehWeps3[7]);
                vehWeps3[8] = ini.GetValueInteger("barettaGuns3", "VEHICLEWEAPONS3", vehWeps3[8]);
                vehWeps3[9] = ini.GetValueInteger("pumpGuns3", "VEHICLEWEAPONS3", vehWeps3[9]);
                vehWeps3[10] = ini.GetValueInteger("molGuns3", "VEHICLEWEAPONS3", vehWeps3[10]);
                vehWeps3[11] = ini.GetValueInteger("grenGuns3", "VEHICLEWEAPONS3", vehWeps3[11]);
                //Veh Ammo3
                vA3[0] = ini.GetValueInteger("akAmmo3", "VEHICLEAMMO3", vA3[0]);
                vA3[1] = ini.GetValueInteger("m4Ammo3", "VEHICLEAMMO3", vA3[1]);
                vA3[2] = ini.GetValueInteger("glockAmmo3", "VEHICLEAMMO3", vA3[2]);
                vA3[3] = ini.GetValueInteger("deagleAmmo3", "VEHICLEAMMO3", vA3[3]);
                vA3[4] = ini.GetValueInteger("mp5Ammo3", "VEHICLEAMMO3", vA3[4]);
                vA3[5] = ini.GetValueInteger("uziAmmo3", "VEHICLEAMMO3", vA3[5]);
                vA3[6] = ini.GetValueInteger("autoAmmo3", "VEHICLEAMMO3", vA3[6]);
                vA3[7] = ini.GetValueInteger("boltAmmo3", "VEHICLEAMMO3", vA3[7]);
                vA3[8] = ini.GetValueInteger("barettaAmmo3", "VEHICLEAMMO3", vA3[8]);
                vA3[9] = ini.GetValueInteger("pumpAmmo3", "VEHICLEAMMO3", vA3[9]);
                vA3[10] = ini.GetValueInteger("molAmmo3", "VEHICLEAMMO3", vA3[10]);
                vA3[11] = ini.GetValueInteger("grenAmmo3", "VEHICLEAMMO3", vA3[11]);
            }
            vehSave4 = ini.GetValueInteger("VehSaved4", "VEHICLE4", vehSave4);
            if (vehSave4 == 1)
            {
                //Vehicle 4
                vehVec4 = ini.GetValueVector3("VehPos4", "VEHICLE4", vehVec4);
                vehHead4 = ini.GetValueFloat("VehHead4", "VEHICLE4", vehHead4);
                indexVehColor4 = ini.GetValueInteger("VehColor4", "VEHICLE4", indexVehColor4);
                vehModel4 = ini.GetValueModel("VehModel4", "VEHICLE4", vehModel4);
                vehHealth4 = ini.GetValueInteger("VehHealth4", "VEHICLE4", vehHealth4);
                vehEngHealth4 = ini.GetValueFloat("VehEngHealth4", "VEHICLE4", vehEngHealth4);
                vEng4 = ini.GetValueBool("EngOn4", "VEHICLE4", vEng4);
                vtFL4 = ini.GetValueBool("TireFrontLeftFlat4", "VEHICLE4", vtFL4);
                vtFR4 = ini.GetValueBool("TireFrontRightFlat4", "VEHICLE4", vtFR4);
                vtBL4 = ini.GetValueBool("TireBackLeftFlat4", "VEHICLE4", vtBL4);
                vtBR4 = ini.GetValueBool("TireBackRightFlat4", "VEHICLE4", vtBR4);
                vehLocked4 = ini.GetValueBool("VehLocked4", "VEHICLE4", vehLocked4);
                //Veh Weps 4
                vehWeps4[0] = ini.GetValueInteger("akGuns4", "VEHICLEWEAPONS4", vehWeps4[0]);
                vehWeps4[1] = ini.GetValueInteger("m4Guns4", "VEHICLEWEAPONS4", vehWeps4[1]);
                vehWeps4[2] = ini.GetValueInteger("glockGuns4", "VEHICLEWEAPONS4", vehWeps4[2]);
                vehWeps4[3] = ini.GetValueInteger("deagleGuns4", "VEHICLEWEAPONS4", vehWeps4[3]);
                vehWeps4[4] = ini.GetValueInteger("mp5Guns4", "VEHICLEWEAPONS4", vehWeps4[4]);
                vehWeps4[5] = ini.GetValueInteger("uziGuns4", "VEHICLEWEAPONS4", vehWeps4[5]);
                vehWeps4[6] = ini.GetValueInteger("autoGuns4", "VEHICLEWEAPONS4", vehWeps4[6]);
                vehWeps4[7] = ini.GetValueInteger("boltGuns4", "VEHICLEWEAPONS4", vehWeps4[7]);
                vehWeps4[8] = ini.GetValueInteger("barettaGuns4", "VEHICLEWEAPONS4", vehWeps4[8]);
                vehWeps4[9] = ini.GetValueInteger("pumpGuns4", "VEHICLEWEAPONS4", vehWeps4[9]);
                vehWeps4[10] = ini.GetValueInteger("molGuns4", "VEHICLEWEAPONS4", vehWeps4[10]);
                vehWeps4[11] = ini.GetValueInteger("grenGuns4", "VEHICLEWEAPONS4", vehWeps4[11]);
                //Veh Ammo4
                vA4[0] = ini.GetValueInteger("akAmmo4", "VEHICLEAMMO4", vA4[0]);
                vA4[1] = ini.GetValueInteger("m4Ammo4", "VEHICLEAMMO4", vA4[1]);
                vA4[2] = ini.GetValueInteger("glockAmmo4", "VEHICLEAMMO4", vA4[2]);
                vA4[3] = ini.GetValueInteger("deagleAmmo4", "VEHICLEAMMO4", vA4[3]);
                vA4[4] = ini.GetValueInteger("mp5Ammo4", "VEHICLEAMMO4", vA4[4]);
                vA4[5] = ini.GetValueInteger("uziAmmo4", "VEHICLEAMMO4", vA4[5]);
                vA4[6] = ini.GetValueInteger("autoAmmo4", "VEHICLEAMMO4", vA4[6]);
                vA4[7] = ini.GetValueInteger("boltAmmo4", "VEHICLEAMMO4", vA4[7]);
                vA4[8] = ini.GetValueInteger("barettaAmmo4", "VEHICLEAMMO4", vA4[8]);
                vA4[9] = ini.GetValueInteger("pumpAmmo4", "VEHICLEAMMO4", vA4[9]);
                vA4[10] = ini.GetValueInteger("molAmmo4", "VEHICLEAMMO4", vA4[10]);
                vA4[11] = ini.GetValueInteger("grenAmmo4", "VEHICLEAMMO4", vA4[11]);
            }
            #endregion
            bool vehLoad1 = true,
                vehLoad2 = true,
                vehLoad3 = true,
                vehLoad4 = true;
            Player[] playerList = Game.PlayerList;
            foreach (var mpPlayer in playerList)
            {
                if (Game.Exists(mpPlayer.Character))
                {

                    if (Game.Exists(mpPlayer.Character) && lp.Character != mpPlayer.Character)
                    {
                        if (Game.Exists(mpPlayer.Character) && vehSave1 == 1 && mpPlayer.Character.Position.DistanceTo(vehVec1) < 200)
                        {
                            vehLoad1 = false;
                        }
                        if (Game.Exists(mpPlayer.Character) && vehSave2 == 1 && mpPlayer.Character.Position.DistanceTo(vehVec2) < 200)
                        {
                            vehLoad2 = false;
                        }
                        if (Game.Exists(mpPlayer.Character) && vehSave3 == 1 && mpPlayer.Character.Position.DistanceTo(vehVec3) < 200)
                        {
                            vehLoad3 = false;
                        }
                        if (Game.Exists(mpPlayer.Character) && vehSave4 == 1 && mpPlayer.Character.Position.DistanceTo(vehVec1) < 200)
                        {
                            vehLoad4 = false;
                        }
                    }
                }
            }
                        // Add index color to vehColor
            if (vehSave1 == 1)
            {
                //Game.DisplayText("Creating Vehicle 1", 2000);
                //Wait(2000);
                vehColor1 = new ColorIndex(indexVehColor1);
                //if (vehLoad1)
                //CreateNewLoadedVehicle(ref vehArray[0], ref vehTemp1, ref vehModel1, ref vehVec1, ref vehColor1, ref vehBlipArray[0], ref vehHead1, ref vehEngHealth1, ref vehHealth1, ref vehSave1, ref posBlip1, ref vEng1, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1, ref vehLocked1, ref mBlipColor1, ref vehStored1);
                //else
                SetGhostTempVehPos(ref vehArray[0], ref vehTemp1, ref vehModel1, ref vehVec1, ref vehColor1, ref vehBlipArray[0], ref vehHead1, ref vehEngHealth1, ref vehHealth1, ref vehSave1, ref posBlip1, ref vEng1, ref vtFL1, ref vtFR1, ref vtBL1, ref vtBR1, ref vehLocked1, ref indexVehColor1, ref mBlipColor1, ref vehStored1, ref bVehVec1);
                    
            }
            if (vehSave2 == 1)
            {
                //Game.DisplayText("Creating Vehicle 2", 2000);
                //Wait(2000);
                vehColor2 = new ColorIndex(indexVehColor2);
                //if (vehLoad2)
               //     CreateNewLoadedVehicle(ref vehArray[1], ref vehTemp2, ref vehModel2, ref vehVec2, ref vehColor2, ref vehBlipArray[1], ref vehHead2, ref vehEngHealth2, ref vehHealth2, ref vehSave2, ref posBlip2, ref vEng2, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2, ref vehLocked2, ref mBlipColor2, ref vehStored2);
               // else
                    SetGhostTempVehPos(ref vehArray[1], ref vehTemp2, ref vehModel2, ref vehVec2, ref vehColor2, ref vehBlipArray[1], ref vehHead2, ref vehEngHealth2, ref vehHealth2, ref vehSave2, ref posBlip2, ref vEng2, ref vtFL2, ref vtFR2, ref vtBL2, ref vtBR2, ref vehLocked2, ref indexVehColor2, ref mBlipColor2, ref vehStored2, ref bVehVec2);
            }
            if (vehSave3 == 1)
            {
                //Game.DisplayText("Creating Vehicle 3", 2000);
                //Wait(2000);
                vehColor3 = new ColorIndex(indexVehColor3);
               // if (vehLoad3)
                //CreateNewLoadedVehicle(ref vehArray[2], ref vehTemp3, ref vehModel3, ref vehVec3, ref vehColor3, ref vehBlipArray[2], ref vehHead3, ref vehEngHealth3, ref vehHealth3, ref vehSave3, ref posBlip3, ref vEng3, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3, ref vehLocked3, ref mBlipColor3, ref vehStored3);
               // else
                    SetGhostTempVehPos(ref vehArray[2], ref vehTemp3, ref vehModel3, ref vehVec3, ref vehColor3, ref vehBlipArray[2], ref vehHead3, ref vehEngHealth3, ref vehHealth3, ref vehSave3, ref posBlip3, ref vEng3, ref vtFL3, ref vtFR3, ref vtBL3, ref vtBR3, ref vehLocked3, ref indexVehColor3, ref mBlipColor3, ref vehStored3, ref bVehVec3);
                    
            }
            if (vehSave4 == 1)
            {
                //Game.DisplayText("Creating Vehicle 4", 2000);
               //Wait(2000);
                vehColor4 = new ColorIndex(indexVehColor4);
               // if (vehLoad4)
               // CreateNewLoadedVehicle(ref vehArray[3], ref vehTemp4, ref vehModel4, ref vehVec4, ref vehColor4, ref vehBlipArray[3], ref vehHead4, ref vehEngHealth4, ref vehHealth4, ref vehSave4, ref posBlip4, ref vEng4, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4, ref vehLocked4, ref mBlipColor4, ref vehStored4);
                //else
                    SetGhostTempVehPos(ref vehArray[3], ref vehTemp4, ref vehModel4, ref vehVec4, ref vehColor4, ref vehBlipArray[3], ref vehHead4, ref vehEngHealth4, ref vehHealth4, ref vehSave4, ref posBlip4, ref vEng4, ref vtFL4, ref vtFR4, ref vtBL4, ref vtBR4, ref vehLocked4, ref indexVehColor4, ref mBlipColor4, ref vehStored4, ref bVehVec4);
                    
            }
            //Wait(3000);
            freshLoad = false;
            Game.DisplayText("EverythingCar Vehicles Loaded", 5000);
        }

        #endregion
        #region PerFrameDraw EC--------
        void EverythingCar_PerFrameDraw(object sender, GraphicsEventArgs e)
        {
            #region SeatBelt Draw
            int xR = 200;
            int yR = 250;
            int widthR = 200;
            int heightR = 25;
            int xBarPosition = (Game.Resolution.Width / 2) + (Game.Resolution.Width / 4) + xScrPos1;    //1100;
            int yBarPosition = (Game.Resolution.Height / 2) + (Game.Resolution.Height / 8) + yScrPos1;  // 550;
            int barWidth = 200;
            int barHeight = 25;
            e.Graphics.Scaling = FontScaling.Pixel;
            Color colorSeatBeltBar = Color.Black;
            Color repairProgBar = Color.Green;
            RectangleF seatBeltBar = new RectangleF(xBarPosition, yBarPosition, barWidth, barHeight);
            RectangleF vehLockBar = new RectangleF(xBarPosition, yBarPosition + 25, barWidth, barHeight);

            if (vehClosest1 == true && Game.Exists(vehArray[0]) && vehArray[0].Position.DistanceTo(lp.Character.Position) <= 15)
            {
                e.Graphics.DrawRectangle(vehLockBar, colorSeatBeltBar);
                if (vehLocked1 == true)
                    e.Graphics.DrawText("Door = LOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOn);
                else
                    e.Graphics.DrawText("Door = UNLOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOff);
            }
            else if (vehClosest2 == true && Game.Exists(vehArray[1]) && vehArray[1].Position.DistanceTo(lp.Character.Position) <= 15)
            {
                e.Graphics.DrawRectangle(vehLockBar, colorSeatBeltBar);
                if (vehLocked2 == true)
                    e.Graphics.DrawText("Door = LOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOn);
                else
                    e.Graphics.DrawText("Door = UNLOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOff);
            }
            else if (vehClosest3 == true && Game.Exists(vehArray[2]) && vehArray[2].Position.DistanceTo(lp.Character.Position) <= 15)
            {
                e.Graphics.DrawRectangle(vehLockBar, colorSeatBeltBar);
                if (vehLocked3 == true)
                    e.Graphics.DrawText("Door = LOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOn);
                else
                    e.Graphics.DrawText("Door = UNLOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOff);
            }
            else if (vehClosest4 == true && Game.Exists(vehArray[3]) && vehArray[3].Position.DistanceTo(lp.Character.Position) <= 15)
            {
                e.Graphics.DrawRectangle(vehLockBar, colorSeatBeltBar);
                if (vehLocked4 == true)
                    e.Graphics.DrawText("Door = LOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOn);
                else
                    e.Graphics.DrawText("Door = UNLOCKED", vehLockBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOff);
            }

            if (lp.Character.isInVehicle())
            {
                e.Graphics.DrawRectangle(seatBeltBar, colorSeatBeltBar);
                if (seatBeltOn == true)
                {
                    e.Graphics.DrawText("SeatBelt = ON", seatBeltBar, TextAlignment.Center| TextAlignment.VerticalCenter, fontSeatBeltOn);
                }
                else
                {
                    e.Graphics.DrawText("SeatBelt = OFF", seatBeltBar, TextAlignment.Center | TextAlignment.VerticalCenter, fontSeatBeltOff);
                }
            }
            #endregion
            #region Vehicle Menu--------

            int xPos = (Game.Resolution.Width / 4);  //200;
            int yPos = (Game.Resolution.Height / 4); //150;
            int width = 1100;
            int height = 50;
            int xPos2 = (Game.Resolution.Width / 2);  //500;
            int yPos2 = (Game.Resolution.Height / 4);  //150;
            RectangleF title = new RectangleF(xPos, yPos, width, height);
            RectangleF m0 = new RectangleF(xPos, yPos + 40, width, height);
            RectangleF m1 = new RectangleF(xPos, yPos + 70, width, height);
            RectangleF m2 = new RectangleF(xPos, yPos + 100, width, height);
            RectangleF m3 = new RectangleF(xPos, yPos + 130, width, height);
            RectangleF m4 = new RectangleF(xPos, yPos + 160, width, height);
            RectangleF m5 = new RectangleF(xPos, yPos + 190, width, height);
            RectangleF m6 = new RectangleF(xPos, yPos + 220, width, height);
            RectangleF m7 = new RectangleF(xPos, yPos + 250, width, height);
            RectangleF m8 = new RectangleF(xPos, yPos + 280, width, height);
            RectangleF m9 = new RectangleF(xPos, yPos + 310, width, height);
            RectangleF m10 = new RectangleF(xPos, yPos + 340, width, height);
            RectangleF m11 = new RectangleF(xPos, yPos + 370, width, height);
            RectangleF m12 = new RectangleF(xPos, yPos + 400, width, height);
            RectangleF m13 = new RectangleF(xPos, yPos + 430, width, height);
            //Rectangles for Locations
            RectangleF p0 = new RectangleF(xPos2, yPos2 + 40, width, height);
            RectangleF p1 = new RectangleF(xPos2, yPos2 + 70, width, height);
            RectangleF p2 = new RectangleF(xPos2, yPos2 + 100, width, height);
            RectangleF p3 = new RectangleF(xPos2, yPos2 + 130, width, height);
            RectangleF p4 = new RectangleF(xPos2, yPos2 + 160, width, height);
            RectangleF p5 = new RectangleF(xPos2, yPos2 + 190, width, height);
            RectangleF p6 = new RectangleF(xPos2, yPos2 + 220, width, height);
            RectangleF p7 = new RectangleF(xPos2, yPos2 + 250, width, height);
            RectangleF p8 = new RectangleF(xPos2, yPos2 + 280, width, height);
            RectangleF p9 = new RectangleF(xPos2, yPos2 + 310, width, height);
            RectangleF p10 = new RectangleF(xPos2, yPos2 + 340, width, height);
            RectangleF p11 = new RectangleF(xPos2, yPos2 + 370, width, height);
            RectangleF p12 = new RectangleF(xPos2, yPos2 + 400, width, height);
            RectangleF p13 = new RectangleF(xPos2, yPos2 + 430, width, height);
            
            #region Main Menu
            if (menuSelectVeh && lp.Character.isAlive)
            {
                e.Graphics.DrawText( lp.Name.ToString() + "'s Saved Vehicles Menu", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                if (selectionVeh == 1)
                {
                    if (vehSave1 == 1)
                    {
                        e.Graphics.DrawText("1) Saved Vehicle 1 (Green Blip)", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                        e.Graphics.DrawText("1) --empty--", m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    if (vehSave1 == 1)
                    {
                        e.Graphics.DrawText("1) Saved Vehicle 1 (Green Blip)", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    else
                        if (vehSave1 == 0)
                            e.Graphics.DrawText("1) --empty--", m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 2)
                {
                    if (vehSave2 == 1)
                    {
                        e.Graphics.DrawText("2) Saved Vehicle 2 (Yellow Blip)", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont); 
                    }
                    else
                        e.Graphics.DrawText("2) --empty--", m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    if (vehSave2 == 1)
                    {
                        e.Graphics.DrawText("2) Saved Vehicle 2 (Yellow Blip)", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    else
                        if (vehSave2 == 0)
                            e.Graphics.DrawText("2) --empty--", m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 3)
                {
                    if (vehSave3 == 1)
                    {
                        e.Graphics.DrawText("3) Saved Vehicle 3 (Purple Blip)", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                        e.Graphics.DrawText("3) --empty--", m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    if (vehSave3 == 1)
                    {
                        e.Graphics.DrawText("3) Saved Vehicle 3 (Purple Blip)", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    else
                        if (vehSave3 == 0)
                            e.Graphics.DrawText("3) --empty--", m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 4)
                {
                    if (vehSave4 == 1)
                    {
                        e.Graphics.DrawText("4) Saved Vehicle 4 (Orange Blip)", m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    }
                    else
                        e.Graphics.DrawText("4) --empty--", m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    if (vehSave4 == 1)
                    {
                        e.Graphics.DrawText("4) Saved Vehicle 4 (Orange Blip)", m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    }
                    else
                        if (vehSave4 == 0)
                            e.Graphics.DrawText("4) --empty--", m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            #endregion
            #region Weapons In Individual Vehicles
            #region MenuVeh1
            if (menuVeh1)
            {
                e.Graphics.DrawText("Saved Vehicle 1 Trunk" , title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                if (selectionVeh == 1)
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 2)
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 3)
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 4)
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 5)
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 6)
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 7)
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 8)
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 9)
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 10)
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 11)
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 12)
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA1[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA1[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
#endregion
            #region MenuVeh2
            if (menuVeh2)
            {
                e.Graphics.DrawText("Saved Vehicle 2 Trunk", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                if (selectionVeh == 1)
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 2)
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 3)
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 4)
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 5)
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 6)
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 7)
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 8)
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 9)
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 10)
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 11)
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 12)
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA2[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA2[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            #endregion
            #region MenuVeh3
            if (menuVeh3)
            {
                e.Graphics.DrawText("Saved Vehicle 3 Trunk", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                if (selectionVeh == 1)
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 2)
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 3)
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 4)
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 5)
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 6)
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 7)
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 8)
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 9)
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 10)
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 11)
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 12)
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA3[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA3[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            #endregion
            #region MenuVeh4
            if (menuVeh4)
            {
                e.Graphics.DrawText("Saved Vehicle 4 Trunk", title, TextAlignment.Left | TextAlignment.VerticalCenter, mainMenuFont);
                if (selectionVeh == 1)
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Glock(s)" + " = " + glockGuns.ToString(), m0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[10].ToString(), p0, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 2)
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Desert Eagle(s)" + " = " + deagleGuns.ToString(), m1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[11].ToString(), p1, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 3)
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("M4(s)" + " = " + m4Guns.ToString(), m2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[5].ToString(), p2, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 4)
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AK(s)" + " = " + akGuns.ToString(), m3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[4].ToString(), p3, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 5)
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("UZI(s)" + " = " + uziGuns.ToString(), m4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[0].ToString(), p4, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 6)
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MP5(s)" + " = " + mp5Guns.ToString(), m5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[1].ToString(), p5, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 7)
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Pump Shotgun(s)" + " = " + pumpGuns.ToString(), m6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[2].ToString(), p6, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 8)
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("Baretta Shotgun(s)" + " = " + barettaGuns.ToString(), m7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[3].ToString(), p7, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 9)
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("AUTO SNIPER(s)" + " = " + autoGuns.ToString(), m8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[7].ToString(), p8, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 10)
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("BOLT SNIPER(s)" + " = " + boltGuns.ToString(), m9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[6].ToString(), p9, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 11)
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("MOLOTOV BELT(s)" + " = " + molGuns.ToString(), m10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[8].ToString(), p10, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
                if (selectionVeh == 12)
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                    e.Graphics.DrawText("Ammo = " + vA4[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, selectionFont);
                }
                else
                {
                    e.Graphics.DrawText("GRENADE BELT(s)" + " = " + grenGuns.ToString(), m11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                    e.Graphics.DrawText("Ammo = " + vA4[9].ToString(), p11, TextAlignment.Left | TextAlignment.VerticalCenter, subMenuFont);
                }
            }
            #endregion
            #endregion
            #endregion
        }
        #endregion
    }
}
       
