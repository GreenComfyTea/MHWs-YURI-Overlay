using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUiSortingCustomization : Customization
{
	public bool? ReversedOrder;

	private int? _typeIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public SortingEnum? Type
	{
		get => _typeIndex.HasValue ? (SortingEnum) _typeIndex : null;
		set => _typeIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _targetedMonsterPriorityIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? TargetedMonsterPriority
	{
		get => _targetedMonsterPriorityIndex.HasValue ? (PriorityEnum) _targetedMonsterPriorityIndex : null;
		set => _targetedMonsterPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _pinnedMonsterPriorityIndex = null;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? PinnedMonsterPriority
	{
		get => _pinnedMonsterPriorityIndex.HasValue ? (PriorityEnum) _pinnedMonsterPriorityIndex.Value : null;
		set => _pinnedMonsterPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool RenderImGui(string? parentName = "", LargeMonsterStaticUiSortingCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization?.Sorting, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization?.ReversedOrder}##{customizationName}", ref ReversedOrder, defaultCustomization?.ReversedOrder);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization?.Type}##{customizationName}", ref _typeIndex, localizationHelper.Sortings, defaultCustomization?._typeIndex);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization?.TargetedMonsterPriority}##{customizationName}", ref _targetedMonsterPriorityIndex, localizationHelper.Priorities, defaultCustomization?._targetedMonsterPriorityIndex);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization?.PinnedMonsterPriority}##{customizationName}", ref _pinnedMonsterPriorityIndex, localizationHelper.Priorities, defaultCustomization?._pinnedMonsterPriorityIndex);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSortingCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ReversedOrder = defaultCustomization.ReversedOrder;
		Type = defaultCustomization.Type;
		TargetedMonsterPriority = defaultCustomization.TargetedMonsterPriority;
		PinnedMonsterPriority = defaultCustomization.PinnedMonsterPriority;
	}
}