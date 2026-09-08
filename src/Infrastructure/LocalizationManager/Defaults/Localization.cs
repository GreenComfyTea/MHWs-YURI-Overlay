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
	public string _1 = "1";
	public string _2 = "2";

	public string ActiveConfig = "Active Config";

	public string AddMissionBeaconOffsetToWorldOffset = "Add Mission Beacon Offset to World Offset";
	public string AddModelRadiusToWorldOffsetY = "Add Model Radius to World Offset Y";
	public string Alignment = "Alignment";

	// Anchor

	public string Anchor = "Anchor";

	public string AnyChangesToFontRequireGameRestart = "Any Changes to Font Require Game Restart!";
	public string ArtStation = "ArtStation";
	public string Background = "Background";

	// Bar

	public string Bar = "Bar";
	public string BottomCenter = "Bottom-Center";

	public string BottomLeft = "Bottom-Left";
	public string BottomRight = "Bottom-Right";
	public string BottomToTop = "Bottom to Top";
	public string BuyMeATea = "Buy Me a Tea";
	public string CalculationCaching = "Calculation Caching";

	public string Center = "Center";
	public string CenterLeft = "Center-Left";
	public string CenterRight = "Center-Right";

	// Color

	public string Color = "Color";
	public string Colors = "Colors";

	// Config

	public string Config = "Config";

	public string Damage = "Damage";
	public string DamageBar = "Damage Bar";

	// Damage Meter

	public string DamageMeterUi = "Damage Meter UI";
	public string DamagePercentage = "Damage Percentage";
	public string DefinedByLocalization = "Defined by Localization";
	public string Delete = "Delete";
	public string Distance = "Distance";
	public string Donate = "Donate";
	public string DonationMessage1 = "If you like the mod, please consider making a small donation!";
	public string DonationMessage2 = "It would help me maintain existing mods and create new ones in the future!";
	public string Dps = "DPS";

	public string DpsBar = "DPS Bar";
	public string DpsPercentage = "DPS Percentage";
	public string Duplicate = "Duplicate";
	public string Dynamic = "Dynamic";

	// Generic Stuff

	public string Enabled = "Enabled";
	public string End = "End";
	public string EndemicLife = "Endemic Life";
	public string EndemicLifeUi = "Endemic Life UI";
	public string FillDirection = "Fill Direction";

	// Font

	public string Font = "Font";

	public string FontScale = "Font Scale";
	public string FontSize = "Font Size";
	public string Foreground = "Foreground";

	public string Format = "Format";
	public string GitHubRepo = "GitHub Repo";
	public string GlobalFonts = "Global Fonts";

	public string GlobalScale = "Global Scale";

	// Global Settings

	public string GlobalSettings = "Global Settings";
	public string Health = "Health";
	public string HealthPercentage = "Health Percentage";
	public string Height = "Height";
	public string Higher1 = "Higher (+1)";
	public string Higher2 = "Higher (+2)";
	public string Higher3 = "Higher (+3)";
	public string HorizontalOversample = "Horizontal Oversample";

	public string HunterMasterRanksLabel = "Hunter Rank/Master Rank Label";

	public string HunterRank = "Hunter Rank";
	public string Id = "Id";
	public string Inside = "Inside";

	public string Inverted = "Inverted";

	// Label

	public string Label = "Label";

	// Localization

	public string Language = "Language";
	public string LargeMonsters = "Large Monsters";

	public string LargeMonstersDynamic = "Large Monsters: Dynamic";
	public string LargeMonstersMapPin = "Large Monsters: Map Pin";
	public string LargeMonstersStatic = "Large Monsters: Static";
	public string LargeMonstersTargeted = "Large Monsters: Targeted";

	// Monsters and Entities
	public string LargeMonstersUi = "Large Monsters UI";
	public string LeftToRight = "Left to Right";

	public string LocalPlayer = "Local Player";
	public string LocalPlayerPriority = "Local Player Priority";
	public string Lower1 = "Lower (-1)";
	public string Lower2 = "Lower (-2)";

	public string Lower3 = "Lower (-3)";

	public string MadeBy = "Made by:";
	public string MapPin = "Map Pin";
	public string MasterRank = "Master Rank";
	public string MaxDistance = "Max Distance";
	public string MaxHealth = "Max Health";
	public string MaxWidth = "Max Width";
	public string MenuFontScale = "Menu Font Scale";
	public string MenuFontScaleModifier = "Menu Font Scale Modifier";

	public string MissionBeaconOffset = "Mission Beacon Offset";

	public string ModelRadius = "Model Radius";

	// Mod Info
	public string ModInfo = "Mod Info";

	public string Name = "Name";

	public string NameLabel = "Name Label";
	public string New = "New";
	public string NewConfigName = "New Config Name";
	public string NexusMods = "Nexus Mods";
	public string Normal = "Normal";
	public string Offset = "Offset";
	public string OpacityFalloff = "Opacity Falloff";
	public string OtherPlayers = "Other Players";

	public string Outline = "Outline";
	public string Outside = "Outside";
	public string OverlayFontScale = "Overlay Font Scale";
	public string OverlayFontScaleModifier = "Overlay Font Scale Modifier";
	public string PayPal = "PayPal";
	public string PercentageLabel = "Percentage Label";

	public string Performance = "Performance";
	public string PinnedMonsterPriority = "Pinned Monster Priority";
	public string PlayerManager = "Player Manager";

	// Position and Offset

	public string Position = "Position";
	public string PositionScaleModifier = "Position Scale Modifier";
	public string Rage = "Rage";
	public string Rename = "Rename";

	public string RenderDeadMonster = "Render Dead Monster";
	public string RenderDeadMonsters = "Render Dead Monsters";

	public string RenderLocalPlayer = "Render Local Player";
	public string RenderNonPinnedMonsters = "Render Non-Pinned Monsters";
	public string RenderNonTargetedMonsters = "Render Non-Targeted Monsters";
	public string RenderOtherPlayers = "Render Other Players";
	public string RenderPinnedMonster = "Render Pinned Monster";
	public string RenderSupportHunters = "Render Support Hunters";
	public string RenderTargetedMonster = "Render Targeted Monster";
	public string Reset = "Reset";

	public string ResetIcon = "\u21BA";
	public string ResetToDefault = "Reset to Default";

	public string ReversedOrder = "Reversed Order";
	public string RightToLeft = "Right to Left";

	public string ScaleWithReFrameworkFontSize = "Scale with REFramework Font Size";

	public string ScreenManager = "Screen Manager";

	public string Settings = "Settings";
	public string Shadow = "Shadow";

	public string Size = "Size";
	public string SizeScaleModifier = "Size Scale Modifier";
	public string SmallMonsters = "Small Monsters";
	public string SmallMonsterUi = "Small Monster UI";

	// Sorting

	public string Sorting = "Sorting";

	public string Spacing = "Spacing";
	public string SplitIntoTwoColors = "Split into Two Colors";
	public string Stamina = "Stamina";
	public string Start = "Start";

	public string Static = "Static";

	public string Style = "Style";
	public string SupportHunters = "Support Hunters";
	public string Targeted = "Targeted";

	public string TargetedMonsterPriority = "Targeted Monster Priority";
	public string Thickness = "Thickness";

	public string TimerBar = "Timer Bar";
	public string TimerLabel = "Timer Label";
	public string TopCenter = "Top-Center";

	public string TopLeft = "Top-Left";
	public string TopRight = "Top-Right";
	public string TopToBottom = "Top to Bottom";
	public string Translators = "Translators";
	public string Twitch = "Twitch";
	public string Twitter = "Twitter";

	public string Type = "Type";
	public string UIs = "UIs";

	public string Update = "Update";
	public string UpdateDelaysSeconds = "Update Delays (seconds)";
	public string ValueLabel = "Value Label";
	public string VerticalOversample = "Vertical Oversample";
	public string Visible = "Visible";
	public string Width = "Width";
	public string WorldOffset = "World Offset";

	public string X = "X";
	public string Y = "Y";
	public string Z = "Z";
}

internal sealed class Localization
{
	[JsonPropertyName("Customization")]
	public ImGuiLocalization ImGui = new();

	[JsonIgnore]
	public string IsoCode = Constants.DEFAULT_LOCALIZATION;

	public LocalizationInfo LocalizationInfo = new();

	public FontsInfo Fonts { get; set; } = new();
}