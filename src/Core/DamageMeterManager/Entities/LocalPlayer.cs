using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app;
using REFrameworkNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LocalPlayer : DamageMeterEntity
{
	public cPlayerManageInfo PlayerManageInfo;

	public float AwardDamage = 0f;

	private readonly List<Timer> _timers = [];

	private bool _isUpdateNamePending = true;
	private bool _isUpdateMemberIndexPending = true;
	private bool _isUpdateHunterRankPending = true;

	public LocalPlayer(cPlayerManageInfo playerManageInfo)
	{
		PlayerManageInfo = playerManageInfo;

		Name = "Local Player";

		try
		{
			Initialize();

			Type = DamageMeterEntityType.LocalPlayer;
			StaticUi = new DamageMeterStaticUi(this);

			LogManager.Info($"[DamageMeter] Initialized Local Player: {Name}!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public override void Dispose()
	{
		try
		{
			foreach(var timer in _timers)
			{
				timer.Dispose();
			}

			_timers.Clear();
			LogManager.Info($"[DamageMeter] Disposed Local Player: {Name}!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Update()
	{
		try
		{
			UpdateMemberIndex();
			UpdateName();
			UpdateHunterRank();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void UpdateAwardDamage()
	{
		try
		{
			var missionManager = API.GetManagedSingletonT<MissionManager>();
			if(missionManager is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] No mission manager");
				return;
			}

			var questDirection = missionManager.QuestDirector;
			if(questDirection is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] No quest director");
				return;
			}

			var questFlowParam = questDirection.Param;
			if(questFlowParam is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] No quest flow param");
				return;
			}

			var questAwardInfo = questFlowParam.QuestAwardInfo;
			if(questAwardInfo is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] No quest award info");
				return;
			}

			if(questAwardInfo.Length < 5)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] quest award info length < 5");
				return;
			}

			var questAwardUnion = questAwardInfo.Get(4);
			if(questAwardUnion is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateDamage] No quest award union");
				return;
			}

			var byte0 = questAwardUnion.Byte0;
			var byte1 = questAwardUnion.Byte1;
			var byte2 = questAwardUnion.Byte2;
			var byte3 = questAwardUnion.Byte3;

			byte[] bytes = [byte0, byte1, byte2, byte3];

			if(!BitConverter.IsLittleEndian) Array.Reverse(bytes);

			var damage = BitConverter.ToSingle(bytes, 0);


			if(Utils.IsApproximatelyEqual(damage, 0f))
			{
				return;
			}

			AwardDamage = damage;

			DisplayedDamage = damage;
			DisplayedDamagePercentage = 1f;
			DisplayedDps = 69f;
			DisplayedDpsPercentage = 1f;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void Initialize()
	{
		InitializeTimers();
		Update();
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.LargeMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(SetUpdateNamePending, Utils.SecondsToMilliseconds(0.1f)));
		_timers.Add(Timers.SetInterval(SetUpdateMemberIndexPending, Utils.SecondsToMilliseconds(0.1f)));
		_timers.Add(Timers.SetInterval(SetUpdateHunterRankPending, Utils.SecondsToMilliseconds(0.1f)));
	}

	private void SetUpdateNamePending()
	{
		_isUpdateNamePending = true;
	}

	private void SetUpdateMemberIndexPending()
	{
		_isUpdateMemberIndexPending = true;
	}

	private void SetUpdateHunterRankPending()
	{
		_isUpdateHunterRankPending = true;
	}

	private void UpdateMemberIndex()
	{
		try
		{
			if(!_isUpdateMemberIndexPending) return;
			_isUpdateMemberIndexPending = false;

			var contextHolder = PlayerManageInfo.ContextHolder;
			if(contextHolder is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateMemberIndex] No player context holder");
				return;
			}

			var playerContext = contextHolder.Pl;
			if(playerContext is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateMemberIndex] No player context");
				return;
			}

			Id = playerContext.StableMemberIndex;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateName()
	{
		try
		{
			if(!_isUpdateNamePending) return;
			_isUpdateNamePending = false;

			var contextHolder = PlayerManageInfo.ContextHolder;
			if(contextHolder is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateMemberIndex] No player context holder");
				return;
			}

			var playerContext = contextHolder.Pl;
			if(playerContext is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateMemberIndex] No player context");
				return;
			}

			var name = playerContext.PlayerName;
			if(name is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateName] No player name");
				return;
			}

			Name = name;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateHunterRank()
	{
		try
		{
			if(!_isUpdateHunterRankPending) return;
			_isUpdateHunterRankPending = false;

			var saveDataManager = API.GetManagedSingletonT<SaveDataManager>();
			if(saveDataManager is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateHunterRank] No save data manager");
				return;
			}

			var saveDataHelper = saveDataManager.Helper;
			if(saveDataHelper is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateHunterRank] No save data helper");
				return;
			}

			var commonParam = saveDataHelper.CommonParam;
			if(commonParam is null)
			{
				LogManager.Warn("[LocalPlayer.UpdateHunterRank] No common param");
				return;
			}

			HunterRank = commonParam._HunterRank;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}