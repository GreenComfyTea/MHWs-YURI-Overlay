using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class PlayerManagerUpdateDelaysCustomization : Customization
{
	public float? Update = null;

	public bool RenderImGui(string? parentName = "", PlayerManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-player-manager";

		if(ImGuiHelper.ResettableTreeNode(localization.PlayerManager, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Update}##{customizationName}", ref Update, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Update);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PlayerManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Update = defaultCustomization.Update;
	}
}