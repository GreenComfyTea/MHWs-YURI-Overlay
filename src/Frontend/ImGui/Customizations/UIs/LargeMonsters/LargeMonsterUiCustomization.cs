using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiCustomization : Customization
{
	public bool? Enabled = null;

	public LargeMonsterDynamicUiCustomization Dynamic = new();
	public LargeMonsterStaticUiCustomization Static = new();
	public LargeMonsterTargetedUiCustomization Targeted = new();
	public LargeMonsterMapPinUiCustomization MapPin = new();

	public bool RenderImGui(string? parentName = "", LargeMonsterUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		if(ImGuiHelper.ResettableTreeNode(localization.LargeMonstersUI, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Dynamic.RenderImGui(customizationName, defaultCustomization?.Dynamic);
			isChanged |= Static.RenderImGui(customizationName, defaultCustomization?.Static);
			isChanged |= Targeted.RenderImGui(customizationName, defaultCustomization?.Targeted);
			isChanged |= MapPin.RenderImGui(customizationName, defaultCustomization?.MapPin);

			ImGui.TreePop();
		}


		return isChanged;
	}

	public void Reset(LargeMonsterUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Dynamic.Reset(defaultCustomization.Dynamic);
		Static.Reset(defaultCustomization.Static);
		Targeted.Reset(defaultCustomization.Targeted);
		MapPin.Reset(defaultCustomization.MapPin);
	}
}