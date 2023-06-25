using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace ItemMagnetPro
{
    internal class MagItem : GlobalItem
    {
        public Player? Target;
        public int ReserveCD;
        public override bool InstancePerEntity => true;

        private Vector2 CalcVelocity(Item item)
        {
            float mult = Math.Clamp(Target.Center.DistanceSQ(item.position) * 0.004f, 32, 320);
            return (Target.Center - item.position).SafeNormalize(Vector2.Zero) * mult;
        }
        private static void TargetMode(Item item, ref float gravity, ref float maxFallSpeed)
        {
            item.velocity *= 0.85f;
            gravity = 0;
            maxFallSpeed = 0;
        }

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            ReserveCD--;
            if (Target != null)
            {
                ReserveCD = 0;
                TargetMode(item, ref gravity, ref maxFallSpeed);
                item.position += CalcVelocity(item);
                if (MagSystem.Timely)
                {
                    Dust.NewDustPerfect(item.position, DustID.AncientLight);
                }
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            ReserveCD = 180;
            Target = null;
        }
        public override bool CanPickup(Item item, Player player)
        {
            if (Target != player && player.GetModPlayer<MagPlayer>().ItemBehavior == ItemAction.Encumber)
            {
                return false;
            }
            return base.CanPickup(item, player);
        }
        public override bool OnPickup(Item item, Player player)
        {
            if (Target == player && player.GetModPlayer<MagPlayer>().ItemBehavior == ItemAction.Exhaust)
            {
                item.TurnToAir();
                return false;
            }
            return base.OnPickup(item, player);
        }
    }
}
