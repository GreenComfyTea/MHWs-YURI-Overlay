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
	private readonly LabelElement _dpsValueLabelElement;
	private readonly LabelElement _dpsPercentageLabelElement;
	private readonly BarElement _dpsBarElement;

	private readonly Func<DamageMeterDpsComponentCustomization> _customizationAccessor;

	public DamageMeterDpsComponent(Func<DamageMeterDpsComponentCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;

		_dpsValueLabelElement = new LabelElement(() => customizationAccessor().ValueLabel);
		_dpsPercentageLabelElement = new LabelElement(() => customizationAccessor().PercentageLabel);
		_dpsBarElement = new BarElement(() => customizationAccessor().Bar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(!customization.Visible) return;

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * offset.X, position.Y + sizeScaleModifier * offset.Y);

		_dpsBarElement.Draw(backgroundDrawList, offsetPosition, 0.69f, opacityScale);
		_dpsPercentageLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, 8.888f);
		_dpsValueLabelElement.Draw(backgroundDrawList, offsetPosition, opacityScale, 8888.8f);
	}
}