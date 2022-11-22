using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace FavoriteMagnet
{
    [Label("$Mods.FavoriteMagnet.Config.Label")]
    public class MagnetConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static MagnetConfig config => ModContent.GetInstance<MagnetConfig>();

        public const string key = "$Mods.FavoriteMagnet.Config.";

        [Header("$Mods.FavoriteMagnet.Config.Header")]

        [Label(key + "Range.Label")]
        [Tooltip(key + "Range.Tooltip")]
        [Range(0, int.MaxValue)]
        [DefaultValue(64)]
        public int range;

        [Label(key + "Delay.Label")]
        [Tooltip(key + "Delay.Tooltip")]
        [Range(0, 60)]
        [DefaultValue(3)]
        public int delaySecond;
    }
}
