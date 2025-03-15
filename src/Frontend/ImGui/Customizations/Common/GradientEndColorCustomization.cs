using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GradientEndColorCustomization : Customization
{
	public bool SplitIntoTwoColors = false;
	[JsonIgnore]
	public ColorInfo ColorInfo1 { get; set; } = new();
	public string _1
	{
		get => ColorInfo1.RgbaHex;
		set => ColorInfo1.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo ColorInfo2 { get; set; } = new();
	public string _2
	{
		get => ColorInfo2.RgbaHex;
		set => ColorInfo2.RgbaHex = value;
	}

	public GradientEndColorCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-start";

		if(ImGui.TreeNode($"{localization.End}##${customizationName}"))
		{
			isChanged |= ImGui.Checkbox(localization.SplitIntoTwoColors, ref SplitIntoTwoColors);

			if(!SplitIntoTwoColors)
			{
				var isStart1Changed = ImGui.ColorPicker4($"##${customizationName}", ref ColorInfo1.vector);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					ColorInfo1.Vector = ColorInfo1.vector;
					ColorInfo2.Vector = ColorInfo1.vector;
				}

				return isChanged;
			}

			if(ImGui.TreeNode($"{localization._1}##${customizationName}"))
			{
				var isStart1Changed = ImGui.ColorPicker4($"##${customizationName}-1", ref ColorInfo1.vector);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					ColorInfo1.Vector = ColorInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization._2}##${customizationName}"))
			{
				var isStart2Changed = ImGui.ColorPicker4($"##${customizationName}-2", ref ColorInfo2.vector);
				isChanged |= isStart2Changed;

				if(isStart2Changed)
				{
					ColorInfo2.Vector = ColorInfo2.vector;
				}

				ImGui.TreePop();
			}


			ImGui.TreePop();
		}

		return isChanged;
	}
}
