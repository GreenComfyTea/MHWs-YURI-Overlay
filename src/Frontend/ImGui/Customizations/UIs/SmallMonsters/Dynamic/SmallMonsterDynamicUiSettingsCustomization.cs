using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SmallMonsterDynamicUiSettingsCustomization : Customization
{
	public bool? RenderDeadMonsters = null;

	public bool? AddMissionBeaconOffsetToWorldOffset = null;
	public bool? AddModelRadiusToWorldOffsetY = null;

	public bool? OpacityFalloff = null;
	public float? MaxDistance = null;

	public bool RenderImGui(string? parentName = "", SmallMonsterDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.AddMissionBeaconOffsetToWorldOffset}##{customizationName}", ref AddMissionBeaconOffsetToWorldOffset, defaultCustomization?.AddMissionBeaconOffsetToWorldOffset);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.AddModelRadiusToWorldOffsetY}##{customizationName}", ref AddModelRadiusToWorldOffsetY, defaultCustomization?.AddModelRadiusToWorldOffsetY);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.OpacityFalloff}##{customizationName}", ref OpacityFalloff, defaultCustomization?.OpacityFalloff);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MaxDistance}##{customizationName}", ref MaxDistance, 0.1f, 0, 65536f, "%.1f", defaultCustomization?.MaxDistance);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SmallMonsterDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;

		AddMissionBeaconOffsetToWorldOffset = defaultCustomization.AddMissionBeaconOffsetToWorldOffset;
		AddModelRadiusToWorldOffsetY = defaultCustomization.AddModelRadiusToWorldOffsetY;
		OpacityFalloff = defaultCustomization.OpacityFalloff;
		MaxDistance = defaultCustomization.MaxDistance;
	}
}