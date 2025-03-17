namespace YURI_Overlay;

internal static class DefaultConfig
{
	public static void ResetTo(JsonDatabase<Config> configDatabase)
	{
		var config = configDatabase.Data;

		config.GlobalSettings.Localization = Constants.DefaultLocalization;

		config.GlobalSettings.GlobalScale.PositionScaleModifier = 1f;
		config.GlobalSettings.GlobalScale.SizeScaleModifier = 1f;

		config.GlobalSettings.Performance.UpdateDelay = 0.1f;
		config.GlobalSettings.Performance.CalculationCaching = true;

		config.LargeMonsterUI.Dynamic.Settings.RenderDeadOrCaptured = false;
		config.LargeMonsterUI.Dynamic.Settings.RenderHighlightedMonster = true;
		config.LargeMonsterUI.Dynamic.Settings.RenderNotHighlightedMonsters = true;
		config.LargeMonsterUI.Dynamic.Settings.AddMissionBeaconOffsetToWorldOffset = false;
		config.LargeMonsterUI.Dynamic.Settings.AddModelRadiusToWorldOffsetY = true;
		config.LargeMonsterUI.Dynamic.Settings.OpacityFalloff = true;
		config.LargeMonsterUI.Dynamic.Settings.MaxDistance = 200f;

		config.LargeMonsterUI.Dynamic.WorldOffset.X = 0f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Y = 0f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Z = 0f;

		config.LargeMonsterUI.Dynamic.Offset.X = -148.5f;
		config.LargeMonsterUI.Dynamic.Offset.Y = 0f;

		config.LargeMonsterUI.Dynamic.NameLabel.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Format = "{0}";
		config.LargeMonsterUI.Dynamic.NameLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.NameLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.Visible = true;

		config.LargeMonsterUI.Dynamic.Health.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Health.Offset.Y = 15f;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.X = 245f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.Bar.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Width = 297f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Height = 12f;

		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x004D1BCC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x004D1BCC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0x34FF4ECC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0x34FF4ECC;

		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;


		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Dynamic.Stamina.Visible = true;

		config.LargeMonsterUI.Dynamic.Stamina.Offset.X = 7;
		config.LargeMonsterUI.Dynamic.Stamina.Offset.Y = 25f;

		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Format = "{0:F0}/{1:F0}";
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Format = "{0:P0}";
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Offset.X = 87f;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Stamina.Bar.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Size.Width = 128f;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Size.Height = 8f;

		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Dynamic.Stamina.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Stamina.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Format = "{0}";
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Offset.X = 87f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Offset.Y = 5f;

		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Size.Width = 128f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Size.Height = 8f;

		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Stamina.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFFD00080;

		config.LargeMonsterUI.Dynamic.Rage.Visible = true;

		config.LargeMonsterUI.Dynamic.Rage.Offset.X = 137f;
		config.LargeMonsterUI.Dynamic.Rage.Offset.Y = 25f;

		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Format = "{0:F0}/{1:F0}";
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Rage.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Offset.X = 87f;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Rage.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Rage.Bar.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Size.Width = 128f;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Size.Height = 8f;

		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Dynamic.Rage.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Rage.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Format = "{0}";
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Offset.X = 87f;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Rage.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Size.Width = 128f;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Size.Height = 8f;

		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Rage.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFF000080;

		config.LargeMonsterUI.Static.Settings.RenderDeadOrCaptured = false;
		config.LargeMonsterUI.Static.Settings.RenderHighlightedMonster = true;
		config.LargeMonsterUI.Static.Settings.RenderNotHighlightedMonsters = true;

		config.LargeMonsterUI.Static.Position.X = 89f;
		config.LargeMonsterUI.Static.Position.Y = -40f;
		config.LargeMonsterUI.Static.Position.Anchor = Anchors.BottomLeft;

		config.LargeMonsterUI.Static.Spacing.X = 320f;
		config.LargeMonsterUI.Static.Spacing.Y = 0f;

		config.LargeMonsterUI.Static.Sorting.Type = Sortings.Name;
		config.LargeMonsterUI.Static.Sorting.ReversedOrder = false;

		config.LargeMonsterUI.Static.NameLabel.Visible = true;
		config.LargeMonsterUI.Static.NameLabel.Format = "{0}";
		config.LargeMonsterUI.Static.NameLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.NameLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.NameLabel.Offset.Y = 0f;
		config.LargeMonsterUI.Static.NameLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.Visible = true;

		config.LargeMonsterUI.Static.Health.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Offset.Y = 15f;

		config.LargeMonsterUI.Static.Health.ValueLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Static.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Static.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.X = 245f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.Bar.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Health.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Width = 297f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Height = 12f;

		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x004016CC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x004016CC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0x34FF4ECC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0x34FF4ECC;

		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Static.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Static.Stamina.Visible = true;

		config.LargeMonsterUI.Static.Stamina.Offset.X = 140f;
		config.LargeMonsterUI.Static.Stamina.Offset.Y = 25f;

		config.LargeMonsterUI.Static.Stamina.ValueLabel.Visible = false;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Stamina.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Stamina.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Offset.X = 54f;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Stamina.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Stamina.Bar.Visible = true;
		config.LargeMonsterUI.Static.Stamina.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Stamina.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Stamina.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Stamina.Bar.Size.Width = 94f;
		config.LargeMonsterUI.Static.Stamina.Bar.Size.Height = 8f;

		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Static.Stamina.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Stamina.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Stamina.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Stamina.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Stamina.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Static.Stamina.TimerLabel.Visible = true;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Format = "{0}";
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Offset.X = 54f;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Offset.Y = 5f;

		config.LargeMonsterUI.Static.Stamina.TimerLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Stamina.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Stamina.TimerBar.Visible = true;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Size.Width = 94f;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Size.Height = 8f;

		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x403100CC;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x403100CC;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFFDA33CC;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFFDA33CC;

		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Static.Stamina.TimerBar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Stamina.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFFD00080;

		config.LargeMonsterUI.Static.Rage.Visible = true;

		config.LargeMonsterUI.Static.Rage.Offset.X = 152f;
		config.LargeMonsterUI.Static.Rage.Offset.Y = 31f;

		config.LargeMonsterUI.Static.Rage.ValueLabel.Visible = false;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Static.Rage.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Rage.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Rage.ValueLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Rage.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Offset.X = 34f;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Rage.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Rage.PercentageLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Rage.Bar.Visible = true;
		config.LargeMonsterUI.Static.Rage.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Rage.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Rage.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Rage.Bar.Size.Width = 74f;
		config.LargeMonsterUI.Static.Rage.Bar.Size.Height = 8f;

		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.Bar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Static.Rage.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Rage.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Rage.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Rage.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Rage.Bar.Outline.Color.ColorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Static.Rage.TimerLabel.Visible = false;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Format = "{0}";
		config.LargeMonsterUI.Static.Rage.TimerLabel.Settings.RightAlignmentShift = 4;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Offset.X = 34f;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Offset.Y = 5f;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Color.ColorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Rage.TimerLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Rage.TimerLabel.Shadow.Color.ColorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Rage.TimerBar.Visible = true;
		config.LargeMonsterUI.Static.Rage.TimerBar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Rage.TimerBar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Rage.TimerBar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Rage.TimerBar.Size.Width = 74f;
		config.LargeMonsterUI.Static.Rage.TimerBar.Size.Height = 8f;

		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.Start.ColorInfo1.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.Start.ColorInfo2.Rgba = 0x40000BCC;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.End.ColorInfo1.Rgba = 0xFF4242CC;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Foreground.End.ColorInfo2.Rgba = 0xFF4242CC;

		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.Start.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.Start.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.Start.ColorInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.End.SplitIntoTwoColors = false;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.End.ColorInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Rage.TimerBar.Colors.Background.End.ColorInfo2.Rgba = 0x0000004A;

		config.LargeMonsterUI.Static.Rage.TimerBar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Rage.TimerBar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Rage.TimerBar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Rage.TimerBar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Rage.TimerBar.Outline.Color.ColorInfo.Rgba = 0xFF000080;

		configDatabase.Save();
	}
}
