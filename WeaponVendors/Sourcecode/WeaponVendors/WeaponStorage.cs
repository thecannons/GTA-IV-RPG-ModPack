// Weapon Storage v1.1 by AngryAmoeba
// http://www.gtaforums.com/index.php?showtopic=486522

using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;

public class WeaponStorage : Script {

    // NESTED CLASS - unique stash of weapons and ammo for each safehouse
    public class Stash {
        public int glock = 0,
                   desertEagle = 0,
                   pistolAmmo = 0,
                   basicShotgun = 0,
                   combatShotgun = 0,
                   shotgunAmmo = 0,
                   uzi = 0,
                   MP5 = 0,
                   SMGAmmo = 0,
                   AK47 = 0,
                   M4 = 0,
                   assaultRifleAmmo = 0,
                   basicSniper = 0,
                   combatSniper = 0,
                   sniperAmmo = 0,
                   rocketLauncher = 0,
                   rocketAmmo = 0,
                   grenades = 0,
                   molotovs = 0,
                   knife = 0,
                   baseballBat = 0;
    } // END NESTED CLASS

    #region GLOBAL_VARIABLES
    Keys openStashKey,
         storeKey,
         retrieveKey,
         nextWeaponKey,
         lastWeaponKey,
         moveMoreKey,
         moveLessKey,
         modifierKey;

    //int weaponMax,
    //    ammoMax;

    Stash alderney = new Stash(),
          xenotime = new Stash(),
          midparkeast = new Stash(),
          bohan = new Stash(),
          hovebeach = new Stash();
    List<Weapon> weaponList;        // List of supported weapons
    Ped pc;                         // The player character

    bool bStashIsOpen = false;      // Is the player using a stash
    string playerRoom = "";         // Used to detect if player is in safehouse
    Stash currentStash;             // The stash for the current safehouse
    int stashWeaponIndex = 0,       // The index of the selected weapon
        moveAmt = 50;               // How much ammo to move at a time

    bool bSaving = false;
    AnimationSet saveAnims = new AnimationSet("amb@savegame");
    #endregion // GLOBAL_VARIABLES

    // Initialization
    public WeaponStorage() {
        Interval = 1000;
        Tick += new EventHandler(WeaponStorage_Tick);
        KeyDown += new GTA.KeyEventHandler(WeaponStorage_KeyDown);
        pc = Player.Character;

        // Fill weapon list
        weaponList = new List<Weapon>();
        weaponList.Add(Weapon.Handgun_Glock);
        weaponList.Add(Weapon.Handgun_DesertEagle);
        weaponList.Add(Weapon.Shotgun_Basic);
        weaponList.Add(Weapon.Shotgun_Baretta);
        weaponList.Add(Weapon.SMG_Uzi);
        weaponList.Add(Weapon.SMG_MP5);
        weaponList.Add(Weapon.Rifle_AK47);
        weaponList.Add(Weapon.Rifle_M4);
        weaponList.Add(Weapon.SniperRifle_Basic);
        weaponList.Add(Weapon.SniperRifle_M40A1);
        weaponList.Add(Weapon.Heavy_RocketLauncher);
        weaponList.Add(Weapon.Thrown_Molotov);
        weaponList.Add(Weapon.Thrown_Grenade);
        weaponList.Add(Weapon.Melee_BaseballBat);
        weaponList.Add(Weapon.Melee_Knife);

        // Get keys and stash values from INI file
        loadINI();
    } // end initialization

    void WeaponStorage_Tick(object sender, EventArgs e) {
        // Save stashes after saving in a bed
        if (hasGameBeenSaved()) saveINI();
        // Show stash info while the player is using a stash
        if (bStashIsOpen)
            if (!bSaving && isPlayerInSafehouse()) showStorageText();
            else endStorage();
    } // end Tick()

    void WeaponStorage_KeyDown(object sender, GTA.KeyEventArgs e) {
        // Beginning and ending storage
        if (e.Key == openStashKey) {
            if (bStashIsOpen) endStorage();
            else if (!bSaving && isPlayerInSafehouse()) startStorage();
        }
        else if (bStashIsOpen) {
            // Cycling through weapons in the stash
            if (e.Key == lastWeaponKey)
                cycleStashLast();
            else if (e.Key == nextWeaponKey)
                cycleStashNext();
            // Adjusting how much ammo is moved
            else if (e.Key == moveMoreKey) {
                if (isKeyPressed(modifierKey)) {
                    if (moveAmt < 10) moveAmt = 50;
                    else moveAmt += 50;
                }
                else if (moveAmt < 10) moveAmt++;
                else moveAmt += 10;
            }
            else if (e.Key == moveLessKey) {
                if (isKeyPressed(modifierKey)) moveAmt -= 50;
                else if (moveAmt <= 10) moveAmt--;
                else moveAmt -= 10;
                if (moveAmt < 1) moveAmt = 1;
            }
            // Retrieving weapons and ammo
            else if (e.Key == retrieveKey)
                if (isKeyPressed(modifierKey)) retrieveWeaponAndAmmo();
                else retrieveAmmo();
            // Storing weapons and ammo
            else if (e.Key == storeKey)
                if (isKeyPressed(modifierKey)) storeWeaponAndAmmo();
                else storeAmmo();
            else return;

            showStorageText();
        } // end else if bStashIsOpen
    } // end KeyDown()

    // =================
    // STORAGE/RETRIEVAL
    // =================
    #region STORAGE_RETRIEVAL

    void startStorage() {
        currentStash = getCurrentStash();
        // This empty check sets stashWeapon & stashWeaponIndex for retrieval
        if (areStashAndInventoryEmpty())
            Game.DisplayText("Stash and inventory are both empty.");
        else {
            GTA.Native.Function.Call("SET_CAMERA_CONTROLS_DISABLED_WITH_PLAYER_CONTROLS", 0);
            Player.CanControlCharacter = false;
            bStashIsOpen = true;
            showStorageText();
        }
    } // end startStorage()

