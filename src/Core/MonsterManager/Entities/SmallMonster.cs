using REFrameworkNET;
using System.Numerics;

namespace YURI_Overlay;

internal sealed class SmallMonster : IDisposable
{
	public app.EnemyCharacter EnemyCharacter;
	public app.cEnemyContext EnemyContext;
	public ManagedObject EnemyCharacterManagedObject;

	public string Name = "Small Monster";
	public app.EnemyDef.ID Id = 0;
	public app.EnemyDef.ROLE_ID RoleId = 0;
	public app.EnemyDef.LEGENDARY_ID LegendaryId = 0;

	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius = 0f;

	public Vector3 Position = Vector3.Zero;
	public float Distance = 0f;

	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;
	public bool IsAlive = false;

	public SmallMonsterDynamicUi DynamicUi;

	private bool _isUpdateNamePending = true;
	private bool _isUpdateMissionBeaconOffsetPending = true;
	private bool _isUpdateModelRadiusPending = true;
	private bool _isUpdateHealthPending = true;

	private readonly List<System.Timers.Timer> _timers = [];

	private Type String_Type;


	private Method NameString_Method;

	public SmallMonster(app.EnemyCharacter enemyCharacter, app.cEnemyContext enemyContext, ManagedObject enemyCharacterManagedObject)
	{
		EnemyCharacter = enemyCharacter;
		EnemyContext = enemyContext;
		EnemyCharacterManagedObject = enemyCharacterManagedObject;

		try
		{
			InitializeTdb();
			Initialize();

			DynamicUi = new SmallMonsterDynamicUi(this);

			ConfigManager.Instance.AnyConfigChanged += OnAnyConfigChanged;

			LogManager.Info($"[SmallMonster] Initialized {Name}!");
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
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[SmallMonster] Disposing {Name}...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		LogManager.Info($"[SmallMonster] {Name} Disposed!");
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
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.SmallMonsters;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(SetUpdateNamePending, Utils.SecondsToMilliseconds(updateDelays.Name)));
		_timers.Add(Timers.SetInterval(SetUpdateMissionBeaconOffset, Utils.SecondsToMilliseconds(updateDelays.MissionBeaconOffset)));
		_timers.Add(Timers.SetInterval(SetUpdateModelRadius, Utils.SecondsToMilliseconds(updateDelays.ModelRadius)));
		_timers.Add(Timers.SetInterval(SetUpdateHealthPending, Utils.SecondsToMilliseconds(updateDelays.Health)));
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

	private void UpdatePosition()
	{
		try
		{
			var position = EnemyCharacter.Pos;
			if(position is null)
			{
				LogManager.Warn("[SmallMonster.UpdatePositionAndDistance] No enemy pos");
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
		Distance = Vector3.Distance(Position, PlayerManager.Instance.Position);
	}

	private unsafe void UpdateIds()
	{
		try
		{
			var basicModule = EnemyContext.Basic;
			if(basicModule is null)
			{
				LogManager.Warn("[SmallMonster.UpdateIds] No enemy basic module");
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

			var name = (string) NameString_Method.InvokeBoxed(String_Type, null, [Id, RoleId, LegendaryId]);
			if(name is null)
			{
				LogManager.Warn("[SmallMonster.UpdateName] No enemy name");
				return;
			}

			Name = name;
			// Name = "Nerscylla Hatchling";
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
				LogManager.Warn("[SmallMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");
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
			if(healthManager is null)
			{
				LogManager.Warn("[SmallMonster.UpdateHealth] No health manager");
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
	private void InitializeTdb()
	{
		try
		{
			var EnemyDef_TypeDef = app.EnemyDef.REFType;

			NameString_Method = EnemyDef_TypeDef.GetMethod("NameString");

			String_Type = NameString_Method.ReturnType.GetType();
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
