using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ScreenManagerUpdateDelaysCustomization : Customization
{
	public float Update = 1f;

	public ScreenManagerUpdateDelaysCustomization() { }

	public bool RenderImGui(string parentName = "", ScreenManagerUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-screen-manager";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.ScreenManager}##${customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Update}##{customizationName}", ref Update, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Update);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(ScreenManagerUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Update = defaultCustomization.Update;
	}
}
