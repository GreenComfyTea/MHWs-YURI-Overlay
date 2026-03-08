using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterMapPinUiSettingsCustomization : Customization
{
	public bool? RenderDeadMonster;
	public bool? RenderTargetedMonster;
	public bool? RenderNonTargetedMonsters;
	public bool? RenderPinnedMonster;
	public bool? RenderNonPinnedMonsters;

	public bool RenderImGui(string? parentName = "", LargeMonsterMapPinUiSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.RenderDeadMonster}##{customizationName}", ref this.RenderDeadMonster, defaultCustomization?.RenderDeadMonster);

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

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterMapPinUiSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.RenderDeadMonster = defaultCustomization.RenderDeadMonster;
		this.RenderTargetedMonster = defaultCustomization.RenderTargetedMonster;
		this.RenderNonTargetedMonsters = defaultCustomization.RenderNonTargetedMonsters;
		this.RenderPinnedMonster = defaultCustomization.RenderPinnedMonster;
		this.RenderNonPinnedMonsters = defaultCustomization.RenderNonPinnedMonsters;
	}
}