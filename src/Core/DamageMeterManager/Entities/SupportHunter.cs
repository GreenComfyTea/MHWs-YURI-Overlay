using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class SupportHunter : DamageMeterEntity
{
	public SupportHunter()
	{
		Name = "Support Hunter";

		StaticUi = new DamageMeterStaticUi(this);

		Type = DamageMeterEntityTypeEnum.SupportHunter;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}