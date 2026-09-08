using System.Numerics;
using app;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class PlayerManager : IDisposable
{
	private static readonly Lazy<PlayerManager> _lazy = new(() => new PlayerManager());

	private readonly List<Timer> _timers = [];

	private bool _isUpdatePending = true;

	private HunterCharacter? _masterPlayerCharacter;

	public cPlayerManageInfo? MasterPlayer;

	public Vector3 Position = Vector3.Zero;

	private PlayerManager()
	{
	}

	public static PlayerManager Instance => _lazy.Value;

	public void Dispose()
	{
		LogManager.Info("[PlayerManager] Disposing...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info("[PlayerManager] Disposed!");
	}

	public event EventHandler MasterPlayerChanged = delegate { };

	public void Initialize()
	{
		LogManager.Info("[PlayerManager] Initializing...");

		this.GameUpdate();
		this.InitializeTimers();

		ConfigManager.Instance.AnyConfigChanged += this.OnAnyConfigChanged;

		LogManager.Info("[PlayerManager] Initialized!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.PlayerManager;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.SetIsUpdatePending, Utils.SecondsToMilliseconds(updateDelays.Update)));
	}

	private void SetIsUpdatePending()
	{
		this._isUpdatePending = true;
	}

	private void GameUpdate()
	{
		try
		{
			this.Update();

			if(this._masterPlayerCharacter is null)
				//LogManager.Warn("[PlayerManager.GameUpdate] No master player character");
				return;

			var playerPosition = this._masterPlayerCharacter.Pos;

			if(playerPosition is null)
			{
				LogManager.Warn("[PlayerManager.GameUpdate] No master player position");

				return;
			}

			this.Position.X = playerPosition.x;
			this.Position.Y = playerPosition.y;
			this.Position.Z = playerPosition.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void Update()
	{
		try
		{
			if(!this._isUpdatePending) return;

			this._isUpdatePending = false;

			var playerManager = API.GetManagedSingletonT<app.PlayerManager>();

			if(playerManager is null)
			{
				LogManager.Warn("[PlayerManager.Update] No player manager");

				this.MasterPlayer = null;
				this._masterPlayerCharacter = null;
				this.EmitMasterPlayerChanged();

				return;
			}

			var masterPlayer = playerManager.getMasterPlayer();

			if(masterPlayer is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player");

				this.MasterPlayer = null;
				this._masterPlayerCharacter = null;
				this.EmitMasterPlayerChanged();

				return;
			}

			this.MasterPlayer = masterPlayer;

			var masterPlayerCharacter = masterPlayer.Character;

			if(masterPlayerCharacter is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player character");

				this.MasterPlayer = null;
				this._masterPlayerCharacter = null;
				this.EmitMasterPlayerChanged();

				return;
			}

			this._masterPlayerCharacter = masterPlayerCharacter;
			this.EmitMasterPlayerChanged();
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

	private void EmitMasterPlayerChanged()
	{
		Utils.EmitEvents(this, this.MasterPlayerChanged);
	}

	[MethodHook(typeof(app.PlayerManager), nameof(app.PlayerManager.update), MethodHookType.Post)]
	private static void OnPostUpdate(ref ulong returnValue)
	{
		Instance.GameUpdate();
	}

	// When returning to title screen, the cached master player character becomes invalid, so we have to discard it
	[MethodHook(typeof(app.PlayerManager), nameof(app.PlayerManager.unregisterPlayer), MethodHookType.Post)]
	private static void OnPostUnregisterPlayer(ref ulong returnValue)
	{
		Instance._masterPlayerCharacter = null;
		Instance._isUpdatePending = true;
		Instance.EmitMasterPlayerChanged();
	}
}