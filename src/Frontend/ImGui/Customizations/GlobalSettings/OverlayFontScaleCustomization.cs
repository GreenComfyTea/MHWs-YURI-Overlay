using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class OverlayFontScaleCustomization : Customization
{
	public bool? ScaleWithReframeworkFontSize;
	public float? OverlayFontScaleModifier;

	public bool RenderImGui(string? parentName = "", OverlayFontScaleCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-overlay-font";

		if(ImGuiHelper.ResettableTreeNode(localization.OverlayFontScale, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox(
				$"{localization.ScaleWithREFrameworkFontSize}##{customizationName}",
				ref this.ScaleWithReframeworkFontSize,
				defaultCustomization?.ScaleWithReframeworkFontSize
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.OverlayFontScaleModifier}##{customizationName}",
				ref this.OverlayFontScaleModifier,
				0.001f,
				1f,
				128f,
				"%.3f",
				defaultCustomization?.OverlayFontScaleModifier
			);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(OverlayFontScaleCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.ScaleWithReframeworkFontSize = defaultCustomization.ScaleWithReframeworkFontSize;
		this.OverlayFontScaleModifier = defaultCustomization.OverlayFontScaleModifier;
	}
}