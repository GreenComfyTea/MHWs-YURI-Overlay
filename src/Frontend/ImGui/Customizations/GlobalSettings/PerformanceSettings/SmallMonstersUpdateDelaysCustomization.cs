using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class SmallMonstersUpdateDelaysCustomization : Customization
{
	public float? Name;
	public float? MissionBeaconOffset;
	public float? ModelRadius;
	public float? Health;

	public bool RenderImGui(string? parentName = "", SmallMonstersUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-small-monsters";

		if(ImGuiHelper.ResettableTreeNode(localization.SmallMonsters, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Name}##{customizationName}", ref this.Name, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Name);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.MissionBeaconOffset}##{customizationName}",
				ref this.MissionBeaconOffset,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.MissionBeaconOffset
			);

			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.ModelRadius}##{customizationName}", ref this.ModelRadius, 0.001f, 0.001f, 10f, "%.3f",
				defaultCustomization?.ModelRadius);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Health}##{customizationName}", ref this.Health, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Health);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SmallMonstersUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Name = defaultCustomization.Name;
		this.MissionBeaconOffset = defaultCustomization.MissionBeaconOffset;
		this.ModelRadius = defaultCustomization.ModelRadius;
		this.Health = defaultCustomization.Health;
	}
}