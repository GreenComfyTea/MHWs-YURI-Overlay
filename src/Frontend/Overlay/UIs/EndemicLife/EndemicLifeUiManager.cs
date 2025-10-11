using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class EndemicLifeUiManager : IDisposable
{
	private List<EndemicLifeEntity> _dynamicEndemicLifeEntities = [];

	private readonly List<Timer> _timers = [];

	public EndemicLifeUiManager()
	{
		Initialize();
	}

	~EndemicLifeUiManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[EndemicLifeUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[EndemicLifeUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info("[EndemicLifeUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[EndemicLifeUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.EndemicLife)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.EndemicLifeUI;

		if(customization.Enabled != true)
		{
			_dynamicEndemicLifeEntities = [];
			return;
		}

		List<EndemicLifeEntity> newEndemicLifeEntities = [];

		// Filter out dead and captured

		foreach(var endemicLifeEntityPair in MonsterManager.Instance.EndemicLifeEntities)
		{
			var endemicLifeEntity = endemicLifeEntityPair.Value;

			newEndemicLifeEntities.Add(endemicLifeEntity);
		}

		// Sort by distance
		// Closest are drawn last
		newEndemicLifeEntities.Sort(EndemicLifeSorting.CompareByDistanceReversed);

		_dynamicEndemicLifeEntities = newEndemicLifeEntities;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.EndemicLifeUI;

		if(customization.Enabled != true) return;

		foreach(var endemicLifeEntity in _dynamicEndemicLifeEntities)
		{
			endemicLifeEntity.DynamicUi?.Draw(backgroundDrawList);
		}
	}
}