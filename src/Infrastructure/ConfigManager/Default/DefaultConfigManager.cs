namespace YURI_Overlay;

internal partial class ConfigManager
{
	public static void ResetToDefault(JsonDatabase<Config> configDatabase)
	{
		ResetToDefault(configDatabase.Data);
		configDatabase.Save();
	}

	public static void ResetToDefault(Config config)
	{
		ResetGlobalSettings(config);
		ResetLargeMonsterUI(config);
		ResetSmallMonsterUI(config);
	}

	private static void ResetGlobalSettings(Config config)
	{
		var globalSettingsConfig = config.GlobalSettings;

		globalSettingsConfig.Localization = Constants.DefaultLocalization;

		globalSettingsConfig.GlobalScale.PositionScaleModifier = 1f;
		globalSettingsConfig.GlobalScale.SizeScaleModifier = 1f;

		globalSettingsConfig.Performance.CalculationCaching = true;

		globalSettingsConfig.Performance.UpdateDelays.ScreenManager.Update = 1f;
		globalSettingsConfig.Performance.UpdateDelays.PlayerManager.Update = 1f;

		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Name = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.MissionBeaconOffset = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.ModelRadius = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Health = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Stamina = 0.25f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Rage = 0.25f;
	}

	private static void ResetLargeMonsterUI(Config config)
	{
		ResetLargeMonsterDynamicUI(config);
		ResetLargeMonsterStaticUI(config);
	}

	private static void ResetLargeMonsterDynamicUI(Config config)
	{
		var largeMonsterDynamicUiConfig = config.LargeMonsterUI.Dynamic;

		largeMonsterDynamicUiConfig.Enabled = true;

		largeMonsterDynamicUiConfig.Settings.RenderDeadMonsters = false;
		largeMonsterDynamicUiConfig.Settings.RenderHighlightedMonster = true;
		largeMonsterDynamicUiConfig.Settings.RenderNonHighlightedMonsters = true;
		largeMonsterDynamicUiConfig.Settings.AddMissionBeaconOffsetToWorldOffset = false;
		largeMonsterDynamicUiConfig.Settings.AddModelRadiusToWorldOffsetY = true;
		largeMonsterDynamicUiConfig.Settings.OpacityFalloff = true;
		largeMonsterDynamicUiConfig.Settings.MaxDistance = 200f;

		largeMonsterDynamicUiConfig.WorldOffset.X = 0f;
		largeMonsterDynamicUiConfig.WorldOffset.Y = 0f;
		largeMonsterDynamicUiConfig.WorldOffset.Z = 0f;

		largeMonsterDynamicUiConfig.Offset.X = -148.5f;
		largeMonsterDynamicUiConfig.Offset.Y = 0f;

		largeMonsterDynamicUiConfig.NameLabel.Visible = true;
		largeMonsterDynamicUiConfig.NameLabel.Format = "{0}";
		largeMonsterDynamicUiConfig.NameLabel.Settings.RightAlignmentShift = 0;
		largeMonsterDynamicUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.NameLabel.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.NameLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Health.Visible = true;

		largeMonsterDynamicUiConfig.Health.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Health.Offset.Y = 15f;

		largeMonsterDynamicUiConfig.Health.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterDynamicUiConfig.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Offset.Y = 9f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Health.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Offset.X = 245f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Health.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterDynamicUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterDynamicUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x004D1BCC;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x004D1BCC;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0x34FF4ECC;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0x34FF4ECC;

		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;


		largeMonsterDynamicUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterDynamicUiConfig.Stamina.Visible = true;

		largeMonsterDynamicUiConfig.Stamina.Offset.X = 7;
		largeMonsterDynamicUiConfig.Stamina.Offset.Y = 25f;

		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Format = "{0:F0}/{1:F0}";
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Format = "{0:P0}";
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Settings.RightAlignmentShift = 4;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Stamina.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterDynamicUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Settings.RightAlignmentShift = 4;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyles.Inside;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFFD00080;

		largeMonsterDynamicUiConfig.Rage.Visible = true;

		largeMonsterDynamicUiConfig.Rage.Offset.X = 137f;
		largeMonsterDynamicUiConfig.Rage.Offset.Y = 25f;

		largeMonsterDynamicUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Format = "{0:F0}/{1:F0}";
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Settings.RightAlignmentShift = 4;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Rage.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Rage.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterDynamicUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterDynamicUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Settings.RightAlignmentShift = 4;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterDynamicUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Style = OutlineStyles.Inside;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFF000080;
	}

