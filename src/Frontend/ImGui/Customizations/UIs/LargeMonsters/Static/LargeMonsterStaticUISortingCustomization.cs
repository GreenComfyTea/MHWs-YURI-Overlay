using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUiSortingCustomization : Customization
{
	public bool? ReversedOrder;

	private int? _typeIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public SortingEnum? Type
	{
		get => this._typeIndex.HasValue ? (SortingEnum) this._typeIndex : null;
		set => this._typeIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _targetedMonsterPriorityIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? TargetedMonsterPriority
	{
		get => this._targetedMonsterPriorityIndex.HasValue ? (PriorityEnum) this._targetedMonsterPriorityIndex : null;
		set => this._targetedMonsterPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	private int? _pinnedMonsterPriorityIndex;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public PriorityEnum? PinnedMonsterPriority
	{
		get => this._pinnedMonsterPriorityIndex.HasValue ? (PriorityEnum) this._pinnedMonsterPriorityIndex.Value : null;
		set => this._pinnedMonsterPriorityIndex = value.HasValue ? (int) value.Value : null;
	}

	public bool RenderImGui(string? parentName = "", LargeMonsterStaticUiSortingCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Sorting, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ReversedOrder}##{customizationName}", ref this.ReversedOrder, defaultCustomization?.ReversedOrder);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Type}##{customizationName}", ref this._typeIndex, localizationHelper.Sortings, defaultCustomization?._typeIndex);

			isChanged |= ImGuiHelper.ResettableCombo(
				$"{localization.TargetedMonsterPriority}##{customizationName}",
				ref this._targetedMonsterPriorityIndex,
				localizationHelper.Priorities,
				defaultCustomization?._targetedMonsterPriorityIndex
			);

			isChanged |= ImGuiHelper.ResettableCombo(
				$"{localization.PinnedMonsterPriority}##{customizationName}",
				ref this._pinnedMonsterPriorityIndex,
				localizationHelper.Priorities,
				defaultCustomization?._pinnedMonsterPriorityIndex
			);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSortingCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.ReversedOrder = defaultCustomization.ReversedOrder;
		this.Type = defaultCustomization.Type;
		this.TargetedMonsterPriority = defaultCustomization.TargetedMonsterPriority;
		this.PinnedMonsterPriority = defaultCustomization.PinnedMonsterPriority;
	}
}