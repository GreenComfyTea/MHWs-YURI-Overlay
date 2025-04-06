using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{

	private List<LargeMonster> _dynamicLargeMonsters = [];
	private List<LargeMonster> _staticLargeMonsters = [];

	private readonly List<System.Timers.Timer> _timers = [];

	public LargeMonsterUiManager()
	{
		Initialize();
	}

	~LargeMonsterUiManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[LargeMonsterUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[LargeMonsterUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUi(backgroundDrawList);
		DrawStaticUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info($"[LargeMonsterUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info($"[LargeMonsterUiManager] Disposed!");
	}
	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.LargeMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.DynamicList)));
		_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.StaticList)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var renderDeadMonsters = customization.Dynamic.Settings.RenderDeadMonsters;

		if(!customization.Enabled || !customization.Dynamic.Enabled)
		{
			_dynamicLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!renderDeadMonsters && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort by distance
		// Closest are drawn last
		newLargeMonsters.Sort(LargeMonsterSorting.CompareByDistanceReversed);

		_dynamicLargeMonsters = newLargeMonsters;
	}

	private void UpdateStatic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var renderDeadMonsters = customization.Dynamic.Settings.RenderDeadMonsters;

		if(!customization.Enabled || !customization.Dynamic.Enabled)
		{
			_staticLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!renderDeadMonsters && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort

		if(customization.Static.Sorting.ReversedOrder)
		{
			switch(customization.Static.Sorting.Type)
			{
				case Sortings.Id:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByIdReversed);
					break;
				case Sortings.Health:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthReversed);
					break;
				case Sortings.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByMaxHealthReversed);
					break;
				case Sortings.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthPercentageReversed);
					break;
				case Sortings.Distance:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByDistanceReversed);
					break;
				case Sortings.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByNameReversed);
					break;
			}
		}
		else
		{
			switch(customization.Static.Sorting.Type)
			{
				case Sortings.Id:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareById);
					break;
				case Sortings.Health:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealth);
					break;
				case Sortings.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByMaxHealth);
					break;
				case Sortings.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthPercentage);
					break;
				case Sortings.Distance:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByDistance);
					break;
				case Sortings.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByName);
					break;
			}
		}

		_staticLargeMonsters = newLargeMonsters;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.Dynamic.Enabled)
		{
			return;
		}

		foreach(var largeMonster in _dynamicLargeMonsters)
		{
			largeMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.Static.Enabled)
		{
			return;
		}

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = _staticLargeMonsters[locationIndex];

			largeMonster.StaticUi.Draw(backgroundDrawList, locationIndex);
		}
	}
}
