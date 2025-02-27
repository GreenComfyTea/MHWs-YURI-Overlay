﻿using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool Visible = true;
	public float Thickness = 1f;
	public float Offset = 0f;

	private int _styleIndex = (int) OutlineStyles.Outside;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public OutlineStyles Style { get => (OutlineStyles) _styleIndex; set => _styleIndex = (int) value; }

	public ColorCustomization Color { get; set; } = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGui.TreeNode($"{localization.outline}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref Visible);
			isChanged |= ImGui.DragFloat($"{localization.thickness}##{customizationName}", ref Thickness, 0.1f, 0, 1024f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.offset}##{customizationName}", ref Offset, 0.1f, -1024f, 1024f, "%.1f");

			isChanged |= ImGui.Combo($"{localization.style}##{customizationName}", ref _styleIndex, localizationHelper.OutlineStyles, localizationHelper.FillDirections.Length);

			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
