using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool? Visible = null;
	public float? Thickness = null;
	public float? Offset = null;

	private int? _styleIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public OutlineStyleEnum? Style
	{
		get => _styleIndex.HasValue ? (OutlineStyleEnum) _styleIndex.Value : null;
		set => _styleIndex = value.HasValue ? (int) value.Value : null;
	}

	public ColorCustomization Color { get; set; } = new();

	public bool RenderImGui(string? parentName = "", BarElementOutlineCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGuiHelper.ResettableTreeNode(localization?.Outline, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization?.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.Thickness}##{customizationName}", ref Thickness, 0.1f, 0, 1024f, "%.1f", defaultCustomization?.Thickness);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization?.Offset}##{customizationName}", ref Offset, 0.1f, -1024f, 1024f, "%.1f", defaultCustomization?.Offset);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization?.Style}##{customizationName}", ref _styleIndex, localizationHelper.OutlineStyles, defaultCustomization?._styleIndex);
			isChanged |= Color.RenderImGui(customizationName, defaultCustomization?.Color);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementOutlineCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Thickness = defaultCustomization.Thickness;
		Offset = defaultCustomization.Offset;
		Style = defaultCustomization.Style;
	}
}