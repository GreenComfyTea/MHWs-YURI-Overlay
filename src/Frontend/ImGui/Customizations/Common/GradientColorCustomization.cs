using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class GradientColorCustomization : Customization
{
	public GradientStartColorCustomization Start = new();
	public GradientEndColorCustomization End = new();

	public bool RenderImGui(string? name = "", string? parentName = "", GradientColorCustomization? defaultCustomization = null)
	{
		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		if(ImGuiHelper.ResettableTreeNode(name, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= this.Start.RenderImGui(customizationName, defaultCustomization?.Start);
			isChanged |= this.End.RenderImGui(customizationName, defaultCustomization?.End);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GradientColorCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Start.Reset(defaultCustomization.Start);
		this.End.Reset(defaultCustomization.End);
	}
}