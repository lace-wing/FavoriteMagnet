﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace ItemMagnetPro
{
    public class MagPlayer : ModPlayer
    {
        public EnumNum<Select> Selection = new EnumNum<Select>();
        public EnumNum<ItemAction> ItemBehavior = new EnumNum<ItemAction>();
        private bool ItemFitsInventory(Item item, bool ignoreVanillaEncumber = true)
        {
            var status = Player.ItemSpace(item);
            if (status.CanTakeItem)
            {
                return ignoreVanillaEncumber || (Player.preventAllItemPickups && ItemID.Sets.IgnoresEncumberingStone[item.type]);
            }
            return false;
        }
        private bool SelectFavorited(Item item)
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
        private bool SelectExisting(Item item)
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
        private bool IsItemSelected(Item item)
        {
            if (Selection == Select.All)
            {
                return true;
            }
            if (Selection == Select.Favorite)
            {
                return SelectFavorited(item);
            }
            if (Selection == Select.NotFavorite)
            {
                return !SelectFavorited(item);
            }
            if (Selection == Select.Existing)
            {
                return SelectExisting(item);
            }
            if (Selection == Select.NotExisting)
            {
                return !SelectExisting(item);
            }
            return false;
        }
        private bool CanAttract(Item item)
        {
            if (item.position.DistanceSQ(Player.Center) > 256 * 256)
            {
                return false;
            }
            if (!item.TryGetGlobalItem(out MagItem magItem) || (item.playerIndexTheItemIsReservedFor == Player.whoAmI && magItem.ReserveCD > 0))
            {
                return false;
            }
            if (ItemFitsInventory(item))
            {
                return IsItemSelected(item);
            }
            return false;
        }
        private void TargetItems()
        {
            List<Item> pool = MagSystem.ItemPool;
            Item item;
            for (int i = pool.Count - 1; i >= 0; i--)
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
            if (MagSystem.Timely && Selection != Select.None)
            {
                TargetItems();
            }
        }
        public override void SaveData(TagCompound tag)
        {
            tag.Add("Selection", Selection.Index);
        }
        public override void LoadData(TagCompound tag)
        {
            Selection.Index = tag.GetInt("Selection");
        }
    }
    public enum Select
    {
        All,
        None,
        Favorite,
        NotFavorite,
        Existing,
        NotExisting
    }
    public enum ItemAction
    {
        None,
        Encumber,
        Exhaust
    }
}
