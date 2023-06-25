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
    public class AttractionInfoDisplay : InfoDisplay
    {
        public readonly Color Encumber = Color.Yellow;
        public readonly Color Exhaust = Color.Red;
        public LocalizedText Display { get; set; }
        public override void Load()
        {
            _ = Display;
        }
        public override bool Active()
        {
            return true;
        }

        public override string DisplayValue(ref Color displayColor)
        {
            Player p = Main.LocalPlayer;
            Select selection = Select.None;
            bool invert = false, e = false;
            return p.GetModPlayer<MagPlayer>().Selection.Value.ToString();
        }
    }
}
