using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader.IO;

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
        public int AttractionMode;
        public bool Encumbered;
        public bool Invert;
        public int Selection;

        public List<int> ItemToAttract = new List<int>();

        public override void ResetEffects()
        {
            Selection = 0;
            ItemToAttract = new List<int>();
        }

        public override void UpdateEquips()
        {
            if (Invert && AttractionMode == 0)
            {
                Invert = false;
                AttractionMode = 3;
            }
            if (Invert && AttractionMode == 3)
            {
                Invert = false;
                AttractionMode = 0;
            }

            if (Invert)
            {
                Selection = 2;
            }

            if (AttractionMode == 0)
            {
                if (AttractionSystem.itemEachPLayer.ContainsKey(Player))
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                }
            }
            if (AttractionMode == 1)
            {
                foreach (Item item in Player.inventory)
                {
                    if (item.favorited)
                    {
                        ItemToAttract.Add(item.type);
                    }
                }
                if (ItemToAttract.Count() > 0)
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                    AttractionSystem.itemEachPLayer.TryAdd(Player, (Selection, ItemToAttract));
                }
            }
            if (AttractionMode == 2)
            {
                foreach (Item item in Player.inventory)
                {
                    ItemToAttract.Add(item.type);
                }
                if (ItemToAttract.Count() > 0)
                {
                    AttractionSystem.itemEachPLayer.Remove(Player);
                    AttractionSystem.itemEachPLayer.TryAdd(Player, (Selection, ItemToAttract));
                }
            }
            if (AttractionMode == 3)
            {
                Selection = 1;
                AttractionSystem.itemEachPLayer.Remove(Player);
                AttractionSystem.itemEachPLayer.TryAdd(Player, (Selection, ItemToAttract));
            }
        }
        public override void Load()
        {
            AttractionMode = 0;
            Encumbered = false;
            Invert = false;
        }
        public override void Unload()
        {
            AttractionMode = 0;
            Encumbered = false;
            Invert = false;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["AttractionMode"] = AttractionMode;
            tag["Encumbered"] = Encumbered;
            tag["Invert"] = Invert;
        }
        public override void LoadData(TagCompound tag)
        {
            AttractionMode = tag.GetInt("AttractionMode");
            Encumbered = tag.GetBool("Encumbered");
            Invert = tag.GetBool("Invert");
        }
    }
}
