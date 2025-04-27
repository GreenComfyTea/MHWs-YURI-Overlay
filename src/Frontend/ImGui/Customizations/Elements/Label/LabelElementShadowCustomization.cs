using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementShadowCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();

	public bool RenderImGui(string parentName = "", LabelElementShadowCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-shadow";

		if(ImGuiHelper.ResettableTreeNode(localization.Shadow, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{parentName}", ref Visible, defaultCustomization?.Visible);

			isChanged |= Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= Color.RenderImGui(customizationName, defaultCustomization?.Color);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementShadowCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		Visible = defaultCustomization.Visible;
		Offset.Reset(defaultCustomization.Offset);
		Color.Reset(defaultCustomization.Color);
	}
}