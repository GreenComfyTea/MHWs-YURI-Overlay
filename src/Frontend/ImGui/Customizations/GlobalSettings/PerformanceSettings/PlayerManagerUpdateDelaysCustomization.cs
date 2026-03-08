using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class PlayerManagerUpdateDelaysCustomization : Customization
{
	public float? Update;

	public bool RenderImGui(string? parentName = "", PlayerManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-player-manager";

		if(ImGuiHelper.ResettableTreeNode(localization.PlayerManager, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Update}##{customizationName}", ref this.Update, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.Update);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PlayerManagerUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Update = defaultCustomization.Update;
	}
}