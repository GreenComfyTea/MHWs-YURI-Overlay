using System.Numerics;
using Hexa.NET.ImGui;

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
		this._customizationAccessor = () => new BarElementCustomization();
	}

	public BarElement(Func<BarElementCustomization?> customizationAccessor)
	{
		this._customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float progress = 0.5f, float opacityScale = 1f)
	{
		var customization = this._customizationAccessor();

		if(customization?.Visible != true)
		{
			return;
		}

		progress = Math.Clamp(progress, 0f, 1f);

		if(customization.Settings.Inverted == true)
		{
			progress = 1 - progress;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var outline = customization.Outline;
		var outlineThickness = (outline.Thickness ?? 1f) * sizeScaleModifier;

		this.UpdateByPosition1(position);
		this.UpdateByProgress2(progress);
		this.UpdateByOpacity3(opacityScale);
		this.Update4();

		// Background

		drawList.AddRectFilledMultiColor(this._backgroundTopLeft, this._backgroundBottomRight, this._backgroundColorTopLeft, this._backgroundColorTopRight, this._backgroundColorBottomRight,
			this._backgroundColorBottomLeft
		);

		// Foreground

		drawList.AddRectFilledMultiColor(this._foregroundTopLeft, this._foregroundBottomRight, this._foregroundColorTopLeft, this._foregroundColorTopRight, this._foregroundColorBottomRight,
			this._foregroundColorBottomLeft
		);

		// Outline

		if(outline.Visible == true && outlineThickness > 0f)
		{
			drawList.AddRect(this._outlineTopLeft, this._outlineBottomRight, this._outlineColor, 0f, ImDrawFlags.None, outlineThickness);
		}
	}

	private void UpdateByPosition1(Vector2 position, bool disableCaching = false)
	{
		var customization = this._customizationAccessor();

		if(customization is null)
		{
			return;
		}

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

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var cachingKey = (outlineStyle, position.X, position.Y, offsetX, offsetY, width, height, outlineThickness, outlineOffset, sizeScaleModifier);

		offsetX *= sizeScaleModifier;
		offsetY *= sizeScaleModifier;

		width *= sizeScaleModifier;
		height *= sizeScaleModifier;

		outlineThickness *= sizeScaleModifier;
		outlineOffset *= sizeScaleModifier;

		if(!disableCaching && cachingKey == this._cashingKeyByPosition1)
		{
			return;
		}

		this._cashingKeyByPosition1 = cachingKey;

		var halfOutlineThickness = outlineThickness / 2f;
		var halfOutlineOffset = outlineOffset / 2f;

		switch(outlineStyle)
		{
			case OutlineStyleEnum.Outside:
				this._positionX = position.X + offsetX;
				this._positionY = position.Y + offsetY;

				this._width = width;
				this._height = height;

				this._outlinePositionX = this._positionX - halfOutlineThickness - outlineOffset;
				this._outlinePositionY = this._positionY - halfOutlineThickness - outlineOffset;

				this._outlineWidth = this._width + outlineThickness + outlineOffset + outlineOffset;
				this._outlineHeight = this._height + outlineThickness + outlineOffset + outlineOffset;

				break;
			case OutlineStyleEnum.Center:
				this._outlinePositionX = position.X + offsetX - halfOutlineOffset;
				this._outlinePositionY = position.Y + offsetY - halfOutlineOffset;

				this._outlineWidth = width + outlineOffset;
				this._outlineHeight = height + outlineOffset;

				this._positionX = this._outlinePositionX + halfOutlineThickness + outlineOffset;
				this._positionY = this._outlinePositionY + halfOutlineThickness + outlineOffset;

				this._width = this._outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				this._height = this._outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;

			case OutlineStyleEnum.Inside:
			default:
				this._outlinePositionX = position.X + offsetX + halfOutlineThickness;
				this._outlinePositionY = position.Y + offsetY + halfOutlineThickness;

				this._outlineWidth = width - outlineThickness;
				this._outlineHeight = height - outlineThickness;

				this._positionX = this._outlinePositionX + halfOutlineThickness + outlineOffset;
				this._positionY = this._outlinePositionY + halfOutlineThickness + outlineOffset;

				this._width = this._outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				this._height = this._outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
		}
	}

	private void UpdateByProgress2(float progress = 0.5f, bool disableCaching = false)
	{
		var customization = this._customizationAccessor();

		if(customization is null)
		{
			return;
		}

		var fillDirection = customization.Settings.FillDirection ?? FillDirectionEnum.LeftToRight;

		var cachingKey = (fillDirection, this._width, this._height, progress);

		if(!disableCaching && cachingKey == this._cashingKeyByProgress2)
		{
			return;
		}

		this._cashingKeyByProgress2 = cachingKey;

		switch(fillDirection)
		{
			case FillDirectionEnum.RightToLeft:
				this._foregroundWidth = this._width * progress;
				this._foregroundHeight = this._height;

				this._backgroundWidth = this._width - this._foregroundWidth;
				this._backgroundHeight = this._height;

				this._foregroundShiftX = this._backgroundWidth;

				break;
			case FillDirectionEnum.TopToBottom:
				this._foregroundWidth = this._width;
				this._foregroundHeight = this._height * progress;

				this._backgroundWidth = this._width;
				this._backgroundHeight = this._height - this._foregroundHeight;

				this._backgroundShiftY = this._foregroundHeight;

				break;
			case FillDirectionEnum.BottomToTop:
				this._foregroundWidth = this._width;
				this._foregroundHeight = this._height * progress;

				this._backgroundWidth = this._width;
				this._backgroundHeight = this._height - this._foregroundHeight;

				this._foregroundShiftY = this._backgroundHeight;

				break;
			case FillDirectionEnum.LeftToRight:
			default:
				this._foregroundWidth = this._width * progress;
				this._foregroundHeight = this._height;

				this._backgroundWidth = this._width - this._foregroundWidth;
				this._backgroundHeight = this._height;

				this._backgroundShiftX = this._foregroundWidth;

				break;
		}
	}

	private void UpdateByOpacity3(float opacityScale = 1f)
	{
		var customization = this._customizationAccessor();

		if(customization is null)
		{
			return;
		}

		var colors = customization.Colors;
		var backgroundColor = colors.Background;
		var foregroundColor = colors.Foreground;

		switch(customization.Settings.FillDirection)
		{
			case FillDirectionEnum.RightToLeft:
				this._backgroundColorTopRight = backgroundColor.Start.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomRight = backgroundColor.Start.ColorInfo2?.Abgr ?? 0xFF000000;

				this._backgroundColorTopLeft = backgroundColor.End.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomLeft = backgroundColor.End.ColorInfo2?.Abgr ?? 0xFF000000;

				this._foregroundColorTopRight = foregroundColor.Start.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomRight = foregroundColor.Start.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				this._foregroundColorTopLeft = foregroundColor.End.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomLeft = foregroundColor.End.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				break;
			case FillDirectionEnum.TopToBottom:
				this._backgroundColorTopLeft = backgroundColor.Start.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorTopRight = backgroundColor.Start.ColorInfo2?.Abgr ?? 0xFF000000;

				this._backgroundColorBottomLeft = backgroundColor.End.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomRight = backgroundColor.End.ColorInfo2?.Abgr ?? 0xFF000000;

				this._foregroundColorTopLeft = foregroundColor.Start.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorTopRight = foregroundColor.Start.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				this._foregroundColorBottomLeft = foregroundColor.End.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomRight = foregroundColor.End.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				break;
			case FillDirectionEnum.BottomToTop:
				this._backgroundColorBottomLeft = backgroundColor.Start.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomRight = backgroundColor.Start.ColorInfo2?.Abgr ?? 0xFF000000;

				this._backgroundColorTopLeft = backgroundColor.End.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorTopRight = backgroundColor.End.ColorInfo2?.Abgr ?? 0xFF000000;

				this._foregroundColorBottomLeft = foregroundColor.Start.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomRight = foregroundColor.Start.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				this._foregroundColorTopLeft = foregroundColor.End.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorTopRight = foregroundColor.End.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				break;
			case FillDirectionEnum.LeftToRight:
			default:
				this._backgroundColorTopLeft = backgroundColor.Start.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomLeft = backgroundColor.Start.ColorInfo2?.Abgr ?? 0xFF000000;

				this._backgroundColorTopRight = backgroundColor.End.ColorInfo1?.Abgr ?? 0xFF000000;
				this._backgroundColorBottomRight = backgroundColor.End.ColorInfo2?.Abgr ?? 0xFF000000;

				this._foregroundColorTopLeft = foregroundColor.Start.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomLeft = foregroundColor.Start.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				this._foregroundColorTopRight = foregroundColor.End.ColorInfo1?.Abgr ?? 0xFFFFFFFF;
				this._foregroundColorBottomRight = foregroundColor.End.ColorInfo2?.Abgr ?? 0xFFFFFFFF;

				break;
		}

		this._outlineColor = customization.Outline.Color.ColorInfo?.Abgr ?? 0xFF000000;

		if(Utils.IsApproximatelyEqual(opacityScale, 1f))
		{
			return;
		}

		this._backgroundColorTopLeft = Utils.ScaleColorOpacityAbgr(this._backgroundColorTopLeft, opacityScale);
		this._backgroundColorTopRight = Utils.ScaleColorOpacityAbgr(this._backgroundColorTopRight, opacityScale);

		this._backgroundColorBottomRight = Utils.ScaleColorOpacityAbgr(this._backgroundColorBottomRight, opacityScale);
		this._backgroundColorBottomLeft = Utils.ScaleColorOpacityAbgr(this._backgroundColorBottomLeft, opacityScale);

		this._foregroundColorTopLeft = Utils.ScaleColorOpacityAbgr(this._foregroundColorTopLeft, opacityScale);
		this._foregroundColorTopRight = Utils.ScaleColorOpacityAbgr(this._foregroundColorTopRight, opacityScale);

		this._foregroundColorBottomRight = Utils.ScaleColorOpacityAbgr(this._foregroundColorBottomRight, opacityScale);
		this._foregroundColorBottomLeft = Utils.ScaleColorOpacityAbgr(this._foregroundColorBottomLeft, opacityScale);

		this._outlineColor = Utils.ScaleColorOpacityAbgr(this._outlineColor, opacityScale);
	}

	private void Update4()
	{
		var customization = this._customizationAccessor();

		// Background

		this._backgroundTopLeft = new Vector2(this._positionX + this._backgroundShiftX, this._positionY + this._backgroundShiftY);

		this._backgroundBottomRight = new Vector2(this._backgroundTopLeft.X + this._backgroundWidth, this._backgroundTopLeft.Y + this._backgroundHeight);

		// Foreground

		this._foregroundTopLeft = new Vector2(this._positionX + this._foregroundShiftX, this._positionY + this._foregroundShiftY);

		this._foregroundBottomRight = new Vector2(this._foregroundTopLeft.X + this._foregroundWidth, this._foregroundTopLeft.Y + this._foregroundHeight);

		// Outline

		if(customization?.Outline.Thickness > 0f)
		{
			this._outlineTopLeft = new Vector2(this._outlinePositionX, this._outlinePositionY);

			this._outlineBottomRight = new Vector2(this._outlinePositionX + this._outlineWidth, this._outlinePositionY + this._outlineHeight);
		}
	}
}