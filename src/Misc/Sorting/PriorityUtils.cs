using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class PriorityUtils
{
	public static int ConvertPriorityToValue(PriorityEnum? priority)
	{
		if(priority is null) return 0;

		switch(priority)
		{
			case PriorityEnum.Higher3:
				return 3;
			case PriorityEnum.Higher2:
				return 2;
			case PriorityEnum.Higher1:
				return 1;
			case PriorityEnum.Lower1:
				return -1;
			case PriorityEnum.Lower2:
				return -2;
			case PriorityEnum.Lower3:
				return -3;
			case PriorityEnum.Normal:
			default:
				return 0;
		}
	}
}