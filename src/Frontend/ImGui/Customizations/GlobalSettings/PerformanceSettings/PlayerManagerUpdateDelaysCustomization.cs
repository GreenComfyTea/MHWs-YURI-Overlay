using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PlayerManagerUpdateDelaysCustomization : Customization
{
	public float Update = 1f;

	public PlayerManagerUpdateDelaysCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-player-manager";

		if(ImGui.TreeNode($"{localization.PlayerManager}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.Update}##{customizationName}", ref Update, 0.001f, 0.001f, 10f, "%.3f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
