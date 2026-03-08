namespace YURI_Overlay;

internal static class SmallMonsterSorting
{
	public static int CompareById(SmallMonster a, SmallMonster b)
	{
		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		var legendaryIdComparison = a.legendaryId.CompareTo(b.legendaryId);

		if(legendaryIdComparison != 0)
		{
			return legendaryIdComparison;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		return !Utils.IsApproximatelyEqual(distanceDifference, 0f)
			? distanceDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByHealth(SmallMonster a, SmallMonster b)
	{
		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByMaxHealth(SmallMonster a, SmallMonster b)
	{
		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByHealthPercentage(SmallMonster a, SmallMonster b)
	{
		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByDistance(SmallMonster a, SmallMonster b)
	{
		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByName(SmallMonster a, SmallMonster b)
	{
		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByIdReversed(SmallMonster a, SmallMonster b)
	{
		var idComparison = b.id.CompareTo(a.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = b.roleId.CompareTo(a.roleId);

		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		var legendaryIdComparison = b.legendaryId.CompareTo(a.legendaryId);

		if(legendaryIdComparison != 0)
		{
			return legendaryIdComparison;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		return Utils.IsApproximatelyEqual(distanceDifference, 0f)
			? distanceDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByHealthReversed(SmallMonster a, SmallMonster b)
	{
		var healthDifference = b.health - a.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByMaxHealthReversed(SmallMonster a, SmallMonster b)
	{
		var maxHealthDifference = b.maxHealth - a.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByHealthPercentageReversed(SmallMonster a, SmallMonster b)
	{
		var healthPercentageDifference = b.healthPercentage - a.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByDistanceReversed(SmallMonster a, SmallMonster b)
	{
		var distanceDifference = b.distance - a.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByNameReversed(SmallMonster a, SmallMonster b)
	{
		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.healthPercentage - b.healthPercentage;

		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.health - b.health;

		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.maxHealth - b.maxHealth;

		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.distance - b.distance;

		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}
}