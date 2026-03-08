using System.Numerics;
using System.Text.Json.Serialization;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class LocalizationCustomization : Customization
{
	private int _activeLocalizationIndex;

	private string[] _localizationNames;
	private string[] _localizationIsoCodes;

	[JsonIgnore]
	private Vector4 TranslatorColor { get; set; } = Constants.ModAuthorColor;

	public LocalizationCustomization(bool stub)
	{
		this._activeLocalizationIndex = 0;

		this._localizationNames = [];
		this._localizationIsoCodes = [];
	}

	public LocalizationCustomization()
	{
		var localizationManager = LocalizationManager.Instance;

		this._localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.LocalizationInfo.Name).ToArray();
		this._localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();

		this._activeLocalizationIndex = Array.IndexOf(this._localizationIsoCodes, localizationManager.ActiveLocalization.Name);

		this.UpdateTranslatorColor();

		localizationManager.ActiveLocalizationChanged += this.OnActiveLocalizationChanged;
		localizationManager.AnyLocalizationChanged += this.OnAnyLocalizationChanged;
	}

	public bool RenderImGui(string? parentName = "")
	{
		var localizationManager = LocalizationManager.Instance;
		var configManager = ConfigManager.Instance;
		var localization = localizationManager.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-language";

		var englishLocalizationIndex = Array.IndexOf(this._localizationIsoCodes, Constants.DefaultLocalization);

		if(ImGuiHelper.ResettableTreeNode(localization.Language, customizationName, ref isChanged, englishLocalizationIndex, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableCombo($"{localization.Language}##{customizationName}", ref this._activeLocalizationIndex, this._localizationNames, englishLocalizationIndex);

			if(isChanged)
			{
				configManager.ActiveConfig.Data.GlobalSettings.Localization = this._localizationIsoCodes[this._activeLocalizationIndex];
				localizationManager.ActivateLocalization(this._localizationIsoCodes[this._activeLocalizationIndex]);
			}

			ImGui.Text($"{LocalizationManager.Instance.ActiveLocalization.Data.ImGui.Translators}:");
			ImGui.SameLine();
			ImGui.TextColored(this.TranslatorColor, localizationManager.ActiveLocalization.Data.LocalizationInfo.Translators);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(int defaultLocalizationIndex = -1)
	{
		if(defaultLocalizationIndex == -1)
		{
			return;
		}

		this._activeLocalizationIndex = defaultLocalizationIndex;
	}

	private void UpdateTranslatorColor()
	{
		this.TranslatorColor = LocalizationManager.Instance.ActiveLocalization.Data.LocalizationInfo.Translators.Equals(Constants.ModAuthor)
			? Constants.ModAuthorColor
			: Constants.ImGuiUserNameColor;
	}

	private void OnAnyLocalizationChanged(object? sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		this._localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.LocalizationInfo.Name).ToArray();
		this._localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();
	}

	private void OnActiveLocalizationChanged(object? sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		this._activeLocalizationIndex = Array.IndexOf(this._localizationIsoCodes, localizationManager.ActiveLocalization.Name);
		this.UpdateTranslatorColor();
	}
}