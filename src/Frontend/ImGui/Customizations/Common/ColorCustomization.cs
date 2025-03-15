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

	public ColorCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-color";

		if(ImGui.TreeNode($"{localization.Color}##${customizationName}"))
		{
			var isColorChanged = ImGui.ColorPicker4($"##${customizationName}", ref ColorInfo.vector);
			isChanged |= isColorChanged;
			if(isColorChanged)
			{
				ColorInfo.Vector = ColorInfo.vector;
			}

			ImGui.TreePop();
		}

		return isChanged;
	}
}
