using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementColorsCustomization : Customization
{
	public GradientColorCustomization Foreground = new();
	public GradientColorCustomization Background = new();

	public bool RenderImGui(string parentName = "", BarElementColorsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-colors";

		if(ImGuiHelper.ResettableTreeNode(localization.Colors, customizationName, ref isChanged, defaultCustomization, Reset))
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