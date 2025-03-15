using System.Numerics;

using REFrameworkNET;

using ValueType = REFrameworkNET.ValueType;

namespace YURI_Overlay;

internal sealed class LargeMonster
{
	public ManagedObject EnemyCharacter;
	public ManagedObject _Em;

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
	private Type chealthManager_Type;
	private Type Single_Type;
	private Type cEnemyModuleConditions_Type;
	private Type cEnemyTiredCondition_Type;
	private Type Boolean_Type;

	private Method NameString_Method;

	private Method get_HealthMgr_Method;
	private Method get_Health_Method;
	private Method get_MaxHealth_Method;
	private Method get_Stamina_Method;
	private Method get_DefaultStamina_Method;
	private Method get_IsValid_Method;
	private Method get_IsActive_Method;
	private Method get_Value_Method;
	private Method get_LimitValue_Method;
	private Method get_ActivateTime_Method;
	private Method get_CurrentTimer_Method;

	private Field _Context_Field;
	private Field _Em_Field;
	private Field Basic_Field;
	private Field EmID_Field;
	private Field RoleID_Field;
	private Field LegendaryID_Field;
	private Field ModelCenterPos_Field;
	private Field x_Field;
	private Field y_Field;
	private Field z_Field;
	private Field ModelRadius_Field;
	private Field Conditions_Field;
	private Field Tired_Field;
	private Field Angry_Field;

