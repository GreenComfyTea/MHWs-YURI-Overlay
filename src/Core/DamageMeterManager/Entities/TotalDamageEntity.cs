using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class TotalDamageEntity : DamageMeterEntity
{
	public TotalDamageEntity()
	{
		Name = "Total Damage";

		StaticUi = new DamageMeterStaticUi(this);

		Type = DamageMeterEntityType.TotalDamage;
	}

	public override void Dispose()
	{
		throw new NotImplementedException();
	}
}