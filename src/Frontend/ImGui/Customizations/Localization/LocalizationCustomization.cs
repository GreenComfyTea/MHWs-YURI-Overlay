using System.Numerics;
using System.Text.Json.Serialization;
using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LocalizationCustomization : Customization
{
	private int _activeLocalizationIndex;

	private string[] _localizationNames = [];
	private string[] _localizationIsoCodes = [];

	[JsonIgnore] private Vector4 TranslatorColor { get; set; } = Constants.ModAuthorColor;

	public LocalizationCustomization()
	{
		var localizationManager = LocalizationManager.Instance;

		_localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.LocalizationInfo.Name).ToArray();
		_localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();

		_activeLocalizationIndex = Array.IndexOf(_localizationIsoCodes, localizationManager.ActiveLocalization.Name);

		UpdateTranslatorColor();

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;
		localizationManager.AnyLocalizationChanged += OnAnyLocalizationChanged;
	}

	public bool RenderImGui(string parentName = "")
	{
		var localizationManager = LocalizationManager.Instance;
		var configManager = ConfigManager.Instance;
		var localization = localizationManager.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-language";

		var englishLocalizationIndex = Array.IndexOf(_localizationIsoCodes, Constants.DefaultLocalization);

		if(ImGuiHelper.ResettableTreeNode(localization.Language, customizationName, ref isChanged, englishLocalizationIndex, Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Language}##{customizationName}", ref _activeLocalizationIndex, _localizationNames, englishLocalizationIndex);
			if(isChanged)
			{
				configManager.ActiveConfig.Data.GlobalSettings.Localization = _localizationIsoCodes[_activeLocalizationIndex];
				localizationManager.ActivateLocalization(_localizationIsoCodes[_activeLocalizationIndex]);
			}

			ImGui.Text($"{LocalizationManager.Instance.ActiveLocalization.Data.ImGui.Translators}:");
			ImGui.SameLine();
			ImGui.TextColored(TranslatorColor, localizationManager.ActiveLocalization.Data.LocalizationInfo.Translators);

			ImGui.TreePop();
		}


		return isChanged;
	}

	public void Reset(int defaultLocalizationIndex = -1)
	{
		if(defaultLocalizationIndex == -1) return;

		_activeLocalizationIndex = defaultLocalizationIndex;
	}

	private void UpdateTranslatorColor()
	{
		TranslatorColor =
			LocalizationManager.Instance.ActiveLocalization.Data.LocalizationInfo.Translators.Equals(Constants.ModAuthor)
				? Constants.ModAuthorColor
				: Constants.ImGuiUserNameColor;
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
		UpdateTranslatorColor();
	}
}