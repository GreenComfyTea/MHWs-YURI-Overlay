using ImGuiNET;

namespace YURI_Overlay;

internal sealed class UpdateDelaysCustomization : Customization
{
	public ScreenManagerUpdateDelaysCustomization ScreenManager = new();
	public PlayerManagerUpdateDelaysCustomization PlayerManager = new();
	public LargeMonstersUpdateDelaysCustomization LargeMonsters = new();
	public SmallMonstersUpdateDelaysCustomization SmallMonsters = new();

	public UpdateDelaysCustomization() { }

	public bool RenderImGui(string parentName = "", UpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-update-delays";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.UpdateDelaysSeconds}##${customizationName}"))
		{
			isChanged |= ScreenManager.RenderImGui(customizationName, defaultCustomization?.ScreenManager);
			isChanged |= PlayerManager.RenderImGui(customizationName, defaultCustomization?.PlayerManager);
			isChanged |= LargeMonsters.RenderImGui(customizationName, defaultCustomization?.LargeMonsters);
			isChanged |= SmallMonsters.RenderImGui(customizationName, defaultCustomization?.SmallMonsters);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(UpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ScreenManager.Reset(defaultCustomization.ScreenManager);
		PlayerManager.Reset(defaultCustomization.PlayerManager);
		LargeMonsters.Reset(defaultCustomization.LargeMonsters);
		SmallMonsters.Reset(defaultCustomization.SmallMonsters);
	}
}
