namespace YURI_Overlay;

internal class TotalDamageEntity : DamageMeterEntity
{
	public TotalDamageEntity()
	{
		this.Name = "Total Damage";

		this.StaticUi = new DamageMeterStaticUi(this);

		this.Type = DamageMeterEntityTypeEnum.Total;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}