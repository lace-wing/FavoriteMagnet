using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FavoriteMagnet
{
    public class AttractionSystem : ModSystem
    {
        public static int timer;

        public static int rangeSqrd;
        public static Dictionary<Player, (int, List<int>)> itemEachPLayer = new Dictionary<Player, (int, List<int>)>();

        public override void PostUpdateTime()
        {
            timer = Math.Max(++timer, 0);
        }
        public override void PreUpdateItems()
        {
            rangeSqrd = (int)Math.Pow(MagnetConfig.config.range * 16, 2);
            if (timer % 30 == 0 && itemEachPLayer.Keys.Count > 0)
            {
                foreach (Player player in itemEachPLayer.Keys)
                {
                    (int, List<int>) sample = new();
                    itemEachPLayer.TryGetValue(player, out sample);
                    foreach (Item item in Main.item)
                    {
                        float disSqrd = Vector2.DistanceSquared(item.Center, player.Center);
                        if (disSqrd <= rangeSqrd)
                        {
                            if (!item.IsAir)
                            {
                                if (sample.Item1 == 1 || (sample.Item1 == 0 && sample.Item2.Contains(item.type)) || (sample.Item1 == 2 && !sample.Item2.Contains(item.type)))
                                {
                                    if (!item.GetGlobalItem<AttractItem>().isAttractedBy.Contains(player))
                                    {
                                        item.GetGlobalItem<AttractItem>().isAttractedBy.Add(player);
                                        item.GetGlobalItem<AttractItem>().disesSqrd.Add(disSqrd);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
