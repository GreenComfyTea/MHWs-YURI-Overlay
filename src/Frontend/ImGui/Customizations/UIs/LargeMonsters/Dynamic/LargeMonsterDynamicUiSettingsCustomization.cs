using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool? RenderDeadMonsters;
	public bool? RenderTargetedMonster;
	public bool? RenderNonTargetedMonsters;
	public bool? RenderPinnedMonster;
	public bool? RenderNonPinnedMonsters;

	public bool? AddMissionBeaconOffsetToWorldOffset;
	public bool? AddModelRadiusToWorldOffsetY;

	public bool? OpacityFalloff;
	public float? MaxDistance;

	public bool RenderImGui(string? parentName = "", LargeMonsterDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref this.RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);

			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderTargetedMonster}##{customizationName}", ref this.RenderTargetedMonster,
				defaultCustomization?.RenderTargetedMonster);

			isChanged |= ImGuiHelper.ResettableCheckbox(
				$"{localization.RenderNonTargetedMonsters}##{customizationName}",
				ref this.RenderNonTargetedMonsters,
				defaultCustomization?.RenderNonTargetedMonsters
			);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderPinnedMonster}##{customizationName}", ref this.RenderPinnedMonster, defaultCustomization?.RenderPinnedMonster);

			isChanged |= ImGuiHelper.ResettableCheckbox(
				$"{localization.RenderNonPinnedMonsters}##{customizationName}",
				ref this.RenderNonPinnedMonsters,
				defaultCustomization?.RenderNonPinnedMonsters
			);

			isChanged |= ImGuiHelper.ResettableCheckbox(
				$"{localization.AddMissionBeaconOffsetToWorldOffset}##{customizationName}",
				ref this.AddMissionBeaconOffsetToWorldOffset,
				defaultCustomization?.AddMissionBeaconOffsetToWorldOffset
			);

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

	public void Reset(LargeMonsterDynamicUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;
		this.RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		this.RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;
		this.RenderPinnedMonster = defaultCustomization.RenderPinnedMonster;
		this.RenderNonPinnedMonsters = defaultCustomization.RenderNonPinnedMonsters;

		this.AddMissionBeaconOffsetToWorldOffset = defaultCustomization.AddMissionBeaconOffsetToWorldOffset;
		this.AddModelRadiusToWorldOffsetY = defaultCustomization.AddModelRadiusToWorldOffsetY;

		this.OpacityFalloff = defaultCustomization.OpacityFalloff;
		this.MaxDistance = defaultCustomization.MaxDistance;
	}
}