using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	[JsonIgnore]
	private int? _fillDirectionIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public FillDirectionEnum? FillDirection
	{
		get => this._fillDirectionIndex.HasValue ? (FillDirectionEnum?) this._fillDirectionIndex.Value : null;
		set => this._fillDirectionIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool? Inverted;

	public bool RenderImGui(string? parentName = "", BarElementSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo(
				$"{localization.FillDirection}##{customizationName}",
				ref this._fillDirectionIndex,
				localizationHelper.FillDirections,
				defaultCustomization?._fillDirectionIndex
			);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Inverted}##{customizationName}", ref this.Inverted, defaultCustomization?.Inverted);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.FillDirection = defaultCustomization.FillDirection;
		this.Inverted = defaultCustomization.Inverted;
	}
}