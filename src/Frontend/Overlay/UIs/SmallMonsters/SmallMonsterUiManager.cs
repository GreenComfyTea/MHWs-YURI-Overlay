using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class SmallMonsterUiManager : IDisposable
{
	private List<SmallMonster> _dynamicSmallMonsters = [];

	private readonly List<Timer> _timers = [];

	public SmallMonsterUiManager()
	{
		Initialize();
	}

	~SmallMonsterUiManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[SmallMonsterUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[SmallMonsterUiManager] Initialized!");
	}

	public void Update()
	{
		UpdateDynamic();
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info("[SmallMonsterUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[SmallMonsterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.SmallMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.DynamicList)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;
		var settings = customization.Settings;

		if(!customization.Enabled)
		{
			_dynamicSmallMonsters = [];
			return;
		}

		List<SmallMonster> newSmallMonsters = [];

		// Filter out dead and captured

		foreach(var smallMonsterPair in MonsterManager.Instance.SmallMonsters)
		{
			var smallMonster = smallMonsterPair.Value;

			if(!settings.RenderDeadMonsters && !smallMonster.IsAlive) continue;

			newSmallMonsters.Add(smallMonster);
		}

		// Sort by distance
		// Closest are drawn last
		newSmallMonsters.Sort(SmallMonsterSorting.CompareByDistanceReversed);

		_dynamicSmallMonsters = newSmallMonsters;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;

		if(!customization.Enabled) return;

		foreach(var smallMonster in _dynamicSmallMonsters)
		{
			smallMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}
}