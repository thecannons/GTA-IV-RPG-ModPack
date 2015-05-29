using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GTA;

namespace WeaponVendors
{
    public class GunMenu : Script
    {
        public static int pages = 0;
        public static int selectedmenu = 0;
        int waitModifier;

        public GunMenu()
        {
            waitModifier = Settings.GetValueInteger("DELAY_SCRIPT", "SETTINGS", 0);
            Wait(waitModifier);
            base.PerFrameDrawing += new GraphicsEventHandler(this.gfxDraw);

        }
        public void gfxDraw(object sender, GraphicsEventArgs e)
        {
            RectangleF radarRectangle = e.Graphics.GetRadarRectangle(FontScaling.Pixel);
            string[] AvailableWeaponsInWepShop = null;
            string[] WeaponNameShop = null;
            int[] WeaponPriceShop = null;
            string text = null;
            Color color = new Color();
            int WeaponsInWepShop = 0;
            if (KeyLayout.wepshopnumber == 1)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop1;
                WeaponNameShop = WeaponVendorsCS.WeaponNameShop1;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop1;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop1;
                color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            }
            else if (KeyLayout.wepshopnumber == 2)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop2;
                WeaponNameShop = WeaponVendorsCS.WeaponNameShop2;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop2;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop2;
                color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            }
            else if (KeyLayout.wepshopnumber == 3)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop3;
                WeaponNameShop = WeaponVendorsCS.WeaponNameShop3;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop3;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop3;
                color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            }
            else if (KeyLayout.wepshopnumber == 4)
            {
                AvailableWeaponsInWepShop = WeaponVendorsCS.AvailableWeaponsInWepShop4;
                WeaponNameShop = WeaponVendorsCS.WeaponNameShop4;
                WeaponPriceShop = WeaponVendorsCS.WeaponPriceShop4;
                WeaponsInWepShop = WeaponVendorsCS.WeaponsInWepShop4;
                color = Color.FromArgb(WeaponVendorsCS.MenuTransparency, WeaponVendorsCS.MenuColorW1);
            }
            if (WeaponVendorsCS.ShowMenues)
            {
                e.Graphics.DrawRectangle(radarRectangle.Width * 1f, radarRectangle.Height * 1.35f, radarRectangle.Width * 1.7f, radarRectangle.Height * 2.1f, color);
                float x = radarRectangle.Width * 1.35f;
                float num3 = radarRectangle.Width * 0.25f;
                float num4 = radarRectangle.Height * 0.25f;
                float num5 = radarRectangle.Height * 0.1f;
                if (WeaponsInWepShop > (20 * pages))
                {
                    if (selectedmenu == 0)
                    {
                        e.Graphics.DrawText(WeaponNameShop[20 * pages], num3, (float)(num4 + (num5 * 1f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[20 * pages].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 1f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[20 * pages], num3, (float)(num4 + (num5 * 1f)), Color.White);
                        text = "$" + WeaponPriceShop[20 * pages].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 1f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (1 + (20 * pages)))
                {
                    if (selectedmenu == 1)
                    {
                        e.Graphics.DrawText(WeaponNameShop[1 + (20 * pages)], num3, (float)(num4 + (num5 * 2f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[1 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 2f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[1 + (20 * pages)], num3, (float)(num4 + (num5 * 2f)), Color.White);
                        text = "$" + WeaponPriceShop[1 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 2f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (2 + (20 * pages)))
                {
                    if (selectedmenu == 2)
                    {
                        e.Graphics.DrawText(WeaponNameShop[2 + (20 * pages)], num3, (float)(num4 + (num5 * 3f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[2 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 3f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[2 + (20 * pages)], num3, (float)(num4 + (num5 * 3f)), Color.White);
                        text = "$" + WeaponPriceShop[2 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 3f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (3 + (20 * pages)))
                {
                    if (selectedmenu == 3)
                    {
                        e.Graphics.DrawText(WeaponNameShop[3 + (20 * pages)], num3, (float)(num4 + (num5 * 4f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[3 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 4f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[3 + (20 * pages)], num3, (float)(num4 + (num5 * 4f)), Color.White);
                        text = "$" + WeaponPriceShop[3 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 4f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (4 + (20 * pages)))
                {
                    if (selectedmenu == 4)
                    {
                        e.Graphics.DrawText(WeaponNameShop[4 + (20 * pages)], num3, (float)(num4 + (num5 * 5f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[4 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 5f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[4 + (20 * pages)], num3, (float)(num4 + (num5 * 5f)), Color.White);
                        text = "$" + WeaponPriceShop[4 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 5f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (5 + (20 * pages)))
                {
                    if (selectedmenu == 5)
                    {
                        e.Graphics.DrawText(WeaponNameShop[5 + (20 * pages)], num3, (float)(num4 + (num5 * 6f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[5 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 6f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[5 + (20 * pages)], num3, (float)(num4 + (num5 * 6f)), Color.White);
                        text = "$" + WeaponPriceShop[5 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 6f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (6 + (20 * pages)))
                {
                    if (selectedmenu == 6)
                    {
                        e.Graphics.DrawText(WeaponNameShop[6 + (20 * pages)], num3, (float)(num4 + (num5 * 7f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[6 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 7f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[6 + (20 * pages)], num3, (float)(num4 + (num5 * 7f)), Color.White);
                        text = "$" + WeaponPriceShop[6 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 7f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (7 + (20 * pages)))
                {
                    if (selectedmenu == 7)
                    {
                        e.Graphics.DrawText(WeaponNameShop[7 + (20 * pages)], num3, (float)(num4 + (num5 * 8f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[7 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 8f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[7 + (20 * pages)], num3, (float)(num4 + (num5 * 8f)), Color.White);
                        text = "$" + WeaponPriceShop[7 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 8f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (8 + (20 * pages)))
                {
                    if (selectedmenu == 8)
                    {
                        e.Graphics.DrawText(WeaponNameShop[8 + (20 * pages)], num3, (float)(num4 + (num5 * 9f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[8 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 9f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[8 + (20 * pages)], num3, (float)(num4 + (num5 * 9f)), Color.White);
                        text = "$" + WeaponPriceShop[8 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 9f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (9 + (20 * pages)))
                {
                    if (selectedmenu == 9)
                    {
                        e.Graphics.DrawText(WeaponNameShop[9 + (20 * pages)], num3, (float)(num4 + (num5 * 10f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[9 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 10f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[9 + (20 * pages)], num3, (float)(num4 + (num5 * 10f)), Color.White);
                        text = "$" + WeaponPriceShop[9 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 10f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (10 + (20 * pages)))
                {
                    if (selectedmenu == 10)
                    {
                        e.Graphics.DrawText(WeaponNameShop[10 + (20 * pages)], num3, (float)(num4 + (num5 * 11f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[10 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 11f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[10 + (20 * pages)], num3, (float)(num4 + (num5 * 11f)), Color.White);
                        text = "$" + WeaponPriceShop[10 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 11f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (11 + (20 * pages)))
                {
                    if (selectedmenu == 11)
                    {
                        e.Graphics.DrawText(WeaponNameShop[11 + (20 * pages)], num3, (float)(num4 + (num5 * 12f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[11 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 12f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[11 + (20 * pages)], num3, (float)(num4 + (num5 * 12f)), Color.White);
                        text = "$" + WeaponPriceShop[11 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 12f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (12 + (20 * pages)))
                {
                    if (selectedmenu == 12)
                    {
                        e.Graphics.DrawText(WeaponNameShop[12 + (20 * pages)], num3, (float)(num4 + (num5 * 13f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[12 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 13f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[12 + (20 * pages)], num3, (float)(num4 + (num5 * 13f)), Color.White);
                        text = "$" + WeaponPriceShop[12 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 13f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (13 + (20 * pages)))
                {
                    if (selectedmenu == 13)
                    {
                        e.Graphics.DrawText(WeaponNameShop[13 + (20 * pages)], num3, (float)(num4 + (num5 * 14f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[13 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 14f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[13 + (20 * pages)], num3, (float)(num4 + (num5 * 14f)), Color.White);
                        text = "$" + WeaponPriceShop[13 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 14f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (14 + (20 * pages)))
                {
                    if (selectedmenu == 14)
                    {
                        e.Graphics.DrawText(WeaponNameShop[14 + (20 * pages)], num3, (float)(num4 + (num5 * 15f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[14 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 15f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[14 + (20 * pages)], num3, (float)(num4 + (num5 * 15f)), Color.White);
                        text = "$" + WeaponPriceShop[14 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 15f)), Color.White);
                    }

                }
                if (WeaponsInWepShop > (15 + (20 * pages)))
                {
                    if (selectedmenu == 15)
                    {
                        e.Graphics.DrawText(WeaponNameShop[15 + (20 * pages)], num3, (float)(num4 + (num5 * 16f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[15 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 16f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[15 + (20 * pages)], num3, (float)(num4 + (num5 * 16f)), Color.White);
                        text = "$" + WeaponPriceShop[15 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 16f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (16 + (20 * pages)))
                {
                    if (selectedmenu == 16)
                    {
                        e.Graphics.DrawText(WeaponNameShop[16 + (20 * pages)], num3, (float)(num4 + (num5 * 17f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[16 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 17f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[16 + (20 * pages)], num3, (float)(num4 + (num5 * 17f)), Color.White);
                        text = "$" + WeaponPriceShop[16 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 17f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (17 + (20 * pages)))
                {
                    if (selectedmenu == 17)
                    {
                        e.Graphics.DrawText(WeaponNameShop[17 + (20 * pages)], num3, (float)(num4 + (num5 * 18f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[17 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 18f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[17 + (20 * pages)], num3, (float)(num4 + (num5 * 18f)), Color.White);
                        text = "$" + WeaponPriceShop[17 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 18f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (18 + (20 * pages)))
                {
                    if (selectedmenu == 18)
                    {
                        e.Graphics.DrawText(WeaponNameShop[18 + (20 * pages)], num3, (float)(num4 + (num5 * 19f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[18 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 19f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[18 + (20 * pages)], num3, (float)(num4 + (num5 * 19f)), Color.White);
                        text = "$" + WeaponPriceShop[18 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 19f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (19 + (20 * pages)))
                {
                    if (selectedmenu == 19)
                    {
                        e.Graphics.DrawText(WeaponNameShop[19 + (20 * pages)], num3, (float)(num4 + (num5 * 20f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[19 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 20f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[19 + (20 * pages)], num3, (float)(num4 + (num5 * 20f)), Color.White);
                        text = "$" + WeaponPriceShop[19 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 20f)), Color.White);
                    }
                }
                if (WeaponsInWepShop > (20 + (20 * pages)))
                {
                    if (selectedmenu == 20)
                    {
                        e.Graphics.DrawText(WeaponNameShop[20 + (20 * pages)], num3, (float)(num4 + (num5 * 21f)), Color.Aqua);
                        text = "$" + WeaponPriceShop[20 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 21f)), Color.Aqua);
                    }
                    else
                    {
                        e.Graphics.DrawText(WeaponNameShop[20 + (20 * pages)], num3, (float)(num4 + (num5 * 21f)), Color.White);
                        text = "$" + WeaponPriceShop[20 + (20 * pages)].ToString();
                        e.Graphics.DrawText(text, x, (float)(num4 + (num5 * 21f)), Color.White);
                    }
                }
            }
        }

    }
}
