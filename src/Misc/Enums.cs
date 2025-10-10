namespace YURI_Overlay;

internal enum FillDirectionEnum
{
	LeftToRight,
	RightToLeft,
	TopToBottom,
	BottomToTop,
}

internal enum OutlineStyleEnum
{
	Inside,
	Center,
	Outside,
}

internal enum PriorityEnum
{
	Higher3,
	Higher2,
	Higher1,
	Normal,
	Lower1,
	Lower2,
	Lower3,
}

internal enum SortingEnum
{
	Id,
	Name,
	Health,
	MaxHealth,
	HealthPercentage,
	Distance,
}

internal enum DamageMeterSortingEnum
{
	Id,
	Name,
	HunterRank,
	MasterRank,

	Damage,
	DamagePercentage,
	Dps,
	DpsPercentage,
}

internal enum AnchorEnum
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

internal enum DamageMeterEntityTypeEnum
{
	LocalPlayer,
	OtherPlayer,
	SupportHunter,
	TotalDamage,
}