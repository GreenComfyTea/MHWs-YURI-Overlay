using System.Numerics;
using ImGuiNET;
using static app.cGUISystemModuleOption;

namespace YURI_Overlay;

internal sealed class LabelElement
{
	private readonly Func<LabelElementCustomization?> _customizationAccessor;

	public LabelElement(Func<LabelElementCustomization?> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f, params object[] args)
	{
		var customization = _customizationAccessor();

		if(customization is null) return;

		if(customization.Visible != true) return;

		if(args.Length == 0) return;

		var text = string.Format(customization.Format ?? "", args);

		if(string.IsNullOrEmpty(text)) return;

		var globalScaleCustomization = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale;
		var sizeScaleModifier = globalScaleCustomization?.SizeScaleModifier ?? 1f;
		var overlayFontScale = globalScaleCustomization?.OverlayFontScale;

		var offset = customization.Offset;
		var shadowOffset = customization.Shadow.Offset;

		var offsetX = offset.X ?? 1f * sizeScaleModifier;
		var offsetY = offset.Y ?? 1f * sizeScaleModifier;

		var shadowOffsetX = shadowOffset.X ?? 1f * sizeScaleModifier;
		var shadowOffsetY = shadowOffset.Y ?? 1f * sizeScaleModifier;

		var textPositionX = position.X + offsetX;
		var textPositionY = position.Y + offsetY;

		var shadowPositionX = textPositionX + shadowOffsetX;
		var shadowPositionY = textPositionY + shadowOffsetY;


		var (alignmentX, alignmentY, textSize) = GetAlignmentShifts(text, customization.Settings.Alignment ?? AnchorEnum.TopLeft);

		text = ImGuiHelper.TruncateTextByMaxWidth(text, customization.Settings.MaxWidth ?? 0f * sizeScaleModifier, textSize);

		Vector2 textPosition = new(textPositionX + alignmentX, textPositionY + alignmentY);
		Vector2 shadowPosition = new(shadowPositionX + alignmentX, shadowPositionY + alignmentY);

		var font = ImGui.GetFont();
		var fontSize = customization.Settings.FontSize ?? Constants.DefaultReframeworkFontSize * overlayFontScale?.OverlayFontScaleModifier ?? 1f;

		if(overlayFontScale?.ScaleWithReframeworkFontSize == true)
		{
			fontSize *= ImGuiManager.Instance.ReframeworkFontSize / Constants.DefaultReframeworkFontSize;
		}

		if(customization.Shadow.Visible == true)
		{
			var shadowColor = customization.Shadow.Color.ColorInfo.Abgr;
			if(opacityScale < 1) shadowColor = Utils.ScaleColorOpacityAbgr(shadowColor, opacityScale);

			backgroundDrawList.AddText(font, fontSize, shadowPosition, shadowColor, text);
		}

		var color = customization.Color.ColorInfo.Abgr;
		if(opacityScale < 1) color = Utils.ScaleColorOpacityAbgr(color, opacityScale);

		//backgroundDrawList.AddText(textPosition, color, text);
		backgroundDrawList.AddText(font, fontSize, textPosition, color, text);
	}

	private static (float, float, Vector2?) GetAlignmentShifts(string text, AnchorEnum alignment)
	{
		Vector2 textSize;
		switch(alignment)
		{
			case AnchorEnum.TopCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, 0, textSize);
			case AnchorEnum.TopRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, 0, textSize);
			case AnchorEnum.CenterLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y / 2, textSize);
			case AnchorEnum.Center:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y / 2, textSize);
			case AnchorEnum.CenterRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y / 2, textSize);
			case AnchorEnum.BottomLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y, textSize);
			case AnchorEnum.BottomCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y, textSize);
			case AnchorEnum.BottomRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y, textSize);
			case AnchorEnum.TopLeft:
			default:
				return (0, 0, null);
		}
	}
}