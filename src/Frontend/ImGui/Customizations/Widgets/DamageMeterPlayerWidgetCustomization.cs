using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterPlayerWidgetCustomization : Customization
{
	public DamageMeterDamageComponentCustomization Damage = new();
	public DamageMeterDpsComponentCustomization Dps = new();
	public bool? Enabled;

	public LabelElementCustomization HunterMasterRanksLabel = new();
	public LabelElementCustomization NameLabel = new();
	public OffsetCustomization Offset = new();

	public bool RenderImGui(string? visibleName, string customizationName = "player", DamageMeterPlayerWidgetCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode($"{visibleName}##{customizationName}", customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref this.Enabled, defaultCustomization?.Enabled);
			isChanged |= this.Offset.RenderImGui($"{localization.Offset}##{customizationName}", defaultCustomization?.Offset);

			isChanged |= this.HunterMasterRanksLabel.RenderImGui(
				localization.HunterMasterRanksLabel,
				$"{customizationName}-hunter-master-ranks-label",
				defaultCustomization?.HunterMasterRanksLabel
			);
			isChanged |= this.NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= this.Damage.RenderImGui(customizationName, defaultCustomization?.Damage);
			isChanged |= this.Dps.RenderImGui(customizationName, defaultCustomization?.Dps);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterPlayerWidgetCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		this.Enabled = defaultCustomization.Enabled;
		this.Offset.Reset(defaultCustomization.Offset);
		this.HunterMasterRanksLabel.Reset(defaultCustomization.HunterMasterRanksLabel);
		this.NameLabel.Reset(defaultCustomization.NameLabel);
		this.Damage.Reset(defaultCustomization.Damage);
		this.Dps.Reset(defaultCustomization.Dps);
	}
}