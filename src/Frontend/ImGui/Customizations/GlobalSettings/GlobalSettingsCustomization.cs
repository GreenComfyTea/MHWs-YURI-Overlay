using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalSettingsCustomization : Customization
{
	public string Localization = Constants.DefaultLocalization;

	//public GlobalFontsCustomization GlobalFonts = new();
	public GlobalScaleCustomization GlobalScale = new();
	public PerformanceCustomization Performance = new();

	public GlobalSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGui.TreeNode($"{localization.GlobalSettings}##${customizationName}"))
		{
			isChanged |= LocalizationManager.Instance.Customization.RenderImGui(customizationName);
			//isChanged |= GlobalFonts.RenderImGui(customizationName);
			isChanged |= GlobalScale.RenderImGui(customizationName);
			isChanged |= Performance.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
