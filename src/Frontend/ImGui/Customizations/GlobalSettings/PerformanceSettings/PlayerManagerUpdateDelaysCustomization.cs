using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PlayerManagerUpdateDelaysCustomization : Customization
{
	public float Update = 1f;

	public PlayerManagerUpdateDelaysCustomization() { }

	public bool RenderImGui(string parentName = "", PlayerManagerUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-player-manager";

		isChanged |= ImGuiHelper.ResetButton(customizationName, defaultCustomization, Reset);

		if(ImGui.TreeNode($"{localization.PlayerManager}##${customizationName}"))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Update}##{customizationName}", ref Update, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Update);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PlayerManagerUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Update = defaultCustomization.Update;
	}
}
