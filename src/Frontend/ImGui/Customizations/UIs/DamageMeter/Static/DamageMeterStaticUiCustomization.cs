using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiCustomization : Customization
{
	public bool? Enabled;
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

		if(ImGuiHelper.ResettableTreeNode($"{localization.DamageMeterUI}##{customizationName}", customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Enabled}##{customizationName}", ref this.Enabled, defaultCustomization?.Enabled);

			isChanged |= this.Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= this.Position.RenderImGui(customizationName, defaultCustomization?.Position);
			isChanged |= this.Spacing.RenderImGui(customizationName, defaultCustomization?.Spacing);
			isChanged |= this.Sorting.RenderImGui(customizationName, defaultCustomization?.Sorting);

			isChanged |= this.LocalPlayer.RenderImGui(localization.LocalPlayer, $"{customizationName}-local-player", defaultCustomization?.LocalPlayer);
			isChanged |= this.OtherPlayers.RenderImGui(localization.OtherPlayers, $"{customizationName}-other-players", defaultCustomization?.OtherPlayers);
			isChanged |= this.SupportHunters.RenderImGui(localization.SupportHunters, $"{customizationName}-support-hunters", defaultCustomization?.SupportHunters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Enabled = defaultCustomization.Enabled;
		this.Settings.Reset(defaultCustomization.Settings);
		this.Position.Reset(defaultCustomization.Position);
		this.Spacing.Reset(defaultCustomization.Spacing);
		this.Sorting.Reset(defaultCustomization.Sorting);

		this.LocalPlayer.Reset(defaultCustomization.LocalPlayer);
		this.OtherPlayers.Reset(defaultCustomization.OtherPlayers);
		this.SupportHunters.Reset(defaultCustomization.SupportHunters);
	}
}