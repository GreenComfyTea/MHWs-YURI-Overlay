using System.Numerics;
using System.Runtime.CompilerServices;

using REFrameworkNET;



namespace YURI_Overlay;

internal sealed class LargeMonster
{
	public app.EnemyCharacter EnemyCharacter;
	public app.cEnemyContext EnemyContext;

	public string Name = "Large Monster";
	public int Id = -1;
	public int RoleId = -1;
	public int LegendaryId = -1;


	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius = 0f;

	public Vector3 Position = Vector3.Zero;
	public float Distance = 0f;

	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;
	public bool IsAlive = false;

	public bool IsStaminaValid = false;
	public bool IsTired = false;
	public float Stamina = -1;
	public float MaxStamina = -1;
	public float StaminaPercentage = -1;
	public float StaminaTimerSeconds = -1;
	public float StaminaMaxTimerSeconds = -1;
	public float StaminaRemainingTimerSeconds = -1;
	public float StaminaRemainingTimerPercentage = -1;
	public string StaminaRemainingTimerString = "0:00";

	public bool IsRageValid = false;
	public bool IsEnraged = false;
	public float Rage = -1;
	public float MaxRage = -1;
	public float RagePercentage = -1;
	public float RageTimerSeconds = -1;
	public float RageMaxTimerSeconds = -1;
	public float RageRemainingTimerSeconds = -1;
	public float RageRemainingTimerPercentage = -1;
	public string RageRemainingTimerString = "0:00";

	public LargeMonsterDynamicUi DynamicUi;
	public LargeMonsterStaticUi StaticUi;


	private bool _isUpdateNamePending = true;
	private bool _isUpdateMissionBeaconOffsetPending = true;
	private bool _isUpdateModelRadiusPending = true;
	private bool _isUpdateHealthPending = true;
	private bool _isUpdateStaminaPending = true;
	private bool _isUpdateRagePending = true;

	private Type String_Type;

	private Field EmID_Field;
	private Field RoleID_Field;
	private Field LegendaryID_Field;

	private Method NameString_Method;

