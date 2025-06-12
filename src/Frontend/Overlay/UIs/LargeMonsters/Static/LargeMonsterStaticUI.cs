using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterStaticUiCustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;
	private readonly LargeMonsterStaminaComponent _staminaComponent;
	private readonly LargeMonsterRageComponent _rageComponent;

	public LargeMonsterStaticUi(LargeMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
		_healthComponent = new LargeMonsterHealthComponent(largeMonster, () => _customizationAccessor().Health);
		_staminaComponent = new LargeMonsterStaminaComponent(largeMonster, () => _customizationAccessor().Stamina);
		_rageComponent = new LargeMonsterRageComponent(largeMonster, () => _customizationAccessor().Rage);
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

		_rageComponent.Draw(backgroundDrawList, position);
		_staminaComponent.Draw(backgroundDrawList, position);
		_healthComponent.Draw(backgroundDrawList, position);
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, _largeMonster.Name);
	}
}