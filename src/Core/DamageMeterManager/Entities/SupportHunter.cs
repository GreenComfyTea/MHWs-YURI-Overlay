namespace YURI_Overlay;

internal class SupportHunter : DamageMeterEntity
{
	public SupportHunter()
	{
		this.Name = "Support Hunter";

		this.StaticUi = new DamageMeterStaticUi(this);

		this.Type = DamageMeterEntityTypeEnum.SupportHunter;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}