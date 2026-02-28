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
		_largeMonster = largeMonster;

		_customizationAccessor = customizationAccessor;

		_staminaValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		_staminaPercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		_staminaTimerLabelElement = new LabelElement(() => customizationAccessor()?.TimerLabel);
		_staminaBarElement = new BarElement(() => customizationAccessor()?.Bar);
		_staminaTimerBarElement = new BarElement(() => customizationAccessor()?.TimerBar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		if(!_largeMonster.IsStaminaValid) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = _customizationAccessor()?.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset?.X ?? 0f), position.Y + sizeScaleModifier * (offset?.Y ?? 0f));

		if(_largeMonster.IsTired)
		{
			_staminaTimerBarElement.Draw(backgroundDrawList, offsetPosition, _largeMonster.StaminaRemainingTimerPercentage, opacityScale);
			_staminaTimerLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.StaminaRemainingTimerString);
			return;
		}

		_staminaBarElement.Draw(backgroundDrawList, offsetPosition, _largeMonster.StaminaPercentage, opacityScale);
		_staminaPercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.StaminaPercentage);
		_staminaValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.Stamina, _largeMonster.MaxStamina);
	}
}