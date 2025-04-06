using ImGuiNET;
using System.Numerics;

namespace YURI_Overlay;

internal sealed class SmallMonsterDynamicUi
{
	private readonly SmallMonster _largeMonster;
	private readonly Func<SmallMonsterDynamicUiCustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly SmallMonsterHealthComponent _healthComponent;

	public SmallMonsterDynamicUi(SmallMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
		_healthComponent = new SmallMonsterHealthComponent(largeMonster, () => _customizationAccessor().Health);
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		var customization = _customizationAccessor();
		var settings = customization.Settings;

		var monsterPosition = _largeMonster.Position;
		var worldOffset = customization.WorldOffset;

		var targetWorldPosition = new Vector3(
			monsterPosition.X + worldOffset.X,
			monsterPosition.Y + worldOffset.Y,
			monsterPosition.Z + worldOffset.Z
		);

		if(settings.AddMissionBeaconOffsetToWorldOffset)
		{
			targetWorldPosition += _largeMonster.MissionBeaconOffset;
		}

		if(settings.AddModelRadiusToWorldOffsetY)
		{
			targetWorldPosition.Y += _largeMonster.ModelRadius;
		}

		var maybeScreenPosition = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition is null)
		{
			return;
		}

		var opacityScale =
			settings.OpacityFalloff && settings.MaxDistance > 0f
			? float.Clamp((settings.MaxDistance - _largeMonster.Distance) / settings.MaxDistance, 0f, 1f)
			: 1f;

		if(Utils.IsApproximatelyEqual(opacityScale, 0f))
		{
			return;
		}

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		screenPosition.X += customization.Offset.X * positionScaleModifier;
		screenPosition.Y += customization.Offset.Y * positionScaleModifier;

		_healthComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _largeMonster.Name);

	}
}
