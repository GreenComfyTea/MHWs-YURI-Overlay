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


	public LargeMonsterDynamicUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGui.TreeNode($"{localization.Dynamic}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Enabled}##{customizationName}", ref Enabled);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= WorldOffset.RenderImGui(customizationName);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= NameLabel.RenderImGui(localization.NameLabel, $"{customizationName}-name-label");
			isChanged |= Health.RenderImGui(customizationName);
			isChanged |= Stamina.RenderImGui(customizationName);
			isChanged |= Rage.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
