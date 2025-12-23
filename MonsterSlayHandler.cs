using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;

namespace Enhanced_Rings
{
    internal static class MonsterSlayHandler
    {
        /// <summary>
        /// Harmony Postfix for onMonsterSlay to handle enhanced napalm rings
        /// </summary>
        public static void OnMonsterSlay_Postfix(Ring __instance, Monster monster, GameLocation location, Farmer who)
        {
            try
            {
                if (monster == null || location == null || who == null)
                    return;

                var config = ModEntry.GetInstance().Config;
                string? itemId = __instance.ItemId;

                int explosionRadius = 0;

                if (itemId == RingIds.NapalmRingPlus)
                {
                    explosionRadius = config.NapalmRingPlusRadius;
                }
                else if (itemId == RingIds.NapalmRingPlus2)
                {
                    explosionRadius = config.NapalmRingPlus2Radius;
                }
                else if (itemId == RingIds.NapalmRingPlus3)
                {
                    explosionRadius = config.NapalmRingPlus3Radius;
                }

                if (explosionRadius > 0)
                {
                    location.explode(monster.Tile, explosionRadius, who, false, -1, location is not Farm && location is not SlimeHutch);
                }
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Error in OnMonsterSlay_Postfix: {ex}", LogLevel.Error);
            }
        }
    }
}