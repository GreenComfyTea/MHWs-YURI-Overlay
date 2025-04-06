namespace YURI_Overlay;

internal static class SmallMonsterSorting
{
	public static int CompareById(SmallMonster a, SmallMonster b)
	{
		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		var legendaryIdComparison = a.LegendaryId.CompareTo(b.LegendaryId);
		if(legendaryIdComparison != 0)
		{
			return legendaryIdComparison;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return 0;
		}

		return distanceDifference < 0f ? -1 : 1;
	}

	public static int CompareByHealth(SmallMonster a, SmallMonster b)
	{
		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByMaxHealth(SmallMonster a, SmallMonster b)
	{
		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByHealthPercentage(SmallMonster a, SmallMonster b)
	{
		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByDistance(SmallMonster a, SmallMonster b)
	{
		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByName(SmallMonster a, SmallMonster b)
	{
		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByIdReversed(SmallMonster a, SmallMonster b)
	{
		var idComparison = b.Id.CompareTo(a.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = b.RoleId.CompareTo(a.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		var legendaryIdComparison = b.LegendaryId.CompareTo(a.LegendaryId);
		if(legendaryIdComparison != 0)
		{
			return legendaryIdComparison;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return 0;
		}

		return distanceDifference < 0f ? -1 : 1;
	}

	public static int CompareByHealthReversed(SmallMonster a, SmallMonster b)
	{
		var healthDifference = b.Health - a.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByMaxHealthReversed(SmallMonster a, SmallMonster b)
	{
		var maxHealthDifference = b.MaxHealth - a.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByHealthPercentageReversed(SmallMonster a, SmallMonster b)
	{
		var healthPercentageDifference = b.HealthPercentage - a.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByDistanceReversed(SmallMonster a, SmallMonster b)
	{
		var distanceDifference = b.Distance - a.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}

	public static int CompareByNameReversed(SmallMonster a, SmallMonster b)
	{
		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
		}

		var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
		if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
		{
			return healthPercentageDifference < 0f ? -1 : 1;
		}

		var healthDifference = a.Health - b.Health;
		if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
		{
			return healthDifference < 0f ? -1 : 1;
		}

		var maxHealthDifference = a.MaxHealth - b.MaxHealth;
		if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
		{
			return maxHealthDifference < 0f ? -1 : 1;
		}

		var distanceDifference = a.Distance - b.Distance;
		if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return distanceDifference < 0f ? -1 : 1;
		}

		var idComparison = a.Id.CompareTo(b.Id);
		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
		if(roleIdComparison != 0)
		{
			return roleIdComparison;
		}

		return a.LegendaryId.CompareTo(b.LegendaryId);
	}
}
