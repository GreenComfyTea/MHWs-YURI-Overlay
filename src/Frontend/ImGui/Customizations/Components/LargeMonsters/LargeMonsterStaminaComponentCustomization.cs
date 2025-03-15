using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaminaComponentCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();
	public LabelElementCustomization TimerLabel = new();
	public BarElementCustomization TimerBar = new();
	public LargeMonsterStaminaComponentCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-stamina";

		if(ImGui.TreeNode($"{localization.Stamina}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Visible}##{customizationName}", ref Visible);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, customizationName);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, customizationName);
			isChanged |= Bar.RenderImGui(localization.Bar, customizationName);
			isChanged |= TimerLabel.RenderImGui(localization.TimerLabel, customizationName);
			isChanged |= TimerBar.RenderImGui(localization.TimerBar, customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
