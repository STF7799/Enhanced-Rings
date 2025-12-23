using StardewModdingAPI;
using StardewValley;
using StardewValley.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enhanced_Rings
{
    public class GameStateQueryManager
    {
        private static IMonitor? _monitor;
        private static ModConfig? _config;
        private static ModEntry? _modInstance;

        public static void Initialize(ModEntry modInstance, ModConfig config, IMonitor monitor)
        {
            _modInstance = modInstance;
            _config = config;
            _monitor = monitor;
            RegisterCustomQueries();
        }

        private static void RegisterCustomQueries()
        {
            try
            {
                GameStateQuery.Register("PLAYER_HAS_LFISH_RING", PlayerHasLFishRingQuery);
            }
            catch (Exception ex)
            {
                _monitor?.Log($"Failed to register custom GameStateQuery conditions: {ex.Message}", LogLevel.Error);
            }
        }

        private static bool PlayerHasLFishRingQuery(string[] queryFields, GameStateQueryContext context)
        {
            try
            {
                if (Game1.player == null)
                    return false;

                return Game1.player.isWearingRing("LFishRing");
            }
            catch (Exception ex)
            {
                _monitor?.Log($"Error in PlayerHasEnhancedRingQuery: {ex.Message}", LogLevel.Error);
                return false;
            }
        }
    }
}