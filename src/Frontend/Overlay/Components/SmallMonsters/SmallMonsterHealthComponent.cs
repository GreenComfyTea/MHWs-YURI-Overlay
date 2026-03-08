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
		this._smallMonster = smallMonster;

		this._customizationAccessor = customizationAccessor;

		this._healthValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		this._healthPercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		this._healthBarElement = new BarElement(() => customizationAccessor()?.Bar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = this._customizationAccessor()?.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset?.X ?? 0f), position.Y + sizeScaleModifier * (offset?.Y ?? 0f));

		this._healthBarElement.Draw(drawList, offsetPosition, this._smallMonster.HealthPercentage, opacityScale);
		this._healthPercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, this._smallMonster.HealthPercentage);
		this._healthValueLabelElement.Draw(drawList, offsetPosition, opacityScale, this._smallMonster.Health, this._smallMonster.MaxHealth);
	}
}