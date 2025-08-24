using CalTooltipFixer.Method;

namespace CalTooltipFixer.ConstantList
{
    public partial class TooltipConstants
    {
        #region 标记名
        public static string PingPath => MethodList.GetLocalText("PingText.");
        public static string CalExtraText => PingPath + "CalTooltipExtra";
        public static string CalamityModifyText => PingPath + "CalamityModify";
        public static string WrathoftheGodsText => PingPath + "WrathoftheGodsModify";
        //抗性
        public static string CalamityResistanceText => PingPath + "CalamityResistance";
        public static string HuntResistanceText => PingPath + "HuntResistance";
        public static string CatalystResistanceText => PingPath + "CatalystResistance";
        public static string CalamityResistanceValue => WeaponPath + "CalamityResistanceValue.";
        #endregion
    }
}