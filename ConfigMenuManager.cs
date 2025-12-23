using GenericModConfigMenu;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace Enhanced_Rings
{
    internal class ConfigMenuManager
    {
        private readonly IModHelper _helper;
        private readonly IManifest _manifest;
        private readonly ModConfig _config;

        public ConfigMenuManager(IModHelper helper, IManifest manifest, ModConfig config)
        {
            _helper = helper;
            _manifest = manifest;
            _config = config;
        }

        public void Initialize()
        {
            _helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = _helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: _manifest,
                reset: () => ResetConfig(),
                save: () => SaveConfig()
            );

            AddBurglarRingSettings(configMenu);
            AddMonsterSpawnRingSettings(configMenu);
            AddCombinedRingSettings(configMenu);
            AddIridiumBandSettings(configMenu);
            AddNapalmRingSettings(configMenu);
            AddLuckyRingSettings(configMenu);
            AddFishingRingSettings(configMenu);
            AddRecipeSettings(configMenu);
            AddPatchSettings(configMenu);
        }

        private void AddBurglarRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.burglar.title"),
                tooltip: () => _helper.Translation.Get("config.section.burglar.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.burglarringplus.rounds.name"),
                tooltip: () => _helper.Translation.Get("config.burglarringplus.rounds.tooltip"),
                getValue: () => _config.BurglarRingPlusRounds,
                setValue: value => _config.BurglarRingPlusRounds = value,
                min: 0,
                max: 50
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.burglarringplus2.rounds.name"),
                tooltip: () => _helper.Translation.Get("config.burglarringplus2.rounds.tooltip"),
                getValue: () => _config.BurglarRingPlus2Rounds,
                setValue: value => _config.BurglarRingPlus2Rounds = value,
                min: 0,
                max: 50
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.burglarringplus3.rounds.name"),
                tooltip: () => _helper.Translation.Get("config.burglarringplus3.rounds.tooltip"),
                getValue: () => _config.BurglarRingPlus3Rounds,
                setValue: value => _config.BurglarRingPlus3Rounds = value,
                min: 0,
                max: 50
            );
        }

        private void AddMonsterSpawnRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.monster-spawn.title"),
                tooltip: () => _helper.Translation.Get("config.section.monster-spawn.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.monsterspawnring.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.monsterspawnring.multiplier.tooltip"),
                getValue: () => (float)_config.MonsterSpawnRingMultiplier,
                setValue: value => _config.MonsterSpawnRingMultiplier = Math.Round(value, 1),
                min: 1.0f,
                max: 5.0f,
                interval: 0.1f,
                formatValue: value => $"{value:F1}x"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.monsterspawnring2.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.monsterspawnring2.multiplier.tooltip"),
                getValue: () => (float)_config.MonsterSpawnRing2Multiplier,
                setValue: value => _config.MonsterSpawnRing2Multiplier = Math.Round(value, 1),
                min: 1.0f,
                max: 5.0f,
                interval: 0.1f,
                formatValue: value => $"{value:F1}x"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.monsterspawnring3.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.monsterspawnring3.multiplier.tooltip"),
                getValue: () => (float)_config.MonsterSpawnRing3Multiplier,
                setValue: value => _config.MonsterSpawnRing3Multiplier = Math.Round(value, 1),
                min: 1.0f,
                max: 5.0f,
                interval: 0.1f,
                formatValue: value => $"{value:F1}x"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.maxmonsterspawnchance.name"),
                tooltip: () => _helper.Translation.Get("config.maxmonsterspawnchance.tooltip"),
                getValue: () => (float)_config.MaxMonsterSpawnChance,
                setValue: value => _config.MaxMonsterSpawnChance = Math.Round(value, 2),
                min: 0.1f,
                max: 0.8f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.affectskullcavern.name"),
                tooltip: () => _helper.Translation.Get("config.affectskullcavern.tooltip"),
                getValue: () => _config.AffectSkullCavern,
                setValue: value => _config.AffectSkullCavern = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.affectvolcanodungeon.name"),
                tooltip: () => _helper.Translation.Get("config.affectvolcanodungeon.tooltip"),
                getValue: () => _config.AffectVolcanoDungeon,
                setValue: value => _config.AffectVolcanoDungeon = value
            );
        }

        private void AddCombinedRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.combined-rings.title"),
                tooltip: () => _helper.Translation.Get("config.section.combined-rings.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.ruby-combined.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.ruby-combined.multiplier.tooltip"),
                getValue: () => _config.RubyRingCombinedMultiplier,
                setValue: value => _config.RubyRingCombinedMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.ruby-combined-plus.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.ruby-combined-plus.multiplier.tooltip"),
                getValue: () => _config.RubyRingCombinedPlusMultiplier,
                setValue: value => _config.RubyRingCombinedPlusMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.emerald-combined.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.emerald-combined.multiplier.tooltip"),
                getValue: () => _config.EmeraldRingCombinedMultiplier,
                setValue: value => _config.EmeraldRingCombinedMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.emerald-combined-plus.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.emerald-combined-plus.multiplier.tooltip"),
                getValue: () => _config.EmeraldRingCombinedPlusMultiplier,
                setValue: value => _config.EmeraldRingCombinedPlusMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.jade-combined.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.jade-combined.multiplier.tooltip"),
                getValue: () => _config.JadeRingCombinedMultiplier,
                setValue: value => _config.JadeRingCombinedMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.jade-combined-plus.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.jade-combined-plus.multiplier.tooltip"),
                getValue: () => _config.JadeRingCombinedPlusMultiplier,
                setValue: value => _config.JadeRingCombinedPlusMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.aquamarine-combined.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.aquamarine-combined.multiplier.tooltip"),
                getValue: () => _config.AquamarineRingCombinedMultiplier,
                setValue: value => _config.AquamarineRingCombinedMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.aquamarine-combined-plus.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.aquamarine-combined-plus.multiplier.tooltip"),
                getValue: () => _config.AquamarineRingCombinedPlusMultiplier,
                setValue: value => _config.AquamarineRingCombinedPlusMultiplier = value,
                min: 0.1f,
                max: 1.0f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );
        }

        private void AddIridiumBandSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.iridium-alpha.title"),
                tooltip: () => _helper.Translation.Get("config.section.iridium-alpha.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.iridium-alpha.attack.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.iridium-alpha.attack.multiplier.tooltip"),
                getValue: () => _config.IridiumBandAlphaAttackMultiplier,
                setValue: value => _config.IridiumBandAlphaAttackMultiplier = value,
                min: 0.05f,
                max: 0.3f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.iridium-alpha.speed.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.iridium-alpha.speed.multiplier.tooltip"),
                getValue: () => _config.IridiumBandAlphaSpeedMultiplier,
                setValue: value => _config.IridiumBandAlphaSpeedMultiplier = value,
                min: 0.05f,
                max: 0.3f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.iridium-alpha.crit-power.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.iridium-alpha.crit-power.multiplier.tooltip"),
                getValue: () => _config.IridiumBandAlphaCritPowerMultiplier,
                setValue: value => _config.IridiumBandAlphaCritPowerMultiplier = value,
                min: 0.05f,
                max: 0.3f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.iridium-alpha.crit-chance.multiplier.name"),
                tooltip: () => _helper.Translation.Get("config.iridium-alpha.crit-chance.multiplier.tooltip"),
                getValue: () => _config.IridiumBandAlphaCritChanceMultiplier,
                setValue: value => _config.IridiumBandAlphaCritChanceMultiplier = value,
                min: 0.05f,
                max: 0.3f,
                interval: 0.05f,
                formatValue: value => $"{value:P0}"
            );
        }

        private void AddNapalmRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.napalm.title"),
                tooltip: () => _helper.Translation.Get("config.section.napalm.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.napalmringplus.radius.name"),
                tooltip: () => _helper.Translation.Get("config.napalmringplus.radius.tooltip"),
                getValue: () => _config.NapalmRingPlusRadius,
                setValue: value => _config.NapalmRingPlusRadius = value,
                min: 2,
                max: 10
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.napalmringplus2.radius.name"),
                tooltip: () => _helper.Translation.Get("config.napalmringplus2.radius.tooltip"),
                getValue: () => _config.NapalmRingPlus2Radius,
                setValue: value => _config.NapalmRingPlus2Radius = value,
                min: 3,
                max: 15
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.napalmringplus3.radius.name"),
                tooltip: () => _helper.Translation.Get("config.napalmringplus3.radius.tooltip"),
                getValue: () => _config.NapalmRingPlus3Radius,
                setValue: value => _config.NapalmRingPlus3Radius = value,
                min: 5,
                max: 20
            );
        }

        private void AddLuckyRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.lucky.title"),
                tooltip: () => _helper.Translation.Get("config.section.lucky.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.luckyringplus.luck.name"),
                tooltip: () => _helper.Translation.Get("config.luckyringplus.luck.tooltip"),
                getValue: () => _config.LuckyRingPlusLuck,
                setValue: value => _config.LuckyRingPlusLuck = value,
                min: 0,
                max: 10
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.luckyringplus2.luck.name"),
                tooltip: () => _helper.Translation.Get("config.luckyringplus2.luck.tooltip"),
                getValue: () => _config.LuckyRingPlus2Luck,
                setValue: value => _config.LuckyRingPlus2Luck = value,
                min: 3,
                max: 15
            );
        }

        private void AddFishingRingSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.fishing.title"),
                tooltip: () => _helper.Translation.Get("config.section.fishing.tooltip")
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.extra-fish-num1.name"),
                tooltip: () => _helper.Translation.Get("config.extra-fish-num1.tooltip"),
                getValue: () => _config.ExtraFishNum1,
                setValue: value => _config.ExtraFishNum1 = value,
                min: 1,
                max: 10
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.extra-fish-num2.name"),
                tooltip: () => _helper.Translation.Get("config.extra-fish-num2.tooltip"),
                getValue: () => _config.ExtraFishNum2,
                setValue: value => _config.ExtraFishNum2 = value,
                min: 2,
                max: 15
            );

            configMenu.AddNumberOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.extra-fish-num3.name"),
                tooltip: () => _helper.Translation.Get("config.extra-fish-num3.tooltip"),
                getValue: () => _config.ExtraFishNum3,
                setValue: value => _config.ExtraFishNum3 = value,
                min: 3,
                max: 20
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-notfish-available.name"),
                tooltip: () => _helper.Translation.Get("config.enable-notfish-available.tooltip"),
                getValue: () => _config.EnableNotfishAvailable,
                setValue: value => _config.EnableNotfishAvailable = value
            );
        }

        private void AddRecipeSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.recipes.title"),
                tooltip: () => _helper.Translation.Get("config.section.recipes.tooltip")
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-combined-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-combined-ring-recipes.tooltip"),
                getValue: () => _config.EnableCombinedRingRecipes,
                setValue: value => _config.EnableCombinedRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-iridium-band-recipe.name"),
                tooltip: () => _helper.Translation.Get("config.enable-iridium-band-recipe.tooltip"),
                getValue: () => _config.EnableIridiumBandRecipe,
                setValue: value => _config.EnableIridiumBandRecipe = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-burglar-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-burglar-ring-recipes.tooltip"),
                getValue: () => _config.EnableBurglarRingRecipes,
                setValue: value => _config.EnableBurglarRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-monster-spawn-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-monster-spawn-ring-recipes.tooltip"),
                getValue: () => _config.EnableMonsterSpawnRingRecipes,
                setValue: value => _config.EnableMonsterSpawnRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-napalm-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-napalm-ring-recipes.tooltip"),
                getValue: () => _config.EnableNapalmRingRecipes,
                setValue: value => _config.EnableNapalmRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-chef-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-chef-ring-recipes.tooltip"),
                getValue: () => _config.EnableChefRingRecipes,
                setValue: value => _config.EnableChefRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-lucky-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-lucky-ring-recipes.tooltip"),
                getValue: () => _config.EnableLuckyRingRecipes,
                setValue: value => _config.EnableLuckyRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-lfish-ring-recipe.name"),
                tooltip: () => _helper.Translation.Get("config.enable-lfish-ring-recipe.tooltip"),
                getValue: () => _config.EnableLFishRingRecipe,
                setValue: value => _config.EnableLFishRingRecipe = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-fish-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-fish-ring-recipes.tooltip"),
                getValue: () => _config.EnableFishRingRecipes,
                setValue: value => _config.EnableFishRingRecipes = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-quality-ring-recipes.name"),
                tooltip: () => _helper.Translation.Get("config.enable-quality-ring-recipes.tooltip"),
                getValue: () => _config.EnableQualityRingRecipes,
                setValue: value => _config.EnableQualityRingRecipes = value
            );
        }

        private void AddPatchSettings(IGenericModConfigMenuApi configMenu)
        {
            configMenu.AddSectionTitle(
                mod: _manifest,
                text: () => _helper.Translation.Get("config.section.patches.title"),
                tooltip: () => _helper.Translation.Get("config.section.patches.tooltip")
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-chef-ring-patch.name"),
                tooltip: () => _helper.Translation.Get("config.enable-chef-ring-patch.tooltip"),
                getValue: () => _config.EnableChefRingPatch,
                setValue: value => _config.EnableChefRingPatch = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-lfish-ring-patch.name"),
                tooltip: () => _helper.Translation.Get("config.enable-lfish-ring-patch.tooltip"),
                getValue: () => _config.EnableLFishRingPatch,
                setValue: value => _config.EnableLFishRingPatch = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-fish-ring-patch.name"),
                tooltip: () => _helper.Translation.Get("config.enable-fish-ring-patch.tooltip"),
                getValue: () => _config.EnableFishRingPatch,
                setValue: value => _config.EnableFishRingPatch = value
            );

            configMenu.AddBoolOption(
                mod: _manifest,
                name: () => _helper.Translation.Get("config.enable-crop-quality-ring-patch.name"),
                tooltip: () => _helper.Translation.Get("config.enable-crop-quality-ring-patch.tooltip"),
                getValue: () => _config.EnableCropQualityRingPatch,
                setValue: value => _config.EnableCropQualityRingPatch = value
            );
        }

        private void ResetConfig()
        {
            // 强盗戒指配置
            _config.BurglarRingPlusRounds = 5;
            _config.BurglarRingPlus2Rounds = 10;
            _config.BurglarRingPlus3Rounds = 15;

            // 怪物生成戒指配置
            _config.MonsterSpawnRingMultiplier = 1.5;
            _config.MonsterSpawnRing2Multiplier = 2.0;
            _config.MonsterSpawnRing3Multiplier = 3.0;
            _config.MaxMonsterSpawnChance = 0.5;
            _config.AffectSkullCavern = true;
            _config.AffectVolcanoDungeon = true;

            // 组合戒指配置
            _config.RubyRingCombinedMultiplier = 0.2f;
            _config.RubyRingCombinedPlusMultiplier = 0.4f;
            _config.EmeraldRingCombinedMultiplier = 0.2f;
            _config.EmeraldRingCombinedPlusMultiplier = 0.4f;
            _config.JadeRingCombinedMultiplier = 0.2f;
            _config.JadeRingCombinedPlusMultiplier = 0.4f;
            _config.AquamarineRingCombinedMultiplier = 0.2f;
            _config.AquamarineRingCombinedPlusMultiplier = 0.4f;

            // 铱环Alpha配置
            _config.IridiumBandAlphaAttackMultiplier = 0.1f;
            _config.IridiumBandAlphaSpeedMultiplier = 0.1f;
            _config.IridiumBandAlphaCritPowerMultiplier = 0.1f;
            _config.IridiumBandAlphaCritChanceMultiplier = 0.1f;

            // 凝固汽油戒指配置
            _config.NapalmRingPlusRadius = 3;
            _config.NapalmRingPlus2Radius = 5;
            _config.NapalmRingPlus3Radius = 7;

            // 幸运戒指配置
            _config.LuckyRingPlusLuck = 3;
            _config.LuckyRingPlus2Luck = 5;

            // 钓鱼戒指配置
            _config.ExtraFishNum1 = 1;
            _config.ExtraFishNum2 = 3;
            _config.ExtraFishNum3 = 5;

            // 配方开关配置
            _config.EnableLuckyRingRecipes = true;
            _config.EnableCombinedRingRecipes = true;
            _config.EnableIridiumBandRecipe = true;
            _config.EnableBurglarRingRecipes = true;
            _config.EnableMonsterSpawnRingRecipes = true;
            _config.EnableNapalmRingRecipes = true;
            _config.EnableChefRingRecipes = true;
            _config.EnableLFishRingRecipe = true;
            _config.EnableFishRingRecipes = true;

            // 补丁开关配置
            _config.EnableChefRingPatch = true;
            _config.EnableLFishRingPatch = true;
            _config.EnableFishRingPatch = true;
            _config.EnableCropQualityRingPatch = true;
            _config.EnableNotfishAvailable = false;
        }

        private void SaveConfig()
        {
            _helper.WriteConfig(_config);
        }
    }
}