	public LargeMonster(app.EnemyCharacter enemyCharacter, app.cEnemyContext enemyContext)
	{
		EnemyCharacter = enemyCharacter;
		EnemyContext = enemyContext;

		try
		{
			InitializeTdb();
			Initialize();

			DynamicUi = new LargeMonsterDynamicUi(this);
			StaticUi = new LargeMonsterStaticUi(this);

			Timers.SetInterval(SetUpdateNamePending, 1000);
			Timers.SetInterval(SetUpdateMissionBeaconOffset, 1000);
			Timers.SetInterval(SetUpdateModelRadius, 1000);
			Timers.SetInterval(SetUpdateHealthPending, 100);
			Timers.SetInterval(SetUpdateStaminaPending, 250);
			Timers.SetInterval(SetUpdateRagePending, 250);

			LogManager.Info($"[LargeMonster] Initialized {Name}");
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
			UpdatePosition();
			UpdateDistance();

			UpdateName();
			UpdateMissionBeaconOffset();
			UpdateModelRadius();

			UpdateHealth();
			var conditionsModule = UpdateStamina();
			UpdateRage(conditionsModule);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void Initialize()
	{
		try
		{
			UpdateIds();
			Update();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
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

	private void UpdatePosition()
	{
		try
		{
			var position = EnemyCharacter.Pos;
			if(position == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos");
				return;
			}

			Position.X = position.x;
			Position.Y = position.y;
			Position.Z = position.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void UpdateDistance()
	{
		Distance = ScreenManager.Instance.GetWorldPositionToCameraDistance(Position);
	}

	private unsafe void UpdateIds()
	{
		try
		{
			var basicModule = EnemyContext.Basic;
			if(basicModule == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy basic module");
				return;
			}

			var ptr = Unsafe.AsPointer(ref basicModule);
			var basicPointer = (ulong) ptr;

			// isValueType = false is intentional, otherwise, value is wrong
			var EmID = (int?) EmID_Field.GetDataBoxed(basicPointer, false);
			if(EmID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy Id");
				return;
			}

			// isValueType = false is intentional, otherwise, value is wrong
			var RoleID = (int?) RoleID_Field.GetDataBoxed(basicPointer, false);
			if(RoleID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy role Id");
				return;
			}

			// isValueType = false is intentional, otherwise, value is wrong
			var LegendaryID = (int?) LegendaryID_Field.GetDataBoxed(basicPointer, false);
			if(LegendaryID == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy legendary Id");
				return;
			}

			Id = (int) EmID;
			RoleId = (int) RoleID;
			LegendaryId = (int) LegendaryID;
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

			var name = (string) NameString_Method.InvokeBoxed(String_Type, null, [Id, RoleId, LegendaryId]);
			if(name == null)
			{
				LogManager.Warn("[LargeMonster.UpdateName] No enemy name");
				return;
			}

			Name = name;
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
			if(missionBeaconOffset == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");
				return;
			}

			MissionBeaconOffset.X = missionBeaconOffset.x;
			MissionBeaconOffset.Y = missionBeaconOffset.y;
			MissionBeaconOffset.Z = missionBeaconOffset.z;
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
			if(healthManager == null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health manager");
				return;
			}

			Health = healthManager.Health;
			MaxHealth = healthManager.MaxHealth;
			HealthPercentage = healthManager.HealthNormalized;

			IsAlive = !Utils.IsApproximatelyEqual(Health, 0f);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private app.cEmModuleConditions UpdateStamina()
	{
		try
		{
			if(!_isUpdateStaminaPending) return null;
			_isUpdateStaminaPending = false;

			var conditionsModule = EnemyContext.Conditions;
			if(conditionsModule == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions module");
				return null;
			}

			var tiredCondition = conditionsModule.Tired;
			if(tiredCondition == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy tired condition");
				return conditionsModule;
			}

			IsRageValid = tiredCondition.IsValid;
			if(!IsRageValid) return conditionsModule;

			IsTired = tiredCondition.IsActive;

			if(IsTired)
			{
				StaminaTimerSeconds = tiredCondition.CurrentTimer;
				StaminaMaxTimerSeconds = tiredCondition.ActivateTime;

				StaminaRemainingTimerSeconds = StaminaMaxTimerSeconds - StaminaTimerSeconds;

				if(!Utils.IsApproximatelyEqual(StaminaMaxTimerSeconds, 0))
				{
					StaminaRemainingTimerPercentage = StaminaRemainingTimerSeconds / StaminaMaxTimerSeconds;
				}

				StaminaRemainingTimerString = Utils.FormatTimer(StaminaTimerSeconds, StaminaMaxTimerSeconds);

				return conditionsModule;
			}

			Stamina = tiredCondition.Stamina;
			MaxStamina = tiredCondition.DefaultStamina;

			if(!Utils.IsApproximatelyEqual(MaxStamina, 0))
			{
				StaminaPercentage = Stamina / MaxStamina;
			}

			return conditionsModule;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return null;
		}
	}

	private void UpdateRage(app.cEmModuleConditions conditionsModule)
	{
		try
		{
			if(!_isUpdateRagePending) return;
			_isUpdateRagePending = false;

			if(conditionsModule == null)
			{
				conditionsModule = EnemyContext.Conditions;
				if(conditionsModule == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions module");
					return;
				}
			}

			var angryCondition = conditionsModule.Angry;
			if(angryCondition == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry condition");
				return;
			}

			IsStaminaValid = angryCondition.IsValid;
			if(!IsStaminaValid) return;

			IsEnraged = angryCondition.IsActive;

			if(IsEnraged)
			{
				RageTimerSeconds = angryCondition.CurrentTimer;
				RageMaxTimerSeconds = angryCondition.ActivateTime;

				RageRemainingTimerSeconds = RageMaxTimerSeconds - RageTimerSeconds;

				if(!Utils.IsApproximatelyEqual(RageMaxTimerSeconds, 0))
				{
					RageRemainingTimerPercentage = RageRemainingTimerSeconds / RageMaxTimerSeconds;
				}

				RageRemainingTimerString = Utils.FormatTimer(RageTimerSeconds, RageMaxTimerSeconds);

				return;
			}

			Rage = angryCondition.Value;
			MaxRage = angryCondition.LimitValue;

			if(!Utils.IsApproximatelyEqual(MaxRage, 0))
			{
				RagePercentage = Rage / MaxRage;
			}
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
			var cEmModuleBasic_TypeDef = app.cEmModuleBasic.REFType;

			EmID_Field = cEmModuleBasic_TypeDef.GetField("EmID");
			RoleID_Field = cEmModuleBasic_TypeDef.GetField("RoleID");
			LegendaryID_Field = cEmModuleBasic_TypeDef.GetField("LegendaryID");

			var EnemyDef_TypeDef = app.EnemyDef.REFType;

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
