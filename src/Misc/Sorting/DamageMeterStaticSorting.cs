namespace YURI_Overlay;

internal static class DamageMeterStaticSorting
{
	public static int CompareById(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareById(a, b);
	}

	public static int CompareByName(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByName(a, b);
	}

	public static int CompareByHunterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByHunterRank(a, b);
	}

	public static int CompareByMasterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByMasterRank(a, b);
	}

	public static int CompareByDamage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDamage(a, b);
	}

	public static int CompareByDamagePercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDamagePercentage(a, b);
	}

	public static int CompareByDps(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDps(a, b);
	}

	public static int CompareByDpsPercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.staticSortingPriority.CompareTo(a.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDpsPercentage(a, b);
	}

	public static int CompareByIdReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByIdReversed(a, b);
	}

	public static int CompareByNameReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByNameReversed(a, b);
	}

	public static int CompareByHunterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByHunterRankReversed(a, b);
	}

	public static int CompareByMasterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByMasterRankReversed(a, b);
	}

	public static int CompareByDamageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDamageReversed(a, b);
	}

	public static int CompareByDamagePercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDamagePercentageReversed(a, b);
	}

	public static int CompareByDpsReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDpsReversed(a, b);
	}

	public static int CompareByDpsPercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.staticSortingPriority.CompareTo(b.staticSortingPriority);

		return priorityComparison != 0 ? priorityComparison : DamageMeterSorting.CompareByDpsPercentageReversed(a, b);
	}
}