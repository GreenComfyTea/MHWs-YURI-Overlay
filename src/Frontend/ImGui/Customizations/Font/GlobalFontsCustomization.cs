using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalFontsCustomization : Customization
{
	public string Localization = Constants.DefaultLocalization;

	public MenuFontCustomization MenuFont = new();
	//public GlobalOverlayFontCustomization GlobalOverlayFont = new();

	public GlobalFontsCustomization() { }

	public bool RenderImGui(string parentName = "", GlobalFontsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-fonts";

		if(ImGui.TreeNode($"{localization.GlobalFonts}##${customizationName}"))
		{
			isChanged |= MenuFont.RenderImGui(customizationName);
			//isChanged |= Performance.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
