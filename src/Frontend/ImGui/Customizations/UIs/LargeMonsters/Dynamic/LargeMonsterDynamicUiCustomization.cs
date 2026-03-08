using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUiCustomization : Customization
{
	public bool? Enabled;
	public LargeMonsterDynamicUiSettingsCustomization Settings = new();
	public WorldOffsetCustomization WorldOffset = new();
	public OffsetCustomization Offset = new();

	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();
	public LargeMonsterStaminaComponentCustomization Stamina = new();
	public LargeMonsterRageComponentCustomization Rage = new();

	public bool RenderImGui(string parentName = "", LargeMonsterDynamicUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGuiHelper.ResettableTreeNode(localization.Dynamic, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref this.Enabled, defaultCustomization?.Enabled);

			isChanged |= this.Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= this.WorldOffset.RenderImGui(customizationName, defaultCustomization?.WorldOffset);
			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= this.Health.RenderImGui(customizationName, defaultCustomization?.Health);
			isChanged |= this.Stamina.RenderImGui(customizationName, defaultCustomization?.Stamina);
			isChanged |= this.Rage.RenderImGui(customizationName, defaultCustomization?.Rage);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterDynamicUiCustomization? defaultCustomization = null)
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
		this.Health.Reset(defaultCustomization.Health);
		this.Stamina.Reset(defaultCustomization.Stamina);
		this.Rage.Reset(defaultCustomization.Rage);
	}
}