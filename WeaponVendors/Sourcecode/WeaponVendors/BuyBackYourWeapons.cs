using System;
using System.Windows.Forms;
using GTA;

public class BuyBackYourWeapons : Script {
    int counter = 0;
	bool deadflag;
    int AMMO_Uzi = 0;
    int AMMO_SniperRifle_M40A1 = 0;
    int AMMO_RocketLauncher = 0;
	int AMMO_MP5 = 0;
	int AMMO_MolotovCocktails = 0;
	int AMMO_Knife = 0;
	int AMMO_Grenades = 0;
	int AMMO_Glock = 0;
	int AMMO_DesertEagle = 0;
	int AMMO_BasicSniperRifle = 0;
	int AMMO_BasicShotgun = 0;
	int AMMO_BaseballBat = 0;
	int AMMO_BarettaShotgun = 0;
	int AMMO_AssaultRifle_M4 = 0;
	int AMMO_AssaultRifle_AK47 = 0;


   public BuyBackYourWeapons() {
	  this.Tick += new EventHandler(this.BuyBackYourWeapons_Tick);
   }
   
      private void BuyBackYourWeapons_Tick(object sender, EventArgs e) {
         if ((Player.Character.Health <= 0) && (deadflag == false) || (GTA.Native.Function.Call<bool>("IS_PLAYER_BEING_ARRESTED") && (deadflag == false))) {
	        AMMO_DesertEagle = Player.Character.Weapons.DesertEagle.Ammo;
			AMMO_Uzi = Player.Character.Weapons.Uzi.Ammo;
			AMMO_SniperRifle_M40A1 = Player.Character.Weapons.SniperRifle_M40A1.Ammo;
			AMMO_RocketLauncher = Player.Character.Weapons.RocketLauncher.Ammo;
			AMMO_MP5 = Player.Character.Weapons.MP5.Ammo;
			AMMO_MolotovCocktails = Player.Character.Weapons.MolotovCocktails.Ammo;
			AMMO_Knife = Player.Character.Weapons.Knife.Ammo;
			AMMO_Grenades = Player.Character.Weapons.Grenades.Ammo;
			AMMO_Glock = Player.Character.Weapons.Glock.Ammo;
			AMMO_DesertEagle = Player.Character.Weapons.DesertEagle.Ammo;
			AMMO_BasicSniperRifle = Player.Character.Weapons.BasicSniperRifle.Ammo;
			AMMO_BasicShotgun = Player.Character.Weapons.BasicShotgun.Ammo;
			AMMO_BaseballBat = Player.Character.Weapons.BaseballBat.Ammo;
			AMMO_BarettaShotgun = Player.Character.Weapons.BarettaShotgun.Ammo;
			AMMO_AssaultRifle_M4 = Player.Character.Weapons.AssaultRifle_M4.Ammo;
			AMMO_AssaultRifle_AK47 = Player.Character.Weapons.AssaultRifle_AK47.Ammo;
		  counter = 0;
		  deadflag = true;
		  GTA.Native.Function.Call("REMOVE_ALL_CHAR_WEAPONS", Player.Character);
         }
		 
		 if ((deadflag == true) && (Player.Character.isAliveAndWell) && (!GTA.Native.Function.Call<bool>("IS_CHAR_IN_WATER", Player.Character)) && (!GTA.Native.Function.Call<bool>("IS_PLAYER_BEING_ARRESTED")) && (counter < 330) && (Player.WantedLevel==0)) {
            Game.DisplayText("Pay $2000 bribe to get back your weapons? Press Y to buy.");
			this.KeyDown += new GTA.KeyEventHandler(this.BuyBackYourWeapons_KeyDown);
			counter += 1;
		 }
		 
		 if (counter >= 329) deadflag = false;
		 

		 
      }

   private void BuyBackYourWeapons_KeyDown(object sender, GTA.KeyEventArgs e) {
        if (e.Key == Keys.Y && deadflag == true && counter < 330) {
		
if ((AMMO_Uzi == 0) && (AMMO_SniperRifle_M40A1 == 0) && (AMMO_RocketLauncher == 0) && (AMMO_MP5 == 0) && (AMMO_MolotovCocktails == 0) && (AMMO_Knife == 0) && (AMMO_Grenades == 0) && (AMMO_Glock == 0) && (AMMO_DesertEagle == 0) && (AMMO_BasicSniperRifle == 0) && (AMMO_BasicShotgun == 0) && (AMMO_BaseballBat == 0) && (AMMO_BarettaShotgun == 0) && (AMMO_AssaultRifle_M4 == 0) && (AMMO_AssaultRifle_AK47 == 0)) { 
Game.DisplayText("You don't have any weapons to return");
deadflag = false;
}

else {
		
if (AMMO_Uzi != 0)    Player.Character.Weapons.Uzi.Ammo = AMMO_Uzi;
if (AMMO_SniperRifle_M40A1 != 0)    Player.Character.Weapons.SniperRifle_M40A1.Ammo = AMMO_SniperRifle_M40A1;
if (AMMO_RocketLauncher != 0)    Player.Character.Weapons.RocketLauncher.Ammo = AMMO_RocketLauncher;
if (AMMO_MP5 != 0)    Player.Character.Weapons.MP5.Ammo = AMMO_MP5;
if (AMMO_MolotovCocktails != 0)    Player.Character.Weapons.MolotovCocktails.Ammo = AMMO_MolotovCocktails;
if (AMMO_Knife != 0)    Player.Character.Weapons.Knife.Ammo = AMMO_Knife;
if (AMMO_Grenades != 0)    Player.Character.Weapons.Grenades.Ammo = AMMO_Grenades;
if (AMMO_Glock != 0)    Player.Character.Weapons.Glock.Ammo = AMMO_Glock;
if (AMMO_DesertEagle != 0)    Player.Character.Weapons.DesertEagle.Ammo = AMMO_DesertEagle;
if (AMMO_BasicSniperRifle != 0)    Player.Character.Weapons.BasicSniperRifle.Ammo = AMMO_BasicSniperRifle;
if (AMMO_BasicShotgun != 0)    Player.Character.Weapons.BasicShotgun.Ammo = AMMO_BasicShotgun;
if (AMMO_BaseballBat != 0)    Player.Character.Weapons.BaseballBat.Ammo = AMMO_BaseballBat;
if (AMMO_BarettaShotgun != 0)    Player.Character.Weapons.BarettaShotgun.Ammo = AMMO_BarettaShotgun;
if (AMMO_AssaultRifle_M4 != 0)    Player.Character.Weapons.AssaultRifle_M4.Ammo = AMMO_AssaultRifle_M4;
if (AMMO_AssaultRifle_AK47 != 0)    Player.Character.Weapons.AssaultRifle_AK47.Ammo = AMMO_AssaultRifle_AK47;

Game.DisplayText("Weapons returned");
Player.Money -= 2000;
   deadflag = false;
   }
					         }
   }


}