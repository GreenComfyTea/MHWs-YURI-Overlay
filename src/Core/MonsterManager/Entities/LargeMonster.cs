using System.Numerics;
using app;
using REFrameworkNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LargeMonster : IDisposable
{
	public EnemyCharacter EnemyCharacter;
	public cEnemyContext EnemyContext;

	public LargeMonsterDynamicUi? DynamicUi;
	public LargeMonsterStaticUi? StaticUi;
	public LargeMonsterTargetedUi? TargetedUi;
	public LargeMonsterMapPinUi? MapPinUi;

	public EnemyDef.ID Id = 0;
	public EnemyDef.ROLE_ID RoleId = 0;
	public EnemyDef.LEGENDARY_ID LegendaryId = 0;

	public string Name = "Large Monster";

	public bool IsTargeted;

	public int DynamicSortingPriority;
	public int StaticSortingPriority;

	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius;

	public Vector3 Position = Vector3.Zero;
	public float Distance;

	public bool IsAlive;
	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;

	public bool IsStaminaValid = true;
	public bool IsTired;
	public float Stamina = -1;
	public float MaxStamina = -1;
	public float StaminaPercentage = -1;

	public float StaminaTimerSeconds = -1;
	public float StaminaMaxTimerSeconds = -1;
	public float StaminaRemainingTimerSeconds = -1;
	public float StaminaRemainingTimerPercentage = -1;
	public string StaminaRemainingTimerString = "0:00";

	public bool IsRageValid = true;
	public bool IsEnraged;
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

	private Type? _stringType;

	private Method? _nameStringMethod;

	public bool IsPinned;

	public LargeMonster(EnemyCharacter enemyCharacter, cEnemyContext enemyContext)
	{
		this.EnemyCharacter = enemyCharacter;
		this.EnemyContext = enemyContext;

		try
		{
			this.InitializeTdb();
			this.Initialize();

			this.DynamicUi = new LargeMonsterDynamicUi(this);
			this.StaticUi = new LargeMonsterStaticUi(this);
			this.TargetedUi = new LargeMonsterTargetedUi(this);
			this.MapPinUi = new LargeMonsterMapPinUi(this);

			ConfigManager.Instance.AnyConfigChanged += this.OnAnyConfigChanged;

			LogManager.Info($"[LargeMonster] Initialized {this.Name}!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[LargeMonster] Disposing {this.Name}...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info($"[LargeMonster] {this.Name} Disposed!");
	}

	public void Update()
	{
		try
		{
			this.UpdatePosition();
			this.UpdateDistance();

			this.UpdateName();
			this.UpdateMissionBeaconOffset();
			this.UpdateModelRadius();

			this.UpdateHealth();
			var conditionsModule = this.UpdateStamina();
			this.UpdateRage(conditionsModule);
			this.UpdateMapPin();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void SetIsTargeted(bool newIsTargeted)
	{
		this.IsTargeted = newIsTargeted;
		this.UpdateSortingPriorities();
	}

	public void UpdateSortingPriorities()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI;

		// Targeted Dynamic UI should be rendered last to be on top
		this.DynamicSortingPriority = this.IsTargeted ? 3 : 0;

		var sortingCustomization = customization.Static.Sorting;
		var targetedMonsterPriorityValue = PriorityUtils.ConvertPriorityToValue(sortingCustomization.TargetedMonsterPriority);
		var pinnedMonsterPriorityValue = PriorityUtils.ConvertPriorityToValue(sortingCustomization.PinnedMonsterPriority);

		if(this.IsTargeted && this.IsPinned)
		{
			if(targetedMonsterPriorityValue > 0)
			{
				this.StaticSortingPriority = targetedMonsterPriorityValue >= pinnedMonsterPriorityValue ? targetedMonsterPriorityValue : pinnedMonsterPriorityValue;
			}
			else if(pinnedMonsterPriorityValue > 0)
			{
				this.StaticSortingPriority = pinnedMonsterPriorityValue;
			}
			else
			{
				this.StaticSortingPriority = targetedMonsterPriorityValue <= pinnedMonsterPriorityValue ? targetedMonsterPriorityValue : pinnedMonsterPriorityValue;
			}
		}
		else if(this.IsTargeted)
		{
			this.StaticSortingPriority = targetedMonsterPriorityValue;
		}
		else if(this.IsPinned)
		{
			this.StaticSortingPriority = pinnedMonsterPriorityValue;
		}
		else
		{
			this.StaticSortingPriority = 0;
		}
	}

	private void Initialize()
	{
		try
		{
			this.InitializeTimers();
			this.UpdateIds();
			this.Update();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.LargeMonsters;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.SetUpdateNamePending, Utils.SecondsToMilliseconds(updateDelays.Name)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateMissionBeaconOffset, Utils.SecondsToMilliseconds(updateDelays.MissionBeaconOffset)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateModelRadius, Utils.SecondsToMilliseconds(updateDelays.ModelRadius)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateHealthPending, Utils.SecondsToMilliseconds(updateDelays.Health)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateStaminaPending, Utils.SecondsToMilliseconds(updateDelays.Stamina)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateRagePending, Utils.SecondsToMilliseconds(updateDelays.Rage)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateMapPinPending, Utils.SecondsToMilliseconds(updateDelays.MapPin)));
	}

	private void SetUpdateNamePending()
	{
		this._isUpdateNamePending = true;
	}

	private void SetUpdateMissionBeaconOffset()
	{
		this._isUpdateMissionBeaconOffsetPending = true;
	}

	private void SetUpdateModelRadius()
	{
		this._isUpdateModelRadiusPending = true;
	}

	private void SetUpdateHealthPending()
	{
		this._isUpdateHealthPending = true;
	}

	private void SetUpdateStaminaPending()
	{
		this._isUpdateStaminaPending = true;
	}

	private void SetUpdateRagePending()
	{
		this._isUpdateRagePending = true;
	}

	private void SetUpdateMapPinPending()
	{
		this._isUpdateMapPinPending = true;
	}

	private void UpdatePosition()
	{
		try
		{
			var position = this.EnemyCharacter.Pos;

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
		this.Distance = Vector3.Distance(this.Position, PlayerManager.Instance.Position);
	}

	private void UpdateIds()
	{
		try
		{
			var basicModule = this.EnemyContext.Basic;

			if(basicModule is null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy basic module");

				return;
			}

			this.Id = basicModule.EmID;
			this.RoleId = basicModule.RoleID;
			this.LegendaryId = basicModule.LegendaryID;
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
			if(!this._isUpdateNamePending)
			{
				return;
			}

			this._isUpdateNamePending = false;

			var name = (string?) this._nameStringMethod?.InvokeBoxed(this._stringType, null, [this.Id, this.RoleId, this.LegendaryId]);

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
			if(!this._isUpdateMissionBeaconOffsetPending)
			{
				return;
			}

			this._isUpdateMissionBeaconOffsetPending = false;

			var missionBeaconOffset = this.EnemyContext.MissionBeaconOffset;

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
			if(!this._isUpdateModelRadiusPending)
			{
				return;
			}

			this._isUpdateModelRadiusPending = false;

			this.ModelRadius = this.EnemyContext.ModelRadius;
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
			if(!this._isUpdateHealthPending)
			{
				return;
			}

			this._isUpdateHealthPending = false;

			var healthManager = this.EnemyCharacter.HealthMgr;

			if(healthManager is null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health manager");

				return;
			}

			this.Health = healthManager.Health;
			this.MaxHealth = healthManager.MaxHealth;

			if(!Utils.IsApproximatelyEqual(this.MaxHealth, 0f))
			{
				this.HealthPercentage = this.Health / this.MaxHealth;
			}

			this.IsAlive = !Utils.IsApproximatelyEqual(this.Health, 0f);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private cEmModuleConditions? UpdateStamina()
	{
		try
		{
			if(!this._isUpdateStaminaPending)
			{
				return null;
			}

			this._isUpdateStaminaPending = false;

			var conditionsModule = this.EnemyContext.Conditions;

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

			this.IsStaminaValid = tiredCondition.IsValid;

			if(!this.IsStaminaValid)
			{
				return conditionsModule;
			}

			this.IsTired = tiredCondition.IsActive;

			if(this.IsTired)
			{
				this.StaminaTimerSeconds = tiredCondition.CurrentTimer;
				this.StaminaMaxTimerSeconds = tiredCondition.ActivateTime;

				this.StaminaRemainingTimerSeconds = this.StaminaMaxTimerSeconds - this.StaminaTimerSeconds;

				if(!Utils.IsApproximatelyEqual(this.StaminaMaxTimerSeconds, 0))
				{
					this.StaminaRemainingTimerPercentage = this.StaminaRemainingTimerSeconds / this.StaminaMaxTimerSeconds;
				}

				this.StaminaRemainingTimerString = Utils.FormatTimer(this.StaminaRemainingTimerSeconds, this.StaminaMaxTimerSeconds);

				return conditionsModule;
			}

			this.Stamina = tiredCondition.Stamina;
			this.MaxStamina = tiredCondition.DefaultStamina;

			if(!Utils.IsApproximatelyEqual(this.MaxStamina, 0))
			{
				this.StaminaPercentage = this.Stamina / this.MaxStamina;
			}

			return conditionsModule;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);

			return null;
		}
	}

	private void UpdateRage(cEmModuleConditions? conditionsModule)
	{
		try
		{
			if(!this._isUpdateRagePending)
			{
				return;
			}

			this._isUpdateRagePending = false;

			if(conditionsModule is null)
			{
				conditionsModule = this.EnemyContext.Conditions;

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

			this.IsRageValid = angryCondition.IsValid;

			if(!this.IsRageValid)
			{
				return;
			}

			this.IsEnraged = angryCondition.IsActive;

			if(this.IsEnraged)
			{
				this.RageTimerSeconds = angryCondition.CurrentTimer;
				this.RageMaxTimerSeconds = angryCondition.ActivateTime;

				this.RageRemainingTimerSeconds = this.RageMaxTimerSeconds - this.RageTimerSeconds;

				if(!Utils.IsApproximatelyEqual(this.RageMaxTimerSeconds, 0))
				{
					this.RageRemainingTimerPercentage = this.RageRemainingTimerSeconds / this.RageMaxTimerSeconds;
				}

				this.RageRemainingTimerString = Utils.FormatTimer(this.RageRemainingTimerSeconds, this.RageMaxTimerSeconds);

				return;
			}

			this.Rage = angryCondition.Value;
			this.MaxRage = angryCondition.LimitValue;

			if(!Utils.IsApproximatelyEqual(this.MaxRage, 0))
			{
				this.RagePercentage = this.Rage / this.MaxRage;
			}
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
			if(!this._isUpdateMapPinPending)
			{
				return;
			}

			this._isUpdateMapPinPending = false;

			this.IsPinned = this.EnemyContext.IsTargetPinOn;
			this.UpdateSortingPriorities();
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

			this._nameStringMethod = enemyDefTypeDef.GetMethod("NameString");

			this._stringType = this._nameStringMethod.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnAnyConfigChanged(object? sender, EventArgs e)
	{
		this.InitializeTimers();
	}
}