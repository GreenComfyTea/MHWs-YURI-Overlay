
using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterUiCustomization : Customization
{
	public LargeMonsterDynamicUiCustomization Dynamic = new();
	public LargeMonsterStaticUiCustomization Static = new();

	public LargeMonsterUiCustomization() { }

	public bool RenderImGui(string parentName = "", LargeMonsterUiCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.LargeMonstersUi}##{customizationName}"))
		{
			isChanged |= Dynamic.RenderImGui(customizationName, defaultCustomization?.Dynamic);
			isChanged |= Static.RenderImGui(customizationName, defaultCustomization?.Static);

			ImGui.TreePop();
		}


		return isChanged;
	}

	public void Reset(LargeMonsterUiCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Dynamic.Reset(defaultCustomization.Dynamic);
		Static.Reset(defaultCustomization.Static);
	}
}
