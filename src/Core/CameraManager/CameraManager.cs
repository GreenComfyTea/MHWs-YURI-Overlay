using app;
using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class CameraManager
{
	private static readonly Lazy<CameraManager> Lazy = new(() => new CameraManager());

	public static CameraManager Instance => Lazy.Value;

	public LargeMonster? TargetedLargeMonster;

	public void Initialize()
	{
		LogManager.Info("[CameraManager] Initializing...");

		this.UpdateCameraTarget();

		LogManager.Info("[CameraManager] Initialized!");
	}

	private void UpdateCameraTarget()
	{
		try
		{
			var customization = ConfigManager.Instance.ActiveConfig.Data;

			if(
				customization.LargeMonsterUI.Enabled != true
				&& customization.LargeMonsterUI.Dynamic.Enabled != true
				&& customization.LargeMonsterUI.Static.Enabled != true
				&& customization.LargeMonsterUI.Targeted.Enabled != true
				&& customization.LargeMonsterUI.MapPin.Enabled != true
			)
			{
				return;
			}

			var cameraManager = API.GetManagedSingletonT<app.CameraManager>();

			if(cameraManager is null)
			{
				LogManager.Warn("[CameraManager.UpdateCameraTarget] No camera manager");

				return;
			}

			var masterPlayerCamera = cameraManager._MasterPlCamera;

			if(masterPlayerCamera is null)
			{
				//LogManager.Warn("[CameraManager.UpdateCameraTarget] No master player camera");
				return;
			}

			var lockTarget = masterPlayerCamera.LockTarget;

			if(lockTarget is null)
			{
				// No target is selected anymore
				if(this.TargetedLargeMonster is not null)
				{
					this.TargetedLargeMonster.SetIsTargeted(false);
					this.TargetedLargeMonster = null;
				}

				return;
			}

			var largeMonster = MonsterManager.Instance.LargeMonsters.FirstOrDefault(iteratedLargeMonster => iteratedLargeMonster.Value.EnemyCharacter == lockTarget._Character);

			if(largeMonster.Value is null)
			{
				LogManager.Warn("[CameraManager.UpdateCameraTarget] No large monster");

				return;
			}

			this.TargetedLargeMonster = largeMonster.Value;
			this.TargetedLargeMonster.SetIsTargeted(true);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	[MethodHookPattern(typeof(PlayerCameraController), "<updateTargetSelector>", MethodHookType.Post)]
	private static void OnPostUpdateTargetSelector(ref ulong returnValue)
	{
		Instance.UpdateCameraTarget();
	}
}