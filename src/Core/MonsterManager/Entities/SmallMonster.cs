using System.Numerics;
using app;
using REFrameworkNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class SmallMonster : IDisposable
{
	public EnemyCharacter EnemyCharacter;
	public cEnemyContext EnemyContext;

	public SmallMonsterDynamicUi? DynamicUi;

	public EnemyDef.ID Id = 0;
	public EnemyDef.ROLE_ID RoleId = 0;
	public EnemyDef.LEGENDARY_ID LegendaryId = 0;

	public string Name = "Small Monster";

	public Vector3 MissionBeaconOffset = Vector3.Zero;
	public float ModelRadius;

	public Vector3 Position = Vector3.Zero;
	public float Distance;

	public bool IsAlive = true;
	public float Health = -1;
	public float MaxHealth = -1;
	public float HealthPercentage = -1;

	private bool _isUpdateNamePending = true;
	private bool _isUpdateMissionBeaconOffsetPending = true;
	private bool _isUpdateModelRadiusPending = true;
	private bool _isUpdateHealthPending = true;

	private readonly List<Timer> _timers = [];

	private Type? _stringType;

	private Method? _nameStringMethod;

	public SmallMonster(EnemyCharacter enemyCharacter, cEnemyContext enemyContext)
	{
		this.EnemyCharacter = enemyCharacter;
		this.EnemyContext = enemyContext;

		try
		{
			this.InitializeTdb();
			this.Initialize();

			this.DynamicUi = new SmallMonsterDynamicUi(this);

			ConfigManager.Instance.AnyConfigChanged += this.OnAnyConfigChanged;

			LogManager.Info($"[SmallMonster] Initialized {this.Name}!");
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
			this.UpdatePosition();
			this.UpdateDistance();

			this.UpdateName();
			this.UpdateMissionBeaconOffset();
			this.UpdateModelRadius();

			this.UpdateHealth();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[SmallMonster] Disposing {this.Name}...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info($"[SmallMonster] {this.Name} Disposed!");
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
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.SmallMonsters;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.SetUpdateNamePending, Utils.SecondsToMilliseconds(updateDelays.Name)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateMissionBeaconOffset, Utils.SecondsToMilliseconds(updateDelays.MissionBeaconOffset)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateModelRadius, Utils.SecondsToMilliseconds(updateDelays.ModelRadius)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateHealthPending, Utils.SecondsToMilliseconds(updateDelays.Health)));
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

	private void UpdatePosition()
	{
		try
		{
			var position = this.EnemyCharacter.Pos;

			if(position is null)
			{
				LogManager.Warn("[SmallMonster.UpdatePositionAndDistance] No enemy pos");

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
				LogManager.Warn("[SmallMonster.UpdateIds] No enemy basic module");

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
				LogManager.Warn("[SmallMonster.UpdateName] No enemy name");

				return;
			}

			this.Name = name;
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
			if(!this._isUpdateMissionBeaconOffsetPending)
			{
				return;
			}

			this._isUpdateMissionBeaconOffsetPending = false;

			var missionBeaconOffset = this.EnemyContext.MissionBeaconOffset;

			if(missionBeaconOffset is null)
			{
				LogManager.Warn("[SmallMonster.UpdateMissionBeaconOffset] No enemy mission beacon offset");

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
				LogManager.Warn("[SmallMonster.UpdateHealth] No health manager");

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