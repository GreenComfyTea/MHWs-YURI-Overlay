using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiSortingCustomization : Customization
{
	public bool? ReversedOrder = null;

	private int? _typeIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public DamageMeterSortingEnum? Type
	{
		get => _typeIndex.HasValue ? (DamageMeterSortingEnum) _typeIndex.Value : null;
		set => _typeIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _localPlayerPriorityIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? LocalPlayerPriority
	{
		get => _localPlayerPriorityIndex.HasValue ? (PriorityEnum) _localPlayerPriorityIndex.Value : null;
		set => _localPlayerPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool RenderImGui(string? parentName = "", DamageMeterStaticUiSortingCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-sorting";

		if(ImGuiHelper.ResettableTreeNode($"{localization.Sorting}##{customizationName}", customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ReversedOrder}##{customizationName}", ref ReversedOrder, defaultCustomization?.ReversedOrder);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Type}##{customizationName}", ref _typeIndex, localizationHelper.DamageMeterSortings, defaultCustomization?._typeIndex);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.LocalPlayerPriority}##{customizationName}", ref _localPlayerPriorityIndex, localizationHelper.Priorities, defaultCustomization?._localPlayerPriorityIndex);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(DamageMeterStaticUiSortingCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ReversedOrder = defaultCustomization.ReversedOrder;
		Type = defaultCustomization.Type;
		LocalPlayerPriority = defaultCustomization.LocalPlayerPriority;
	}
}