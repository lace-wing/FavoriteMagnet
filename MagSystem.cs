﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace FavoriteMagnet
{
    internal class MagSystem : ModSystem
    {
        public static bool Timely => Main.GameUpdateCount % 30 == 0;
        public static List<Item> ItemPool = new List<Item>();
        public override void PreUpdatePlayers()
        {
            if (Timely)
            {
                foreach(Item item in Main.item)
                {
                    if (!item.TryGetGlobalItem(out MagItem mag))
                    {
                        continue;
                    }
                    mag.Target = null;
                }
                ItemPool = Main.item.ToList();
            }
        }
    }
}
