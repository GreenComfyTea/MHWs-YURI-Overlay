using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterDamageComponent
{
	private readonly DamageMeterEntity _damageMeterEntity;

	private readonly LabelElement _damageValueLabelElement;
	private readonly LabelElement _damagePercentageLabelElement;
	private readonly BarElement _damageBarElement;

	private readonly Func<DamageMeterDamageComponentCustomization?> _customizationAccessor;

	public DamageMeterDamageComponent(DamageMeterEntity damageMeterEntity, Func<DamageMeterDamageComponentCustomization?> customizationAccessor)
	{
		_damageMeterEntity = damageMeterEntity;
		_customizationAccessor = customizationAccessor;

		_damageValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		_damagePercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		_damageBarElement = new BarElement(() => customizationAccessor()?.Bar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(customization?.Visible != true) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset.X ?? 0f), position.Y + sizeScaleModifier * (offset.Y ?? 0f));

		_damageBarElement.Draw(drawList, offsetPosition, _damageMeterEntity.DisplayedDamagePercentage, opacityScale);
		_damagePercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, _damageMeterEntity.DisplayedDamagePercentage);
		_damageValueLabelElement.Draw(drawList, offsetPosition, opacityScale, _damageMeterEntity.DisplayedDamage);
	}
}