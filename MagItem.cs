using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ItemMagnetPro
{
    internal class MagItem : GlobalItem
    {
        public Player? Target;
        public int ReserveCD;
        public static int MaxSpeed = 36;
        public static int MinSpeed = 9;
        public override bool InstancePerEntity => true;

        private Vector2 CalcVelocity(Item item)
        {
            float mult = MathHelper.Lerp(MinSpeed, MaxSpeed, Target.Center.DistanceSQ(item.position) * (1f / MagPlayer.RangeSQ));
            return (Target.Center - item.position).SafeNormalize(Vector2.Zero) * mult;
        }
        private static void TargetMode(Item item, ref float gravity, ref float maxFallSpeed)
        {
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
                    Dust d = Dust.NewDustPerfect(item.position, DustID.AncientLight);
                    d.noGravity = true;
                }
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            ReserveCD = MagPlayer.Delay;
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
