using System.Numerics;
using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class GradientStartColorCustomization : Customization
{
	public bool? SplitIntoTwoColors = null;

	[JsonIgnore]
	public ColorInfo? ColorInfo1 = null;

	[JsonIgnore]
	public ColorInfo? ColorInfo2 = null;

	public string? _1
	{
		get => ColorInfo1?.RgbaHex;
		set
		{
			if(value is null)
			{
				ColorInfo1 = null;
				return;
			}

			ColorInfo1 ??= new ColorInfo();
			ColorInfo1.RgbaHex = value;
		}
	}

	public string? _2
	{
		get => ColorInfo2?.RgbaHex;
		set
		{
			if(value is null)
			{
				ColorInfo2 = null;
				return;
			}

			ColorInfo2 ??= new ColorInfo();
			ColorInfo2.RgbaHex = value;
		}
	}

	public bool RenderImGui(string? parentName = "", GradientStartColorCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-start";

		if(ImGuiHelper.ResettableTreeNode(localization.Start, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox(localization.SplitIntoTwoColors, ref SplitIntoTwoColors, defaultCustomization?.SplitIntoTwoColors);

			if(SplitIntoTwoColors == false)
			{
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}", ref ColorInfo1, defaultCustomization?.ColorInfo1);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					ColorInfo1 ??= new ColorInfo();
					ColorInfo1.Vector = ColorInfo1.vector;

					ColorInfo2 ??= new ColorInfo();
					ColorInfo2.Vector = ColorInfo2.vector;
				}

				ImGui.TreePop();

				return isChanged;
			}

			if(ImGui.TreeNode($"{localization._1}##{customizationName}"))
			{
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}-1", ref ColorInfo1, defaultCustomization?.ColorInfo1);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					ColorInfo1 ??= new ColorInfo();
					ColorInfo1.Vector = ColorInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization._2}##{customizationName}"))
			{
				var isStart2Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}-2", ref ColorInfo2, defaultCustomization?.ColorInfo2);
				isChanged |= isStart2Changed;

				if(isStart2Changed)
				{
					ColorInfo2 ??= new ColorInfo();
					ColorInfo2.Vector = ColorInfo2.vector;
				}

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GradientStartColorCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		SplitIntoTwoColors = defaultCustomization.SplitIntoTwoColors;
		_1 = defaultCustomization._1;
		_2 = defaultCustomization._2;
	}
}