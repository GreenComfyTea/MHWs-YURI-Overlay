using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class SpacingCustomization : Customization
{
	public float? X = null;
	public float? Y = null;

	public bool RenderImGui(string? parentName = "", SpacingCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-spacing";

		if(ImGuiHelper.ResettableTreeNode(localization.Spacing, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##{customizationName}", ref X, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##{customizationName}", ref Y, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.Y);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SpacingCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
	}
}