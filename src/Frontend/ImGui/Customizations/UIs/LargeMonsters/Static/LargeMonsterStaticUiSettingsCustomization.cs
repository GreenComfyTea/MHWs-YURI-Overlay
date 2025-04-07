using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool RenderDeadMonsters = false;
	public bool RenderHighlightedMonster = true;
	public bool RenderNonHighlightedMonsters = true;

	public LargeMonsterStaticUiSettingsCustomization() { }

	public bool RenderImGui(string parentName = "", LargeMonsterStaticUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters, defaultCustomization?.RenderDeadMonsters);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.renderHighlightedMonster}##{customizationName}", ref RenderHighlightedMonster, defaultCustomization?.RenderHighlightedMonster);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderNonHighlightedMonsters}##{customizationName}", ref RenderNonHighlightedMonsters, defaultCustomization?.RenderNonHighlightedMonsters);

			//isChanged |= ImGuiHelper.ResettableCombo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.SortingLocations);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;
		RenderHighlightedMonster = defaultCustomization.RenderHighlightedMonster;
		RenderNonHighlightedMonsters = defaultCustomization.RenderNonHighlightedMonsters;
	}
}
