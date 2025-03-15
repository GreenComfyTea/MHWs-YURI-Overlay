using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiCustomization : Customization
{
	public bool Enabled = true;
	public LargeMonsterStaticUiSettingsCustomization Settings = new();
	public AnchoredPositionCustomization Position = new();
	public SpacingCustomization Spacing = new();
	public LargeMonsterStaticUiSortingCustomization Sorting = new();


	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();
	public LargeMonsterStaminaComponentCustomization Stamina = new();
	public LargeMonsterRageComponentCustomization Rage = new();

	public LargeMonsterStaticUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-static";

		if(ImGui.TreeNode($"{localization.Static}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Enabled}##{customizationName}", ref Enabled);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= Position.RenderImGui(customizationName);
			isChanged |= Spacing.RenderImGui(customizationName);
			isChanged |= Sorting.RenderImGui(customizationName);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label");
			isChanged |= Health.RenderImGui(customizationName);
			isChanged |= Stamina.RenderImGui(customizationName);
			isChanged |= Rage.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
