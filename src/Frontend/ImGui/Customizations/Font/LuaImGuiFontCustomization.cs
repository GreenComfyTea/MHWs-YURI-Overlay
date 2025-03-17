using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LuaImGuiFontCustomization : Customization
{
	public float FontScale = 0.5f;

	public LuaImGuiFontCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var changed = false;
		var customizationName = $"{parentName}-font";

		if(ImGui.TreeNode($"{localization.MenuFont}##{customizationName}"))
		{
			changed |= ImGui.DragFloat($"{localization.FontScale}##{customizationName}", ref FontScale, 0.01f, 0.001f, 16f, "%.3f");

			ImGui.TreePop();
		}

		return changed;
	}
}
