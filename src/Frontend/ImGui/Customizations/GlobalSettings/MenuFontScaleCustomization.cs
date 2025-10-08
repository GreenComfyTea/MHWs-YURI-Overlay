using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class MenuFontScaleCustomization : Customization
{
	public bool ScaleWithREFrameworkFontSize = false;
	public float MenuFontScaleModifier = 1f;

	public bool RenderImGui(string parentName = "", MenuFontScaleCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-menu-font";

		if(ImGuiHelper.ResettableTreeNode(localization.MenuFontScale, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ScaleWithREFrameworkFontSize}##{customizationName}", ref ScaleWithREFrameworkFontSize, defaultCustomization?.ScaleWithREFrameworkFontSize);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MenuFontScaleModifier}##{customizationName}", ref MenuFontScaleModifier, 0.001f, 1f, 128f, "%.3f", defaultCustomization?.MenuFontScaleModifier);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(MenuFontScaleCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ScaleWithREFrameworkFontSize = defaultCustomization.ScaleWithREFrameworkFontSize;
		MenuFontScaleModifier = defaultCustomization.MenuFontScaleModifier;
	}
}