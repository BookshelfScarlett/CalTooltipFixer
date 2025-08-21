using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CalTooltipFixer.ConstantList;
using CalTooltipFixer.Content.Players;
using CalTooltipFixer.Method;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalTooltipFixer.Content.Items.BossesResistances
{
    public abstract class BaseResistance : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "ResistanceItem";
        /// <summary>
        /// 对应Boss物品的大小
        /// </summary>
        public virtual Vector2 BossHeadBox{ get; }
        /// <summary>
        /// 这个物品对应的Boss的ID
        /// 考虑到可能有多个boss，因此是个数组
        /// </summary>
        public virtual int[] BossType { get; }
        /// <summary>
        /// Boss的减伤系列的物品，属于字符串数组
        /// </summary>
        public virtual string[] WeaponType{ get; }
        /// <summary>
        /// 原版的武器数组，需要单独打表
        /// </summary>
        public virtual string[] WeaponTypeVanilla => null;
        /// <summary>
        /// 该物品对应的mod名
        /// </summary>
        public virtual string WeaponMod => "CalamityMod";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = (int)BossHeadBox.X;
            Item.width = (int)BossHeadBox.Y;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Camera;
            Item.consumable = false;
            ExtraSD();
        }
        public override bool AltFunctionUse(Player player) => player.ThisMod()._cacheResistanceList is not null;
        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.ThisMod();

            //如果使用右键功能，直接返回
            if (player.altFunctionUse == 2)
            {
                return true;
            }

            //第一步：查看是否缓存的武器抗性查阅与当前类名+"Bool"相同，如果是则不可使用该物品
            if (modPlayer._cacheResistanceList is not null && modPlayer._cacheResistanceList == GetType().Name + "Bool")
                return false;
            //第二步：查阅对应的boss是否在场。如果是则不可用
            foreach (int bossType in BossType)
            {
                if (NPC.AnyNPCs(bossType))
                    return false;
            }

            return base.CanUseItem(player);
        }
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Language.GetTextValue("Mods.CalTooltipFixer.ResistanceItem.GeneralResistanceTooltip"));
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D paperTex = ModContent.Request<Texture2D>("CalTooltipFixer/Content/Items/BossesResistances/BossPlaceholder").Value;
            Vector2 paperPosition = position + new Vector2(2f, 2f);
            spriteBatch.Draw(paperTex, paperPosition, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
        }

        public override bool? UseItem(Player player)
        {
            var modPlayer = player.ThisMod();
            ref string _cache = ref modPlayer._cacheResistanceList;
            //只有第一帧
            bool isFirstFrame = player.itemAnimation == player.itemAnimationMax;
            if (!isFirstFrame)
                return base.UseItem(player);
            //右键清空与重置对应的缓存名和boolen
            if (player.altFunctionUse == 2 && isFirstFrame)
            {
                //干掉对应Boolen
                if (_cache is not null)
                {
                    ClearCacheResistanceWithItsBoolen(modPlayer, false);
                    //清空缓存
                    _cache = null;
                }
                //返回取消下方的代码加载
                return base.UseItem(player);
            }
            //额外特判：如果缓存名与当前使用物品的名字+Bool不同，则干掉原本的Boolen
            string thisType = GetType().Name + "Bool";
            if (_cache is not null && _cache != thisType)
            {
                //干掉缓存名对应的Boolen
                ClearCacheResistanceWithItsBoolen(modPlayer, false);

            }
            //将类名+Bool保存至这个缓存
            _cache = thisType;
            //拼接武器数组成大型字符串
            string weaponList = GetWeaponList(WeaponType);
            //获取起始符
            string headerString = GetHeaderName(WeaponMod);
            //发送
            Main.NewText(headerString + " " + weaponList);
            //原版由于字符问题，需要单独发送，已经处理好了特殊情况了
            GetVanillaList();
            //如果是其他模组的武器需要，则补充，不过应该不会出现这种情况吧
            ExtraListShouldShow(player);
            //设置Boss对应的boolen
            SetResistanceBoolen(modPlayer);
            return base.UseItem(player);
        }
        public static void ClearCacheResistanceWithItsBoolen(TooltipPlayer modPlayer, bool value)
        {
            //反射获取字符串，关闭原本的boolen
            FieldInfo boolenField = modPlayer.GetType().GetField(modPlayer._cacheResistanceList, BindingFlags.Instance | BindingFlags.Public);
            if (boolenField is null)
                CalTooltipFixer.Instance.Logger.Warn($"玩家类不存在该字符段{boolenField}");
            if (boolenField.FieldType != typeof(bool))
                CalTooltipFixer.Instance.Logger.Warn($"该字符段{boolenField}不是Bool值");
            //设置boolen为对应值
            boolenField.SetValue(modPlayer, value);
        }
        public virtual void ExtraListShouldShow(Player player)
        {

        }
        internal string GetWeaponList(string[] list)
        {
            if (list is null || list.Length == 0)
            {
                CalTooltipFixer.Instance.Logger.Warn("No weapon type in array!");
                return null;
            }
            //遍历武器数组，格式化后变成大型字符串
            return string.Join("", list.Select(str => $"[i:{WeaponMod}/{str}]"));
        }
        internal static string GetHeaderName(string headerName)
        {
            string basicString = Language.GetTextValue(TooltipConstants.ResistanceHeaderText);
            string modName = Language.GetTextValue(MethodList.StringNameHandler("ModName." + headerName));
            string realString;
            try
            {
                realString = string.Format(basicString, modName);
            }
            catch
            {
                realString = basicString + "格式化出错";
            }
            return realString;
        }
        internal void GetVanillaList()
        {
            //空数组直接返回即可
            if (WeaponTypeVanilla is null || WeaponTypeVanilla.Length == 0)
                return;
            string headString = GetHeaderName("Vanilla");
            string vanillaList = string.Join("", WeaponTypeVanilla.Select(str => $"[i:{str}]"));
            //发送即可
            Main.NewText(headString + " " + vanillaList);
        }
        public static int NPCID<T>() where T : ModNPC => ModContent.NPCType<T>();
        
        /// <summary>
        /// 重写这个方法，标记模组player类某个特定boolen，防止多次复用
        /// </summary>
        /// <param name="set"></param>
        public virtual void SetResistanceBoolen(TooltipPlayer set){}
        public virtual void ExtraSD()
        {

        }
    }
}