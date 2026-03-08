using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LabelElementCustomization : Customization
{
	public bool? Visible;
	public string? Format;
	public LabelElementSettingsCustomization Settings = new();
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();
	public LabelElementShadowCustomization Shadow = new();

	public bool RenderImGui(string? visibleName = "", string customizationName = "label", LabelElementCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGuiHelper.ResettableTreeNode(visibleName, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCheckbox($"{localization.Visible}##{customizationName}", ref this.Visible, defaultCustomization?.Visible);

			if(this.Format is not null)
			{
				isChanged |= ImGuiHelper.ResettableInputText($"{localization.Format}##{customizationName}", ref this.Format, defaultValue: defaultCustomization?.Format);
			}

			isChanged |= this.Settings.RenderImGui(customizationName, defaultCustomization?.Settings);
			isChanged |= this.Offset.RenderImGui(customizationName, defaultCustomization?.Offset);
			isChanged |= this.Color.RenderImGui(customizationName, defaultCustomization?.Color);
			isChanged |= this.Shadow.RenderImGui(customizationName, defaultCustomization?.Shadow);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(LabelElementCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.Visible = defaultCustomization.Visible;
		this.Format = defaultCustomization.Format;

		this.Offset.Reset(defaultCustomization.Offset);
		this.Settings.Reset(defaultCustomization.Settings);
		this.Color.Reset(defaultCustomization.Color);
		this.Shadow.Reset(defaultCustomization.Shadow);
	}
}