using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class AnchoredPositionCustomization : Customization
{
	public float? X = null;
	public float? Y = null;

	private int? _anchorIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AnchorEnum? Anchor
	{
		get => _anchorIndex.HasValue ? (AnchorEnum) _anchorIndex.Value : null;
		set => _anchorIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool RenderImGui(string? parentName = "", AnchoredPositionCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-anchored-position";

		if(ImGuiHelper.ResettableTreeNode(localization.Position, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##{customizationName}", ref X, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##{customizationName}", ref Y, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.Y);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Anchor}##{customizationName}", ref _anchorIndex, localizationHelper.Anchors, defaultCustomization?._anchorIndex);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(AnchoredPositionCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
		_anchorIndex = defaultCustomization._anchorIndex;
	}
}