using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class DamageMeterSorting
{
	public static int CompareById(DamageMeterEntity a, DamageMeterEntity b)
	{
		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		return 0;
	}

	public static int CompareByName(DamageMeterEntity a, DamageMeterEntity b)
	{
		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		return 0;
	}

	public static int CompareByHunterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByMasterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDamage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDamagePercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDps(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDpsPercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsPercentageDifference = a.DisplayedDpsPercentage - b.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var dpsDifference = a.DisplayedDps - b.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var damageDifference = a.DisplayedDamage - b.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = a.DisplayedDamagePercentage - b.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = a.MasterRank - b.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = a.HunterRank - b.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByIdReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		return 0;
	}

	public static int CompareByNameReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		return 0;
	}

	public static int CompareByHunterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByMasterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDamageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDamagePercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDpsReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}

	public static int CompareByDpsPercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsPercentageDifference = b.DisplayedDpsPercentage - a.DisplayedDpsPercentage;
		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f)) return dpsPercentageDifference < 0f ? -1 : 1;

		var dpsDifference = b.DisplayedDps - a.DisplayedDps;
		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f)) return dpsDifference < 0f ? -1 : 1;

		var damageDifference = b.DisplayedDamage - a.DisplayedDamage;
		if(!Utils.IsApproximatelyEqual(damageDifference, 0f)) return damageDifference < 0f ? -1 : 1;

		var damagePercentageDifference = b.DisplayedDamagePercentage - a.DisplayedDamagePercentage;
		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f)) return damagePercentageDifference < 0f ? -1 : 1;

		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0) return nameComparison;

		var masterRankDifference = b.MasterRank - a.MasterRank;
		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f)) return masterRankDifference < 0f ? -1 : 1;

		var hunterRankDifference = b.HunterRank - a.HunterRank;
		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f)) return hunterRankDifference < 0f ? -1 : 1;

		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0) return idComparison;

		return 0;
	}
}