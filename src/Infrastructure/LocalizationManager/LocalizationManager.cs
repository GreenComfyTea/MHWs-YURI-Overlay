namespace YURI_Overlay;

internal sealed class LocalizationManager : IDisposable
{
	private static readonly Lazy<LocalizationManager> _lazy = new(() => new LocalizationManager());

	private LocalizationWatcher _localizationWatcherInstance;
	public JsonDatabase<Localization> ActiveLocalization;

	public LocalizationCustomization Customization;

	public JsonDatabase<Localization> DefaultLocalization;

	public Dictionary<string, JsonDatabase<Localization>> Localizations;

	private LocalizationManager()
	{
		this.Customization = new LocalizationCustomization(true);
		this.DefaultLocalization = new JsonDatabase<Localization>(true);
		this.ActiveLocalization = this.DefaultLocalization;
		this.Localizations = [];
		this._localizationWatcherInstance = new LocalizationWatcher(true);
	}

	public static LocalizationManager Instance => _lazy.Value;

	public void Dispose()
	{
		LogManager.Info("[LocalizationManager] Disposing...");

		this._localizationWatcherInstance.Dispose();

		foreach(var localization in this.Localizations)
		{
			localization.Value.Dispose();
		}

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info("[LocalizationManager] Disposed!");
	}

	public event EventHandler ActiveLocalizationChanged = delegate { };
	public event EventHandler AnyLocalizationChanged = delegate { };

	public void Initialize()
	{
		LogManager.Info("[LocalizationManager] Initializing...");

		var configManager = ConfigManager.Instance;

		this.LoadAllLocalizations();
		this.ActivateLocalization(configManager.ActiveConfig.Data.GlobalSettings.Localization);

		configManager.AnyConfigChanged += this.OnAnyConfigChanged;

		this._localizationWatcherInstance = new LocalizationWatcher();
		this.Customization = new LocalizationCustomization();

		LogManager.Info("[LocalizationManager] Initialized!");
	}

	public void ActivateLocalization(JsonDatabase<Localization> localization)
	{
		LogManager.Info($"[LocalizationManager] Activating localization \"{localization.Name}\"...");

		this.ActiveLocalization = localization;

		this.EmitActiveLocalizationChanged();

		LogManager.Info($"[LocalizationManager] Localization \"{localization.Name}\" is activated!");
	}

	public void ActivateLocalization(string? name)
	{
		if(name is null) return;

		LogManager.Info($"[LocalizationManager] Searching for localization \"{name}\" to activate it...");

		var isGetConfigSuccess = this.Localizations.TryGetValue(name, out var localization);

		if(!isGetConfigSuccess || localization is null)
		{
			LogManager.Info($"[LocalizationManager] localization \"{name}\" is not found.");
			LogManager.Info("[LocalizationManager] Activating default localization...");

			this.ActivateLocalization(this.DefaultLocalization);

			return;
		}

		LogManager.Info($"[LocalizationManager] Localization \"{name}\" is found!");

		this.ActivateLocalization(localization);
	}

	public void InitializeLocalization(string name)
	{
		LogManager.Info($"[LocalizationManager] Initializing localization \"{name}\"...");

		JsonDatabase<Localization> newLocalization = new(Constants.LOCALIZATIONS_PATH, name);
		newLocalization.Data.IsoCode = name;
		newLocalization.Save();

		newLocalization.Changed += this.OnLocalizationFileChanged;
		newLocalization.RenamedFrom += this.OnLocalizationFileRenamedFrom;
		newLocalization.RenamedTo += this.OnLocalizationFileRenamedTo;
		newLocalization.Deleted += this.OnLocalizationFileDeleted;
		newLocalization.Error += this.OnLocalizationFileError;

		this.Localizations[name] = newLocalization;

		LogManager.Info($"[LocalizationManager] Localization \"{name}\" is initialized!");
	}

	private void InitializeDefaultLocalization()
	{
		LogManager.Info("[LocalizationManager] Initializing default localization...");

		JsonDatabase<Localization> defaultLocalization = new(Constants.LOCALIZATIONS_PATH, Constants.DEFAULT_LOCALIZATION);
		defaultLocalization.Data = new Localization();
		defaultLocalization.Save();
		this.Localizations[Constants.DEFAULT_LOCALIZATION] = defaultLocalization;
		this.DefaultLocalization = defaultLocalization;

		LogManager.Info("[LocalizationManager] Default localization is initialized!");
	}

	private void LoadAllLocalizations()
	{
		try
		{
			LogManager.Info("[LocalizationManager] Loading all localizations...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.LOCALIZATIONS_PATH)!);

			var allConfigFilePathNames = Directory.GetFiles(Constants.LOCALIZATIONS_PATH);

			foreach(var configFilePathName in allConfigFilePathNames)
			{
				var name = Path.GetFileNameWithoutExtension(configFilePathName);

				if(name == Constants.DEFAULT_LOCALIZATION) continue;

				this.InitializeLocalization(name);
			}

			this.InitializeDefaultLocalization();

			LogManager.Info("[LocalizationManager] Loading all localizations is done!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnAnyConfigChanged(object? sender, EventArgs eventArgs)
	{
		var configManager = ConfigManager.Instance;

		if(this.ActiveLocalization.Name == configManager.ActiveConfig.Data.GlobalSettings.Localization) return;

		this.ActivateLocalization(configManager.ActiveConfig.Data.GlobalSettings.Localization);
	}

	private void OnLocalizationFileChanged(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file changed.");
		this.EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileCreated(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file created.");
		this.EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedFrom(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file renamed from.");
		this.EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedTo(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file renamed to.");
		this.EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileDeleted(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file deleted.");
		this.EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileError(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file throw an error.");
	}

	private void EmitActiveLocalizationChanged()
	{
		Utils.EmitEvents(this, this.ActiveLocalizationChanged);
	}

	private void EmitAnyLocalizationChanged()
	{
		Utils.EmitEvents(this, this.AnyLocalizationChanged);
	}
}