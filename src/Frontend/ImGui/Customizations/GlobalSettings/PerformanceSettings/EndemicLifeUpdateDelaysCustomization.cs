using ImGuiNET;

namespace YURI_Overlay;

internal sealed class EndemicLifeUpdateDelaysCustomization : Customization
{
	public float? Name = null;
	public float? ModelRadius = null;

	public bool RenderImGui(string? parentName = "", EndemicLifeUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-endemic-life";

		if(ImGuiHelper.ResettableTreeNode(localization.EndemicLife, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref ModelRadius, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.ModelRadius);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Name = defaultCustomization.Name;
		ModelRadius = defaultCustomization.ModelRadius;
	}
}