using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.GameData.Locations;
using StardewValley.GameData.Objects;
using System.Collections.Generic;
using System.Threading;

namespace Enhanced_Rings
{
    internal class AssetEditor
    {
        private readonly ITranslationHelper _i18n;
        private readonly string _modId;

        public AssetEditor(ITranslationHelper i18n, string modId)
        {
            _i18n = i18n;
            _modId = modId;
        }

        public void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/BurglarRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/BurglarRing.png", AssetLoadPriority.Exclusive);
                return;
            }

            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/MonsterRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/MonsterRing.png", AssetLoadPriority.Exclusive);
                return;
            }

            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/CombinedRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/CombinedRingA.png", AssetLoadPriority.Exclusive);
                return;
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Data/Objects"))
            {
                e.Edit(asset => EditObjectsData(asset.AsDictionary<string, ObjectData>().Data));
                return;
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Data/CraftingRecipes"))
            {
                e.Edit(asset => EditCraftingRecipes(asset.AsDictionary<string, string>().Data));
            }

            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/BombRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/BombRing.png", AssetLoadPriority.Exclusive);
                return;
            }
            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/ChefRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/ChefRing.png", AssetLoadPriority.Exclusive);
                return;
            }
            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/LuckyRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/LuckyRing.png", AssetLoadPriority.Exclusive);
                return;
            }
            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/LFishRing"))
            {
                e.LoadFromModFile<Texture2D>("assets/LFishRing.png", AssetLoadPriority.Exclusive);
                return;
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Locations"))
            {
                e.Edit(asset =>
                {
                    var data = asset.AsDictionary<string, LocationData>().Data;
                    EditLocationsData(data);
                });
                return;
            }
            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/ExtraFishRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/ExtraFishRing.png", AssetLoadPriority.Exclusive);
                return;
            }

            if (e.NameWithoutLocale.IsEquivalentTo($"Mods/{_modId}/QualityRings"))
            {
                e.LoadFromModFile<Texture2D>("assets/QualityRing.png", AssetLoadPriority.Exclusive);
                return;
            }
        }

        private void EditObjectsData(IDictionary<string, ObjectData> data)
        {
            data["BurglarRingPlus"] = new ObjectData
            {
                Name = "BurglarRingPlus",
                DisplayName = _i18n.Get("ring.burglar-plus.name"),
                Description = _i18n.Get("ring.burglar-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 2500,
                Texture = $"Mods/{_modId}/BurglarRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_jade", "ring_item" }
            };

            data["BurglarRingPlus2"] = new ObjectData
            {
                Name = "BurglarRingPlus2",
                DisplayName = _i18n.Get("ring.burglar-plus2.name"),
                Description = _i18n.Get("ring.burglar-plus2.description"),
                Type = "Ring",
                Category = -96,
                Price = 5000,
                Texture = $"Mods/{_modId}/BurglarRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_jade", "ring_item" }
            };

            data["BurglarRingPlus3"] = new ObjectData
            {
                Name = "BurglarRingPlus3",
                DisplayName = _i18n.Get("ring.burglar-plus3.name"),
                Description = _i18n.Get("ring.burglar-plus3.description"),
                Type = "Ring",
                Category = -96,
                Price = 15000,
                Texture = $"Mods/{_modId}/BurglarRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_jade", "ring_item" }
            };

            data["MonsterSpawnRing"] = new ObjectData
            {
                Name = "MonsterSpawnRing",
                DisplayName = _i18n.Get("ring.monster-spawn.name"),
                Description = _i18n.Get("ring.monster-spawn.description"),
                Type = "Ring",
                Category = -96,
                Price = 2000,
                Texture = $"Mods/{_modId}/MonsterRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_red", "ring_item" }
            };

            data["MonsterSpawnRing2"] = new ObjectData
            {
                Name = "MonsterSpawnRing2",
                DisplayName = _i18n.Get("ring.monster-spawn2.name"),
                Description = _i18n.Get("ring.monster-spawn2.description"),
                Type = "Ring",
                Category = -96,
                Price = 5000,
                Texture = $"Mods/{_modId}/MonsterRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_red", "ring_item" }
            };

            data["MonsterSpawnRing3"] = new ObjectData
            {
                Name = "MonsterSpawnRing3",
                DisplayName = _i18n.Get("ring.monster-spawn3.name"),
                Description = _i18n.Get("ring.monster-spawn3.description"),
                Type = "Ring",
                Category = -96,
                Price = 12000,
                Texture = $"Mods/{_modId}/MonsterRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_red", "ring_item" }
            };

            data["RubyRingCombined"] = new ObjectData
            {
                Name = "RubyRingCombined",
                DisplayName = _i18n.Get("ring.ruby-combined.name"),
                Description = _i18n.Get("ring.ruby-combined.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_red", "ring_item" }
            };

            data["RubyRingCombinedPlus"] = new ObjectData
            {
                Name = "RubyRingCombinedPlus",
                DisplayName = _i18n.Get("ring.ruby-combined-plus.name"),
                Description = _i18n.Get("ring.ruby-combined-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 6000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_red", "ring_item" }
            };

            data["EmeraldRingCombined"] = new ObjectData
            {
                Name = "EmeraldRingCombined",
                DisplayName = _i18n.Get("ring.emerald-combined.name"),
                Description = _i18n.Get("ring.emerald-combined.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_green", "ring_item" }
            };

            data["EmeraldRingCombinedPlus"] = new ObjectData
            {
                Name = "EmeraldRingCombinedPlus",
                DisplayName = _i18n.Get("ring.emerald-combined-plus.name"),
                Description = _i18n.Get("ring.emerald-combined-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 6000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 3,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_green", "ring_item" }
            };

            data["JadeRingCombined"] = new ObjectData
            {
                Name = "JadeRingCombined",
                DisplayName = _i18n.Get("ring.jade-combined.name"),
                Description = _i18n.Get("ring.jade-combined.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 4,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_jade", "ring_item" }
            };

            data["JadeRingCombinedPlus"] = new ObjectData
            {
                Name = "JadeRingCombinedPlus",
                DisplayName = _i18n.Get("ring.jade-combined-plus.name"),
                Description = _i18n.Get("ring.jade-combined-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 6000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 5,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_jade", "ring_item" }
            };

            data["AquamarineRingCombined"] = new ObjectData
            {
                Name = "AquamarineRingCombined",
                DisplayName = _i18n.Get("ring.aquamarine-combined.name"),
                Description = _i18n.Get("ring.aquamarine-combined.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 6,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_aqua", "ring_item" }
            };

            data["AquamarineRingCombinedPlus"] = new ObjectData
            {
                Name = "AquamarineRingCombinedPlus",
                DisplayName = _i18n.Get("ring.aquamarine-combined-plus.name"),
                Description = _i18n.Get("ring.aquamarine-combined-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 6000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 7,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_aqua", "ring_item" }
            };

            data["IridiumBandAlpha"] = new ObjectData
            {
                Name = "IridiumBandAlpha",
                DisplayName = _i18n.Get("ring.iridium-alpha.name"),
                Description = _i18n.Get("ring.iridium-alpha.description"),
                Type = "Ring",
                Category = -96,
                Price = 15000,
                Texture = $"Mods/{_modId}/CombinedRings",
                SpriteIndex = 8,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_iridium", "ring_item" }
            };
            data["NapalmRingPlus"] = new ObjectData
            {
                Name = "NapalmRingPlus",
                DisplayName = _i18n.Get("ring.napalm-plus.name"),
                Description = _i18n.Get("ring.napalm-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 5000,
                Texture = $"Mods/{_modId}/BombRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_orange", "ring_item" }
            };

            data["NapalmRingPlus2"] = new ObjectData
            {
                Name = "NapalmRingPlus2",
                DisplayName = _i18n.Get("ring.napalm-plus2.name"),
                Description = _i18n.Get("ring.napalm-plus2.description"),
                Type = "Ring",
                Category = -96,
                Price = 10000,
                Texture = $"Mods/{_modId}/BombRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_orange", "ring_item" }
            };

            data["NapalmRingPlus3"] = new ObjectData
            {
                Name = "NapalmRingPlus3",
                DisplayName = _i18n.Get("ring.napalm-plus3.name"),
                Description = _i18n.Get("ring.napalm-plus3.description"),
                Type = "Ring",
                Category = -96,
                Price = 15000,
                Texture = $"Mods/{_modId}/BombRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_orange", "ring_item" }
            };
            data["ChefRing"] = new ObjectData
            {
                Name = "ChefRing",
                DisplayName = _i18n.Get("ring.chef.name"),
                Description = _i18n.Get("ring.chef.description"),
                Type = "Ring",
                Category = -96,
                Price = 1000,
                Texture = $"Mods/{_modId}/ChefRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_white", "ring_item" }
            };

            data["ChefRing2"] = new ObjectData
            {
                Name = "ChefRing2",
                DisplayName = _i18n.Get("ring.chef2.name"),
                Description = _i18n.Get("ring.chef2.description"),
                Type = "Ring",
                Category = -96,
                Price = 500000,
                Texture = $"Mods/{_modId}/ChefRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_gold", "ring_item" }
            };

            data["ChefRing3"] = new ObjectData
            {
                Name = "ChefRing3",
                DisplayName = _i18n.Get("ring.chef3.name"),
                Description = _i18n.Get("ring.chef3.description"),
                Type = "Ring",
                Category = -96,
                Price = 600000,
                Texture = $"Mods/{_modId}/ChefRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_rainbow", "ring_item" }
            };

            data["LuckyRingPlus"] = new ObjectData
            {
                Name = "LuckyRingPlus",
                DisplayName = _i18n.Get("ring.lucky-plus.name"),
                Description = _i18n.Get("ring.lucky-plus.description"),
                Type = "Ring",
                Category = -96,
                Price = 100,
                Texture = $"Mods/{_modId}/LuckyRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_gold", "ring_item" }
            };

            data["LuckyRingPlus2"] = new ObjectData
            {
                Name = "LuckyRingPlus2",
                DisplayName = _i18n.Get("ring.lucky-plus2.name"),
                Description = _i18n.Get("ring.lucky-plus2.description"),
                Type = "Ring",
                Category = -96,
                Price = 100,
                Texture = $"Mods/{_modId}/LuckyRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_gold", "ring_item" }
            };

            data["LFishRing"] = new ObjectData
            {
                Name = "LFishRing",
                DisplayName = _i18n.Get("ring.lfish.name"),
                Description = _i18n.Get("ring.lfish.description"),
                Type = "Ring",
                Category = -96,
                Price = 1000,
                Texture = $"Mods/{_modId}/LFishRing",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_green", "ring_item" }
            };

            data["ExtraFishRing1"] = new ObjectData
            {
                Name = "ExtraFishRing1",
                DisplayName = _i18n.Get("ring.extrafish1.name"),
                Description = _i18n.Get("ring.extrafish1.description"),
                Type = "Ring",
                Category = -96,
                Price = 1000,
                Texture = $"Mods/{_modId}/ExtraFishRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_blue", "ring_item" }
            };

            data["ExtraFishRing2"] = new ObjectData
            {
                Name = "ExtraFishRing2",
                DisplayName = _i18n.Get("ring.extrafish2.name"),
                Description = _i18n.Get("ring.extrafish2.description"),
                Type = "Ring",
                Category = -96,
                Price = 2000,
                Texture = $"Mods/{_modId}/ExtraFishRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_blue", "ring_item" }
            };

            data["ExtraFishRing3"] = new ObjectData
            {
                Name = "ExtraFishRing3",
                DisplayName = _i18n.Get("ring.extrafish3.name"),
                Description = _i18n.Get("ring.extrafish3.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/ExtraFishRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_blue", "ring_item" }
            };

            data["QualityRing1"] = new ObjectData
            {
                Name = "QualityRing1",
                DisplayName = _i18n.Get("ring.quality1.name"),
                Description = _i18n.Get("ring.quality1.description"),
                Type = "Ring",
                Category = -96,
                Price = 1000,
                Texture = $"Mods/{_modId}/QualityRings",
                SpriteIndex = 0,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_purple", "ring_item" }
            };

            data["QualityRing2"] = new ObjectData
            {
                Name = "QualityRing2",
                DisplayName = _i18n.Get("ring.quality2.name"),
                Description = _i18n.Get("ring.quality2.description"),
                Type = "Ring",
                Category = -96,
                Price = 2000,
                Texture = $"Mods/{_modId}/QualityRings",
                SpriteIndex = 1,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_purple", "ring_item" }
            };

            data["QualityRing3"] = new ObjectData
            {
                Name = "QualityRing3",
                DisplayName = _i18n.Get("ring.quality3.name"),
                Description = _i18n.Get("ring.quality3.description"),
                Type = "Ring",
                Category = -96,
                Price = 3000,
                Texture = $"Mods/{_modId}/QualityRings",
                SpriteIndex = 2,
                CanBeGivenAsGift = false,
                CanBeTrashed = true,
                ContextTags = new List<string> { "color_purple", "ring_item" }
            };
        }

        private void EditCraftingRecipes(IDictionary<string, string> data)
        {
            var config = ModEntry.GetInstance().Config;

            if (config.EnableBurglarRingRecipes)
            {
                data["BurglarRingPlus"] = $"526 1 684 200 766 500 335 20/Home/BurglarRingPlus/false/Combat 3/{_i18n.Get("crafting.burglar-plus.name")}";

                data["BurglarRingPlus2"] = $"BurglarRingPlus 1 767 200 768 200 336 20/Home/BurglarRingPlus2/false/Combat 5/{_i18n.Get("crafting.burglar-plus2.name")}";

                data["BurglarRingPlus3"] = $"BurglarRingPlus2 1 74 1 769 200 337 20 881 100/Home/BurglarRingPlus3/false/Combat 7/{_i18n.Get("crafting.burglar-plus3.name")}";
            }

            if (config.EnableMonsterSpawnRingRecipes)
            {
                data["MonsterSpawnRing"] = $"335 100 766 200 767 10/Home/MonsterSpawnRing/false/Combat 2/{_i18n.Get("crafting.monster-spawn.name")}";
                data["MonsterSpawnRing2"] = $"MonsterSpawnRing 1 336 10 684 20 769 10/Home/MonsterSpawnRing2/false/Combat 4/{_i18n.Get("crafting.monster-spawn2.name")}";
                data["MonsterSpawnRing3"] = $"MonsterSpawnRing2 1 337 10 768 10 74 1/Home/MonsterSpawnRing3/false/Combat 6/{_i18n.Get("crafting.monster-spawn3.name")}";
            }

            if (config.EnableCombinedRingRecipes)
            {
                data["RubyRingCombined"] = $"534 2 64 10/Home/RubyRingCombined/false/Combat 3/{_i18n.Get("crafting.ruby-combined.name")}";
                data["RubyRingCombinedPlus"] = $"RubyRingCombined 2 64 20/Home/RubyRingCombinedPlus/false/Combat 5/{_i18n.Get("crafting.ruby-combined-plus.name")}";

                data["EmeraldRingCombined"] = $"533 2 60 10/Home/EmeraldRingCombined/false/Combat 3/{_i18n.Get("crafting.emerald-combined.name")}";
                data["EmeraldRingCombinedPlus"] = $"EmeraldRingCombined 2 60 20/Home/EmeraldRingCombinedPlus/false/Combat 5/{_i18n.Get("crafting.emerald-combined-plus.name")}";

                data["JadeRingCombined"] = $"532 2 70 10/Home/JadeRingCombined/false/Combat 3/{_i18n.Get("crafting.jade-combined.name")}";
                data["JadeRingCombinedPlus"] = $"JadeRingCombined 2 70 20/Home/JadeRingCombinedPlus/false/Combat 5/{_i18n.Get("crafting.jade-combined-plus.name")}";

                data["AquamarineRingCombined"] = $"531 2 62 10/Home/AquamarineRingCombined/false/Combat 3/{_i18n.Get("crafting.aquamarine-combined.name")}";
                data["AquamarineRingCombinedPlus"] = $"AquamarineRingCombined 2 62 20/Home/AquamarineRingCombinedPlus/false/Combat 5/{_i18n.Get("crafting.aquamarine-combined-plus.name")}";
            }

            if (config.EnableNapalmRingRecipes)
            {
                data["NapalmRingPlus"] = $"811 1 286 100/Home/NapalmRingPlus/false/Combat 4/{_i18n.Get("crafting.napalm-plus.name")}";
                data["NapalmRingPlus2"] = $"NapalmRingPlus 1 287 100/Home/NapalmRingPlus2/false/Combat 6/{_i18n.Get("crafting.napalm-plus2.name")}";
                data["NapalmRingPlus3"] = $"NapalmRingPlus2 1 288 100/Home/NapalmRingPlus3/false/Combat 8/{_i18n.Get("crafting.napalm-plus3.name")}";
            }

            if (config.EnableIridiumBandRecipe)
            {
                data["IridiumBandAlpha"] = $"534 1 533 1 531 1 532 1 337 10 74 1/Home/IridiumBandAlpha/false/Combat 8/{_i18n.Get("crafting.iridium-alpha.name")}";
            }

            if (config.EnableChefRingRecipes)
            {
                data["ChefRing"] = $"245 30 247 30 419 30 335 15/Home/ChefRing/false/Farming 1/{_i18n.Get("crafting.chef.name")}";

                data["ChefRing2"] = $"ChefRing 1 917 100 336 30 110 1/Home/ChefRing2/false/Farming 3/{_i18n.Get("crafting.chef2.name")}";

                data["ChefRing3"] = $"ChefRing2 1 74 1 337 30 499 20/Home/ChefRing3/false/Farming 5/{_i18n.Get("crafting.chef3.name")}";
            }

            if (config.EnableLuckyRingRecipes)
            {
                data["LuckyRingPlus"] = $"859 2 446 5/Home/LuckyRingPlus/false/Foraging 1/{_i18n.Get("crafting.lucky-plus.name")}";

                data["LuckyRingPlus2"] = $"LuckyRingPlus 1 279 1/Home/LuckyRingPlus2/false/Foraging 1/{_i18n.Get("crafting.lucky-plus2.name")}";
            }

            if (config.EnableLFishRingRecipe)
            {
                data["LFishRing"] = $"74 5 898 1 899 1 900 1 901 1 902 1/Home/LFishRing/false/Fishing 10/{_i18n.Get("crafting.lfish.name")}";
            }

            if (config.EnableFishRingRecipes)
            {
                data["ExtraFishRing1"] = $"152 30 153 30 157 30/Home/ExtraFishRing1/false/Fishing 1/{_i18n.Get("crafting.extrafish1.name")}";

                data["ExtraFishRing2"] = $"ExtraFishRing1 1 SeaJelly 10 RiverJelly 10 CaveJelly 10 910 3 74 3/Home/ExtraFishRing2/false/Fishing 5/{_i18n.Get("crafting.extrafish2.name")}";

                data["ExtraFishRing3"] = $"ExtraFishRing2 1 ChallengeBait 100 795 1 836 1 798 1/Home/ExtraFishRing3/false/Fishing 8/{_i18n.Get("crafting.extrafish3.name")}";
            }

            if (config.EnableQualityRingRecipes)
            {
                data["QualityRing1"] = $"92 999 685 999/Home/QualityRing1/false/Foraging 4/{_i18n.Get("crafting.quality1.name")}";

                data["QualityRing2"] = $"QualityRing1 1 92 1998 881 300 336 30/Home/QualityRing2/false/Foraging 7/{_i18n.Get("crafting.quality2.name")}";
                
                data["QualityRing3"] = $"QualityRing2 1 337 10 910 5 74 3 768 500 769 500/Home/QualityRing3/false/Foraging 10/{_i18n.Get("crafting.quality3.name")}";
            }
        }

        private static void EditLocationsData(IDictionary<string, LocationData> data)
        {
            var config = ModEntry.GetInstance().Config;
            if (!config.EnableLFishRingPatch)
                return;

            foreach (var location in data)
            {
                if (location.Value?.Fish != null)
                {
                    foreach (var fishSpawn in location.Value.Fish)
                    {
                        if ((fishSpawn.ItemId == "(O)898" && fishSpawn.Id == "(O)898") ||
                            (fishSpawn.ItemId == "(O)899" && fishSpawn.Id == "(O)899") ||
                            (fishSpawn.ItemId == "(O)900" && fishSpawn.Id == "(O)900") ||
                            (fishSpawn.ItemId == "(O)901" && fishSpawn.Id == "(O)901") ||
                            (fishSpawn.ItemId == "(O)902" && fishSpawn.Id == "(O)902"))
                        {
                            fishSpawn.Condition = $"PLAYER_HAS_LFISH_RING | {fishSpawn.Condition}";
                        }
                    }
                }
            }
        }
    }
}