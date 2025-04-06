using ImGuiNET;

namespace YURI_Overlay;

internal class BarElementColorsCustomization : Customization
{
	public GradientColorCustomization Foreground = new();
	public GradientColorCustomization Background = new();

	public bool RenderImGui(string parentName = "", BarElementColorsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-colors";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Colors}##{customizationName}"))
		{
			isChanged |= Foreground.RenderImGui(localization.Foreground, customizationName, defaultCustomization?.Foreground);
			isChanged |= Background.RenderImGui(localization.Background, customizationName, defaultCustomization?.Background);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementColorsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;
		Foreground.Reset(defaultCustomization.Foreground);
		Background.Reset(defaultCustomization.Background);
	}
}
