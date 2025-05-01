namespace YURI_Overlay;

internal sealed class LocalizationHelper
{
	private static readonly Lazy<LocalizationHelper> _lazy = new(() => new LocalizationHelper());

	public static LocalizationHelper Instance => _lazy.Value;

	public string[] DefaultFillDirections = new string[4];
	public string[] FillDirections = new string[4];

	public string[] DefaultOutlineStyles = new string[3];
	public string[] OutlineStyles = new string[3];

	public string[] DefaultSortings = new string[6];
	public string[] Sortings = new string[6];

	public string[] DefaultPriorities = new string[6];
	public string[] Priorities = new string[6];

	public string[] DefaultAnchors = new string[9];
	public string[] Anchors = new string[9];

	public string DefaultDefinedByLocalization = "";
	public string DefinedByLocalization = "";

	public void Initialize()
	{
		var localizationManager = LocalizationManager.Instance;

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;

		var defaultLocalization = localizationManager.DefaultLocalization.Data.ImGui;

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
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

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

		Sortings =
		[
			localization.Id,
			localization.Name,
			localization.Health,
			localization.MaxHealth,
			localization.HealthPercentage,
			localization.Distance,
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

	private void OnActiveLocalizationChanged(object sender, EventArgs e)
	{
		Update();
	}
}