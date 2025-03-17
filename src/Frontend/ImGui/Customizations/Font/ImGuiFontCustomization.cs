using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ImGuiFontCustomization : Customization
{
	public float FontSize = 17f;
	public int HorizontalOversample = 2;
	public int VerticalOversample = 2;

	public ImGuiFontCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-font";

		if(ImGui.TreeNode($"{localization.Font}##{customizationName}"))
		{

			ImGui.Text(localization.AnyChangesToFontRequireGameRestart);

			isChanged |= ImGui.DragFloat($"{localization.FontSize}##{customizationName}", ref FontSize, 0.1f, 1f, 128f, "%.1f");
			isChanged |= ImGui.SliderInt($"{localization.HorizontalOversample}##{customizationName}", ref HorizontalOversample, 0, 8);
			isChanged |= ImGui.SliderInt($"{localization.VerticalOversample}##{customizationName}", ref VerticalOversample, 0, 8);

			ImGui.TreePop();
		}

		return isChanged;
	}
}