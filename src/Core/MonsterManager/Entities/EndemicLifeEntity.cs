using System.Numerics;
using app;
using REFrameworkNET;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class EndemicLifeEntity : IDisposable
{
	public EnemyCharacter EnemyCharacter;
	public cEnemyContext EnemyContext;

	public EndemicLifeDynamicUi? DynamicUi;

	public EnemyDef.ID Id = 0;
	public EnemyDef.ROLE_ID RoleId = 0;
	public EnemyDef.LEGENDARY_ID LegendaryId = 0;

	public string Name = "Endemic Life";

	public float ModelRadius;

	public Vector3 Position = Vector3.Zero;
	public float Distance;

	private readonly List<Timer> _timers = [];

	private bool _isUpdateNamePending = true;
	private bool _isUpdateModelRadiusPending = true;

	private Type? _stringType;

	private Method? _nameStringMethod;

	public EndemicLifeEntity(EnemyCharacter enemyCharacter, cEnemyContext enemyContext)
	{
		this.EnemyCharacter = enemyCharacter;
		this.EnemyContext = enemyContext;

		try
		{
			this.InitializeTdb();
			this.Initialize();

			this.DynamicUi = new EndemicLifeDynamicUi(this);

			ConfigManager.Instance.AnyConfigChanged += this.OnAnyConfigChanged;

			LogManager.Info($"[EndemicLife] Initialized {this.Name}!");
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
			this.UpdateModelRadius();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[EndemicLife] Disposing {this.Name}...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info($"[EndemicLife] {this.Name} Disposed!");
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
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.EndemicLife;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.SetUpdateNamePending, Utils.SecondsToMilliseconds(updateDelays.Name)));
		this._timers.Add(Timers.SetInterval(this.SetUpdateModelRadius, Utils.SecondsToMilliseconds(updateDelays.ModelRadius)));
	}

	private void SetUpdateNamePending()
	{
		this._isUpdateNamePending = true;
	}

	private void SetUpdateModelRadius()
	{
		this._isUpdateModelRadiusPending = true;
	}

	private void UpdatePosition()
	{
		try
		{
			var position = this.EnemyCharacter.Pos;

			if(position is null)
			{
				LogManager.Warn("[EndemicLife.UpdatePositionAndDistance] No enemy pos");

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
				LogManager.Warn("[EndemicLife.UpdateIds] No enemy basic module");

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
				LogManager.Warn("[EndemicLife.UpdateName] No enemy name");

				return;
			}

			this.Name = name;
			//Name = "Nerscylla Hatchling";
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