using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool RenderDeadMonsters = false;
	public bool RenderTargetedMonster = true;
	public bool RenderNonTargetedMonsters = true;

	public bool RenderImGui(string parentName = "", LargeMonsterStaticUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderTargetedMonster}##{customizationName}", ref RenderTargetedMonster, defaultCustomization?.RenderTargetedMonster);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonTargetedMonsters}##{customizationName}", ref RenderNonTargetedMonsters, defaultCustomization?.RenderNonTargetedMonsters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;
		RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;
	}
}