using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SmallMonstersUpdateDelaysCustomization : Customization
{
	public float Name = 5f;
	public float MissionBeaconOffset = 5f;
	public float ModelRadius = 5f;
	public float Health = 0.5f;
	public float DynamicList = 0.2f;

	public bool RenderImGui(string parentName = "", SmallMonstersUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monsters";

		if(ImGuiHelper.ResettableTreeNode(localization.SmallMonsters, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MissionBeaconOffset}##{customizationName}", ref MissionBeaconOffset, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.MissionBeaconOffset);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref ModelRadius, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.ModelRadius);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Health}##{customizationName}", ref Health, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Health);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.DynamicList}##{customizationName}", ref DynamicList, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.DynamicList);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SmallMonstersUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Name = defaultCustomization.Name;
		MissionBeaconOffset = defaultCustomization.MissionBeaconOffset;
		ModelRadius = defaultCustomization.ModelRadius;
		Health = defaultCustomization.Health;
		DynamicList = defaultCustomization.DynamicList;
	}
}