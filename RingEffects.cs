using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Objects;

namespace Enhanced_Rings
{
    internal static class RingEffects
    {
        /// <summary>
        /// Harmony Postfix for ring buff effects
        /// </summary>
        public static void AddEquipmentEffects_Postfix(Ring __instance, BuffEffects effects)
        {
            try
            {
                var config = ModEntry.GetInstance().Config;
                string? itemId = __instance.ItemId;

                if (itemId == "BurglarRingPlus")
                {
                    effects.Speed.Value += 1f;
                }
                else if (itemId == "BurglarRingPlus2")
                {
                    effects.Speed.Value += 1f;
                    effects.Attack.Value += 1f;
                }
                else if (itemId == "BurglarRingPlus3")
                {
                    effects.Speed.Value += 2f;
                    effects.Attack.Value += 1f;
                }
                else if (itemId == "RubyRingCombined")
                {
                    effects.AttackMultiplier.Value += config.RubyRingCombinedMultiplier;
                }
                else if (itemId == "RubyRingCombinedPlus")
                {
                    effects.AttackMultiplier.Value += config.RubyRingCombinedPlusMultiplier;
                }
                else if (itemId == "EmeraldRingCombined")
                {
                    effects.WeaponSpeedMultiplier.Value += config.EmeraldRingCombinedMultiplier;
                }
                else if (itemId == "EmeraldRingCombinedPlus")
                {
                    effects.WeaponSpeedMultiplier.Value += config.EmeraldRingCombinedPlusMultiplier;
                }
                else if (itemId == "JadeRingCombined")
                {
                    effects.CriticalPowerMultiplier.Value += config.JadeRingCombinedMultiplier;
                }
                else if (itemId == "JadeRingCombinedPlus")
                {
                    effects.CriticalPowerMultiplier.Value += config.JadeRingCombinedPlusMultiplier;
                }
                else if (itemId == "AquamarineRingCombined")
                {
                    effects.CriticalChanceMultiplier.Value += config.AquamarineRingCombinedMultiplier;
                }
                else if (itemId == "AquamarineRingCombinedPlus")
                {
                    effects.CriticalChanceMultiplier.Value += config.AquamarineRingCombinedPlusMultiplier;
                }
                else if (itemId == "IridiumBandAlpha")
                {
                    effects.AttackMultiplier.Value += config.IridiumBandAlphaAttackMultiplier;
                    effects.WeaponSpeedMultiplier.Value += config.IridiumBandAlphaSpeedMultiplier;
                    effects.CriticalPowerMultiplier.Value += config.IridiumBandAlphaCritPowerMultiplier;
                    effects.CriticalChanceMultiplier.Value += config.IridiumBandAlphaCritChanceMultiplier;
                }
                else if (itemId == RingIds.LuckyRingPlus)
                {
                    effects.LuckLevel.Value += config.LuckyRingPlusLuck;
                }
                else if (itemId == RingIds.LuckyRingPlus2)
                {
                    effects.LuckLevel.Value += config.LuckyRingPlus2Luck;
                }
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Err while add ring buff: {ex}", LogLevel.Error);
            }
        }
    }
}