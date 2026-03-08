using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUi
{
	private readonly Func<DamageMeterStaticUiCustomization?> _customizationAccessor;

	private readonly DamageMeterPlayerWidget? _playerWidget;

	public DamageMeterStaticUi(DamageMeterEntity damageMeterEntity)
	{
		//_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data..DamageMeter;
		this._customizationAccessor = () => new DamageMeterStaticUiCustomization();

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.LocalPlayer)
		{
			this._playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => this._customizationAccessor()?.LocalPlayer);

			return;
		}

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.OtherPlayer)
		{
			this._playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => this._customizationAccessor()?.OtherPlayers);

			return;
		}

		if(damageMeterEntity.Type == DamageMeterEntityTypeEnum.SupportHunter)
		{
			this._playerWidget = new DamageMeterPlayerWidget(damageMeterEntity, () => this._customizationAccessor()?.SupportHunters);
		}
	}

	public void Draw(ImDrawListPtr drawList, int locationIndex)
	{
		var customization = this._customizationAccessor.Invoke();

		if(customization?.Enabled != true)
		{
			return;
		}

		var spacing = customization.Spacing;

		var anchoredPosition = customization.Position;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += spacing.X ?? 0f * positionScaleModifier * locationIndex;
		position.Y += spacing.Y ?? 0f * positionScaleModifier * locationIndex;

		this._playerWidget?.Draw(drawList, position);
	}
}