    void endStorage() {
        pc.Weapons.Unarmed.Select();
        GTA.Native.Function.Call("SET_CAMERA_CONTROLS_DISABLED_WITH_PLAYER_CONTROLS", 1);
        Player.CanControlCharacter = true;
        bStashIsOpen = false;
        Game.DisplayText(" ", 1);
    } // end endStorage()

    void cycleStashNext() {
        do {
            if (stashWeaponIndex >= weaponList.Count - 1) stashWeaponIndex = 0;
            else stashWeaponIndex++;
        } while (getStashWeaponCount() <= 0 && !isStashWeaponHeld());
    } // end cycleStashNext()

    void cycleStashLast() {
        do {
            if (stashWeaponIndex <= 0) stashWeaponIndex = weaponList.Count - 1;
            else stashWeaponIndex--;
        } while (getStashWeaponCount() <= 0 && !isStashWeaponHeld());
    } // end cycleStashLast()

    void storeAmmo() {
        if (!isStashWeaponHeld()) return;

        // Determine whether held ammo is enough for moveAmt
        int heldAmmo = getStashWeaponHeldAmmo(),
            ammoToStore = (heldAmmo < moveAmt ? heldAmmo : moveAmt);

        // Move ammo from inventory to stash
        if (heldAmmo <= ammoToStore) storeWeaponAndAmmo(); // Melee weapons always end up here
        else {
            setStashWeaponAmmo(getStashWeaponAmmo() + ammoToStore);
            setStashWeaponHeldAmmo(getStashWeaponHeldAmmo() - ammoToStore);
        }
    } // end storeAmmo()

    void storeWeaponAndAmmo() {
        if (isStashWeaponHeld()) {
            setStashWeaponCount(getStashWeaponCount() + 1);
            setStashWeaponAmmo(getStashWeaponAmmo() + getStashWeaponHeldAmmo());
            setStashWeaponHeldAmmo(0);
            pc.Weapons.FromType(weaponList[stashWeaponIndex]).Remove();
        }
    } // end storeWeaponAndAmmo()

    void retrieveAmmo() {
        int ammoToRetrieve,
            stashWeaponHeldAmmo,
            stashWeaponAmmo = getStashWeaponAmmo();
        if (stashWeaponAmmo <= 0) return;

        stashWeaponHeldAmmo = getStashWeaponHeldAmmo();

        if (stashWeaponHeldAmmo <= 0) retrieveWeapon();
        else {
            ammoToRetrieve = getStashWeaponMaxAmmo() - stashWeaponHeldAmmo;
            if (ammoToRetrieve > moveAmt) ammoToRetrieve = moveAmt;
            if (ammoToRetrieve > stashWeaponAmmo) ammoToRetrieve = stashWeaponAmmo;

            // Move ammo from stash to inventory
            setStashWeaponHeldAmmo(stashWeaponHeldAmmo + ammoToRetrieve);
            setStashWeaponAmmo(stashWeaponAmmo - ammoToRetrieve);
        }
    } // end retrieveAmmo()

    void retrieveWeapon() {
        if (isStashWeaponHeld() || getStashWeaponCount() <= 0) return;

        // Swap weapons of same type, and handle melee weapons at end of switch block
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock:
                if (pc.Weapons.DesertEagle.isPresent) {
                    currentStash.desertEagle++;
                    currentStash.pistolAmmo += pc.Weapons.DesertEagle.Ammo;
                    pc.Weapons.DesertEagle.Remove();
                }
                break;
            case Weapon.Handgun_DesertEagle:
                if (pc.Weapons.Glock.isPresent) {
                    currentStash.glock++;
                    currentStash.pistolAmmo += pc.Weapons.Glock.Ammo;
                    pc.Weapons.Glock.Remove();
                }
                break;
            case Weapon.Shotgun_Basic:
                if (pc.Weapons.BarettaShotgun.isPresent) {
                    currentStash.combatShotgun++;
                    currentStash.shotgunAmmo += pc.Weapons.BarettaShotgun.Ammo;
                    pc.Weapons.BarettaShotgun.Remove();
                }
                break;
            case Weapon.Shotgun_Baretta:
                if (pc.Weapons.BasicShotgun.isPresent) {
                    currentStash.basicShotgun++;
                    currentStash.shotgunAmmo += pc.Weapons.BasicShotgun.Ammo;
                    pc.Weapons.BasicShotgun.Remove();
                }
                break;
            case Weapon.SMG_Uzi:
                if (pc.Weapons.MP5.isPresent) {
                    currentStash.MP5++;
                    currentStash.SMGAmmo += pc.Weapons.MP5.Ammo;
                    pc.Weapons.MP5.Remove();
                }
                break;
            case Weapon.SMG_MP5:
                if (pc.Weapons.Uzi.isPresent) {
                    currentStash.uzi++;
                    currentStash.SMGAmmo += pc.Weapons.Uzi.Ammo;
                    pc.Weapons.Uzi.Remove();
                }
                break;
            case Weapon.Rifle_AK47:
                if (pc.Weapons.AssaultRifle_M4.isPresent) {
                    currentStash.M4++;
                    currentStash.assaultRifleAmmo += pc.Weapons.AssaultRifle_M4.Ammo;
                    pc.Weapons.AssaultRifle_M4.Remove();
                }
                break;
            case Weapon.Rifle_M4:
                if (pc.Weapons.AssaultRifle_AK47.isPresent) {
                    currentStash.AK47++;
                    currentStash.assaultRifleAmmo += pc.Weapons.AssaultRifle_AK47.Ammo;
                    pc.Weapons.AssaultRifle_AK47.Remove();
                }
                break;
            case Weapon.SniperRifle_Basic:
                if (pc.Weapons.SniperRifle_M40A1.isPresent) {
                    currentStash.combatSniper++;
                    currentStash.sniperAmmo += pc.Weapons.SniperRifle_M40A1.Ammo;
                    pc.Weapons.SniperRifle_M40A1.Remove();
                }
                break;
            case Weapon.SniperRifle_M40A1:
                if (pc.Weapons.BasicSniperRifle.isPresent) {
                    currentStash.basicSniper++;
                    currentStash.sniperAmmo += pc.Weapons.BasicSniperRifle.Ammo;
                    pc.Weapons.BasicSniperRifle.Remove();
                }
                break;
            case Weapon.Thrown_Molotov:
                if (pc.Weapons.Grenades.isPresent) {
                    currentStash.grenades += pc.Weapons.Grenades.Ammo;
                    pc.Weapons.Grenades.Remove();
                }
                break;
            case Weapon.Thrown_Grenade:
                if (pc.Weapons.MolotovCocktails.isPresent) {
                    currentStash.molotovs += pc.Weapons.MolotovCocktails.Ammo;
                    pc.Weapons.MolotovCocktails.Remove();
                }
                break;
            case Weapon.Melee_BaseballBat:
                if (pc.Weapons.Knife.isPresent) {
                    pc.Weapons.Knife.Remove();
                    currentStash.knife++;
                }
                break;
            case Weapon.Melee_Knife:
                if (pc.Weapons.BaseballBat.isPresent) {
                    pc.Weapons.BaseballBat.Remove();
                    currentStash.baseballBat++;
                }
                break;
            case Weapon.Heavy_RocketLauncher: break;
        }

