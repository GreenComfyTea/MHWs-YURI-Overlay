using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PerformanceCustomization : Customization
{
	public float UpdateDelay = 0.1f;
	public bool CalculationCaching = true;

	public PerformanceCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-performance";

		if(ImGui.TreeNode($"{localization.Performance}##${customizationName}"))
		{
			//isChanged |= ImGui.DragFloat($"{localization.UpdateDelaySeconds}##{customizationName}", ref UpdateDelay, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.Checkbox($"{localization.CalculationCaching}##{customizationName}", ref CalculationCaching);

			ImGui.TreePop();
		}

		return isChanged;
	}
}

