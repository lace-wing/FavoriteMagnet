using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Differencing;
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
        public int delay;
        public Player delayPlayer;
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            while (isAttractedBy.Count > 0)
            {
                int index = disesSqrd.IndexOf(disesSqrd.Min());
                Player plr = isAttractedBy[index];
                bool canGoTo = plr != delayPlayer || delay <= 0;
                if (plr != null && canGoTo && !plr.dead)
                {
                    target = plr;
                    break;
                }
                else
                {
                    isAttractedBy.Remove(isAttractedBy[index]);
                    disesSqrd.Remove(disesSqrd[index]);
                }
            }

            delay = Math.Max(--delay, 0);

            if (target != null)
            {
                bool vacant = false;
                int max = 50;
                if (item.IsACoin)
                {
                    max = 54;
                }
                for (int i = 0; i < max; i++)
                {
                    if (target.inventory[i].IsAir || (target.inventory[i].type == item.type && target.inventory[i].stack < item.maxStack))
                    {
                        vacant = true;
                        break;
                    }
                }
                if (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane || item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum || item.type == ItemID.ManaCloakStar)
                {
                    vacant = true;
                }
                if (vacant)
                {
                    gravity = 0;
                    Vector2 v = Vector2.Normalize(target.Center - item.Center) * Math.Clamp(Vector2.DistanceSquared(target.Center, item.Center) * 256 / AttractionSystem.rangeSqrd, 7.2f, 36f);
                    item.velocity *= 0.98f;
                    item.position += v;
                    if (AttractionSystem.timer % 10 == 0)
                    {
                        Dust dust = Dust.NewDustPerfect(item.position, DustID.AncientLight);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Zero;
                    }
                }
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            delay = MagnetConfig.config.delaySecond * 60;
            delayPlayer = player;
            isAttractedBy.Clear();
            disesSqrd.Clear();
            target = null;
        }

        public override bool OnPickup(Item item, Player player)
        {
            delay = MagnetConfig.config.delaySecond * 60;
            delayPlayer = player;
            isAttractedBy.Clear();
            disesSqrd.Clear();
            target = null;
            return base.OnPickup(item, player);
        }

        public override bool CanPickup(Item item, Player player)
        {
            if ((delayPlayer != player || delay <= 0) && target == player)
            {
                return true;
            }
            else if (item.type == ItemID.Heart || item.type == ItemID.CandyApple || item.type == ItemID.CandyCane || item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum || item.type == ItemID.ManaCloakStar)
            {
                return true;
            }
            else if (player.active && player.GetModPlayer<AttractivePlayer>().Encumbered)
            {
                return false;
            }
            return base.CanPickup(item, player);
        }
    }
}
