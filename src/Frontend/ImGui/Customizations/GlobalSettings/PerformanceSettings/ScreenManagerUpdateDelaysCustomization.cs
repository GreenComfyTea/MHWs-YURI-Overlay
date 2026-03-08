using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class ScreenManagerUpdateDelaysCustomization : Customization
{
	public float? Update;

	public bool RenderImGui(string? parentName = "", ScreenManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-screen-manager";

		if(ImGuiHelper.ResettableTreeNode(localization.ScreenManager, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Update}##{customizationName}", ref this.Update, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Update);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(ScreenManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Update = defaultCustomization.Update;
	}
}