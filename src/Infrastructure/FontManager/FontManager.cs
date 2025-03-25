namespace YURI_Overlay;

internal sealed partial class FontManager
{
	private static readonly Lazy<FontManager> Lazy = new(() => new FontManager());
	public static FontManager Instance => Lazy.Value;

	//public bool IsInitialized = false;
	//public ImGuiFontCustomization Customization;

	//private readonly ushort[] FullGlyphRange = [0x0020, ushort.MaxValue, 0];
	//private readonly ushort[] EmojiGlyphRange = [0x2122, 0x2B55, 0];

	//public Dictionary<string, FontObject> Fonts = [];

	//private readonly List<string> FontNames = [];

	//public FontObject ActiveFont = (string.Empty, null);

	//public List<ushort[]> GlyphRanges = [];

	private FontManager() { }

	//public void Initialize()
	//{
	//	LogManager.Info("[FontManager] Initializing...");

	//	GlyphRanges.Add(FullGlyphRange);
	//	GlyphRanges.Add(EmojiGlyphRange);

	//	LoadAllFonts();
	//	SetCurrentFont(LocalizationManager.Instance.ActiveLocalization);

	//	IsInitialized = true;

	//	LogManager.Info("[FontManager] Initialization Done!");
	//}

	//public void LoadAllFonts()
	//{
	//	LogManager.Info("[FontManager] Loading All Fonts...");

	//	foreach(var localizationPair in LocalizationManager.Instance.Localizations)
	//	{
	//		var localizationIsoName = localizationPair.Key;
	//		var localization = localizationPair.Value;

	//		if(Fonts.TryGetValue(localizationIsoName, out _)) continue;

	//		Fonts[localizationIsoName] = LoadFont(localization);
	//	}

	//	ImGui.GetIO().Fonts.Build();

	//	LogManager.Info("[FontManager] Loading All Fonts Done!");
	//}

	//public FontObject LoadFont(JsonDatabase<Localization> localization)
	//{
	//	var fontConfigs = ConfigManager.Instance.ActiveConfig.Data.Fonts;

	//	var fontInfo = localization.Data.CustomizationFontInfo;
	//	var fontName = fontInfo.Name;

	//	LogManager.Info($"[FontManager] {fontName}: Loading...");

	//	if(FontNames.Contains(fontName))
	//	{
	//		LogManager.Info($"[FontManager] {fontName}: Already Loaded. Skipping.");
	//		Fonts.TryGetValue(fontName, out var foundFont);
	//		return foundFont;
	//	}

	//	var glyphRanges = GetGlyphRanges(localization);
	//	GlyphRanges.Add(glyphRanges);

	//	var isFound = fontConfigs.TryGetValue(fontName, out var customization);

	//	if(!isFound)
	//	{
	//		customization = new ImGuiFontCustomization();
	//		fontConfigs[fontName] = customization;
	//	}

	//	var newFont = RegisterFont(
	//		Path.Combine(Constants.FontsPath, fontName),
	//		customization!.FontSize,
	//		glyphRanges,
	//		false,
	//		customization.VerticalOversample,
	//		customization.HorizontalOversample
	//	);

	//	RegisterFont(
	//		$"{Constants.FontsPath}{Constants.EmojiFont}",
	//		customization.FontSize,
	//		EmojiGlyphRange,
	//		true,
	//		customization.VerticalOversample,
	//		customization.HorizontalOversample
	//	);

	//	FontNames.Add(fontName);

	//	LogManager.Info($"[FontManager] {fontName}: Loading Done!");
	//	return (fontName, newFont);
	//}

	//public void SetCurrentFont(JsonDatabase<Localization> localization)
	//{
	//	if(!IsInitialized) return;

	//	Fonts.TryGetValue(localization.Name, out ActiveFont);

	//	ConfigManager.Instance.ActiveConfig.Data.Fonts.TryGetValue(localization.Data.CustomizationFontInfo.Name, out Customization);
	//}

	//public void RecreateFontCustomizations()
	//{
	//	var fontConfig = ConfigManager.Instance.ActiveConfig.Data.Fonts;

	//	foreach(var fontName in FontNames)
	//	{
	//		fontConfig[fontName] = new ImGuiFontCustomization();
	//	}
	//}

	//private static ushort[] GetGlyphRanges(JsonDatabase<Localization> localization)
	//{
	//	var glyphRangeStringArray = localization.Data.CustomizationFontInfo.GlyphRanges;

	//	var glyphRanges = new ushort[glyphRangeStringArray.Length + 1];

	//	for(var i = 0; i < glyphRangeStringArray.Length; i += 2)
	//	{
	//		var glyph = Convert.ToUInt16(glyphRangeStringArray[i], 16);

	//		glyphRanges[i] = glyph;
	//	}

	//	glyphRanges[^1] = 0;

	//	return glyphRanges;
	//}

	//private unsafe ImFontPtr RegisterFont(string filePathName, float fontSize, ushort[] glyphRanges, bool mergeMode = false, int horizontalOversample = 2, int verticalOversample = 2)
	//{
	//	try
	//	{
	//		LogManager.Info($"[FontManager] Registering Font: {filePathName}");

	//		if(string.IsNullOrEmpty(filePathName))
	//		{
	//			LogManager.Error("[FontManager] File path is null or empty.");
	//			return null;
	//		}

	//		string fullPath = Path.Combine(@"D:\Programs\Steam\steamapps\common\MonsterHunterWilds\", filePathName);
	//		if(!File.Exists(fullPath))
	//		{
	//			LogManager.Error($"[FontManager] Font file not found: {fullPath}");
	//			return null;
	//		}

	//		if(glyphRanges is null || glyphRanges.Length == 0)
	//		{
	//			LogManager.Error("[FontManager] Glyph ranges are null or empty.");
	//			return null;
	//		}

	//		fixed(ushort* glyphRangesPointer = glyphRanges)
	//		{
	//			LogManager.Info($"[FontManager] Allocating ImFontConfig");

	//			ImFontConfig* fontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
	//			if(fontConfig is null)
	//			{
	//				LogManager.Error("[FontManager] ImFontConfig allocation failed.");
	//				return null;
	//			}

	//			fontConfig->MergeMode = mergeMode ? (byte) 1 : (byte) 0;
	//			fontConfig->PixelSnapH = 1;
	//			fontConfig->GlyphMinAdvanceX = fontSize;
	//			fontConfig->GlyphRanges = glyphRangesPointer;

	//			LogManager.Info($"[FontManager] Registering Font at {fullPath} with size {fontSize}");

	//			ImFontPtr font = ImGui.GetIO().Fonts.AddFontFromFileTTF(fullPath, fontSize, fontConfig);

	//			if(font.NativePtr is null)
	//			{
	//				LogManager.Error("[FontManager] AddFontFromFileTTF returned null.");
	//				ImGuiNative.ImFontConfig_destroy(fontConfig);
	//				return null;
	//			}

	//			LogManager.Info("[FontManager] Font successfully registered.");

	//			ImGuiNative.ImFontConfig_destroy(fontConfig);

	//			return font;
	//		}
	//	}
	//	catch(Exception exception)
	//	{
	//		LogManager.Error(exception);
	//		return null;
	//	}
	//}
}