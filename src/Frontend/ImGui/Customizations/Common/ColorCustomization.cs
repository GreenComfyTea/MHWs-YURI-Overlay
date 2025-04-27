using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo ColorInfo = new();

	public string Color
	{
		get => ColorInfo.RgbaHex;
		set => ColorInfo.RgbaHex = value;
	}

	public bool RenderImGui(string parentName = "", ColorCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-color";

		if(ImGuiHelper.ResettableTreeNode(localization.Color, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			var isColorChanged = ImGuiHelper.ResettableColorPicker4($"##${customizationName}", ref ColorInfo.vector, defaultCustomization?.ColorInfo.vector);
			isChanged |= isColorChanged;
			if(isColorChanged) ColorInfo.Vector = ColorInfo.vector;

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(ColorCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ColorInfo.Vector = defaultCustomization.ColorInfo.Vector;
	}
}