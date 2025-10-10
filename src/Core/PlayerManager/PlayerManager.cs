using System.Numerics;
using app;
using REFrameworkNET;
using REFrameworkNET.Attributes;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class PlayerManager : IDisposable
{
	private static readonly Lazy<PlayerManager> Lazy = new(() => new PlayerManager());
	public static PlayerManager Instance => Lazy.Value;

	public Vector3 Position = Vector3.Zero;

	public EventHandler MasterPlayerChanged = delegate { };

	public cPlayerManageInfo? MasterPlayer;

	private readonly List<Timer> _timers = [];

	private HunterCharacter? _masterPlayerCharacter;

	private bool _isUpdatePending = true;


	private PlayerManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[PlayerManager] Initializing...");

		GameUpdate();
		InitializeTimers();

		ConfigManager.Instance.AnyConfigChanged += OnAnyConfigChanged;

		LogManager.Info("[PlayerManager] Initialized!");
	}

	public void Dispose()
	{
		LogManager.Info("[PlayerManager] Disposing...");

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		LogManager.Info("[PlayerManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig?.Data?.GlobalSettings.Performance.UpdateDelays.PlayerManager;

		foreach(var timer in _timers)
		{
			timer.Dispose();
		}

		_timers.Clear();

		_timers.Add(Timers.SetInterval(SetIsUpdatePending, Utils.SecondsToMilliseconds(updateDelays?.Update)));
	}

	private void SetIsUpdatePending()
	{
		_isUpdatePending = true;
	}

	private void GameUpdate()
	{
		try
		{
			Update();

			if(_masterPlayerCharacter is null)
			{
				//LogManager.Warn("[PlayerManager.GameUpdate] No master player character");
				return;
			}

			var playerPosition = _masterPlayerCharacter.Pos;
			if(playerPosition is null)
			{
				LogManager.Warn("[PlayerManager.GameUpdate] No master player position");
				return;
			}

			Position.X = playerPosition.x;
			Position.Y = playerPosition.y;
			Position.Z = playerPosition.z;
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
			if(!_isUpdatePending) return;
			_isUpdatePending = false;

			var playerManager = API.GetManagedSingletonT<app.PlayerManager>();
			if(playerManager is null)
			{
				LogManager.Warn("[PlayerManager.Update] No player manager");

				MasterPlayer = null;
				_masterPlayerCharacter = null;
				EmitMasterPlayerChanged();

				return;
			}

			var masterPlayer = playerManager.getMasterPlayer();
			if(masterPlayer is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player");

				MasterPlayer = null;
				_masterPlayerCharacter = null;
				EmitMasterPlayerChanged();

				return;
			}

			MasterPlayer = masterPlayer;

			var masterPlayerCharacter = masterPlayer.Character;
			if(masterPlayerCharacter is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player character");

				MasterPlayer = null;
				_masterPlayerCharacter = null;
				EmitMasterPlayerChanged();

				return;
			}

			_masterPlayerCharacter = masterPlayerCharacter;
			EmitMasterPlayerChanged();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnAnyConfigChanged(object? sender, EventArgs e)
	{
		InitializeTimers();
	}

	private void EmitMasterPlayerChanged()
	{
		Utils.EmitEvents(this, MasterPlayerChanged);
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