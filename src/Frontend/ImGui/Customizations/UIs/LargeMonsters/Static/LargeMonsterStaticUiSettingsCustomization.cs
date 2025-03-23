using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool RenderDeadMonsters = false;
	public bool RenderHighlightedMonster = true;
	public bool RenderNonHighlightedMonsters = true;


	public LargeMonsterStaticUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.Settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.RenderDeadMonsters}##{customizationName}", ref RenderDeadMonsters);
			//isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			//isChanged |= ImGui.Checkbox($"{localization.RenderNonHighlightedMonsters}##{customizationName}", ref RenderNonHighlightedMonsters);

			//isChanged |= ImGuiHelper.Combo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.SortingLocations, localizationHelper.SortingLocations.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
