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
        public static Dictionary<Player, (int, List<Item>)> itemEachPLayer = new Dictionary<Player, (int, List<Item>)>();

        public override void PostUpdateTime()
        {
            timer = Math.Max(++timer, 0);
        }
        public override void PreUpdateItems()
        {
            rangeSqrd = 5400;
            if (timer % 30 == 0 && itemEachPLayer.Keys.Count > 0)
            {
                foreach (Player player in itemEachPLayer.Keys)
                {
                    (int, List<Item>) sample = new();
                    itemEachPLayer.TryGetValue(player, out sample);
                    foreach (Item item in Main.item)
                    {
                        float disSqrd = Vector2.DistanceSquared(item.Center, player.Center);
                        if (disSqrd <= rangeSqrd)
                        {
                            if (sample.Item1 == 3 || sample.Item2.Contains(item))
                            {
                                if (item.active && !item.GetGlobalItem<AttractItem>().isAttractedBy.Contains(player))
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
