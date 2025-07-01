namespace YURI_Overlay;

internal sealed class Config
{
	public GlobalSettingsCustomization GlobalSettings = new();
	public LargeMonsterUiCustomization LargeMonsterUI = new();
	public SmallMonsterDynamicUiCustomization SmallMonsterUI = new();

	public EndemicLifeDynamicUiCustomization EndemicLifeUI = new();
	//public DamageMeterStaticUiCustomization DamageMeterUI = new();
}