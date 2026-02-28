using Hexa.NET.ImGui;
using System.Diagnostics;
using System.Text.Json;

namespace YURI_Overlay;

internal sealed class ImGuiManager
{
	private static readonly Lazy<ImGuiManager> Lazy = new(() => new ImGuiManager());

	public static ImGuiManager Instance => Lazy.Value;

	public float ComboBoxWidth = 100f;
	public float ColorPickerWidth = 100f;

	public float ReframeworkFontSize = Constants.DefaultReframeworkFontSize;

	private bool _isOpened;
	private bool _isForceModInfoOpen = true;
	private string _modTitle = string.Empty;

	private Debouncer? _onConfigChangedEmitDebouncer;
	private Debouncer? _onConfigChangedSaveDebouncer;

	private ImGuiManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[ImGuiManager] Initializing...");

		_modTitle = $"{Constants.ModName} v{Constants.Version}";

		_onConfigChangedEmitDebouncer = new Debouncer();
		_onConfigChangedSaveDebouncer = new Debouncer();

		LogManager.Info("[ImGuiManager] Initialized!");
	}

	public void Draw()
	{
		try
		{
			var localizationManager = LocalizationManager.Instance;

			var isClicked = ImGui.Button($"{_modTitle}##button");
			if (isClicked) _isOpened = !_isOpened;

			ReframeworkFontSize = ImGui.GetFontSize();

			if (!_isOpened) return;

			var configManager = ConfigManager.Instance;

			var activeLocalization = localizationManager.ActiveLocalization.Data;
			var changed = false;

			ImGui.SetNextWindowPos(Constants.DefaultWindowPosition, ImGuiCond.FirstUseEver);
			ImGui.SetNextWindowSize(Constants.DefaultWindowSize, ImGuiCond.FirstUseEver);

			ImGui.Begin($"{_modTitle}##window", ref _isOpened);

			CalculateWidths();

			if (_isForceModInfoOpen) ImGui.SetNextItemOpen(true);

			if (ImGui.TreeNode(activeLocalization.ImGui.ModInfo))
			{
				ImGui.Text(activeLocalization.ImGui.MadeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.ModAuthorColor, Constants.ModAuthor);

				if (ImGui.Button(activeLocalization.ImGui.NexusMods)) Utils.OpenLink(Constants.NexusModsLink);

				ImGui.SameLine();
				if (ImGui.Button(activeLocalization.ImGui.GitHubRepo)) Utils.OpenLink(Constants.GithubRepoLink);

				if (ImGui.Button(activeLocalization.ImGui.Twitch)) Utils.OpenLink(Constants.TwitchLink);

				ImGui.SameLine();
				if (ImGui.Button(activeLocalization.ImGui.Twitter)) Utils.OpenLink(Constants.TwitterLink);

				ImGui.SameLine();
				if (ImGui.Button(activeLocalization.ImGui.ArtStation)) Utils.OpenLink(Constants.ArtStationLink);

				ImGui.Text(activeLocalization.ImGui.DonationMessage1);
				ImGui.Text(activeLocalization.ImGui.DonationMessage2);

				if (ImGui.Button(activeLocalization.ImGui.Donate)) Utils.OpenLink(Constants.StreamElementsTipLink);

				ImGui.SameLine();
				if (ImGui.Button(activeLocalization.ImGui.PayPal)) Utils.OpenLink(Constants.PaypalLink);

				ImGui.SameLine();
				if (ImGui.Button(activeLocalization.ImGui.BuyMeATea)) Utils.OpenLink(Constants.KofiLink);

				ImGui.TreePop();
			}
			else
				_isForceModInfoOpen = false;

			ImGui.Separator();
			ImGui.NewLine();
			ImGui.Separator();

			var defaultConfig = ConfigManager.Instance.DefaultConfig;

			changed |= configManager.Customization.RenderImGui("config-settings");
			changed |= configManager.ActiveConfig.Data.GlobalSettings.RenderImGui("global-settings", defaultConfig?.GlobalSettings);
			changed |= configManager.ActiveConfig.Data.LargeMonsterUI.RenderImGui("large-monster-ui", defaultConfig?.LargeMonsterUI);
			changed |= configManager.ActiveConfig.Data.SmallMonsterUI.RenderImGui("small-monster-ui", defaultConfig?.SmallMonsterUI);
			changed |= configManager.ActiveConfig.Data.EndemicLifeUI.RenderImGui("endemic-life-ui", defaultConfig?.EndemicLifeUI);
			//changed |= configManager.ActiveConfig.Data..DamageMeterUI.RenderImGui("damage-meter-ui", defaultConfig.DamageMeterUI);

			Debug();

			//if(menuFont is not null)
			//{
			//	//ImGui.PopFont();
			//}

			//io.FontGlobalScale = oldFontGlobalScale;

			if (changed)
			{
				_onConfigChangedEmitDebouncer?.Debounce(OnConfigChangedEmit, 25);
				_onConfigChangedSaveDebouncer?.Debounce(OnConfigChangedSave, 100);
			}
		}
		catch (Exception exception)
		{
			LogManager.Error(exception);
		}
		finally
		{
            ImGui.End();
        }
	}

	private void CalculateWidths()
	{
		var windowSize = ImGui.GetWindowSize();

		ComboBoxWidth = Constants.ComboboxWidthMultiplier * windowSize.X;

		var maxColorPickerWidthByWindowWidth = Constants.ColorPickerWidthMultiplier * windowSize.X;
		var maxColorPickerWidthByWindowHeight = Constants.ColorPickerWidthToHeightRatio * windowSize.Y;

		ColorPickerWidth = Math.Min(maxColorPickerWidthByWindowWidth, maxColorPickerWidthByWindowHeight);
	}

	private void Debug()
	{
#if DEBUG
		ImGui.ShowDemoWindow();
		ImGui.ShowAboutWindow();
		ImGui.ShowDebugLogWindow();
		ImGui.ShowMetricsWindow();

		// No separate windows
		//ImGui.ShowFontSelector("font-selector");
		// ImGui.ShowStyleEditor();
		// ImGui.ShowUserGuide();
#endif
	}

	private void OnConfigChangedEmit()
	{
		ConfigManager.Instance.EmitAnyConfigChanged();
	}

	private void OnConfigChangedSave()
	{
		LogManager.Info("[ImGuiManager] Config changed.");

		ConfigManager.Instance.ActiveConfig.Save();
	}
}