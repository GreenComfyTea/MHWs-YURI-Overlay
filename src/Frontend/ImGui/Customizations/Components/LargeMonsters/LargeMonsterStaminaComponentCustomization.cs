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

	public bool RenderImGui(string parentName = "", LargeMonsterStaminaComponentCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-stamina";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Stamina}##{customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, customizationName, defaultCustomization?.ValueLabel);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, customizationName, defaultCustomization?.PercentageLabel);
			isChanged |= Bar.RenderImGui(localization.Bar, customizationName, defaultCustomization?.Bar);
			isChanged |= TimerLabel.RenderImGui(localization.TimerLabel, customizationName, defaultCustomization?.TimerLabel);
			isChanged |= TimerBar.RenderImGui(localization.TimerBar, customizationName, defaultCustomization?.TimerBar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaminaComponentCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Offset.Reset(defaultCustomization.Offset);
		ValueLabel.Reset(defaultCustomization.ValueLabel);
		PercentageLabel.Reset(defaultCustomization.PercentageLabel);
		Bar.Reset(defaultCustomization.Bar);
		TimerLabel.Reset(defaultCustomization.TimerLabel);
		TimerBar.Reset(defaultCustomization.TimerBar);
	}
}
