using System.Numerics;

using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class PlayerManager
{
	private static readonly Lazy<PlayerManager> Lazy = new(() => new PlayerManager());
	public static PlayerManager Instance => Lazy.Value;

	public Vector3 Position = Vector3.Zero;

	private app.HunterCharacter _masterPlayerCharacter;

	private bool _isTimeoutElapsed = false;

	private PlayerManager() { }

	public void Initialize()
	{
		LogManager.Info("[PlayerManager] Initializing...");

		// Doing stuff with the player manager right after the game starts leads to a crash
		// So we wait for a bit, 5 minutes should be enough to assume player manager is safe
		// It's a temporary solution, we should find a better way to handle this, detect if save is loaded, perhaps
		Timers.SetTimeout(() =>
		{
			_isTimeoutElapsed = true;
			Timers.SetInterval(Update, 1000);
		}, 5 * 60 * 1000);

		LogManager.Info("[PlayerManager] Initialized!");
	}

	public void FrameUpdate()
	{
		if(!_isTimeoutElapsed)
		{
			var cameraPosition = ScreenManager.Instance.CameraPosition;

			Position.X = cameraPosition.X;
			Position.Y = cameraPosition.Y;
			Position.Z = cameraPosition.Z;

			return;
		}

		if(_masterPlayerCharacter == null)
		{
			//LogManager.Warn("[PlayerManager.FrameUpdate] No master player character");
			return;
		}

		var position = _masterPlayerCharacter.Pos;
		if(position == null)
		{
			LogManager.Warn("[PlayerManager.FrameUpdate] No master player position");
			return;
		}

		Position.X = position.x;
		Position.Y = position.y;
		Position.Z = position.z;
	}

	private void Update()
	{
		var playerManager = API.GetManagedSingletonT<app.PlayerManager>();
		if(playerManager == null)
		{
			LogManager.Warn("[PlayerManager.Update] No player manager");
			return;
		}

		var masterPlayer = playerManager.getMasterPlayer();
		if(masterPlayer == null)
		{
			LogManager.Warn("[PlayerManager.Update] No master player");
			return;
		}

		var masterPlayerCharacter = masterPlayer.Character;
		if(masterPlayerCharacter == null)
		{
			LogManager.Warn("[PlayerManager.Update] No master player character");
			return;
		}

		_masterPlayerCharacter = masterPlayerCharacter;
	}
}
