using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiSortingCustomization : Customization
{
	public bool? ReversedOrder;

	private int? _typeIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public DamageMeterSortingEnum? Type
	{
		get => this._typeIndex.HasValue ? (DamageMeterSortingEnum) this._typeIndex.Value : null;
		set => this._typeIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _localPlayerPriorityIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? LocalPlayerPriority
	{
		get => this._localPlayerPriorityIndex.HasValue ? (PriorityEnum) this._localPlayerPriorityIndex.Value : null;
		set => this._localPlayerPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool RenderImGui(string? parentName = "", DamageMeterStaticUiSortingCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-sorting";

		if(ImGuiHelper.ResettableTreeNode($"{localization.Sorting}##{customizationName}", customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ReversedOrder}##{customizationName}", ref this.ReversedOrder, defaultCustomization?.ReversedOrder);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Type}##{customizationName}", ref this._typeIndex, localizationHelper.DamageMeterSortings, defaultCustomization?._typeIndex);

			isChanged |= ImGuiHelper.ResettableCombo(
				$"{localization.LocalPlayerPriority}##{customizationName}",
				ref this._localPlayerPriorityIndex,
				localizationHelper.Priorities,
				defaultCustomization?._localPlayerPriorityIndex
			);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiSortingCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.ReversedOrder = defaultCustomization.ReversedOrder;
		this.Type = defaultCustomization.Type;
		this.LocalPlayerPriority = defaultCustomization.LocalPlayerPriority;
	}
}