using System.Numerics;
using app;
using REFrameworkNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LargeMonster : IDisposable
{
	public EnemyCharacter EnemyCharacter;
	public cEnemyContext EnemyContext;

	public LargeMonsterDynamicUi DynamicUi;
	public LargeMonsterStaticUi StaticUi;
	public LargeMonsterTargetedUi TargetedUi;
	public LargeMonsterMapPinUi MapPinUi;

	public EnemyDef.ID Id = 0;
	public EnemyDef.ROLE_ID RoleId = 0;
	public EnemyDef.LEGENDARY_ID LegendaryId = 0;

	public string Name = "Large Monster";

	public bool IsTargeted = false;

	public int DynamicSortingPriority = 0;
	public int StaticSortingPriority = 0;

	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius = 0f;

	public Vector3 Position = Vector3.Zero;
	public float Distance = 0f;

	public bool IsAlive;
	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;

	public bool IsStaminaValid = true;
	public bool IsTired = false;
	public float Stamina = -1;
	public float MaxStamina = -1;
	public float StaminaPercentage = -1;

	public float StaminaTimerSeconds = -1;
	public float StaminaMaxTimerSeconds = -1;
	public float StaminaRemainingTimerSeconds = -1;
	public float StaminaRemainingTimerPercentage = -1;
	public string StaminaRemainingTimerString = "0:00";


	public bool IsRageValid = true;
	public bool IsEnraged = false;
	public float Rage = -1;
	public float MaxRage = -1;
	public float RagePercentage = -1;

	public float RageTimerSeconds = -1;
	public float RageMaxTimerSeconds = -1;
	public float RageRemainingTimerSeconds = -1;
	public float RageRemainingTimerPercentage = -1;
	public string RageRemainingTimerString = "0:00";

	private readonly List<Timer> _timers = [];

	private bool _isUpdateNamePending = true;
	private bool _isUpdateMissionBeaconOffsetPending = true;
	private bool _isUpdateModelRadiusPending = true;
	private bool _isUpdateHealthPending = true;
	private bool _isUpdateStaminaPending = true;
	private bool _isUpdateRagePending = true;
	private bool _isUpdateMapPinPending = true;

	private Type _stringType;

	private Method _nameStringMethod;

	public bool IsPinned = false;

	public LargeMonster(EnemyCharacter enemyCharacter, cEnemyContext enemyContext)
	{
		EnemyCharacter = enemyCharacter;
		EnemyContext = enemyContext;

		try
		{
			InitializeTdb();
			Initialize();

			DynamicUi = new LargeMonsterDynamicUi(this);
			StaticUi = new LargeMonsterStaticUi(this);
			TargetedUi = new LargeMonsterTargetedUi(this);
			MapPinUi = new LargeMonsterMapPinUi(this);

			ConfigManager.Instance.AnyConfigChanged += OnAnyConfigChanged;

			LogManager.Info($"[LargeMonster] Initialized {Name}!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[LargeMonster] Disposing {Name}...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		LogManager.Info($"[LargeMonster] {Name} Disposed!");
	}

	public void Update()
	{
		try
		{
			UpdatePosition();
			UpdateDistance();

			UpdateName();
			UpdateMissionBeaconOffset();
			UpdateModelRadius();

			UpdateHealth();
			var conditionsModule = UpdateStamina();
			UpdateRage(conditionsModule);
			UpdateMapPin();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void SetIsTargeted(bool newIsTargeted)
	{
		IsTargeted = newIsTargeted;
		UpdateSortingPriorities();
	}

	public void UpdateSortingPriorities()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		// Targeted Dynamic UI should be rendered last to be on top
		DynamicSortingPriority = IsTargeted ? 3 : 0;

		var sortingCustomization = customization.Static.Sorting;
		var targetedMonsterPriorityValue = PriorityUtils.ConvertPriorityToValue(sortingCustomization.TargetedMonsterPriority);
		var pinnedMonsterPriorityValue = PriorityUtils.ConvertPriorityToValue(sortingCustomization.PinnedMonsterPriority);


		if(IsTargeted && IsPinned)
		{
			if(targetedMonsterPriorityValue > 0)
			{
				StaticSortingPriority = targetedMonsterPriorityValue >= pinnedMonsterPriorityValue ? targetedMonsterPriorityValue : pinnedMonsterPriorityValue;
			}
			else if(pinnedMonsterPriorityValue > 0)
			{
				StaticSortingPriority = pinnedMonsterPriorityValue;
			}
			else
			{
				StaticSortingPriority = targetedMonsterPriorityValue <= pinnedMonsterPriorityValue ? targetedMonsterPriorityValue : pinnedMonsterPriorityValue;
			}
		}
		else if(IsTargeted)
		{
			StaticSortingPriority = targetedMonsterPriorityValue;
		}
		else if(IsPinned)
		{
			StaticSortingPriority = pinnedMonsterPriorityValue;
		}
		else
		{
			StaticSortingPriority = 0;
		}
	}

	private void Initialize()
	{
		try
		{
			InitializeTimers();
			UpdateIds();
			Update();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.LargeMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(SetUpdateNamePending, Utils.SecondsToMilliseconds(updateDelays.Name)));
		_timers.Add(Timers.SetInterval(SetUpdateMissionBeaconOffset,
			Utils.SecondsToMilliseconds(updateDelays.MissionBeaconOffset)));
		_timers.Add(Timers.SetInterval(SetUpdateModelRadius, Utils.SecondsToMilliseconds(updateDelays.ModelRadius)));
		_timers.Add(Timers.SetInterval(SetUpdateHealthPending, Utils.SecondsToMilliseconds(updateDelays.Health)));
		_timers.Add(Timers.SetInterval(SetUpdateStaminaPending, Utils.SecondsToMilliseconds(updateDelays.Stamina)));
		_timers.Add(Timers.SetInterval(SetUpdateRagePending, Utils.SecondsToMilliseconds(updateDelays.Rage)));
		_timers.Add(Timers.SetInterval(SetUpdateMapPinPending, Utils.SecondsToMilliseconds(updateDelays.MapPin)));
	}

	private void SetUpdateNamePending()
	{
		_isUpdateNamePending = true;
	}

	private void SetUpdateMissionBeaconOffset()
	{
		_isUpdateMissionBeaconOffsetPending = true;
	}

	private void SetUpdateModelRadius()
	{
		_isUpdateModelRadiusPending = true;
	}

	private void SetUpdateHealthPending()
	{
		_isUpdateHealthPending = true;
	}

	private void SetUpdateStaminaPending()
	{
		_isUpdateStaminaPending = true;
	}

	private void SetUpdateRagePending()
	{
		_isUpdateRagePending = true;
	}

	private void SetUpdateMapPinPending()
	{
		_isUpdateMapPinPending = true;
	}

	private void UpdatePosition()
	{
		try
		{
			var position = EnemyCharacter.Pos;
			if(position is null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos");
				return;
			}

			this.Position.X = position.x;
			this.Position.Y = position.y;
			this.Position.Z = position.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateDistance()
	{
		Distance = Vector3.Distance(Position, PlayerManager.Instance.Position);
	}

	private void UpdateIds()
	{
		try
		{
			var basicModule = EnemyContext.Basic;
			if(basicModule is null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy basic module");
				return;
			}

			Id = basicModule.EmID;
			RoleId = basicModule.RoleID;
			LegendaryId = basicModule.LegendaryID;
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

			var name = (string) _nameStringMethod.InvokeBoxed(_stringType, null, [Id, RoleId, LegendaryId]);
			if(name is null)
			{
				LogManager.Warn("[LargeMonster.UpdateName] No enemy name");
				return;
			}

			this.Name = name;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateMissionBeaconOffset()
	{
		try
		{
			if(!_isUpdateMissionBeaconOffsetPending) return;
			_isUpdateMissionBeaconOffsetPending = false;

			var missionBeaconOffset = EnemyContext.MissionBeaconOffset;
			if(missionBeaconOffset is null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");
				return;
			}

			this.MissionBeaconOffset.X = missionBeaconOffset.x;
			this.MissionBeaconOffset.Y = missionBeaconOffset.y;
			this.MissionBeaconOffset.Z = missionBeaconOffset.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateModelRadius()
	{
		try
		{
			if(!_isUpdateModelRadiusPending) return;
			_isUpdateModelRadiusPending = false;

			ModelRadius = EnemyContext.ModelRadius;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	// Has to be called from in-game update method, because it is encrypted protected memory.
	private void UpdateHealth()
	{
		try
		{
			if(!_isUpdateHealthPending) return;
			_isUpdateHealthPending = false;

			var healthManager = EnemyCharacter.HealthMgr;
			if(healthManager is null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health manager");
				return;
			}

			Health = healthManager.Health;
			MaxHealth = healthManager.MaxHealth;
			if(!Utils.IsApproximatelyEqual(MaxHealth, 0f)) HealthPercentage = Health / MaxHealth;

			IsAlive = !Utils.IsApproximatelyEqual(Health, 0f);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private cEmModuleConditions UpdateStamina()
	{
		try
		{
			if(!_isUpdateStaminaPending) return null;
			_isUpdateStaminaPending = false;

			var conditionsModule = EnemyContext.Conditions;
			if(conditionsModule is null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions module");
				return null;
			}

			var tiredCondition = conditionsModule.Tired;
			if(tiredCondition is null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy tired condition");
				return conditionsModule;
			}

			IsStaminaValid = tiredCondition.IsValid;
			if(!IsStaminaValid) return conditionsModule;

			IsTired = tiredCondition.IsActive;

			if(IsTired)
			{
				StaminaTimerSeconds = tiredCondition.CurrentTimer;
				StaminaMaxTimerSeconds = tiredCondition.ActivateTime;

				StaminaRemainingTimerSeconds = StaminaMaxTimerSeconds - StaminaTimerSeconds;

				if(!Utils.IsApproximatelyEqual(StaminaMaxTimerSeconds, 0))
					StaminaRemainingTimerPercentage = StaminaRemainingTimerSeconds / StaminaMaxTimerSeconds;

				StaminaRemainingTimerString = Utils.FormatTimer(StaminaRemainingTimerSeconds, StaminaMaxTimerSeconds);

				return conditionsModule;
			}

			Stamina = tiredCondition.Stamina;
			MaxStamina = tiredCondition.DefaultStamina;

			if(!Utils.IsApproximatelyEqual(MaxStamina, 0)) StaminaPercentage = Stamina / MaxStamina;

			return conditionsModule;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return null;
		}
	}

	private void UpdateRage(cEmModuleConditions conditionsModule)
	{
		try
		{
			if(!_isUpdateRagePending) return;
			_isUpdateRagePending = false;

			if(conditionsModule is null)
			{
				conditionsModule = EnemyContext.Conditions;
				if(conditionsModule is null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions module");
					return;
				}
			}

			var angryCondition = conditionsModule.Angry;
			if(angryCondition is null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry condition");
				return;
			}

			IsRageValid = angryCondition.IsValid;
			if(!IsRageValid) return;

			IsEnraged = angryCondition.IsActive;

			if(IsEnraged)
			{
				RageTimerSeconds = angryCondition.CurrentTimer;
				RageMaxTimerSeconds = angryCondition.ActivateTime;

				RageRemainingTimerSeconds = RageMaxTimerSeconds - RageTimerSeconds;

				if(!Utils.IsApproximatelyEqual(RageMaxTimerSeconds, 0))
					RageRemainingTimerPercentage = RageRemainingTimerSeconds / RageMaxTimerSeconds;

				RageRemainingTimerString = Utils.FormatTimer(RageRemainingTimerSeconds, RageMaxTimerSeconds);

				return;
			}

			Rage = angryCondition.Value;
			MaxRage = angryCondition.LimitValue;

			if(!Utils.IsApproximatelyEqual(MaxRage, 0)) RagePercentage = Rage / MaxRage;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateMapPin()
	{
		try
		{
			if(!_isUpdateMapPinPending) return;
			_isUpdateMapPinPending = false;

			IsPinned = EnemyContext.IsTargetPinOn;
			UpdateSortingPriorities();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void InitializeTdb()
	{
		try
		{
			var enemyDefTypeDef = EnemyDef.REFType;

			_nameStringMethod = enemyDefTypeDef.GetMethod("NameString");

			_stringType = _nameStringMethod.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnAnyConfigChanged(object sender, EventArgs e)
	{
		InitializeTimers();
	}
}