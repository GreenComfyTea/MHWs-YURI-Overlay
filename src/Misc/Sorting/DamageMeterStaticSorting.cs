using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class DamageMeterStaticSorting
{
	public static int CompareById(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareById(a, b);
	}

	public static int CompareByName(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByName(a, b);
	}

	public static int CompareByHunterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByHunterRank(a, b);
	}

	public static int CompareByMasterRank(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByMasterRank(a, b);
	}

	public static int CompareByDamage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDamage(a, b);
	}

	public static int CompareByDamagePercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDamagePercentage(a, b);
	}

	public static int CompareByDps(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDps(a, b);
	}

	public static int CompareByDpsPercentage(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = b.StaticSortingPriority.CompareTo(a.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDpsPercentage(a, b);
	}

	public static int CompareByIdReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByIdReversed(a, b);
	}

	public static int CompareByNameReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByNameReversed(a, b);
	}

	public static int CompareByHunterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByHunterRankReversed(a, b);
	}

	public static int CompareByMasterRankReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByMasterRankReversed(a, b);
	}

	public static int CompareByDamageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDamageReversed(a, b);
	}

	public static int CompareByDamagePercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDamagePercentageReversed(a, b);
	}

	public static int CompareByDpsReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDpsReversed(a, b);
	}

	public static int CompareByDpsPercentageReversed(DamageMeterEntity a, DamageMeterEntity b)
	{
		var priorityComparison = a.StaticSortingPriority.CompareTo(b.StaticSortingPriority);
		if(priorityComparison != 0) return priorityComparison;

		return DamageMeterSorting.CompareByDpsPercentageReversed(a, b);
	}
}