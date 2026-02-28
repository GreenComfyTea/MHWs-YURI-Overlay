using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	[JsonIgnore]
	private int? _fillDirectionIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public FillDirectionEnum? FillDirection
	{
		get => _fillDirectionIndex.HasValue ? (FillDirectionEnum?) _fillDirectionIndex.Value : null;
		set => _fillDirectionIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool? Inverted = null;

	public bool RenderImGui(string? parentName = "", BarElementSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.FillDirection}##{customizationName}", ref _fillDirectionIndex, localizationHelper.FillDirections, defaultCustomization?._fillDirectionIndex);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Inverted}##{customizationName}", ref Inverted, defaultCustomization?.Inverted);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		FillDirection = defaultCustomization.FillDirection;
		Inverted = defaultCustomization.Inverted;
	}
}