# Axe Boy

Godot + C# 项目，包含角色“罗比·怀特”及其卡牌、能力与遗物。

**项目概述**
- **角色**: 罗比·怀特
- **核心机制**: **怨灵**、**存在感**、**暴露**等。
- **内容**: 自定义卡牌、能力、遗物以及相关资源。

**主要内容**
- **代码**: [Scripts](axeboy/Scripts/) 中包含卡牌、角色、力量与遗物实现。
- **本地化**: 见 [axeboy/localization/zhs](axeboy/localization/zhs)（`cards.json`, `powers.json`, `characters.json`, `relics.json`）。
- **资源**: 美术与字体位于 [axeboy/](axeboy/) 子目录下的 `images/`、`fonts/` 等。

**快速开始 / 构建**
- 需要: .NET SDK（与项目兼容的版本）和 Godot 编辑器。
- 在项目根目录构建（Windows PowerShell 示例）:

```powershell
dotnet build AxeBoy.csproj
```

- 使用 Godot 打开项目文件 `project.godot`，在编辑器中运行。

**本地化与命名**
- 名称与描述以 `axeboy/localization/zhs/*.json` 为准；README 中提及的若干术语来自这些文件（示例：`怨灵`、`存在感`、`窥视者`、`安息树枝`）。

**贡献**
- 欢迎提交 PR 或 issue。请在 PR 描述中说明变更范围（卡牌/力量/资源/本地化）。

