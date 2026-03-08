using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class PositionCustomization : Customization
{
	public float? X;
	public float? Y;

	public bool RenderImGui(string? parentName = "", PositionCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-position";

		if(ImGuiHelper.ResettableTreeNode(localization.Position, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##{customizationName}", ref this.X, 0.1f, 0f, 8192f, "%.1f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##{customizationName}", ref this.Y, 0.1f, 0f, 8192f, "%.1f", defaultCustomization?.Y);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(PositionCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.X = defaultCustomization.X;
		this.Y = defaultCustomization.Y;
	}
}