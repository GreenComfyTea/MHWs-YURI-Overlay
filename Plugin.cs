using System.Diagnostics.CodeAnalysis;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using REFrameworkNET.Callbacks;

namespace YURI_Overlay;

// Cool plugin, very wow
[SuppressMessage("Roslynator", "RCS1102:Make class Static", Justification = "<Pending>")]
public class Plugin
{
	public static bool IsInitialized { get; private set; }

	[PluginEntryPoint]
	public static void Main()
	{
		LogManager.Info("Entering the void...");

		try
		{
			Task.Run(Initialize);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	[PluginExitPoint]
	public static void Unload()
	{
		LogManager.Info("Disposing...");

		//DamageMeterManager.Instance.Dispose();
		MonsterManager.Instance.Dispose();

		PlayerManager.Instance.Dispose();
		ScreenManager.Instance.Dispose();

		LocalizationManager.Instance.Dispose();
		ConfigManager.Instance.Dispose();

		API.LocalFrameGC();

		LogManager.Info("Disposed!");
		LogManager.Info("I permitted it to pass over me and through me. When it had gone past I turned the inner eye to see its path. Where the fear had gone, there was nothing. Only I remained...");
	}

	private static void Initialize()
	{
		try
		{
			LogManager.Info("Managers: Initializing...");

			var configManager = ConfigManager.Instance;
			var localizationManager = LocalizationManager.Instance;
			var localizationHelper = LocalizationHelper.Instance;
			//var luaFontManager = LuaFontManager.Instance;

			//var fontManager = FontManager.Instance;

			var imGuiManager = ImGuiManager.Instance;
			var overlayManager = OverlayManager.Instance;

			var screenManager = ScreenManager.Instance;
			var playerManager = PlayerManager.Instance;

			var monsterManager = MonsterManager.Instance;
			var cameraManager = CameraManager.Instance;
			//var damageMeterManager = DamageMeterManager.Instance;

			configManager.Initialize();
			localizationManager.Initialize();
			localizationHelper.Initialize();
			//luaFontManager.Initialize();

			//fontManager.Initialize();

			imGuiManager.Initialize();
			overlayManager.Initialize();

			screenManager.Initialize();
			playerManager.Initialize();

			monsterManager.Initialize();
			cameraManager.Initialize();
			//damageMeterManager.Initialize();

			LogManager.Info("Managers: Initialized!");
			LogManager.Info("Callbacks: Initializing...");

			UpdateBehavior.Post += OnUpdate;
			ImGuiDrawUI.Post += OnImGuiDrawUi;
			ImGuiRender.Post += OnImGuiRender;

			IsInitialized = true;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private static void OnUpdate()
	{
		//try
		//{
		//	LuaFontManager.Instance.GameUpdate();
		//}
		//catch(Exception exception)
		//{
		//	LogManager.Error(exception);
		//}

		ScreenManager.Instance.GameUpdate();
		//DamageMeterManager.Instance.Update();
	}

	private static void OnImGuiDrawUi()
	{
		if(!IsInitialized) return;

		ImGuiManager.Instance.Draw();
	}

	private static void OnImGuiRender()
	{
		if(!IsInitialized) return;

		OverlayManager.Instance.Draw();
	}
}