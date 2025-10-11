using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LabelElementSettingsCustomization : Customization
{
	private int? _alignmentIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AnchorEnum? Alignment
	{
		get => _alignmentIndex.HasValue ? (AnchorEnum) _alignmentIndex.Value : null;
		set => _alignmentIndex = value.HasValue ? (int) value.Value : null;
	}

	public float? FontSize = null;
	public float? MaxWidth = null;

	public bool RenderImGui(string? parentName = "", LabelElementSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Alignment}##{customizationName}", ref _alignmentIndex, localizationHelper.Anchors, defaultCustomization?._alignmentIndex);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.FontSize}##{customizationName}", ref FontSize, 0.1f, 1f, 128f, "%.1f", defaultCustomization?.FontSize);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MaxWidth}##{customizationName}", ref MaxWidth, 0.1f, 0f, 4096f, "%.1f", defaultCustomization?.MaxWidth);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Alignment = defaultCustomization.Alignment;
		FontSize = defaultCustomization.FontSize;
		MaxWidth = defaultCustomization.MaxWidth;
	}
}