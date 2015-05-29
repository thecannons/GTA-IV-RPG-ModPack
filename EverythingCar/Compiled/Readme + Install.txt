----------------------------------------------------
-	EverythingCar
-	Mod by: Daimyo21
-	contact: daimyo21mods@gmail.com
-	Works in Multiplayer and Singleplayer
----------------------------------------------------

Changelog v1.2b
-New:
	*Ability to change ammo transfer amount (default RShiftKey + left/right arrow)
	*Customizable vehicle display menu controls are now shown in INI file
-Fixes:
	*Changing ammo transfer to greater than 10 would bug out when placing ammo in vehicle, allowing you to dupe ammo
	*EverythingCarSaves.INI save file was showing improper weapons/ammo, this should now be fixed
	*Other fixes/adjustments to the code
-Known Issue
	*When storing grenades/molotovs, the vehicle display menu sometimes shows all stored weapons at 0(ammo shown is fine), extracting ammo/weapons or removing grenades/molotovs fixes this.


Changelog v1.1b

-Fixed cars not spawning in singleplayer on load (this was already fixed, uploaded wrong file)
-Fixed INI control for placing weapon in trunk, was CTRL + T, now its Shift + T

EverythingCar is a script that adds more functionality and depth to cars while making 
cars persistent in multiplayer (should work in singleplayer just the same, report otherwise!)

This script was originally intended to add depth for LCPDFR policing vehicles but can be used for anything

Features:

-Save up to 4 cars in multiplayer(or SP) that will save weapons/ammo/damage persistently and automatically (see INI file for controls)
-Seatbelt on/off so you wont fly out the window during crashes
-Lock doors/Set Alarm in and out of the vehicle to avoid those darn thieves! (broken window sets off alarm but makes that certain door unlocked)
-Store weapons and ammo in vehicle (this is great if using the weapon weight mod!)
-Openable trunk/hood within or outside of vehicle (my PvPWanted script syncs this feature in multiplayer)
-Repair engine and tires of vehicle, and body for a fee specified in INI
-Airtime functionality when taking big jumps and landing on tires, causing them to pop like in real life (must land on all 4 tires, this sometimes doesnt work)
-Optional: Police can search civilian players trunk of vehicles for weapons (unfinished, possibly broken)

Installation:

Copy EverythingCar.net, EverythingCar.ini, EverythingCarSaves.ini into scripts folder.

Known issues:

-Crashing may occur with higher intervals
-Older versions sometimes lose car data (this was fixed, report otherwise)
-When storing grenades/molotovs, vehicle display menu shows improper weapon values, extracting ammo/weapons or removing grenades fixes this.



General CREDITS:
-AngryAmoeba, his scripts like Bank Account, Weapon Weight and Weapon storage inspired me a ton and are wonderful solid scripts. 
I've talked to Angry and he is super cool, allowing me to re-release some of his scripts with added functionality.
Some of his functionality is burrowed/modified in the persistent portions of my scripts as well as his ATM locator functionality in my PvP Wanted scripts hospital respawn functionality.

LCPDFR.com, without your mod and support, none of this would be possible!

Also special thanks to scripthook IRC community channel found here: http://www.gtaforums.com/index.php?showtopic=479739
If your interested in coding, these guys are the ones to talk to!