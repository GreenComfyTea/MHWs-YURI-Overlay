using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiCustomization : Customization
{
	public bool? Enabled;

	public LargeMonsterDynamicUiCustomization Dynamic = new();
	public LargeMonsterStaticUiCustomization Static = new();
	public LargeMonsterTargetedUiCustomization Targeted = new();
	public LargeMonsterMapPinUiCustomization MapPin = new();

	public bool RenderImGui(string? parentName = "", LargeMonsterUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		if(ImGuiHelper.ResettableTreeNode(localization.LargeMonstersUI, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref this.Enabled, defaultCustomization?.Enabled);

			isChanged |= this.Dynamic.RenderImGui(customizationName, defaultCustomization?.Dynamic);
			isChanged |= this.Static.RenderImGui(customizationName, defaultCustomization?.Static);
			isChanged |= this.Targeted.RenderImGui(customizationName, defaultCustomization?.Targeted);
			isChanged |= this.MapPin.RenderImGui(customizationName, defaultCustomization?.MapPin);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Enabled = defaultCustomization.Enabled;
		this.Dynamic.Reset(defaultCustomization.Dynamic);
		this.Static.Reset(defaultCustomization.Static);
		this.Targeted.Reset(defaultCustomization.Targeted);
		this.MapPin.Reset(defaultCustomization.MapPin);
	}
}