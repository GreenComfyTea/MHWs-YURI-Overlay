using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class PriorityUtils
{
	public static int ConvertPriorityToValue(Priority priority)
	{
		switch(priority)
		{
			case Priority.Higher3:
				return 3;
			case Priority.Higher2:
				return 2;
			case Priority.Higher1:
				return 1;
			case Priority.Lower1:
				return -1;
			case Priority.Lower2:
				return -2;
			case Priority.Lower3:
				return -3;
			case Priority.Normal:
			default:
				return 0;
		}
	}
}