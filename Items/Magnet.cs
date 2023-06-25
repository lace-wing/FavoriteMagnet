using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ItemMagnetPro.Items
{
	public class Magnet : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Default;
			Item.width = 32;
			Item.height = 30;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 12;
			Item.value = 12;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.CoinPickup;
			Item.autoReuse = false;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
		{
			MagPlayer mag = player.GetModPlayer<MagPlayer>();
			if (player.itemAnimation == 1)
			{
				if (player.altFunctionUse == 2)
				{
					mag.Selection -= 1;
				}
				else
				{
					mag.Selection += 1;
				}
			}
			return base.UseItem(player);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override bool ConsumeItem(Player player)
		{
			return false;
		}

		public override void RightClick(Player player)
		{
			player.GetModPlayer<MagPlayer>().ItemBehavior += 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 4);
			recipe.AddIngredient(ItemID.ClayBlock, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}