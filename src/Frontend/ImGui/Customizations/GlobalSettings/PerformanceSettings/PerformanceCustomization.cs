using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PerformanceCustomization : Customization
{
	public bool CalculationCaching = true;
	public UpdateDelaysCustomization UpdateDelays = new();

	public PerformanceCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-performance";

		if(ImGui.TreeNode($"{localization.Performance}##${customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.CalculationCaching}##{customizationName}", ref CalculationCaching);
			isChanged |= UpdateDelays.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}

