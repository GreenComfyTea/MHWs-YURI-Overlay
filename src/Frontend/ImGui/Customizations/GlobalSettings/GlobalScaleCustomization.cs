using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalScaleCustomization : Customization
{
	public float PositionScaleModifier = 1f;
	public float SizeScaleModifier = 1f;

	public GlobalScaleCustomization() { }

	public bool RenderImGui(string parentName = "", GlobalScaleCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.GlobalScale, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.PositionScaleModifier}##{customizationName}", ref PositionScaleModifier, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.PositionScaleModifier);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.SizeScaleModifier}##{customizationName}", ref SizeScaleModifier, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.SizeScaleModifier);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GlobalScaleCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		PositionScaleModifier = defaultCustomization.PositionScaleModifier;
		SizeScaleModifier = defaultCustomization.SizeScaleModifier;
	}
}

