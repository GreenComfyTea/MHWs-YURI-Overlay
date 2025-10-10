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
			AnchorEnum.TopCenter => new Vector2(
				displayWidth / 2f + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				(anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.TopRight => new Vector2(
				displayWidth + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				(anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.CenterLeft => new Vector2(
				(anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight / 2f + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.Center => new Vector2(
				displayWidth / 2f + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight / 2f + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.CenterRight => new Vector2(
				displayWidth + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight / 2f + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.BottomLeft => new Vector2(
				(anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.BottomCenter => new Vector2(
				displayWidth / 2f + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			AnchorEnum.BottomRight => new Vector2(
				displayWidth + (anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				displayHeight + (anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
			_ => new Vector2(
				(anchoredPositionCustomization.X ?? 0f) * positionScaleModifier,
				(anchoredPositionCustomization.Y ?? 0f) * positionScaleModifier
			),
		};
	}
}