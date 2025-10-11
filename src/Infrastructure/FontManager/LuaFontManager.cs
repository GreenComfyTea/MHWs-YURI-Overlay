using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LuaFontManager
{
	private static readonly Lazy<LuaFontManager> Lazy = new(() => new LuaFontManager());
	public static LuaFontManager Instance => Lazy.Value;

	public Dictionary<string, ImFontPtr> Fonts = [];

	public List<string> FontNames = [];

	public EventHandler FontsChanged = delegate { };

	public (string, ImFontPtr)? ActiveMenuFont = null;
	public (string, ImFontPtr)? ActiveGlobalOverlayFont = null;

	private bool _isGameUpdatePending = true;

	private LuaFontManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[LuaFontManager] Initializing...");

		Timers.SetInterval(SetGameUpdatePending, 1000);

		ConfigManager.Instance.AnyConfigChanged = OnAnyConfigChanged;

		LogManager.Info("[LuaFontManager] Initialized!");
	}

	public void GameUpdate()
	{
		try
		{
			if(!_isGameUpdatePending) return;
			_isGameUpdatePending = false;

			// Update all fonts list

			var areFontsAdded = false;

			var fonts = ImGui.GetIO().Fonts.Fonts;

			for(var i = 0; i < fonts.Size; i++)
			{
				var font = fonts[i];
				var fontName = font.GetDebugName();

				fontName = string.Empty.Equals(fontName) ? "Default" : fontName;

				if(FontNames.Contains(fontName)) continue;

				LogManager.Info($"[LuaFontManager] Font \"{fontName}\": Initialized!");

				FontNames.Add(fontName);
				Fonts[fontName] = font;

				areFontsAdded = true;
			}

			if(areFontsAdded) EmitFontsChanged();

			UpdateActiveFonts();
		}
		catch(Exception error)
		{
			LogManager.Error($"[LuaFontManager] {error}");
		}
	}

	private void UpdateActiveFonts()
	{
		try
		{
			//var selectedMenuFontOption = ConfigManager.Instance.ActiveConfig.Data..GlobalSettings.GlobalFonts.MenuFont.FontName;

			//var localization = LocalizationManager.Instance.ActiveLocalization.Data;

			//LogManager.Debug($"selectedMenuFontOption: {selectedMenuFontOption}");

			//if(selectedMenuFontOption == LocalizationHelper.Instance.DefaultDefinedByLocalization)
			//{
			//	var localizationMenuFontName = localization.Fonts.MenuFont.Name;

			//	LogManager.Debug($"1 localizationMenuFontName: {localizationMenuFontName}");

			//	if(Fonts.TryGetValue($"{localizationMenuFontName}, 32px", out var localizationMenuFont))
			//	{
			//		ActiveMenuFont = (selectedMenuFontOption, localizationMenuFont);
			//		LogManager.Debug($"11 Active Menu Font: {selectedMenuFontOption}");
			//	}
			//	else
			//	{
			//		var foundIndex = FontNames.FindIndex((iteratedFontName) => iteratedFontName.Contains(localizationMenuFontName));

			//		if(foundIndex == -1)
			//		{
			//			ActiveMenuFont = null;
			//			LogManager.Debug($"12 Active Menu Font: null");
			//		}
			//		else
			//		{
			//			var foundFontName = FontNames[foundIndex];
			//			var foundFont = Fonts[foundFontName];
			//			ActiveMenuFont = (foundFontName, foundFont);
			//			LogManager.Debug($"13 Active Menu Font: {selectedMenuFontOption}");
			//		}
			//	}
			//}
			//else
			//{
			//	if(Fonts.TryGetValue($"{selectedMenuFontOption}, 32px", out var menuFont))
			//	{
			//		ActiveMenuFont = (selectedMenuFontOption, menuFont);
			//		LogManager.Debug($"21 Active Menu Font: {selectedMenuFontOption}");
			//	}
			//	else
			//	{
			//		var foundIndex = FontNames.FindIndex((iteratedFontName) => iteratedFontName.Contains(selectedMenuFontOption));

			//		if(foundIndex == -1)
			//		{
			//			ActiveMenuFont = null;
			//			LogManager.Debug($"22 Active Menu Font: null");
			//		}
			//		else
			//		{
			//			var foundFontName = FontNames[foundIndex];
			//			var foundFont = Fonts[foundFontName];
			//			ActiveMenuFont = (foundFontName, foundFont);
			//			LogManager.Debug($"23 Active Menu Font: {selectedMenuFontOption}");
			//		}
			//	}
			//}
		}
		catch(Exception e)
		{
			LogManager.Error($"[LuaFontManager] Error while updating active fonts: {e}");
		}
	}

	private void SetGameUpdatePending()
	{
		_isGameUpdatePending = true;
	}

	private void OnAnyConfigChanged(object? sender, EventArgs args)
	{
		UpdateActiveFonts();
	}

	private void EmitFontsChanged()
	{
		Utils.EmitEvents(this, FontsChanged);
	}
}