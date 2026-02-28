using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiCustomization : Customization
{
	public bool? Enabled = null;
	public DamageMeterStaticUiSettingsCustomization Settings = new();
	public AnchoredPositionCustomization Position = new();
	public SpacingCustomization Spacing = new();
	public DamageMeterStaticUiSortingCustomization Sorting = new();

	public DamageMeterPlayerWidgetCustomization LocalPlayer = new();
	public DamageMeterPlayerWidgetCustomization OtherPlayers = new();
	public DamageMeterPlayerWidgetCustomization SupportHunters = new();

	public bool RenderImGui(string? parentName = "", DamageMeterStaticUiCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}";

		if(ImGuiHelper.ResettableTreeNode($"{localization.DamageMeterUI}##{customizationName}", customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref Enabled, defaultCustomization?.Enabled);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= Position.RenderImGui(customizationName, defaultCustomization?.Position);
			isChanged |= Spacing.RenderImGui(customizationName, defaultCustomization?.Spacing);
			isChanged |= Sorting.RenderImGui(customizationName, defaultCustomization?.Sorting);

			isChanged |= LocalPlayer.RenderImGui(localization.LocalPlayer, $"{customizationName}-local-player", defaultCustomization?.LocalPlayer);
			isChanged |= OtherPlayers.RenderImGui(localization.OtherPlayers, $"{customizationName}-other-players", defaultCustomization?.OtherPlayers);
			isChanged |= SupportHunters.RenderImGui(localization.SupportHunters, $"{customizationName}-support-hunters", defaultCustomization?.SupportHunters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Enabled = defaultCustomization.Enabled;
		Settings.Reset(defaultCustomization.Settings);
		Position.Reset(defaultCustomization.Position);
		Spacing.Reset(defaultCustomization.Spacing);
		Sorting.Reset(defaultCustomization.Sorting);

		LocalPlayer.Reset(defaultCustomization.LocalPlayer);
		OtherPlayers.Reset(defaultCustomization.OtherPlayers);
		SupportHunters.Reset(defaultCustomization.SupportHunters);
	}
}