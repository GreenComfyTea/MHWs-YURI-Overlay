using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonstersUpdateDelaysCustomization : Customization
{
	public float Name = 1f;
	public float MissionBeaconOffset = 1f;
	public float ModelRadius = 1f;
	public float Health = 0.1f;
	public float Stamina = 0.25f;
	public float Rage = 0.25f;
	public float DynamicList = 0.1f;
	public float StaticList = 0.1f;

	public LargeMonstersUpdateDelaysCustomization() { }

	public bool RenderImGui(string parentName = "", LargeMonstersUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monsters";

		if(ImGuiHelper.ResettableTreeNode(localization.LargeMonsters, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MissionBeaconOffset}##{customizationName}", ref MissionBeaconOffset, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.MissionBeaconOffset);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref ModelRadius, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.ModelRadius);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Health}##{customizationName}", ref Health, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Health);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Stamina}##{customizationName}", ref Stamina, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Stamina);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Rage}##{customizationName}", ref Rage, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Rage);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.DynamicList}##{customizationName}", ref DynamicList, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.DynamicList);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.StaticList}##{customizationName}", ref StaticList, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.StaticList);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonstersUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Name = defaultCustomization.Name;
		MissionBeaconOffset = defaultCustomization.MissionBeaconOffset;
		ModelRadius = defaultCustomization.ModelRadius;
		Health = defaultCustomization.Health;
		Stamina = defaultCustomization.Stamina;
		Rage = defaultCustomization.Rage;
		DynamicList = defaultCustomization.DynamicList;
		StaticList = defaultCustomization.StaticList;
	}
}
