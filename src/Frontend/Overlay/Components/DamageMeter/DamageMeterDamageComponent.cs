using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterDamageComponent
{
	private readonly LabelElement _damageValueLabelElement;
	private readonly LabelElement _damagePercentageLabelElement;
	private readonly BarElement _damageBarElement;

	private readonly Func<DamageMeterDamageComponentCustomization> _customizationAccessor;

	public DamageMeterDamageComponent(Func<DamageMeterDamageComponentCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;

		_damageValueLabelElement = new LabelElement(() => customizationAccessor().ValueLabel);
		_damagePercentageLabelElement = new LabelElement(() => customizationAccessor().PercentageLabel);
		_damageBarElement = new BarElement(() => customizationAccessor().Bar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(!customization.Visible) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * offset.X, position.Y + sizeScaleModifier * offset.Y);

		_damageBarElement.Draw(backgroundDrawList, offsetPosition, 0.69f, opacityScale);
		_damagePercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, 8.888f);
		_damageValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, 8888888.8f);
	}
}