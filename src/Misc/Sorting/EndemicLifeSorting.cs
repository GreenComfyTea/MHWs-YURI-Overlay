namespace YURI_Overlay;

internal static class EndemicLifeSorting
{
	public static int CompareById(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var distanceDifference = a.distance - b.distance;

		return !Utils.IsApproximatelyEqual(distanceDifference, 0f)
			? distanceDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByDistance(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByName(EndemicLifeEntity a, EndemicLifeEntity b)
	{
		var nameComparison = string.CompareOrdinal(a.name, b.name);

		if(nameComparison != 0)
		{
			return nameComparison;
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

	public static int CompareByIdReversed(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var distanceDifference = a.distance - b.distance;

		return !Utils.IsApproximatelyEqual(distanceDifference, 0f)
			? distanceDifference < 0f
				? -1
				: 1
			: 0;
	}

	public static int CompareByDistanceReversed(EndemicLifeEntity a, EndemicLifeEntity b)
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

		var idComparison = a.id.CompareTo(b.id);

		if(idComparison != 0)
		{
			return idComparison;
		}

		var roleIdComparison = a.roleId.CompareTo(b.roleId);

		return roleIdComparison != 0 ? roleIdComparison : a.legendaryId.CompareTo(b.legendaryId);
	}

	public static int CompareByNameReversed(EndemicLifeEntity a, EndemicLifeEntity b)
	{
		var nameComparison = string.CompareOrdinal(b.name, a.name);

		if(nameComparison != 0)
		{
			return nameComparison;
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