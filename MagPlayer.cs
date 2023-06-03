using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace FavoriteMagnet
{
    public class MagPlayer : ModPlayer
    {
        
        public Select Selection = Select.None;
        public bool Invert;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("Selection", Selection);
            tag.Add("Invert", Invert);
        }
        public override void LoadData(TagCompound tag)
        {
            Selection = (Select)tag.GetInt("Selection");
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
