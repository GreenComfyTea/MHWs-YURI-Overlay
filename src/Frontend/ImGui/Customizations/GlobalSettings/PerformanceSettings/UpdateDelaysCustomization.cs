using ImGuiNET;

namespace YURI_Overlay;

internal sealed class UpdateDelaysCustomization : Customization
{
	public ScreenManagerUpdateDelaysCustomization ScreenManager = new();
	public PlayerManagerUpdateDelaysCustomization PlayerManager = new();
	public LargeMonstersUpdateDelaysCustomization LargeMonsters = new();

	public UpdateDelaysCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-update-delays";

		if(ImGui.TreeNode($"{localization.UpdateDelaysSeconds}##${customizationName}"))
		{
			isChanged |= ScreenManager.RenderImGui(customizationName);
			isChanged |= PlayerManager.RenderImGui(customizationName);
			isChanged |= LargeMonsters.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
