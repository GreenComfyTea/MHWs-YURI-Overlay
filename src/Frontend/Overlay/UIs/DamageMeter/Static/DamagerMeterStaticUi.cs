using ImGuiNET;

namespace YURI_Overlay;

internal sealed class DamageMeterStaticUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<DamageMeterStaticUiCustomization> _customizationAccessor;

	private readonly LabelElement _hunterMasterRanksLabelElement;
	private readonly LabelElement _nameLabelElement;
	private readonly LabelElement _dpsLabelElement;
	private readonly LabelElement _damageLabelElement;
	private readonly LabelElement _damagePercentageLabelElement;

	private readonly BarElement _dpsBar;
	private readonly BarElement _damageBar;

	public DamageMeterStaticUi(LargeMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;

		static DamageMeterPlayerUiCustomization LocalPlayerCustomizationAccessor() => ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI.LocalPlayer;

		_hunterMasterRanksLabelElement = new LabelElement(() => LocalPlayerCustomizationAccessor().HunterMasterRanksLabel);
		_nameLabelElement = new LabelElement(() => LocalPlayerCustomizationAccessor().NameLabel);
		_dpsLabelElement = new LabelElement(() => LocalPlayerCustomizationAccessor().DpsLabel);
		_damageLabelElement = new LabelElement(() => LocalPlayerCustomizationAccessor().DamageLabel);
		_damagePercentageLabelElement = new LabelElement(() => LocalPlayerCustomizationAccessor().DamagePercentageLabel);

		_dpsBar = new BarElement(() => LocalPlayerCustomizationAccessor().DpsBar);
		_damageBar = new BarElement(() => LocalPlayerCustomizationAccessor().DamageBar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, int locationIndex)
	{
		var customization = _customizationAccessor();

		var spacing = customization.Spacing;

		var anchoredPosition = customization.Position;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += spacing.X * positionScaleModifier * locationIndex;
		position.Y += spacing.Y * positionScaleModifier * locationIndex;

		_damagePercentageLabelElement.Draw(backgroundDrawList, position, 1f, "69%");
		_damageLabelElement.Draw(backgroundDrawList, position, 1f, "6969");
		_dpsLabelElement.Draw(backgroundDrawList, position, 1f, "69.69");
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, "Local player");
		_hunterMasterRanksLabelElement.Draw(backgroundDrawList, position, 1f, "[069:069]");
	}
}