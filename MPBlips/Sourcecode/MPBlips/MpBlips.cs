using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace MPBlips
{
    public class MPBlip : Script
    {
        int pos;
        Blip[] BlipArray = new Blip[32];
        Player playerMp = null;
        int waitModifier;
        //Model[] PoliceModelsArray = new Model[] { "M_Y_COP", "M_Y_COP_TRAFFIC", "CS_MITCHCOP", "M_M_FATCOP_01", "M_M_FBI", "M_Y_BOUNCER_01", "M_Y_BOUNCER_02", "M_M_ARMOURED", "IG_FRANCIS_MC", "M_Y_NHELIPILOT", "M_Y_SWAT", "M_Y_STROOPER" };

        public MPBlip()
        {
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            Interval = Settings.GetValueInteger("INTERVAL", "SETTINGS", 100);
            this.Tick += new EventHandler(MPBlip_Tick);
        }

        void MPBlip_Tick(object sender, EventArgs e)
        {
            Player[] playerList = Game.PlayerList;

            foreach (var playerMp in playerList)
            {
                this.playerMp = playerMp;
                pos = Array.IndexOf(playerList, playerMp);
                if (Game.LocalPlayer.Character != playerMp)
                {
                    if (Game.LocalPlayer.Character.Model == "M_Y_COP" || Game.LocalPlayer.Character.Model == "M_Y_COP_TRAFFIC" || Game.LocalPlayer.Character.Model == "CS_MITCHCOP" || Game.LocalPlayer.Character.Model == "M_M_FATCOP_01" || Game.LocalPlayer.Character.Model == "M_M_FBI" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_01" || Game.LocalPlayer.Character.Model == "M_Y_BOUNCER_02" || Game.LocalPlayer.Character.Model == "M_M_ARMOURED" || Game.LocalPlayer.Character.Model == "IG_FRANCIS_MC" || Game.LocalPlayer.Character.Model == "M_Y_NHELIPILOT" || Game.LocalPlayer.Character.Model == "M_Y_SWAT" || Game.LocalPlayer.Character.Model == "M_Y_STROOPER") //-- Is LocalPlayer a cop?
                    {
                        CreateCopBlip();
                    }
                    else
                    {
                        CreateCrookBlip();
                    }
                }
            }
        }
        void CreateCopBlip()
        {
            if (this.playerMp.Character.Model == "M_Y_COP" || this.playerMp.Character.Model == "M_Y_COP_TRAFFIC" || this.playerMp.Character.Model == "CS_MITCHCOP" || this.playerMp.Character.Model == "M_M_FATCOP_01" || this.playerMp.Character.Model == "M_M_FBI" || this.playerMp.Character.Model == "M_Y_BOUNCER_01" || this.playerMp.Character.Model == "M_Y_BOUNCER_02" || this.playerMp.Character.Model == "M_M_ARMOURED" || this.playerMp.Character.Model == "IG_FRANCIS_MC" || this.playerMp.Character.Model == "M_Y_NHELIPILOT" || this.playerMp.Character.Model == "M_Y_SWAT" || this.playerMp.Character.Model == "M_Y_STROOPER")
            {//-- How LocalPlayer Cop sees other player cops
                if (this.playerMp.Character.isAlive && ((Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 150) || // cops see cops within 150
                    (this.playerMp.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 200) || // Cops see cops within 200 while other in vehicle
                    (this.playerMp.Character.isInVehicle() && this.playerMp.Character.CurrentVehicle.SirenActive && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 250) ||
                    (Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.SirenActive && this.playerMp.Character.isInVehicle() && this.playerMp.Character.CurrentVehicle.SirenActive && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 300) ||
                    (this.playerMp.Character.isShooting && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 300)))
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle()) // is cop in vehicle?
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Icon = BlipIcon.Misc_TaxiRank;
                        BlipArray[pos].Color = BlipColor.Turquoise;
                        BlipArray[pos].Scale = 1.0f;
                        BlipArray[pos].Name = "Officer in vehicle";
                        if (Game.Exists(this.playerMp.Character) && Game.Exists(this.playerMp.Character.CurrentVehicle) && this.playerMp.Character.CurrentVehicle.SirenActive)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 1.0f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Name = "Officer in vehicle";
                            BlipArray[pos].Color = BlipColor.Cyan;
                            Wait(100);
                            BlipArray[pos].Color = BlipColor.Red;
                            Wait(100);
                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                    else
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Color = BlipColor.DarkTurquoise;
                        BlipArray[pos].Scale = 0.8f;
                        BlipArray[pos].Name = "Officer on foot";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }

                }
                else
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    if (Game.Exists(BlipArray[pos]))
                    {
                        BlipArray[pos].Delete();
                    }
                }
            }
            else // -- How LocalPlayer Cop see Criminal Players
            {
                //jitsuin
                if (this.playerMp.Character.isAlive && (((this.playerMp.Character.isShooting || (this.playerMp.Character.isInVehicle() && this.playerMp.Character.CurrentVehicle.Speed > 60) || this.playerMp.WantedLevel == 1) && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 250) ||
                    (this.playerMp.Character.isOnScreen && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 120) ||
                    (this.playerMp.Character.isInVehicle() && this.playerMp.Character.isOnScreen && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 180) ||
                    (this.playerMp.WantedLevel == 2 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 400) ||
                    (this.playerMp.Character.isOnScreen && this.playerMp.Character.isInVehicle() && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.SirenActive && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 250) ||
                    (this.playerMp.Character.isOnScreen && this.playerMp.Character.isInVehicle() && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 200) ||
                    (this.playerMp.Character.isOnScreen && !(this.playerMp.Character.isInVehicle()) && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.CurrentVehicle.SirenActive && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 180) ||
                    (this.playerMp.Character.isOnScreen && !(this.playerMp.Character.isInVehicle()) && Game.LocalPlayer.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 150) ||
                    (this.playerMp.WantedLevel == 3 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 500) ||
                    (this.playerMp.WantedLevel == 4 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 600) ||
                    (this.playerMp.WantedLevel == 5 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 850) ||
                    (this.playerMp.WantedLevel == 6 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 50000)))
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle())
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Icon = BlipIcon.Misc_Objective;
                        BlipArray[pos].Scale = 0.7f;
                        BlipArray[pos].Color = BlipColor.DarkRed;
                        BlipArray[pos].Name = "Crook in vehicle";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle() && this.playerMp.WantedLevel > 0)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Wanted player in a vehicle in your area! Arrest him!", 5000, 1 });
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 1.0f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                            BlipArray[pos].Color = BlipColor.DarkRed;
                            BlipArray[pos].Name = "Crook wanted in vehicle";
                            if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel >= 4)
                            {
                                if (Game.Exists(BlipArray[pos]))
                                {
                                    BlipArray[pos].Delete();
                                }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Extremely dangerous wanted player in a vehicle around your area! Arrest with caution!", 5000, 1 });
                                BlipArray[pos] = this.playerMp.Character.AttachBlip();
                                BlipArray[pos].Scale = 1.0f;
                                BlipArray[pos].Display = BlipDisplay.MapOnly;
                                BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                                BlipArray[pos].Name = "Crook highly wanted in vehicle";
                                BlipArray[pos].Color = BlipColor.DarkRed;
                                Wait(100);
                                BlipArray[pos].Color = BlipColor.Red;
                                Wait(100);
                            }

                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                    else
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Color = BlipColor.DarkRed;
                        BlipArray[pos].Scale = 0.8f;
                        BlipArray[pos].Name = "Crook on foot";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel > 0)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Wanted player on foot in your area! Arrest him!", 5000, 1 });
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 0.8f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                            BlipArray[pos].Color = BlipColor.DarkRed;
                            BlipArray[pos].Name = "Crook wanted on foot";
                            if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel >= 4)
                            {
                                if (Game.Exists(BlipArray[pos]))
                                {
                                    BlipArray[pos].Delete();
                                }
                                Function.Call("PRINT_STRING_WITH_LITERAL_STRING_NOW", new Parameter[] { "STRING", "Extremely dangerous wanted player on foot around your area! Arrest with caution!", 5000, 1 });
                                BlipArray[pos] = this.playerMp.Character.AttachBlip();
                                BlipArray[pos].Scale = 0.8f;
                                BlipArray[pos].Display = BlipDisplay.MapOnly;
                                BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                                BlipArray[pos].Name = "Crook highly wanted on foot";
                                BlipArray[pos].Color = BlipColor.DarkRed;
                                Wait(100);
                                BlipArray[pos].Color = BlipColor.Red;
                                Wait(100);
                            }
                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }

                }
                else
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    if (Game.Exists(BlipArray[pos]))
                    {
                        BlipArray[pos].Delete();
                    }
                }
            }

        }
        void CreateCrookBlip() // How LocalPlayer Criminal sees other Player Cops
        {
            if (this.playerMp.Character.Model == "M_Y_COP" || this.playerMp.Character.Model == "M_Y_COP_TRAFFIC" || this.playerMp.Character.Model == "CS_MITCHCOP" || this.playerMp.Character.Model == "M_M_FATCOP_01" || this.playerMp.Character.Model == "M_M_FBI" || this.playerMp.Character.Model == "M_Y_BOUNCER_01" || this.playerMp.Character.Model == "M_Y_BOUNCER_02" || this.playerMp.Character.Model == "M_M_ARMOURED" || this.playerMp.Character.Model == "IG_FRANCIS_MC" || this.playerMp.Character.Model == "M_Y_NHELIPILOT" || this.playerMp.Character.Model == "M_Y_SWAT" || this.playerMp.Character.Model == "M_Y_STROOPER")
            {
                if (this.playerMp.Character.isAlive && ((this.playerMp.Character.isShooting && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 200) ||
                        (this.playerMp.Character.isOnScreen && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 100) ||
                        (Game.LocalPlayer.WantedLevel > 0 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 50) ||
                        (Game.LocalPlayer.WantedLevel > 0 && this.playerMp.Character.isInVehicle() && this.playerMp.Character.CurrentVehicle.SirenActive && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 150)))
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle()) // is Criminal in vehicle?
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Icon = BlipIcon.Misc_TaxiRank;
                        BlipArray[pos].Color = BlipColor.Turquoise;
                        BlipArray[pos].Scale = 1.0f;
                        BlipArray[pos].Name = "Officer in vehicle";
                        if (Game.Exists(this.playerMp.Character) && Game.Exists(this.playerMp.Character.CurrentVehicle) && this.playerMp.Character.CurrentVehicle.SirenActive)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 1.0f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Name = "Officer in vehicle";
                            BlipArray[pos].Color = BlipColor.Cyan;
                            Wait(100);
                            BlipArray[pos].Color = BlipColor.Red;
                            Wait(100);
                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                    else
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Color = BlipColor.DarkTurquoise;
                        BlipArray[pos].Scale = 0.8f;
                        BlipArray[pos].Name = "Officer on foot";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                }
                else
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    if (Game.Exists(BlipArray[pos]))
                    {
                        BlipArray[pos].Delete();
                    }
                }
            }
            else  // How LocalPlayer Criminal sees other Player Cops
            {
                if (this.playerMp.Character.isAlive && (((this.playerMp.Character.isShooting || this.playerMp.WantedLevel == 1) && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 300) ||
                (this.playerMp.Character.isOnScreen && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 100) ||
                (this.playerMp.Character.isInVehicle() && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 150) ||
                (Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 60) ||
                (this.playerMp.WantedLevel == 2 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 350) ||
                (this.playerMp.WantedLevel == 3 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 400) ||
                (this.playerMp.WantedLevel == 4 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 550) ||
                (this.playerMp.WantedLevel == 5 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 700) ||
                (this.playerMp.WantedLevel == 6 && Game.LocalPlayer.Character.Position.DistanceTo2D(this.playerMp.Character.Position) <= 50000)))
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle())
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Icon = BlipIcon.Misc_Objective;
                        BlipArray[pos].Scale = 0.7f;
                        BlipArray[pos].Color = BlipColor.DarkRed;
                        BlipArray[pos].Name = "Crook in vehicle";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isInVehicle() && this.playerMp.WantedLevel > 0)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 1.0f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                            BlipArray[pos].Color = BlipColor.DarkRed;
                            BlipArray[pos].Name = "Crook wanted in vehicle";
                            if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel >= 4)
                            {
                                if (Game.Exists(BlipArray[pos]))
                                {
                                    BlipArray[pos].Delete();
                                }
                                BlipArray[pos] = this.playerMp.Character.AttachBlip();
                                BlipArray[pos].Scale = 1.0f;
                                BlipArray[pos].Display = BlipDisplay.MapOnly;
                                BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                                BlipArray[pos].Name = "Crook highly wanted in vehicle";
                                BlipArray[pos].Color = BlipColor.DarkRed;
                                Wait(100);
                                BlipArray[pos].Color = BlipColor.Red;
                                Wait(100);
                            }

                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                    else
                    {
                        if (Game.Exists(BlipArray[pos]))
                        {
                            BlipArray[pos].Delete();
                        }
                        BlipArray[pos] = this.playerMp.Character.AttachBlip();
                        BlipArray[pos].Display = BlipDisplay.MapOnly;
                        BlipArray[pos].Color = BlipColor.DarkRed;
                        BlipArray[pos].Scale = 0.8f;
                        BlipArray[pos].Name = "Crook on foot";
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel > 0)
                        {
                            if (Game.Exists(BlipArray[pos]))
                            {
                                BlipArray[pos].Delete();
                            }
                            BlipArray[pos] = this.playerMp.Character.AttachBlip();
                            BlipArray[pos].Scale = 0.8f;
                            BlipArray[pos].Display = BlipDisplay.MapOnly;
                            BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                            BlipArray[pos].Color = BlipColor.DarkRed;
                            BlipArray[pos].Name = "Crook wanted on foot";
                            if (Game.Exists(this.playerMp.Character) && this.playerMp.WantedLevel >= 4)
                            {
                                if (Game.Exists(BlipArray[pos]))
                                {
                                    BlipArray[pos].Delete();
                                }
                                BlipArray[pos] = this.playerMp.Character.AttachBlip();
                                BlipArray[pos].Scale = 0.8f;
                                BlipArray[pos].Display = BlipDisplay.MapOnly;
                                BlipArray[pos].Icon = BlipIcon.Person_Assassin;
                                BlipArray[pos].Name = "Crook highly wanted on foot";
                                BlipArray[pos].Color = BlipColor.DarkRed;
                                Wait(100);
                                BlipArray[pos].Color = BlipColor.Red;
                                Wait(100);
                            }
                        }
                        if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    }
                }
                else
                {
                    if (Game.Exists(this.playerMp.Character) && this.playerMp.Character.isDead && Game.Exists(BlipArray[pos])) { BlipArray[pos].Delete(); }
                    if (Game.Exists(BlipArray[pos]))
                    {
                        BlipArray[pos].Delete();
                    }
                }
            }
            }
    }
}