        if (getStashWeaponAmmo() > 0) {
            setStashWeaponCount(getStashWeaponCount() - 1);
            setStashWeaponAmmo(getStashWeaponAmmo() - 1);
            setStashWeaponHeldAmmo(1);
            moveAmt--;
            retrieveAmmo(); // CRASHES if held ammo is 0 at this call, so it's set to 1 just prior
            moveAmt++;
        }
    } // end retrieveWeapon()

    void retrieveWeaponAndAmmo() {
        retrieveWeapon();
        if (weaponList[stashWeaponIndex] != pc.Weapons.AnyMelee) {
            while (getStashWeaponAmmo() > 0 && getStashWeaponHeldAmmo() < getStashWeaponMaxAmmo())
                retrieveAmmo();
        }
    } // end retrieveWeaponAndAmmo()

    void showStorageText() {
        string msg = "";
        msg += "[ " + getStashWeaponName() + " ]";
        if (stashWeaponIndex < 11) msg += " x" + getStashWeaponCount() + "       [ Ammo ]";
        msg += "   Stash: " + getStashWeaponAmmo() + "    " +
               "Held: " + getStashWeaponHeldAmmo() + "    " +
               "Move: " + moveAmt;
        Game.DisplayText(msg);
    } // end showStorageText()
    #endregion // STORAGE_RETRIEVAL

    // ================
    //  BOOLEAN CHECKS
    // ================
    #region BOOLEAN_CHECKS

    bool isPlayerInSafehouse() {
        playerRoom = pc.CurrentRoom.ToString().Substring(2, 8);
        switch (playerRoom) {
            case "98A9FF4C":
            case "35A0C2D1":
            case "797B2DC8":
            case "5FCCED00":
            case "E58A7910":
            case "B63B9A73":
            case "A21B9BAE":
                return true;
            default:
                return false;
        }
    } // end isPlayerInSafehouse

    bool areStashAndInventoryEmpty() {
        bool empty = true;
        int origIndex = stashWeaponIndex;

        stashWeaponIndex = 0;
        while (empty && stashWeaponIndex < weaponList.Count) {
            if (getStashWeaponAmmo() > 0 || isStashWeaponHeld())
                empty = false;
            else stashWeaponIndex++;
        }

        if (empty) stashWeaponIndex = origIndex;
        return empty;
    } // end areStashAndInventoryEmpty()

    bool isStashWeaponHeld() {
        return pc.Weapons.FromType(weaponList[stashWeaponIndex]).isPresent;
    } // end isStashWeaponHeld()

    bool hasGameBeenSaved() {
        if (!bSaving) {
            if (pc.Animation.isPlaying(saveAnims, "lie_on_bed_l") ||
                pc.Animation.isPlaying(saveAnims, "lie_on_bed_r"))
                bSaving = true;
        }
        else if (pc.Animation.isPlaying(saveAnims, "get_out_bed_l") ||
                 pc.Animation.isPlaying(saveAnims, "get_out_bed_r") ||
                 pc.Animation.isPlaying(saveAnims, "upset_get_out_bed_l") ||
                 pc.Animation.isPlaying(saveAnims, "angry_get_out_bed_l")) {
            bSaving = false;
            if (GTA.Native.Function.Call<bool>("DID_SAVE_COMPLETE_SUCCESSFULLY"))
                return true;
        }
        return false;
    } // end hasGameBeenSaved()
    #endregion // BOOLEAN_CHECKS

    // ===============
    //     GETTERS
    // ===============
    #region GETTERS

    Stash getCurrentStash() {
        //playerRoom = pc.CurrentRoom.ToString().Substring(2, 8);
        switch (playerRoom) {
            case "98A9FF4C": return alderney;
            case "35A0C2D1":
            case "797B2DC8": return xenotime;
            case "5FCCED00": return midparkeast;
            case "E58A7910":
            case "B63B9A73": return bohan;
            case "A21B9BAE": return hovebeach;
            default: throw new Exception("Cannot call getCurrentStash() outside of safehouse.");
        }
    } // end getCurrentStash

    string getStashWeaponName() {
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock: return "Glock";
            case Weapon.Handgun_DesertEagle: return "Desert Eagle";
            case Weapon.Shotgun_Basic: return "Basic Shotgun";
            case Weapon.Shotgun_Baretta: return "Combat Shotgun";
            case Weapon.SMG_Uzi: return "Uzi";
            case Weapon.SMG_MP5: return "MP5";
            case Weapon.Rifle_AK47: return "AK47";
            case Weapon.Rifle_M4: return "M4";
            case Weapon.SniperRifle_Basic: return "Basic Sniper";
            case Weapon.SniperRifle_M40A1: return "Combat Sniper";
            case Weapon.Heavy_RocketLauncher: return "RPG";
            case Weapon.Thrown_Molotov: return "Molotovs";
            case Weapon.Thrown_Grenade: return "Grenades";
            case Weapon.Melee_BaseballBat: return "Baseball Bats";
            case Weapon.Melee_Knife: return "Knives";
            default: return "INVALID WEAPON";
        }
    } // end getStashWeaponName()

    int getStashWeaponCount() {
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock: return currentStash.glock;
            case Weapon.Handgun_DesertEagle: return currentStash.desertEagle;
            case Weapon.Shotgun_Basic: return currentStash.basicShotgun;
            case Weapon.Shotgun_Baretta: return currentStash.combatShotgun;
            case Weapon.SMG_Uzi: return currentStash.uzi;
            case Weapon.SMG_MP5: return currentStash.MP5;
            case Weapon.Rifle_AK47: return currentStash.AK47;
            case Weapon.Rifle_M4: return currentStash.M4;
            case Weapon.SniperRifle_Basic: return currentStash.basicSniper;
            case Weapon.SniperRifle_M40A1: return currentStash.combatSniper;
            case Weapon.Heavy_RocketLauncher: return currentStash.rocketLauncher;
            case Weapon.Thrown_Molotov: return currentStash.molotovs;
            case Weapon.Thrown_Grenade: return currentStash.grenades;
            case Weapon.Melee_BaseballBat: return currentStash.baseballBat;
            case Weapon.Melee_Knife: return currentStash.knife;
            default: return -1;
        }
    } // end getStashWeaponAmmo()

    int getStashWeaponAmmo() {
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock:
            case Weapon.Handgun_DesertEagle: return currentStash.pistolAmmo;
            case Weapon.Shotgun_Basic:
            case Weapon.Shotgun_Baretta: return currentStash.shotgunAmmo;
            case Weapon.SMG_Uzi:
            case Weapon.SMG_MP5: return currentStash.SMGAmmo;
            case Weapon.Rifle_AK47:
            case Weapon.Rifle_M4: return currentStash.assaultRifleAmmo;
            case Weapon.SniperRifle_Basic:
            case Weapon.SniperRifle_M40A1: return currentStash.sniperAmmo;
            case Weapon.Heavy_RocketLauncher: return currentStash.rocketAmmo;
            case Weapon.Thrown_Molotov: return currentStash.molotovs;
            case Weapon.Thrown_Grenade: return currentStash.grenades;
            case Weapon.Melee_BaseballBat: return currentStash.baseballBat;
            case Weapon.Melee_Knife: return currentStash.knife;
            default: return -999;
        }
    } // end getStashWeaponAmmo()

    int getStashWeaponHeldAmmo() {
        int heldAmmo;

        // Special case for melee weapons, which show 0 for ammo even when present
        if ((weaponList[stashWeaponIndex] == Weapon.Melee_BaseballBat && pc.Weapons.BaseballBat.isPresent)
            || (weaponList[stashWeaponIndex] == Weapon.Melee_Knife && pc.Weapons.Knife.isPresent))
            heldAmmo = 1;
        else heldAmmo = pc.Weapons.FromType(weaponList[stashWeaponIndex]).Ammo;

        return heldAmmo;
    } // end getStashWeaponHeldAmmo()

    int getStashWeaponMaxAmmo() {
        return pc.Weapons.FromType(weaponList[stashWeaponIndex]).MaxAmmo;
    } // end getStashWeaponMaxAmmo()
    #endregion // GETTERS

    // ===============
    //     SETTERS
    // ===============
    #region SETTERS

    void setStashWeaponCount(int count) {
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock: currentStash.glock = count; break;
            case Weapon.Handgun_DesertEagle: currentStash.desertEagle = count; break;
            case Weapon.Shotgun_Basic: currentStash.basicShotgun = count; break;
            case Weapon.Shotgun_Baretta: currentStash.combatShotgun = count; break;
            case Weapon.SMG_Uzi: currentStash.uzi = count; break;
            case Weapon.SMG_MP5: currentStash.MP5 = count; break;
            case Weapon.Rifle_AK47: currentStash.AK47 = count; break;
            case Weapon.Rifle_M4: currentStash.M4 = count; break;
            case Weapon.SniperRifle_Basic: currentStash.basicSniper = count; break;
            case Weapon.SniperRifle_M40A1: currentStash.combatSniper = count; break;
            case Weapon.Heavy_RocketLauncher: currentStash.rocketLauncher = count; break;
            case Weapon.Thrown_Molotov:
            case Weapon.Thrown_Grenade: break;
            case Weapon.Melee_BaseballBat: currentStash.baseballBat = count; break;
            case Weapon.Melee_Knife: currentStash.knife = count; break;
            default:
                Game.Console.Print("Invalid stashWeapon in setStashWeaponCount()");
                break;
        }
    } // end setStashWeaponCount()

    void setStashWeaponAmmo(int ammo) {
        switch (weaponList[stashWeaponIndex]) {
            case Weapon.Handgun_Glock:
            case Weapon.Handgun_DesertEagle: currentStash.pistolAmmo = ammo; break;
            case Weapon.Shotgun_Basic:
            case Weapon.Shotgun_Baretta: currentStash.shotgunAmmo = ammo; break;
            case Weapon.SMG_Uzi:
            case Weapon.SMG_MP5: currentStash.SMGAmmo = ammo; break;
            case Weapon.Rifle_AK47:
            case Weapon.Rifle_M4: currentStash.assaultRifleAmmo = ammo; break;
            case Weapon.SniperRifle_Basic:
            case Weapon.SniperRifle_M40A1: currentStash.sniperAmmo = ammo; break;
            case Weapon.Heavy_RocketLauncher: currentStash.rocketAmmo = ammo; break;
            case Weapon.Thrown_Molotov: currentStash.molotovs = ammo; break;
            case Weapon.Thrown_Grenade: currentStash.grenades = ammo; break;
            case Weapon.Melee_BaseballBat:
            case Weapon.Melee_Knife: break;
            default:
                Game.Console.Print("Invalid stashWeapon in setStashWeaponAmmo()");
                break;
        }
    } // end setStashWeaponAmmo()

    void setStashWeaponHeldAmmo(int ammo) {
        pc.Weapons.FromType(weaponList[stashWeaponIndex]).Ammo = ammo;
    } // end setStashWeaponHeldAmmo()
    #endregion // SETTERS

    // ===============
    //     INI FILE
    // ===============
    #region INI_FILE

    void loadINI() {
        SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "WeaponStorage.ini"));
        ini.Load();

        openStashKey = ini.GetValueKey("OpenStashKey", "KEYS", Keys.RControlKey);
        storeKey = ini.GetValueKey("StoreKey", "KEYS", Keys.RShiftKey);
        retrieveKey = ini.GetValueKey("RetrieveKey", "KEYS", Keys.Enter);
        nextWeaponKey = ini.GetValueKey("NextWeaponKey", "KEYS", Keys.Right);
        lastWeaponKey = ini.GetValueKey("LastWeaponKey", "KEYS", Keys.Left);
        moveMoreKey = ini.GetValueKey("MoveMoreKey", "KEYS", Keys.Up);
        moveLessKey = ini.GetValueKey("MoveLessKey", "KEYS", Keys.Down);
        modifierKey = ini.GetValueKey("ModifierKey", "KEYS", Keys.PageDown);

        //weaponMax = ini.GetValueInteger("WeaponMax", "OPTIONS", 10);
        //ammoMax = ini.GetValueInteger("AmmoMax", "OPTIONS", 1000);

        alderney.glock = ini.GetValueInteger("Glock", "STASH: ALDERNEY", 0);
        alderney.desertEagle = ini.GetValueInteger("DesertEagle", "STASH: ALDERNEY", 0);
        alderney.pistolAmmo = ini.GetValueInteger("PistolAmmo", "STASH: ALDERNEY", 0);
        alderney.basicShotgun = ini.GetValueInteger("BasicShotgun", "STASH: ALDERNEY", 0);
        alderney.combatShotgun = ini.GetValueInteger("CombatShotgun", "STASH: ALDERNEY", 0);
        alderney.shotgunAmmo = ini.GetValueInteger("ShotgunAmmo", "STASH: ALDERNEY", 0);
        alderney.uzi = ini.GetValueInteger("Uzi", "STASH: ALDERNEY", 0);
        alderney.MP5 = ini.GetValueInteger("MP5", "STASH: ALDERNEY", 0);
        alderney.SMGAmmo = ini.GetValueInteger("SMGAmmo", "STASH: ALDERNEY", 0);
        alderney.AK47 = ini.GetValueInteger("AK47", "STASH: ALDERNEY", 0);
        alderney.M4 = ini.GetValueInteger("M4", "STASH: ALDERNEY", 0);
        alderney.assaultRifleAmmo = ini.GetValueInteger("AssaultRifleAmmo", "STASH: ALDERNEY", 0);
        alderney.basicSniper = ini.GetValueInteger("BasicSniper", "STASH: ALDERNEY", 0);
        alderney.combatSniper = ini.GetValueInteger("CombatSniper", "STASH: ALDERNEY", 0);
        alderney.sniperAmmo = ini.GetValueInteger("SniperAmmo", "STASH: ALDERNEY", 0);
        alderney.rocketLauncher = ini.GetValueInteger("RocketLauncher", "STASH: ALDERNEY", 0);
        alderney.rocketAmmo = ini.GetValueInteger("RocketAmmo", "STASH: ALDERNEY", 0);
        alderney.molotovs = ini.GetValueInteger("Molotovs", "STASH: ALDERNEY", 0);
        alderney.grenades = ini.GetValueInteger("Grenades", "STASH: ALDERNEY", 0);
        alderney.baseballBat = ini.GetValueInteger("BaseballBat", "STASH: ALDERNEY", 0);
        alderney.knife = ini.GetValueInteger("Knife", "STASH: ALDERNEY", 0);

        bohan.glock = ini.GetValueInteger("Glock", "STASH: BOHAN", 0);
        bohan.desertEagle = ini.GetValueInteger("DesertEagle", "STASH: BOHAN", 0);
        bohan.pistolAmmo = ini.GetValueInteger("PistolAmmo", "STASH: BOHAN", 0);
        bohan.basicShotgun = ini.GetValueInteger("BasicShotgun", "STASH: BOHAN", 0);
        bohan.combatShotgun = ini.GetValueInteger("CombatShotgun", "STASH: BOHAN", 0);
        bohan.shotgunAmmo = ini.GetValueInteger("ShotgunAmmo", "STASH: BOHAN", 0);
        bohan.uzi = ini.GetValueInteger("Uzi", "STASH: BOHAN", 0);
        bohan.MP5 = ini.GetValueInteger("MP5", "STASH: BOHAN", 0);
        bohan.SMGAmmo = ini.GetValueInteger("SMGAmmo", "STASH: BOHAN", 0);
        bohan.AK47 = ini.GetValueInteger("AK47", "STASH: BOHAN", 0);
        bohan.M4 = ini.GetValueInteger("M4", "STASH: BOHAN", 0);
        bohan.assaultRifleAmmo = ini.GetValueInteger("AssaultRifleAmmo", "STASH: BOHAN", 0);
        bohan.basicSniper = ini.GetValueInteger("BasicSniper", "STASH: BOHAN", 0);
        bohan.combatSniper = ini.GetValueInteger("CombatSniper", "STASH: BOHAN", 0);
        bohan.sniperAmmo = ini.GetValueInteger("SniperAmmo", "STASH: BOHAN", 0);
        bohan.rocketLauncher = ini.GetValueInteger("RocketLauncher", "STASH: BOHAN", 0);
        bohan.rocketAmmo = ini.GetValueInteger("RocketAmmo", "STASH: BOHAN", 0);
        bohan.molotovs = ini.GetValueInteger("Molotovs", "STASH: BOHAN", 0);
        bohan.grenades = ini.GetValueInteger("Grenades", "STASH: BOHAN", 0);
        bohan.baseballBat = ini.GetValueInteger("BaseballBat", "STASH: BOHAN", 0);
        bohan.knife = ini.GetValueInteger("Knife", "STASH: BOHAN", 0);

        hovebeach.glock = ini.GetValueInteger("Glock", "STASH: HOVE BEACH", 0);
        hovebeach.desertEagle = ini.GetValueInteger("DesertEagle", "STASH: HOVE BEACH", 0);
        hovebeach.pistolAmmo = ini.GetValueInteger("PistolAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.basicShotgun = ini.GetValueInteger("BasicShotgun", "STASH: HOVE BEACH", 0);
        hovebeach.combatShotgun = ini.GetValueInteger("CombatShotgun", "STASH: HOVE BEACH", 0);
        hovebeach.shotgunAmmo = ini.GetValueInteger("ShotgunAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.uzi = ini.GetValueInteger("Uzi", "STASH: HOVE BEACH", 0);
        hovebeach.MP5 = ini.GetValueInteger("MP5", "STASH: HOVE BEACH", 0);
        hovebeach.SMGAmmo = ini.GetValueInteger("SMGAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.AK47 = ini.GetValueInteger("AK47", "STASH: HOVE BEACH", 0);
        hovebeach.M4 = ini.GetValueInteger("M4", "STASH: HOVE BEACH", 0);
        hovebeach.assaultRifleAmmo = ini.GetValueInteger("AssaultRifleAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.basicSniper = ini.GetValueInteger("BasicSniper", "STASH: HOVE BEACH", 0);
        hovebeach.combatSniper = ini.GetValueInteger("CombatSniper", "STASH: HOVE BEACH", 0);
        hovebeach.sniperAmmo = ini.GetValueInteger("SniperAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.rocketLauncher = ini.GetValueInteger("RocketLauncher", "STASH: HOVE BEACH", 0);
        hovebeach.rocketAmmo = ini.GetValueInteger("RocketAmmo", "STASH: HOVE BEACH", 0);
        hovebeach.molotovs = ini.GetValueInteger("Molotovs", "STASH: HOVE BEACH", 0);
        hovebeach.grenades = ini.GetValueInteger("Grenades", "STASH: HOVE BEACH", 0);
        hovebeach.baseballBat = ini.GetValueInteger("BaseballBat", "STASH: HOVE BEACH", 0);
        hovebeach.knife = ini.GetValueInteger("Knife", "STASH: HOVE BEACH", 0);

        midparkeast.glock = ini.GetValueInteger("Glock", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.desertEagle = ini.GetValueInteger("DesertEagle", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.pistolAmmo = ini.GetValueInteger("PistolAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.basicShotgun = ini.GetValueInteger("BasicShotgun", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.combatShotgun = ini.GetValueInteger("CombatShotgun", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.shotgunAmmo = ini.GetValueInteger("ShotgunAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.uzi = ini.GetValueInteger("Uzi", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.MP5 = ini.GetValueInteger("MP5", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.SMGAmmo = ini.GetValueInteger("SMGAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.AK47 = ini.GetValueInteger("AK47", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.M4 = ini.GetValueInteger("M4", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.assaultRifleAmmo = ini.GetValueInteger("AssaultRifleAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.basicSniper = ini.GetValueInteger("BasicSniper", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.combatSniper = ini.GetValueInteger("CombatSniper", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.sniperAmmo = ini.GetValueInteger("SniperAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.rocketLauncher = ini.GetValueInteger("RocketLauncher", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.rocketAmmo = ini.GetValueInteger("RocketAmmo", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.molotovs = ini.GetValueInteger("Molotovs", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.grenades = ini.GetValueInteger("Grenades", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.baseballBat = ini.GetValueInteger("BaseballBat", "STASH: MIDDLE PARK EAST", 0);
        midparkeast.knife = ini.GetValueInteger("Knife", "STASH: MIDDLE PARK EAST", 0);

        xenotime.glock = ini.GetValueInteger("Glock", "STASH: XENOTIME", 0);
        xenotime.desertEagle = ini.GetValueInteger("DesertEagle", "STASH: XENOTIME", 0);
        xenotime.pistolAmmo = ini.GetValueInteger("PistolAmmo", "STASH: XENOTIME", 0);
        xenotime.basicShotgun = ini.GetValueInteger("BasicShotgun", "STASH: XENOTIME", 0);
        xenotime.combatShotgun = ini.GetValueInteger("CombatShotgun", "STASH: XENOTIME", 0);
        xenotime.shotgunAmmo = ini.GetValueInteger("ShotgunAmmo", "STASH: XENOTIME", 0);
        xenotime.uzi = ini.GetValueInteger("Uzi", "STASH: XENOTIME", 0);
        xenotime.MP5 = ini.GetValueInteger("MP5", "STASH: XENOTIME", 0);
        xenotime.SMGAmmo = ini.GetValueInteger("SMGAmmo", "STASH: XENOTIME", 0);
        xenotime.AK47 = ini.GetValueInteger("AK47", "STASH: XENOTIME", 0);
        xenotime.M4 = ini.GetValueInteger("M4", "STASH: XENOTIME", 0);
        xenotime.assaultRifleAmmo = ini.GetValueInteger("AssaultRifleAmmo", "STASH: XENOTIME", 0);
        xenotime.basicSniper = ini.GetValueInteger("BasicSniper", "STASH: XENOTIME", 0);
        xenotime.combatSniper = ini.GetValueInteger("CombatSniper", "STASH: XENOTIME", 0);
        xenotime.sniperAmmo = ini.GetValueInteger("SniperAmmo", "STASH: XENOTIME", 0);
        xenotime.rocketLauncher = ini.GetValueInteger("RocketLauncher", "STASH: XENOTIME", 0);
        xenotime.rocketAmmo = ini.GetValueInteger("RocketAmmo", "STASH: XENOTIME", 0);
        xenotime.molotovs = ini.GetValueInteger("Molotovs", "STASH: XENOTIME", 0);
        xenotime.grenades = ini.GetValueInteger("Grenades", "STASH: XENOTIME", 0);
        xenotime.baseballBat = ini.GetValueInteger("BaseballBat", "STASH: XENOTIME", 0);
        xenotime.knife = ini.GetValueInteger("Knife", "STASH: XENOTIME", 0);
    } // end loadINI()

    void saveINI() {
        SettingsFile ini = SettingsFile.Open(Path.Combine("scripts", "WeaponStorage.ini"));

        ini.SetValue("Glock", "STASH: ALDERNEY", alderney.glock);
        ini.SetValue("DesertEagle", "STASH: ALDERNEY", alderney.desertEagle);
        ini.SetValue("PistolAmmo", "STASH: ALDERNEY", alderney.pistolAmmo);
        ini.SetValue("BasicShotgun", "STASH: ALDERNEY", alderney.basicShotgun);
        ini.SetValue("CombatShotgun", "STASH: ALDERNEY", alderney.combatShotgun);
        ini.SetValue("ShotgunAmmo", "STASH: ALDERNEY", alderney.shotgunAmmo);
        ini.SetValue("Uzi", "STASH: ALDERNEY", alderney.uzi);
        ini.SetValue("MP5", "STASH: ALDERNEY", alderney.MP5);
        ini.SetValue("SMGAmmo", "STASH: ALDERNEY", alderney.SMGAmmo);
        ini.SetValue("AK47", "STASH: ALDERNEY", alderney.AK47);
        ini.SetValue("M4", "STASH: ALDERNEY", alderney.M4);
        ini.SetValue("AssaultRifleAmmo", "STASH: ALDERNEY", alderney.assaultRifleAmmo);
        ini.SetValue("BasicSniper", "STASH: ALDERNEY", alderney.basicSniper);
        ini.SetValue("CombatSniper", "STASH: ALDERNEY", alderney.combatSniper);
        ini.SetValue("SniperAmmo", "STASH: ALDERNEY", alderney.sniperAmmo);
        ini.SetValue("RocketLauncher", "STASH: ALDERNEY", alderney.rocketAmmo);
        ini.SetValue("RocketAmmo", "STASH: ALDERNEY", alderney.rocketLauncher);
        ini.SetValue("Molotovs", "STASH: ALDERNEY", alderney.molotovs);
        ini.SetValue("Grenades", "STASH: ALDERNEY", alderney.grenades);
        ini.SetValue("BaseballBat", "STASH: ALDERNEY", alderney.baseballBat);
        ini.SetValue("Knife", "STASH: ALDERNEY", alderney.knife);

        ini.SetValue("Glock", "STASH: BOHAN", bohan.glock);
        ini.SetValue("DesertEagle", "STASH: BOHAN", bohan.desertEagle);
        ini.SetValue("PistolAmmo", "STASH: BOHAN", bohan.pistolAmmo);
        ini.SetValue("BasicShotgun", "STASH: BOHAN", bohan.basicShotgun);
        ini.SetValue("CombatShotgun", "STASH: BOHAN", bohan.combatShotgun);
        ini.SetValue("ShotgunAmmo", "STASH: BOHAN", bohan.shotgunAmmo);
        ini.SetValue("Uzi", "STASH: BOHAN", bohan.uzi);
        ini.SetValue("MP5", "STASH: BOHAN", bohan.MP5);
        ini.SetValue("SMGAmmo", "STASH: BOHAN", bohan.SMGAmmo);
        ini.SetValue("AK47", "STASH: BOHAN", bohan.AK47);
        ini.SetValue("M4", "STASH: BOHAN", bohan.M4);
        ini.SetValue("AssaultRifleAmmo", "STASH: BOHAN", bohan.assaultRifleAmmo);
        ini.SetValue("BasicSniper", "STASH: BOHAN", bohan.basicSniper);
        ini.SetValue("CombatSniper", "STASH: BOHAN", bohan.combatSniper);
        ini.SetValue("SniperAmmo", "STASH: BOHAN", bohan.sniperAmmo);
        ini.SetValue("RocketLauncher", "STASH: BOHAN", bohan.rocketAmmo);
        ini.SetValue("RocketAmmo", "STASH: BOHAN", bohan.rocketLauncher);
        ini.SetValue("Molotovs", "STASH: BOHAN", bohan.molotovs);
        ini.SetValue("Grenades", "STASH: BOHAN", bohan.grenades);
        ini.SetValue("BaseballBat", "STASH: BOHAN", bohan.baseballBat);
        ini.SetValue("Knife", "STASH: BOHAN", bohan.knife);

        ini.SetValue("Glock", "STASH: HOVE BEACH", hovebeach.glock);
        ini.SetValue("DesertEagle", "STASH: HOVE BEACH", hovebeach.desertEagle);
        ini.SetValue("PistolAmmo", "STASH: HOVE BEACH", hovebeach.pistolAmmo);
        ini.SetValue("BasicShotgun", "STASH: HOVE BEACH", hovebeach.basicShotgun);
        ini.SetValue("CombatShotgun", "STASH: HOVE BEACH", hovebeach.combatShotgun);
        ini.SetValue("ShotgunAmmo", "STASH: HOVE BEACH", hovebeach.shotgunAmmo);
        ini.SetValue("Uzi", "STASH: HOVE BEACH", hovebeach.uzi);
        ini.SetValue("MP5", "STASH: HOVE BEACH", hovebeach.MP5);
        ini.SetValue("SMGAmmo", "STASH: HOVE BEACH", hovebeach.SMGAmmo);
        ini.SetValue("AK47", "STASH: HOVE BEACH", hovebeach.AK47);
        ini.SetValue("M4", "STASH: HOVE BEACH", hovebeach.M4);
        ini.SetValue("AssaultRifleAmmo", "STASH: HOVE BEACH", hovebeach.assaultRifleAmmo);
        ini.SetValue("BasicSniper", "STASH: HOVE BEACH", hovebeach.basicSniper);
        ini.SetValue("CombatSniper", "STASH: HOVE BEACH", hovebeach.combatSniper);
        ini.SetValue("SniperAmmo", "STASH: HOVE BEACH", hovebeach.sniperAmmo);
        ini.SetValue("RocketLauncher", "STASH: HOVE BEACH", hovebeach.rocketAmmo);
        ini.SetValue("RocketAmmo", "STASH: HOVE BEACH", hovebeach.rocketLauncher);
        ini.SetValue("Molotovs", "STASH: HOVE BEACH", hovebeach.molotovs);
        ini.SetValue("Grenades", "STASH: HOVE BEACH", hovebeach.grenades);
        ini.SetValue("BaseballBat", "STASH: HOVE BEACH", hovebeach.baseballBat);
        ini.SetValue("Knife", "STASH: HOVE BEACH", hovebeach.knife);

        ini.SetValue("Glock", "STASH: MIDDLE PARK EAST", midparkeast.glock);
        ini.SetValue("DesertEagle", "STASH: MIDDLE PARK EAST", midparkeast.desertEagle);
        ini.SetValue("PistolAmmo", "STASH: MIDDLE PARK EAST", midparkeast.pistolAmmo);
        ini.SetValue("BasicShotgun", "STASH: MIDDLE PARK EAST", midparkeast.basicShotgun);
        ini.SetValue("CombatShotgun", "STASH: MIDDLE PARK EAST", midparkeast.combatShotgun);
        ini.SetValue("ShotgunAmmo", "STASH: MIDDLE PARK EAST", midparkeast.shotgunAmmo);
        ini.SetValue("Uzi", "STASH: MIDDLE PARK EAST", midparkeast.uzi);
        ini.SetValue("MP5", "STASH: MIDDLE PARK EAST", midparkeast.MP5);
        ini.SetValue("SMGAmmo", "STASH: MIDDLE PARK EAST", midparkeast.SMGAmmo);
        ini.SetValue("AK47", "STASH: MIDDLE PARK EAST", midparkeast.AK47);
        ini.SetValue("M4", "STASH: MIDDLE PARK EAST", midparkeast.M4);
        ini.SetValue("AssaultRifleAmmo", "STASH: MIDDLE PARK EAST", midparkeast.assaultRifleAmmo);
        ini.SetValue("BasicSniper", "STASH: MIDDLE PARK EAST", midparkeast.basicSniper);
        ini.SetValue("CombatSniper", "STASH: MIDDLE PARK EAST", midparkeast.combatSniper);
        ini.SetValue("SniperAmmo", "STASH: MIDDLE PARK EAST", midparkeast.sniperAmmo);
        ini.SetValue("RocketLauncher", "STASH: MIDDLE PARK EAST", midparkeast.rocketAmmo);
        ini.SetValue("RocketAmmo", "STASH: MIDDLE PARK EAST", midparkeast.rocketLauncher);
        ini.SetValue("Molotovs", "STASH: MIDDLE PARK EAST", midparkeast.molotovs);
        ini.SetValue("Grenades", "STASH: MIDDLE PARK EAST", midparkeast.grenades);
        ini.SetValue("BaseballBat", "STASH: MIDDLE PARK EAST", midparkeast.baseballBat);
        ini.SetValue("Knife", "STASH: MIDDLE PARK EAST", midparkeast.knife);

        ini.SetValue("Glock", "STASH: XENOTIME", xenotime.glock);
        ini.SetValue("DesertEagle", "STASH: XENOTIME", xenotime.desertEagle);
        ini.SetValue("PistolAmmo", "STASH: XENOTIME", xenotime.pistolAmmo);
        ini.SetValue("BasicShotgun", "STASH: XENOTIME", xenotime.basicShotgun);
        ini.SetValue("CombatShotgun", "STASH: XENOTIME", xenotime.combatShotgun);
        ini.SetValue("ShotgunAmmo", "STASH: XENOTIME", xenotime.shotgunAmmo);
        ini.SetValue("Uzi", "STASH: XENOTIME", xenotime.uzi);
        ini.SetValue("MP5", "STASH: XENOTIME", xenotime.MP5);
        ini.SetValue("SMGAmmo", "STASH: XENOTIME", xenotime.SMGAmmo);
        ini.SetValue("AK47", "STASH: XENOTIME", xenotime.AK47);
        ini.SetValue("M4", "STASH: XENOTIME", xenotime.M4);
        ini.SetValue("AssaultRifleAmmo", "STASH: XENOTIME", xenotime.assaultRifleAmmo);
        ini.SetValue("BasicSniper", "STASH: XENOTIME", xenotime.basicSniper);
        ini.SetValue("CombatSniper", "STASH: XENOTIME", xenotime.combatSniper);
        ini.SetValue("SniperAmmo", "STASH: XENOTIME", xenotime.sniperAmmo);
        ini.SetValue("RocketLauncher", "STASH: XENOTIME", xenotime.rocketAmmo);
        ini.SetValue("RocketAmmo", "STASH: XENOTIME", xenotime.rocketLauncher);
        ini.SetValue("Molotovs", "STASH: XENOTIME", xenotime.molotovs);
        ini.SetValue("Grenades", "STASH: XENOTIME", xenotime.grenades);
        ini.SetValue("BaseballBat", "STASH: XENOTIME", xenotime.baseballBat);
        ini.SetValue("Knife", "STASH: XENOTIME", xenotime.knife);

        ini.Save();
    } // end saveINI()
    #endregion // INI_FILE
} // end class