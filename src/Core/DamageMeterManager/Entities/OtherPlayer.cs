namespace YURI_Overlay;

internal class OtherPlayer : DamageMeterEntity
{
	public OtherPlayer()
	{
		this.Name = "Other Player";

		this.StaticUi = new DamageMeterStaticUi(this);

		this.Type = DamageMeterEntityTypeEnum.OtherPlayer;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}