using System.Collections.Generic;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedNebulaArmor : BaseFixedArmor
    {
        public const double DamageNerfedPerLevel = 7.5;
        public const int RegenNerfedPerLevel = 2;
        public const int ManaRegenNerfedPerLevel = 5;
        public const int NerfedDR = 12;
        internal static string DamagePickUp => "[i:NebulaPickup1]";
        internal static string RegenPickUp => "[i:NebulaPickup2]";
        internal static string ManaPickUp => "[i:NebulaPickup3]";
        public override int HeadType => ItemID.NebulaHelmet;
        public override int BodyType => ItemID.NebulaBreastplate;
        public override int LegsType => ItemID.NebulaLeggings;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            string fixedText = ThisArmorFixedText.ToLangValue();
            if (string.IsNullOrEmpty(fixedText))
                return;
                
            string realText = fixedText.GetFormatString(DamagePickUp, DamageNerfedPerLevel.ToString(), RegenPickUp, RegenNerfedPerLevel.ToString(), ManaPickUp, ManaRegenNerfedPerLevel.ToString());
            string hasColoredText = GetColoredText(TooltipConstants.CalamityModifyColor, TooltipConstants.CalamityModifyText);
            setBonusLine.Text += "\n" + hasColoredText + "\n" + realText;
        }
    }
}