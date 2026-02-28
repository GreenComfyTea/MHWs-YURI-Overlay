using System.Diagnostics;
using Hexa.NET.ImGui;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{
	private List<LargeMonster> _dynamicLargeMonsters = [];
	private List<LargeMonster> _staticLargeMonsters = [];
	private LargeMonster? _targetedLargeMonster = null;
	private LargeMonster? _pinnedLargeMonster = null;

	private readonly List<Timer> _timers = [];

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
		DrawTargetedUi(backgroundDrawList);
		DrawMapPinUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info("[LargeMonsterUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[LargeMonsterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterDynamic)));
		_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterStatic)));
		_timers.Add(Timers.SetInterval(UpdateTargeted, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterTargeted)));
		_timers.Add(Timers.SetInterval(UpdateMapPin, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterMapPin)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Dynamic.Settings;

		if(customization.Enabled != true || customization.Dynamic.Enabled != true)
		{
			_dynamicLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(settingsCustomization is not null)
			{
				if(settingsCustomization.RenderDeadMonsters != true && !largeMonster.IsAlive) continue;
				if(settingsCustomization.RenderTargetedMonster != true && largeMonster.IsTargeted) continue;
				if(settingsCustomization.RenderNonTargetedMonsters != true && !largeMonster.IsTargeted) continue;
				if(settingsCustomization.RenderPinnedMonster != true && largeMonster.IsPinned) continue;
				if(settingsCustomization.RenderNonPinnedMonsters != true && !largeMonster.IsPinned) continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort by distance
		// Closest are drawn last
		newLargeMonsters.Sort(LargeMonsterDynamicSorting.CompareByDistanceReversed);

		_dynamicLargeMonsters = newLargeMonsters;
	}

	private void UpdateStatic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Static.Settings;

		if(customization.Enabled != true || customization.Static.Enabled != true)
		{
			_staticLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(settingsCustomization is not null)
			{
				if(settingsCustomization.RenderDeadMonsters != true && !largeMonster.IsAlive) continue;
				if(settingsCustomization.RenderTargetedMonster != true && largeMonster.IsTargeted) continue;
				if(settingsCustomization.RenderNonTargetedMonsters != true && !largeMonster.IsTargeted) continue;
				if(settingsCustomization.RenderPinnedMonster != true && largeMonster.IsPinned) continue;
				if(settingsCustomization.RenderNonPinnedMonsters != true && !largeMonster.IsPinned) continue;
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort

		if(customization.Static.Sorting.ReversedOrder == true)
		{
			switch(customization.Static.Sorting.Type)
			{
				case SortingEnum.Id:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByIdReversed);
					break;
				case SortingEnum.Name:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByNameReversed);
					break;
				case SortingEnum.Health:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthReversed);
					break;
				case SortingEnum.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByMaxHealthReversed);
					break;
				case SortingEnum.Distance:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByDistanceReversed);
					break;
				case SortingEnum.HealthPercentage:
				default:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthPercentageReversed);
					break;
			}
		}
		else
		{
			switch(customization.Static.Sorting.Type)
			{
				case SortingEnum.Id:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareById);
					break;
				case SortingEnum.Name:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByName);
					break;
				case SortingEnum.Health:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealth);
					break;
				case SortingEnum.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByMaxHealth);
					break;

				case SortingEnum.Distance:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByDistance);
					break;
				case SortingEnum.HealthPercentage:
				default:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthPercentage);
					break;
			}
		}

		_staticLargeMonsters = newLargeMonsters;
	}

	private void UpdateTargeted()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Targeted.Settings;

		if(customization.Enabled != true || customization.Targeted.Enabled != true)
		{
			_targetedLargeMonster = null;
			return;
		}

		var newTargetedLargeMonster = CameraManager.Instance.TargetedLargeMonster;
		if(newTargetedLargeMonster is null)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderDeadMonster != true && !newTargetedLargeMonster.IsAlive)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderTargetedMonster != true && newTargetedLargeMonster.IsTargeted)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderNonTargetedMonsters != true && !newTargetedLargeMonster.IsTargeted)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderPinnedMonster != true && newTargetedLargeMonster.IsPinned)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderNonPinnedMonsters != true && !newTargetedLargeMonster.IsPinned)
		{
			_targetedLargeMonster = null;
			return;
		}


		_targetedLargeMonster = newTargetedLargeMonster;
	}

	private void UpdateMapPin()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.MapPin.Settings;

		if(customization.Enabled != true || customization.MapPin.Enabled != true)
		{
			_pinnedLargeMonster = null;
			return;
		}

		var newPinnedLargeMonster = MonsterManager.Instance.LargeMonsters.FirstOrDefault(largeMonsterPair => largeMonsterPair.Value.IsPinned).Value;
		if(newPinnedLargeMonster is null)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderDeadMonster != true && !newPinnedLargeMonster.IsAlive)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderTargetedMonster != true && newPinnedLargeMonster.IsTargeted)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderNonTargetedMonsters != true && !newPinnedLargeMonster.IsTargeted)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderPinnedMonster != true && newPinnedLargeMonster.IsPinned)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(settingsCustomization.RenderNonPinnedMonsters != true && !newPinnedLargeMonster.IsPinned)
		{
			_pinnedLargeMonster = null;
			return;
		}


		_pinnedLargeMonster = newPinnedLargeMonster;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Dynamic.Enabled != true) return;

		foreach(var largeMonster in _dynamicLargeMonsters)
		{
			largeMonster.DynamicUi?.Draw(backgroundDrawList);
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Static.Enabled != true) return;

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = _staticLargeMonsters[locationIndex];

			largeMonster.StaticUi?.Draw(backgroundDrawList, locationIndex);
		}
	}

	private void DrawTargetedUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Targeted.Enabled != true) return;
		if(_targetedLargeMonster is null) return;

		_targetedLargeMonster.TargetedUi?.Draw(backgroundDrawList);
	}

	private void DrawMapPinUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.MapPin.Enabled != true) return;
		if(_pinnedLargeMonster is null) return;

		_pinnedLargeMonster.MapPinUi?.Draw(backgroundDrawList);
	}
}