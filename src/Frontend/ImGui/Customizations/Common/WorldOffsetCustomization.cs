using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class WorldOffsetCustomization : Customization
{
	public float? X;
	public float? Y;
	public float? Z;

	public bool RenderImGui(string? parentName = "", WorldOffsetCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-world-offset";

		if(ImGuiHelper.ResettableTreeNode(localization.WorldOffset, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.X}##{customizationName}", ref this.X, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.X);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Y}##{customizationName}", ref this.Y, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.Y);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.Z}##{customizationName}", ref this.Z, 0.001f, -4096f, 4096f, "%.3f", defaultCustomization?.Z);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(WorldOffsetCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.X = defaultCustomization.X;
		this.Y = defaultCustomization.Y;
		this.Z = defaultCustomization.Z;
	}
}