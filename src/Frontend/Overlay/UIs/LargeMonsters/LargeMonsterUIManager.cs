using Hexa.NET.ImGui;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{
	private List<LargeMonster> _dynamicLargeMonsters = [];
	private List<LargeMonster> _staticLargeMonsters = [];
	private LargeMonster? _targetedLargeMonster;
	private LargeMonster? _pinnedLargeMonster;

	private readonly List<Timer> _timers = [];

	public LargeMonsterUiManager()
	{
		this.Initialize();
	}

	~LargeMonsterUiManager()
	{
		this.Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[LargeMonsterUiManager] Initializing...");

		this.InitializeTimers();

		LogManager.Info("[LargeMonsterUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr drawList)
	{
		this.DrawDynamicUi(drawList);
		this.DrawStaticUi(drawList);
		this.DrawTargetedUi(drawList);
		this.DrawMapPinUi(drawList);
	}

	public void Dispose()
	{
		LogManager.Info("[LargeMonsterUiManager] Disposing...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[LargeMonsterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterDynamic)));
		this._timers.Add(Timers.SetInterval(this.UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterStatic)));
		this._timers.Add(Timers.SetInterval(this.UpdateTargeted, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterTargeted)));
		this._timers.Add(Timers.SetInterval(this.UpdateMapPin, Utils.SecondsToMilliseconds(updateDelays.LargeMonsterMapPin)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Dynamic.Settings;

		if(customization.Enabled != true || customization.Dynamic.Enabled != true)
		{
			this._dynamicLargeMonsters = [];

			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(settingsCustomization is not null)
			{
				if(settingsCustomization.RenderDeadMonsters != true && !largeMonster.IsAlive)
				{
					continue;
				}

				if(settingsCustomization.RenderTargetedMonster != true && largeMonster.IsTargeted)
				{
					continue;
				}

				if(settingsCustomization.RenderNonTargetedMonsters != true && !largeMonster.IsTargeted)
				{
					continue;
				}

				if(settingsCustomization.RenderPinnedMonster != true && largeMonster.IsPinned)
				{
					continue;
				}

				if(settingsCustomization.RenderNonPinnedMonsters != true && !largeMonster.IsPinned)
				{
					continue;
				}
			}

			newLargeMonsters.Add(largeMonster);
		}

		// Sort by distance
		// Closest are drawn last
		newLargeMonsters.Sort(LargeMonsterDynamicSorting.CompareByDistanceReversed);

		this._dynamicLargeMonsters = newLargeMonsters;
	}

	private void UpdateStatic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Static.Settings;

		if(customization.Enabled != true || customization.Static.Enabled != true)
		{
			this._staticLargeMonsters = [];

			return;
		}

		List<LargeMonster> newLargeMonsters = [];

		// Filters

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(settingsCustomization is not null)
			{
				if(settingsCustomization.RenderDeadMonsters != true && !largeMonster.IsAlive)
				{
					continue;
				}

				if(settingsCustomization.RenderTargetedMonster != true && largeMonster.IsTargeted)
				{
					continue;
				}

				if(settingsCustomization.RenderNonTargetedMonsters != true && !largeMonster.IsTargeted)
				{
					continue;
				}

				if(settingsCustomization.RenderPinnedMonster != true && largeMonster.IsPinned)
				{
					continue;
				}

				if(settingsCustomization.RenderNonPinnedMonsters != true && !largeMonster.IsPinned)
				{
					continue;
				}
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

		this._staticLargeMonsters = newLargeMonsters;
	}

	private void UpdateTargeted()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.Targeted.Settings;

		if(customization.Enabled != true || customization.Targeted.Enabled != true)
		{
			this._targetedLargeMonster = null;

			return;
		}

		var newTargetedLargeMonster = CameraManager.Instance.TargetedLargeMonster;

		if(newTargetedLargeMonster is null)
		{
			this._targetedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderDeadMonster != true && !newTargetedLargeMonster.IsAlive)
		{
			this._targetedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderTargetedMonster != true && newTargetedLargeMonster.IsTargeted)
		{
			this._targetedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderNonTargetedMonsters != true && !newTargetedLargeMonster.IsTargeted)
		{
			this._targetedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderPinnedMonster != true && newTargetedLargeMonster.IsPinned)
		{
			this._targetedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderNonPinnedMonsters != true && !newTargetedLargeMonster.IsPinned)
		{
			this._targetedLargeMonster = null;

			return;
		}

		this._targetedLargeMonster = newTargetedLargeMonster;
	}

	private void UpdateMapPin()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;
		var settingsCustomization = customization.MapPin.Settings;

		if(customization.Enabled != true || customization.MapPin.Enabled != true)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		var newPinnedLargeMonster = MonsterManager.Instance.LargeMonsters.FirstOrDefault(largeMonsterPair => largeMonsterPair.Value.IsPinned).Value;

		if(newPinnedLargeMonster is null)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderDeadMonster != true && !newPinnedLargeMonster.IsAlive)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderTargetedMonster != true && newPinnedLargeMonster.IsTargeted)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderNonTargetedMonsters != true && !newPinnedLargeMonster.IsTargeted)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderPinnedMonster != true && newPinnedLargeMonster.IsPinned)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		if(settingsCustomization.RenderNonPinnedMonsters != true && !newPinnedLargeMonster.IsPinned)
		{
			this._pinnedLargeMonster = null;

			return;
		}

		this._pinnedLargeMonster = newPinnedLargeMonster;
	}

	private void DrawDynamicUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Dynamic.Enabled != true)
		{
			return;
		}

		foreach(var largeMonster in this._dynamicLargeMonsters)
		{
			largeMonster.DynamicUi?.Draw(drawList);
		}
	}

	private void DrawStaticUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Static.Enabled != true)
		{
			return;
		}

		for(var locationIndex = 0; locationIndex < this._staticLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = this._staticLargeMonsters[locationIndex];

			largeMonster.StaticUi?.Draw(drawList, locationIndex);
		}
	}

	private void DrawTargetedUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.Targeted.Enabled != true)
		{
			return;
		}

		if(this._targetedLargeMonster is null)
		{
			return;
		}

		this._targetedLargeMonster.TargetedUi?.Draw(drawList);
	}

	private void DrawMapPinUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		if(customization.Enabled != true || customization.MapPin.Enabled != true)
		{
			return;
		}

		if(this._pinnedLargeMonster is null)
		{
			return;
		}

		this._pinnedLargeMonster.MapPinUi?.Draw(drawList);
	}
}