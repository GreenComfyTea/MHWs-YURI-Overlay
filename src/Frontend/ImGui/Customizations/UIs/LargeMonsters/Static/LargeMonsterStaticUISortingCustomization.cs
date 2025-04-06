using ImGuiNET;
using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSortingCustomization : Customization
{
	private int _typeIndex = (int) Sortings.Name;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Sortings Type { get => (Sortings) _typeIndex; set => _typeIndex = (int) value; }

	public bool ReversedOrder = false;

	public LargeMonsterStaticUiSortingCustomization() { }

	public bool RenderImGui(string parentName = "", LargeMonsterStaticUiSortingCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.Sorting}##{customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Type}##{customizationName}", ref _typeIndex, localizationHelper.Sortings, defaultCustomization?._typeIndex);
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.ReversedOrder}##{customizationName}", ref ReversedOrder, defaultCustomization?.ReversedOrder);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LargeMonsterStaticUiSortingCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Type = defaultCustomization.Type;
		ReversedOrder = defaultCustomization.ReversedOrder;
	}
}
