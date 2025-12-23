using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace Enhanced_Rings
{
    public class ModEntry : Mod
    {
        private static ModEntry? Instance;
        internal static ModEntry GetInstance() => Instance!;
        private HarmonyPatcher? _harmonyPatcher;
        private AssetEditor? _assetEditor;
        private ConfigMenuManager? _configMenuManager;
        public ModConfig Config { get; set; } = new ModConfig();

        public override void Entry(IModHelper helper)
        {
            GameStateQueryManager.Initialize(this, Config, Monitor);
            this.Config = this.Helper.ReadConfig<ModConfig>();
            Instance = this;
            _harmonyPatcher = new HarmonyPatcher(ModManifest.UniqueID);
            _assetEditor = new AssetEditor(helper.Translation, ModManifest.UniqueID);
            _configMenuManager = new ConfigMenuManager(helper, ModManifest, Config);
            _harmonyPatcher.ApplyPatches();
            helper.Events.Content.AssetRequested += _assetEditor.OnAssetRequested;
            _configMenuManager.Initialize();
        }
    }
}