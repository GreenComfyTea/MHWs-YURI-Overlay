using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterRageComponentCustomization : Customization
{
	public bool? Visible = null;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();
	public LabelElementCustomization TimerLabel = new();
	public BarElementCustomization TimerBar = new();

	public bool RenderImGui(string parentName = "", LargeMonsterRageComponentCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-rage";

		if(ImGuiHelper.ResettableTreeNode(localization.Rage, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, $"{customizationName}-value-label", defaultCustomization?.ValueLabel);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, $"{customizationName}-percentage-label", defaultCustomization?.PercentageLabel);
			isChanged |= Bar.RenderImGui(localization.Bar, $"{customizationName}-bar", defaultCustomization?.Bar);
			isChanged |= TimerLabel.RenderImGui(localization.TimerLabel, $"{customizationName}-timer-label", defaultCustomization?.TimerLabel);
			isChanged |= TimerBar.RenderImGui(localization.TimerBar, $"{customizationName}-timer-bar", defaultCustomization?.TimerBar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterRageComponentCustomization? defaultCustomization = null)
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