using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal sealed class LocalizationInfo
{
	public string Name = "English";
	public string Translators = "GreenComfyTea";
}

internal sealed class FontsInfo
{
	public FontInfo MenuFont = new();
	public FontInfo OverlayFont = new();
}

internal sealed class FontInfo
{
	public string Name { get; set; } = "NotoSans-Bold.ttf";
	public string[] GlyphRanges { get; set; } = ["0x0020", "0xFFFF"];
}

internal sealed class ImGuiLocalization
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
	public string Translators = "Translators";

	// Generic Stuff

	public string Enabled = "Enabled";
	public string Visible = "Visible";

	public string ResetIcon = "\uF0E2";
	public string ResetToDefault = "Reset to Default";

	// Position and Offset

	public string Position = "Position";
	public string WorldOffset = "World Offset";
	public string Offset = "Offset";

	public string X = "X";
	public string Y = "Y";
	public string Z = "Z";

	// Color

	public string Color = "Color";
	public string Colors = "Colors";
	public string Background = "Background";
	public string Foreground = "Foreground";
	public string SplitIntoTwoColors = "Split into Two Colors";
	public string Start = "Start";
	public string End = "End";
	public string _1 = "1";
	public string _2 = "2";

	// Anchor

	public string Anchor = "Anchor";

	public string TopLeft = "Top-Left";
	public string TopCenter = "Top-Center";
	public string TopRight = "Top-Right";

	public string Center = "Center";
	public string CenterLeft = "Center-Left";
	public string CenterRight = "Center-Right";

	public string BottomLeft = "Bottom-Left";
	public string BottomCenter = "Bottom-Center";
	public string BottomRight = "Bottom-Right";

	// Sorting

	public string Sorting = "Sorting";

	public string Type = "Type";
	public string Id = "Id";
	public string MaxHealth = "Max Health";
	public string HealthPercentage = "Health Percentage";
	public string Distance = "Distance";

	public string ReversedOrder = "Reversed Order";

	public string TargetedMonsterPriority = "Targeted Monster Priority";
	public string PinnedMonsterPriority = "Pinned Monster Priority";

	public string Lower3 = "Lower (-3)";
	public string Lower2 = "Lower (-2)";
	public string Lower1 = "Lower (-1)";
	public string Normal = "Normal";
	public string Higher1 = "Higher (+1)";
	public string Higher2 = "Higher (+2)";
	public string Higher3 = "Higher (+3)";

	// Global Settings

	public string GlobalSettings = "Global Settings";

	public string GlobalScale = "Global Scale";
	public string PositionScaleModifier = "Position Scale Modifier";
	public string SizeScaleModifier = "Size Scale Modifier";

	public string Performance = "Performance";
	public string UpdateDelaysSeconds = "Update Delays (seconds)";
	public string CalculationCaching = "Calculation Caching";

	public string ScreenManager = "Screen Manager";
	public string PlayerManager = "Player Manager";
	public string SmallMonsters = "Small Monsters";
	public string LargeMonsters = "Large Monsters";
	public string EndemicLife = "Endemic Life";
	public string UIs = "UIs";

	public string Name = "Name";
	public string Health = "Health";
	public string Stamina = "Stamina";
	public string Rage = "Rage";

	public string MissionBeaconOffset = "Mission Beacon Offset";
	public string ModelRadius = "Model Radius";

	public string LargeMonstersDynamic = "Large Monsters: Dynamic";
	public string LargeMonstersStatic = "Large Monsters: Static";
	public string LargeMonstersTargeted = "Large Monsters: Targeted";
	public string LargeMonstersMapPin = "Large Monsters: Map Pin";

	public string Update = "Update";

	// Font

	public string Font = "Font";

	public string AnyChangesToFontRequireGameRestart = "Any Changes to Font Require Game Restart!";
	public string FontSize = "Font Size";
	public string HorizontalOversample = "Horizontal Oversample";
	public string VerticalOversample = "Vertical Oversample";

	public string MenuFont = "Menu Font";

	public string FontScale = "Font Scale";
	public string DefinedByLocalization = "Defined by Localization";
	public string GlobalFonts = "Global Fonts";

	// Label

	public string Label = "Label";

	public string Format = "Format";
	public string Alignment = "Alignment";
	public string Shadow = "Shadow";

	public string NameLabel = "Name Label";
	public string ValueLabel = "Value Label";
	public string PercentageLabel = "Percentage Label";
	public string TimerLabel = "Timer Label";

	// Bar

	public string Bar = "Bar";

	public string Settings = "Settings";
	public string FillDirection = "Fill Direction";
	public string LeftToRight = "Left to Right";
	public string RightToLeft = "Right to Left";
	public string TopToBottom = "Top to Bottom";
	public string BottomToTop = "Bottom to Top";

	public string Size = "Size";
	public string Width = "Width";
	public string Height = "Height";

	public string Outline = "Outline";
	public string Thickness = "Thickness";

	public string Style = "Style";
	public string Inside = "Inside";
	public string Outside = "Outside";

	public string Inverted = "Inverted";

	public string TimerBar = "Timer Bar";

	// Monsters and Entities
	public string LargeMonstersUI = "Large Monsters UI";
	public string SmallMonsterUI = "Small Monster UI";
	public string EndemicLifeUI = "Endemic Life UI";

	public string Static = "Static";
	public string Dynamic = "Dynamic";
	public string Targeted = "Targeted";
	public string MapPin = "Map Pin";

	public string Spacing = "Spacing";

	public string RenderDeadMonster = "Render Dead Monster";
	public string RenderDeadMonsters = "Render Dead Monsters";
	public string RenderTargetedMonster = "Render Targeted Monster";
	public string RenderNonTargetedMonsters = "Render Non-Targeted Monsters";
	public string RenderPinnedMonster = "Render Pinned Monster";
	public string RenderNonPinnedMonsters = "Render Non-Pinned Monsters";

	public string AddMissionBeaconOffsetToWorldOffset = "Add Mission Beacon Offset to World Offset";
	public string AddModelRadiusToWorldOffsetY = "Add Model Radius to World Offset Y";
	public string OpacityFalloff = "Opacity Falloff";
	public string MaxDistance = "Max Distance";
}

internal sealed class Localization
{
	[JsonIgnore]
	public string IsoCode = Constants.DefaultLocalization;

	public LocalizationInfo LocalizationInfo = new();

	public FontsInfo Fonts { get; set; } = new();

	[JsonPropertyName("Customization")]
	public ImGuiLocalization ImGui = new();
}