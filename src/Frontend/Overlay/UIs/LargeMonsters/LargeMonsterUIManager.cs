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
		LogManager.Info("[LargeMonsterUIManager] Initializing...");

		_updateTimer = Timers.SetInterval(Update, 100);

		LogManager.Info("[LargeMonsterUIManager] Initialized!");
	}

	public void Update()
	{
		UpdateDynamic();
		UpdateStatic();
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		//UpdateAllDistances();
		DrawDynamicUi(backgroundDrawList);
		DrawStaticUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info($"[LargeMonsterUIManager] Disposing...");
		_updateTimer.Dispose();
		LogManager.Info($"[LargeMonsterUIManager] Disposed!");
	}

	//private void UpdateAllDistances()
	//{
	//	foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
	//	{
	//		var largeMonster = largeMonsterPair.Value;

	//		largeMonster.UpdateDistance();
	//	}
	//}

	private void UpdateDynamic()
	{
		var config = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;
		var settings = config.Settings;

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!settings.RenderDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			if(settings.MaxDistance > 0f && largeMonster.Distance > settings.MaxDistance)
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
		var config = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!config.Settings.RenderDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort

		if(config.Sorting.ReversedOrder)
		{
			switch(config.Sorting.Type)
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
			switch(config.Sorting.Type)
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
