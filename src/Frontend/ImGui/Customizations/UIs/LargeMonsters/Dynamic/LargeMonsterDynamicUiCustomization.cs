using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiCustomization : Customization
{
	public bool Enabled = true;
	public LargeMonsterDynamicUiSettingsCustomization Settings = new();
	public WorldOffsetCustomization WorldOffset = new();
	public OffsetCustomization Offset = new();

	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();
	public LargeMonsterStaminaComponentCustomization Stamina = new();
	public LargeMonsterRageComponentCustomization Rage = new();

	public bool RenderImGui(string parentName = "", LargeMonsterDynamicUiCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGuiHelper.ResettableTreeNode(localization.Dynamic, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= WorldOffset.RenderImGui(customizationName, defaultCustomization?.WorldOffset);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label", defaultCustomization?.NameLabel);
			isChanged |= Health.RenderImGui(customizationName, defaultCustomization?.Health);
			isChanged |= Stamina.RenderImGui(customizationName, defaultCustomization?.Stamina);
			isChanged |= Rage.RenderImGui(customizationName, defaultCustomization?.Rage);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterDynamicUiCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Settings.Reset(defaultCustomization.Settings);
		WorldOffset.Reset(defaultCustomization.WorldOffset);
		Offset.Reset(defaultCustomization.Offset);

		NameLabel.Reset(defaultCustomization.NameLabel);
		Health.Reset(defaultCustomization.Health);
		Stamina.Reset(defaultCustomization.Stamina);
		Rage.Reset(defaultCustomization.Rage);
	}
}