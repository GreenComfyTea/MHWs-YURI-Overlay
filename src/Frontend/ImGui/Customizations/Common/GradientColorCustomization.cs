using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GradientColorCustomization : Customization
{
	public GradientStartColorCustomization Start = new();
	public GradientEndColorCustomization End = new();

	public GradientColorCustomization() { }


	public bool RenderImGui(string parentName = "", string name = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		if(ImGui.TreeNode($"{name}##${customizationName}"))
		{
			isChanged |= Start.RenderImGui(customizationName);
			isChanged |= End.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		return RenderImGui(parentName, localization.Color);
	}
}
