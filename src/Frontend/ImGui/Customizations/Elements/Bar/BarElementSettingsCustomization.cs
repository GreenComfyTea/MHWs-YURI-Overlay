﻿using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	[JsonIgnore]
	private int _fillDirectionIndex = (int) FillDirections.LeftToRight;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public FillDirections FillDirection { get => (FillDirections) _fillDirectionIndex; set => _fillDirectionIndex = (int) value; }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.Settings}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.FillDirection}##{customizationName}", ref _fillDirectionIndex, localizationHelper.FillDirections, localizationHelper.FillDirections.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
