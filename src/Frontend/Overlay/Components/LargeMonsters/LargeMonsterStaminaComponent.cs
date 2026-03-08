using System.Numerics;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaminaComponent
{
	private readonly LargeMonster _largeMonster;

	private readonly LabelElement _staminaValueLabelElement;
	private readonly LabelElement _staminaPercentageLabelElement;
	private readonly BarElement _staminaBarElement;
	private readonly LabelElement _staminaTimerLabelElement;
	private readonly BarElement _staminaTimerBarElement;

	private readonly Func<LargeMonsterStaminaComponentCustomization?> _customizationAccessor;

	public LargeMonsterStaminaComponent(LargeMonster largeMonster, Func<LargeMonsterStaminaComponentCustomization?> customizationAccessor)
	{
		this._largeMonster = largeMonster;

		this._customizationAccessor = customizationAccessor;

		this._staminaValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		this._staminaPercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		this._staminaTimerLabelElement = new LabelElement(() => customizationAccessor()?.TimerLabel);
		this._staminaBarElement = new BarElement(() => customizationAccessor()?.Bar);
		this._staminaTimerBarElement = new BarElement(() => customizationAccessor()?.TimerBar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		if(!this._largeMonster.IsStaminaValid)
		{
			return;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = this._customizationAccessor()?.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset?.X ?? 0f), position.Y + sizeScaleModifier * (offset?.Y ?? 0f));

		if(this._largeMonster.IsTired)
		{
			this._staminaTimerBarElement.Draw(drawList, offsetPosition, this._largeMonster.StaminaRemainingTimerPercentage, opacityScale);
			this._staminaTimerLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.StaminaRemainingTimerString);

			return;
		}

		this._staminaBarElement.Draw(drawList, offsetPosition, this._largeMonster.StaminaPercentage, opacityScale);
		this._staminaPercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.StaminaPercentage);
		this._staminaValueLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.Stamina, this._largeMonster.MaxStamina);
	}
}