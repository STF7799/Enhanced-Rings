# Enhanced Rings

Adds more rings and ring-related gameplay hooks to Stardew Valley.

- **Author:** STF7799
- **Version:** 1.8.12
- **Nexus Mods:** https://www.nexusmods.com/stardewvalley/mods/39744
- **Unique ID:** `STF7799.EnhancedRings`

## Requirements

- Stardew Valley 1.6+
- SMAPI 4.0.0+
- Optional: Fishing Overhaul Revived (`Hiztaar.FishingOverhaulRevived`)

## Installation

1. Install SMAPI.
2. Download and extract the mod.
3. Place the `Enhanced Rings` folder into your `Stardew Valley/Mods` directory.
4. Launch the game through SMAPI.

## Features

- Additional rings with configurable effects.
- Ring-related monster spawn, drop, and slaying hooks.
- Fishing catch integration.
- Crop and cooking quality hooks.
- In-game configuration through Generic Mod Config Menu.
- Optional compatibility with Fishing Overhaul Revived.

## Configuration

The mod can be configured in game through Generic Mod Config Menu, or by
editing the generated config file after the first launch.

## Building from source

This repository contains the C# source and mod assets. It is source-only:
create a Stardew Valley SMAPI mod project targeting `net6.0`, set the game
path for your local Stardew Valley installation, and build with the .NET SDK.

```bash
dotnet build
```

The output DLL and `manifest.json` need to be placed together in the mod
folder. See the SMAPI modding documentation for the standard project setup.

## License

This project is **source-available, not OSI open source**.

You may use, modify, and redistribute the source and compiled mod for
**non-commercial purposes only**. Selling the mod, paywalling it, including it
in a paid modpack, or charging for access is not allowed.

See [LICENSE](LICENSE) for the full terms.

Third-party code, Stardew Valley, SMAPI, and other mods remain under their own
licenses and ownership.

## Credits

- STF7799 — author
- Hiztaar — Fishing Overhaul Revived compatibility target
- SMAPI and Stardew Valley modding community

## 中文说明

Enhanced Rings 为星露谷物语增加更多戒指和戒指相关的玩法钩子。

- 需要 Stardew Valley 1.6+ 和 SMAPI 4.0.0+
- 可选支持 Fishing Overhaul Revived
- 解压到 `Stardew Valley/Mods` 后通过 SMAPI 启动
- 可使用 Generic Mod Config Menu 配置

许可证为**源码可见、禁止商业使用**，不是 OSI 定义的开源许可证。允许非商业使用、修改和再分发；禁止售卖、付费墙、付费整合包等商业用途。完整条款见 [LICENSE](LICENSE)。
