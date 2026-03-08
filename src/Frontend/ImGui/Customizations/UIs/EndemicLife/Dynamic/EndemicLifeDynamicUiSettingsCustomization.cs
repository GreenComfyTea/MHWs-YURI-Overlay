using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class EndemicLifeDynamicUiSettingsCustomization : Customization
{
	public bool? AddModelRadiusToWorldOffsetY;

	public bool? OpacityFalloff;
	public float? MaxDistance;

	public bool RenderImGui(string? parentName = "", EndemicLifeDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox(
				$"{localization.AddModelRadiusToWorldOffsetY}##{customizationName}",
				ref this.AddModelRadiusToWorldOffsetY,
				defaultCustomization?.AddModelRadiusToWorldOffsetY
			);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.OpacityFalloff}##{customizationName}", ref this.OpacityFalloff, defaultCustomization?.OpacityFalloff);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MaxDistance}##{customizationName}", ref this.MaxDistance, 0.1f, 0, 65536f, "%.1f", defaultCustomization?.MaxDistance);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.AddModelRadiusToWorldOffsetY = defaultCustomization.AddModelRadiusToWorldOffsetY;
		this.OpacityFalloff = defaultCustomization.OpacityFalloff;
		this.MaxDistance = defaultCustomization.MaxDistance;
	}
}