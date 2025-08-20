using CalTooltipFixer.Method;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.BuffPlaceholder
{
    public class IconAstralInfection : IconBuffBase, ILocalizedModType
    {
        public new string LocalizationCategory => "Buffs.Placeholder";
        public override string GetBuffIcon => DoTPath + "AstralInfectionDebuff";
    }
}