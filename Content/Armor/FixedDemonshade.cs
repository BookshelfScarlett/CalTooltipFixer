using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Armor.Demonshade;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Items;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedDemonshadeArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.ThisItem<DemonshadeHelm>() || item.ThisItem<DemonshadeBreastplate>() || item.ThisItem<DemonshadeHelm>();
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            SetWOTGPing(item,tooltips);
            SetOldFashionedPing(item, tooltips);
            
        }

        private static void SetOldFashionedPing(Item item, List<TooltipLine> tooltips)
        {
            // Player player = Main.LocalPlayer;
            // if (!player.Calamity().oldFashioned)
                return;
            // TooltipLine origiTooltip = tooltips.Find(line => line.Name == "SetBonus");
            // if (IsFullDemonshade(player))
            // {
            //     if (origiTooltip != null)
            //     {
            //         origiTooltip.Text += "\n" + TooltipConstants.GetCanOldFashionedText;
            //     }
            // }
        }

        private static void SetWOTGPing(Item item, List<TooltipLine> tooltips)
        {
            //没开启众神之怒时不要做任何事
            if (CalTooltipFixer.WrathOfTheGod is null)
                return;
            string armorTooltip = MethodList.GetLocalText("ArmorTooltip.");
            TooltipLine origiTooltip = tooltips.Find(line => line.Name == "SetBonus");
            Player player = Main.LocalPlayer;
            if (IsFullDemonshade(player))
            {
                //原有套装添加新的内容
                string fixedText = Language.GetTextValue(armorTooltip + "DemonshadeArmor");
                if (origiTooltip != null && fixedText != null)
                {
                    origiTooltip.Text += "\n" + Language.GetTextValue(fixedText);
                }
            }
        }

        public static bool IsFullDemonshade(Player player)
        {
            return player.ThisBodyPart<DemonshadeHelm>("Head") && player.ThisBodyPart<DemonshadeBreastplate>("Body") && player.ThisBodyPart<DemonshadeGreaves>("Legs");
        }
    }
}