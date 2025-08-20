using System.Collections.Generic;
using CalTooltipFixer.Method;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Armor
{
    public class FixedShogun : GlobalItem
    {
        internal string HeadName = "ShogunHelm";
        internal string BodyName = "ShogunChestplate";
        internal string LegsName = "ShogunPants";
        internal Mod Hunt = CalTooltipFixer.Hunt;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) 
        {
            if (Hunt is null)
                return false;
            int headType = GetShogunParts(HeadName);
            return headType != -1 && entity.type == headType;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //每个地方都得小心点
            if (Hunt is null)
                return;
            int headType = GetShogunParts(HeadName);
            //检查是否存在物品，这个属于防御性的
            if (headType == -1f || item.type != headType)
                return;
            
            string armorTooltip = MethodList.GetLocalText("ArmorTooltip.");
            string fixedTooltip = Language.GetTextValue(armorTooltip + "ShogunHead");

            //直接获取最后一行的索引
            int targetIndex = tooltips.Count - 1;
            //插入这一行
            tooltips.Insert(targetIndex + 1, new TooltipLine(Mod, "Insert", fixedTooltip));
        }
        public static bool IsFullSet(Player player, int head, int body, int legs)
        {
            return player.ThisBodyPart("Head", head) && player.ThisBodyPart("Body", body) && player.ThisBodyPart("Legs", legs);
        }
        public int GetShogunParts(string name)
        {
            if (Hunt is null)
                return -1;
            if (Hunt.TryFind(name, out ModItem thisItem))
                return thisItem.Type;

            return -1;
        }
    }
}