using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace YURI_Overlay;

internal static class Constants
{
	public const string ModAuthor = "GreenComfyTea";
	public const string ModName = "YURI Overlay";
	public const string ModNameNoWhitespaces = "YURI_Overlay";

	public const string Version = "1.5.1";

	public const string DataPath = @"reframework\data\";
	public const string PluginDataPath = $@"{DataPath}{ModNameNoWhitespaces}\";
	public const string FontsPath = @"reframework\fonts\";
	public const string LocalizationsPath = $@"{PluginDataPath}localizations\";

	public const string ConfigsPath = $@"{PluginDataPath}configs\";

	public const string CurrentConfig = "current_config";
	public const string CurrentConfigWithExtension = $"{CurrentConfig}.json";
	public const string CurrentConfigFilePathName = $"{PluginDataPath}{CurrentConfigWithExtension}";

	public const string DefaultConfig = "default";
	public const string DefaultConfigWithExtension = $"{DefaultConfig}.json";
	public const string DefaultConfigFilePathName = $"{PluginDataPath}{DefaultConfigWithExtension}";

	public const string DefaultLocalization = "en-US";

	public const string NexusModsLink = "https://www.nexusmods.com/monsterhunterwilds/mods/62";
	public const string GithubRepoLink = "https://github.com/GreenComfyTea/MHWs-YURI-Overlay";
	public const string NightlyLink = "https://github.com/GreenComfyTea/MHWs-YURI-Overlay-Nightly/releases";
	public const string TwitchLink = "https://twitch.tv/GreenComfyTea";
	public const string TwitterLink = "https://twitter.com/GreenComfyTea";
	public const string ArtStationLink = "https://GreenComfyTea.artstation.com";
	public const string StreamElementsTipLink = "https://streamelements.com/GreenComfyTea/tip";
	public const string PaypalLink = "https://paypal.me/GreenComfyTea";
	public const string KofiLink = "https://ko-fi.com/GreenComfyTea";

	public const string EmptyJson = "{}";

	public const string EmojiFont = "NotoEmoji-Bold.ttf";

	public const int ReenableWatcherDelayMilliseconds = 100;
	public const long DuplicateEventThresholdTicks = 10000;

	public const float Epsilon = 0.000001f;

	public const float ComboboxWidthMultiplier = 0.4f;
	public const float ColorPickerWidthMultiplier = 0.5f;

	public const float ColorPickerWidthToHeightRatio = 801f / 778f;

	public const uint MaxConfigNameLength = 64;

	public static readonly Vector2 DefaultWindowPosition = new(480, 60);
	public static readonly Vector2 DefaultWindowSize = new(600, 500);

	public static readonly Vector4 ModAuthorColor = new(0.702f, 0.851f, 0.424f, 1f);
	public static readonly Vector4 ImGuiUserNameColor = new(0.5f, 0.710f, 1f, 1f);

	public static readonly JsonSerializerOptions JsonSerializerOptionsInstance = new()
	{
		WriteIndented = true,
		AllowTrailingCommas = true,
		IncludeFields = true,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};
}