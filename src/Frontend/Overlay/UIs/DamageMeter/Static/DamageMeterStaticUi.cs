using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUi
{
	private readonly Func<DamageMeterStaticUiCustomization> _customizationAccessor;

	private readonly DamageMeterPlayerWidget _playerWidget;

	public DamageMeterStaticUi(int type = 0)
	{
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;

		if(type == 0)
		{
			_playerWidget = new DamageMeterPlayerWidget(() => _customizationAccessor().LocalPlayer);
			return;
		}

		if(type == 1)
		{
			_playerWidget = new DamageMeterPlayerWidget(() => _customizationAccessor().OtherPlayers);
			return;
		}

		if(type == 2)
		{
			_playerWidget = new DamageMeterPlayerWidget(() => _customizationAccessor().SupportHunters);
		}
	}

	public void Draw(ImDrawListPtr backgroundDrawList, int locationIndex)
	{
		var customization = _customizationAccessor();

		if(!customization.Enabled) return;

		var spacing = customization.Spacing;

		var anchoredPosition = customization.Position;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += spacing.X * positionScaleModifier * locationIndex;
		position.Y += spacing.Y * positionScaleModifier * locationIndex;

		_playerWidget.Draw(backgroundDrawList, position);
	}
}