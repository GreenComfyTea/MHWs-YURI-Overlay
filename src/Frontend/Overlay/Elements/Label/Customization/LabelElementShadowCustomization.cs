﻿using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementShadowCustomization : Customization
{
	public bool visible = true;
	public OffsetCustomization offset = new();
	public ColorCustomization color = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-shadow";

		if(ImGui.TreeNode($"{localization.shadow}##{parentName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{parentName}", ref visible);

			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
