using ImGuiNET;
using System.Text.Json.Serialization;

namespace YURI_Overlay;
internal sealed class AnchoredPositionCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;

	private int _anchorIndex = (int) Anchors.TopLeft;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Anchors Anchor
	{
		get => (Anchors) _anchorIndex;
		set => _anchorIndex = (int) value;
	}

	public AnchoredPositionCustomization() { }

	public bool RenderImGui(string parentName = "", AnchoredPositionCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-anchored-position";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Position}##${customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.Y);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Anchor}##{customizationName}", ref _anchorIndex, localizationHelper.Anchors, defaultCustomization?._anchorIndex);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(AnchoredPositionCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		X = defaultCustomization.X;
		Y = defaultCustomization.Y;
		_anchorIndex = defaultCustomization._anchorIndex;
	}
}
