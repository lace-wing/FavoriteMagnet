using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace FavoriteMagnet.InfoDisplays
{
    public class AttractionInfoDisplay : InfoDisplay
    {
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
            return Display.Value;
        }
    }
}
