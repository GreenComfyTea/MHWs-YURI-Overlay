using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterTargetedUiCustomization : Customization
{
	public bool? Enabled = null;
	public LargeMonsterTargetedUiSettingsCustomization Settings = new();
	public AnchoredPositionCustomization Position = new();

	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();
	public LargeMonsterStaminaComponentCustomization Stamina = new();
	public LargeMonsterRageComponentCustomization Rage = new();

	public bool RenderImGui(string? parentName = "", LargeMonsterTargetedUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-targeted";

		if(ImGuiHelper.ResettableTreeNode(localization?.Targeted, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization?.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= Position.RenderImGui(customizationName, defaultCustomization?.Position);
			isChanged |= NameLabel.RenderImGui(localization?.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= Health.RenderImGui(customizationName, defaultCustomization?.Health);
			isChanged |= Stamina.RenderImGui(customizationName, defaultCustomization?.Stamina);
			isChanged |= Rage.RenderImGui(customizationName, defaultCustomization?.Rage);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterTargetedUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Settings.Reset(defaultCustomization.Settings);
		Position.Reset(defaultCustomization.Position);

		NameLabel.Reset(defaultCustomization.NameLabel);
		Health.Reset(defaultCustomization.Health);
		Stamina.Reset(defaultCustomization.Stamina);
		Rage.Reset(defaultCustomization.Rage);
	}
}