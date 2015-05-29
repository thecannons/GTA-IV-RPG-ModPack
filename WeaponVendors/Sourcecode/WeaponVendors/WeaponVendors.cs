using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using System.Drawing;


namespace WeaponVendors
{
    public class WeaponVendorsCS : Script
    {

        public static Vector3 wepShop1 = new Vector3(-1335.88f, 314.26f, 13.62f);
        public static Vector3 wepShop2 = new Vector3(78.33f, -339.48f, 10.17f);
        public static Vector3 wepShop3 = new Vector3(1062.77f, 89.70f, 33.25f);
        public static Vector3 wepShop4s = new Vector3(1198.69f, 1695.95f, 16.73f);
        public static int WeaponsInWepShop1 = 13;
        public static int WeaponsInWepShop2 = 13;
        public static int WeaponsInWepShop3 = 13;
        public static int WeaponsInWepShop4 = 13;
        public static string[] AvailableWeaponsInWepShop1 = null;
        public static string[] AvailableWeaponsInWepShop2 = null;
        public static string[] AvailableWeaponsInWepShop3 = null;
        public static string[] AvailableWeaponsInWepShop4 = null;
        public static string[] WeaponNameShop1 = null;
        public static string[] WeaponNameShop2 = null;
        public static string[] WeaponNameShop3 = null;
        public static string[] WeaponNameShop4 = null;
        public static int[] WeaponPriceShop1 = null;
        public static int[] WeaponPriceShop2 = null;
        public static int[] WeaponPriceShop3 = null;
        public static int[] WeaponPriceShop4 = null;
        public static bool ShowMenues = false;

        public static int MenuTransparency = 50;
        public static Color MenuColorW1 = new Color();
        public static Color MenuColorW2 = new Color();
        public static Color MenuColorW3 = new Color();
        public static Color MenuColorW4 = new Color();
        public static bool ProcessOfBuying = false;
        public Checkpoint StoreClerk1;
        public Checkpoint StoreClerk2;
        public Checkpoint StoreClerk3;
        public Checkpoint StoreClerk4;
        int waitModifier;





        public WeaponVendorsCS()
        {
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 5000);

            this.Tick += new EventHandler(ShopCheckTick);
            MenuColorW1 = Color.FromName(base.Settings.GetValueString("MENUCOLOR_SHOP1", "SETTINGS", "DarkRed"));
            MenuColorW2 = Color.FromName(base.Settings.GetValueString("MENUCOLOR_SHOP2", "SETTINGS", "MidnightBlue"));
            MenuColorW3 = Color.FromName(base.Settings.GetValueString("MENUCOLOR_SHOP3", "SETTINGS", "Orangered"));
            MenuColorW4 = Color.FromName(base.Settings.GetValueString("MENUCOLOR_SHOP4", "SETTINGS", "Black"));
            BindKey(Keys.G, true, false, false, EnableWeaponVendors);
            int index = 0;
            string str;
            string str2;
            string str3;
            WeaponsInWepShop1 = base.Settings.GetValueInteger("WEAPONCOUNT", "WEAPONSHOP1", 14);
            AvailableWeaponsInWepShop1 = new string[WeaponsInWepShop1];
            WeaponNameShop1 = new string[WeaponsInWepShop1];
            WeaponPriceShop1 = new int[WeaponsInWepShop1];

            while (index < WeaponsInWepShop1)
            {
                str = "WEAPON" + (index + 1);
                str2 = "WEAPON" + (index + 1) + "NAME";
                str3 = "WEAPON" + (index + 1) + "PRICE";
                AvailableWeaponsInWepShop1[index] = base.Settings.GetValueString(str, "WEAPONSHOP1", "NONE");
                WeaponNameShop1[index] = base.Settings.GetValueString(str2, "WEAPONSHOP1", AvailableWeaponsInWepShop1[index]);
                WeaponPriceShop1[index] = base.Settings.GetValueInteger(str3, "WEAPONSHOP1", 1);
                index++;

            }
            WeaponsInWepShop2 = base.Settings.GetValueInteger("WEAPONCOUNT", "WEAPONSHOP2", 14);
            AvailableWeaponsInWepShop2 = new string[WeaponsInWepShop2];
            WeaponNameShop2 = new string[WeaponsInWepShop2];
            WeaponPriceShop2 = new int[WeaponsInWepShop2];

            for (index = 0; index < WeaponsInWepShop2; index++)
            {
                str = "WEAPON" + (index + 1);
                str2 = "WEAPON" + (index + 1) + "NAME";
                str3 = "WEAPON" + (index + 1) + "PRICE";
                AvailableWeaponsInWepShop2[index] = base.Settings.GetValueString(str, "WEAPONSHOP2", "NONE");
                WeaponNameShop2[index] = base.Settings.GetValueString(str2, "WEAPONSHOP2", AvailableWeaponsInWepShop1[index]);
                WeaponPriceShop2[index] = base.Settings.GetValueInteger(str3, "WEAPONSHOP2", 1);

            }
            WeaponsInWepShop3 = base.Settings.GetValueInteger("WEAPONCOUNT", "WEAPONSHOP3", 14);
            AvailableWeaponsInWepShop3 = new string[WeaponsInWepShop3];
            WeaponNameShop3 = new string[WeaponsInWepShop3];
            WeaponPriceShop3 = new int[WeaponsInWepShop3];

