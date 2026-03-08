using System.Numerics;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class SmallMonsterDynamicUi
{
	private readonly SmallMonster _largeMonster;
	private readonly Func<SmallMonsterDynamicUiCustomization?> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly SmallMonsterHealthComponent _healthComponent;

	public SmallMonsterDynamicUi(SmallMonster largeMonster)
	{
		this._largeMonster = largeMonster;
		this._customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;

		this._nameLabelElement = new LabelElement(() => this._customizationAccessor()?.NameLabel);
		this._healthComponent = new SmallMonsterHealthComponent(largeMonster, () => this._customizationAccessor()?.Health);
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

		this._healthComponent.Draw(drawList, screenPosition, opacityScale);
		this._nameLabelElement.Draw(drawList, screenPosition, opacityScale, this._largeMonster.Name);
	}
}