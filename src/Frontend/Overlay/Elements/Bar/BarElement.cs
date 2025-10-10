using System.Numerics;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElement
{
	private readonly Func<BarElementCustomization?> _customizationAccessor;

	private (OutlineStyleEnum, float, float, float, float, float, float, float, float, float) _cashingKeyByPosition1 = (OutlineStyleEnum.Inside, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
	private (FillDirectionEnum, float, float, float) _cashingKeyByProgress2 = (FillDirectionEnum.LeftToRight, 0f, 0f, 0f);

	private float _outlinePositionX;
	private float _outlinePositionY;

	private float _outlineWidth;
	private float _outlineHeight;

	private float _positionX;
	private float _positionY;

	private float _width;
	private float _height;

	private float _foregroundWidth;
	private float _foregroundHeight;

	private float _backgroundWidth;
	private float _backgroundHeight;

	private float _foregroundShiftX;
	private float _foregroundShiftY;

	private float _backgroundShiftX;
	private float _backgroundShiftY;

	private uint _backgroundColorTopLeft;
	private uint _backgroundColorTopRight;
	private uint _backgroundColorBottomRight;
	private uint _backgroundColorBottomLeft;

	private uint _foregroundColorTopLeft;
	private uint _foregroundColorTopRight;
	private uint _foregroundColorBottomRight;
	private uint _foregroundColorBottomLeft;

	private uint _outlineColor;

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

	public BarElement(Func<BarElementCustomization?> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float progress = 0.5f, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(customization?.Visible != true) return;

		progress = Math.Clamp(progress, 0f, 1f);
		if(customization.Settings.Inverted == true) progress = 1 - progress;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig?.Data?.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var outline = customization.Outline;
		var outlineThickness = (outline.Thickness ?? 1f) * sizeScaleModifier;

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

		if(outline.Visible == true && outlineThickness > 0f)
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

		if(customization is null) return;

		var offset = customization.Offset;
		var size = customization.Size;
		var outline = customization.Outline;

		var offsetX = offset.X ?? 0f;
		var offsetY = offset.Y ?? 0f;

		var width = size.Width ?? 0f;
		var height = size.Height ?? 0f;

		var outlineThickness = outline.Thickness ?? 1f;
		var outlineOffset = outline.Offset ?? 0f;
		var outlineStyle = outline.Style ?? OutlineStyleEnum.Outside;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig?.Data?.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var cachingKey = (outlineStyle, position.X, position.Y, offsetX, offsetY, width, height, outlineThickness, outlineOffset, sizeScaleModifier);

		offsetX *= sizeScaleModifier;
		offsetY *= sizeScaleModifier;

		width *= sizeScaleModifier;
		height *= sizeScaleModifier;

		outlineThickness *= sizeScaleModifier;
		outlineOffset *= sizeScaleModifier;

		if(!disableCaching && cachingKey == _cashingKeyByPosition1) return;

		_cashingKeyByPosition1 = cachingKey;

		var halfOutlineThickness = outlineThickness / 2f;
		var halfOutlineOffset = outlineOffset / 2f;

		switch(outlineStyle)
		{
			case OutlineStyleEnum.Outside:
				_positionX = position.X + offsetX;
				_positionY = position.Y + offsetY;

				_width = width;
				_height = height;

				_outlinePositionX = _positionX - halfOutlineThickness - outlineOffset;
				_outlinePositionY = _positionY - halfOutlineThickness - outlineOffset;

				_outlineWidth = _width + outlineThickness + outlineOffset + outlineOffset;
				_outlineHeight = _height + outlineThickness + outlineOffset + outlineOffset;

				break;
			case OutlineStyleEnum.Center:
				_outlinePositionX = position.X + offsetX - halfOutlineOffset;
				_outlinePositionY = position.Y + offsetY - halfOutlineOffset;

				_outlineWidth = width + outlineOffset;
				_outlineHeight = height + outlineOffset;

				_positionX = _outlinePositionX + halfOutlineThickness + outlineOffset;
				_positionY = _outlinePositionY + halfOutlineThickness + outlineOffset;

				_width = _outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				_height = _outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;

			case OutlineStyleEnum.Inside:
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

		if(customization is null) return;

		var fillDirection = customization.Settings.FillDirection ?? FillDirectionEnum.LeftToRight;

		var cachingKey = (fillDirection, _width, _height, progress);

		if(!disableCaching && cachingKey == _cashingKeyByProgress2) return;

		_cashingKeyByProgress2 = cachingKey;

		switch(fillDirection)
		{
			case FillDirectionEnum.RightToLeft:
				_foregroundWidth = _width * progress;
				_foregroundHeight = _height;

				_backgroundWidth = _width - _foregroundWidth;
				_backgroundHeight = _height;

				_foregroundShiftX = _backgroundWidth;
				break;
			case FillDirectionEnum.TopToBottom:
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_backgroundShiftY = _foregroundHeight;

				break;
			case FillDirectionEnum.BottomToTop:
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_foregroundShiftY = _backgroundHeight;

				break;
			case FillDirectionEnum.LeftToRight:
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
		if(customization is null) return;

		var colors = customization.Colors;
		var backgroundColor = colors.Background;
		var foregroundColor = colors.Foreground;

		switch(customization.Settings.FillDirection)
		{
			case FillDirectionEnum.RightToLeft:
				_backgroundColorTopRight = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorTopLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorBottomLeft = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopRight = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomLeft = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirectionEnum.TopToBottom:
				_backgroundColorTopLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorTopRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorBottomLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorTopRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorBottomLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirectionEnum.BottomToTop:
				_backgroundColorBottomLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorTopLeft = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorTopRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorBottomLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorTopRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
			case FillDirectionEnum.LeftToRight:
			default:
				_backgroundColorTopLeft = backgroundColor.Start.ColorInfo1.Abgr;
				_backgroundColorBottomLeft = backgroundColor.Start.ColorInfo2.Abgr;

				_backgroundColorTopRight = backgroundColor.End.ColorInfo1.Abgr;
				_backgroundColorBottomRight = backgroundColor.End.ColorInfo2.Abgr;

				_foregroundColorTopLeft = foregroundColor.Start.ColorInfo1.Abgr;
				_foregroundColorBottomLeft = foregroundColor.Start.ColorInfo2.Abgr;

				_foregroundColorTopRight = foregroundColor.End.ColorInfo1.Abgr;
				_foregroundColorBottomRight = foregroundColor.End.ColorInfo2.Abgr;
				break;
		}

		_outlineColor = customization.Outline.Color.ColorInfo.Abgr;

		if(Utils.IsApproximatelyEqual(opacityScale, 1f)) return;

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

		if(customization?.Outline.Thickness > 0f)
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