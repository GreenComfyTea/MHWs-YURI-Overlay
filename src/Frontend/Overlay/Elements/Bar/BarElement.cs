using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElement
{
	private readonly Func<BarElementCustomization> _customizationAccessor;

	private (OutlineStyles, float, float, float, float, float, float, float, float, float) _cashingKeyByPosition1 = (OutlineStyles.Inside, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
	private (FillDirections, float, float, float) _cashingKeyByProgress2 = (FillDirections.LeftToRight, 0f, 0f, 0f);

	private float _outlinePositionX = 0f;
	private float _outlinePositionY = 0f;

	private float _outlineWidth = 0f;
	private float _outlineHeight = 0f;

	private float _positionX = 0f;
	private float _positionY = 0f;

	private float _width = 0f;
	private float _height = 0f;

	private float _foregroundWidth = 0f;
	private float _foregroundHeight = 0f;

	private float _backgroundWidth = 0f;
	private float _backgroundHeight = 0f;

	private float _foregroundShiftX = 0f;
	private float _foregroundShiftY = 0f;

	private float _backgroundShiftX = 0f;
	private float _backgroundShiftY = 0f;

	private uint _backgroundColorTopLeft = 0;
	private uint _backgroundColorTopRight = 0;
	private uint _backgroundColorBottomRight = 0;
	private uint _backgroundColorBottomLeft = 0;

	private uint _foregroundColorTopLeft = 0;
	private uint _foregroundColorTopRight = 0;
	private uint _foregroundColorBottomRight = 0;
	private uint _foregroundColorBottomLeft = 0;

	private uint _outlineColor = 0;

	private Vector2 _backgroundTopLeft = Vector2.Zero;
	private Vector2 _backgroundBottomRight = Vector2.Zero;

	private Vector2 _foregroundTopLeft = Vector2.Zero;
	private Vector2 _foregroundBottomRight = Vector2.Zero;

	private Vector2 _outlineTopLeft = Vector2.Zero;
	private Vector2 _outlineBottomRight = Vector2.Zero;

	public BarElement()
	{
		_customizationAccessor = () => new BarElementCustomization();
	}

	public BarElement(Func<BarElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float progress = 0.5f, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(!customization.Visible)
		{
			return;
		}

		progress = Math.Clamp(progress, 0f, 1f);

		var outline = customization.Outline;
		var outlineThickness = outline.Thickness;

		UpdateByPosition1(position);
		UpdateByProgress2(progress);
		UpdateByOpacity3(opacityScale);
		Update4();

		// Background

		backgroundDrawList.AddRectFilledMultiColor(
			_backgroundTopLeft,
			_backgroundBottomRight,
			_backgroundColorTopLeft,
			_backgroundColorTopRight,
			_backgroundColorBottomRight,
			_backgroundColorBottomLeft);

		// Foreground

		backgroundDrawList.AddRectFilledMultiColor(
			_foregroundTopLeft,
			_foregroundBottomRight,
			_foregroundColorTopLeft,
			_foregroundColorTopRight,
			_foregroundColorBottomRight,
			_foregroundColorBottomLeft);

		// Outline

		if(outline.Visible && outlineThickness > 0f)
		{
			backgroundDrawList.AddRect(
				_outlineTopLeft,
				_outlineBottomRight,
				_outlineColor,
				0f,
				ImDrawFlags.None,
				outlineThickness);
		}
	}

	private void UpdateByPosition1(Vector2 position, bool disableCaching = false)
	{
		var customization = _customizationAccessor();

		var offset = customization.Offset;
		var size = customization.Size;
		var outline = customization.Outline;

		var outlineThickness = outline.Thickness;
		var outlineOffset = outline.Offset;
		var outlineStyle = outline.Style;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier;

		var cachingKey = (outlineStyle, position.X, position.Y, offset.X, offset.Y, size.Width, size.Height, outlineThickness, outlineOffset, sizeScaleModifier);

		var offsetX = offset.X * sizeScaleModifier;
		var offsetY = offset.Y * sizeScaleModifier;

		var width = size.Width * sizeScaleModifier;
		var height = size.Height * sizeScaleModifier;

		outlineThickness = outline.Thickness * sizeScaleModifier;
		outlineOffset = outline.Offset * sizeScaleModifier;

		if(!disableCaching && cachingKey == _cashingKeyByPosition1)
		{
			return;
		}

		_cashingKeyByPosition1 = cachingKey;

		var halfOutlineThickness = outlineThickness / 2f;
		var halfOutlineOffset = outlineOffset / 2f;

		switch(outlineStyle)
		{
			case OutlineStyles.Outside:
				_positionX = position.X + offsetX;
				_positionY = position.Y + offsetY;

				_width = width;
				_height = height;

				_outlinePositionX = _positionX - halfOutlineThickness - outlineOffset;
				_outlinePositionY = _positionY - halfOutlineThickness - outlineOffset;

				_outlineWidth = _width + outlineThickness + outlineOffset + outlineOffset;
				_outlineHeight = _height + outlineThickness + outlineOffset + outlineOffset;

				break;
			case OutlineStyles.Center:
				_outlinePositionX = position.X + offsetX - halfOutlineOffset;
				_outlinePositionY = position.Y + offsetY - halfOutlineOffset;

				_outlineWidth = width + outlineOffset;
				_outlineHeight = height + outlineOffset;

				_positionX = _outlinePositionX + halfOutlineThickness + outlineOffset;
				_positionY = _outlinePositionY + halfOutlineThickness + outlineOffset;

				_width = _outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				_height = _outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;

			case OutlineStyles.Inside:
			default:
				_outlinePositionX = position.X + offsetX + halfOutlineThickness;
				_outlinePositionY = position.Y + offsetY + halfOutlineThickness;

				_outlineWidth = width - outlineThickness;
				_outlineHeight = height - outlineThickness;

				_positionX = _outlinePositionX + halfOutlineThickness + outlineOffset;
				_positionY = _outlinePositionY + halfOutlineThickness + outlineOffset;

				_width = _outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				_height = _outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
		}
	}

	private void UpdateByProgress2(float progress = 0.5f, bool disableCaching = false)
	{
		var customization = _customizationAccessor();

		var fillDirection = customization.Settings.FillDirection;

		var cachingKey = (fillDirection, _width, _height, progress);

		if(!disableCaching && cachingKey == _cashingKeyByProgress2)
		{
			return;
		}

		_cashingKeyByProgress2 = cachingKey;

		switch(fillDirection)
		{
			case FillDirections.RightToLeft:
				_foregroundWidth = _width * progress;
				_foregroundHeight = _height;

				_backgroundWidth = _width - _foregroundWidth;
				_backgroundHeight = _height;

				_foregroundShiftX = _backgroundWidth;
				break;
			case FillDirections.TopToBottom:
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_backgroundShiftY = _foregroundHeight;

				break;
			case FillDirections.BottomToTop:
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_foregroundShiftY = _backgroundHeight;

				break;
			case FillDirections.LeftToRight:
			default:
				_foregroundWidth = _width * progress;
				_foregroundHeight = _height;

				_backgroundWidth = _width - _foregroundWidth;
				_backgroundHeight = _height;

				_backgroundShiftX = _foregroundWidth;

				break;
		}
	}

	private void UpdateByOpacity3(float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		var colors = customization.Colors;
		var backgroundColor = colors.Background;
		var foregroundColor = colors.Foreground;

		switch(customization.Settings.FillDirection)
		{
			case FillDirections.RightToLeft:
				_backgroundColorTopRight = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorTopLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorBottomLeft = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopRight = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomLeft = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirections.TopToBottom:
				_backgroundColorTopLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorTopRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorBottomLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorTopRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorBottomLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirections.BottomToTop:
				_backgroundColorBottomLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorTopLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorTopRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorBottomLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorTopRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirections.LeftToRight:
			default:
				_foregroundColorTopLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomLeft = backgroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopRight = backgroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomLeft = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopRight = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
		}

		_outlineColor = customization.Outline.Color.ColorInfo.Abgr;

		if(Utils.IsApproximatelyEqual(opacityScale, 1f))
		{
			return;
		}

		_backgroundColorTopLeft = Utils.ScaleColorOpacityAbgr(_backgroundColorTopLeft, opacityScale);
		_backgroundColorTopRight = Utils.ScaleColorOpacityAbgr(_backgroundColorTopRight, opacityScale);

		_backgroundColorBottomRight = Utils.ScaleColorOpacityAbgr(_backgroundColorBottomRight, opacityScale);
		_backgroundColorBottomLeft = Utils.ScaleColorOpacityAbgr(_backgroundColorBottomLeft, opacityScale);

		_foregroundColorTopLeft = Utils.ScaleColorOpacityAbgr(_foregroundColorTopLeft, opacityScale);
		_foregroundColorTopRight = Utils.ScaleColorOpacityAbgr(_foregroundColorTopRight, opacityScale);

		_foregroundColorBottomRight = Utils.ScaleColorOpacityAbgr(_foregroundColorBottomRight, opacityScale);
		_foregroundColorBottomLeft = Utils.ScaleColorOpacityAbgr(_foregroundColorBottomLeft, opacityScale);

		_outlineColor = Utils.ScaleColorOpacityAbgr(_outlineColor, opacityScale);
	}

	private void Update4()
	{
		var customization = _customizationAccessor();

		// Background

		_backgroundTopLeft = new Vector2(
				_positionX + _backgroundShiftX,
				_positionY + _backgroundShiftY
			);

		_backgroundBottomRight = new Vector2(
			_backgroundTopLeft.X + _backgroundWidth,
			_backgroundTopLeft.Y + _backgroundHeight
		);

		// Foreground

		_foregroundTopLeft = new Vector2(
				_positionX + _foregroundShiftX,
				_positionY + _foregroundShiftY
			);

		_foregroundBottomRight = new Vector2(
			_foregroundTopLeft.X + _foregroundWidth,
			_foregroundTopLeft.Y + _foregroundHeight
		);

		// Outline

		if(customization.Outline.Thickness > 0f)
		{
			_outlineTopLeft = new Vector2(
				_outlinePositionX,
				_outlinePositionY
			);

			_outlineBottomRight = new Vector2(
				_outlinePositionX + _outlineWidth,
				_outlinePositionY + _outlineHeight
			);
		}
	}
}
