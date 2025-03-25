using System.Numerics;

using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

internal sealed class PlayerManager : IDisposable
{
	private static readonly Lazy<PlayerManager> Lazy = new(() => new PlayerManager());
	public static PlayerManager Instance => Lazy.Value;

	public Vector3 Position = Vector3.Zero;

	private app.HunterCharacter _masterPlayerCharacter;

	private bool _isUpdatePending = true;
	private System.Timers.Timer _updateTimer;

	private PlayerManager() { }

	public void Initialize()
	{
		LogManager.Info("[PlayerManager] Initializing...");

		GameUpdate();

		_updateTimer = Timers.SetInterval(SetIsUpdatePending, 1000);

		LogManager.Info("[PlayerManager] Initialized!");
	}

	public void Dispose()
	{
		LogManager.Info("[PlayerManager] Disposing...");

		_updateTimer.Dispose();

		LogManager.Info("[PlayerManager] Disposed!");
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

			var position = _masterPlayerCharacter.Pos;
			if(position is null)
			{
				LogManager.Warn("[PlayerManager.GameUpdate] No master player position");
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
				return;
			}

			var masterPlayer = playerManager.getMasterPlayer();
			if(masterPlayer is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player");
				return;
			}

			var masterPlayerCharacter = masterPlayer.Character;
			if(masterPlayerCharacter is null)
			{
				//LogManager.Warn("[PlayerManager.Update] No master player character");
				return;
			}

			_masterPlayerCharacter = masterPlayerCharacter;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	[MethodHook(typeof(app.PlayerManager), nameof(app.PlayerManager.update), MethodHookType.Post)]
	private static void OnPostUpdate(ref ulong returnValue)
	{
		Instance.GameUpdate();
	}
}
