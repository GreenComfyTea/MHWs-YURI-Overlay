using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterPlayerWidget
{
	private readonly Func<DamageMeterPlayerWidgetCustomization> _customizationAccessor;

	private readonly LabelElement _hunterMasterRanksLabelElement;
	private readonly LabelElement _nameLabelElement;
	private readonly DamageMeterDamageComponent _damageComponent;
	private readonly DamageMeterDpsComponent _dpsComponent;

	public DamageMeterPlayerWidget(Func<DamageMeterPlayerWidgetCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;

		_hunterMasterRanksLabelElement = new LabelElement(() => _customizationAccessor().HunterMasterRanksLabel);
		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
		_damageComponent = new DamageMeterDamageComponent(() => _customizationAccessor().Damage);
		_dpsComponent = new DamageMeterDpsComponent(() => _customizationAccessor().DPS);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position)
	{
		var customization = _customizationAccessor();

		if(!customization.Enabled) return;

		_dpsComponent.Draw(backgroundDrawList, position);
		_damageComponent.Draw(backgroundDrawList, position);
		_hunterMasterRanksLabelElement.Draw(backgroundDrawList, position, 1f, 888, 888);
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, "WWWWWWWWWWWWWWW", 888, 888);
	}
}