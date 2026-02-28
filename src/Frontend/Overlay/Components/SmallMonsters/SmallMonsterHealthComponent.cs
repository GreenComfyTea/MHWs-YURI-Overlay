using System.Numerics;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class SmallMonsterHealthComponent
{
	private readonly SmallMonster _smallMonster;

	private readonly LabelElement _healthValueLabelElement;
	private readonly LabelElement _healthPercentageLabelElement;
	private readonly BarElement _healthBarElement;

	private readonly Func<SmallMonsterHealthComponentCustomization?> _customizationAccessor;

	public SmallMonsterHealthComponent(SmallMonster smallMonster, Func<SmallMonsterHealthComponentCustomization?> customizationAccessor)
	{
		_smallMonster = smallMonster;

		_customizationAccessor = customizationAccessor;

		_healthValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		_healthPercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		_healthBarElement = new BarElement(() => customizationAccessor()?.Bar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = _customizationAccessor()?.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset?.X ?? 0f), position.Y + sizeScaleModifier * (offset?.Y ?? 0f));

		_healthBarElement.Draw(drawList, offsetPosition, _smallMonster.HealthPercentage, opacityScale);
		_healthPercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, _smallMonster.HealthPercentage);
		_healthValueLabelElement.Draw(drawList, offsetPosition, opacityScale, _smallMonster.Health, _smallMonster.MaxHealth);
	}
}