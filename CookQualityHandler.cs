using HarmonyLib;
using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Quests;
using System.Collections.Generic;
using System.Reflection;

namespace Enhanced_Rings
{
    internal static class CookQualityHandler
    {
        private static IList<Item> GetContainerContents(CraftingPage instance)
        {
            return Traverse.Create(instance).Method("getContainerContents").GetValue<IList<Item>>();
        }
        /// <summary>
        /// Harmony Prefix for CraftingPage.clickCraftingRecipe to handle chef ring effects
        /// </summary>
        public static bool ClickCraftingRecipe_Prefix(CraftingPage __instance, ClickableTextureComponent c, bool playSound = true)
        {
            try
            {
                //return true;
                CraftingRecipe recipe = __instance.pagesOfCraftingRecipes[__instance.currentCraftingPage][c];
                Item crafted = recipe.createItem();
                List<KeyValuePair<string, int>>? seasoning = null;
                if (__instance.cooking && crafted.Quality == 0)
                {
                    bool hasChefRing = Game1.player.isWearingRing("ChefRing");
                    bool hasChefRing2 = Game1.player.isWearingRing("ChefRing2");
                    bool hasChefRing3 = Game1.player.isWearingRing("ChefRing3");
                    if (hasChefRing3)
                    {
                        crafted.Quality = 4;
                    }
                    else if (hasChefRing2)
                    {
                        crafted.Quality = 2;
                    }
                    else if (hasChefRing)
                    {
                        crafted.Quality = 1;
                    }
                    else
                    {
                        //vanilla logic
                        seasoning = new List<KeyValuePair<string, int>>
                        {
                        new("917", 1)
                        };
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
                if (__instance.heldItem == null)
                {
                    recipe.consumeIngredients(__instance._materialContainers);
                    __instance.heldItem = crafted;
                    if (playSound)
                    {
                        Game1.playSound("coin", null);
                    }
                }
                else
                {
                    if (!(__instance.heldItem.Name == crafted.Name) || !__instance.heldItem.getOne().canStackWith(crafted.getOne()) || __instance.heldItem.Stack + recipe.numberProducedPerCraft - 1 >= __instance.heldItem.maximumStackSize())
                    {
                        return false;
                    }
                    __instance.heldItem.Stack += recipe.numberProducedPerCraft;
                    recipe.consumeIngredients(__instance._materialContainers);
                    if (playSound)
                    {
                        Game1.playSound("coin", null);
                    }
                }
                if (seasoning != null)
                {
                    if (playSound)
                    {
                        Game1.playSound("breathin", null);
                    }
                    CraftingRecipe.ConsumeAdditionalIngredients(seasoning, __instance._materialContainers);
                    if (!CraftingRecipe.DoesFarmerHaveAdditionalIngredientsInInventory(seasoning,GetContainerContents(__instance)))
                    {
                        Game1.showGlobalMessage(Game1.content.LoadString("Strings\\StringsFromCSFiles:Seasoning_UsedLast"));
                    }
                }
                Game1.player.NotifyQuests(quest => quest.OnRecipeCrafted(recipe, crafted, false), false);
                if (!__instance.cooking && Game1.player.craftingRecipes.ContainsKey(recipe.name))
                {
                    NetStringDictionary<int, NetInt> craftingRecipes = Game1.player.craftingRecipes;
                    string name = recipe.name;
                    craftingRecipes[name] += recipe.numberProducedPerCraft;
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
                if (Game1.options.gamepadControls && __instance.heldItem != null && Game1.player.couldInventoryAcceptThisItem(__instance.heldItem))
                {
                    Game1.player.addItemToInventoryBool(__instance.heldItem, false);
                    __instance.heldItem = null;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Error in ClickCraftingRecipe_Prefix: {ex}", LogLevel.Error);
                return true;
            }
        }
    }
}