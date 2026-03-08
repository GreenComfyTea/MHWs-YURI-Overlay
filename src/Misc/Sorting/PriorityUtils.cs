namespace YURI_Overlay;

internal sealed class PriorityUtils
{
	public static int ConvertPriorityToValue(PriorityEnum? priority)
	{
		return priority is null
			? 0
			: priority switch
			{
				PriorityEnum.Higher3 => 3,
				PriorityEnum.Higher2 => 2,
				PriorityEnum.Higher1 => 1,
				PriorityEnum.Lower1 => -1,
				PriorityEnum.Lower2 => -2,
				PriorityEnum.Lower3 => -3,
				var _ => 0,
			};
	}
}