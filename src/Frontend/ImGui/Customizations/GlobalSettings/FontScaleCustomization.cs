using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class FontScaleCustomization : Customization
{
	public MenuFontScaleCustomization MenuFontScale = new();
	public OverlayFontScaleCustomization OverlayFontScale = new();

	public bool RenderImGui(string parentName = "", FontScaleCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-font-scale";

		if(ImGuiHelper.ResettableTreeNode(localization.FontScale, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= MenuFontScale.RenderImGui(customizationName, defaultCustomization?.MenuFontScale);
			isChanged |= OverlayFontScale.RenderImGui(customizationName, defaultCustomization?.OverlayFontScale);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(FontScaleCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		MenuFontScale.Reset(defaultCustomization.MenuFontScale);
		OverlayFontScale.Reset(defaultCustomization.OverlayFontScale);
	}
}