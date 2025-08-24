using System;
using System.Collections.Generic;
using CalamityMod.Items.Armor.Daedalus;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedDaedalusRogue : BaseFixedArmor
    {
        #region Args
        public const int ShardCap = 30;
        public const double DamageMul = 13.75;
        public const string CrystalBulletName = "[i:CrystalBullet]";
        #endregion
        public override int HeadType => ArmorType<DaedalusHeadRogue>();
        public override int BodyType => ArmorType<DaedalusBreastplate>();
        public override int LegsType => ArmorType<DaedalusLeggings>();
        public override string ArmorTextPath => "FixedDaedalus.Rogue";
        public override bool ShouldPingOldFashioned => true;
        public override void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine)
        {
            bool ifIsFull = owner.ThisBodyPart("Head", HeadType) && owner.ThisBodyPart("Body", BodyType) && owner.ThisBodyPart("Legs", LegsType);
            if (!ifIsFull)
                return;
             
            string fixedText = ThisArmorFixedText.ToLangValue();
            string formatedBaseText = MethodList.GetLocalText("ArmorTooltip.FixedDaedalus.General").ToLangValue().GetFormatString(CrystalBulletName);
            if (string.IsNullOrEmpty(fixedText))
                return;
                
            string formatText = fixedText.GetFormatString(DamageMul, TooltipConstants.RogueClassName.ToLangValue(), ShardCap);
            setBonusLine.Text += Header + formatedBaseText + "\n" + formatText;
        }
        public override bool ExtraOldFashion(Player owner)
        {
            return owner.ThisBodyPart("Head", HeadType) && owner.ThisBodyPart("Body", BodyType) && owner.ThisBodyPart("Legs", LegsType);
        }
    }
}