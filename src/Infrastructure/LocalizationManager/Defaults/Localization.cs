using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal class LocalizationInfo
{
	public string Name = "English";
	public string Translators = "GreenComfyTea";
}

internal class FontsInfo
{
	public FontInfo MenuFont = new();
	public FontInfo OverlayFont = new();
}

internal class FontInfo
{
	public string Name { get; set; } = "NotoSans-Bold.ttf";
	public string[] GlyphRanges { get; set; } = ["0x0020", "0xFFFF"];
}

internal class ImGuiLocalization
{
	// Mod Info
	public string ModInfo = "Mod Info";

	public string MadeBy = "Made by:";
	public string NexusMods = "Nexus Mods";
	public string GitHubRepo = "GitHub Repo";
	public string Twitch = "Twitch";
	public string Twitter = "Twitter";
	public string ArtStation = "ArtStation";
	public string DonationMessage1 = "If you like the mod, please consider making a small donation!";
	public string DonationMessage2 = "It would help me maintain existing mods and create new ones in the future!";
	public string Donate = "Donate";
	public string PayPal = "PayPal";
	public string BuyMeATea = "Buy Me a Tea";

	// Config

	public string Config = "Config";

	public string ActiveConfig = "Active Config";
	public string NewConfigName = "New Config Name";
	public string New = "New";
	public string Duplicate = "Duplicate";
	public string Delete = "Delete";
	public string Reset = "Reset";
	public string Rename = "Rename";

	// Localization

	public string Language = "Language";

	// Bar

	public string Bar = "Bar";
	public string Visible = "Visible";
	public string Settings = "Settings";
	public string FillDirection = "Fill Direction";
	public string LeftToRight = "Left to Right";
	public string RightToLeft = "Right to Left";
	public string TopToBottom = "Top to Bottom";
	public string BottomToTop = "Bottom to Top";
	public string Offset = "Offset";
	public string X = "X";
	public string Y = "Y";
	public string Z = "Z";
	public string Size = "Size";
	public string Width = "Width";
	public string Height = "Height";
	public string Outline = "Outline";
	public string Thickness = "Thickness";
	public string Style = "Style";
	public string Inside = "inside";
	public string Center = "Center";
	public string Outside = "Outside";
	public string Colors = "Colors";
	public string Background = "Background";
	public string Foreground = "Foreground";
	public string Color = "Color";
	public string SplitIntoTwoColors = "Split into Two Colors";
	public string Start = "Start";
	public string End = "End";
	public string _1 = "1";
	public string _2 = "2";

	// Label

	public string Label = "Label";
	public string Format = "Format";
	public string RightAlignmentShift = "Right Alignment Shift";
	public string Shadow = "Shadow";
	public string ValueLabel = "Value Label";
	public string PercentageLabel = "Percentage Label";

	// Large Monsters
	public string LargeMonstersUi = "Large Monsters UI";
	public string Static = "Static";
	public string Dynamic = "Dynamic";
	public string Highlighted = "Highlighted";
	public string Spacing = "Spacing";
	public string Position = "Position";

	public string Enabled = "Enabled";
	public string RenderDeadOrCaptured = "Render Dead or Captured";
	public string RenderHighlightedMonster = "Render Highlighted Monster";
	public string RenderNotHighlightedMonsters = "Render Non-highlighted Monsters";
	public string AddMissionBeaconOffsetToWorldOffset = "Add Mission Beacon Offset to World Offset";
	public string AddModelRadiusToWorldOffsetY = "Add Model Radius to World Offset Y";
	public string OpacityFalloff = "Opacity Falloff";
	public string MaxDistance = "Max Distance";
	public string WorldOffset = "World Offset";

	public string Name = "Name";
	public string Health = "Health";

	public string HighlightedMonsterLocation = "Highlighted Monster Location";
	public string Normal = "Normal";
	public string First = "First";
	public string Last = "Last";

	public string Sorting = "Sorting";
	public string Type = "Type";
	public string Id = "Id";
	public string MaxHealth = "Max Health";
	public string HealthPercentage = "Health Percentage";
	public string Distance = "Distance";

	public string ReversedOrder = "Reversed Order";

	public string Anchor = "Anchor";
	public string TopLeft = "Top-Left";
	public string TopCenter = "Top-Center";
	public string TopRight = "Top-Right";
	public string CenterLeft = "Center-Left";
	public string CenterRight = "Center-Right";
	public string BottomLeft = "Bottom-Left";
	public string BottomCenter = "Bottom-Center";
	public string BottomRight = "Bottom-Right";

	public string NameLabel = "Name Label";

	public string GlobalSettings = "Global Settings";

	public string GlobalScale = "Global Scale";
	public string PositionScaleModifier = "Position Scale Modifier";
	public string SizeScaleModifier = "Size Scale Modifier";

	public string Performance = "Performance";
	public string UpdateDelaySeconds = "Update Delay (seconds)";
	public string CalculationCaching = "Calculation Caching";

	public string Font = "Font";
	public string AnyChangesToFontRequireGameRestart = "Any Changes to Font Require Game Restart!";
	public string FontSize = "Font Size";
	public string HorizontalOversample = "Horizontal Oversample";
	public string VerticalOversample = "Vertical Oversample";

	public string Stamina = "Stamina";
	public string TimerLabel = "Timer Label";
	public string Rage = "Rage";
	public string TimerBar = "Timer Bar";

	public string MenuFont = "Menu Font";
	public string FontScale = "Font Scale";
	public string DefinedByLocalization = "Defined by Localization";
	public string GlobalFonts = "Global Fonts";
}

internal class Localization
{
	[JsonIgnore]
	public string IsoCode = Constants.DefaultLocalization;

	public LocalizationInfo LocalizationInfo = new();

	public FontsInfo Fonts { get; set; } = new();

	[JsonPropertyName("Customization")]
	public ImGuiLocalization ImGui = new();
}
