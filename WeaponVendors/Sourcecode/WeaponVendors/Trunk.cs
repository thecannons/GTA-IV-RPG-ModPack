using System;
using System.Windows.Forms;
using GTA;

namespace Trunk
{

    // ### Press G to open nearest trunk :)

    public class Trunk : Script
    {

        public Trunk()
        {
            this.Tick += new EventHandler(this.ScriptCommunicationExample2_Tick);
            Wait(0);
        }


        private int Rj()
        {
            string rand = null;
            int r = Ra(52);
            if (r == 0) { rand = "0x797F1AEB"; }
            else if (r == 1) { rand = "0xA6A36C89"; }
            else if (r == 2) { rand = "0x64092246"; }
            else if (r == 3) { rand = "0x4C244EC4"; }
            else if (r == 4) { rand = "0xFAD5AB95"; }
            else if (r == 5) { rand = "0x1F5BA936"; }
            else if (r == 6) { rand = "0x8C5E2354"; }
            else if (r == 7) { rand = "0xEF265E44"; }
            else if (r == 8) { rand = "0x841672B8"; }
            else if (r == 9) { rand = "0xE6C7978D"; }
            else if (r == 10) { rand = "0xD1BBA26E"; }
            else if (r == 11) { rand = "0xAEE0CD2B"; }
            else if (r == 12) { rand = "0x73B108D9"; }
            else if (r == 13) { rand = "0xE204FA6F"; }
            else if (r == 14) { rand = "0x4D64ECE2"; }
            else if (r == 15) { rand = "0x48F004BD"; }
            else if (r == 16) { rand = "0x51DEAE9C"; }
            else if (r == 17) { rand = "0xA117440C"; }
            else if (r == 18) { rand = "0xA7A350DA"; }
            else if (r == 19) { rand = "0x384A3A39"; }
            else if (r == 20) { rand = "0x841672B8"; }
            else if (r == 21) { rand = "0xAE3DA68C"; }
            else if (r == 22) { rand = "0x2DD35878"; }
            else if (r == 23) { rand = "0xDAFA26DD"; }
            else if (r == 24) { rand = "0x59306DE7"; }
            else if (r == 25) { rand = "0xBE861E82"; }
            else if (r == 26) { rand = "0x7B0B668E"; }
            else if (r == 27) { rand = "0xFBB6A5D0"; }
            else if (r == 28) { rand = "0xB893A78D"; }
            else if (r == 29) { rand = "0xB762AA41"; }
            else if (r == 30) { rand = "0x6DDCFC60"; }
            else if (r == 31) { rand = "0x73329CC8"; }
            else if (r == 32) { rand = "0x55F66268"; }
            else if (r == 33) { rand = "0xAAAEDA74"; }
            else if (r == 34) { rand = "0xD825FA46"; }
            else if (r == 35) { rand = "0x7E80B364"; }
            else if (r == 36) { rand = "0x7CC1B852"; }
            else if (r == 37) { rand = "0xECC1DB60"; }
            else if (r == 38) { rand = "0xAF51E081"; }
            else if (r == 39) { rand = "0x8693C9DE"; }
            else if (r == 40) { rand = "0x9029DB58"; }
            else if (r == 41) { rand = "0x9CE7F4D4"; }
            else if (r == 42) { rand = "0xEFFE8D8F"; }
            else if (r == 43) { rand = "0xFEAD2A02"; }
            else if (r == 44) { rand = "0xC18FDCED"; }
            else if (r == 45) { rand = "0xEEACB722"; }
            else if (r == 46) { rand = "0x97E7CF12"; }
            else if (r == 47) { rand = "0xC72B657D"; }
            else if (r == 48) { rand = "0xC3022546"; }
            else if (r == 49) { rand = "0x84442872"; }
            else if (r == 50) { rand = "0x3C4E43BC"; }
            else if (r == 51) { rand = "0xA6A36C89"; }

            int tr = Convert.ToInt32(rand, 16);
            return tr;

        }
        private int Ra(int max)
        {
            Random random = new Random();
            return random.Next(max);
        }
        private int R(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void ScriptCommunicationExample2_Tick(object sender, EventArgs e)
        {
            {

                if ((isKeyPressed(Keys.G)))//(isKeyPressed(Keys.MButton))||
                {
                    Ped Pede = null;
                    AnimationSet anims = new AnimationSet("missgun_car");

                    Wait(0);

                    GTA.Native.Function.Call("REQUEST_ANIMS", "missgun_car");

                    Wait(0);
                    Vector3 sx = Player.Character.GetOffsetPosition(new Vector3(0.0f, 4.0f, 0.0f));
                    Vehicle v = null;
                    v = GTA.World.GetClosestVehicle(sx, 4.8F);
                    if (v != null && v.Exists() && v.Health >= 50 && v.Speed <= 1.0f)
                    {

                        sx = v.GetOffsetPosition(new Vector3(0.0f, -3.0f, 0.0f));
                        if (Player.Character.Position.DistanceTo(sx) < 1.1f)
                        {
                            //if(!GTA.Native.Function.Call<bool>("IS_CAR_DOOR_FULLY_OPEN",v, 5))
                            {


                                v.FreezePosition = true;
                                Player.Character.Task.ClearAll();
                                Player.Character.Task.GoTo(sx, true);
                                Wait(600);
                                if (v.Exists()&&v!=null)
                                {
                                    float H = v.Heading;
                                    Player.Character.Task.TurnTo(v.Position);
                                    Wait(400);
                                    Player.Character.Heading = H + 25;

                                    Wait(50);
                                    GTA.Native.Function.Call("TASK_PLAY_ANIM", Player.Character, "open_trunk", "missgun_car", 4.00, 0, 1, 1, 0, -2);
                                    Wait(950);
                                    GTA.Native.Function.Call("OPEN_CAR_DOOR", v, 5);
                                    Wait(1500);

                                    Pede = v.GetPedOnSeat(VehicleSeat.Driver);
                                    if (Exists(Pede) && Pede.isAlive)
                                    {
                                        if ((Pede.RelationshipGroup == RelationshipGroup.Cop) && Player.WantedLevel <= 1)
                                        {

                                            GTA.Native.Function.Call("ALTER_WANTED_LEVEL", Player, 1);
                                            GTA.Native.Function.Call("APPLY_WANTED_LEVEL_CHANGE_NOW", Player);
                                        }

                                        int rand = Ra(2);
                                        if (rand != 1)
                                        {

                                            Game.DisplayText("Random junk found");
                                            GTA.Object temp = World.CreateObject(Rj(), sx);

                                            if (temp != null)
                                            {
                                                while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", temp))
                                                { Wait(0); }

                                                temp.NoLongerNeeded();
                                                GTA.Native.Function.Call("SET_OBJECT_AS_STEALABLE", temp, 1);
                                                GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", temp, 1);
                                                GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", temp);
                                                GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Player.Character, temp, 0.0f, 0.0f, 0.0f, 0);
                                                GTA.Native.Function.Call("DRAW_LIGHT_WITH_RANGE", temp.Position.X, temp.Position.Y, temp.Position.Z - 1.0F, 250, 250, 250, 2.0F, 75.0F);

                                            }
                                            Wait(1400);
                                            Player.Character.Task.ClearAll();
                                            Player.Character.Task.GoTo(sx, true);
                                            Wait(400);
                                            H = v.Heading;
                                            Player.Character.Task.TurnTo(v.Position);
                                            Wait(400);
                                            Player.Character.Heading = H + 25;

                                            //  Player.Character.Task.ClearAll();
                                            // Player.Character.Task.TurnTo(p1.GetBonePosition(Bone.Head));
                                            Wait(50);
                                            if (Player.Character.Position.DistanceTo(sx) < 1.0f)
                                            {
                                                GTA.Native.Function.Call("TASK_PLAY_ANIM", Player.Character, "shut_trunk", "missgun_car", 4.00, 0, 1, 1, 0, -2);
                                                Wait(950);

                                                GTA.Native.Function.Call("SHUT_CAR_DOOR", v, 5);
                                            }
                                            v.FreezePosition = false;
                                            Wait(2000);

                                        }
                                        else
                                        {


                                            v.FreezePosition = false;
                                            rand = Ra(15);
                                            int B;
                                            if (rand == 1) { Game.DisplayText("Illegal weapon found! Handgun"); Player.Character.Weapons.inSlot(WeaponSlot.Handgun).Ammo += 65; Wait(0); }
                                            else if (rand == 2) { Game.DisplayText("Illegal weapon found! SMG"); Player.Character.Weapons.inSlot(WeaponSlot.SMG).Ammo += 65; Wait(0); }
                                            else if (rand == 3) { Game.DisplayText("Illegal weapon found! Rifle"); Player.Character.Weapons.inSlot(WeaponSlot.Rifle).Ammo += 65; Wait(0); }
                                            else if (rand == 4) { Game.DisplayText("Illegal weapon found! Baton"); Player.Character.Weapons.BaseballBat.Ammo += 1; Wait(0); }
                                            else if (rand == 5) { Game.DisplayText("Illegal weapon found! Knife"); Player.Character.Weapons.Knife.Ammo += 1; Wait(0); }
                                            else if (rand == 6) { Game.DisplayText("Illegal weapon found! Handgun"); Player.Character.Weapons.inSlot(WeaponSlot.Handgun).Ammo += 65; Wait(0); }
                                            else if (rand == 7) { Game.DisplayText("Illegal weapon found! Shotgun"); Player.Character.Weapons.inSlot(WeaponSlot.Shotgun).Ammo += 65; Wait(0); }
                                            else if (rand == 8) { Game.DisplayText("Illegal weapon found! Knife"); Player.Character.Weapons.Knife.Ammo += 1; Wait(0); }
                                            else if (rand == 9) { Game.DisplayText("Illegal weapon found! Knife"); Player.Character.Weapons.Knife.Ammo += 1; Wait(0); }
                                            else if (rand == 10) { Game.DisplayText("Illegal weapon found! Handgun"); Player.Character.Weapons.inSlot(WeaponSlot.Handgun).Ammo += 65; Wait(0); }
                                            else if (rand == 11) { Game.DisplayText("Illegal weapon found! Baton"); Player.Character.Weapons.BaseballBat.Ammo += 1; Wait(0); }
                                            else if (rand == 12) { Game.DisplayText("Drug money found!"); B = R(100, 1000); Player.Money += B; Wait(0); }
                                            else if (rand == 13) { Game.DisplayText("Drugs found!"); B = R(100, 1000); Player.Money += B; Wait(0); }
                                            else { GTA.Native.Function.Call("ADD_ARMOUR_TO_CHAR", Player.Character, 50); Wait(0); }




                                            GTA.Native.Function.Call("SET_CHAR_AS_MISSION_CHAR", Pede, 1);
                                            Pede.RelationshipGroup = RelationshipGroup.Criminal;
                                            Pede.Health = Pede.Health + 175;
                                            B = R(1000, 8000);//Pede.Money += B;
                                            GTA.Native.Function.Call("SET_CHAR_MONEY", Pede, B);
                                            //Game.DisplayText("LCPD: suspect is aggressive!");
                                            //Pede.RelationshipGroup = RelationshipGroup.Criminal;

                                            rand = R(9, 18);

                                            GTA.Native.Function.Call("GIVE_WEAPON_TO_CHAR", Pede, rand, 9999, true);
                                            GTA.Native.Function.Call("SET_CURRENT_CHAR_WEAPON", Pede, rand, true);
                                            GTA.Native.Function.Call("Block_Ped_Weapon_Switching", Pede, 1);

                                            rand = Ra(2);
                                            if (rand == 1)
                                            {
                                                GTA.Native.Function.Call("Task_Car_Drive_Wander", Pede, v, 27.0F, 2);
                                            }
                                            else
                                            {

                                                Pede.Task.FightAgainst(Player.Character);
                                            }
                                            Pede.Task.AlwaysKeepTask = true;
                                            Wait(1000);
                                            GTA.Native.Function.Call("SAY_AMBIENT_SPEECH", Pede, "Surprised", 1, 1, 0);
                                            Wait(6000);
                                            GTA.Native.Function.Call("SET_CHAR_AS_MISSION_CHAR", Pede, 0);

                                        }

                                    }

                                    else
                                    {

                                        Game.DisplayText("Random junk found");
                                        GTA.Object temp = World.CreateObject(Rj(), sx);

                                        if (temp != null)
                                        {
                                            while (!GTA.Native.Function.Call<bool>("DOES_OBJECT_EXIST", temp))
                                            { Wait(0); }
                                            temp.NoLongerNeeded();
                                            GTA.Native.Function.Call("SET_OBJECT_DYNAMIC", temp, 1);
                                            GTA.Native.Function.Call("MARK_OBJECT_AS_NO_LONGER_NEEDED", temp);
                                            GTA.Native.Function.Call("TASK_PICKUP_AND_CARRY_OBJECT", Player.Character, temp, 0.0f, 0.0f, 0.0f, 0);
                                            GTA.Native.Function.Call("DRAW_LIGHT_WITH_RANGE", temp.Position.X, temp.Position.Y, temp.Position.Z - 1.0F, 250, 250, 250, 2.0F, 75.0F);

                                        }
                                        Wait(1400);
                                        Player.Character.Task.ClearAll();
                                        Player.Character.Task.GoTo(sx, true);
                                        Wait(400);
                                        H = v.Heading;
                                        Player.Character.Task.TurnTo(v.Position);
                                        Wait(400);
                                        Player.Character.Heading = H + 25;
                                        v.FreezePosition = false;
                                        //  Player.Character.Task.ClearAll();
                                        // Player.Character.Task.TurnTo(p1.GetBonePosition(Bone.Head));
                                        Wait(50);
                                        if (Player.Character.Position.DistanceTo(sx) < 1.0f)
                                        {
                                            GTA.Native.Function.Call("TASK_PLAY_ANIM", Player.Character, "shut_trunk", "missgun_car", 4.00, 0, 1, 1, 0, -2);
                                            Wait(950);

                                            GTA.Native.Function.Call("SHUT_CAR_DOOR", v, 5);
                                        }

                                        Wait(1000);

                                    }

                                }
                                Pede = null;
                            }

                        }

                    }

                }
            }


        }
    }
}


