using Hexa.NET.ImGui;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class SmallMonsterUiManager : IDisposable
{
	private List<SmallMonster> _dynamicSmallMonsters = [];

	private readonly List<Timer> _timers = [];

	public SmallMonsterUiManager()
	{
		this.Initialize();
	}

	~SmallMonsterUiManager()
	{
		this.Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[SmallMonsterUiManager] Initializing...");

		this.InitializeTimers();

		LogManager.Info("[SmallMonsterUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr drawList)
	{
		this.DrawDynamicUi(drawList);
	}

	public void Dispose()
	{
		LogManager.Info("[SmallMonsterUiManager] Disposing...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[SmallMonsterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.SmallMonsters)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;
		var settings = customization.Settings;

		if(customization.Enabled != true)
		{
			this._dynamicSmallMonsters = [];

			return;
		}

		List<SmallMonster> newSmallMonsters = [];

		// Filter out dead and captured

		foreach(var smallMonsterPair in MonsterManager.Instance.SmallMonsters)
		{
			var smallMonster = smallMonsterPair.Value;

			if(settings?.RenderDeadMonsters != true && !smallMonster.IsAlive)
			{
				continue;
			}

			newSmallMonsters.Add(smallMonster);
		}

		// Sort by distance
		// Closest are drawn last
		newSmallMonsters.Sort(SmallMonsterSorting.CompareByDistanceReversed);

		this._dynamicSmallMonsters = newSmallMonsters;
	}

	private void DrawDynamicUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.SmallMonsterUI;

		if(customization?.Enabled != true)
		{
			return;
		}

		foreach(var smallMonster in this._dynamicSmallMonsters)
		{
			smallMonster.DynamicUi?.Draw(drawList);
		}
	}
}