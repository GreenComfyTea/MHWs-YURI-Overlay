using ImGuiNET;

namespace YURI_Overlay;

internal sealed class WorldOffsetCustomization : Customization
{
	public float? X = null;
	public float? Y = null;
	public float? Z = null;

	public bool RenderImGui(string? parentName = "", WorldOffsetCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-world-offset";

		if(ImGuiHelper.ResettableTreeNode(localization?.WorldOffset, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.X}##${customizationName}", ref X, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.Y}##${customizationName}", ref Y, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.Y);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.Z}##${customizationName}", ref Z, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.Z);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(WorldOffsetCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
		Z = defaultCustomization.Z;
	}
}