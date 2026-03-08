using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LabelElementShadowCustomization : Customization
{
	public bool? Visible;
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();

	public bool RenderImGui(string? parentName = "", LabelElementShadowCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-shadow";

		if(ImGuiHelper.ResettableTreeNode(localization.Shadow, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{parentName}", ref this.Visible, defaultCustomization?.Visible);

			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.Color.RenderImGui(customizationName, defaultCustomization?.Color);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementShadowCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Visible = defaultCustomization.Visible;
		this.Offset.Reset(defaultCustomization.Offset);
		this.Color.Reset(defaultCustomization.Color);
	}
}