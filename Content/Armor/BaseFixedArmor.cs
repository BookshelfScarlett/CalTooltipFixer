using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Weapons.Summon;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public abstract class BaseFixedArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;
        /// <summary>
        /// 头盔部件类型
        /// </summary>
        public virtual int HeadType => -1;
        /// <summary>
        /// 胸甲部件类型
        /// </summary>
        public virtual int BodyType => -1;
        /// <summary>
        /// 护腿部件类型
        /// </summary>
        public virtual int LegsType => -1;
        /// <summary>
        /// 路径
        /// </summary>
        public virtual string ArmorTextPath => GetType().Name;
        public string ThisArmorFixedText => MethodList.GetLocalText($"ArmorTooltip.{ArmorTextPath}");
        /// <summary>
        /// 是否需要标记古典酒，一般情况下默认是开启套装效果
        /// </summary>
        public virtual bool ShouldPingOldFashioned => false;
        /// <summary>
        /// 是否标记处理整套护甲，用于专门修改setBonus，取否不会考虑整套
        /// </summary>
        public virtual bool ShouldFullArmor => true;
        public static string GetPingText => GetColoredText(TooltipConstants.CalExtraColor, TooltipConstants.CalExtraText);
        public static string Header => "\n" + GetPingText + "\n";
        public float OldFashionedMultipler = OldFashioned.AccessoryAndSetBonusDamageMultiplier;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ShouldFullArmor)
            {
                return entity.type == HeadType || entity.type == BodyType || entity.type == LegsType;
            }
            else
            {
                return entity.type == ShouldApplyTo();
            }
        }
        /// <summary>
        /// 标记这个tottip应该由哪个部件展示。与盔甲效果的更多提示无关，目前硬编码中只处理了其古典酒的情况
        /// </summary>
        /// <returns></returns>
        public virtual int ShouldApplyTo() => HeadType;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player Owner = Main.LocalPlayer;
            if (ShouldFullArmor is false)
                NotFullArmorTooltip(item, tooltips, Owner);
            else
            {
                TooltipLine setBonusLine = tooltips.Find(line => line.Name == "SetBonus");
                //忘记做判定了！！！
                if(setBonusLine is not null)
                    IsFullArmorTooltip(item, tooltips, Owner, setBonusLine);
                if (ShouldPingOldFashioned)
                    PingOldFashioned(Owner, setBonusLine);
            }
        }

        public void PingOldFashioned(Player owner, TooltipLine setBonusLine)
        {
            bool extraPing = ExtraOldFashion(owner);
            if (!owner.Calamity().oldFashioned)
                return;
            if (!extraPing)
                return;
            
            if (setBonusLine is null)
                return;
                
            //直接赋值，替换一整个内容后再加入新的内容。
            setBonusLine.Text += "\n" + TooltipConstants.GetCanOldFashionedText.ToLangValue().GetFormatString("[i:CalamityMod/OldFashioned]");
        }

        public virtual bool ExtraOldFashion(Player owner) => false;

        /// <summary>
        ///  全甲时的新tooltip
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tooltips"></param>
        /// <param name="owner"></param>
        /// <param name="setBonusLine">以SetBonus作为起始的tooltipline</param>
        public virtual void IsFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner, TooltipLine setBonusLine) { }

        public virtual void NotFullArmorTooltip(Item item, List<TooltipLine> tooltips, Player owner) { }
        public static int ArmorType<T>() where T : ModItem => ModContent.ItemType<T>();
        public static string GetColoredText(Color color, string textPath) => $"[c/{color.ToHexStringColor()}:{textPath.ToLangValue()}]";
    }
}