using ImGuiNET;
using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	[JsonIgnore]
	private int _fillDirectionIndex = (int) FillDirections.LeftToRight;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public FillDirections FillDirection { get => (FillDirections) _fillDirectionIndex; set => _fillDirectionIndex = (int) value; }

	public bool Inverted = false;

	public bool RenderImGui(string parentName = "", BarElementSettingsCustomization defaultCustomization = null)
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

	public void Reset(BarElementSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		FillDirection = defaultCustomization.FillDirection;
		Inverted = defaultCustomization.Inverted;
	}
}
