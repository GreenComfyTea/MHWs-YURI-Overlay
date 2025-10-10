using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PositionCustomization : Customization
{
	public float? X = null;
	public float? Y = null;

	public bool RenderImGui(string? parentName = "", PositionCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-position";

		if(ImGuiHelper.ResettableTreeNode(localization?.Position, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.X}##${customizationName}", ref X, 0.1f, 0f, 8192f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.Y}##${customizationName}", ref Y, 0.1f, 0f, 8192f, "%.1f", defaultCustomization?.Y);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PositionCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
	}
}