            for (index = 0; index < WeaponsInWepShop3; index++)
            {
                str = "WEAPON" + (index + 1);
                str2 = "WEAPON" + (index + 1) + "NAME";
                str3 = "WEAPON" + (index + 1) + "PRICE";
                AvailableWeaponsInWepShop3[index] = base.Settings.GetValueString(str, "WEAPONSHOP3", "NONE");
                WeaponNameShop3[index] = base.Settings.GetValueString(str2, "WEAPONSHOP3", AvailableWeaponsInWepShop1[index]);
                WeaponPriceShop3[index] = base.Settings.GetValueInteger(str3, "WEAPONSHOP3", 1);

            }
            WeaponsInWepShop4 = base.Settings.GetValueInteger("WEAPONCOUNT", "WEAPONSHOP4", 14);
            AvailableWeaponsInWepShop4 = new string[WeaponsInWepShop4];
            WeaponNameShop4 = new string[WeaponsInWepShop4];
            WeaponPriceShop4 = new int[WeaponsInWepShop4];

            for (index = 0; index < WeaponsInWepShop4; index++)
            {
                str = "WEAPON" + (index + 1);
                str2 = "WEAPON" + (index + 1) + "NAME";
                str3 = "WEAPON" + (index + 1) + "PRICE";
                AvailableWeaponsInWepShop4[index] = base.Settings.GetValueString(str, "WEAPONSHOP4", "NONE");
                WeaponNameShop4[index] = base.Settings.GetValueString(str2, "WEAPONSHOP4", AvailableWeaponsInWepShop1[index]);
                WeaponPriceShop4[index] = base.Settings.GetValueInteger(str3, "WEAPONSHOP4", 1);

            }

            //Spawn Clerks and Blips:
            Wait(500);

            Blip BlipShop1 = Blip.AddBlip(wepShop1);
            BlipShop1.Icon = BlipIcon.Building_WeaponShop;
            BlipShop1.Name = "Ammo Brothers";
            BlipShop1.ShowOnlyWhenNear = true;
            BlipShop1.Display = BlipDisplay.ArrowAndMap;

            Wait(500);

            Blip BlipShop2 = Blip.AddBlip(wepShop2);
            BlipShop2.Icon = BlipIcon.Building_WeaponShop;
            BlipShop2.Name = "Dirty Harry's Gun Depot";
            BlipShop2.ShowOnlyWhenNear = true;
            BlipShop2.Display = BlipDisplay.ArrowAndMap;

            Wait(500);

            Blip BlipShop3 = Blip.AddBlip(wepShop3);
            BlipShop3.Icon = BlipIcon.Building_WeaponShop;
            BlipShop3.Name = "Liberty Steel Weapons";
            BlipShop3.ShowOnlyWhenNear = true;
            BlipShop3.Display = BlipDisplay.ArrowAndMap;

            Wait(500);


            Blip BlipShop4 = Blip.AddBlip(wepShop4s);
            BlipShop4.Icon = BlipIcon.Building_WeaponShop;
            BlipShop4.Name = "StripTastic Underground";
            BlipShop4.ShowOnlyWhenNear = true;
            BlipShop4.Display = BlipDisplay.ArrowAndMap;

            Wait(500);
        }
        public void ShopCheckTick(object sender, EventArgs e)
        {
            if (ProcessOfBuying && Player.WantedLevel > 0 || Player.Character.isDead && ProcessOfBuying)
            {
                Game.FadeScreenOut(500);
                Game.FadeScreenIn(500);
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "I dont serve dead criminals!", 3000, 1 });
                this.Player.CanControlCharacter = true;
                this.Player.IgnoredByEveryone = false;
                WeaponVendorsCS.ShowMenues = false;
                KeyLayout.wepshopnumber = 0;
                WeaponVendorsCS.ProcessOfBuying = false;
            }
            if (this.Player.Character.Position.DistanceTo2D(wepShop1) < 3f && !ProcessOfBuying || this.Player.Character.Position.DistanceTo2D(wepShop2) < 3f && !ProcessOfBuying || this.Player.Character.Position.DistanceTo2D(wepShop3) < 3f && !ProcessOfBuying || this.Player.Character.Position.DistanceTo2D(wepShop4s) < 3f && !ProcessOfBuying)
            {
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press SHIFT+B (DEFAULT) to open store window", 10000, 1 });
                  
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
        void EnableWeaponVendors()
        {
            if (!KeyLayout.weaponVendorsEnabled)
            {
                KeyLayout.weaponVendorsEnabled = true;
                checkpointBuild(StoreClerk1, wepShop1, 2.0f, Color.DarkOrange, true);
                checkpointBuild(StoreClerk2, wepShop2, 2.0f, Color.DarkOrange, true);
                checkpointBuild(StoreClerk3, wepShop3, 2.0f, Color.DarkOrange, true);
                checkpointBuild(StoreClerk4, wepShop4s, 2.0f, Color.DarkOrange, true);
            }
            else
            {
                Game.DisplayText("Weapon vendors already enabled!");
            }
        }

    }
}


