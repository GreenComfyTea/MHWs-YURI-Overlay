using via.network.session;

namespace YURI_Overlay;

internal sealed class LocalizationCustomization : Customization
{
	private int _activeLocalizationIndex = 0;

	private string[] _localizationNames = [];
	private string[] _localizationIsoCodes = [];

	public LocalizationCustomization()
	{
		var localizationManager = LocalizationManager.Instance;

		_localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.LocalizationInfo.Name).ToArray();
		_localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();

		_activeLocalizationIndex = Array.IndexOf(_localizationIsoCodes, localizationManager.ActiveLocalization.Name);

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;
		localizationManager.AnyLocalizationChanged += OnAnyLocalizationChanged;
	}

	public bool RenderImGui(string parentName = "")
	{
		var localizationManager = LocalizationManager.Instance;
		var configManager = ConfigManager.Instance;
		var localization = localizationManager.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		var englishLocalizationIndex = Array.IndexOf(_localizationIsoCodes, Constants.DefaultLocalization);

		var isActiveConfigChanged = ImGuiHelper.ResettableCombo(localization.Language, ref _activeLocalizationIndex, _localizationNames, englishLocalizationIndex);
		if(isActiveConfigChanged)
		{
			isChanged |= isActiveConfigChanged;

			configManager.ActiveConfig.Data.GlobalSettings.Localization = _localizationIsoCodes[_activeLocalizationIndex];
			localizationManager.ActivateLocalization(_localizationIsoCodes[_activeLocalizationIndex]);
		}

		return isChanged;
	}

	public void Reset(int defaultLocalizationIndex = -1)
	{
		if (defaultLocalizationIndex == -1) return;

		_activeLocalizationIndex = defaultLocalizationIndex;
	}

	private void OnAnyLocalizationChanged(object sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		_localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.LocalizationInfo.Name).ToArray();
		_localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();
	}

	private void OnActiveLocalizationChanged(object sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		_activeLocalizationIndex = Array.IndexOf(_localizationIsoCodes, localizationManager.ActiveLocalization.Name);
	}
}
