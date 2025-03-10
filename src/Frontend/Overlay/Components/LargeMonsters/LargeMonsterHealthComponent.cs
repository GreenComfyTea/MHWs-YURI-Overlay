﻿using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponent
{
	private readonly LargeMonster _largeMonster;

	private readonly LabelElement _healthValueLabelElement;
	private readonly LabelElement _healthPercentageLabelElement;
	private readonly BarElement _healthBarElement;

	private readonly Func<LargeMonsterHealthComponentCustomization> customizationAccessor;

	public LargeMonsterHealthComponent(LargeMonster largeMonster, Func<LargeMonsterHealthComponentCustomization> customizationAccessor)
	{
		_largeMonster = largeMonster;

		this.customizationAccessor = customizationAccessor;

		_healthValueLabelElement = new LabelElement(() => customizationAccessor().ValueLabel);
		_healthPercentageLabelElement = new LabelElement(() => customizationAccessor().PercentageLabel);
		_healthBarElement = new BarElement(() => customizationAccessor().Bar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		var offset = customizationAccessor().Offset;
		var offsetPosition = new Vector2(position.X + offset.X, position.Y + offset.Y);

		_healthBarElement.Draw(backgroundDrawList, offsetPosition, _largeMonster.HealthPercentage, opacityScale);
		_healthPercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.HealthPercentage);
		_healthValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _largeMonster.Health, _largeMonster.MaxHealth);
	}
}
