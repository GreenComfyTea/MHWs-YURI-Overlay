using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class DamageMeterUiManager : IDisposable
{
	private readonly List<Timer> _timers = [];

	public DamageMeterUiManager()
	{
		Initialize();
	}

	~DamageMeterUiManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[DamageMeterUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[DamageMeterUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawStaticUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info("[DamageMeterUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[DamageMeterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.DamageMeter)));
	}

	private void UpdateStatic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;
		var settings = customization.Settings;

		if(!customization.Enabled)
		{
			return;
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;

		if(!customization.Enabled) return;

		var localPlayerUi = new DamageMeterStaticUi(0);
		var otherPlayerUi = new DamageMeterStaticUi(1);
		var supportHunterUi = new DamageMeterStaticUi(2);

		localPlayerUi.Draw(backgroundDrawList, 0);
		otherPlayerUi.Draw(backgroundDrawList, 1);
		otherPlayerUi.Draw(backgroundDrawList, 2);
		otherPlayerUi.Draw(backgroundDrawList, 3);
		supportHunterUi.Draw(backgroundDrawList, 4);
		supportHunterUi.Draw(backgroundDrawList, 5);
		supportHunterUi.Draw(backgroundDrawList, 6);
	}
}