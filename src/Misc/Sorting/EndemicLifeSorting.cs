namespace YURI_Overlay;

internal static class EndemicLifeSorting
{
	public static int CompareById(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var distanceDifference = a.Distance - b.Distance;
		if(Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return 0;
		}

		return distanceDifference < 0f ? -1 : 1;
	}

	public static int CompareByDistance(EndemicLifeEntity a, EndemicLifeEntity b)
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

	public static int CompareByName(EndemicLifeEntity a, EndemicLifeEntity b)
	{
		var nameComparison = string.CompareOrdinal(a.Name, b.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
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

	public static int CompareByIdReversed(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var distanceDifference = a.Distance - b.Distance;
		if(Utils.IsApproximatelyEqual(distanceDifference, 0f))
		{
			return 0;
		}

		return distanceDifference < 0f ? -1 : 1;
	}

	public static int CompareByDistanceReversed(EndemicLifeEntity a, EndemicLifeEntity b)
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

	public static int CompareByNameReversed(EndemicLifeEntity a, EndemicLifeEntity b)
	{
		var nameComparison = string.CompareOrdinal(b.Name, a.Name);
		if(nameComparison != 0)
		{
			return nameComparison;
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
