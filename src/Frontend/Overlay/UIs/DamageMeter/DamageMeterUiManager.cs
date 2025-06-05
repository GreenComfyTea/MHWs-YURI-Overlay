using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class DamageMeterUiManager : IDisposable
{
	private List<SmallMonster> _staticSmallMonsters = [];

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
		LogManager.Info("[SmallMonsterUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[SmallMonsterUiManager] Initialized!");
	}

	public void Update()
	{
		UpdateStatic();
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawStaticUi(backgroundDrawList);
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
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.SmallMonsters)));
	}

	private void UpdateStatic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;
		var settings = customization.Settings;

		if(!customization.Enabled)
		{
			_staticSmallMonsters = [];
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

		_staticSmallMonsters = newSmallMonsters;
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;

		if(!customization.Enabled) return;

		foreach(var smallMonster in _staticSmallMonsters)
		{
			smallMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}
}