using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LabelElementSettingsCustomization : Customization
{
	private int? _alignmentIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AnchorEnum? Alignment
	{
		get => this._alignmentIndex.HasValue ? (AnchorEnum) this._alignmentIndex.Value : null;
		set => this._alignmentIndex = value.HasValue ? (int) value.Value : null;
	}

	public float? FontSize;
	public float? MaxWidth;

	public bool RenderImGui(string? parentName = "", LabelElementSettingsCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Alignment}##{customizationName}", ref this._alignmentIndex, localizationHelper.Anchors,
				defaultCustomization?._alignmentIndex);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.FontSize}##{customizationName}", ref this.FontSize, 0.1f, 1f, 128f, "%.1f", defaultCustomization?.FontSize);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.MaxWidth}##{customizationName}", ref this.MaxWidth, 0.1f, 0f, 4096f, "%.1f", defaultCustomization?.MaxWidth);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementSettingsCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Alignment = defaultCustomization.Alignment;
		this.FontSize = defaultCustomization.FontSize;
		this.MaxWidth = defaultCustomization.MaxWidth;
	}
}