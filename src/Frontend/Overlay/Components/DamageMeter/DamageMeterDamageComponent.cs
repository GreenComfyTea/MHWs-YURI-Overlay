using System.Numerics;
using Hexa.NET.ImGui;

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
		this._damageMeterEntity = damageMeterEntity;
		this._customizationAccessor = customizationAccessor;

		this._damageValueLabelElement = new LabelElement(() => customizationAccessor()?.ValueLabel);
		this._damagePercentageLabelElement = new LabelElement(() => customizationAccessor()?.PercentageLabel);
		this._damageBarElement = new BarElement(() => customizationAccessor()?.Bar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = this._customizationAccessor();

		if(customization?.Visible != true)
		{
			return;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset.X ?? 0f), position.Y + sizeScaleModifier * (offset.Y ?? 0f));

		this._damageBarElement.Draw(drawList, offsetPosition, this._damageMeterEntity.DisplayedDamagePercentage, opacityScale);
		this._damagePercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, this._damageMeterEntity.DisplayedDamagePercentage);
		this._damageValueLabelElement.Draw(drawList, offsetPosition, opacityScale, this._damageMeterEntity.DisplayedDamage);
	}
}