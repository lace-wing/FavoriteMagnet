using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace FavoriteMagnet
{
    internal class MagItem : GlobalItem
    {
        public Player Target;
        public Player Holder;
        public int HolderCD;
        public override bool InstancePerEntity => true;

        private Vector2 CalcVelocity(Item item)
        {
            float mult = Math.Clamp(Target.Center.DistanceSQ(item.position), 32, 320);
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
            if (Target != null)
            {
                TargetMode(item, ref gravity, ref maxFallSpeed);
                item.position += CalcVelocity(item);
            }
        }
        public override void UpdateInventory(Item item, Player player)
        {
            Holder = player;
            HolderCD = 180;
            Target = null;
        }
    }
}
