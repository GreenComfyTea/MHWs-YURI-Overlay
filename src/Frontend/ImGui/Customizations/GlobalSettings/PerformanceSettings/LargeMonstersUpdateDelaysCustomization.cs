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

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monsters";

		if(ImGui.TreeNode($"{localization.LargeMonsters}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.Name}##{customizationName}", ref Name, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.MissionBeaconOffset}##{customizationName}", ref MissionBeaconOffset, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.ModelRadius}##{customizationName}", ref ModelRadius, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.Health}##{customizationName}", ref Health, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.Stamina}##{customizationName}", ref Stamina, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.Rage}##{customizationName}", ref Rage, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.DynamicList}##{customizationName}", ref DynamicList, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.StaticList}##{customizationName}", ref StaticList, 0.001f, 0.001f, 10f, "%.3f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
