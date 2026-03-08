using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class BarElementColorsCustomization : Customization
{
	public GradientColorCustomization Foreground = new();
	public GradientColorCustomization Background = new();

	public bool RenderImGui(string? parentName = "", BarElementColorsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-colors";

		if(ImGuiHelper.ResettableTreeNode(localization.Colors, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= this.Foreground.RenderImGui(localization.Foreground, $"{customizationName}-foreground", defaultCustomization?.Foreground);
			isChanged |= this.Background.RenderImGui(localization.Background, $"{customizationName}-background", defaultCustomization?.Background);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementColorsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Foreground.Reset(defaultCustomization.Foreground);
		this.Background.Reset(defaultCustomization.Background);
	}
}