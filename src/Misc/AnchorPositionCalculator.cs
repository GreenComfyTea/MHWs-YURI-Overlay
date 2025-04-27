using System.Numerics;

namespace YURI_Overlay;

internal static class AnchorPositionCalculator
{
	public static Vector2 Convert(AnchoredPositionCustomization anchoredPositionCustomization, float positionScaleModifier = 1f)
	{
		var displaySize = ScreenManager.Instance.WindowSize;

		var displayWidth = displaySize.X;
		var displayHeight = displaySize.Y;

		return anchoredPositionCustomization.Anchor switch
		{
			Anchor.TopCenter => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.TopRight => new Vector2(
				displayWidth + anchoredPositionCustomization.X * positionScaleModifier,
				anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.CenterLeft => new Vector2(
				anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.Center => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.CenterRight => new Vector2(
				displayWidth + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.BottomLeft => new Vector2(
				anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.BottomCenter => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchor.BottomRight => new Vector2(
				displayWidth + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight + anchoredPositionCustomization.Y * positionScaleModifier
			),
			_ => new Vector2(
				anchoredPositionCustomization.X * positionScaleModifier,
				anchoredPositionCustomization.Y * positionScaleModifier
			),
		};
	}
}