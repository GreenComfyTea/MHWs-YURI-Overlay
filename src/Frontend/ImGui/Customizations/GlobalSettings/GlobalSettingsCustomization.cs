﻿using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalSettingsCustomization : Customization
{
	public string? Localization = null;

	//public GlobalFontsCustomization GlobalFonts = new();
	public GlobalScaleCustomization GlobalScale = new();
	public PerformanceCustomization Performance = new();

	public bool RenderImGui(string? parentName = "", GlobalSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.GlobalSettings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			if(LocalizationManager.Instance.Customization is not null) isChanged |= LocalizationManager.Instance.Customization.RenderImGui(customizationName);
			//isChanged |= GlobalFonts.RenderImGui(customizationName);
			isChanged |= GlobalScale.RenderImGui(customizationName, defaultCustomization?.GlobalScale);
			isChanged |= Performance.RenderImGui(customizationName, defaultCustomization?.Performance);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GlobalSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		GlobalScale.Reset(defaultCustomization.GlobalScale);
		Performance.Reset(defaultCustomization.Performance);
	}
}