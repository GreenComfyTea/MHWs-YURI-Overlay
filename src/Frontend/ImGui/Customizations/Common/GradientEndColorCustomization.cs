using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class GradientEndColorCustomization : Customization
{
	public bool? SplitIntoTwoColors;

	[JsonIgnore]
	public ColorInfo? ColorInfo1;

	[JsonIgnore]
	public ColorInfo? ColorInfo2;

	public string? _1
	{
		get => this.ColorInfo1?.RgbaHex;
		set
		{
			if(value is null)
			{
				this.ColorInfo1 = null;

				return;
			}

			this.ColorInfo1 ??= new ColorInfo();
			this.ColorInfo1.RgbaHex = value;
		}
	}

	public string? _2
	{
		get => this.ColorInfo2?.RgbaHex;
		set
		{
			if(value is null)
			{
				this.ColorInfo2 = null;

				return;
			}

			this.ColorInfo2 ??= new ColorInfo();
			this.ColorInfo2.RgbaHex = value;
		}
	}

	public bool RenderImGui(string? parentName = "", GradientEndColorCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-end";

		if(ImGuiHelper.ResettableTreeNode(localization.End, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox(localization.SplitIntoTwoColors, ref this.SplitIntoTwoColors, defaultCustomization?.SplitIntoTwoColors);

			if(this.SplitIntoTwoColors == false)
			{
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}", ref this.ColorInfo1, defaultCustomization?.ColorInfo1);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					this.ColorInfo1 ??= new ColorInfo();
					this.ColorInfo1.Vector = this.ColorInfo1.vector;

					this.ColorInfo2 ??= new ColorInfo();
					this.ColorInfo2.Vector = this.ColorInfo1.vector;
				}

				ImGui.TreePop();

				return isChanged;
			}

			if(ImGui.TreeNode($"{localization._1}##{customizationName}"))
			{
				var isStart1Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}-1", ref this.ColorInfo2, defaultCustomization?.ColorInfo1);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					this.ColorInfo1 ??= new ColorInfo();
					this.ColorInfo1.Vector = this.ColorInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization._2}##{customizationName}"))
			{
				var isStart2Changed = ImGuiHelper.ResettableColorPicker4($"##{customizationName}-2", ref this.ColorInfo2, defaultCustomization?.ColorInfo2);
				isChanged |= isStart2Changed;

				if(isStart2Changed)
				{
					this.ColorInfo2 ??= new ColorInfo();
					this.ColorInfo2.Vector = this.ColorInfo2.vector;
				}

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GradientEndColorCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.SplitIntoTwoColors = defaultCustomization.SplitIntoTwoColors;
		this._1 = defaultCustomization._1;
		this._2 = defaultCustomization._2;
	}
}