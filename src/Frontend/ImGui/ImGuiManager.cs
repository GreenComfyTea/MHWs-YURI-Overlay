using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ImGuiManager
{
	private static readonly Lazy<ImGuiManager> Lazy = new(() => new ImGuiManager());

	public static ImGuiManager Instance => Lazy.Value;

	public float ComboBoxWidth = 100f;

	private bool _isOpened = false;
	private bool _isForceModInfoOpen = true;
	private string _modTitle = string.Empty;

	private Debouncer _onConfigChangedEmitDebouncer;
	private Debouncer _onConfigChangedSaveDebouncer;

	private ImGuiManager() { }

	public void Initialize()
	{
		LogManager.Info("[ImGuiManager] Initializing...");

		_modTitle = $"{Constants.ModName} v{Constants.Version}";

		_onConfigChangedEmitDebouncer = new Debouncer();
		_onConfigChangedSaveDebouncer = new Debouncer();

		LogManager.Info("[ImGuiManager] Initialized!");

		return;
	}

	public void Draw()
	{
		try
		{
			var localizationManager = LocalizationManager.Instance;

			var isClicked = ImGui.Button($"{_modTitle}##button");
			if(isClicked)
			{
				_isOpened = !_isOpened;
			}

			if(!_isOpened)
			{
				return;
			}

			var configManager = ConfigManager.Instance;

			var activeLocalization = localizationManager.ActiveLocalization.Data;
			var changed = false;

			ImGui.SetNextWindowPos(Constants.DefaultWindowPosition, ImGuiCond.FirstUseEver);
			ImGui.SetNextWindowSize(Constants.DefaultWindowSize, ImGuiCond.FirstUseEver);

			var menuFont = LuaFontManager.Instance.ActiveMenuFont;

			if(menuFont != null)
			{
				//LogManager.Debug($"Will push: {menuFont}");
				//ImGui.PushFont(menuFont.Value.Item2);
			}

			ImGui.Begin($"{_modTitle}##window", ref _isOpened);

			ComboBoxWidth = Constants.ComboboxWidthMultiplier * ImGui.GetWindowSize().X;

			if(_isForceModInfoOpen)
			{
				ImGui.SetNextItemOpen(true);
			}

			if(ImGui.TreeNode(activeLocalization.ImGui.ModInfo))
			{
				ImGui.Text(activeLocalization.ImGui.MadeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.ModAuthorColor, Constants.ModAuthor);

				if(ImGui.Button(activeLocalization.ImGui.NexusMods))
				{
					Utils.OpenLink(Constants.NexusModsLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.ImGui.GitHubRepo))
				{
					Utils.OpenLink(Constants.GithubRepoLink);
				}

				if(ImGui.Button(activeLocalization.ImGui.Twitch))
				{
					Utils.OpenLink(Constants.TwitchLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.ImGui.Twitter))
				{
					Utils.OpenLink(Constants.TwitterLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.ImGui.ArtStation))
				{
					Utils.OpenLink(Constants.ArtStationLink);
				}

				ImGui.Text(activeLocalization.ImGui.DonationMessage1);
				ImGui.Text(activeLocalization.ImGui.DonationMessage2);

				if(ImGui.Button(activeLocalization.ImGui.Donate))
				{
					Utils.OpenLink(Constants.StreamElementsTipLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.ImGui.PayPal))
				{
					Utils.OpenLink(Constants.PaypalLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.ImGui.BuyMeATea))
				{
					Utils.OpenLink(Constants.KofiLink);
				}

				ImGui.TreePop();
			}
			else
			{
				_isForceModInfoOpen = false;
			}

			ImGui.Separator();
			ImGui.NewLine();
			ImGui.Separator();

			var defaultConfig = ConfigManager.Instance.DefaultConfig;

			changed |= configManager.Customization.RenderImGui("config-settings");
			changed |= configManager.ActiveConfig.Data.GlobalSettings.RenderImGui("global-settings", defaultConfig.GlobalSettings);
			changed |= configManager.ActiveConfig.Data.LargeMonsterUI.RenderImGui("large-monster-ui", defaultConfig.LargeMonsterUI);

			//ImGui.ShowDemoWindow();
			//ImGui.ShowAboutWindow();
			//ImGui.ShowDebugLogWindow();
			//ImGui.ShowFontSelector("font-selector");
			//ImGui.ShowMetricsWindow();
			//ImGui.ShowStyleEditor();
			//ImGui.ShowUserGuide();

			if(menuFont != null)
			{
				//ImGui.PopFont();
			}

			ImGui.End();

			if(changed)
			{
				_onConfigChangedEmitDebouncer.Debounce(OnConfigChangedEmit, 25);
				_onConfigChangedSaveDebouncer.Debounce(OnConfigChangedSave, 100);
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
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