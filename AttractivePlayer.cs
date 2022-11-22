using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FavoriteMagnet
{
    public class AttractivePlayer : ModPlayer
    {
        /// <summary>
        /// 吸取物品的模式
        /// <para>0 不吸</para>
        /// <para>1 吸收藏</para>
        /// <para>2 吸背包里的</para>
        /// <para>3 吸所有</para>
        /// </summary>
        public int attractionMode;

        public List<Item> itemToAttract = new List<Item>();

        public override void ResetEffects()
        {
            //attractionMode = 0;
            itemToAttract = new List<Item>();
        }

        public override void PreUpdateBuffs()
        {
            if (attractionMode == 0)
            {
                if (AttractionSystem.itemEachPLayer.ContainsKey(Player))
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                }
            }
            if (attractionMode == 1)
            {
                foreach (Item item in Player.inventory)
                {
                    if (item.favorited)
                    {
                        itemToAttract.Add(item);
                    }
                }
                if (itemToAttract.Count() > 0)
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                    AttractionSystem.itemEachPLayer.TryAdd(Player, (attractionMode, itemToAttract));
                }
            }
            if (attractionMode == 2)
            {
                foreach (Item item in Player.inventory)
                {
                    itemToAttract.Add(item);
                }
                if (itemToAttract.Count() > 0)
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                    AttractionSystem.itemEachPLayer.TryAdd(Player, (attractionMode, itemToAttract));
                }
            }
            if (attractionMode == 3)
            {
                AttractionSystem.itemEachPLayer.Remove(Player);
                AttractionSystem.itemEachPLayer.TryAdd(Player, (attractionMode, itemToAttract));
            }
        }
    }
}
