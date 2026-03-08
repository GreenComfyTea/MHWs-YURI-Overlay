using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class UpdateDelaysCustomization : Customization
{
	public ScreenManagerUpdateDelaysCustomization ScreenManager = new();
	public PlayerManagerUpdateDelaysCustomization PlayerManager = new();
	public LargeMonstersUpdateDelaysCustomization LargeMonsters = new();
	public SmallMonstersUpdateDelaysCustomization SmallMonsters = new();
	public EndemicLifeUpdateDelaysCustomization EndemicLife = new();
	public UiUpdateDelaysCustomization UIs = new();

	public bool RenderImGui(string? parentName = "", UpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-update-delays";

		if(ImGuiHelper.ResettableTreeNode(localization.UpdateDelaysSeconds, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= this.ScreenManager.RenderImGui(customizationName, defaultCustomization?.ScreenManager);
			isChanged |= this.PlayerManager.RenderImGui(customizationName, defaultCustomization?.PlayerManager);
			isChanged |= this.LargeMonsters.RenderImGui(customizationName, defaultCustomization?.LargeMonsters);
			isChanged |= this.SmallMonsters.RenderImGui(customizationName, defaultCustomization?.SmallMonsters);
			isChanged |= this.EndemicLife.RenderImGui(customizationName, defaultCustomization?.EndemicLife);
			isChanged |= this.UIs.RenderImGui(customizationName, defaultCustomization?.UIs);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(UpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.ScreenManager.Reset(defaultCustomization.ScreenManager);
		this.PlayerManager.Reset(defaultCustomization.PlayerManager);
		this.LargeMonsters.Reset(defaultCustomization.LargeMonsters);
		this.SmallMonsters.Reset(defaultCustomization.SmallMonsters);
		this.EndemicLife.Reset(defaultCustomization.EndemicLife);
		this.UIs.Reset(defaultCustomization.UIs);
	}
}