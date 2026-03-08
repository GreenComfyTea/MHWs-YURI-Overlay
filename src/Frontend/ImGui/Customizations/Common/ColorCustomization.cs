using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class ColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo? ColorInfo;

	public string? Color
	{
		get => this.ColorInfo?.RgbaHex;
		set
		{
			if(value is null)
			{
				this.ColorInfo = null;

				return;
			}

			this.ColorInfo ??= new ColorInfo();
			this.ColorInfo.RgbaHex = value;
		}
	}

	public bool RenderImGui(string? parentName = "", ColorCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-color";

		if(ImGuiHelper.ResettableTreeNode(localization.Color, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			var isColorChanged = ImGuiHelper.ResettableColorPicker4($"##{customizationName}", ref this.ColorInfo, defaultCustomization?.ColorInfo);
			isChanged |= isColorChanged;

			if(isColorChanged)
			{
				this.ColorInfo ??= new ColorInfo();
				this.ColorInfo.Vector = this.ColorInfo.vector;
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(ColorCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Color = defaultCustomization.Color;
	}
}