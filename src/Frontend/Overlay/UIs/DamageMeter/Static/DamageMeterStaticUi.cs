using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUi
{
	private readonly Func<DamageMeterStaticUiCustomization?> _customizationAccessor;

	private readonly DamageMeterPlayerWidget? _playerWidget;

	public DamageMeterStaticUi(DamageMeterEntity damageMeterEntity)
	{
		//_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data..DamageMeter;
		_customizationAccessor = () => new DamageMeterStaticUiCustomization();

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.LocalPlayer)
		{
			_playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => _customizationAccessor()?.LocalPlayer);
			return;
		}

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.OtherPlayer)
		{
			_playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => _customizationAccessor()?.OtherPlayers);
			return;
		}

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.SupportHunter)
		{
			_playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => _customizationAccessor()?.SupportHunters);
		}
	}

	public void Draw(ImDrawListPtr drawList, int locationIndex)
	{
		var customization = _customizationAccessor.Invoke();

		if(customization?.Enabled != true) return;

		var spacing = customization.Spacing;

		var anchoredPosition = customization.Position;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += spacing.X ?? 0f * positionScaleModifier * locationIndex;
		position.Y += spacing.Y ?? 0f * positionScaleModifier * locationIndex;

		_playerWidget?.Draw(drawList, position);
	}
}