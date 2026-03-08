namespace YURI_Overlay;

internal static class LargeMonsterDynamicSorting
{
	public static int CompareById(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareById(a, b);
	}

	public static int CompareByName(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByName(a, b);
	}

	public static int CompareByHealth(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByHealth(a, b);
	}

	public static int CompareByMaxHealth(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByMaxHealth(a, b);
	}

	public static int CompareByHealthPercentage(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByHealthPercentage(a, b);
	}

	public static int CompareByDistance(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = b.dynamicSortingPriority.CompareTo(a.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByDistance(a, b);
	}

	public static int CompareByIdReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByIdReversed(a, b);
	}

	public static int CompareByNameReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByNameReversed(a, b);
	}

	public static int CompareByHealthReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByHealthReversed(a, b);
	}

	public static int CompareByMaxHealthReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByMaxHealthReversed(a, b);
	}

	public static int CompareByHealthPercentageReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByHealthPercentageReversed(a, b);
	}

	public static int CompareByDistanceReversed(LargeMonster a, LargeMonster b)
	{
		var priorityComparison = a.dynamicSortingPriority.CompareTo(b.dynamicSortingPriority);

		return priorityComparison != 0 ? priorityComparison : LargeMonsterSorting.CompareByDistanceReversed(a, b);
	}
}