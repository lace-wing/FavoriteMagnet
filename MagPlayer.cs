using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace FavoriteMagnet
{
    public class MagPlayer : ModPlayer
    {
        public EnumNum<Select> Selection = new EnumNum<Select>();
        public bool Invert;
        private bool InvVacant
        {
            get
            {
                bool vacant = false;
                foreach(Item item in Player.inventory)
                {
                    if (item.IsAir)
                    {
                        vacant = true;
                        break;
                    }
                }
                return vacant;
            }
        }
        private bool VVVacant
        {
            get
            {
                if (!Player.IsVoidVaultEnabled)
                {
                    return false;
                }
                bool vacant = false;
                foreach(Item item in Player.bank4.item)
                {
                    if (item.IsAir)
                    {
                        vacant = true;
                        break;
                    }
                }
                return vacant;
            }
        }
        private bool ItemSelected(Item item)
        {
            if (Selection == Select.All)
            {
                return true;
            }
            if (Selection == Select.None)
            {
                return false;
            }
            if (Selection == Select.Existing)
            {
                foreach (Item i in Player.inventory)
                {
                    if (i.type == item.type)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (Selection == Select.Favorite)
            {
                foreach (Item i in Player.inventory)
                {
                    if (i.favorited && i.type == item.type)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        private bool CanAttract(Item item)
        {
            if (!item.TryGetGlobalItem(out MagItem magItem) || magItem.Holder == null || (magItem.Holder == Player && magItem.HolderCD > 0))
            {
                return false;
            }
            if (item.position.DistanceSQ(Player.Center) > 256)
            if (ItemID.Sets.IgnoresEncumberingStone.Contains() && !InvVacant && !VVVacant)
            {
                return false;
            }
            //TODO cursed vacancy checks
            return ItemSelected(item);
        }
        private void TargetItems()
        {
            List<Item> pool = MagSystem.ItemPool;
            Item item;
            for (int i = pool.Count; i >= 0; i--)
            {
                item = pool[i];
                if (CanAttract(item))
                {
                    item.GetGlobalItem<MagItem>().Target = Player;
                    pool.RemoveAt(i);
                }
            }
        }

        public override void UpdateEquips()
        {
            if (MagSystem.Timely)
            {
                TargetItems();
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Selection", Selection.Index);
            tag.Add("Invert", Invert);
        }
        public override void LoadData(TagCompound tag)
        {
            Selection.Index = tag.GetInt("Selection");
            Invert = tag.GetBool("Invert");
        }
    }
    public enum Select
    {
        None,
        Favorite,
        Existing,
        All
    }
}
