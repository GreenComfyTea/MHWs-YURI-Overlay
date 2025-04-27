namespace YURI_Overlay;

internal enum FillDirection
{
	LeftToRight,
	RightToLeft,
	TopToBottom,
	BottomToTop,
}

internal enum OutlineStyle
{
	Inside,
	Center,
	Outside,
}

internal enum Priority
{
	Lower3 = -3,
	Lower2 = -2,
	Lower1 = -1,
	Normal = 0,
	Higher1 = 1,
	Higher2 = 2,
	Higher3 = 3,
}

internal enum Sorting
{
	Name,
	Id,
	Health,
	MaxHealth,
	HealthPercentage,
	Distance,
}

internal enum Anchor
{
	TopLeft,
	TopCenter,
	TopRight,
	CenterLeft,
	Center,
	CenterRight,
	BottomLeft,
	BottomCenter,
	BottomRight,
}