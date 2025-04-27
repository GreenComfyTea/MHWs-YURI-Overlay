using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponentCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();

	public bool RenderImGui(string parentName = "", LargeMonsterHealthComponentCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-health";

		if(ImGuiHelper.ResettableTreeNode(localization.Health, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, customizationName, defaultCustomization?.ValueLabel);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, customizationName, defaultCustomization?.PercentageLabel);
			isChanged |= Bar.RenderImGui(localization.Bar, customizationName, defaultCustomization?.Bar);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterHealthComponentCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Offset.Reset(defaultCustomization.Offset);
		ValueLabel.Reset(defaultCustomization.ValueLabel);
		PercentageLabel.Reset(defaultCustomization.PercentageLabel);
		Bar.Reset(defaultCustomization.Bar);
	}
}