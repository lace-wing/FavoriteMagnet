using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace FavoriteMagnet
{
    public class AttractItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public List<Player> isAttractedBy = new List<Player>();
        public List<float> disesSqrd = new List<float>();
        public Player target;
        public Dictionary<Player, int> delay = new Dictionary<Player, int>();

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            //Main.NewText($"{isAttractedBy.Count}");
            for (int i = 0; i < isAttractedBy.Count; i++)
            {
                Player plr = isAttractedBy[i];
                int t = 180;
                bool canGoTo = !delay.TryGetValue(plr, out t) || t <= 0;
                if (canGoTo && plr.active)
                {
                    target = plr;
                }
                //if (plr1 != null && plr1.active && canGoTo && disesSqrd[i == isAttractedBy.Count ? i : i + 1] <= disesSqrd[i])
                //{
                //    target = isAttractedBy[i];
                //}
            }

            foreach (Player player in delay.Keys)
            {
                if (player == null)
                {
                    delay.Remove(player);
                }
                else
                {
                    delay[player] = Math.Max(--delay[player], 0);
                    if (item.type == ItemID.Torch)
                    {
                        Main.NewText(delay[player]);
                    }
                }
            }

            if (target != null)
            {
                bool vacant = false;
                for (int i = 0; i < 50; i++)
                {
                    if (target.inventory[i].IsAir)
                    {
                        vacant = true;
                        break;
                    }
                }
                if (vacant)
                {
                    //gravity = 0;
                    item.position += Vector2.Normalize(target.Center - item.Center) * Math.Max(Vector2.DistanceSquared(target.Center, item.Center), 128f) / AttractionSystem.rangeSqrd;
                }
            }

            if (item.type == ItemID.Torch && Main.time % 60 == 0)
            {
                string s = target == null ? "null" : target.name;
                // Main.NewText($"{isAttractedBy.Count} player is pulling, dis is {disesSqrd.Count}, target is {target.name}");\
                CombatText.NewText(item.Hitbox, Color.White, $"{s} 1");
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            delay.TryAdd(player, 180);
            if (delay[player] < 180)
            {
                delay[player] = 180;
            }

            isAttractedBy.Clear();
            disesSqrd.Clear();
            target = null;
        }
    }
}
