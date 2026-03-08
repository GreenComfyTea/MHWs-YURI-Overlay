using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class EndemicLifeUpdateDelaysCustomization : Customization
{
	public float? Name;
	public float? ModelRadius;

	public bool RenderImGui(string? parentName = "", EndemicLifeUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-endemic-life";

		if(ImGuiHelper.ResettableTreeNode(localization.EndemicLife, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref this.Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);

			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref this.ModelRadius, 0.001f, 0.001f, 10f, "%.3f",
				defaultCustomization?.ModelRadius);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(EndemicLifeUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Name = defaultCustomization.Name;
		this.ModelRadius = defaultCustomization.ModelRadius;
	}
}