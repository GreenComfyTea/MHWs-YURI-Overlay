using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementCustomization : Customization
{
	public bool Visible = true;
	public BarElementSettingsCustomization Settings = new();
	public OffsetCustomization Offset = new();
	public SizeCustomization Size = new();
	public BarElementColorsCustomization Colors = new();
	public BarElementOutlineCustomization Outline = new();

	public bool RenderImGui(string visibleName, string customizationName = "bar", BarElementCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode(visibleName, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref Visible, defaultCustomization?.Visible);

			isChanged |= Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= Size.RenderImGui(customizationName, defaultCustomization?.Size);
			isChanged |= Colors.RenderImGui(customizationName, defaultCustomization?.Colors);
			isChanged |= Outline.RenderImGui(customizationName, defaultCustomization?.Outline);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(BarElementCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Settings.Reset(defaultCustomization.Settings);
		Offset.Reset(defaultCustomization.Offset);
		Size.Reset(defaultCustomization.Size);
		Colors.Reset(defaultCustomization.Colors);
		Outline.Reset(defaultCustomization.Outline);
	}
}