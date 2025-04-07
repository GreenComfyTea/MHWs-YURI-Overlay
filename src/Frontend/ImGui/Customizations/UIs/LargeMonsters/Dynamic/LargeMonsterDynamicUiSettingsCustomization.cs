using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool RenderDeadMonsters = false;
	public bool RenderHighlightedMonster = true;
	public bool RenderNonHighlightedMonsters = true;

	public bool AddMissionBeaconOffsetToWorldOffset = false;
	public bool AddModelRadiusToWorldOffsetY = true;

	public bool OpacityFalloff = true;
	public float MaxDistance = 200f;

	public LargeMonsterDynamicUiSettingsCustomization() { }

	public bool RenderImGui(string parentName = "", LargeMonsterDynamicUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderHighlightedMonster}##{customizationName}", ref RenderHighlightedMonster, defaultCustomizations?.RenderHighlightedMonster);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonHighlightedMonsters}##{customizationName}", ref RenderNonHighlightedMonsters, defaultCustomizations?.RenderNonHighlightedMonster);
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
		RenderHighlightedMonster = defaultCustomization.RenderHighlightedMonster;
		RenderNonHighlightedMonsters = defaultCustomization.RenderNonHighlightedMonsters;

		AddMissionBeaconOffsetToWorldOffset = defaultCustomization.AddMissionBeaconOffsetToWorldOffset;
		AddModelRadiusToWorldOffsetY = defaultCustomization.AddModelRadiusToWorldOffsetY;

		OpacityFalloff = defaultCustomization.OpacityFalloff;
		MaxDistance = defaultCustomization.MaxDistance;
	}
}
