using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{

	private List<LargeMonster> _dynamicLargeMonsters = [];
	private List<LargeMonster> _staticLargeMonsters = [];

	private System.Timers.Timer _updateTimer;

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

		_updateTimer = Timers.SetInterval(Update, 100);

		LogManager.Info("[LargeMonsterUiManager] Initialized!");
	}

	public void Update()
	{
		UpdateDynamic();
		UpdateStatic();
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUi(backgroundDrawList);
		DrawStaticUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info($"[LargeMonsterUiManager] Disposing...");
		_updateTimer.Dispose();
		LogManager.Info($"[LargeMonsterUiManager] Disposed!");
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;
		var settings = customization.Settings;

		if(!customization.Enabled)
		{
			_dynamicLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!settings.RenderDeadOrCaptured && !largeMonster.IsAlive)
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
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		if(!customization.Enabled)
		{
			_staticLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!customization.Settings.RenderDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort

		if(customization.Sorting.ReversedOrder)
		{
			switch(customization.Sorting.Type)
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
			switch(customization.Sorting.Type)
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
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;

		if(!customization.Enabled)
		{
			return;
		}

		for(var locationIndex = 0; locationIndex < _dynamicLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = _staticLargeMonsters[locationIndex];

			largeMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		if(!customization.Enabled)
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
