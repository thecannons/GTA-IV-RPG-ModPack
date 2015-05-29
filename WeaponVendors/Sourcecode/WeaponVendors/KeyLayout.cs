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
    public class KeyLayout : Script
    {

        int num2 = 0;
        int num3 = 0;
        public static string[] BoughtWeapons = null;
        //----- Declare Ammo Prices Variables
        public int AMMO_GLOCK;
        public int AMMO_DEAGLE;
        public int AMMO_UZI;
        public int AMMO_MP5;
        public int AMMO_PUMP;
        public int AMMO_COMBAT;
        public int AMMO_AK47;
        public int AMMO_M4;
        public int AMMO_BOLT_SNIPER;
        public int AMMO_AUTO_SNIPER;
        public int AMMO_RPG;
        public int ARMOR_PRICE;
        //----- Buy Amount Variables
        public int AMMO_BUY_AMT_RIFLES;
        public int AMMO_BUY_AMT_SMGS;
        public int AMMO_BUY_AMT_SHOTGUNS;
        public int AMMO_BUY_AMT_PISTOLS;
        public int AMMO_BUY_AMT_SNIPERS;
        public int AMMO_BUY_AMT_EXPLOSIVES;
        public int AMMO_BUY_AMT_ROCKETS;
        //--MAX BUY AMMO VARIABLES
        public int MAX_BUY_PISTOLS;
        public int MAX_BUY_SMGS;
        public int MAX_BUY_RIFLES;
        public int MAX_BUY_SHOTGUNS;
        public int MAX_BUY_SNIPERS;
        public int MAX_BUY_EXPLOSIVES;
        public int MAX_BUY_ROCKETS;
        //--Wanted or not?
        
        public static int wepshopnumber = 0;
        public string NewWeapon;
        private Keys Buy;
        private Keys Key_Up;
        private Keys Key_Down;
        private Keys Purchase;
        private Keys Exit;
        private Keys BuyArmor;
        public static bool weaponVendorsEnabled = false;
        int waitModifier;
        public KeyLayout()
        {
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            this.ARMOR_PRICE = Settings.GetValueInteger("ARMOR_PRICE", "AMMOPRICES", 1000);
            this.BuyArmor = Settings.GetValueKey("BUYARMOR", "SETTINGS", Keys.B); 
            this.Purchase = Settings.GetValueKey("PURCHASE", "SETTINGS", Keys.NumPad9);
            this.Exit = Settings.GetValueKey("EXIT", "SETTINGS", Keys.NumPad0);
            this.Buy = Settings.GetValueKey("BUYKEY", "SETTINGS", Keys.B);
            this.Key_Up = Settings.GetValueKey("KEY_UP", "SETTINGS", Keys.NumPad8);
            this.Key_Down = Settings.GetValueKey("KEY_DOWN", "SETTINGS", Keys.NumPad2);
            this.KeyDown += new GTA.KeyEventHandler(BuyKey);
            AMMO_GLOCK = base.Settings.GetValueInteger("AMMO_GLOCK", "AMMOPRICES", 50);
            AMMO_DEAGLE = base.Settings.GetValueInteger("AMMO_DEAGLE", "AMMOPRICES", 80);
            AMMO_UZI = base.Settings.GetValueInteger("AMMO_UZI", "AMMOPRICES", 100);
            AMMO_MP5 = base.Settings.GetValueInteger("AMMO_MP5", "AMMOPRICES", 150);
            AMMO_PUMP = base.Settings.GetValueInteger("AMMO_PUMP", "AMMOPRICES", 100);
            AMMO_COMBAT = base.Settings.GetValueInteger("AMMO_COMBAT", "AMMOPRICES", 150);
            AMMO_AK47 = base.Settings.GetValueInteger("AMMO_AK47", "AMMOPRICES", 200);
            AMMO_M4 = base.Settings.GetValueInteger("AMMO_M4", "AMMOPRICES", 250);
            AMMO_BOLT_SNIPER = base.Settings.GetValueInteger("AMMO_BOLT_SNIPER", "AMMOPRICES", 350);
            AMMO_AUTO_SNIPER = base.Settings.GetValueInteger("AMMO_AUTO_SNIPER", "AMMOPRICES", 500);
            AMMO_RPG = base.Settings.GetValueInteger("AMMO_RPG", "AMMOPRICES", 2500);
            AMMO_BUY_AMT_PISTOLS = base.Settings.GetValueInteger("AMMO_AMT_PISTOLS", "AMMOAMOUNT", 50);
            AMMO_BUY_AMT_SMGS = base.Settings.GetValueInteger("AMMO_AMT_SMGS", "AMMOAMOUNT", 50);
            AMMO_BUY_AMT_RIFLES = base.Settings.GetValueInteger("AMMO_AMT_SMGS", "AMMOAMOUNT", 50);
            AMMO_BUY_AMT_SHOTGUNS = base.Settings.GetValueInteger("AMMO_AMT_SHOTGUNS", "AMMOAMOUNT", 10);
            AMMO_BUY_AMT_SNIPERS = base.Settings.GetValueInteger("AMMO_AMT_SNIPERS", "AMMOAMOUNT", 10);
            AMMO_BUY_AMT_EXPLOSIVES = base.Settings.GetValueInteger("AMMO_AMT_EXPLOSIVES", "AMMOAMOUNT", 1);
            AMMO_BUY_AMT_ROCKETS = base.Settings.GetValueInteger("AMMO_AMT_ROCKETS", "AMMOAMOUNT", 1);
            MAX_BUY_PISTOLS = base.Settings.GetValueInteger("MAX_BUY_PISTOLS", "MAXBUYAMMO", 600);
            MAX_BUY_SMGS = base.Settings.GetValueInteger("MAX_BUY_SMGS", "MAXBUYAMMO", 600);
            MAX_BUY_RIFLES = base.Settings.GetValueInteger("MAX_BUY_RIFLES", "MAXBUYAMMO", 600);
            MAX_BUY_SHOTGUNS = base.Settings.GetValueInteger("MAX_BUY_SHOTGUNS", "MAXBUYAMMO", 80);
            MAX_BUY_SNIPERS = base.Settings.GetValueInteger("MAX_BUY_SNIPERS", "MAXBUYAMMO", 30);
            MAX_BUY_EXPLOSIVES = base.Settings.GetValueInteger("MAX_BUY_EXPLOSIVES", "MAXBUYAMMO", 10);
            MAX_BUY_ROCKETS = base.Settings.GetValueInteger("MAX_BUY_ROCKETS", "MAXBUYAMMO", 3);
 

  

        }
        public void BuyKey(object sender, GTA.KeyEventArgs e)
        {
            string[] AvailableWeaponsInWepShop = null;
            string[] WeaponNameShop = null;
            int[] WeaponPriceShop = null;
            int WeaponsInWepShop = 0;
            Color color = new Color();
            if (wepshopnumber == 1)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop1;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop1;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop1;
                num2 = AvailableWeaponsInWepShop.Length / 20;
                num3 = (AvailableWeaponsInWepShop.Length - (20 * num2)) - 1;
            }

            WeaponNameShop = WeaponVendorsCS.WeaponNameShop1;
            WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop1;
            WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop1;
            color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            if (wepshopnumber == 2)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop2;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop2;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop2;
                num2 = AvailableWeaponsInWepShop.Length / 20;
                num3 = (AvailableWeaponsInWepShop.Length - (20 * num2)) - 1;
            }

            WeaponNameShop = WeaponVendorsCS.WeaponNameShop2;
            WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop2;
            WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop2;
            color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            if (wepshopnumber == 3)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop3;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop3;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop3;
                num2 = AvailableWeaponsInWepShop.Length / 20;
                num3 = (AvailableWeaponsInWepShop.Length - (20 * num2)) - 1;
            }

            WeaponNameShop = WeaponVendorsCS.WeaponNameShop3;
            WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop3;
            WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop3;
            color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            if (wepshopnumber == 4)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop4;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop4;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop4;
                num2 = AvailableWeaponsInWepShop.Length / 20;
                num3 = (AvailableWeaponsInWepShop.Length - (20 * num2)) - 1;
            }

            WeaponNameShop = WeaponVendorsCS.WeaponNameShop4;
            WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop4;
            WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop4;
            color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);


            if (e.Shift && base.isKeyPressed(this.Buy))
            {
                if (weaponVendorsEnabled)
                {
                    if (this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop1) < 3f)
                    {
                        if (Player.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out of my shop!", 5000, 1 });

                        }
                        else if (Player.WantedLevel < 1)
                        {
                            wepshopnumber = 1;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome to Ammo Brothers. What can I target you with?", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press CTRL + B(DEFAULT) to buy Armor!", 20000, 1 });
                            this.Player.CanControlCharacter = false;
                            this.Player.IgnoredByEveryone = true;
                            WeaponVendorsCS.ShowMenues = true;
                            WeaponVendorsCS.ProcessOfBuying = true;
                        }


                    }
                    if (this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop2) < 3f)
                    {
                        if (Player.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out of my shop!", 5000, 1 });

                        }
                        else if (Player.WantedLevel < 1)
                        {
                            wepshopnumber = 2;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome to Dirty Harry's. What can I get you started with?", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press CTRL + B(DEFAULT) to buy Armor!", 20000, 1 });
                            this.Player.CanControlCharacter = false;
                            this.Player.IgnoredByEveryone = true;
                            WeaponVendorsCS.ShowMenues = true;
                            WeaponVendorsCS.ProcessOfBuying = true;
                        }

                    }
                    if (this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop3) < 3f)
                    {
                        if (Player.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out of my shop!", 5000, 1 });

                        }
                        else if (Player.WantedLevel < 1)
                        {
                            wepshopnumber = 3;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Welcome to Liberty Steel. We are Ron Paul ceritfied!", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press CTRL + B(DEFAULT) to buy Armor!", 20000, 1 });
                            this.Player.CanControlCharacter = false;
                            this.Player.IgnoredByEveryone = true;
                            WeaponVendorsCS.ShowMenues = true;
                            WeaponVendorsCS.ProcessOfBuying = true;
                        }
                    }
                    if (this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop4s) < 3f)
                    {
                        if (Player.WantedLevel > 0)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Your wanted by police! Get out of my shop!", 5000, 1 });

                        }
                        else if (Player.WantedLevel < 1)
                        {
                            wepshopnumber = 4;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Hey, make it quick, this isnt exactly legal...", 5000, 1 });
                            Wait(1000);
                            Game.FadeScreenOut(500);
                            Wait(1000);
                            Game.FadeScreenIn(500);
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press CTRL + B(DEFAULT) to buy Armor!", 20000, 1 });
                            this.Player.CanControlCharacter = false;
                            this.Player.IgnoredByEveryone = true;
                            WeaponVendorsCS.ShowMenues = true;
                            WeaponVendorsCS.ProcessOfBuying = true;
                        }
                    }
                }
                else
                {
                    if (this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop1) < 3f || this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop2) < 3f || this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop3) < 3f || this.Player.Character.Position.DistanceTo2D(WeaponVendorsCS.wepShop4s) < 3f)
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Weapon Vendors are not enabled, to do this, press SHIFT+G", 20000, 1 });
                    }
                }
            }

            if (e.Control && base.isKeyPressed(this.BuyArmor) && WeaponVendorsCS.ProcessOfBuying)
            {
                if (Player.Money < ARMOR_PRICE)
                {
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 5000, 1 });
                }
                if (Player.Character.Armor >= 100)
                {
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You already have a vest!", 5000, 1 });


                }
                if (Player.Character.Armor < 100 && Player.Money >= ARMOR_PRICE)
                {
                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your Vest, thatll be $1000", 5000, 1 });
                    Player.Money -= ARMOR_PRICE;
                    Player.Character.Armor = 100;
                }
                
            }


            if (e.Key == Exit && WeaponVendorsCS.ProcessOfBuying)
            {
                Game.FadeScreenOut(500);
                Wait(1000);
                Game.FadeScreenIn(500);
                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Have a nice day!", 3000, 1 });
                this.Player.CanControlCharacter = true;
                this.Player.IgnoredByEveryone = false;
                WeaponVendorsCS.ShowMenues = false;
                wepshopnumber = 0;
                WeaponVendorsCS.ProcessOfBuying = false;
                
            }

            if (e.Key == Key_Down)
            {

                if ((GunMenu.pages == num2) && (GunMenu.selectedmenu == num3))
                {
                    GunMenu.selectedmenu = 0;
                }
                else if (GunMenu.selectedmenu == 0x13)
                {
                    GunMenu.selectedmenu = 0;
                }
                else
                {
                    GunMenu.selectedmenu++;
                }
            }
            if (e.Key == Key_Up)
            {

                if (GunMenu.selectedmenu != 0)
                {
                    GunMenu.selectedmenu--;
                }
                else if (GunMenu.pages == num2)
                {
                    GunMenu.selectedmenu = num3;
                }
                else
                {
                    GunMenu.selectedmenu = 0x13;
                }
            }
            if (e.Key == Purchase && WeaponVendorsCS.ProcessOfBuying)
            {
               if (base.Player.Money < WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)])
                {

                    Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You dont have enough money!", 0xfa0, 1 });
                }
                else if (Player.Money > WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)])
                {
                    NewWeapon = AvailableWeaponsInWepShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                    Wait(500);
                    
                    if (NewWeapon == "AssaultRifle_M4")
                    {
                        if (Player.Character.Weapons.AssaultRifle_M4.Ammo >= MAX_BUY_RIFLES)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Rifle_M4);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.AssaultRifle_M4.isPresent && Player.Character.Weapons.AssaultRifle_M4.Ammo <= MAX_BUY_RIFLES)
                        {
                            
                            Player.Money -= AMMO_M4;
                            Player.Character.Weapons.AssaultRifle_M4.Ammo += AMMO_BUY_AMT_RIFLES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your M4", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Rifle_M4);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                            
                        }
                        if (Player.Character.Weapons.AssaultRifle_M4.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.AssaultRifle_M4.Ammo = 60;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Thats a sweet M4 Assault Rifle.", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "AssaultRifle_AK47")
                    {
                        if (Player.Character.Weapons.AssaultRifle_AK47.Ammo >= MAX_BUY_RIFLES)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Rifle_AK47);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.AssaultRifle_AK47.isPresent && Player.Character.Weapons.AssaultRifle_AK47.Ammo <= MAX_BUY_RIFLES)
                        {
                            Player.Money -= AMMO_AK47;
                            Player.Character.Weapons.AssaultRifle_AK47.Ammo += AMMO_BUY_AMT_RIFLES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your AK47", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Rifle_AK47);
                            Wait(300);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.AssaultRifle_AK47.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.AssaultRifle_AK47.Ammo = 60;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Gotta love the AK.", 0xfa0, 1 });
                        }
                    }
                    
                    if (NewWeapon == "MP5")
                    {
                        if (Player.Character.Weapons.MP5.Ammo >= MAX_BUY_SMGS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SMG_MP5);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.MP5.isPresent && Player.Character.Weapons.MP5.Ammo <= MAX_BUY_SMGS)
                        {
                            Player.Money -= AMMO_MP5;
                            Player.Character.Weapons.MP5.Ammo += AMMO_BUY_AMT_SMGS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your MP5", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SMG_MP5);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                            
                        }
                        if (Player.Character.Weapons.MP5.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.MP5.Ammo = 60;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Thats a sweet piece!", 0xfa0, 1 });
                        }

                    }

                    if (NewWeapon == "Uzi")
                    {
                        if (Player.Character.Weapons.Uzi.Ammo >= MAX_BUY_SMGS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SMG_Uzi);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Uzi.isPresent && Player.Character.Weapons.Uzi.Ammo <= MAX_BUY_SMGS)
                        {

                            Player.Money -= AMMO_UZI;
                            Player.Character.Weapons.Uzi.Ammo += AMMO_BUY_AMT_SMGS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your Uzi", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SMG_Uzi);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Uzi.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.Uzi.Ammo = 100;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Careful with that toy!", 0xfa0, 1 });
                        }

                    }

                    if (NewWeapon == "DesertEagle")
                    {
                        if (Player.Character.Weapons.DesertEagle.Ammo >= MAX_BUY_PISTOLS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Handgun_DesertEagle);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.DesertEagle.isPresent && Player.Character.Weapons.DesertEagle.Ammo <= MAX_BUY_PISTOLS)
                        {
;
                            Player.Money -= AMMO_DEAGLE;
                            Player.Character.Weapons.DesertEagle.Ammo += AMMO_BUY_AMT_PISTOLS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your Desert Eagle", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Handgun_DesertEagle);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.DesertEagle.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.DesertEagle.Ammo = 28;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "It packs a punch, be careful.", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "Glock")
                    {
                        if (Player.Character.Weapons.Glock.Ammo >= MAX_BUY_PISTOLS)
                        {

                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Handgun_Glock);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Glock.isPresent && Player.Character.Weapons.Glock.Ammo <= MAX_BUY_PISTOLS)
                        {

                            Player.Money -= AMMO_GLOCK;
                            Player.Character.Weapons.Glock.Ammo += AMMO_BUY_AMT_PISTOLS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your Glock", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Handgun_Glock);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Glock.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.Glock.Ammo = 34;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Relibale in every encounter.", 0xfa0, 1 });
                        }
                    }
                    if (NewWeapon == "BasicSniperRifle")
                    {
                        if (Player.Character.Weapons.SniperRifle_M40A1.Ammo >= MAX_BUY_SNIPERS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SniperRifle_M40A1);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.SniperRifle_M40A1.isPresent && Player.Character.Weapons.SniperRifle_M40A1.Ammo <= MAX_BUY_SNIPERS)
                        {
                            Player.Money -= AMMO_AUTO_SNIPER;
                            Player.Character.Weapons.SniperRifle_M40A1.Ammo += AMMO_BUY_AMT_SNIPERS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your automatic sniper rifle", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SniperRifle_M40A1);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.SniperRifle_M40A1.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.SniperRifle_M40A1.Ammo = 20;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "What ya need this for?  The president?", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "SniperRifle_M40A1")
                    {
                        if (Player.Character.Weapons.BasicSniperRifle.Ammo >= MAX_BUY_SNIPERS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SniperRifle_Basic);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BasicSniperRifle.isPresent && Player.Character.Weapons.BasicSniperRifle.Ammo <= MAX_BUY_SNIPERS)
                        {
                            Player.Money -= AMMO_BOLT_SNIPER;
                            Player.Character.Weapons.BasicSniperRifle.Ammo += AMMO_BUY_AMT_SNIPERS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your bolt-action Sniper Rifle", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.SniperRifle_Basic);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BasicSniperRifle.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.BasicSniperRifle.Ammo = 10;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You going deer hunting?", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "MolotovCocktails")
                    {
                        if (Player.Character.Weapons.MolotovCocktails.Ammo >= MAX_BUY_EXPLOSIVES)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Thrown_Molotov);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.MolotovCocktails.isPresent && Player.Character.Weapons.MolotovCocktails.Ammo <= MAX_BUY_EXPLOSIVES)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.MolotovCocktails.Ammo += AMMO_BUY_AMT_EXPLOSIVES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your extra Molotov Cocktail", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Thrown_Molotov);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.MolotovCocktails.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.MolotovCocktails.Ammo = AMMO_BUY_AMT_EXPLOSIVES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Careful when handling those, they are heavy!", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "Grenades")
                    {
                        if (Player.Character.Weapons.Grenades.Ammo >= MAX_BUY_EXPLOSIVES)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Thrown_Grenade);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Grenades.isPresent && Player.Character.Weapons.Grenades.Ammo <= MAX_BUY_EXPLOSIVES)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.Grenades.Ammo += AMMO_BUY_AMT_EXPLOSIVES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your extra Grenade", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Thrown_Grenade);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Grenades.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.Grenades.Ammo = AMMO_BUY_AMT_EXPLOSIVES;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Don't pull the pin until your ready!", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "BasicShotgun")
                    {
                        if (Player.Character.Weapons.BasicShotgun.Ammo >= MAX_BUY_SHOTGUNS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Shotgun_Basic);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BasicShotgun.isPresent && Player.Character.Weapons.BasicShotgun.Ammo <= MAX_BUY_SHOTGUNS)
                        {
                            Player.Money -= AMMO_PUMP;
                            Player.Character.Weapons.BasicShotgun.Ammo += AMMO_BUY_AMT_SHOTGUNS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your pump-action shotgun", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Shotgun_Basic);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BasicShotgun.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.BasicShotgun.Ammo = 20;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the perfect in home defense system.", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "BarettaShotgun")
                    {
                        if (Player.Character.Weapons.BarettaShotgun.Ammo >= MAX_BUY_SHOTGUNS)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "That is the maximum amount of this ammo that we will sell you.", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Shotgun_Baretta);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BarettaShotgun.isPresent && Player.Character.Weapons.BarettaShotgun.Ammo <= MAX_BUY_SHOTGUNS)
                        {
                            Player.Money -= AMMO_COMBAT;
                            Player.Character.Weapons.BarettaShotgun.Ammo += AMMO_BUY_AMT_SHOTGUNS;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Heres your ammo for your automatic shotgun", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Shotgun_Baretta);
                            Wait(500);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                            Wait(1000);
                            Function.Call("DISPLAY_CASH", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BarettaShotgun.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.BarettaShotgun.Ammo = 20;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Nothing sweeter than an automatic shotgun.", 0xfa0, 1 });
                        }
                    }

                    if (NewWeapon == "BaseballBat")
                    {
                        if (Player.Character.Weapons.BaseballBat.isPresent)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You already have one!", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Melee_BaseballBat);
                            Wait(300);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.BaseballBat.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.BaseballBat.Ammo = 1;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "This will take down anyone with one swing", 0xfa0, 1 });
                        }
                    }
                    if (NewWeapon == "Knife")
                    {
                        if (Player.Character.Weapons.Knife.isPresent)
                        {
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "You already have one!", 0xfa0, 1 });
                            Player.Character.Task.SwapWeapon(Weapon.Melee_Knife);
                            Wait(300);
                            Function.Call("DISPLAY_AMMO", new Parameter[] { 1 });
                        }
                        if (Player.Character.Weapons.Knife.Ammo <= 0)
                        {
                            Player.Money -= WeaponPriceShop[GunMenu.selectedmenu + (GunMenu.pages * 20)];
                            Player.Character.Weapons.Knife.Ammo = 1;
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "It'll cut through almost anything.", 0xfa0, 1 });
                        }

                    }
                    if (Player.Money >= ARMOR_PRICE && WeaponVendorsCS.ProcessOfBuying && Player.Character.Armor < 100)
                    {
                        Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Press CTRL + B(DEFAULT) to buy Armor!", 25000, 1 });
                    }
                }
            }

        }
    }
}

