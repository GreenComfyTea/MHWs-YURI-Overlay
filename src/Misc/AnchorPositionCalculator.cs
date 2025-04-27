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
			Anchors.TopCenter => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.TopRight => new Vector2(
				displayWidth + anchoredPositionCustomization.X * positionScaleModifier,
				anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.CenterLeft => new Vector2(
				anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.Center => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.CenterRight => new Vector2(
				displayWidth + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight / 2f + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.BottomLeft => new Vector2(
				anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.BottomCenter => new Vector2(
				displayWidth / 2f + anchoredPositionCustomization.X * positionScaleModifier,
				displayHeight + anchoredPositionCustomization.Y * positionScaleModifier
			),
			Anchors.BottomRight => new Vector2(
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