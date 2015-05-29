## Daimyo's RPG ModPack-Community Edition v1.2.2b ##



![Alt text](https://github.com/Daimyo21/GTA-IV-RPG-ModPack/blob/master/IntroImage.png "Daimyo's RPG ModPack-Community Edition")



Changelog v1.2.2b
-EverythingCar:
	-New:
		*Ability to change ammo transfer amount (default RShiftKey + left/right arrow)
		*Customizable vehicle display menu controls are now shown in INI file
	-Fixes:
		*Changing ammo transfer to greater than 10 would bug out when placing ammo in vehicle, allowing you to dupe ammo
		*EverythingCarSaves.INI save file was showing improper weapons/ammo, this should now be fixed
		*Other fixes/adjustments to the code
	-Known Issue
		*When storing grenades/molotovs, the vehicle display menu sometimes shows all stored weapons at 0(ammo shown is fine), extracting ammo/weapons or removing grenades/molotovs fixes this.


Changelog v1.2.1b:
-PersistentPersons
	*Voice now set according to character skin when player loads previous character or uses PersistentPersons "Insta-Cop" function to choose a police skin
		#If using this mod for other means and not policing, simply save the character and load it to set default voice of that character
		#Default voice works for ANY skin, not just police, as long as you use save the character and then load it,
	*All official police uniform skins (not security/body guards etc) will give a voice preview when using "Insta-Cop" or when loading a official police skin


Changelog v1.2b:
-Fast/Easy ModPack Installer Included (save files will not be overwritten, INI/Mod files can be individually evaluated to overwriting)
-Recommended Realistic Car/Handling Damage installer with backup files
-PublicHostGame v1.1.1
	*Global Controls Image updated with PersistentPersons walk key added (possible other control additions)
-PersistentPersons v1.1
	*Walk mode available in Multiplayer (check INI for controls, default CAPSLOCK + movement keys)
	*Arrest reward system enabled with tweaks and INI customizable settings
-BleedHeal v1.1
	*Bleed heal animation mechanics more usable
	*Animation mechanics more compatible with walking animation used in PersistentPersons script (Health Limit Parameters in both INIs must be the same)


Changelog v1.1b:
-PublicHostGame:
	*Updated Global Controls Menu for EverythingCar wrong/missing entries
-EverythingCar:
	*Fixed cars not spawning in singleplayer on load (this was already fixed, uploaded wrong file)
	*Fixed INI control for placing weapon in trunk, was CTRL + T, now its Shift + T

###IMPORTANT/REQUIRED###
	*Make sure you have the latest .NET Framework 4.5, all C++ redistributals, ScriptHookDotNet 1.7.1.7, Aru's C++ Script Hook v.0.5.1 
	for GTA IV 1.0.7.0 or and EFLC 1.1.2.0

Instructions:

If you simply want to install all mods(only ones with included scripts), run the installer

Each Folder represents a seperate mod with seperate readme + install instructions

Modified and Recommended Community EXTRAS are community mods, some with modifications as marked by - Modified

Some Mods that have been permitted include modified INI files as marked by - INI Modified

Other community mods are simply INI files as they are not permitted to be released with this mod pack


-PublicHostGame mod includes a folder called PublicHostGame_Menu which includes a Global Controls Menu of all mods and their control schemes
	*This menu can be accessible with default SHIFT + ALT + M
