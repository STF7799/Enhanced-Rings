using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Internal;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.Tools;

namespace Enhanced_Rings
{
    internal class HarmonyPatcher
    {
        private readonly string _modId;
        private readonly Harmony _harmony;

        public HarmonyPatcher(string modId)
        {
            _modId = modId;
            _harmony = new Harmony(modId);

        }

        public void ApplyPatches()
        {
            var config = ModEntry.GetInstance().Config;
            _harmony.Patch(
                original: AccessTools.Method(typeof(Ring), nameof(Ring.AddEquipmentEffects)),
                postfix: new HarmonyMethod(typeof(RingEffects), nameof(RingEffects.AddEquipmentEffects_Postfix))
            );

            _harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.monsterDrop)),
                postfix: new HarmonyMethod(typeof(MonsterDropHandler), nameof(MonsterDropHandler.MonsterDrop_Postfix))
            );

            _harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), "adjustLevelChances"),
                postfix: new HarmonyMethod(typeof(MonsterSpawnHandler), nameof(MonsterSpawnHandler.AdjustMineShaftMonsterChance_Postfix))
            );

            _harmony.Patch(
                original: AccessTools.Method(typeof(VolcanoDungeon), "adjustLevelChances"),
                postfix: new HarmonyMethod(typeof(MonsterSpawnHandler), nameof(MonsterSpawnHandler.AdjustVolcanoDungeonMonsterChance_Postfix))
            );

            _harmony.Patch(
                original: AccessTools.Method(typeof(Ring), nameof(Ring.onMonsterSlay)),
                postfix: new HarmonyMethod(typeof(MonsterSlayHandler), nameof(MonsterSlayHandler.OnMonsterSlay_Postfix))
            );
            if (config.EnableChefRingPatch)
            {
                _harmony.Patch(
                    original: AccessTools.Method("StardewValley.Menus.CraftingPage:clickCraftingRecipe"),
                    prefix: new HarmonyMethod(typeof(CookQualityHandler), nameof(CookQualityHandler.ClickCraftingRecipe_Prefix))
                );
            }
            if (config.EnableFishRingPatch)
            {
                _harmony.Patch(
                    original: AccessTools.Method(typeof(FishingRod), nameof(FishingRod.pullFishFromWater)),
                    prefix: new HarmonyMethod(typeof(FishCaughtHandler), nameof(FishCaughtHandler.PullFishFromWater_Prefix))
                );
            }
            if (config.EnableCropQualityRingPatch)
            {
                try
                {
                    CropQualityPatcher.ApplyPatch(_harmony);
                }
                catch (System.Exception ex)
                {
                    ModEntry.GetInstance().Monitor.Log($"Failed to apply crop quality ring patch: {ex}", LogLevel.Error);
                }
            }
        }
    }
}