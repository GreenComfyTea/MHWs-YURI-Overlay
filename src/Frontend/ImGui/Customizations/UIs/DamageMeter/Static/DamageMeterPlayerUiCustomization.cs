using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterPlayerUiCustomization : Customization
{
	public bool Enabled = true;

	public LabelElementCustomization HunterMasterRanksLabel = new();
	public LabelElementCustomization NameLabel = new();
	public LabelElementCustomization DpsLabel = new();
	public LabelElementCustomization DamageLabel = new();
	public LabelElementCustomization DamagePercentageLabel = new();
	public BarElementCustomization DpsBar = new();
	public BarElementCustomization DamageBar = new();

	public bool RenderImGui(string visibleName, string customizationName = "player", DamageMeterPlayerUiCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode($"{visibleName}##{customizationName}", customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= HunterMasterRanksLabel.RenderImGui(localization.HunterMasterRanksLabel, $"{customizationName}-hunter-master-ranks-label", defaultCustomization?.HunterMasterRanksLabel);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= DpsLabel.RenderImGui(localization.DpsLabel, $"{customizationName}-dps-label", defaultCustomization?.DpsLabel);
			isChanged |= DamageLabel.RenderImGui(localization.DamageLabel, $"{customizationName}-damage-label", defaultCustomization?.DamageLabel);
			isChanged |= DamagePercentageLabel.RenderImGui(localization.DamagePercentageLabel, $"{customizationName}-damage-percentage-label", defaultCustomization?.DamagePercentageLabel);
			isChanged |= DpsBar.RenderImGui(localization.DpsBar, $"{customizationName}-dps-bar", defaultCustomization?.DpsBar);
			isChanged |= DamageBar.RenderImGui(localization.DamageBar, $"{customizationName}-damage-bar", defaultCustomization?.DamageBar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterPlayerUiCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		HunterMasterRanksLabel.Reset(defaultCustomization.HunterMasterRanksLabel);
		NameLabel.Reset(defaultCustomization.NameLabel);
		DpsLabel.Reset(defaultCustomization.DpsLabel);
		DamageLabel.Reset(defaultCustomization.DamageLabel);
		DamagePercentageLabel.Reset(defaultCustomization.DamagePercentageLabel);
		DpsBar.Reset(defaultCustomization.DpsBar);
		DamageBar.Reset(defaultCustomization.DamageBar);
	}
}