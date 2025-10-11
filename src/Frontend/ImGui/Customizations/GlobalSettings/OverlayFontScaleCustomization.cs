using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class OverlayFontScaleCustomization : Customization
{
	public bool? ScaleWithReframeworkFontSize = null;
	public float? OverlayFontScaleModifier = null;

	public bool RenderImGui(string? parentName = "", OverlayFontScaleCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-overlay-font";

		if(ImGuiHelper.ResettableTreeNode(localization.OverlayFontScale, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ScaleWithREFrameworkFontSize}##{customizationName}", ref ScaleWithReframeworkFontSize, defaultCustomization?.ScaleWithReframeworkFontSize);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.OverlayFontScaleModifier}##{customizationName}", ref OverlayFontScaleModifier, 0.001f, 1f, 128f, "%.3f", defaultCustomization?.OverlayFontScaleModifier);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(OverlayFontScaleCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ScaleWithReframeworkFontSize = defaultCustomization.ScaleWithReframeworkFontSize;
		OverlayFontScaleModifier = defaultCustomization.OverlayFontScaleModifier;
	}
}