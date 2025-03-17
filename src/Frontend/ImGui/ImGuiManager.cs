using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ImGuiManager
{
	private static readonly Lazy<ImGuiManager> Lazy = new(() => new ImGuiManager());

	public static ImGuiManager Instance => Lazy.Value;

	public float ComboBoxWidth = 100f;
	public bool IsOpened = false;

	private bool _isForceModInfoOpen = true;

	private ImGuiManager() { }

	public ImGuiManager Initialize()
	{
		LogManager.Info("ImGuiManager: Initializing...");

		IsOpened = ReframeworkManager.Instance.IsReframeworkMenuOpen;

		LogManager.Info("ImGuiManager: Initialized!");

		return this;
	}

	public void Draw()
	{
		try
		{
			if(!IsOpened)
			{
				return;
			}

			var configManager = ConfigManager.Instance;
			var localizationManager = LocalizationManager.Instance;

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

			ImGui.Begin($"{Constants.ModName} v{Constants.Version}", ref IsOpened);

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

			changed |= configManager.Customization.RenderImGui("config-settings");
			changed |= configManager.ActiveConfig.Data.GlobalSettings.RenderImGui("global-settings");
			changed |= configManager.ActiveConfig.Data.LargeMonsterUI.RenderImGui("large-monster-ui");

			if(menuFont != null)
			{
				//ImGui.PopFont();
			}

			ImGui.End();

			if(changed)
			{
				LogManager.Info("[ImGuiManager] Changes detected. Saving...");
				configManager.ActiveConfig.Save();
			}
		}
		catch(Exception e)
		{
			LogManager.Error(e);
		}
	}
}