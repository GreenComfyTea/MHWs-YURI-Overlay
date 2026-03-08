using System.Numerics;
using Hexa.NET.ImGui;

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
		this._damageMeterEntity = damageMeterEntity;
		this._customizationAccessor = customizationAccessor;

		this._dpsValueLabelElement = new LabelElement(() => customizationAccessor?.Invoke()?.ValueLabel);
		this._dpsPercentageLabelElement = new LabelElement(() => customizationAccessor?.Invoke()?.PercentageLabel);
		this._dpsBarElement = new BarElement(() => customizationAccessor?.Invoke()?.Bar);
	}

	public void Draw(ImDrawListPtr drawList, Vector2 position, float opacityScale = 1f)
	{
		var customization = this._customizationAccessor?.Invoke();

		if(customization?.Visible != true)
		{
			return;
		}

		var sizeScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.SizeScaleModifier ?? 1f;

		var offset = customization.Offset;
		var offsetPosition = new Vector2(position.X + sizeScaleModifier * (offset.X ?? 0f), position.Y + sizeScaleModifier * (offset.Y ?? 0f));

		this._dpsBarElement.Draw(drawList, offsetPosition, this._damageMeterEntity.DisplayedDpsPercentage, opacityScale);
		this._dpsPercentageLabelElement.Draw(drawList, offsetPosition, opacityScale, this._damageMeterEntity.DisplayedDpsPercentage);
		this._dpsValueLabelElement.Draw(drawList, offsetPosition, opacityScale, this._damageMeterEntity.DisplayedDps);
	}
}