using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterTargetedUiSettingsCustomization : Customization
{
	public bool RenderDeadMonster = false;
	public bool RenderTargetedMonster = true;
	public bool RenderNonTargetedMonsters = true;

	public bool RenderImGui(string parentName = "", LargeMonsterTargetedUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonster}##{customizationName}", ref RenderDeadMonster, defaultCustomization?.RenderDeadMonster);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterTargetedUiSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonster = defaultCustomization.RenderDeadMonster;
		RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;
	}
}