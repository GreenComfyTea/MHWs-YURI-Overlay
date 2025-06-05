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
	Higher3,
	Higher2,
	Higher1,
	Normal,
	Lower1,
	Lower2,
	Lower3,
}

internal enum Sorting
{
	Id,
	Name,
	Health,
	MaxHealth,
	HealthPercentage,
	Distance,
}

internal enum DamageMeterSorting
{
	HunterRank,
	MasterRank,
	Name,
	Dps,
	Damage,
	DamagePercentage
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