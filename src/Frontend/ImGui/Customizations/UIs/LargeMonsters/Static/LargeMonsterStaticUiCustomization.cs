using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUiCustomization : Customization
{
	public bool? Enabled = null;
	public LargeMonsterStaticUiSettingsCustomization Settings = new();
	public AnchoredPositionCustomization Position = new();
	public SpacingCustomization Spacing = new();
	public LargeMonsterStaticUiSortingCustomization Sorting = new();

	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();
	public LargeMonsterStaminaComponentCustomization Stamina = new();
	public LargeMonsterRageComponentCustomization Rage = new();

	public bool RenderImGui(string? parentName = "", LargeMonsterStaticUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-static";

		if(ImGuiHelper.ResettableTreeNode(localization.Static, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= Position.RenderImGui(customizationName, defaultCustomization?.Position);
			isChanged |= Spacing.RenderImGui(customizationName, defaultCustomization?.Spacing);
			isChanged |= Sorting.RenderImGui(customizationName, defaultCustomization?.Sorting);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= Health.RenderImGui(customizationName, defaultCustomization?.Health);
			isChanged |= Stamina.RenderImGui(customizationName, defaultCustomization?.Stamina);
			isChanged |= Rage.RenderImGui(customizationName, defaultCustomization?.Rage);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Settings.Reset(defaultCustomization.Settings);
		Position.Reset(defaultCustomization.Position);
		Spacing.Reset(defaultCustomization.Spacing);
		Sorting.Reset(defaultCustomization.Sorting);

		NameLabel.Reset(defaultCustomization.NameLabel);
		Health.Reset(defaultCustomization.Health);
		Stamina.Reset(defaultCustomization.Stamina);
		Rage.Reset(defaultCustomization.Rage);
	}
}