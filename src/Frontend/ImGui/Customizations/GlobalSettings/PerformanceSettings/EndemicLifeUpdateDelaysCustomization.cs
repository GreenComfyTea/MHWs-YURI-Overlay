using ImGuiNET;

namespace YURI_Overlay;

internal sealed class EndemicLifeUpdateDelaysCustomization : Customization
{
	public float Name = 5f;
	public float ModelRadius = 5f;
	public float DynamicList = 0.2f;

	public bool RenderImGui(string parentName = "", EndemicLifeUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monsters";

		if(ImGuiHelper.ResettableTreeNode(localization.SmallMonsters, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref ModelRadius, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.ModelRadius);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.DynamicList}##{customizationName}", ref DynamicList, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.DynamicList);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Name = defaultCustomization.Name;
		ModelRadius = defaultCustomization.ModelRadius;
		DynamicList = defaultCustomization.DynamicList;
	}
}