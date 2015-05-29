----------------------------------------------------
-	PvP Wanted v1.0
-	Mod by: Daimyo21
-	contact: daimyo21mods@gmail.com
-	Works in Multiplayer Only
----------------------------------------------------

PvP Wanted is a script that adds several features and functions and is made ONLY for multiplayer
and intended to be played alongside LCPDFR and my other scripts that add RPG/Realism elements


What was this script originally for?
	-It was originally created to add a PvP element for LCPDFR like SAMP/MTA Cops N Crooks, 
	however I ran out of time to work on it so many PvP features were left out because they were unfinished.

So is the script useless?
	-NO! Far from it in fact! I added a buttload of features that pertain more to LCPDFR and 
	my other scripts (its sort of all over the place, Ill leave it open-source)

PvP Wanted Features:

#Realism approach (all optional)
-Disables driver-side shooting while in a car
-Passenger side shooting made difficult to aim
-Blind-firing from cover made difficult to aim
-Optional Respawn at nearest hospital setting (entire map must be opened in multiplayer free roam)

#Multiplayer/Miscellaneous
-(Optional)Police officer pedestrian penalty/reward system (reward/penalty money amount can be set in .INI file)
	*Cops are rewarded for killing pedestrians who have a weapon or attacked a cop (basically wanted)
	*Cops are rewarded for killing pedestrians with weapons
	*Cops are penalyzed for killing peds that have no weapon and have not attacked the officer
-(Optional)Basic multiplayer freeroam implementation of Proximity Voice Chat (since freeroam doesnt have that)
	*if players > X meters from you, the script mutes other players mics and unmutes them when they are close enough

#Use at own risk (most unfinished):
-Optional: Ability for player cops to give civilian players wanted levels (tested, dont spam, pretty sure it was stable, report otherwise/stop using if script crashes, does not require ALLOW_PVP_FUNCTIONS)
	*Must be within 75-100 meters away and in police's field of view
	*On-screen messages synced through multiplayer (stating what cop is giving you the wanted level)
-Optional: PvP Arrest Functions, Tested, but Unfinished/possibly  (ALLOW_PVP_FUNCTIONS must be true for both players)
	*In order for this to work, the criminal player must be wanted and ragdolled (taser pistol mod is great for this)
	and a player police officers must be on foot AND within 10 meters to begin the arresting functionality (all automated and locks controls)
	*The criminal player is automatically teleported to nearest hospital with loss of weapons and money specified
	*Officer is rewarded for his arrest dependent on criminal wanted level amount
-Optional: PvP reward/penalties, automated wanted level chase functionality (ALLOW_PVP_FUNCTIONS setting enables/disables)
-Ability for player cops to give civilians warning messages (Disabled due to script crashing, feel free to try and get workin in scripts)

Installation:

Copy PvP Wanted.net and PvP Wanted.ini in your scripts folder

General CREDITS:
-AngryAmoeba, his scripts like Bank Account, Weapon Weight and Weapon storage inspired me a ton and are wonderful solid scripts. 
I've talked to Angry and he is super cool, allowing me to re-release some of his scripts with added functionality.
Some of his functionality is burrowed/modified in the persistent portions of my scripts as well as his ATM locator functionality in my PvP Wanted scripts hospital respawn functionality.

LCPDFR.com, without your mod and support, none of this would be possible!

Also special thanks to scripthook IRC community channel found here: http://www.gtaforums.com/index.php?showtopic=479739
If your interested in coding, these guys are the ones to talk to!