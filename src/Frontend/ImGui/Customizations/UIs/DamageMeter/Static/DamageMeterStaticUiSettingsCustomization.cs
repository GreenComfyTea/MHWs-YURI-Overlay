using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiSettingsCustomization : Customization
{
	public bool? RenderLocalPlayer;
	public bool? RenderOtherPlayers;
	public bool? RenderSupportHunters;

	public bool RenderImGui(string? parentName = "", DamageMeterStaticUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode($"{localization.Settings}##{customizationName}", customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderLocalPlayer}##{customizationName}", ref this.RenderLocalPlayer, defaultCustomization?.RenderLocalPlayer);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderOtherPlayers}##{customizationName}", ref this.RenderOtherPlayers, defaultCustomization?.RenderOtherPlayers);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderSupportHunters}##{customizationName}", ref this.RenderSupportHunters, defaultCustomization?.RenderSupportHunters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.RenderLocalPlayer = defaultCustomization.RenderLocalPlayer;
		this.RenderOtherPlayers = defaultCustomization.RenderOtherPlayers;
		this.RenderSupportHunters = defaultCustomization.RenderSupportHunters;
	}
}