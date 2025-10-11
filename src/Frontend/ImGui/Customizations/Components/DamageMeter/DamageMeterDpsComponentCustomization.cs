using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterDpsComponentCustomization : Customization
{
	public bool? Visible = null;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();

	public bool RenderImGui(string? parentName = "", DamageMeterDpsComponentCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dps";

		if(ImGuiHelper.ResettableTreeNode(localization.DPS, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, $"{customizationName}-value-label", defaultCustomization?.ValueLabel);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, $"{customizationName}-percentage-label", defaultCustomization?.PercentageLabel);
			isChanged |= Bar.RenderImGui(localization.Bar, $"{customizationName}-bar", defaultCustomization?.Bar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterDpsComponentCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Offset.Reset(defaultCustomization.Offset);
		ValueLabel.Reset(defaultCustomization.ValueLabel);
		PercentageLabel.Reset(defaultCustomization.PercentageLabel);
		Bar.Reset(defaultCustomization.Bar);
	}
}