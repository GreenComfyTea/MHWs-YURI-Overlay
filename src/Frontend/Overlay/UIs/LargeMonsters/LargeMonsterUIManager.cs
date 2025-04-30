using System.Diagnostics;
using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{
	private List<LargeMonster> _dynamicLargeMonsters = [];
	private List<LargeMonster> _staticLargeMonsters = [];
	private LargeMonster _targetedLargeMonster = null;
	private LargeMonster _pinnedLargeMonster = null;

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
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.LargeMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.DynamicList)));
		_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.StaticList)));
		_timers.Add(Timers.SetInterval(UpdateTargeted, Utils.SecondsToMilliseconds(updateDelays.Targeted)));
		_timers.Add(Timers.SetInterval(UpdateMapPin, Utils.SecondsToMilliseconds(updateDelays.MapPin)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Dynamic.Settings;

		if(!customization.Enabled || !customization.Dynamic.Enabled)
		{
			_dynamicLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!settingsCustomization.RenderDeadMonsters && !largeMonster.IsAlive) continue;
			if(!settingsCustomization.RenderTargetedMonster && largeMonster.IsTargeted) continue;
			if(!settingsCustomization.RenderNonTargetedMonsters && !largeMonster.IsTargeted) continue;
			if(!settingsCustomization.RenderPinnedMonster && largeMonster.IsPinOn) continue;
			if(!settingsCustomization.RenderNonPinnedMonsters && !largeMonster.IsPinOn) continue;

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

		if(!customization.Enabled || !customization.Static.Enabled)
		{
			_staticLargeMonsters = [];
			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(!settingsCustomization.RenderDeadMonsters && !largeMonster.IsAlive) continue;
			if(!settingsCustomization.RenderTargetedMonster && largeMonster.IsTargeted) continue;
			if(!settingsCustomization.RenderNonTargetedMonsters && !largeMonster.IsTargeted) continue;
			if(!settingsCustomization.RenderPinnedMonster && largeMonster.IsPinOn) continue;
			if(!settingsCustomization.RenderNonPinnedMonsters && !largeMonster.IsPinOn) continue;

			newLargeMonsters.Add(largeMonster);
		}

		// Sort

		if(customization.Static.Sorting.ReversedOrder)
		{
			switch(customization.Static.Sorting.Type)
			{
				case Sorting.Id:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByIdReversed);
					break;
				case Sorting.Health:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthReversed);
					break;
				case Sorting.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByMaxHealthReversed);
					break;
				case Sorting.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthPercentageReversed);
					break;
				case Sorting.Distance:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByDistanceReversed);
					break;
				case Sorting.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByNameReversed);
					break;
			}
		}
		else
		{
			switch(customization.Static.Sorting.Type)
			{
				case Sorting.Id:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareById);
					break;
				case Sorting.Health:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealth);
					break;
				case Sorting.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByMaxHealth);
					break;
				case Sorting.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByHealthPercentage);
					break;
				case Sorting.Distance:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByDistance);
					break;
				case Sorting.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterStaticSorting.CompareByName);
					break;
			}
		}

		_staticLargeMonsters = newLargeMonsters;
	}

	private void UpdateTargeted()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Targeted.Settings;

		if(!customization.Enabled || !customization.Targeted.Enabled)
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

		if(!settingsCustomization.RenderDeadMonster && !newTargetedLargeMonster.IsAlive)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderTargetedMonster && newTargetedLargeMonster.IsTargeted)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderNonTargetedMonsters && !newTargetedLargeMonster.IsTargeted)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderPinnedMonster && newTargetedLargeMonster.IsPinOn)
		{
			_targetedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderNonPinnedMonsters && !newTargetedLargeMonster.IsPinOn)
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

		if(!customization.Enabled || !customization.MapPin.Enabled)
		{
			_pinnedLargeMonster = null;
			return;
		}

		var newPinnedLargeMonster = MonsterManager.Instance.LargeMonsters.FirstOrDefault(largeMonsterPair => largeMonsterPair.Value.IsPinOn).Value;
		if(newPinnedLargeMonster is null)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderDeadMonster && !newPinnedLargeMonster.IsAlive)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderTargetedMonster && newPinnedLargeMonster.IsTargeted)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderNonTargetedMonsters && !newPinnedLargeMonster.IsTargeted)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderPinnedMonster && newPinnedLargeMonster.IsPinOn)
		{
			_pinnedLargeMonster = null;
			return;
		}

		if(!settingsCustomization.RenderNonPinnedMonsters && !newPinnedLargeMonster.IsPinOn)
		{
			_pinnedLargeMonster = null;
			return;
		}


		_pinnedLargeMonster = newPinnedLargeMonster;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.Dynamic.Enabled) return;

		foreach(var largeMonster in _dynamicLargeMonsters)
		{
			largeMonster.DynamicUi.Draw(backgroundDrawList);
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.Static.Enabled) return;

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = _staticLargeMonsters[locationIndex];

			if(largeMonster.IsPinOn) largeMonster.StaticUi.Draw(backgroundDrawList, locationIndex);
		}
	}

	private void DrawTargetedUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.Targeted.Enabled) return;
		if(_targetedLargeMonster is null) return;

		_targetedLargeMonster.TargetedUi.Draw(backgroundDrawList);
	}

	private void DrawMapPinUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(!customization.Enabled || !customization.MapPin.Enabled) return;
		if(_pinnedLargeMonster is null) return;

		_pinnedLargeMonster.MapPinUi.Draw(backgroundDrawList);
	}
}