	private static void ResetLargeMonsterStaticUI(Config config)
	{
		var largeMonsterStaticUiConfig = config.LargeMonsterUI.Static;

		largeMonsterStaticUiConfig.Enabled = true;

		largeMonsterStaticUiConfig.Settings.RenderDeadMonsters = false;
		largeMonsterStaticUiConfig.Settings.RenderHighlightedMonster = true;
		largeMonsterStaticUiConfig.Settings.RenderNonHighlightedMonsters = true;

		largeMonsterStaticUiConfig.Position.X = 89f;
		largeMonsterStaticUiConfig.Position.Y = -40f;
		largeMonsterStaticUiConfig.Position.Anchor = Anchors.BottomLeft;

		largeMonsterStaticUiConfig.Spacing.X = 320f;
		largeMonsterStaticUiConfig.Spacing.Y = 0f;

		largeMonsterStaticUiConfig.Sorting.Type = Sortings.Name;
		largeMonsterStaticUiConfig.Sorting.ReversedOrder = false;

		largeMonsterStaticUiConfig.NameLabel.Visible = true;
		largeMonsterStaticUiConfig.NameLabel.Format = "{0}";
		largeMonsterStaticUiConfig.NameLabel.Settings.RightAlignmentShift = 0;
		largeMonsterStaticUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.NameLabel.Offset.Y = 0f;
		largeMonsterStaticUiConfig.NameLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Health.Visible = true;

		largeMonsterStaticUiConfig.Health.Offset.X = 0f;
		largeMonsterStaticUiConfig.Health.Offset.Y = 15f;

		largeMonsterStaticUiConfig.Health.ValueLabel.Visible = true;
		largeMonsterStaticUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterStaticUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Offset.Y = 9f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Health.PercentageLabel.Visible = true;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Offset.X = 245f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Health.Bar.Visible = true;
		largeMonsterStaticUiConfig.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterStaticUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterStaticUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x004016CC;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x004016CC;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0x34FF4ECC;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0x34FF4ECC;

		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterStaticUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterStaticUiConfig.Stamina.Visible = true;

		largeMonsterStaticUiConfig.Stamina.Offset.X = 140f;
		largeMonsterStaticUiConfig.Stamina.Offset.Y = 25f;

		largeMonsterStaticUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Settings.RightAlignmentShift = 4;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Offset.X = 54f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Stamina.Bar.Visible = true;
		largeMonsterStaticUiConfig.Stamina.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterStaticUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Size.Width = 94f;
		largeMonsterStaticUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterStaticUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Settings.RightAlignmentShift = 4;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Offset.X = 54f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Size.Width = 94f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyles.Inside;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFFD00080;

		largeMonsterStaticUiConfig.Rage.Visible = true;

		largeMonsterStaticUiConfig.Rage.Offset.X = 152f;
		largeMonsterStaticUiConfig.Rage.Offset.Y = 31f;

		largeMonsterStaticUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Rage.ValueLabel.Settings.RightAlignmentShift = 0;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Settings.RightAlignmentShift = 4;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Offset.X = 34f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Rage.Bar.Visible = true;
		largeMonsterStaticUiConfig.Rage.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterStaticUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Size.Width = 74f;
		largeMonsterStaticUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterStaticUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Style = OutlineStyles.Inside;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		largeMonsterStaticUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterStaticUiConfig.Rage.TimerLabel.Settings.RightAlignmentShift = 4;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Offset.X = 34f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		largeMonsterStaticUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		largeMonsterStaticUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Size.Width = 74f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Style = OutlineStyles.Inside;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFF000080;
	}

	private static void ResetSmallMonsterUI(Config config)
	{
		ResetSmallMonsterDynamicUI(config);
	}

	private static void ResetSmallMonsterDynamicUI(Config config)
	{
		var smallMonsterDynamicUiConfig = config.SmallMonsterUI;

		smallMonsterDynamicUiConfig.Enabled = true;

		smallMonsterDynamicUiConfig.Settings.RenderDeadMonsters = false;
		smallMonsterDynamicUiConfig.Settings.AddMissionBeaconOffsetToWorldOffset = false;
		smallMonsterDynamicUiConfig.Settings.AddModelRadiusToWorldOffsetY = true;
		smallMonsterDynamicUiConfig.Settings.OpacityFalloff = true;
		smallMonsterDynamicUiConfig.Settings.MaxDistance = 200f;

		smallMonsterDynamicUiConfig.WorldOffset.X = 0f;
		smallMonsterDynamicUiConfig.WorldOffset.Y = 0f;
		smallMonsterDynamicUiConfig.WorldOffset.Z = 0f;

		smallMonsterDynamicUiConfig.Offset.X = -81f;
		smallMonsterDynamicUiConfig.Offset.Y = 0f;

		smallMonsterDynamicUiConfig.NameLabel.Visible = true;
		smallMonsterDynamicUiConfig.NameLabel.Format = "{0}";
		smallMonsterDynamicUiConfig.NameLabel.Settings.RightAlignmentShift = 0;
		smallMonsterDynamicUiConfig.NameLabel.Offset.X = 7f;
		smallMonsterDynamicUiConfig.NameLabel.Offset.Y = 0f;
		smallMonsterDynamicUiConfig.NameLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		smallMonsterDynamicUiConfig.NameLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		smallMonsterDynamicUiConfig.Health.Visible = true;

		smallMonsterDynamicUiConfig.Health.Offset.X = 0f;
		smallMonsterDynamicUiConfig.Health.Offset.Y = 15f;

		smallMonsterDynamicUiConfig.Health.ValueLabel.Visible = false;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		smallMonsterDynamicUiConfig.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Offset.X = 7f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Offset.Y = 9f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		smallMonsterDynamicUiConfig.Health.PercentageLabel.Visible = false;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Offset.X = 245f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		smallMonsterDynamicUiConfig.Health.Bar.Visible = true;
		smallMonsterDynamicUiConfig.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		smallMonsterDynamicUiConfig.Health.Bar.Settings.Inverted = false;
		smallMonsterDynamicUiConfig.Health.Bar.Offset.X = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Offset.Y = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Size.Width = 162f;
		smallMonsterDynamicUiConfig.Health.Bar.Size.Height = 12f;

		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x004D1BCC;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x004D1BCC;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0x34FF4ECC;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0x34FF4ECC;

		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		smallMonsterDynamicUiConfig.Health.Bar.Outline.Visible = true;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Thickness = 2f;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Offset = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Style = OutlineStyles.Inside;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;
	}

}
