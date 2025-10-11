using System.Numerics;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterDynamicUiCustomization?> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;
	private readonly LargeMonsterStaminaComponent _staminaComponent;
	private readonly LargeMonsterRageComponent _rageComponent;

	public LargeMonsterDynamicUi(LargeMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;

		_nameLabelElement = new LabelElement(() => _customizationAccessor()?.NameLabel);
		_healthComponent = new LargeMonsterHealthComponent(largeMonster, () => _customizationAccessor()?.Health);
		_staminaComponent = new LargeMonsterStaminaComponent(largeMonster, () => _customizationAccessor()?.Stamina);
		_rageComponent = new LargeMonsterRageComponent(largeMonster, () => _customizationAccessor()?.Rage);
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		var customization = _customizationAccessor();

		if(customization?.Enabled != true) return;

		var settings = customization.Settings;

		var monsterPosition = _largeMonster.Position;
		var worldOffset = customization.WorldOffset;

		var targetWorldPosition = new Vector3(
			monsterPosition.X + (worldOffset.X ?? 0f),
			monsterPosition.Y + (worldOffset.Y ?? 0f),
			monsterPosition.Z + (worldOffset.Z ?? 0f)
		);

		if(settings.AddMissionBeaconOffsetToWorldOffset == true) targetWorldPosition += _largeMonster.MissionBeaconOffset;

		if(settings.AddModelRadiusToWorldOffsetY == true) targetWorldPosition.Y += _largeMonster.ModelRadius;

		var maybeScreenPosition = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition is null) return;

		var maxDistance = settings.MaxDistance ?? 0f;

		var opacityScale =
			settings.OpacityFalloff == true && maxDistance > 0f
				? float.Clamp((maxDistance - _largeMonster.Distance) / maxDistance, 0f, 1f)
				: 1f;

		if(Utils.IsApproximatelyEqual(opacityScale, 0f)) return;

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		screenPosition.X += (customization.Offset.X ?? 0f) * positionScaleModifier;
		screenPosition.Y += (customization.Offset.Y ?? 0f) * positionScaleModifier;

		_rageComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_staminaComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_healthComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _largeMonster.Name);
	}
}