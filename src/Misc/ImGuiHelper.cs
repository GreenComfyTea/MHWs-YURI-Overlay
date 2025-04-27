using System.Numerics;
using ImGuiNET;

namespace YURI_Overlay;

internal static class ImGuiHelper
{
	public static bool Combo(string label, ref int currentItem, string[] items)
	{
		ImGui.SetNextItemWidth(ImGuiManager.Instance.ComboBoxWidth);
		return ImGui.Combo(label, ref currentItem, items, items.Length);
	}

	public static bool InputText(string label, ref string input, uint maxLength = 256)
	{
		return ImGui.InputText(label, ref input, maxLength);
	}

	public static bool ResettableCombo(string label, ref int currentItem, string[] items, int? defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) currentItem = (int) defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		ImGui.SetNextItemWidth(ImGuiManager.Instance.ComboBoxWidth);
		isChanged |= Combo(label, ref currentItem, items);

		return isChanged;
	}

	public static bool ResettableInputText(string label, ref string input, uint maxLength = 256, string defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) input = defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		isChanged |= InputText(label, ref input, maxLength);

		return isChanged;
	}

	public static bool ResettableDragFloat(string label, ref float value, float speed, float minValue, float maxValue, string format, float? defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) value = (float) defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		isChanged |= ImGui.DragFloat(label, ref value, speed, minValue, maxValue, format);

		return isChanged;
	}

	public static bool ResettableColorPicker4(string label, ref Vector4 value, Vector4? defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) value = (Vector4) defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		;
		ImGui.SetNextItemWidth(ImGuiManager.Instance.ColorPickerWidth);
		isChanged |= ImGui.ColorPicker4(label, ref value);

		return isChanged;
	}

	public static bool ResettableCheckbox(string label, ref bool value, bool? defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) value = (bool) defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		isChanged |= ImGui.Checkbox(label, ref value);

		return isChanged;
	}

	public static bool ResettableInputInt(string label, ref int value, int? defaultValue = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultValue is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{label}");
			if(isChanged) value = (int) defaultValue;

			Tooltip(localization.ResetToDefault);
			ImGui.SameLine();
		}

		isChanged |= ImGui.InputInt(label, ref value);

		return isChanged;
	}

	public static bool ResetButton<T>(string parentName, T defaultCustomization, Action<T> resetMethod, bool isSameLine = true)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(defaultCustomization is not null)
		{
			isChanged |= ImGui.Button($"{localization.ResetIcon}##{parentName}");
			if(isChanged) resetMethod(defaultCustomization);

			Tooltip(localization.ResetToDefault);
			if(isSameLine) ImGui.SameLine();
		}

		return isChanged;
	}

	public static void Tooltip(string text)
	{
		if(ImGui.IsItemHovered())
		{
			ImGui.BeginTooltip();
			ImGui.Text(text);
			ImGui.EndTooltip();
		}
	}

	public static bool ResettableTreeNode<T>(string label, string customizationName, ref bool isChanged, T defaultCustomization, Action<T> resetMethod)
	{
		isChanged |= ResetButton(customizationName, defaultCustomization, resetMethod);

		return ImGui.TreeNode($"{label}##${customizationName}");
	}
}