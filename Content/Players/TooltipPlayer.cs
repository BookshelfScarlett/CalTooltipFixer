using CalTooltipFixer.Method;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Players
{
    public partial class TooltipPlayer : ModPlayer
    {
        //h缓存表单null"
        public string _cacheResistanceList = null;
        #region 抗性表单的Boolen，这个必须得打表了。起名规则为对应物品的类名+Bool
        public bool ResistanceAresBool = false;
        public bool ResistanceThanatosBool = false;
        public bool ResistanceExoTwinsBool = false;
        #endregion
    }
}