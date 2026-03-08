using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class GlobalScaleCustomization : Customization
{
	public float? PositionScaleModifier;
	public float? SizeScaleModifier;

	public OverlayFontScaleCustomization OverlayFontScale = new();

	public bool RenderImGui(string? parentName = "", GlobalScaleCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGuiHelper.ResettableTreeNode(localization.GlobalScale, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.PositionScaleModifier}##{customizationName}",
				ref this.PositionScaleModifier,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.PositionScaleModifier
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.SizeScaleModifier}##{customizationName}",
				ref this.SizeScaleModifier,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.SizeScaleModifier
			);

			isChanged |= this.OverlayFontScale.RenderImGui(customizationName, defaultCustomization?.OverlayFontScale);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(GlobalScaleCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.PositionScaleModifier = defaultCustomization.PositionScaleModifier;
		this.SizeScaleModifier = defaultCustomization.SizeScaleModifier;

		this.OverlayFontScale.Reset(defaultCustomization.OverlayFontScale);
	}
}