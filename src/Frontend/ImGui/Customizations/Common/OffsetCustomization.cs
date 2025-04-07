using ImGuiNET;

namespace YURI_Overlay;

internal sealed class OffsetCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;

	public OffsetCustomization() { }

	public bool RenderImGui(string parentName = "", OffsetCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-offset";

		if(ImGuiHelper.ResettableTreeNode(localization.Offset, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.Y);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(OffsetCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
	}
}
