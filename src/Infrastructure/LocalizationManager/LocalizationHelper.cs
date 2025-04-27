namespace YURI_Overlay;

internal sealed class LocalizationHelper
{
	private static readonly Lazy<LocalizationHelper> _lazy = new(() => new LocalizationHelper());

	public static LocalizationHelper Instance => _lazy.Value;

	public string[] DefaultFillDirections = new string[4];
	public string[] FillDirections = new string[4];

	public string[] DefaultOutlineStyles = new string[3];
	public string[] OutlineStyles = new string[3];

	public string[] DefaultSortingLocations = new string[3];
	public string[] SortingLocations = new string[3];

	public string[] DefaultSortings = new string[6];
	public string[] Sortings = new string[6];

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

		DefaultSortingLocations =
		[
			defaultLocalization.Normal,
			defaultLocalization.First,
			defaultLocalization.Last,
		];

		DefaultSortings =
		[
			defaultLocalization.Name,
			defaultLocalization.Id,
			defaultLocalization.Health,
			defaultLocalization.MaxHealth,
			defaultLocalization.HealthPercentage,
			defaultLocalization.Distance,
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

		SortingLocations =
		[
			localization.Normal,
			localization.First,
			localization.Last,
		];

		Sortings =
		[
			localization.Name,
			localization.Id,
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