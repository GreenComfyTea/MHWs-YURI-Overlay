using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal abstract class DamageMeterEntity : IDisposable
{
	public DamageMeterStaticUi StaticUi;

	public DamageMeterEntityType Type;

	public string Name = "Hatsune Miku";

	public int Id = -1;

	public int HunterRank = -2;
	public int MasterRank = 0;

	public float DisplayedDamage = 0f;
	public float DisplayedDamagePercentage = 0f;

	public float DisplayedDps = 0f;
	public float DisplayedDpsPercentage = 0f;

	public int StaticSortingPriority = 0;

	public abstract void Dispose();
}