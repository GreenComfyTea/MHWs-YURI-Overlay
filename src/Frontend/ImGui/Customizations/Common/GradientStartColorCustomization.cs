using ImGuiNET;
using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal sealed class GradientStartColorCustomization : Customization
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

	public GradientStartColorCustomization() { }

	public bool RenderImGui(string parentName = "", GradientStartColorCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-start";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Start}##${customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox(localization.SplitIntoTwoColors, ref SplitIntoTwoColors, defaultCustomization?.SplitIntoTwoColors);

			if(!SplitIntoTwoColors)
			{
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##${customizationName}", ref ColorInfo1.vector, defaultCustomization?.ColorInfo1.vector);
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
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##${customizationName}-1", ref ColorInfo1.vector, defaultCustomization?.ColorInfo1.vector);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					ColorInfo1.Vector = ColorInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization._2}##${customizationName}"))
			{
				var isStart2Changed = ImGuiHelper.ResettableColorPicker4($"##${customizationName}-2", ref ColorInfo2.vector, defaultCustomization?.ColorInfo2.vector);
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

	public void Reset(GradientStartColorCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		SplitIntoTwoColors = defaultCustomization.SplitIntoTwoColors;
		ColorInfo1.Vector = defaultCustomization.ColorInfo1.vector;
		ColorInfo2.Vector = defaultCustomization.ColorInfo2.vector;
	}
}
