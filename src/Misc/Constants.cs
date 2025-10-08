using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace YURI_Overlay;

internal static class Constants
{
	public const string ModAuthor = "GreenComfyTea";
	public const string ModName = "YURI Overlay";
	public const string ModNameNoWhitespaces = "YURI_Overlay";

	private const string ReleaseVersion = "1.5.5";

#if DEBUG
	public static readonly string Version = $"{ReleaseVersion}-debug-{DateTime.Now:yyyy-MM-dd---HH:mm:ss}";
#else
	public const string Version = ReleaseVersion;
#endif

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

	public static readonly Vector4 InGameColor_000_I_NONE = new(1f, 1f, 1f, 1f);                             // 0xFFFFFFFF (ABGR) -> 0xFFFFFFFF (RGBA), 0xFFFFFFFF (ARGB)
	public static readonly Vector4 InGameColor_001_I_WHITE = new(0.914f, 0.917f, 0.921f, 1f);                // 0xFFEBEAE9 (ABGR) -> 0xE9EAEBFF (RGBA), 0xFFE9EAEB (ARGB)
	public static readonly Vector4 InGameColor_002_I_GRAY = new(0.506f, 0.506f, 0.510f, 1f);                 // 0xFF828181 (ABGR) -> 0x818182FF (RGBA), 0xFF818182 (ARGB)
	public static readonly Vector4 InGameColor_003_I_ROSE = new(0.749f, 0.188f, 0.329f, 1f);                 // 0xFF5430BF (ABGR) -> 0xBF3054FF (RGBA), 0xFFBF3054 (ARGB)
	public static readonly Vector4 InGameColor_004_I_PINK = new(0.902f, 0.541f, 0.580f, 1f);                 // 0xFF948AE6 (ABGR) -> 0xE68A94FF (RGBA), 0xFFE68A94 (ARGB)
	public static readonly Vector4 InGameColor_005_I_RED = new(0.882f, 0.314f, 0.341f, 1f);                  // 0xFF5750E1 (ABGR) -> 0xE15057FF (RGBA), 0xFFE15057 (ARGB)
	public static readonly Vector4 InGameColor_006_I_VERMILION = new(0.882f, 0.427f, 0.267f, 1f);            // 0xFF446DE1 (ABGR) -> 0xE16D44FF (RGBA), 0xFFE16D44 (ARGB)
	public static readonly Vector4 InGameColor_007_I_ORANGE = new(0.875f, 0.612f, 0.396f, 1f);               // 0xFF659CDF (ABGR) -> 0xDF9C65FF (RGBA), 0xFFDF9C65 (ARGB)
	public static readonly Vector4 InGameColor_008_I_BROWN = new(0.702f, 0.561f, 0.282f, 1f);                // 0xFF488FB3 (ABGR) -> 0xB38F48FF (RGBA), 0xFFB38F48 (ARGB)
	public static readonly Vector4 InGameColor_009_I_IVORY = new(0.851f, 0.792f, 0.678f, 1f);                // 0xFFADCAD9 (ABGR) -> 0xD9CAADFF (RGBA), 0xFFD9CAAD (ARGB)
	public static readonly Vector4 InGameColor_010_I_YELLOW = new(0.851f, 0.765f, 0.341f, 1f);               // 0xFF57C3D9 (ABGR) -> 0xD9C357FF (RGBA), 0xFFD9C357 (ARGB)
	public static readonly Vector4 InGameColor_011_I_LEMON = new(0.902f, 0.902f, 0f, 1f);                    // 0xFF00E6E6 (ABGR) -> 0xE6E600FF (RGBA), 0xFFE6E600 (ARGB)
	public static readonly Vector4 InGameColor_012_I_SGREEN = new(0.514f, 0.698f, 0.224f, 1f);               // 0xFF39B283 (ABGR) -> 0x83B239FF (RGBA), 0xFF83B239 (ARGB)
	public static readonly Vector4 InGameColor_013_I_MOS = new(0.239f, 0.498f, 0.227f, 1f);                  // 0xFF3A7F3D (ABGR) -> 0x3D7F3AFF (RGBA), 0xFF3D7F3A (ARGB)
	public static readonly Vector4 InGameColor_014_I_GREEN = new(0.278f, 0.698f, 0.404f, 1f);                // 0xFF67B247 (ABGR) -> 0x47B267FF (RGBA), 0xFF47B267 (ARGB)
	public static readonly Vector4 InGameColor_015_I_EMERALD = new(0.596f, 0.851f, 0.788f, 1f);              // 0xFFC9D998 (ABGR) -> 0x98D9C9FF (RGBA), 0xFF98D9C9 (ARGB)
	public static readonly Vector4 InGameColor_016_I_SKY = new(0.306f, 0.765f, 0.898f, 1f);                  // 0xFFE5C34E (ABGR) -> 0x4EC3E5FF (RGBA), 0xFF4EC3E5 (ARGB)
	public static readonly Vector4 InGameColor_017_I_BLUE = new(0.400f, 0.529f, 1f, 1f);                     // 0xFFFF8766 (ABGR) -> 0x6687FFFF (RGBA), 0xFF6687FF (ARGB)
	public static readonly Vector4 InGameColor_018_I_ULTRAMARINE = new(0.298f, 0.298f, 0.851f, 1f);          // 0xFFD94C4C (ABGR) -> 0x4C4CD9FF (RGBA), 0xFF4C4CD9 (ARGB)
	public static readonly Vector4 InGameColor_019_I_BPURPLE = new(0.408f, 0.369f, 0.804f, 1f);              // 0xFFCD5E68 (ABGR) -> 0x685ECDFF (RGBA), 0xFF685ECD (ARGB)
	public static readonly Vector4 InGameColor_020_I_PURPLE = new(0.592f, 0.533f, 0.820f, 1f);               // 0xFFD18897 (ABGR) -> 0x9788D1FF (RGBA), 0xFF9788D1 (ARGB)
	public static readonly Vector4 InGameColor_021_I_DPURPLE = new(0.337f, 0.216f, 0.620f, 1f);              // 0xFF9E3756 (ABGR) -> 0x56379EFF (RGBA), 0xFF56379E (ARGB)
	public static readonly Vector4 InGameColor_022_RARE_01 = new(0.588f, 0.588f, 0.588f, 1f);                // 0xFF969696 (ABGR) -> 0x969696FF (RGBA), 0xFF969696 (ARGB)
	public static readonly Vector4 InGameColor_023_RARE_02 = new(0.871f, 0.871f, 0.871f, 1f);                // 0xFFDEDEDE (ABGR) -> 0xDEDEDEFF (RGBA), 0xFFDEDEDE (ARGB)
	public static readonly Vector4 InGameColor_024_RARE_03 = new(0.643f, 0.769f, 0.231f, 1f);                // 0xFF3BC4A4 (ABGR) -> 0xA4C43BFF (RGBA), 0xFFA4C43B (ARGB)
	public static readonly Vector4 InGameColor_025_RARE_04 = new(0.278f, 0.639f, 0.247f, 1f);                // 0xFF3FA347 (ABGR) -> 0x47A33FFF (RGBA), 0xFF47A33F (ARGB)
	public static readonly Vector4 InGameColor_026_RARE_05 = new(0.361f, 0.682f, 0.733f, 1f);                // 0xFFBBAE5C (ABGR) -> 0x5CAEBBFF (RGBA), 0xFF5CAEBB (ARGB)
	public static readonly Vector4 InGameColor_027_RARE_06 = new(0.341f, 0.373f, 0.851f, 1f);                // 0xFFD95F57 (ABGR) -> 0x575FD9FF (RGBA), 0xFF575FD9 (ARGB)
	public static readonly Vector4 InGameColor_028_RARE_07 = new(0.573f, 0.447f, 0.890f, 1f);                // 0xFFE37292 (ABGR) -> 0x9272E3FF (RGBA), 0xFF9272E3 (ARGB)
	public static readonly Vector4 InGameColor_029_RARE_08 = new(0.780f, 0.427f, 0.275f, 1f);                // 0xFF466DC7 (ABGR) -> 0xC76D46FF (RGBA), 0xFFC76D46 (ARGB)
	public static readonly Vector4 InGameColor_030_RARE_09 = new(0.702f, 0.263f, 0.416f, 1f);                // 0xFF6A43B3 (ABGR) -> 0xB3436AFF (RGBA), 0xFFB3436A (ARGB)
	public static readonly Vector4 InGameColor_031_RARE_10 = new(0.180f, 0.788f, 0.902f, 1f);                // 0xFFE6C92E (ABGR) -> 0x2EC9E6FF (RGBA), 0xFF2EC9E6 (ARGB)
	public static readonly Vector4 InGameColor_032_RARE_11 = new(0.949f, 0.761f, 0.114f, 1f);                // 0xFF1DC2F2 (ABGR) -> 0xF2C21DFF (RGBA), 0xFFF2C21D (ARGB)
	public static readonly Vector4 InGameColor_033_RARE_12 = new(0.706f, 0.961f, 1f, 1f);                    // 0xFFFFF5B4 (ABGR) -> 0xB4F5FFFF (RGBA), 0xFFB4F5FF (ARGB)
	public static readonly Vector4 InGameColor_034_RANK_Prog00 = new(0.941f, 0.941f, 0.941f, 1f);            // 0xFFF0F0F0 (ABGR) -> 0xF0F0F0FF (RGBA), 0xFFF0F0F0 (ARGB)
	public static readonly Vector4 InGameColor_035_RANK_Prog01 = new(0.608f, 0.800f, 0.337f, 1f);            // 0xFF56CC9B (ABGR) -> 0x9BCC56FF (RGBA), 0xFF9BCC56 (ARGB)
	public static readonly Vector4 InGameColor_036_RANK_Prog02 = new(0.424f, 0.639f, 0.851f, 1f);            // 0xFFD9A36C (ABGR) -> 0x6CA3D9FF (RGBA), 0xFF6CA3D9 (ARGB)
	public static readonly Vector4 InGameColor_037_RANK_Prog03 = new(0.902f, 0.522f, 0.271f, 1f);            // 0xFF4585E6 (ABGR) -> 0xE68545FF (RGBA), 0xFFE68545 (ARGB)
	public static readonly Vector4 InGameColor_038_TXT_White01 = new(0.961f, 0.961f, 0.961f, 1f);            // 0xFFF5F5F5 (ABGR) -> 0xF5F5F5FF (RGBA), 0xFFF5F5F5 (ARGB)
	public static readonly Vector4 InGameColor_039_TXT_White02 = new(0.910f, 0.910f, 0.910f, 1f);            // 0xFFE8E8E8 (ABGR) -> 0xE8E8E8FF (RGBA), 0xFFE8E8E8 (ARGB)
	public static readonly Vector4 InGameColor_040_TXT_White03 = new(0.612f, 0.612f, 0.612f, 1f);            // 0xFF9C9C9C (ABGR) -> 0x9C9C9CFF (RGBA), 0xFF9C9C9C (ARGB)
	public static readonly Vector4 InGameColor_041_TXT_Gray01 = new(0.851f, 0.851f, 0.851f, 1f);             // 0xFFD9D9D9 (ABGR) -> 0xD9D9D9FF (RGBA), 0xFFD9D9D9 (ARGB)
	public static readonly Vector4 InGameColor_042_TXT_Black01 = new(0.016f, 0.020f, 0.027f, 1f);            // 0xFF070504 (ABGR) -> 0x040507FF (RGBA), 0xFF040507 (ARGB)
	public static readonly Vector4 InGameColor_043_TXT_Safe = new(0.392f, 0.824f, 0.471f, 1f);               // 0xFF78D264 (ABGR) -> 0x64D278FF (RGBA), 0xFF64D278 (ARGB)
	public static readonly Vector4 InGameColor_044_TXT_Danger = new(0.980f, 0.431f, 0.392f, 1f);             // 0xFF646EFA (ABGR) -> 0xFA6E64FF (RGBA), 0xFFFA6E64 (ARGB)
	public static readonly Vector4 InGameColor_045_TXT_Accent = new(0.941f, 0.902f, 0.471f, 1f);             // 0xFF78E6F0 (ABGR) -> 0xF0E678FF (RGBA), 0xFFF0E678 (ARGB)
	public static readonly Vector4 InGameColor_046_TXT_Accent2 = new(0.667f, 0.843f, 0.961f, 1f);            // 0xFFF5D7AA (ABGR) -> 0xAAD7F5FF (RGBA), 0xFFAAD7F5 (ARGB)
	public static readonly Vector4 InGameColor_047_TXT_Accent3 = new(0.969f, 0.663f, 0.267f, 1f);            // 0xFF44A9F7 (ABGR) -> 0xF7A944FF (RGBA), 0xFFF7A944 (ARGB)
	public static readonly Vector4 InGameColor_048_TXT_Sub = new(0.600f, 0.600f, 0.600f, 1f);                // 0xFF999999 (ABGR) -> 0x999999FF (RGBA), 0xFF999999 (ARGB)
	public static readonly Vector4 InGameColor_049_TXT_Max = new(0.941f, 0.698f, 0.424f, 1f);                // 0xFF6CB2F0 (ABGR) -> 0xF0B26CFF (RGBA), 0xFFF0B26C (ARGB)
	public static readonly Vector4 InGameColor_050_TXT_CharaName = new(0.749f, 0.749f, 0.749f, 1f);          // 0xFFBFBFBF (ABGR) -> 0xBFBFBFFF (RGBA), 0xFFBFBFBF (ARGB)
	public static readonly Vector4 InGameColor_051_TXT_Choice_01 = new(0.941f, 0.902f, 0.471f, 1f);          // 0xFF78E6F0 (ABGR) -> 0xF0E678FF (RGBA), 0xFFF0E678 (ARGB)
	public static readonly Vector4 InGameColor_052_TXT_Choice_02 = new(0.525f, 0.655f, 0.749f, 1f);          // 0xFFBFA786 (ABGR) -> 0x86A7BFFF (RGBA), 0xFF86A7BF (ARGB)
	public static readonly Vector4 InGameColor_053_TXT_Title = new(0.980f, 0.937f, 0.733f, 0.902f);          // 0xE6BBEFFA (ABGR) -> 0xFAEFBBE6 (RGBA), 0xE6FAEFBB (ARGB)
	public static readonly Vector4 InGameColor_054_TXT_currency00num = new(0.961f, 0.843f, 0.471f, 1f);      // 0xFF78D7F5 (ABGR) -> 0xF5D778FF (RGBA), 0xFFF5D778 (ARGB)
	public static readonly Vector4 InGameColor_055_TXT_currency00unit = new(0.800f, 0.663f, 0.239f, 1f);     // 0xFF3DA9CC (ABGR) -> 0xCCA93DFF (RGBA), 0xFFCCA93D (ARGB)
	public static readonly Vector4 InGameColor_056_TXT_currency01num = new(0.561f, 0.800f, 0.573f, 1f);      // 0xFF92CC8F (ABGR) -> 0x8FCC92FF (RGBA), 0xFF8FCC92 (ARGB)
	public static readonly Vector4 InGameColor_057_TXT_currency01unit = new(0.302f, 0.600f, 0.318f, 1f);     // 0xFF51994D (ABGR) -> 0x4D9951FF (RGBA), 0xFF4D9951 (ARGB)
	public static readonly Vector4 InGameColor_058_TXT_currency02num = new(0.843f, 0.647f, 0.251f, 1f);      // 0xFF40A5D7 (ABGR) -> 0xD7A540FF (RGBA), 0xFFD7A540 (ARGB)
	public static readonly Vector4 InGameColor_059_TXT_currency02unit = new(0.706f, 0.541f, 0.212f, 1f);     // 0xFF368AB4 (ABGR) -> 0xB48A36FF (RGBA), 0xFFB48A36 (ARGB)
	public static readonly Vector4 InGameColor_060_TXT_currency03num = new(0.808f, 0.541f, 0.902f, 1f);      // 0xFFE68ACE (ABGR) -> 0xCE8AE6FF (RGBA), 0xFFCE8AE6 (ARGB)
	public static readonly Vector4 InGameColor_061_TXT_currency03unit = new(0.694f, 0.400f, 0.800f, 1f);     // 0xFFCC66B1 (ABGR) -> 0xB166CCFF (RGBA), 0xFFB166CC (ARGB)
	public static readonly Vector4 InGameColor_062_GUI_White = new(1f, 1f, 1f, 1f);                          // 0xFFFFFFFF (ABGR) -> 0xFFFFFFFF (RGBA), 0xFFFFFFFF (ARGB)
	public static readonly Vector4 InGameColor_063_GUI_Black = new(0f, 0f, 0f, 1f);                          // 0xFF000000 (ABGR) -> 0x000000FF (RGBA), 0xFF000000 (ARGB)
	public static readonly Vector4 InGameColor_064_GUI_Disable = new(0.612f, 0.612f, 0.612f, 1f);            // 0xFF9C9C9C (ABGR) -> 0x9C9C9CFF (RGBA), 0xFF9C9C9C (ARGB)
	public static readonly Vector4 InGameColor_065_GUI_Safe = new(0.392f, 0.902f, 0.510f, 1f);               // 0xFF82E664 (ABGR) -> 0x64E682FF (RGBA), 0xFF64E682 (ARGB)
	public static readonly Vector4 InGameColor_066_GUI_Danger = new(0.941f, 0.459f, 0.424f, 1f);             // 0xFF6C75F0 (ABGR) -> 0xF0756CFF (RGBA), 0xFFF0756C (ARGB)
	public static readonly Vector4 InGameColor_067_GUI_Acrtive01 = new(0.835f, 1f, 0f, 1f);                  // 0xFF00FFD5 (ABGR) -> 0xD5FF00FF (RGBA), 0xFFD5FF00 (ARGB)
	public static readonly Vector4 InGameColor_068_GUI_Acrtive02 = new(0.941f, 1f, 0f, 1f);                  // 0xFF00FFF0 (ABGR) -> 0xF0FF00FF (RGBA), 0xFFF0FF00 (ARGB)
	public static readonly Vector4 InGameColor_069_GUI_DShadow = new(0.102f, 0.086f, 0.051f, 1f);            // 0xFF0D161A (ABGR) -> 0x1A160DFF (RGBA), 0xFF1A160D (ARGB)
	public static readonly Vector4 InGameColor_070_GUI_Psolo = new(0.945f, 0.584f, 0.369f, 1f);              // 0xFF5E95F1 (ABGR) -> 0xF1955EFF (RGBA), 0xFFF1955E (ARGB)
	public static readonly Vector4 InGameColor_071_GUI_P1 = new(0.922f, 0.459f, 0.471f, 1f);                 // 0xFF7875EB (ABGR) -> 0xEB7578FF (RGBA), 0xFFEB7578 (ARGB)
	public static readonly Vector4 InGameColor_072_GUI_P2 = new(0.380f, 0.675f, 0.949f, 1f);                 // 0xFFF2AC61 (ABGR) -> 0x61ACF2FF (RGBA), 0xFF61ACF2 (ARGB)
	public static readonly Vector4 InGameColor_073_GUI_P3 = new(0.965f, 0.867f, 0.435f, 1f);                 // 0xFF6FDDF6 (ABGR) -> 0xF6DD6FFF (RGBA), 0xFFF6DD6F (ARGB)
	public static readonly Vector4 InGameColor_074_GUI_P4 = new(0.384f, 0.737f, 0.369f, 1f);                 // 0xFF5EBC62 (ABGR) -> 0x62BC5EFF (RGBA), 0xFF62BC5E (ARGB)
	public static readonly Vector4 InGameColor_075_GUI_PNPC = new(0.792f, 0.718f, 0.831f, 1f);               // 0xFFD4B7CA (ABGR) -> 0xCAB7D4FF (RGBA), 0xFFCAB7D4 (ARGB)
	public static readonly Vector4 InGameColor_076_GUI_PStealth = new(0.655f, 0.392f, 0.792f, 1f);           // 0xFFCA64A7 (ABGR) -> 0xA764CAFF (RGBA), 0xFFA764CA (ARGB)
	public static readonly Vector4 InGameColor_077_GUI_Tab00 = new(0.831f, 0.698f, 0.302f, 1f);              // 0xFF4DB2D4 (ABGR) -> 0xD4B24DFF (RGBA), 0xFFD4B24D (ARGB)
	public static readonly Vector4 InGameColor_078_GUI_Tab01 = new(0.478f, 0.639f, 0.800f, 1f);              // 0xFFCCA37A (ABGR) -> 0x7AA3CCFF (RGBA), 0xFF7AA3CC (ARGB)
	public static readonly Vector4 InGameColor_079_GUI_Tab02 = new(0.800f, 0.439f, 0.369f, 1f);              // 0xFF5E70C8 (ABGR) -> 0xC8705EFF (RGBA), 0xFFC8705E (ARGB)
	public static readonly Vector4 InGameColor_080_GUI_Tab03 = new(0.635f, 0.467f, 0.863f, 1f);              // 0xFFDC77A2 (ABGR) -> 0xA277DCFF (RGBA), 0xFFA277DC (ARGB)
	public static readonly Vector4 InGameColor_081_GUI_Tab04 = new(0.353f, 0.631f, 0.306f, 1f);              // 0xFF4EA15A (ABGR) -> 0x5AA14EFF (RGBA), 0xFF5AA14E (ARGB)
	public static readonly Vector4 InGameColor_082_GUI_Tab05 = new(0.733f, 0.502f, 0.302f, 1f);              // 0xFF4D80BB (ABGR) -> 0xBB804DFF (RGBA), 0xFFBB804D (ARGB)
	public static readonly Vector4 InGameColor_083_GUI_Tab06 = new(0.424f, 0.694f, 0.678f, 1f);              // 0xFFADB16C (ABGR) -> 0x6CB1ADFF (RGBA), 0xFF6CB1AD (ARGB)
	public static readonly Vector4 InGameColor_084_GUI_MapEmWarningLv1 = new(0.180f, 0.851f, 0.902f, 1f);    // 0xFFE6D92E (ABGR) -> 0x2ED9E6FF (RGBA), 0xFF2ED9E6 (ARGB)
	public static readonly Vector4 InGameColor_085_GUI_MapEmWarningLv2 = new(0.490f, 0.902f, 0.271f, 1f);    // 0xFF45E67D (ABGR) -> 0x7DE645FF (RGBA), 0xFF7DE645 (ARGB)
	public static readonly Vector4 InGameColor_086_GUI_MapEmWarningLv3 = new(0.831f, 0.902f, 0.224f, 1f);    // 0xFF39E6D4 (ABGR) -> 0xD4E639FF (RGBA), 0xFFD4E639 (ARGB)
	public static readonly Vector4 InGameColor_087_GUI_MapEmWarningLv4 = new(0.902f, 0.686f, 0.180f, 1f);    // 0xFF2EAFE6 (ABGR) -> 0xE6AF2EFF (RGBA), 0xFFE6AF2E (ARGB)
	public static readonly Vector4 InGameColor_088_GUI_MapEmWarningLv5 = new(0.902f, 0.286f, 0.227f, 1f);    // 0xFF3A49E6 (ABGR) -> 0xE6493AFF (RGBA), 0xFFE6493A (ARGB)
	public static readonly Vector4 InGameColor_089_GUI_Sharp00 = new(0.902f, 0.227f, 0.227f, 1f);            // 0xFF3A3AE6 (ABGR) -> 0xE63A3AFF (RGBA), 0xFFE63A3A (ARGB)
	public static readonly Vector4 InGameColor_090_GUI_Sharp01 = new(0.902f, 0.541f, 0.271f, 1f);            // 0xFF458AE6 (ABGR) -> 0xE68A45FF (RGBA), 0xFFE68A45 (ARGB)
	public static readonly Vector4 InGameColor_091_GUI_Sharp02 = new(0.851f, 0.773f, 0.255f, 1f);            // 0xFF41C5D9 (ABGR) -> 0xD9C541FF (RGBA), 0xFFD9C541 (ARGB)
	public static readonly Vector4 InGameColor_092_GUI_Sharp03 = new(0.408f, 0.800f, 0.322f, 1f);            // 0xFF52CC68 (ABGR) -> 0x68CC52FF (RGBA), 0xFF68CC52 (ARGB)
	public static readonly Vector4 InGameColor_093_GUI_Sharp04 = new(0.255f, 0.635f, 0.851f, 1f);            // 0xFFD9A241 (ABGR) -> 0x41A2D9FF (RGBA), 0xFF41A2D9 (ARGB)
	public static readonly Vector4 InGameColor_094_GUI_Sharp05 = new(0.851f, 0.851f, 0.851f, 1f);            // 0xFFD9D9D9 (ABGR) -> 0xD9D9D9FF (RGBA), 0xFFD9D9D9 (ARGB)
	public static readonly Vector4 InGameColor_095_GUI_Sharp06 = new(0.776f, 0.404f, 0.902f, 1f);            // 0xFFE667C6 (ABGR) -> 0xC667E6FF (RGBA), 0xFFC667E6 (ARGB)
	public static readonly Vector4 InGameColor_096_GUI_LSword_Spr00 = new(0.961f, 0.961f, 0.961f, 1f);       // 0xFFF5F5F5 (ABGR) -> 0xF5F5F5FF (RGBA), 0xFFF5F5F5 (ARGB)
	public static readonly Vector4 InGameColor_097_GUI_LSword_Spr01 = new(0.969f, 0.839f, 0.192f, 1f);       // 0xFF31D6F7 (ABGR) -> 0xF7D631FF (RGBA), 0xFFF7D631 (ARGB)
	public static readonly Vector4 InGameColor_098_GUI_LSword_Spr02 = new(0.988f, 0.263f, 0.349f, 1f);       // 0xFF5943FC (ABGR) -> 0xFC4359FF (RGBA), 0xFFFC4359 (ARGB)
	public static readonly Vector4 InGameColor_099_GUI_Insect_Ext00 = new(0.392f, 0.902f, 0.510f, 1f);       // 0xFF82E664 (ABGR) -> 0x64E682FF (RGBA), 0xFF64E682 (ARGB)
	public static readonly Vector4 InGameColor_100_GUI_Insect_Ext01 = new(0.988f, 0.263f, 0.349f, 1f);       // 0xFF5943FC (ABGR) -> 0xFC4359FF (RGBA), 0xFFFC4359 (ARGB)
	public static readonly Vector4 InGameColor_101_GUI_Insect_Ext02 = new(0.961f, 0.961f, 0.961f, 1f);       // 0xFFF5F5F5 (ABGR) -> 0xF5F5F5FF (RGBA), 0xFFF5F5F5 (ARGB)
	public static readonly Vector4 InGameColor_102_GUI_Insect_Ext03 = new(0.969f, 0.663f, 0.267f, 1f);       // 0xFF44A9F7 (ABGR) -> 0xF7A944FF (RGBA), 0xFFF7A944 (ARGB)
	public static readonly Vector4 InGameColor_103_GUI_Insect_Ext02_2 = new(0.702f, 0.702f, 0.702f, 1f);     // 0xFFB3B3B3 (ABGR) -> 0xB3B3B3FF (RGBA), 0xFFB3B3B3 (ARGB)
	public static readonly Vector4 InGameColor_104_GUI_Horn_Note00 = new(0.961f, 0.961f, 0.961f, 1f);        // 0xFFF5F5F5 (ABGR) -> 0xF5F5F5FF (RGBA), 0xFFF5F5F5 (ARGB)
	public static readonly Vector4 InGameColor_105_GUI_Horn_Note01 = new(1f, 0.388f, 0.388f, 1f);            // 0xFF6363FF (ABGR) -> 0xFF6363FF (RGBA), 0xFFFF6363 (ARGB)
	public static readonly Vector4 InGameColor_106_GUI_Horn_Note02 = new(0.498f, 0.627f, 1f, 1f);            // 0xFFFFA07F (ABGR) -> 0x7FA0FFFF (RGBA), 0xFF7FA0FF (ARGB)
	public static readonly Vector4 InGameColor_107_GUI_Horn_Note03 = new(0.482f, 0.871f, 0.922f, 1f);        // 0xFFEBDE7B (ABGR) -> 0x7BDEEBFF (RGBA), 0xFF7BDEEB (ARGB)
	public static readonly Vector4 InGameColor_108_GUI_Horn_Note04 = new(0.475f, 0.843f, 0.514f, 1f);        // 0xFF83D779 (ABGR) -> 0x79D783FF (RGBA), 0xFF79D783 (ARGB)
	public static readonly Vector4 InGameColor_109_GUI_Horn_Note05 = new(0.973f, 0.820f, 0.196f, 1f);        // 0xFF32D1F8 (ABGR) -> 0xF8D132FF (RGBA), 0xFFF8D132 (ARGB)
	public static readonly Vector4 InGameColor_110_GUI_Horn_Note06 = new(1f, 0.620f, 0.412f, 1f);            // 0xFF699EFF (ABGR) -> 0xFF9E69FF (RGBA), 0xFFFF9E69 (ARGB)
	public static readonly Vector4 InGameColor_111_GUI_Horn_Note07 = new(0.769f, 0.482f, 0.984f, 1f);        // 0xFFFB7BC4 (ABGR) -> 0xC47BFBFF (RGBA), 0xFFC47BFB (ARGB)
	public static readonly Vector4 InGameColor_112_GUI_Horn_Activation = new(0.902f, 0.902f, 0.361f, 1f);    // 0xFF5CE6E6 (ABGR) -> 0xE6E65CFF (RGBA), 0xFFE6E65C (ARGB)
	public static readonly Vector4 InGameColor_113_GUI_Horn_ActivationAdd = new(0.792f, 0.475f, 0.949f, 1f); // 0xFFF279CA (ABGR) -> 0xCA79F2FF (RGBA), 0xFFCA79F2 (ARGB)

	public static readonly JsonSerializerOptions JsonSerializerOptionsInstance = new()
	{
		WriteIndented = true,
		AllowTrailingCommas = true,
		IncludeFields = true,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};
}