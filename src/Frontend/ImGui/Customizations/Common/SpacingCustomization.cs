using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SpacingCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;

	public SpacingCustomization() { }

	public bool RenderImGui(string parentName = "", SpacingCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-spacing";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Spacing}##${customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, -4096f, 4096f, "%.1f", defaultCustomization?.Y);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SpacingCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
	}
}
