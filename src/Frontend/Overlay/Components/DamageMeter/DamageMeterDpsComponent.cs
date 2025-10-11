using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterDpsComponent
{
	private readonly DamageMeterEntity _damageMeterEntity;

	private readonly LabelElement _dpsValueLabelElement;
	private readonly LabelElement _dpsPercentageLabelElement;
	private readonly BarElement _dpsBarElement;

	private readonly Func<DamageMeterDpsComponentCustomization?>? _customizationAccessor;

	public DamageMeterDpsComponent(DamageMeterEntity damageMeterEntity, Func<DamageMeterDpsComponentCustomization?>? customizationAccessor)
	{
		_damageMeterEntity = damageMeterEntity;
		_customizationAccessor = customizationAccessor;

		_dpsValueLabelElement = new LabelElement(() => customizationAccessor?.Invoke()?.ValueLabel);
		_dpsPercentageLabelElement = new LabelElement(() => customizationAccessor?.Invoke()?.PercentageLabel);
		_dpsBarElement = new BarElement(() => customizationAccessor?.Invoke()?.Bar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = _customizationAccessor?.Invoke();

		if(customization?.Visible != true) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset.X ?? 0f), position.Y + sizeScaleModifier * (offset.Y ?? 0f));

		_dpsBarElement.Draw(backgroundDrawList, offsetPosition, _damageMeterEntity.DisplayedDpsPercentage, opacityScale);
		_dpsPercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _damageMeterEntity.DisplayedDpsPercentage);
		_dpsValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, _damageMeterEntity.DisplayedDps);
	}
}