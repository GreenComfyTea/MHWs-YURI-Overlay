using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class OtherPlayer : DamageMeterEntity
{
	public OtherPlayer()
	{
		Name = "Other Player";

		StaticUi = new DamageMeterStaticUi(this);

		Type = DamageMeterEntityTypeEnum.OtherPlayer;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}