using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace FavoriteMagnet.Items
{
	public class MagneticSword : ModItem
	{
        /// <summary>
        /// ��ȡ��Ʒ��ģʽ
        /// <para>0 ����</para>
        /// <para>1 ���ղ�</para>
        /// <para>2 ������</para>
        /// </summary>
        public int mode;
		public int range;
		/// <summary>
		/// ����ʯ�Ƿ���, ����ȡ����Ʒ����Ӱ��
		/// </summary>
		public bool encumbering;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a magnetic sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2 && player.itemAnimation == 2)
			{
				mode++;
				if (mode > 3)
				{
					mode = 0;
				}
			}
			else
			{
				player.GetModPlayer<AttractivePlayer>().attractionMode = mode;
			}
			return base.UseItem(player);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			encumbering = !encumbering;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			base.ModifyTooltips(tooltips);
			TooltipLine modeAndEnc = new TooltipLine(Mod, "ModeAndEnc", $"Mode: {mode}, Encumbering: {encumbering}");
			tooltips.Add(modeAndEnc);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}