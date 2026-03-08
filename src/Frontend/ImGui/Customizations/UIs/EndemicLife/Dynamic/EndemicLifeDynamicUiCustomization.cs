using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class EndemicLifeDynamicUiCustomization : Customization
{
	public bool? Enabled;
	public EndemicLifeDynamicUiSettingsCustomization Settings = new();
	public WorldOffsetCustomization WorldOffset = new();
	public OffsetCustomization Offset = new();

	public LabelElementCustomization NameLabel = new();

	public bool RenderImGui(string? parentName = "", EndemicLifeDynamicUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGuiHelper.ResettableTreeNode(localization.EndemicLifeUI, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref this.Enabled, defaultCustomization?.Enabled);

			isChanged |= this.Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= this.WorldOffset.RenderImGui(customizationName, defaultCustomization?.WorldOffset);
			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeDynamicUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Enabled = defaultCustomization.Enabled;
		this.Settings.Reset(defaultCustomization.Settings);
		this.WorldOffset.Reset(defaultCustomization.WorldOffset);
		this.Offset.Reset(defaultCustomization.Offset);

		this.NameLabel.Reset(defaultCustomization.NameLabel);
	}
}