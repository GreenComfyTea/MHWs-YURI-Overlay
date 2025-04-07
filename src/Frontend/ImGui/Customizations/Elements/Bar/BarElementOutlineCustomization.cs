using ImGuiNET;
using System.Text.Json.Serialization;
namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool Visible = true;
	public float Thickness = 1f;
	public float Offset = 0f;

	private int _styleIndex = (int) OutlineStyles.Outside;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public OutlineStyles Style { get => (OutlineStyles) _styleIndex; set => _styleIndex = (int) value; }

	public ColorCustomization Color { get; set; } = new();

	public bool RenderImGui(string parentName = "", BarElementOutlineCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGuiHelper.ResettableTreeNode(localization.Outline, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Thickness}##{customizationName}", ref Thickness, 0.1f, 0, 1024f, "%.1f", defaultCustomization?.Thickness);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Offset}##{customizationName}", ref Offset, 0.1f, -1024f, 1024f, "%.1f", defaultCustomization?.Offset);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Style}##{customizationName}", ref _styleIndex, localizationHelper.OutlineStyles, defaultCustomization?._styleIndex);
			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementOutlineCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Thickness = defaultCustomization.Thickness;
		Offset = defaultCustomization.Offset;
		Style = defaultCustomization.Style;
	}
}
