using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool RenderDeadMonsters = false;
	public bool RenderTargetedMonster = true;
	public bool RenderNonTargetedMonsters = true;

	public bool AddMissionBeaconOffsetToWorldOffset = false;
	public bool AddModelRadiusToWorldOffsetY = true;

	public bool OpacityFalloff = true;
	public float MaxDistance = 200f;

	public bool RenderImGui(string parentName = "", LargeMonsterDynamicUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderTargetedMonster}##{customizationName}", ref RenderTargetedMonster, defaultCustomization?.RenderTargetedMonster);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonTargetedMonsters}##{customizationName}", ref RenderNonTargetedMonsters, defaultCustomization?.RenderNonTargetedMonsters);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.AddMissionBeaconOffsetToWorldOffset}##{customizationName}", ref AddMissionBeaconOffsetToWorldOffset, defaultCustomization?.AddMissionBeaconOffsetToWorldOffset);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.AddModelRadiusToWorldOffsetY}##{customizationName}", ref AddModelRadiusToWorldOffsetY, defaultCustomization?.AddModelRadiusToWorldOffsetY);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.OpacityFalloff}##{customizationName}", ref OpacityFalloff, defaultCustomization?.OpacityFalloff);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MaxDistance}##{customizationName}", ref MaxDistance, 0.1f, 0, 65536f, "%.1f", defaultCustomization?.MaxDistance);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterDynamicUiSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;
		RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;

		AddMissionBeaconOffsetToWorldOffset = defaultCustomization.AddMissionBeaconOffsetToWorldOffset;
		AddModelRadiusToWorldOffsetY = defaultCustomization.AddModelRadiusToWorldOffsetY;

		OpacityFalloff = defaultCustomization.OpacityFalloff;
		MaxDistance = defaultCustomization.MaxDistance;
	}
}