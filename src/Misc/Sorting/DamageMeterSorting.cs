namespace YURI_Overlay;

internal static class DamageMeterSorting
{
	public static int CompareById(DamageMeterEntity a, DamageMeterEntity b)
	{
		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		return !Utils.IsApproximatelyEqual(hunterRankDifference, 0f)
			? hunterRankDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByName(DamageMeterEntity a, DamageMeterEntity b)
	{
		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		return !Utils.IsApproximatelyEqual(hunterRankDifference, 0f)
			? hunterRankDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByHunterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByMasterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDamage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDamagePercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDps(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDpsPercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsPercentageDifference = a.displayedDpsPercentage - b.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = a.displayedDps - b.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var damageDifference = a.displayedDamage - b.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = a.displayedDamagePercentage - b.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = a.masterRank - b.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = a.hunterRank - b.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByIdReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var idComparison = b.id.CompareTo(a.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		return !Utils.IsApproximatelyEqual(hunterRankDifference, 0f)
			? hunterRankDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByNameReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var idComparison = b.id.CompareTo(a.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		return !Utils.IsApproximatelyEqual(hunterRankDifference, 0f)
			? hunterRankDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByHunterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByMasterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDamageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDamagePercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDpsReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}

	public static int CompareByDpsPercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var dpsPercentageDifference = b.displayedDpsPercentage - a.displayedDpsPercentage;

		if(!Utils.IsApproximatelyEqual(dpsPercentageDifference, 0f))
		{
			return dpsPercentageDifference < 0f ? -1 : 1;
		}

		var dpsDifference = b.displayedDps - a.displayedDps;

		if(!Utils.IsApproximatelyEqual(dpsDifference, 0f))
		{
			return dpsDifference < 0f ? -1 : 1;
		}

		var damageDifference = b.displayedDamage - a.displayedDamage;

		if(!Utils.IsApproximatelyEqual(damageDifference, 0f))
		{
			return damageDifference < 0f ? -1 : 1;
		}

		var damagePercentageDifference = b.displayedDamagePercentage - a.displayedDamagePercentage;

		if(!Utils.IsApproximatelyEqual(damagePercentageDifference, 0f))
		{
			return damagePercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var masterRankDifference = b.masterRank - a.masterRank;

		if(!Utils.IsApproximatelyEqual(masterRankDifference, 0f))
		{
			return masterRankDifference < 0f ? -1 : 1;
		}

		var hunterRankDifference = b.hunterRank - a.hunterRank;

		if(!Utils.IsApproximatelyEqual(hunterRankDifference, 0f))
		{
			return hunterRankDifference < 0f ? -1 : 1;
		}

		var idComparison = b.id.CompareTo(a.id);

		return idComparison != 0 ? idComparison : 0;
	}
}