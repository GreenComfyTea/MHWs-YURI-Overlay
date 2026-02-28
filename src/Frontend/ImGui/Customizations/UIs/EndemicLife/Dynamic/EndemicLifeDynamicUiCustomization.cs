using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class EndemicLifeDynamicUiCustomization : Customization
{
	public bool? Enabled = null;
	public EndemicLifeDynamicUiSettingsCustomization Settings = new();
	public WorldOffsetCustomization WorldOffset = new();
	public OffsetCustomization Offset = new();

	public LabelElementCustomization NameLabel = new();

	public bool RenderImGui(string? parentName = "", EndemicLifeDynamicUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGuiHelper.ResettableTreeNode(localization.EndemicLifeUI, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= WorldOffset.RenderImGui(customizationName, defaultCustomization?.WorldOffset);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeDynamicUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Settings.Reset(defaultCustomization.Settings);
		WorldOffset.Reset(defaultCustomization.WorldOffset);
		Offset.Reset(defaultCustomization.Offset);

		NameLabel.Reset(defaultCustomization.NameLabel);
	}
}