	public LargeMonster(ManagedObject enemyCharacter)
	{
		EnemyCharacter = enemyCharacter;

		try
		{
			InitializeTdb();
			Initialize();
			UpdateHealth();

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
			var Conditions = UpdateStamina();
			UpdateRage(Conditions);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void Initialize()
	{
		try
		{
			var _Context = (ManagedObject) _Context_Field.GetDataBoxed((ulong) EnemyCharacter.Ptr(), false);
			if(_Context == null)
			{
				LogManager.Warn("[LargeMonster.Initialize] No enemy context holder");
				return;
			}

			_Em = (ManagedObject) _Em_Field.GetDataBoxed((ulong) _Context.Ptr(), false);
			if(_Em == null)
			{
				LogManager.Warn("[LargeMonster.Initialize] No enemy context");
				return;
			}

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

	private unsafe void UpdatePosition()
	{
		try
		{
			// Can't cache for some reason :(
			var position = (ValueType) EnemyCharacter.Call("get_Pos");
			//var pos = (ValueType) ModelCenterPos_Field.GetDataBoxed(vec3_Type, (ulong) _Em.Ptr(), true);
			if(position == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos");
				return;
			}

			var positionPointer = (ulong) position.Ptr();

			var x = (float?) x_Field.GetDataBoxed(positionPointer, true);
			if(x == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos x");
				return;
			}

			var y = (float?) y_Field.GetDataBoxed(positionPointer, true);
			if(y == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos y");
				return;
			}

			var z = (float?) z_Field.GetDataBoxed(positionPointer, true);
			if(z == null)
			{
				LogManager.Warn("[LargeMonster.UpdatePositionAndDistance] No enemy pos z");
				return;
			}

			Position.X = (float) x;
			Position.Y = (float) y;
			Position.Z = (float) z;
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
			var Basic = (ManagedObject) Basic_Field.GetDataBoxed((ulong) _Em.Ptr(), false);
			if(Basic == null)
			{
				LogManager.Warn("[LargeMonster.UpdateIds] No enemy basic module");
				return;
			}

			var basicPointer = (ulong) Basic.Ptr();

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
			//Name = "Tempered Guardian Fulgur Anjanath";
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void UpdateMissionBeaconOffset()
	{
		try
		{
			if(!_isUpdateMissionBeaconOffsetPending) return;
			_isUpdateMissionBeaconOffsetPending = false;

			// Can't cache for some reason :(
			var _MissionBeaconOffset = (ValueType) _Em.GetField("MissionBeaconOffset");
			//var pos = (ValueType) ModelCenterPos_Field.GetDataBoxed(vec3_Type, (ulong) _Em.Ptr(), true);
			if(_MissionBeaconOffset == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");
				return;
			}

			var MissionBeaconOffset_Ptr = (ulong) _MissionBeaconOffset.Ptr();

			var x = (float?) x_Field.GetDataBoxed(Single_Type, MissionBeaconOffset_Ptr, true);
			if(x == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset x");
				return;
			}

			var y = (float?) y_Field.GetDataBoxed(Single_Type, MissionBeaconOffset_Ptr, true);
			if(y == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset y");
				return;
			}

			var z = (float?) z_Field.GetDataBoxed(Single_Type, MissionBeaconOffset_Ptr, true);
			if(z == null)
			{
				LogManager.Warn("[LargeMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset z");
				return;
			}

			MissionBeaconOffset.X = (float) x;
			MissionBeaconOffset.Y = (float) y;
			MissionBeaconOffset.Z = (float) z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void UpdateModelRadius()
	{
		try
		{
			if(!_isUpdateModelRadiusPending) return;
			_isUpdateModelRadiusPending = false;

			// isValueType = false is intentional, otherwise, value is wrong
			var _ModelRadius = (float?) ModelRadius_Field.GetDataBoxed(Single_Type, (ulong) _Em.Ptr(), false);
			if(_ModelRadius == null)
			{
				LogManager.Warn("[LargeMonster.UpdateModelRadius] No enemy model radius");
				return;
			}

			ModelRadius = (float) _ModelRadius;
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

			var healthManager = (ManagedObject) get_HealthMgr_Method.InvokeBoxed(chealthManager_Type, EnemyCharacter, []);
			if(healthManager == null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health manager");
				return;
			}

			var health = (float?) get_Health_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(health == null)
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No health");
				return;
			}

			var maxHealth = (float?) get_MaxHealth_Method.InvokeBoxed(Single_Type, healthManager, []);
			if(maxHealth == null || Utils.IsApproximatelyEqual((float) maxHealth, 0f))
			{
				LogManager.Warn("[LargeMonster.UpdateHealth] No max health");
				return;
			}

			Health = (float) health;
			MaxHealth = (float) maxHealth;
			HealthPercentage = (float) (health / maxHealth);

			IsAlive = !Utils.IsApproximatelyEqual((float) health, 0f);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe ManagedObject UpdateStamina()
	{
		try
		{
			if(!_isUpdateStaminaPending) return null;
			_isUpdateStaminaPending = false;

			var Conditions = (ManagedObject) Conditions_Field.GetDataBoxed(cEnemyModuleConditions_Type, (ulong) _Em.Ptr(), false);
			if(Conditions == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions");
				return null;
			}

			var Tired = (ManagedObject) Tired_Field.GetDataBoxed(cEnemyTiredCondition_Type, (ulong) Conditions.Ptr(), false);
			if(Tired == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy tired condition");
				return Conditions;
			}

			var isValid = (bool?) get_IsValid_Method.InvokeBoxed(Boolean_Type, Tired, []);
			if(isValid == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry is valid");
				return Conditions;
			}

			IsRageValid = (bool) isValid;
			if(!IsRageValid) return Conditions;

			var isActive = (bool?) get_IsActive_Method.InvokeBoxed(Boolean_Type, Tired, []);
			if(isActive == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy tired is active");
				return Conditions;
			}

			IsTired = (bool) isActive;

			if(IsTired)
			{
				var currentTimer = (float?) get_CurrentTimer_Method.InvokeBoxed(Single_Type, Tired, []);
				if(currentTimer == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy current timer");
					return Conditions;
				}

				var activateTime = (float?) get_ActivateTime_Method.InvokeBoxed(Single_Type, Tired, []);
				if(activateTime == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy activate time");
					return Conditions;
				}

				StaminaTimerSeconds = (float) currentTimer;
				StaminaMaxTimerSeconds = (float) activateTime;
				StaminaRemainingTimerSeconds = StaminaMaxTimerSeconds - StaminaTimerSeconds;
				StaminaRemainingTimerPercentage = StaminaRemainingTimerSeconds / StaminaMaxTimerSeconds;
				StaminaRemainingTimerString = Utils.FormatTimer(StaminaTimerSeconds, StaminaMaxTimerSeconds);

				return Conditions;
			}

			var stamina = (float?) get_Stamina_Method.InvokeBoxed(Single_Type, Tired, []);
			if(stamina == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy stamina");
				return Conditions;
			}

			var defaultStamina = (float?) get_DefaultStamina_Method.InvokeBoxed(Single_Type, Tired, []);
			if(defaultStamina == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy default stamina");
				return Conditions;
			}

			Stamina = (float) stamina;
			MaxStamina = (float) defaultStamina;
			StaminaPercentage = Stamina / MaxStamina;

			return Conditions;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return null;
		}
	}

	private unsafe void UpdateRage(ManagedObject Conditions)
	{
		try
		{
			if(!_isUpdateRagePending) return;
			_isUpdateRagePending = false;

			if(Conditions == null)
			{
				Conditions = (ManagedObject) Conditions_Field.GetDataBoxed(cEnemyModuleConditions_Type, (ulong) _Em.Ptr(), false);
				if(Conditions == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy conditions");
					return;
				}
			}

			var Angry = (ManagedObject) Angry_Field.GetDataBoxed(cEnemyTiredCondition_Type, (ulong) Conditions.Ptr(), false);
			if(Angry == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry condition");
				return;
			}

			var isValid = (bool?) get_IsValid_Method.InvokeBoxed(Boolean_Type, Angry, []);
			if(isValid == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry is valid");
				return;
			}

			IsStaminaValid = (bool) isValid;
			if(!IsStaminaValid) return;

			var isActive = (bool?) get_IsActive_Method.InvokeBoxed(Boolean_Type, Angry, []);
			if(isActive == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry is active");
				return;
			}

			IsEnraged = (bool) isActive;

			if(IsEnraged)
			{
				var CurrentTimer = (float?) get_CurrentTimer_Method.InvokeBoxed(Single_Type, Angry, []);
				if(CurrentTimer == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry current timer");
					return;
				}

				var ActivateTime = (float?) get_ActivateTime_Method.InvokeBoxed(Single_Type, Angry, []);
				if(ActivateTime == null)
				{
					LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry activate time");
					return;
				}

				RageTimerSeconds = (float) CurrentTimer;
				RageMaxTimerSeconds = (float) ActivateTime;
				RageRemainingTimerSeconds = RageMaxTimerSeconds - RageTimerSeconds;
				RageRemainingTimerPercentage = RageRemainingTimerSeconds / RageMaxTimerSeconds;
				RageRemainingTimerString = Utils.FormatTimer(RageTimerSeconds, RageMaxTimerSeconds);

				return;
			}

			var value = (float?) get_Value_Method.InvokeBoxed(Single_Type, Angry, []);
			if(value == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry value");
				return;
			}

			var limitValue = (float?) get_LimitValue_Method.InvokeBoxed(Single_Type, Angry, []);
			if(limitValue == null)
			{
				LogManager.Warn("[LargeMonster.UpdateStamina] No enemy angry limit value");
				return;
			}

			Rage = (float) value;
			MaxRage = (float) limitValue;
			RagePercentage = Rage / MaxRage;
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
			var EnemyCharacter_TypeDef = TDB.Get().GetType("app.EnemyCharacter");

			get_HealthMgr_Method = EnemyCharacter_TypeDef.GetMethod("get_HealthMgr");
			_Context_Field = EnemyCharacter_TypeDef.GetField("_Context");

			_Em_Field = _Context_Field.GetType().GetField("_Em");

			var enemyContext_TypeDef = _Em_Field.GetType();

			Basic_Field = enemyContext_TypeDef.GetField("Basic");
			ModelRadius_Field = enemyContext_TypeDef.GetField("ModelRadius");

			var cEmModuleBasic_TypeDef = Basic_Field.GetType();

			EmID_Field = cEmModuleBasic_TypeDef.GetField("EmID");
			RoleID_Field = cEmModuleBasic_TypeDef.GetField("RoleID");
			LegendaryID_Field = cEmModuleBasic_TypeDef.GetField("LegendaryID");

			var EnemyDef_TypeDef = TDB.Get().GetType("app.EnemyDef");

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();

			ModelCenterPos_Field = enemyContext_TypeDef.GetField("ModelCenterPos");

			var vec3_TypeDef = ModelCenterPos_Field.GetType();

			x_Field = vec3_TypeDef.GetField("x");
			y_Field = vec3_TypeDef.GetField("y");
			z_Field = vec3_TypeDef.GetField("z");

			var chealthManager_TypeDef = get_HealthMgr_Method.ReturnType;
			chealthManager_Type = chealthManager_TypeDef.GetType();

			get_Health_Method = chealthManager_TypeDef.GetMethod("get_Health");
			get_MaxHealth_Method = chealthManager_TypeDef.GetMethod("get_MaxHealth");

			Single_Type = get_Health_Method.ReturnType.GetType();

			Conditions_Field = enemyContext_TypeDef.GetField("Conditions");

			var cEnemyModuleConditions_TypeDef = Conditions_Field.GetType();
			cEnemyModuleConditions_Type = cEnemyModuleConditions_TypeDef.GetType();

			Tired_Field = cEnemyModuleConditions_TypeDef.GetField("<Tired>k__BackingField");
			Angry_Field = cEnemyModuleConditions_TypeDef.GetField("<Angry>k__BackingField");

			var cEnemyTiredCondition_TypeDef = Tired_Field.GetType();
			cEnemyTiredCondition_Type = cEnemyTiredCondition_TypeDef.GetType();

			get_Stamina_Method = cEnemyTiredCondition_TypeDef.GetMethod("get_Stamina");
			get_DefaultStamina_Method = cEnemyTiredCondition_TypeDef.GetMethod("get_DefaultStamina");

			var cEnemyActivateValueBase_TypeDef = TDB.Get().GetType("app.cEnemyActivateValueBase");


			get_IsValid_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_IsValid");
			get_IsActive_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_IsActive");
			get_Value_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_Value");
			get_LimitValue_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_LimitValue");
			get_ActivateTime_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_ActivateTime");
			get_CurrentTimer_Method = cEnemyActivateValueBase_TypeDef.GetMethod("get_CurrentTimer");

			Boolean_Type = get_IsActive_Method.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
