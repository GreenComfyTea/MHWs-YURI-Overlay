using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonstersUpdateDelaysCustomization : Customization
{
	public float? Name;
	public float? MissionBeaconOffset;
	public float? ModelRadius;
	public float? Health;
	public float? Stamina;
	public float? Rage;
	public float? MapPin;

	public bool RenderImGui(string? parentName = "", LargeMonstersUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monsters";

		if(ImGuiHelper.ResettableTreeNode(localization.LargeMonsters, customizationName, ref isChanged, defaultCustomization, this.Reset))
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
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Stamina}##{customizationName}", ref this.Stamina, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Stamina);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Rage}##{customizationName}", ref this.Rage, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Rage);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MapPin}##{customizationName}", ref this.MapPin, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.MapPin);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonstersUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Name = defaultCustomization.Name;
		this.MissionBeaconOffset = defaultCustomization.MissionBeaconOffset;
		this.ModelRadius = defaultCustomization.ModelRadius;
		this.Health = defaultCustomization.Health;
		this.Stamina = defaultCustomization.Stamina;
		this.Rage = defaultCustomization.Rage;
		this.MapPin = defaultCustomization.MapPin;
	}
}