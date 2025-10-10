using app;
using REFrameworkNET.Attributes;
using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class CameraManager
{
	private static readonly Lazy<CameraManager> Lazy = new(() => new CameraManager());

	public static CameraManager Instance => Lazy.Value;

	public LargeMonster? TargetedLargeMonster = null;

	public CameraManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[CameraManager] Initializing...");

		UpdateCameraTarget();

		LogManager.Info("[CameraManager] Initialized!");
	}

	private void UpdateCameraTarget()
	{
		try
		{
			var customization = ConfigManager.Instance.ActiveConfig?.Data;

			if(customization is null) return;

			if(customization.LargeMonsterUI.Enabled != true
			   && customization.LargeMonsterUI.Dynamic.Enabled != true
			   && customization.LargeMonsterUI.Static.Enabled != true
			   && customization.LargeMonsterUI.Targeted.Enabled != true
			   && customization.LargeMonsterUI.MapPin.Enabled != true)
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
				if(TargetedLargeMonster is not null)
				{
					TargetedLargeMonster.SetIsTargeted(false);
					TargetedLargeMonster = null;
				}

				return;
			}

			var largeMonster = MonsterManager.Instance.LargeMonsters.FirstOrDefault(
				iteratedLargeMonster => iteratedLargeMonster.Value.EnemyCharacter == lockTarget._Character
			);

			if(largeMonster.Value is null)
			{
				LogManager.Warn("[CameraManager.UpdateCameraTarget] No large monster");
				return;
			}

			TargetedLargeMonster = largeMonster.Value;
			TargetedLargeMonster.SetIsTargeted(true);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	[MethodHook(typeof(PlayerCameraController), "<updateTargetSelector>b__140_0", MethodHookType.Post)]
	private static void OnPostUpdateTargetSelector(ref ulong returnValue)
	{
		Instance.UpdateCameraTarget();
	}
}