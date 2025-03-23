using ImGuiNET;

namespace YURI_Overlay;

internal sealed class MenuFontCustomization : Customization
{
	public string FontName = string.Empty;

	private int _activeFontIndex = 0;
	private string[] _fontNames = [];

	public MenuFontCustomization()
	{
		LuaFontManager.Instance.FontsChanged += OnFontsChanged;
		LocalizationManager.Instance.ActiveLocalizationChanged += OnActiveLocalizationChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-menu-font";

		if(ImGui.TreeNode($"{localization.MenuFont}##{customizationName}"))
		{
			var isFontChanged = ImGuiHelper.Combo(localization.Font, ref _activeFontIndex, _fontNames);
			isChanged |= isFontChanged;

			if(isFontChanged)
			{
				FontName = _activeFontIndex == 0 ? localizationHelper.DefaultDefinedByLocalization : _fontNames[_activeFontIndex];
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	private void UpdateFontNames()
	{
		var localizationHelper = LocalizationHelper.Instance;

		var newFontNames = LuaFontManager.Instance.FontNames.ToArray();
		newFontNames = newFontNames.Prepend(localizationHelper.DefinedByLocalization).ToArray();

		_fontNames = newFontNames;
		FontName = _activeFontIndex == 0 ? localizationHelper.DefaultDefinedByLocalization : newFontNames[_activeFontIndex];
	}

	private void OnFontsChanged(object sender, EventArgs e)
	{
		UpdateFontNames();
	}

	private void OnActiveLocalizationChanged(object sender, EventArgs e)
	{
		UpdateFontNames();
	}
}
