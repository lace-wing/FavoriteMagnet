using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace ItemMagnetPro.InfoDisplays
{
    public class AttractionDisplay : InfoDisplay
    {
        public static readonly Color Encumber = Color.Yellow;
        public static readonly Color Exhaust = Color.Red;
        public static readonly string SelectionKey = "Mods.ItemMagnetPro.InfoDisplays.Selections.";
        public override bool Active()
        {
            return true;
        }

        public override string DisplayValue(ref Color displayColor)
        {
            Player p = Main.LocalPlayer;
            var mp = p.GetModPlayer<MagPlayer>();
            if (mp.ItemBehavior == ItemAction.Encumber)
            {
                displayColor = Encumber;
            }
            else if (mp.ItemBehavior == ItemAction.Exhaust)
            {
                displayColor = Exhaust;
            }
            return Language.GetTextValue(SelectionKey + mp.Selection.Value);
        }
    }
}
