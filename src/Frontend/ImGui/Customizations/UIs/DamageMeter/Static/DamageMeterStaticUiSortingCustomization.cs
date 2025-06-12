using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUiSortingCustomization : Customization
{
	public bool ReversedOrder;

	private int _typeIndex = (int) DamageMeterSorting.Name;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public DamageMeterSorting Type
	{
		get => (DamageMeterSorting) _typeIndex;
		set => _typeIndex = (int) value;
	}

	private int _localPlayerPriorityIndex = (int) Priority.Normal;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Priority LocalPlayerPriority
	{
		get => (Priority) _localPlayerPriorityIndex;
		set => _localPlayerPriorityIndex = (int) value;
	}

	public bool RenderImGui(string parentName = "", DamageMeterStaticUiSortingCustomization defaultCustomization = null)
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

	public void Reset(DamageMeterStaticUiSortingCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		ReversedOrder = defaultCustomization.ReversedOrder;
		Type = defaultCustomization.Type;
		LocalPlayerPriority = defaultCustomization.LocalPlayerPriority;
	}
}