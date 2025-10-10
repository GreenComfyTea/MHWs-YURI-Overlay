using ImGuiNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class DamageMeterUiManager : IDisposable
{
	private List<DamageMeterEntity> _damageMeterEntities = [];

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
		LogManager.Info("[DamageMeterUiManager] Initializing...");

		InitializeTimers();

		LogManager.Info("[DamageMeterUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		//DrawStaticUi(backgroundDrawList);
	}

	public void Dispose()
	{
		LogManager.Info("[DamageMeterUiManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[DamageMeterUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig?.Data?.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		//_timers.Add(Timers.SetInterval(UpdateStatic, Utils.SecondsToMilliseconds(updateDelays.DamageMeter)));
	}

	//private void UpdateStatic()
	//{
	//	var customization = ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;
	//	var settingsCustomization = customization.Settings;

	//	if(!customization.Enabled)
	//	{
	//		_damageMeterEntities = [];
	//		return;
	//	}

	//	List<DamageMeterEntity> newDamageMeterEntities = [];

	//	// Filters

	//	if(settingsCustomization.RenderLocalPlayer && DamageMeterManager.Instance.LocalPlayer is not null)
	//	{
	//		newDamageMeterEntities.Add(DamageMeterManager.Instance.LocalPlayer);
	//	}

	//	if(settingsCustomization.RenderOtherPlayers)
	//	{
	//		foreach(var otherPlayerPair in DamageMeterManager.Instance.OtherPlayers)
	//		{
	//			var otherPlayer = otherPlayerPair.Value;

	//			newDamageMeterEntities.Add(otherPlayer);
	//		}
	//	}

	//	if(settingsCustomization.RenderSupportHunters)
	//	{
	//		foreach(var supportHunterPair in DamageMeterManager.Instance.SupportHunters)
	//		{
	//			var supportHunter = supportHunterPair.Value;

	//			if(!settingsCustomization.RenderSupportHunters) continue;

	//			newDamageMeterEntities.Add(supportHunter);
	//		}
	//	}

	//	// Sort

	//	if(customization.Sorting.ReversedOrder)
	//	{
	//		switch(customization.Sorting.Type)
	//		{
	//			case DamageMeterSortingEnum.Id:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByIdReversed);
	//				break;
	//			case DamageMeterSortingEnum.Name:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByNameReversed);
	//				break;
	//			case DamageMeterSortingEnum.HunterRank:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByHunterRankReversed);
	//				break;
	//			case DamageMeterSortingEnum.MasterRank:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByMasterRankReversed);
	//				break;
	//			case DamageMeterSortingEnum.DamagePercentage:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDamagePercentageReversed);
	//				break;
	//			case DamageMeterSortingEnum.Dps:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDpsReversed);
	//				break;
	//			case DamageMeterSortingEnum.DpsPercentage:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDpsPercentageReversed);
	//				break;
	//			case DamageMeterSortingEnum.Damage:
	//			default:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDamageReversed);
	//				break;
	//		}
	//	}
	//	else
	//	{
	//		switch(customization.Sorting.Type)
	//		{
	//			case DamageMeterSortingEnum.Id:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareById);
	//				break;
	//			case DamageMeterSortingEnum.Name:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByName);
	//				break;
	//			case DamageMeterSortingEnum.HunterRank:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByHunterRank);
	//				break;
	//			case DamageMeterSortingEnum.MasterRank:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByMasterRank);
	//				break;
	//			case DamageMeterSortingEnum.DamagePercentage:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDamagePercentage);
	//				break;
	//			case DamageMeterSortingEnum.Dps:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDps);
	//				break;
	//			case DamageMeterSortingEnum.DpsPercentage:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDpsPercentage);
	//				break;
	//			case DamageMeterSortingEnum.Damage:
	//			default:
	//				newDamageMeterEntities.Sort(DamageMeterStaticSorting.CompareByDamage);
	//				break;
	//		}
	//	}

	//	_damageMeterEntities = newDamageMeterEntities;
	//}

	//private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	//{
	//	var customization = ConfigManager.Instance.ActiveConfig.Data.DamageMeterUI;

	//	if(!customization.Enabled) return;

	//	for(var locationIndex = 0; locationIndex < _damageMeterEntities.Count; locationIndex++)
	//	{
	//		var damageMeterEntity = _damageMeterEntities[locationIndex];

	//		damageMeterEntity.StaticUi.Draw(backgroundDrawList, locationIndex);
	//	}
	//}
}