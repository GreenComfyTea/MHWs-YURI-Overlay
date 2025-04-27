namespace YURI_Overlay;

internal static class LargeMonsterDynamicSorting
{
	public static int CompareById(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareById(a, b);
	}

	public static int CompareByHealth(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByHealth(a, b);
	}

	public static int CompareByMaxHealth(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByMaxHealth(a, b);
	}

	public static int CompareByHealthPercentage(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByHealthPercentage(a, b);
	}

	public static int CompareByDistance(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByDistance(a, b);
	}

	public static int CompareByName(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByName(a, b);
	}

	public static int CompareByIdReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByIdReversed(a, b);
	}

	public static int CompareByHealthReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByHealthReversed(a, b);
	}

	public static int CompareByMaxHealthReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByMaxHealthReversed(a, b);
	}

	public static int CompareByHealthPercentageReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByHealthPercentageReversed(a, b);
	}

	public static int CompareByDistanceReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByDistanceReversed(a, b);
	}

	public static int CompareByNameReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.DynamicSortingPriority.CompareTo(b.DynamicSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return LargeMonsterSorting.CompareByNameReversed(a, b);
	}
}