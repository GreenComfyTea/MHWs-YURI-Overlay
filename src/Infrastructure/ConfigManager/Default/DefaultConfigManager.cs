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
		ResetLargeMonsterUi(config);
		ResetSmallMonsterUi(config);
		ResetEndemicLifeUi(config);
		//ResetDamageMeterUI(config);
	}

	private static void ResetGlobalSettings(Config config)
	{
		var globalSettingsConfig = config.GlobalSettings;

		globalSettingsConfig.Localization = Constants.DefaultLocalization;

		globalSettingsConfig.GlobalScale.PositionScaleModifier = 1f;
		globalSettingsConfig.GlobalScale.SizeScaleModifier = 1f;

		globalSettingsConfig.GlobalScale.OverlayFontScale.ScaleWithReframeworkFontSize = false;
		globalSettingsConfig.GlobalScale.OverlayFontScale.OverlayFontScaleModifier = 1f;

		globalSettingsConfig.Performance.CalculationCaching = true;

		globalSettingsConfig.Performance.UpdateDelays.ScreenManager.Update = 1f;
		globalSettingsConfig.Performance.UpdateDelays.PlayerManager.Update = 1f;

		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Name = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.MissionBeaconOffset = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.ModelRadius = 1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Health = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Stamina = 0.25f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.Rage = 0.25f;
		globalSettingsConfig.Performance.UpdateDelays.LargeMonsters.MapPin = 0.25f;

		globalSettingsConfig.Performance.UpdateDelays.SmallMonsters.Name = 5f;
		globalSettingsConfig.Performance.UpdateDelays.SmallMonsters.MissionBeaconOffset = 5f;
		globalSettingsConfig.Performance.UpdateDelays.SmallMonsters.ModelRadius = 5f;
		globalSettingsConfig.Performance.UpdateDelays.SmallMonsters.Health = 0.5f;

		globalSettingsConfig.Performance.UpdateDelays.EndemicLife.Name = 5f;
		globalSettingsConfig.Performance.UpdateDelays.EndemicLife.ModelRadius = 5f;

		globalSettingsConfig.Performance.UpdateDelays.UIs.LargeMonsterDynamic = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.LargeMonsterStatic = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.LargeMonsterTargeted = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.LargeMonsterMapPin = 0.1f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.SmallMonsters = 0.2f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.EndemicLife = 0.2f;
		globalSettingsConfig.Performance.UpdateDelays.UIs.DamageMeter = 0.5f;
	}

	private static void ResetLargeMonsterUi(Config config)
	{
		config.LargeMonsterUI.Enabled = true;

		ResetLargeMonsterDynamicUi(config);
		ResetLargeMonsterStaticUi(config);
		ResetLargeMonsterTargetedUi(config);
		ResetLargeMonsterMapPinUi(config);
	}

	private static void ResetLargeMonsterDynamicUi(Config config)
	{
		var largeMonsterDynamicUiConfig = config.LargeMonsterUI.Dynamic;

		largeMonsterDynamicUiConfig.Enabled = true;

		largeMonsterDynamicUiConfig.Settings.RenderDeadMonsters = false;
		largeMonsterDynamicUiConfig.Settings.RenderTargetedMonster = true;
		largeMonsterDynamicUiConfig.Settings.RenderNonTargetedMonsters = true;
		largeMonsterDynamicUiConfig.Settings.RenderPinnedMonster = true;
		largeMonsterDynamicUiConfig.Settings.RenderNonPinnedMonsters = true;

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
		largeMonsterDynamicUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomLeft;
		largeMonsterDynamicUiConfig.NameLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.NameLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.NameLabel.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Health.Visible = true;

		largeMonsterDynamicUiConfig.Health.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Health.Offset.Y = 0f;

		largeMonsterDynamicUiConfig.Health.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterDynamicUiConfig.Health.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Offset.Y = 9f;

		largeMonsterDynamicUiConfig.Health.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Health.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Offset.X = 290f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Health.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Health.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterDynamicUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterDynamicUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start._1 = "#004D1BCC";
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start._2 = "#004D1BCC";

		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End._1 = "#34FF4ECC";
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End._2 = "#34FF4ECC";

		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Health.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterDynamicUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterDynamicUiConfig.Health.Bar.Outline.Color.Color = "#00000080";

		largeMonsterDynamicUiConfig.Stamina.Visible = true;

		largeMonsterDynamicUiConfig.Stamina.Offset.X = 7;
		largeMonsterDynamicUiConfig.Stamina.Offset.Y = 10f;

		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Format = "{0:F0}/{1:F0}";
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Format = "{0:P0}";
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Offset.X = 121f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Stamina.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterDynamicUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterDynamicUiConfig.Stamina.Bar.Outline.Color.Color = "#00000080";

		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Offset.X = 121f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterDynamicUiConfig.Stamina.TimerBar.Outline.Color.Color = "#FFD00080";

		largeMonsterDynamicUiConfig.Rage.Visible = true;

		largeMonsterDynamicUiConfig.Rage.Offset.X = 137f;
		largeMonsterDynamicUiConfig.Rage.Offset.Y = 10f;

		largeMonsterDynamicUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Format = "{0:F0}/{1:F0}";
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Rage.Bar.Visible = true;
		largeMonsterDynamicUiConfig.Rage.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterDynamicUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterDynamicUiConfig.Rage.Bar.Outline.Color.Color = "#00000080";

		largeMonsterDynamicUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Settings.FontSize = 16f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Offset.X = 87f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterDynamicUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Size.Width = 128f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterDynamicUiConfig.Rage.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterDynamicUiConfig.Rage.TimerBar.Outline.Color.Color = "#FF000080";
	}

	private static void ResetLargeMonsterStaticUi(Config config)
	{
		var largeMonsterStaticUiConfig = config.LargeMonsterUI.Static;

		largeMonsterStaticUiConfig.Enabled = true;

		largeMonsterStaticUiConfig.Settings.RenderDeadMonsters = false;
		largeMonsterStaticUiConfig.Settings.RenderTargetedMonster = true;
		largeMonsterStaticUiConfig.Settings.RenderNonTargetedMonsters = true;
		largeMonsterStaticUiConfig.Settings.RenderPinnedMonster = true;
		largeMonsterStaticUiConfig.Settings.RenderNonPinnedMonsters = true;

		largeMonsterStaticUiConfig.Position.X = 89f;
		largeMonsterStaticUiConfig.Position.Y = -26f;
		largeMonsterStaticUiConfig.Position.Anchor = AnchorEnum.BottomLeft;

		largeMonsterStaticUiConfig.Spacing.X = 311f;
		largeMonsterStaticUiConfig.Spacing.Y = 0f;

		largeMonsterStaticUiConfig.Sorting.ReversedOrder = false;
		largeMonsterStaticUiConfig.Sorting.Type = SortingEnum.Name;
		largeMonsterStaticUiConfig.Sorting.TargetedMonsterPriority = PriorityEnum.Higher2;
		largeMonsterStaticUiConfig.Sorting.PinnedMonsterPriority = PriorityEnum.Higher1;

		largeMonsterStaticUiConfig.NameLabel.Visible = true;
		largeMonsterStaticUiConfig.NameLabel.Format = "{0}";
		largeMonsterStaticUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomLeft;
		largeMonsterStaticUiConfig.NameLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.NameLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.NameLabel.Offset.Y = 2f;
		largeMonsterStaticUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Health.Visible = true;

		largeMonsterStaticUiConfig.Health.Offset.X = 0f;
		largeMonsterStaticUiConfig.Health.Offset.Y = 0f;

		largeMonsterStaticUiConfig.Health.ValueLabel.Visible = true;
		largeMonsterStaticUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Health.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterStaticUiConfig.Health.ValueLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Offset.Y = 9f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Health.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Health.PercentageLabel.Visible = true;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Health.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Offset.X = 290f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Health.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Health.Bar.Visible = true;
		largeMonsterStaticUiConfig.Health.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterStaticUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterStaticUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start._1 = "#004016CC";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.Start._2 = "#004016CC";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End._1 = "#34FF4ECC";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Foreground.End._2 = "#34FF4ECC";

		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterStaticUiConfig.Health.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterStaticUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterStaticUiConfig.Health.Bar.Outline.Color.Color = "#00000080";

		largeMonsterStaticUiConfig.Stamina.Visible = true;

		largeMonsterStaticUiConfig.Stamina.Offset.X = 140f;
		largeMonsterStaticUiConfig.Stamina.Offset.Y = 10f;

		largeMonsterStaticUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Offset.X = 87f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Stamina.Bar.Visible = true;
		largeMonsterStaticUiConfig.Stamina.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterStaticUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Size.Width = 94f;
		largeMonsterStaticUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterStaticUiConfig.Stamina.Bar.Outline.Color.Color = "#00000080";

		largeMonsterStaticUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Offset.X = 87f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Size.Width = 94f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterStaticUiConfig.Stamina.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterStaticUiConfig.Stamina.TimerBar.Outline.Color.Color = "#FFD00080";

		largeMonsterStaticUiConfig.Rage.Visible = true;

		largeMonsterStaticUiConfig.Rage.Offset.X = 152f;
		largeMonsterStaticUiConfig.Rage.Offset.Y = 16f;

		largeMonsterStaticUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterStaticUiConfig.Rage.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Offset.X = 67f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Rage.Bar.Visible = true;
		largeMonsterStaticUiConfig.Rage.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterStaticUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Size.Width = 74f;
		largeMonsterStaticUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterStaticUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterStaticUiConfig.Rage.Bar.Outline.Color.Color = "#00000080";

		largeMonsterStaticUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterStaticUiConfig.Rage.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Settings.FontSize = 16f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Offset.X = 67f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterStaticUiConfig.Rage.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterStaticUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterStaticUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Size.Width = 74f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterStaticUiConfig.Rage.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterStaticUiConfig.Rage.TimerBar.Outline.Color.Color = "#FF000080";
	}

	private static void ResetLargeMonsterTargetedUi(Config config)
	{
		var largeMonsterTargetedUiConfig = config.LargeMonsterUI.Targeted;

		largeMonsterTargetedUiConfig.Enabled = true;

		largeMonsterTargetedUiConfig.Settings.RenderDeadMonster = false;
		largeMonsterTargetedUiConfig.Settings.RenderTargetedMonster = true;
		largeMonsterTargetedUiConfig.Settings.RenderNonTargetedMonsters = true;
		largeMonsterTargetedUiConfig.Settings.RenderPinnedMonster = true;
		largeMonsterTargetedUiConfig.Settings.RenderNonPinnedMonsters = true;

		largeMonsterTargetedUiConfig.Position.X = 477f;
		largeMonsterTargetedUiConfig.Position.Y = -313f;
		largeMonsterTargetedUiConfig.Position.Anchor = AnchorEnum.BottomLeft;

		largeMonsterTargetedUiConfig.NameLabel.Visible = true;
		largeMonsterTargetedUiConfig.NameLabel.Format = "{0}";
		largeMonsterTargetedUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomLeft;
		largeMonsterTargetedUiConfig.NameLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.NameLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterTargetedUiConfig.NameLabel.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Health.Visible = true;

		largeMonsterTargetedUiConfig.Health.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Health.Offset.Y = 0f;

		largeMonsterTargetedUiConfig.Health.ValueLabel.Visible = true;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterTargetedUiConfig.Health.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Offset.Y = 9f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Health.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Health.PercentageLabel.Visible = true;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Offset.X = 290f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Health.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Health.Bar.Visible = true;
		largeMonsterTargetedUiConfig.Health.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterTargetedUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterTargetedUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterTargetedUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterTargetedUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.Start._1 = "#004016CC";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.Start._2 = "#004016CC";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.End._1 = "#34FF4ECC";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Foreground.End._2 = "#34FF4ECC";

		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Health.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterTargetedUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterTargetedUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterTargetedUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterTargetedUiConfig.Health.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterTargetedUiConfig.Health.Bar.Outline.Color.Color = "#00000080";

		largeMonsterTargetedUiConfig.Stamina.Visible = true;

		largeMonsterTargetedUiConfig.Stamina.Offset.X = 140f;
		largeMonsterTargetedUiConfig.Stamina.Offset.Y = 10f;

		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Stamina.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Format = "{0:P1}";
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Offset.X = 87f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Stamina.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Stamina.Bar.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterTargetedUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterTargetedUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterTargetedUiConfig.Stamina.Bar.Size.Width = 94f;
		largeMonsterTargetedUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterTargetedUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterTargetedUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterTargetedUiConfig.Stamina.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterTargetedUiConfig.Stamina.Bar.Outline.Color.Color = "#00000080";

		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Offset.X = 87f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Stamina.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Size.Width = 94f;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterTargetedUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterTargetedUiConfig.Stamina.TimerBar.Outline.Color.Color = "#FFD00080";

		largeMonsterTargetedUiConfig.Rage.Visible = true;

		largeMonsterTargetedUiConfig.Rage.Offset.X = 152f;
		largeMonsterTargetedUiConfig.Rage.Offset.Y = 16f;

		largeMonsterTargetedUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Rage.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Offset.X = 67f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Rage.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Rage.Bar.Visible = true;
		largeMonsterTargetedUiConfig.Rage.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterTargetedUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterTargetedUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterTargetedUiConfig.Rage.Bar.Size.Width = 74f;
		largeMonsterTargetedUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterTargetedUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterTargetedUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterTargetedUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterTargetedUiConfig.Rage.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterTargetedUiConfig.Rage.Bar.Outline.Color.Color = "#00000080";

		largeMonsterTargetedUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Settings.FontSize = 16f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Offset.X = 67f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterTargetedUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterTargetedUiConfig.Rage.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterTargetedUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Size.Width = 74f;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterTargetedUiConfig.Rage.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterTargetedUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterTargetedUiConfig.Rage.TimerBar.Outline.Color.Color = "#FF000080";
	}

	private static void ResetLargeMonsterMapPinUi(Config config)
	{
		var largeMonsterMapPinUiConfig = config.LargeMonsterUI.MapPin;

		largeMonsterMapPinUiConfig.Enabled = true;

		largeMonsterMapPinUiConfig.Settings.RenderDeadMonster = false;
		largeMonsterMapPinUiConfig.Settings.RenderTargetedMonster = true;
		largeMonsterMapPinUiConfig.Settings.RenderNonTargetedMonsters = true;
		largeMonsterMapPinUiConfig.Settings.RenderPinnedMonster = true;
		largeMonsterMapPinUiConfig.Settings.RenderNonPinnedMonsters = true;

		largeMonsterMapPinUiConfig.Position.X = 477f;
		largeMonsterMapPinUiConfig.Position.Y = -218f;
		largeMonsterMapPinUiConfig.Position.Anchor = AnchorEnum.BottomLeft;

		largeMonsterMapPinUiConfig.NameLabel.Visible = true;
		largeMonsterMapPinUiConfig.NameLabel.Format = "{0}";
		largeMonsterMapPinUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomLeft;
		largeMonsterMapPinUiConfig.NameLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.NameLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.NameLabel.Offset.X = 7f;
		largeMonsterMapPinUiConfig.NameLabel.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.NameLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.NameLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Health.Visible = true;

		largeMonsterMapPinUiConfig.Health.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Health.Offset.Y = 0f;

		largeMonsterMapPinUiConfig.Health.ValueLabel.Visible = true;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterMapPinUiConfig.Health.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Offset.X = 7f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Offset.Y = 9f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Health.ValueLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Health.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Health.PercentageLabel.Visible = true;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Offset.X = 290f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Health.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Health.Bar.Visible = true;
		largeMonsterMapPinUiConfig.Health.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterMapPinUiConfig.Health.Bar.Settings.Inverted = false;
		largeMonsterMapPinUiConfig.Health.Bar.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Health.Bar.Offset.Y = 0f;
		largeMonsterMapPinUiConfig.Health.Bar.Size.Width = 297f;
		largeMonsterMapPinUiConfig.Health.Bar.Size.Height = 12f;

		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.Start._1 = "#004016CC";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.Start._2 = "#004016CC";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.End._1 = "#34FF4ECC";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Foreground.End._2 = "#34FF4ECC";

		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Health.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterMapPinUiConfig.Health.Bar.Outline.Visible = true;
		largeMonsterMapPinUiConfig.Health.Bar.Outline.Thickness = 2f;
		largeMonsterMapPinUiConfig.Health.Bar.Outline.Offset = 0f;
		largeMonsterMapPinUiConfig.Health.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterMapPinUiConfig.Health.Bar.Outline.Color.Color = "#00000080";

		largeMonsterMapPinUiConfig.Stamina.Visible = true;

		largeMonsterMapPinUiConfig.Stamina.Offset.X = 140f;
		largeMonsterMapPinUiConfig.Stamina.Offset.Y = 10f;

		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Visible = false;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Offset.X = 7f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Stamina.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Visible = false;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Format = "{0:P1}";
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Offset.X = 87f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Stamina.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Stamina.Bar.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterMapPinUiConfig.Stamina.Bar.Settings.Inverted = false;
		largeMonsterMapPinUiConfig.Stamina.Bar.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Stamina.Bar.Offset.Y = 0f;
		largeMonsterMapPinUiConfig.Stamina.Bar.Size.Width = 94f;
		largeMonsterMapPinUiConfig.Stamina.Bar.Size.Height = 8f;

		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterMapPinUiConfig.Stamina.Bar.Outline.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.Bar.Outline.Thickness = 2f;
		largeMonsterMapPinUiConfig.Stamina.Bar.Outline.Offset = 0f;
		largeMonsterMapPinUiConfig.Stamina.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterMapPinUiConfig.Stamina.Bar.Outline.Color.Color = "#00000080";

		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Visible = false;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Format = "{0}";
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Offset.X = 87f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Stamina.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Stamina.TimerBar.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Settings.Inverted = true;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Offset.Y = 0f;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Size.Width = 94f;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Size.Height = 8f;

		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.Start._1 = "#403100CC";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.Start._2 = "#403100CC";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.End._1 = "#FFDA33CC";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Foreground.End._2 = "#FFDA33CC";

		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterMapPinUiConfig.Stamina.TimerBar.Outline.Visible = true;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Outline.Thickness = 2f;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Outline.Offset = 0f;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterMapPinUiConfig.Stamina.TimerBar.Outline.Color.Color = "#FFD00080";

		largeMonsterMapPinUiConfig.Rage.Visible = true;

		largeMonsterMapPinUiConfig.Rage.Offset.X = 152f;
		largeMonsterMapPinUiConfig.Rage.Offset.Y = 16f;

		largeMonsterMapPinUiConfig.Rage.ValueLabel.Visible = false;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Format = "{0:F1}/{1:F0}";
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Offset.X = 7f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Rage.ValueLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Rage.ValueLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Visible = false;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Format = "{0:P1}";
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Offset.X = 67f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Rage.PercentageLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Rage.Bar.Visible = true;
		largeMonsterMapPinUiConfig.Rage.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterMapPinUiConfig.Rage.Bar.Settings.Inverted = false;
		largeMonsterMapPinUiConfig.Rage.Bar.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Rage.Bar.Offset.Y = 0f;
		largeMonsterMapPinUiConfig.Rage.Bar.Size.Width = 74f;
		largeMonsterMapPinUiConfig.Rage.Bar.Size.Height = 8f;

		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.End._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.Bar.Colors.Background.End._2 = "#0000004A";

		largeMonsterMapPinUiConfig.Rage.Bar.Outline.Visible = true;
		largeMonsterMapPinUiConfig.Rage.Bar.Outline.Thickness = 2f;
		largeMonsterMapPinUiConfig.Rage.Bar.Outline.Offset = 0f;
		largeMonsterMapPinUiConfig.Rage.Bar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterMapPinUiConfig.Rage.Bar.Outline.Color.Color = "#00000080";

		largeMonsterMapPinUiConfig.Rage.TimerLabel.Visible = false;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Format = "{0}";
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Settings.Alignment = AnchorEnum.TopRight;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Settings.FontSize = 16f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Settings.MaxWidth = 0f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Offset.X = 67f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Offset.Y = 5f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Color.Color = "#FFFFFFFF";

		largeMonsterMapPinUiConfig.Rage.TimerLabel.Shadow.Visible = true;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Shadow.Offset.X = 2f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		largeMonsterMapPinUiConfig.Rage.TimerLabel.Shadow.Color.Color = "#000000FF";

		largeMonsterMapPinUiConfig.Rage.TimerBar.Visible = true;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Settings.Inverted = false;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Offset.X = 0f;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Offset.Y = 0f;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Size.Width = 74f;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Size.Height = 8f;

		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.Start._1 = "#40000BCC";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.Start._2 = "#40000BCC";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.End._1 = "#FF4242CC";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Foreground.End._2 = "#FF4242CC";

		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.Start._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.Start._2 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.End._1 = "#0000004A";
		largeMonsterMapPinUiConfig.Rage.TimerBar.Colors.Background.End._2 = "#0000004A";

		largeMonsterMapPinUiConfig.Rage.TimerBar.Outline.Visible = true;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Outline.Thickness = 2f;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Outline.Offset = 0f;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Outline.Style = OutlineStyleEnum.Inside;
		largeMonsterMapPinUiConfig.Rage.TimerBar.Outline.Color.Color = "#FF000080";
	}

	private static void ResetSmallMonsterUi(Config config)
	{
		ResetSmallMonsterDynamicUi(config);
	}

	private static void ResetSmallMonsterDynamicUi(Config config)
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
		smallMonsterDynamicUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomLeft;
		smallMonsterDynamicUiConfig.NameLabel.Settings.FontSize = 16f;
		smallMonsterDynamicUiConfig.NameLabel.Settings.MaxWidth = 0f;
		smallMonsterDynamicUiConfig.NameLabel.Offset.X = 7f;
		smallMonsterDynamicUiConfig.NameLabel.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		smallMonsterDynamicUiConfig.NameLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";

		smallMonsterDynamicUiConfig.Health.Visible = true;

		smallMonsterDynamicUiConfig.Health.Offset.X = 0f;
		smallMonsterDynamicUiConfig.Health.Offset.Y = 0f;

		smallMonsterDynamicUiConfig.Health.ValueLabel.Visible = false;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		smallMonsterDynamicUiConfig.Health.ValueLabel.Settings.Alignment = AnchorEnum.TopLeft;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Settings.FontSize = 16f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Settings.MaxWidth = 0f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Offset.X = 7f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Offset.Y = 9f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Color.Color = "#FFFFFFFF";

		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.Health.ValueLabel.Shadow.Color.Color = "#000000FF";

		smallMonsterDynamicUiConfig.Health.PercentageLabel.Visible = false;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Format = "{0:P1}";
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Settings.Alignment = AnchorEnum.TopRight;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Settings.FontSize = 16f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Settings.MaxWidth = 0f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Offset.X = 155f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Offset.Y = 9f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Color.Color = "#FFFFFFFF";

		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Visible = true;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.X = 2f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		smallMonsterDynamicUiConfig.Health.PercentageLabel.Shadow.Color.Color = "#000000FF";

		smallMonsterDynamicUiConfig.Health.Bar.Visible = true;
		smallMonsterDynamicUiConfig.Health.Bar.Settings.FillDirection = FillDirectionEnum.LeftToRight;
		smallMonsterDynamicUiConfig.Health.Bar.Settings.Inverted = false;
		smallMonsterDynamicUiConfig.Health.Bar.Offset.X = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Offset.Y = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Size.Width = 162f;
		smallMonsterDynamicUiConfig.Health.Bar.Size.Height = 9f;

		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start._1 = "#004D1BCC";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.Start._2 = "#004D1BCC";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End._1 = "#34FF4ECC";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Foreground.End._2 = "#34FF4ECC";

		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start._1 = "#0000004A";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.Start._2 = "#0000004A";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End._1 = "#0000004A";
		smallMonsterDynamicUiConfig.Health.Bar.Colors.Background.End._2 = "#0000004A";

		smallMonsterDynamicUiConfig.Health.Bar.Outline.Visible = true;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Thickness = 2f;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Offset = 0f;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Style = OutlineStyleEnum.Inside;
		smallMonsterDynamicUiConfig.Health.Bar.Outline.Color.Color = "#00000080";
	}

	private static void ResetEndemicLifeUi(Config config)
	{
		ResetEndemicLifeDynamicUi(config);
	}

	private static void ResetEndemicLifeDynamicUi(Config config)
	{
		var endemicLifeDynamicUiConfig = config.EndemicLifeUI;

		endemicLifeDynamicUiConfig.Enabled = true;

		endemicLifeDynamicUiConfig.Settings.AddModelRadiusToWorldOffsetY = true;
		endemicLifeDynamicUiConfig.Settings.OpacityFalloff = true;
		endemicLifeDynamicUiConfig.Settings.MaxDistance = 200f;

		endemicLifeDynamicUiConfig.WorldOffset.X = 0f;
		endemicLifeDynamicUiConfig.WorldOffset.Y = 0f;
		endemicLifeDynamicUiConfig.WorldOffset.Z = 0f;

		endemicLifeDynamicUiConfig.Offset.X = 0f;
		endemicLifeDynamicUiConfig.Offset.Y = 0f;

		endemicLifeDynamicUiConfig.NameLabel.Visible = true;
		endemicLifeDynamicUiConfig.NameLabel.Format = "{0}";
		endemicLifeDynamicUiConfig.NameLabel.Settings.Alignment = AnchorEnum.BottomCenter;
		endemicLifeDynamicUiConfig.NameLabel.Settings.FontSize = 16f;
		endemicLifeDynamicUiConfig.NameLabel.Settings.MaxWidth = 0f;
		endemicLifeDynamicUiConfig.NameLabel.Offset.X = 7f;
		endemicLifeDynamicUiConfig.NameLabel.Offset.Y = 0f;
		endemicLifeDynamicUiConfig.NameLabel.Color.Color = "#FFFFFFFF";

		endemicLifeDynamicUiConfig.NameLabel.Shadow.Visible = true;
		endemicLifeDynamicUiConfig.NameLabel.Shadow.Offset.X = 2f;
		endemicLifeDynamicUiConfig.NameLabel.Shadow.Offset.Y = 2f;
		endemicLifeDynamicUiConfig.NameLabel.Shadow.Color.Color = "#000000FF";
	}

	//private static void ResetDamageMeterUI(Config config)
	//{
	//	ResetDamageMeterStaticUI(config);
	//}

	//private static void ResetDamageMeterStaticUI(Config config)
	//{
	//	var damageMeterStaticUiConfig = config.DamageMeterUI;

	//	damageMeterStaticUiConfig.Enabled = true;

	//	damageMeterStaticUiConfig.Settings.RenderLocalPlayer = true;
	//	damageMeterStaticUiConfig.Settings.RenderOtherPlayers = false;
	//	damageMeterStaticUiConfig.Settings.RenderSupportHunters = false;

	//	damageMeterStaticUiConfig.Position.X = -18f;
	//	damageMeterStaticUiConfig.Position.Y = -62f;
	//	damageMeterStaticUiConfig.Position.Anchor = Anchor.BottomCenter;

	//	damageMeterStaticUiConfig.Spacing.X = 0f;
	//	damageMeterStaticUiConfig.Spacing.Y = -29f;

	//	damageMeterStaticUiConfig.Sorting.ReversedOrder = true;
	//	damageMeterStaticUiConfig.Sorting.Type = DamageMeterSortingEnum.Damage;
	//	damageMeterStaticUiConfig.Sorting.LocalPlayerPriority = Priority.Normal;

	//	damageMeterStaticUiConfig.LocalPlayer.Enabled = true;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Visible = false;
	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Format = "[{0}:{1}]";

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Offset.X = -7f;
	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.HunterMasterRanksLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Visible = true;
	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Format = "[{2}:{3}] {0}";

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Settings.Alignment = Anchor.BottomLeft;
	//  damageMeterStaticUiConfig.LocalPlayer.NameLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Settings.MaxWidth = 140f;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Offset.X = 7f;
	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.NameLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Offset.X = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Offset.X = 286f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Visible = true;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Offset.X = 353f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.Start._1 = 0x401F0CCC;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.Start._2 = 0x401F0CCC;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.End._1 = 0xF1955ECC; // Same as InGameColor_070_GUI_Psolo = 0xFFF1955E (ARGB)
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Foreground.End._2 = 0xF1955ECC; // Same as InGameColor_070_GUI_Psolo = 0xFFF1955E (ARGB)

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.LocalPlayer.Damage.Bar.Outline.Color.Color = 0x00000080;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Offset.X = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Settings.Alignment = Anchor.BottomRight;
//  damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Offset.X = 198f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Visible = false;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Offset.X = 353f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Offset.Y = 23f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Visible = false;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Offset.Y = 7f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.Start._1 = 0x401F0CCC;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.Start._2 = 0x401F0CCC;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.End._1 = 0xF1955ECC; // Same as InGameColor_070_GUI_Psolo = 0xFFF1955E (ARGB)
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Foreground.End._2 = 0xF1955ECC; // Same as InGameColor_070_GUI_Psolo = 0xFFF1955E (ARGB)

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.LocalPlayer.DPS.Bar.Outline.Color.Color = 0x00000080;

	//	damageMeterStaticUiConfig.OtherPlayers.Enabled = true;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Visible = false;
	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Format = "[{0}:{1}]";

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Offset.X = -7f;
	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.HunterMasterRanksLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Visible = true;
	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Format = "[{2}:{3}] {0}";

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Settings.Alignment = Anchor.BottomLeft;
	//  damageMeterStaticUiConfig.OtherPlayers.NameLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Settings.MaxWidth = 140f;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Offset.X = 7f;
	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.NameLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Offset.X = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Offset.X = 286f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Visible = true;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Offset.X = 353f;
	//	;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.Start._1 = 0x0D2740CC;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.Start._2 = 0x0D2740CC;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.End._1 = 0x61ACF2CC; // Same as InGameColor_072_GUI_P2 = 0xFF61ACF2 (ARGB)
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Foreground.End._2 = 0x61ACF2CC; // Same as InGameColor_072_GUI_P2 = 0xFF61ACF2 (ARGB)

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.OtherPlayers.Damage.Bar.Outline.Color.Color = 0x00000080;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Offset.X = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Offset.X = 198f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Visible = false;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Offset.X = 353f;
	//	;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Offset.Y = 23f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Visible = false;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Offset.Y = 7f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.Start._1 = 0x0D2740CC;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.Start._2 = 0x0D2740CC;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.End._1 = 0x61ACF2CC; // Same as InGameColor_072_GUI_P2 = 0xFF61ACF2 (ARGB)
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Foreground.End._2 = 0x61ACF2CC; // Same as InGameColor_072_GUI_P2 = 0xFF61ACF2 (ARGB)


	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.OtherPlayers.DPS.Bar.Outline.Color.Color = 0x00000080;

	//	damageMeterStaticUiConfig.SupportHunters.Enabled = true;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Visible = false;
	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Format = "[{0}:{1}]";

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Offset.X = -7f;
	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.HunterMasterRanksLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Visible = true;
	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Format = "[{2}:{3}] {0}";

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Settings.Alignment = Anchor.BottomLeft;
	//  damageMeterStaticUiConfig.SupportHunters.NameLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Settings.MaxWidth = 140f;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Offset.X = 7f;
	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.NameLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Offset.X = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Offset.X = 286f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Visible = true;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Offset.X = 353f;
	//	;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.Start._1 = 0x1D1621CC;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.Start._2 = 0x1D1621CC;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.End._1 = 0xCAB7D4CC; // Same as InGameColor_075_GUI_PNPC = 0xFFCAB7D4 (ARGB)
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Foreground.End._2 = 0xCAB7D4CC; // Same as InGameColor_075_GUI_PNPC = 0xFFCAB7D4 (ARGB)


	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.SupportHunters.Damage.Bar.Outline.Color.Color = 0x00000080;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Offset.X = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Offset.Y = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Visible = true;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Format = "{0:F1}";

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Offset.X = 198f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.ValueLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Visible = false;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Format = "{0:P1}";

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Settings.Alignment = Anchor.BottomRight;
	//  damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Settings.FontSize = 16f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Settings.MaxWidth = 0f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Offset.X = 353f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Offset.Y = 23f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Color.Color = 0xFFFFFFFF;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Shadow.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Shadow.Offset.X = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Shadow.Offset.Y = 2f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.PercentageLabel.Shadow.Color.Color = 0x000000FF;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Visible = false;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Offset.X = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Offset.Y = 7f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Size.Width = 360f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Size.Height = 10f;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.Start._1 = 0x1D1621CC;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.Start._2 = 0x1D1621CC;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.End._1 = 0xCAB7D4CC; // Same as InGameColor_075_GUI_PNPC = 0xFFCAB7D4 (ARGB)
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Foreground.End._2 = 0xCAB7D4CC; // Same as InGameColor_075_GUI_PNPC = 0xFFCAB7D4 (ARGB)

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.Start._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.Start._2 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.End.SplitIntoTwoColors = false;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.End._1 = 0x0000004A;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Colors.Background.End._2 = 0x0000004A;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Outline.Visible = true;

	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Outline.Thickness = 2f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Outline.Offset = 0f;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Outline.Style = OutlineStyle.Inside;
	//	damageMeterStaticUiConfig.SupportHunters.DPS.Bar.Outline.Color.Color = 0x00000080;
	//}
}