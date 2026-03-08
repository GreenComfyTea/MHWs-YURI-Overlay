using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class PerformanceCustomization : Customization
{
	public bool? CalculationCaching;
	public UpdateDelaysCustomization UpdateDelays = new();

	public bool RenderImGui(string? parentName = "", PerformanceCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-performance";

		if(ImGuiHelper.ResettableTreeNode(localization.Performance, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.CalculationCaching}##{customizationName}", ref this.CalculationCaching, defaultCustomization?.CalculationCaching);
			isChanged |= this.UpdateDelays.RenderImGui(customizationName, defaultCustomization?.UpdateDelays);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PerformanceCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.CalculationCaching = defaultCustomization.CalculationCaching;
		this.UpdateDelays.Reset(defaultCustomization.UpdateDelays);
	}
}