using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SmallMonsterUiManager : IDisposable
{

	private List<SmallMonster> _dynamicSmallMonsters = [];

	private System.Timers.Timer _updateTimer;

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

		_updateTimer = Timers.SetInterval(Update, 100);

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
		LogManager.Info($"[SmallMonsterUiManager] Disposing...");

		_updateTimer.Dispose();

		LogManager.Info($"[SmallMonsterUiManager] Disposed!");
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

			if(!settings.RenderDeadMonsters && !smallMonster.IsAlive)
			{
				continue;
			}

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

		if(!customization.Enabled)
		{
			return;
		}

		foreach(var SmallMonster in _dynamicSmallMonsters)
		{
			SmallMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}
}
