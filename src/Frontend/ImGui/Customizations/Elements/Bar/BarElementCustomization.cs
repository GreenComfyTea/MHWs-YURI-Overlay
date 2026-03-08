using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class BarElementCustomization : Customization
{
	public bool? Visible;
	public BarElementSettingsCustomization Settings = new();
	public OffsetCustomization Offset = new();
	public SizeCustomization Size = new();
	public BarElementColorsCustomization Colors = new();
	public BarElementOutlineCustomization Outline = new();

	public bool RenderImGui(string? visibleName = "", string customizationName = "bar", BarElementCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode(visibleName, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref this.Visible, defaultCustomization?.Visible);

			isChanged |= this.Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.Size.RenderImGui(customizationName, defaultCustomization?.Size);
			isChanged |= this.Colors.RenderImGui(customizationName, defaultCustomization?.Colors);
			isChanged |= this.Outline.RenderImGui(customizationName, defaultCustomization?.Outline);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Visible = defaultCustomization.Visible;
		this.Settings.Reset(defaultCustomization.Settings);
		this.Offset.Reset(defaultCustomization.Offset);
		this.Size.Reset(defaultCustomization.Size);
		this.Colors.Reset(defaultCustomization.Colors);
		this.Outline.Reset(defaultCustomization.Outline);
	}
}