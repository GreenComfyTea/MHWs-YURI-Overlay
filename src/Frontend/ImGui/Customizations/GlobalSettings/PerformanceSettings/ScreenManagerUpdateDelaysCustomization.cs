using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ScreenManagerUpdateDelaysCustomization : Customization
{
	public float Update = 1f;

	public ScreenManagerUpdateDelaysCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-screen-manager";

		if(ImGui.TreeNode($"{localization.ScreenManager}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.Update}##{customizationName}", ref Update, 0.001f, 0.001f, 10f, "%.3f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
