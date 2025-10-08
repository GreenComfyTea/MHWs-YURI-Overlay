using System.Numerics;
using ImGuiNET;
using static app.cGUISystemModuleOption;

namespace YURI_Overlay;

internal sealed class LabelElement
{
	private readonly Func<LabelElementCustomization> _customizationAccessor;

	public LabelElement(Func<LabelElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f, params object[] args)
	{
		var customization = _customizationAccessor();

		if(!customization.Visible) return;

		if(args.Length == 0) return;

		var text = string.Format(customization.Format, args);

		if(string.IsNullOrEmpty(text)) return;

		var globalScaleCustomization = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale;
		var sizeScaleModifier = globalScaleCustomization.SizeScaleModifier;
		var overlayFontScale = globalScaleCustomization.FontScale.OverlayFontScale;

		var offset = customization.Offset;
		var shadowOffset = customization.Shadow.Offset;

		var offsetX = offset.X * sizeScaleModifier;
		var offsetY = offset.Y * sizeScaleModifier;

		var shadowOffsetX = shadowOffset.X * sizeScaleModifier;
		var shadowOffsetY = shadowOffset.Y * sizeScaleModifier;

		var textPositionX = position.X + offsetX;
		var textPositionY = position.Y + offsetY;

		var shadowPositionX = textPositionX + shadowOffsetX;
		var shadowPositionY = textPositionY + shadowOffsetY;

		var (alignmentX, alignmentY, textSize) = GetAlignmentShifts(text, customization.Settings.Alignment);

		text = ImGuiHelper.TruncateTextByMaxWidth(text, customization.Settings.MaxWidth * sizeScaleModifier, textSize);

		Vector2 textPosition = new(textPositionX + alignmentX, textPositionY + alignmentY);
		Vector2 shadowPosition = new(shadowPositionX + alignmentX, shadowPositionY + alignmentY);

		var font = ImGui.GetFont();
		var fontSize = customization.Settings.FontSize * overlayFontScale.OverlayFontScaleModifier;

		if(overlayFontScale.ScaleWithReframeworkFontSize)
		{
			fontSize *= ImGuiManager.Instance.ReframeworkFontSize / Constants.DefaultReframeworkFontSize;
		}

		if(customization.Shadow.Visible)
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

	private static (float, float, Vector2?) GetAlignmentShifts(string text, Anchor alignment)
	{
		Vector2 textSize;
		switch(alignment)
		{
			case Anchor.TopCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, 0, textSize);
			case Anchor.TopRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, 0, textSize);
			case Anchor.CenterLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y / 2, textSize);
			case Anchor.Center:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y / 2, textSize);
			case Anchor.CenterRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y / 2, textSize);
			case Anchor.BottomLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y, textSize);
			case Anchor.BottomCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y, textSize);
			case Anchor.BottomRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y, textSize);
			case Anchor.TopLeft:
			default:
				return (0, 0, null);
		}
	}
}