using System.Numerics;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class EndemicLifeDynamicUi
{
	private readonly EndemicLifeEntity _endemicLifeEntity;
	private readonly Func<EndemicLifeDynamicUiCustomization?> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;

	public EndemicLifeDynamicUi(EndemicLifeEntity endemicLifeEntity)
	{
		_endemicLifeEntity = endemicLifeEntity;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig?.Data?.EndemicLifeUI;

		_nameLabelElement = new LabelElement(() => _customizationAccessor()?.NameLabel);
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		var customization = _customizationAccessor();

		if(customization?.Enabled != true) return;

		var settings = customization.Settings;

		var entityPosition = _endemicLifeEntity.Position;
		var worldOffset = customization.WorldOffset;

		var targetWorldPosition = new Vector3(
			entityPosition.X + worldOffset.X ?? 0f,
			entityPosition.Y + worldOffset.Y ?? 0f,
			entityPosition.Z + worldOffset.Z ?? 0f
		);

		if(settings.AddModelRadiusToWorldOffsetY == true) targetWorldPosition.Y += _endemicLifeEntity.ModelRadius;

		var maybeScreenPosition = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition is null) return;

		var maxDistance = settings.MaxDistance ?? 0f;

		var opacityScale =
			settings.OpacityFalloff == true && maxDistance > 0f
				? float.Clamp((maxDistance - _endemicLifeEntity.Distance) / maxDistance, 0f, 1f)
				: 1f;

		if(Utils.IsApproximatelyEqual(opacityScale, 0f)) return;

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig?.Data?.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		screenPosition.X += (customization.Offset.X ?? 0f) * positionScaleModifier;
		screenPosition.Y += (customization.Offset.Y ?? 0f) * positionScaleModifier;

		_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _endemicLifeEntity.Name);
	}
}