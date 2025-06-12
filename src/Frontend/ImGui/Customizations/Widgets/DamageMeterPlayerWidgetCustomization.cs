using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterPlayerWidgetCustomization : Customization
{
	public bool Enabled = true;
	public OffsetCustomization Offset = new();

	public LabelElementCustomization HunterMasterRanksLabel = new();
	public LabelElementCustomization NameLabel = new();
	public DamageMeterDamageComponentCustomization Damage = new();
	public DamageMeterDpsComponentCustomization DPS = new();

	public bool RenderImGui(string visibleName, string customizationName = "player", DamageMeterPlayerWidgetCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode($"{visibleName}##{customizationName}", customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);
			isChanged |= Offset.RenderImGui($"{localization.Offset}##{customizationName}", defaultCustomization?.Offset);

			isChanged |= HunterMasterRanksLabel.RenderImGui(localization.HunterMasterRanksLabel, $"{customizationName}-hunter-master-ranks-label", defaultCustomization?.HunterMasterRanksLabel);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= Damage.RenderImGui(customizationName, defaultCustomization?.Damage);
			isChanged |= DPS.RenderImGui(customizationName, defaultCustomization?.DPS);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterPlayerWidgetCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Offset.Reset(defaultCustomization.Offset);
		HunterMasterRanksLabel.Reset(defaultCustomization.HunterMasterRanksLabel);
		NameLabel.Reset(defaultCustomization.NameLabel);
		Damage.Reset(defaultCustomization.Damage);
		DPS.Reset(defaultCustomization.DPS);
	}
}