----------------------------------------------------
-	MPBlips v1.0
-	Mod by: Daimyo21
-	contact: daimyo21mods@gmail.com
-	Works in Multiplayer Only
----------------------------------------------------

Intro: In Freeroam blips can either be on or off with every other blip mode broken, this is where MPBlips comes in!

MPBlips is a multiplayer-only script specifically written to work alongside LCPFR for both PvP or PvE, but can be used for other means of play online.

What does it do?
	-Its first intent is to make blips more interesting and dynamic by highlighting player cops and civilians as 
	Blue or Red dots with many different conditions, shapes, and animations.

Features:
-Source script included! Since I have no parameters, I'm leaving this script open-source so you can customize to your likings
-Custom Blips of players in Police(blue) and Civilian(red) skins
-Blips dynamically change based on conditions such as (more detailed conditions at bottom):
	-Smaller blue/red circular blip to show the cop/civilian is on foot
	-Large red Square Blip if civilian is in a vehicle
	-PatrolCar blip for cops in a vehicle
	-Flashing red and blue blip for cops only with sirens active
	-Wanted blip for civilians that are wanted, the higher the wanted level, the farther the blip 
	can be seen (max stars and the blip begins to flash and show anywhere on the map)
	-Police and civilians see each other differently based on conditions like wanted level, 
	sirens, shooting, field of view (meaning if you look left, you'll see a blip that you didnt before, if in range)
	-Cooperative condition blips between police so this script can be used with LCPDFR etc.
	
Detailed Conditions:

###Police###:
-Cops see other cops within 150 meters always
-Cops see other cops within 200 meters while other cop is in vehicle 
	* +50 meter bonus if your siren is on.
	* +100 meter bonus if your siren AND the other cops siren is on for a total of 300 meter blip range
	
-Cops see civilians if within
	*250 meters if speeding above 60mph | OR shooting, | OR wanted level is 1
	*120 meters if civilian is: on the cops screen (or field of view)
	*180 meters if civilian is: on the cops screen | AND in car
	*200 meters if civilian is: in vehicle | AND is on the cops screen | AND cop is in a vehicle
	*250 meters if civilian is: in vehicle | AND is on the cops screen | AND cop is in vehicle | with sirens active
	*150 meters if civilian is: NOT in vehicle | AND is on the cops screen | AND cop is in a vehicle
	*180 meters if civilian is: NOT in vehicle | AND is on the cops screen | AND cop is in a vehicle | with sirens active
	*400 meters if civilian has: a wanted level of 2
	*500 meters if civilian has: a wanted level of 3
	*600 meters if civilian has: a wanted level of 4
	*850 meters if civilian has: a wanted level of 5
	*infinite meters if civilian has: a wanted level of 6 (basically anywhere on the map)
	
###Civilians###:
-Civilians see cops within:
	*50 meters always
	*100 meters if cop is: on civilians screen (field of view)
	*150 meters if cop is: in vehicle with siren on
	*200 meters if cop is: shooting
	
-Civilians see other Civilians within:
	*60 meters always
	*100 meters if other civilian is: on screen (field of view)
	*300 meters if other civilian is: shooting OR | other civilian wanted level is 1
	*350 meters if other civilians wanted level is 2
	*400 meters if other civilians wanted level is 3
	*550 meters if other civilians wanted level is 4
	*700 meters if other civilians wanted level is 5
	*infinite meters if other civilians wanted level is 6 (from anywhere)
	
Installation:

Drag and drop MPBlips.net and MPBlips.ini into your scripts folder and start multiplayer freeroam with no blips on! (can take up to 60 seconds to load scripts)

Modding:

Feel free to modify the MPBlips.cs script but do not re-release without crediting!


KNOWN ISSUES:

-Some blips dont delete properly off other players so you may see multiple blips on one player, reloadscript to fix
-Script may crash, sometimes possibly due to higher interval setting, reloadscript to fix (save progress with persistent scripts!)


General CREDITS:
-AngryAmoeba, his scripts like Bank Account, Weapon Weight and Weapon storage inspired me a ton and are wonderful solid scripts. 
I've talked to Angry and he is super cool, allowing me to re-release some of his scripts with added functionality.
Some of his functionality is burrowed/modified in the persistent portions of my scripts as well as his ATM locator functionality in my PvP Wanted scripts hospital respawn functionality.

LCPDFR.com, without your mod and support, none of this would be possible!

Also special thanks to scripthook IRC community channel found here: http://www.gtaforums.com/index.php?showtopic=479739
If your interested in coding, these guys are the ones to talk to!