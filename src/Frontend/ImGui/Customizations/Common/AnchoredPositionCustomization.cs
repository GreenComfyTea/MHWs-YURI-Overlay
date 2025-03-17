using System.Text.Json.Serialization;

using ImGuiNET;

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

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-anchored-position";

		if(ImGui.TreeNode($"{localization.Position}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, -8192f, 8192f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, -8192f, 8192f, "%.1f");

			isChanged |= ImGui.Combo($"{localization.Anchor}##{customizationName}", ref _anchorIndex, localizationHelper.Anchors, localizationHelper.Anchors.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
