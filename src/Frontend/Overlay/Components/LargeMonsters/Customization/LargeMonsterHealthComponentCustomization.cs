﻿using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponentCustomization : Customization
{
	public bool visible = true;
	public OffsetCustomization offset = new();
	public LabelElementCustomization healthValueLabel = new();
	public LabelElementCustomization healthPercentageLabel = new();
	public BarElementCustomization healthBar = new();

	public LargeMonsterHealthComponentCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-health";

		if(ImGui.TreeNode($"{localization.health}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= healthValueLabel.RenderImGui(localization.healthValueLabel, customizationName);
			isChanged |= healthPercentageLabel.RenderImGui(localization.healthPercentageLabel, customizationName);
			isChanged |= healthBar.RenderImGui(localization.healthBar, customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
