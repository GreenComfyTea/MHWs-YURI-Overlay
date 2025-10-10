using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GradientColorCustomization : Customization
{
	public GradientStartColorCustomization Start = new();
	public GradientEndColorCustomization End = new();

	public bool RenderImGui(string? name = "", string? parentName = "", GradientColorCustomization? defaultCustomization = null)
	{
		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		if(ImGuiHelper.ResettableTreeNode(name, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= Start.RenderImGui(customizationName, defaultCustomization?.Start);
			isChanged |= End.RenderImGui(customizationName, defaultCustomization?.End);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GradientColorCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Start.Reset(defaultCustomization.Start);
		End.Reset(defaultCustomization.End);
	}
}