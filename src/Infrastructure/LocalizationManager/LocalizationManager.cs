namespace YURI_Overlay;

internal sealed class LocalizationManager : IDisposable
{
	private static readonly Lazy<LocalizationManager> Lazy = new(() => new LocalizationManager());

	public static LocalizationManager Instance => Lazy.Value;

	public LocalizationCustomization? Customization;

	public JsonDatabase<Localization>? ActiveLocalization;
	public JsonDatabase<Localization>? DefaultLocalization;
	public Dictionary<string, JsonDatabase<Localization>> Localizations = [];
	public EventHandler ActiveLocalizationChanged = delegate { };
	public EventHandler AnyLocalizationChanged = delegate { };

	private LocalizationWatcher? _localizationWatcherInstance;

	private LocalizationManager()
	{
	}

	~LocalizationManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[LocalizationManager] Initializing...");

		var configManager = ConfigManager.Instance;

		LoadAllLocalizations();

		if(configManager.ActiveConfig?.Data is not null)
		{
			ActivateLocalization(configManager.ActiveConfig.Data.GlobalSettings.Localization);
		}

		configManager.AnyConfigChanged += OnAnyConfigChanged;

		_localizationWatcherInstance = new LocalizationWatcher();
		Customization = new LocalizationCustomization();

		LogManager.Info("[LocalizationManager] Initialized!");
	}

	public void ActivateLocalization(JsonDatabase<Localization>? localization)
	{
		if(localization is null)
		{
			return;
		}

		LogManager.Info($"[LocalizationManager] Activating localization \"{localization.Name}\"...");

		ActiveLocalization = localization;

		EmitActiveLocalizationChanged();

		LogManager.Info($"[LocalizationManager] Localization \"{localization.Name}\" is activated!");
	}

	public void ActivateLocalization(string? name)
	{
		if(name is null)
		{
			return;
		}

		LogManager.Info($"[LocalizationManager] Searching for localization \"{name}\" to activate it...");

		var isGetConfigSuccess = Localizations.TryGetValue(name, out var localization);

		if(!isGetConfigSuccess)
		{
			LogManager.Info($"[LocalizationManager] localization \"{name}\" is not found.");
			LogManager.Info("[LocalizationManager] Activating default localization...");

			ActivateLocalization(DefaultLocalization);
			return;
		}

		LogManager.Info($"[LocalizationManager] Localization \"{name}\" is found!");

		ActivateLocalization(localization);
	}

	public void InitializeLocalization(string name)
	{
		LogManager.Info($"[LocalizationManager] Initializing localization \"{name}\"...");

		JsonDatabase<Localization> newLocalization = new(Constants.LocalizationsPath, name);
		if(newLocalization.Data is not null) newLocalization.Data.IsoCode = name;
		newLocalization.Save();

		newLocalization.Changed += OnLocalizationFileChanged!;
		newLocalization.RenamedFrom += OnLocalizationFileRenamedFrom!;
		newLocalization.RenamedTo += OnLocalizationFileRenamedTo!;
		newLocalization.Deleted += OnLocalizationFileDeleted!;
		newLocalization.Error += OnLocalizationFileError!;

		Localizations[name] = newLocalization;

		LogManager.Info($"[LocalizationManager] Localization \"{name}\" is initialized!");
	}

	public void Dispose()
	{
		LogManager.Info("[LocalizationManager] Disposing...");

		_localizationWatcherInstance?.Dispose();

		foreach(var localization in Localizations)
		{
			localization.Value.Dispose();
		}

		LogManager.Info("[LocalizationManager] Disposed!");
	}

	private void InitializeDefaultLocalization()
	{
		LogManager.Info("[LocalizationManager] Initializing default localization...");

		JsonDatabase<Localization> defaultLocalization = new(Constants.LocalizationsPath, Constants.DefaultLocalization);
		defaultLocalization.Data = new Localization();
		defaultLocalization.Save();
		Localizations[Constants.DefaultLocalization] = defaultLocalization;
		DefaultLocalization = defaultLocalization;

		LogManager.Info("[LocalizationManager] Default localization is initialized!");
	}

	private void LoadAllLocalizations()
	{
		try
		{
			LogManager.Info("[LocalizationManager] Loading all localizations...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.LocalizationsPath)!);

			var allConfigFilePathNames = Directory.GetFiles(Constants.LocalizationsPath);

			foreach(var configFilePathName in allConfigFilePathNames)
			{
				var name = Path.GetFileNameWithoutExtension(configFilePathName);

				if(name == Constants.DefaultLocalization) continue;

				InitializeLocalization(name);
			}

			InitializeDefaultLocalization();

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

		if(ActiveLocalization is null) return;
		if(configManager.ActiveConfig?.Data is null) return;

		if(ActiveLocalization.Name == configManager.ActiveConfig.Data.GlobalSettings.Localization) return;

		ActivateLocalization(configManager.ActiveConfig.Data.GlobalSettings.Localization);
	}

	private void OnLocalizationFileChanged(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file changed.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileCreated(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file created.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedFrom(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file renamed from.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedTo(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file renamed to.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileDeleted(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file deleted.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileError(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[LocalizationManager] Localization file throw an error.");
	}

	private void EmitActiveLocalizationChanged()
	{
		Utils.EmitEvents(this, ActiveLocalizationChanged);
	}

	private void EmitAnyLocalizationChanged()
	{
		Utils.EmitEvents(this, AnyLocalizationChanged);
	}
}