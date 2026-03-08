using System.Numerics;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LargeMonsterRageComponent
{
	private readonly LargeMonster _largeMonster;

	private readonly LabelElement _rageValueLabelElement;
	private readonly LabelElement _ragePercentageLabelElement;
	private readonly BarElement _rageBarElement;
	private readonly LabelElement _rageTimerLabelElement;
	private readonly BarElement _rageTimerBarElement;

	private readonly Func<LargeMonsterRageComponentCustomization?> _customizationAccessor;

	public LargeMonsterRageComponent(LargeMonster largeMonster, Func<LargeMonsterRageComponentCustomization?> customizationAccessor)
	{
		this._largeMonster = largeMonster;

		this._customizationAccessor = customizationAccessor;

		this._rageValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		this._ragePercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		this._rageBarElement = new BarElement(() => customizationAccessor()?.Bar);
		this._rageTimerLabelElement = new LabelElement(() => customizationAccessor()?.TimerLabel);
		this._rageTimerBarElement = new BarElement(() => customizationAccessor()?.TimerBar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		if(!this._largeMonster.IsRageValid)
		{
			return;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = this._customizationAccessor()?.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset?.X ?? 0f), position.Y + sizeScaleModifier * (offset?.Y ?? 0f));

		if(this._largeMonster.IsEnraged)
		{
			this._rageTimerBarElement.Draw(drawList, offsetPosition, this._largeMonster.RageRemainingTimerPercentage, opacityScale);
			this._rageTimerLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.RageRemainingTimerString);

			return;
		}

		this._rageBarElement.Draw(drawList, offsetPosition, this._largeMonster.RagePercentage, opacityScale);
		this._ragePercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.RagePercentage);
		this._rageValueLabelElement.Draw(drawList, offsetPosition, opacityScale, this._largeMonster.Rage, this._largeMonster.MaxRage);
	}
}