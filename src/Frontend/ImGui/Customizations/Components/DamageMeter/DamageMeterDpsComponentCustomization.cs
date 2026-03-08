using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterDpsComponentCustomization : Customization
{
	public bool? Visible;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();

	public bool RenderImGui(string? parentName = "", DamageMeterDpsComponentCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dps";

		if(ImGuiHelper.ResettableTreeNode(localization.DPS, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref this.Visible, defaultCustomization?.Visible);
			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.ValueLabel.RenderImGui(localization.ValueLabel, $"{customizationName}-value-label", defaultCustomization?.ValueLabel);
			isChanged |= this.PercentageLabel.RenderImGui(localization.PercentageLabel, $"{customizationName}-percentage-label", defaultCustomization?.PercentageLabel);
			isChanged |= this.Bar.RenderImGui(localization.Bar, $"{customizationName}-bar", defaultCustomization?.Bar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterDpsComponentCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Visible = defaultCustomization.Visible;
		this.Offset.Reset(defaultCustomization.Offset);
		this.ValueLabel.Reset(defaultCustomization.ValueLabel);
		this.PercentageLabel.Reset(defaultCustomization.PercentageLabel);
		this.Bar.Reset(defaultCustomization.Bar);
	}
}