using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace FavoriteMagnet
{
    public class AttractionInfoDisplay : InfoDisplay
    {
        public override bool Active()
        {
            return true;
        }

        public override string DisplayValue()
        {
            Player p = Main.LocalPlayer;
            int m = 0;
            bool i = false, e = false;
            string key1 = "Mods.FavoriteMagnet.Info.", key2 = "";
            if (p.active)
            {
                m = p.GetModPlayer<AttractivePlayer>().attractionMode;
                i = p.GetModPlayer<AttractivePlayer>().invert;
                e = p.GetModPlayer<AttractivePlayer>().encumbered;
            }
            if (m == 0)
            {
                key2 = "Not";
            }
            if (m == 1)
            {
                key2 = "Favorite";
            }
            if (m == 2)
            {
                key2 = "Inventory";
            }
            if (m == 3)
            {
                key2 = "All";
            }
            string inv = i ? Language.GetTextValue(key1 + "Invert") : "";
            string mode = Language.GetTextValue(key1 + key2);
            string encum = e ? Language.GetTextValue(key1 + "Encumbered") : "";
            string info = Language.GetTextValue("Mods.FavoriteMagnet.Info.AttractionInfo", inv, mode, encum);
            return info;
        }
    }
}
