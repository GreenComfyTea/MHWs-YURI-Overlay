using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementSettingsCustomization : Customization
{
	public int RightAlignmentShift = 0;

	public LabelElementSettingsCustomization() { }

	public bool RenderImGui(string parentName = "", LabelElementSettingsCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.Settings, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableInputInt($"{localization.RightAlignmentShift}##{customizationName}", ref RightAlignmentShift, defaultCustomization?.RightAlignmentShift);

			if(isChanged && RightAlignmentShift < 0) RightAlignmentShift = 0;

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementSettingsCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		RightAlignmentShift = defaultCustomization.RightAlignmentShift;
	}
}
