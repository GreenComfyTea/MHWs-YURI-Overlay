using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterMapPinUiSettingsCustomization : Customization
{
	public bool? RenderDeadMonster = null;
	public bool? RenderTargetedMonster = null;
	public bool? RenderNonTargetedMonsters = null;
	public bool? RenderPinnedMonster = null;
	public bool? RenderNonPinnedMonsters = null;

	public bool RenderImGui(string? parentName = "", LargeMonsterMapPinUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonster}##{customizationName}", ref RenderDeadMonster, defaultCustomization?.RenderDeadMonster);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderTargetedMonster}##{customizationName}", ref RenderTargetedMonster, defaultCustomization?.RenderTargetedMonster);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonTargetedMonsters}##{customizationName}", ref RenderNonTargetedMonsters, defaultCustomization?.RenderNonTargetedMonsters);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderPinnedMonster}##{customizationName}", ref RenderPinnedMonster, defaultCustomization?.RenderPinnedMonster);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonPinnedMonsters}##{customizationName}", ref RenderNonPinnedMonsters, defaultCustomization?.RenderNonPinnedMonsters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterMapPinUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonster = defaultCustomization.RenderDeadMonster;
		RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;
		RenderPinnedMonster = defaultCustomization.RenderPinnedMonster;
		RenderNonPinnedMonsters = defaultCustomization.RenderNonPinnedMonsters;
	}
}