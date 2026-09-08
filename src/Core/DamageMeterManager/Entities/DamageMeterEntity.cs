namespace YURI_Overlay;

internal abstract class DamageMeterEntity : IDisposable
{
	public float DisplayedDamage = 0f;
	public float DisplayedDamagePercentage = 0f;

	public float DisplayedDps = 0f;
	public float DisplayedDpsPercentage = 0f;

	public int HunterRank = -2;

	public int Id = -1;
	public int MasterRank = 0;

	public string Name = "Hatsune Miku";

	public int StaticSortingPriority = 0;
	public DamageMeterStaticUi? StaticUi;

	public DamageMeterEntityTypeEnum Type;

	public abstract void Dispose();
}