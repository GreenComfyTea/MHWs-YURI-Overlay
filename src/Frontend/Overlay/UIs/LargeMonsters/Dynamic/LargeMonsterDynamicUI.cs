using System.Numerics;
using Hexa.NET.ImGui;

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
		this._largeMonster = largeMonster;
		this._customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;

		this._nameLabelElement = new LabelElement(() => this._customizationAccessor()?.NameLabel);
		this._healthComponent = new LargeMonsterHealthComponent(largeMonster, () => this._customizationAccessor()?.Health);
		this._staminaComponent = new LargeMonsterStaminaComponent(largeMonster, () => this._customizationAccessor()?.Stamina);
		this._rageComponent = new LargeMonsterRageComponent(largeMonster, () => this._customizationAccessor()?.Rage);
	}

	public void Draw(ImDrawListPtr drawList)
	{
		var customization = this._customizationAccessor();

		if(customization?.Enabled != true)
		{
			return;
		}

		var settings = customization.Settings;

		var monsterPosition = this._largeMonster.Position;
		var worldOffset = customization.WorldOffset;

		var targetWorldPosition = new Vector3(monsterPosition.X + (worldOffset.X ?? 0f), monsterPosition.Y + (worldOffset.Y ?? 0f), monsterPosition.Z + (worldOffset.Z ?? 0f));

		if(settings.AddMissionBeaconOffsetToWorldOffset == true)
		{
			targetWorldPosition += this._largeMonster.MissionBeaconOffset;
		}

		if(settings.AddModelRadiusToWorldOffsetY == true)
		{
			targetWorldPosition.Y += this._largeMonster.ModelRadius;
		}

		var maybeScreenPosition = ScreenManager.Instance.ConvertWorldPositionToScreenPosition(targetWorldPosition);

		// Not on screen
		if(maybeScreenPosition is null)
		{
			return;
		}

		var maxDistance = settings.MaxDistance ?? 0f;

		var opacityScale = settings.OpacityFalloff == true && maxDistance > 0f ? float.Clamp((maxDistance - this._largeMonster.Distance) / maxDistance, 0f, 1f) : 1f;

		if(Utils.IsApproximatelyEqual(opacityScale, 0f))
		{
			return;
		}

		var screenPosition = (Vector2) maybeScreenPosition;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		screenPosition.X += (customization.Offset.X ?? 0f) * positionScaleModifier;
		screenPosition.Y += (customization.Offset.Y ?? 0f) * positionScaleModifier;

		this._rageComponent.Draw(drawList, screenPosition, opacityScale);
		this._staminaComponent.Draw(drawList, screenPosition, opacityScale);
		this._healthComponent.Draw(drawList, screenPosition, opacityScale);
		this._nameLabelElement.Draw(drawList, screenPosition, opacityScale, this._largeMonster.Name);
	}
}