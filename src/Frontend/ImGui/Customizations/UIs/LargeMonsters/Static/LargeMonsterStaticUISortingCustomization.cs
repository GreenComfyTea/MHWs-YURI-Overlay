using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUiSortingCustomization : Customization
{
	public bool ReversedOrder;

	private int _typeIndex = (int) Sorting.Name;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Sorting Type
	{
		get => (Sorting) _typeIndex;
		set => _typeIndex = (int) value;
	}

	private int _targetedMonsterPriorityIndex = (int) Priority.Higher2;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Priority TargetedMonsterPriority
	{
		get => (Priority) _targetedMonsterPriorityIndex;
		set => _targetedMonsterPriorityIndex = (int) value;
	}

	private int _pinnedMonsterPriorityIndex = (int) Priority.Higher1;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Priority PinnedMonsterPriority
	{
		get => (Priority) _pinnedMonsterPriorityIndex;
		set => _pinnedMonsterPriorityIndex = (int) value;
	}

	public bool RenderImGui(string parentName = "", LargeMonsterStaticUiSortingCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Sorting, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ReversedOrder}##{customizationName}", ref ReversedOrder, defaultCustomization?.ReversedOrder);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Type}##{customizationName}", ref _typeIndex, localizationHelper.Sortings, defaultCustomization?._typeIndex);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.TargetedMonsterPriority}##{customizationName}", ref _targetedMonsterPriorityIndex, localizationHelper.Priorities, defaultCustomization?._targetedMonsterPriorityIndex);
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.PinnedMonsterPriority}##{customizationName}", ref _pinnedMonsterPriorityIndex, localizationHelper.Priorities, defaultCustomization?._pinnedMonsterPriorityIndex);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSortingCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ReversedOrder = defaultCustomization.ReversedOrder;
		Type = defaultCustomization.Type;
		TargetedMonsterPriority = defaultCustomization.TargetedMonsterPriority;
		PinnedMonsterPriority = defaultCustomization.PinnedMonsterPriority;
	}
}