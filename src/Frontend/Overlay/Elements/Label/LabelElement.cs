using ImGuiNET;
using System.Numerics;

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

		if(!customization.Visible)
		{
			return;
		}

		if(args.Length == 0)
		{
			return;
		}

		var text = string.Format(customization.Format, args);

		if(string.IsNullOrEmpty(text))
		{
			return;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier;

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

		var (alignmentX, alignmentY) = GetAlignmentShifts(text, customization.Settings.Alignment);

		Vector2 textPosition = new(textPositionX + alignmentX, textPositionY + alignmentY);
		Vector2 shadowPosition = new(shadowPositionX + alignmentX, shadowPositionY + alignmentY);

		if(customization.Shadow.Visible)
		{
			var shadowColor = customization.Shadow.Color.ColorInfo.Abgr;
			if(opacityScale < 1)
			{
				shadowColor = Utils.ScaleColorOpacityAbgr(shadowColor, opacityScale);
			}

			backgroundDrawList.AddText(shadowPosition, shadowColor, text);
		}

		var color = customization.Color.ColorInfo.Abgr;
		if(opacityScale < 1)
		{
			color = Utils.ScaleColorOpacityAbgr(color, opacityScale);
		}

		backgroundDrawList.AddText(textPosition, color, text);
	}

	private static (float, float) GetAlignmentShifts(string text, Anchors alignment)
	{
		Vector2 textSize;
		switch(alignment)
		{
			case Anchors.TopCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, 0);
			case Anchors.TopRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, 0);
			case Anchors.CenterLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y / 2);
			case Anchors.Center:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y / 2);
			case Anchors.CenterRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y / 2);
			case Anchors.BottomLeft:
				textSize = ImGui.CalcTextSize(text);
				return (0, -textSize.Y);
			case Anchors.BottomCenter:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X / 2, -textSize.Y);
			case Anchors.BottomRight:
				textSize = ImGui.CalcTextSize(text);
				return (-textSize.X, -textSize.Y);
			case Anchors.TopLeft:
			default:
				return (0, 0);
		}
	}
}
