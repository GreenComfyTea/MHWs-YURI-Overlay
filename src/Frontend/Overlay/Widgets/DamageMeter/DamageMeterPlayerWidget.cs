using System.Numerics;
using Hexa.NET.ImGui;

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
		this._damageMeterEntity = damageMeterEntity;
		this._customizationAccessor = customizationAccessor;

		this._hunterMasterRanksLabelElement = new LabelElement(() => this._customizationAccessor()?.HunterMasterRanksLabel);
		this._nameLabelElement = new LabelElement(() => this._customizationAccessor()?.NameLabel);
		this._damageComponent = new DamageMeterDamageComponent(damageMeterEntity, () => this._customizationAccessor()?.Damage);
		this._dpsComponent = new DamageMeterDpsComponent(damageMeterEntity, () => this._customizationAccessor()?.DPS);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position)
	{
		var customization = this._customizationAccessor();

		if(customization?.Enabled != true)
		{
			return;
		}

		this._dpsComponent.Draw(drawList, position);
		this._damageComponent.Draw(drawList, position);
		this._hunterMasterRanksLabelElement.Draw(drawList, position, 1f, this._damageMeterEntity.HunterRank, this._damageMeterEntity.MasterRank);
		this._nameLabelElement.Draw(drawList, position, 1f, this._damageMeterEntity.Name, this._damageMeterEntity.Id, this._damageMeterEntity.HunterRank, this._damageMeterEntity.MasterRank);
	}
}