namespace YURI_Overlay;

internal sealed class LocalizationHelper
{
	private static readonly Lazy<LocalizationHelper> Lazy = new(() => new LocalizationHelper());

	public static LocalizationHelper Instance => Lazy.Value;

	public string[] DefaultFillDirections = [];
	public string[] FillDirections = [];

	public string[] DefaultOutlineStyles = [];
	public string[] OutlineStyles = [];

	public string[] DefaultSortings = [];
	public string[] Sortings = [];

	public string[] DefaultDamageMeterSortings = [];
	public string[] DamageMeterSortings = [];

	public string[] DefaultPriorities = [];
	public string[] Priorities = [];

	public string[] DefaultAnchors = [];
	public string[] Anchors = [];

	public string DefaultDefinedByLocalization = "";
	public string DefinedByLocalization = "";

	public void Initialize()
	{
		var localizationManager = LocalizationManager.Instance;

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;

		var defaultLocalization = localizationManager.DefaultLocalization?.Data?.ImGui;

		if(defaultLocalization is null)
		{
			LogManager.Warn("LocalizationHelper Initialization Failed! No Default Localization!");
			return;
		}

		DefaultFillDirections =
		[
			defaultLocalization.LeftToRight,
			defaultLocalization.RightToLeft,
			defaultLocalization.TopToBottom,
			defaultLocalization.BottomToTop,
		];

		DefaultOutlineStyles =
		[
			defaultLocalization.Inside,
			defaultLocalization.Center,
			defaultLocalization.Outside,
		];

		DefaultSortings =
		[
			defaultLocalization.Id,
			defaultLocalization.Name,
			defaultLocalization.Health,
			defaultLocalization.MaxHealth,
			defaultLocalization.HealthPercentage,
			defaultLocalization.Distance,
		];

		DefaultSortings =
		[
			defaultLocalization.Id,
			defaultLocalization.Name,
			defaultLocalization.HunterRank,
			defaultLocalization.MasterRank,
			defaultLocalization.Damage,
			defaultLocalization.DamagePercentage,
			defaultLocalization.DPS,
			defaultLocalization.DPSPercentage,
		];

		DefaultPriorities =
		[
			defaultLocalization.Higher3,
			defaultLocalization.Higher2,
			defaultLocalization.Higher1,
			defaultLocalization.Normal,
			defaultLocalization.Lower1,
			defaultLocalization.Lower2,
			defaultLocalization.Lower3,
		];

		DefaultAnchors =
		[
			defaultLocalization.TopLeft,
			defaultLocalization.TopCenter,
			defaultLocalization.TopRight,
			defaultLocalization.CenterLeft,
			defaultLocalization.Center,
			defaultLocalization.CenterRight,
			defaultLocalization.BottomLeft,
			defaultLocalization.BottomCenter,
			defaultLocalization.BottomRight,
		];

		DefinedByLocalization = defaultLocalization.DefinedByLocalization.Replace(" ", "");

		Update();
	}

	public void Update()
	{
		var localization = LocalizationManager.Instance.ActiveLocalization?.Data?.ImGui;

		if(localization is null)
		{
			LogManager.Warn("LocalizationHelper Update Failed! No Localization!");
			return;
		}

		FillDirections =
		[
			localization.LeftToRight,
			localization.RightToLeft,
			localization.TopToBottom,
			localization.BottomToTop,
		];

		OutlineStyles =
		[
			localization.Inside,
			localization.Center,
			localization.Outside,
		];

		Sortings =
		[
			localization.Id,
			localization.Name,
			localization.Health,
			localization.MaxHealth,
			localization.HealthPercentage,
			localization.Distance,
		];

		DamageMeterSortings =
		[
			localization.Id,
			localization.Name,
			localization.HunterRank,
			localization.MasterRank,
			localization.Damage,
			localization.DamagePercentage,
			localization.DPS,
			localization.DPSPercentage,
		];

		Priorities =
		[
			localization.Higher3,
			localization.Higher2,
			localization.Higher1,
			localization.Normal,
			localization.Lower1,
			localization.Lower2,
			localization.Lower3,
		];

		Anchors =
		[
			localization.TopLeft,
			localization.TopCenter,
			localization.TopRight,
			localization.CenterLeft,
			localization.Center,
			localization.CenterRight,
			localization.BottomLeft,
			localization.BottomCenter,
			localization.BottomRight,
		];

		DefinedByLocalization = localization.DefinedByLocalization;
	}

	private void OnActiveLocalizationChanged(object? sender, EventArgs e)
	{
		Update();
	}
}