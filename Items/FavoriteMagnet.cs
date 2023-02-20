using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace FavoriteMagnet.Items
{
	public class FavoriteMagnet : ModItem
	{
        public int mode;
		public int range;
		/// <summary>
		/// 负重石是否开启, 所吸取的物品不受影响
		/// </summary>
		public bool encumbering;

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
			if (player.itemAnimation == 2)
			{
				if (player.altFunctionUse == 2)
				{
					player.GetModPlayer<AttractivePlayer>().AttractionMode++;
					if (player.GetModPlayer<AttractivePlayer>().AttractionMode > 3)
					{
						player.GetModPlayer<AttractivePlayer>().AttractionMode = 0;
					}
				}
				else
				{
					player.GetModPlayer<AttractivePlayer>().Invert = !player.GetModPlayer<AttractivePlayer>().Invert;
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
			player.GetModPlayer<AttractivePlayer>().Encumbered = !player.GetModPlayer<AttractivePlayer>().Encumbered;
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