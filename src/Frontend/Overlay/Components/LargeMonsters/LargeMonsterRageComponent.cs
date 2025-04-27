using System.Numerics;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterRageComponent
{
	private readonly LargeMonster _largeMonster;

	private readonly LabelElement _rageValueLabelElement;
	private readonly LabelElement _ragePercentageLabelElement;
	private readonly BarElement _rageBarElement;
	private readonly LabelElement _rageTimerLabelElement;
	private readonly BarElement _rageTimerBarElement;

	private readonly Func<LargeMonsterRageComponentCustomization> _customizationAccessor;

	public LargeMonsterRageComponent(LargeMonster largeMonster, Func<LargeMonsterRageComponentCustomization> customizationAccessor)
	{
		_largeMonster = largeMonster;

		_customizationAccessor = customizationAccessor;

		_rageValueLabelElement = new LabelElement(() => customizationAccessor().ValueLabel);
		_ragePercentageLabelElement = new LabelElement(() => customizationAccessor().PercentageLabel);
		_rageBarElement = new BarElement(() => customizationAccessor().Bar);
		_rageTimerLabelElement = new LabelElement(() => customizationAccessor().TimerLabel);
		_rageTimerBarElement = new BarElement(() => customizationAccessor().TimerBar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		if(!_largeMonster.IsRageValid) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier;

		var offset = _customizationAccessor().Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * offset.X, position.Y + sizeScaleModifier * offset.Y);

		if(_largeMonster.IsEnraged)
		{
			_rageTimerBarElement.Draw(backgroundDrawList, offsetPosition, _largeMonster.RageRemainingTimerPercentage, opacityScale);
			_rageTimerLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.RageRemainingTimerString);
			return;
		}

		_rageBarElement.Draw(backgroundDrawList, offsetPosition, _largeMonster.RagePercentage, opacityScale);
		_ragePercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.RagePercentage);
		_rageValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.Rage, _largeMonster.MaxRage);
	}
}