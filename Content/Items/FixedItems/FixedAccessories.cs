using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Potions.Alcohol;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.FixedItems
{
    public partial class FixedItem : GlobalItem
    {
        public int NanoBladeHomingDist = 12;
        public double NanoDamageMul = 1.05;
        public int NanoBeamDamage = 120;
        public int NanoBladeDamage = 60;
        public int BloodflareCoreRegenSpeed = 10;
        private void AccessoriesAndMiscCases(Item item, List<TooltipLine> tooltips)
        {
            if (item.ThisItem<Nanotech>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "NanotechFixed.BladeCap", NanoBladeCap, NanoBladeHomingDist);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "NanotechFixed.RealDamage", NanoDamageMul);
                tooltips.QuickNewLineNoColor(Mod, OldFashionedDamageShown, ProjectileString.ToLangValue("Nanoblade"), NanoBladeDamage,TooltipConstants.RogueClassName.ToLangValue());
                tooltips.QuickNewLineNoColor(Mod, OldFashionedDamageShown, ProjectileString.ToLangValue("Nanobeam"), NanoBeamDamage,TooltipConstants.RogueClassName.ToLangValue());
            }
            if (item.ThisItem<Nucleogenesis>())
            {
                const int sparkDamage = 60;
                string immnueDebuff = $"{MethodList.GetBuffIcon("IconIrradiated")}{MethodList.GetBuffIcon("IconAstralInfection")}{MethodList.GetBuffIcon("IconShadowFlames")}{MethodList.GetBuffIcon("IconElectric")}";
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.DebuffList", immnueDebuff);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.General", ProjectileString.ToLangValue("ElectricSpark"), LocalPlayer.ToOldFashioned(sparkDamage), TooltipConstants.SummonClassName.ToLangValue());

            }
            if (item.ThisItem<StarTaintedGenerator>())
            {
                const int sparkDamage = 40;
                string immnueDebuff = $"{MethodList.GetBuffIcon("IconIrradiated")}{MethodList.GetBuffIcon("IconAstralInfection")}{MethodList.GetBuffIcon("IconElectric")}";
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.DebuffList", immnueDebuff);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "Nucleogenesis.General", ProjectileString.ToLangValue("ElectricSpark"), LocalPlayer.ToOldFashioned(sparkDamage), TooltipConstants.SummonClassName.ToLangValue());
            }
            //血炎晶核：实际恢复速度
            if (item.ThisItem<BloodflareCore>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "BloodCoreRealRegen", BloodflareCoreRegenSpeed);
            }
            //天蓝石：不会与羽落生效
            if (item.ThisItem<AeroStone>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AeroStoneNoFeather");
            }
            //阳炎战旗：显示多少距离的加成和实际加成
            if (item.ThisItem<WarbanneroftheSun>())
            {
                ModifyWarbannerTooltip(tooltips, TooltipConstants.ItemPath + "WarbannerBonus");
            }
            //惊天神盗盒
            if (item.ThisItem<VeneratedLocket>())
            {
                const int locketDamage = 7;
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "VLocket", locketDamage);
            }
            //翱翔证章：无法叠加
            if (item.ThisItem<AscendantInsignia>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AscendantInsignia", "[i:EmpressFlightBooster]");
            }
            //潜水服：提示这个东西伤害减免和移动速度
            if (item.ThisItem<AbyssalDivingSuit>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AbyssGear.MoveSpeed");
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AbyssGear.DR");
            }
            //聚合脑：延长增益的效果与保留效果实际
            if (item.ThisItem<TheAmalgam>())
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "AmalgamBuffsSave");
            }
            if (item.ThisItem(ItemID.SlimySaddle) || item.ThisItem(ItemID.PogoStick) || item.ThisItem(ItemID.QueenSlimeMountSaddle))
            {
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "SlimeSaddleNerfed");
            }
            //圣天：小作文
            if (item.ThisItem<AngelicAlliance>())
            {
                string replacedPath = $"{TooltipConstants.ItemPath}{nameof(AngelicAlliance)}.ActualShift";
                tooltips.HoldingShiftToReplace(Mod, replacedPath, TooltipConstants.CalExtraText, TooltipConstants.CalExtraColor);
            }
            //酒：小作文
            if (item.ThisItem<OldFashioned>())
            {
                string replacedPath = $"{TooltipConstants.ItemPath}OldFashionedBounesText";
                tooltips.HoldingShiftToReplace(Mod, replacedPath, TooltipConstants.CalExtraText, TooltipConstants.CalExtraColor);
                tooltips.QuickNewLineNoColor(Mod, TooltipConstants.ItemPath + "OldFashionedBounesText");
            }
        }
    }
}