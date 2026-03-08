using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterStaticUiCustomization?> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;
	private readonly LargeMonsterStaminaComponent _staminaComponent;
	private readonly LargeMonsterRageComponent _rageComponent;

	public LargeMonsterStaticUi(LargeMonster largeMonster)
	{
		this._largeMonster = largeMonster;
		this._customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		this._nameLabelElement = new LabelElement(() => this._customizationAccessor()?.NameLabel);
		this._healthComponent = new LargeMonsterHealthComponent(largeMonster, () => this._customizationAccessor()?.Health);
		this._staminaComponent = new LargeMonsterStaminaComponent(largeMonster, () => this._customizationAccessor()?.Stamina);
		this._rageComponent = new LargeMonsterRageComponent(largeMonster, () => this._customizationAccessor()?.Rage);
	}

	public void Draw(ImDrawListPtr drawList, int locationIndex)
	{
		var customization = this._customizationAccessor();

		if(customization?.Enabled != true)
		{
			return;
		}

		var spacing = customization.Spacing;
		var anchoredPosition = customization.Position;
		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier ?? 1f;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += (spacing.X ?? 0f) * positionScaleModifier * locationIndex;
		position.Y += (spacing.Y ?? 0f) * positionScaleModifier * locationIndex;

		this._rageComponent.Draw(drawList, position);
		this._staminaComponent.Draw(drawList, position);
		this._healthComponent.Draw(drawList, position);
		this._nameLabelElement.Draw(drawList, position, 1f, this._largeMonster.Name);
	}
}