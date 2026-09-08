// ReSharper disable InconsistentNaming

namespace YURI_Overlay;

internal sealed class Config
{
	public EndemicLifeDynamicUiCustomization EndemicLifeUI = new();
	public GlobalSettingsCustomization GlobalSettings = new();
	public LargeMonsterUiCustomization LargeMonsterUI = new();

	public SmallMonsterDynamicUiCustomization SmallMonsterUI = new();
	//public DamageMeterStaticUiCustomization DamageMeterUI = new();
}