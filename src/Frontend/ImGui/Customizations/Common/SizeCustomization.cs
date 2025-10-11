using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SizeCustomization : Customization
{
	public float? Width = null;
	public float? Height = null;

	public bool RenderImGui(string? parentName = "", SizeCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-size";

		if(ImGuiHelper.ResettableTreeNode(localization.Size, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Width}##{customizationName}", ref Width, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.Width);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Height}##{customizationName}", ref Height, 0.1f, -8192f, 8192f, "%.1f", defaultCustomization?.Height);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(SizeCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Width = defaultCustomization.Width;
		Height = defaultCustomization.Height;
	}
}