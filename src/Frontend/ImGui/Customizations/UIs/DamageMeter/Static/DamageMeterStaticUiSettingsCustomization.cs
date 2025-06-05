using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiSettingsCustomization : Customization
{
	//public bool RenderLocalPlayer = true;
	//public bool RenderOtherPlayers = true;
	//public bool RenderSupportHunters = true;

	public bool RenderImGui(string parentName = "", DamageMeterStaticUiSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode($"{localization.Settings}##{customizationName}", customizationName, ref isChanged, defaultCustomization, Reset))
		{
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderLocalPlayer}##{customizationName}", ref RenderLocalPlayer, defaultCustomization?.RenderLocalPlayer);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderOtherPlayers}##{customizationName}", ref RenderOtherPlayers, defaultCustomization?.RenderOtherPlayers);
			//isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderSupportHunters}##{customizationName}", ref RenderSupportHunters, defaultCustomization?.RenderSupportHunters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		//RenderDeadMonsters = defaultCustomization.RenderDeadMonsters;
	}
}