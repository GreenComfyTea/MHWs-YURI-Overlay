using ImGuiNET;
using System.Numerics;

namespace YURI_Overlay;

internal sealed class EndemicLifeDynamicUi
{
	private readonly EndemicLifeEntity _endemicLifeEntity;
	private readonly Func<EndemicLifeDynamicUiCustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;

	public EndemicLifeDynamicUi(EndemicLifeEntity endemicLifeEntity)
	{
		_endemicLifeEntity = endemicLifeEntity;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.EndemicLifeUI;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		var customization = _customizationAccessor();
		var settings = customization.Settings;

		var entityPosition = _endemicLifeEntity.Position;
		var worldOffset = customization.WorldOffset;

		var targetWorldPosition = new Vector3(
			entityPosition.X + worldOffset.X,
			entityPosition.Y + worldOffset.Y,
			entityPosition.Z + worldOffset.Z
		);

		if(settings.AddMissionBeaconOffsetToWorldOffset)
		{
			targetWorldPosition += _endemicLifeEntity.MissionBeaconOffset;
		}

		if(settings.AddModelRadiusToWorldOffsetY)
		{
			targetWorldPosition.Y += _endemicLifeEntity.ModelRadius;
		}

		var maybeScreenPosition = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition is null)
		{
			return;
		}

		var opacityScale =
			settings.OpacityFalloff && settings.MaxDistance > 0f
			? float.Clamp((settings.MaxDistance - _endemicLifeEntity.Distance) / settings.MaxDistance, 0f, 1f)
			: 1f;

		if(Utils.IsApproximatelyEqual(opacityScale, 0f))
		{
			return;
		}

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		screenPosition.X += customization.Offset.X * positionScaleModifier;
		screenPosition.Y += customization.Offset.Y * positionScaleModifier;

		_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _endemicLifeEntity.Name);

	}
}
