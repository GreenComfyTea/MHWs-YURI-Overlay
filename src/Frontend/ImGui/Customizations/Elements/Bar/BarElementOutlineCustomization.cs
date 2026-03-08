using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool? Visible;
	public float? Thickness;
	public float? Offset;

	private int? _styleIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public OutlineStyleEnum? Style
	{
		get => this._styleIndex.HasValue ? (OutlineStyleEnum) this._styleIndex.Value : null;
		set => this._styleIndex = value.HasValue ? (int) value.Value : null;
	}

	public ColorCustomization Color { get; set; } = new();

	public bool RenderImGui(string? parentName = "", BarElementOutlineCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGuiHelper.ResettableTreeNode(localization.Outline, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref this.Visible, defaultCustomization?.Visible);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Thickness}##{customizationName}", ref this.Thickness, 0.1f, 0, 1024f, "%.1f", defaultCustomization?.Thickness);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Offset}##{customizationName}", ref this.Offset, 0.1f, -1024f, 1024f, "%.1f", defaultCustomization?.Offset);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Style}##{customizationName}", ref this._styleIndex, localizationHelper.OutlineStyles, defaultCustomization?._styleIndex);
			isChanged |= this.Color.RenderImGui(customizationName, defaultCustomization?.Color);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementOutlineCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Visible = defaultCustomization.Visible;
		this.Thickness = defaultCustomization.Thickness;
		this.Offset = defaultCustomization.Offset;
		this.Style = defaultCustomization.Style;
	}
}