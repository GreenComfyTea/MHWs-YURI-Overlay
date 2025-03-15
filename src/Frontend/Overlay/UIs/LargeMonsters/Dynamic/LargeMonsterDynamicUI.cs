using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterDynamicUiCustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;
	private readonly LargeMonsterStaminaComponent _staminaComponent;
	private readonly LargeMonsterRageComponent _rageComponent;

	public LargeMonsterDynamicUi(LargeMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
		_healthComponent = new LargeMonsterHealthComponent(largeMonster, () => _customizationAccessor().Health);
		_staminaComponent = new LargeMonsterStaminaComponent(largeMonster, () => _customizationAccessor().Stamina);
		_rageComponent = new LargeMonsterRageComponent(largeMonster, () => _customizationAccessor().Rage);
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

		var (maybeScreenPosition, _) = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition == null)
		{
			return;
		}

		var opacityScale =
			settings.OpacityFalloff && settings.MaxDistance > 0f
			? (settings.MaxDistance - _largeMonster.Distance) / settings.MaxDistance
			: 1f;

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		screenPosition.X += customization.Offset.X * positionScaleModifier;
		screenPosition.Y += customization.Offset.Y * positionScaleModifier;

		_rageComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_staminaComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_healthComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _largeMonster.Name);

	}
}
