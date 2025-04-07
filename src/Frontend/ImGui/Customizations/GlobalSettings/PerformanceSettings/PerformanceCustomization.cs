using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PerformanceCustomization : Customization
{
	public bool CalculationCaching = true;
	public UpdateDelaysCustomization UpdateDelays = new();

	public PerformanceCustomization() { }

	public bool RenderImGui(string parentName = "", PerformanceCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-performance";

		if(ImGuiHelper.ResettableTreeNode(localization.Performance, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.CalculationCaching}##{customizationName}", ref CalculationCaching, defaultCustomization?.CalculationCaching);
			isChanged |= UpdateDelays.RenderImGui(customizationName, defaultCustomization?.UpdateDelays);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PerformanceCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		CalculationCaching = defaultCustomization.CalculationCaching;
		UpdateDelays.Reset(defaultCustomization.UpdateDelays);
	}
}

