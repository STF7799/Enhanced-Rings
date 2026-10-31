using HarmonyLib;
using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enhanced_Rings
{
    internal static class CookQualityHandler
    {
        // 辅助方法：获取容器内容（用于调味料检查）
        private static IList<Item> GetContainerContents(CraftingPage instance)
        {
            return Traverse.Create(instance).Method("getContainerContents").GetValue<IList<Item>>();
        }

        // ==================== 公共逻辑 ====================

        /// <summary>获取数量倍数（根据魔法厨师戒指）</summary>
        private static int GetQuantityMultiplier()
        {
            var config = ModEntry.GetInstance().Config;
            if (Game1.player.isWearingRing("MagicChefRing3"))
                return config.MagicChefRing3Multiplier;
            if (Game1.player.isWearingRing("MagicChefRing2"))
                return config.MagicChefRing2Multiplier;
            if (Game1.player.isWearingRing("MagicChefRing1"))
                return config.MagicChefRing1Multiplier;
            return 1;
        }

        /// <summary>获取应设置的品质，并返回是否跳过调味料</summary>
        private static (int newQuality, bool skipSeasoning) GetCookedQuality()
        {
            if (Game1.player.isWearingRing("ChefRing3"))
                return (4, true);
            if (Game1.player.isWearingRing("ChefRing2"))
                return (2, true);
            if (Game1.player.isWearingRing("ChefRing"))
                return (1, true);
            return (0, false); // 品质0表示未提升，需要调味料逻辑
        }

        // ==================== Prefix 模式（原有实现） ====================
        public static bool ClickCraftingRecipe_Prefix(CraftingPage __instance, ClickableTextureComponent c, bool playSound = true)
        {
            try
            {
                CraftingRecipe recipe = __instance.pagesOfCraftingRecipes[__instance.currentCraftingPage][c];
                Item crafted = recipe.createItem();

                // 数量倍数
                int quantityMultiplier = GetQuantityMultiplier();
                int baseProduced = recipe.numberProducedPerCraft;
                int totalProduced = baseProduced * quantityMultiplier;
                crafted.Stack = totalProduced;

                // 品质处理
                List<KeyValuePair<string, int>>? seasoning = null;
                if (__instance.cooking && crafted.Quality == 0)
                {
                    var (newQuality, skipSeasoning) = GetCookedQuality();
                    if (skipSeasoning)
                    {
                        crafted.Quality = newQuality;
                    }
                    else
                    {
                        // 原版调味料逻辑
                        seasoning = new List<KeyValuePair<string, int>> { new("917", 1) };
                        if (CraftingRecipe.DoesFarmerHaveAdditionalIngredientsInInventory(seasoning, GetContainerContents(__instance)))
                        {
                            crafted.Quality = 2;
                        }
                        else
                        {
                            seasoning = null;
                        }
                    }
                }

                // 物品处理
                if (__instance.heldItem == null)
                {
                    recipe.consumeIngredients(__instance._materialContainers);
                    __instance.heldItem = crafted;
                    if (playSound) Game1.playSound("coin");
                }
                else
                {
                    if (!(__instance.heldItem.Name == crafted.Name) ||
                        !__instance.heldItem.getOne().canStackWith(crafted.getOne()) ||
                        __instance.heldItem.Stack + totalProduced - 1 >= __instance.heldItem.maximumStackSize())
                    {
                        return false; // 无法堆叠，中断原方法
                    }
                    __instance.heldItem.Stack += totalProduced;
                    recipe.consumeIngredients(__instance._materialContainers);
                    if (playSound) Game1.playSound("coin");
                }

                // 调味料消耗
                if (seasoning != null)
                {
                    if (playSound) Game1.playSound("breathin");
                    CraftingRecipe.ConsumeAdditionalIngredients(seasoning, __instance._materialContainers);
                    if (!CraftingRecipe.DoesFarmerHaveAdditionalIngredientsInInventory(seasoning, GetContainerContents(__instance)))
                    {
                        Game1.showGlobalMessage(Game1.content.LoadString("Strings\\StringsFromCSFiles:Seasoning_UsedLast"));
                    }
                }

                // 任务与统计
                Game1.player.NotifyQuests(quest => quest.OnRecipeCrafted(recipe, crafted, false), false);
                if (!__instance.cooking && Game1.player.craftingRecipes.ContainsKey(recipe.name))
                {
                    Game1.player.craftingRecipes[recipe.name] += recipe.numberProducedPerCraft;
                }
                if (__instance.cooking)
                {
                    Game1.player.cookedRecipe(__instance.heldItem.ItemId);
                    Game1.stats.checkForCookingAchievements();
                }
                else
                {
                    Game1.stats.checkForCraftingAchievements();
                }

                // 手柄快捷转移
                if (Game1.options.gamepadControls && __instance.heldItem != null && Game1.player.couldInventoryAcceptThisItem(__instance.heldItem))
                {
                    Game1.player.addItemToInventoryBool(__instance.heldItem, false);
                    __instance.heldItem = null;
                }

                return false; // 阻止原方法执行
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Error in ClickCraftingRecipe_Prefix: {ex}", LogLevel.Error);
                return true; // 出错时执行原方法
            }
        }
    }
}