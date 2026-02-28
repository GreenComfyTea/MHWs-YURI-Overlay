using Hexa.NET.ImGui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class DamageMeterPlayerWidget
{
	private readonly DamageMeterEntity _damageMeterEntity;
	private readonly Func<DamageMeterPlayerWidgetCustomization?> _customizationAccessor;

	private readonly LabelElement _hunterMasterRanksLabelElement;
	private readonly LabelElement _nameLabelElement;
	private readonly DamageMeterDamageComponent _damageComponent;
	private readonly DamageMeterDpsComponent _dpsComponent;

	public DamageMeterPlayerWidget(DamageMeterEntity damageMeterEntity, Func<DamageMeterPlayerWidgetCustomization?> customizationAccessor)
	{
		_damageMeterEntity = damageMeterEntity;
		_customizationAccessor = customizationAccessor;

		_hunterMasterRanksLabelElement = new LabelElement(() => _customizationAccessor()?.HunterMasterRanksLabel);
		_nameLabelElement = new LabelElement(() => _customizationAccessor()?.NameLabel);
		_damageComponent = new DamageMeterDamageComponent(damageMeterEntity, () => _customizationAccessor()?.Damage);
		_dpsComponent = new DamageMeterDpsComponent(damageMeterEntity, () => _customizationAccessor()?.DPS);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position)
	{
		var customization = _customizationAccessor();

		if(customization?.Enabled != true) return;

		_dpsComponent.Draw(backgroundDrawList, position);
		_damageComponent.Draw(backgroundDrawList, position);
		_hunterMasterRanksLabelElement.Draw(backgroundDrawList, position, 1f, _damageMeterEntity.HunterRank, _damageMeterEntity.MasterRank);
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, _damageMeterEntity.Name, _damageMeterEntity.Id, _damageMeterEntity.HunterRank, _damageMeterEntity.MasterRank);
	}
}