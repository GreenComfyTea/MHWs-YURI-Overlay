using System.Numerics;

using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class ScreenManager : IDisposable
{
	private static readonly Lazy<ScreenManager> Lazy = new(() => new ScreenManager());
	public static ScreenManager Instance => Lazy.Value;

	public Vector2 WindowSize = new(1920f, 1080f);
	public Vector3 CameraPosition = Vector3.Zero;

	private via.Camera _primaryCamera;
	private Matrix4x4 _viewProjectionMatrix = Matrix4x4.Identity;

	private Vector3 _cameraForward = Vector3.Zero;

	private float _overheadX = 0.25f * 1920f;
	private float _overheadY = 0.25f * 1080f;

	private bool _isUpdatePending = true;
	private System.Timers.Timer _updateTimer;

	private Type SceneView_Type;

	private Method get_MainView_Method;

	private ScreenManager() { }

	public void Initialize()
	{
		LogManager.Info("[ScreenManager] Initializing...");

		InitializeTdb();

		GameUpdate();

		_updateTimer = Timers.SetInterval(SetIsUpdatePending, 1000);

		LogManager.Info("[ScreenManager] Initialized!");
	}

	// worldPos2ScreenPos returns gibberish for some reason :(
	public Vector2? ConvertWorldPositionToScreenPosition(Vector3 worldPosition)
	{
		try
		{
			// Calculate vector from camera to world position
			var cameraToWorld = worldPosition - CameraPosition;

			// Check if world position is behind the camera
			if(Vector3.Dot(cameraToWorld, -_cameraForward) <= 0f)
			{
				return null;
			}

			var worldPosition4 = new Vector4(worldPosition, 1.0f);

			var clipSpacePosition = Vector4.Transform(worldPosition4, _viewProjectionMatrix);

			if(Utils.IsApproximatelyEqual(clipSpacePosition.W, 0f))
			{
				return null;
			}

			// Perform perspective division to get NDC
			var normalizedDeviceCoordinatesX = clipSpacePosition.X / clipSpacePosition.W;
			var normalizedDeviceCoordinatesY = clipSpacePosition.Y / clipSpacePosition.W;

			// Convert NDC to screen coordinates
			var screenX = (normalizedDeviceCoordinatesX + 1.0f) / 2.0f * WindowSize.X;
			var screenY = (1.0f - normalizedDeviceCoordinatesY) / 2.0f * WindowSize.Y;

			if(screenX < -_overheadX) return null;
			if(screenX > WindowSize.X + _overheadX) return null;
			if(screenY < -_overheadY) return null;
			if(screenY > WindowSize.Y + _overheadY) return null;

			return new Vector2(screenX, screenY);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return null;
		}
	}

	public float GetWorldPositionToCameraDistance(Vector3 worldPosition)
	{
		return Vector3.Distance(CameraPosition, worldPosition);
	}

	public void GameUpdate()
	{
		try
		{
			Update();

			if(_primaryCamera is null)
			{
				//LogManager.Warn("[ScreenManager.GameUpdate] No primary camera");
				return;
			}

			var viewProjectionMatrix = _primaryCamera.ViewProjMatrix;
			if(viewProjectionMatrix is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera view projection matrix");
				return;
			}

			_viewProjectionMatrix.M11 = viewProjectionMatrix.m00;
			_viewProjectionMatrix.M12 = viewProjectionMatrix.m01;
			_viewProjectionMatrix.M13 = viewProjectionMatrix.m02;
			_viewProjectionMatrix.M14 = viewProjectionMatrix.m03;
			_viewProjectionMatrix.M21 = viewProjectionMatrix.m10;
			_viewProjectionMatrix.M22 = viewProjectionMatrix.m11;
			_viewProjectionMatrix.M23 = viewProjectionMatrix.m12;
			_viewProjectionMatrix.M24 = viewProjectionMatrix.m13;
			_viewProjectionMatrix.M31 = viewProjectionMatrix.m20;
			_viewProjectionMatrix.M32 = viewProjectionMatrix.m21;
			_viewProjectionMatrix.M33 = viewProjectionMatrix.m22;
			_viewProjectionMatrix.M34 = viewProjectionMatrix.m23;
			_viewProjectionMatrix.M41 = viewProjectionMatrix.m30;
			_viewProjectionMatrix.M42 = viewProjectionMatrix.m31;
			_viewProjectionMatrix.M43 = viewProjectionMatrix.m32;
			_viewProjectionMatrix.M44 = viewProjectionMatrix.m33;

			var primaryCameraGameObject = _primaryCamera.GameObject;
			if(primaryCameraGameObject is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera game object");
				return;
			}

			var primaryCameraTransform = primaryCameraGameObject.Transform;
			if(primaryCameraTransform is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera transform");
				return;
			}

			var primaryCameraPosition = primaryCameraTransform.Position;
			if(primaryCameraPosition is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera position");
				return;
			}

			CameraPosition.X = primaryCameraPosition.x;
			CameraPosition.Y = primaryCameraPosition.y;
			CameraPosition.Z = primaryCameraPosition.z;

			var primaryCameraForward = primaryCameraTransform.AxisZ;
			if(primaryCameraForward is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera forward");
				return;
			}

			_cameraForward.X = primaryCameraForward.x;
			_cameraForward.Y = primaryCameraForward.y;
			_cameraForward.Z = primaryCameraForward.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info("[ScreenManager] Disposing...");

		_updateTimer.Dispose();

		LogManager.Info("[ScreenManager] Disposed!");
	}

	private void SetIsUpdatePending()
	{
		_isUpdatePending = true;
	}

	private unsafe void Update()
	{
		try
		{
			if(!_isUpdatePending) return;
			_isUpdatePending = false;

			var sceneManager = API.GetNativeSingleton("via.SceneManager");
			if(sceneManager is null)
			{
				LogManager.Warn("[ScreenManager.Update] No scene manager");
				return;
			}

			var mainViewObject = (ManagedObject) get_MainView_Method.InvokeBoxed(SceneView_Type, sceneManager, []);
			if(mainViewObject is null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view");
				return;
			}

			var mainViewPtr = (ulong) mainViewObject.Ptr();
			var mainView = ManagedObject.ToManagedObject(mainViewPtr).As<via.SceneView>();
			if(mainView is null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view");
				return;
			}

			_primaryCamera = mainView.PrimaryCamera;
			if(_primaryCamera is null)
			{
				//LogManager.Warn("[ScreenManager.Update] No primary camera");
				return;
			}


			var windowSize = mainView.WindowSize;
			if(windowSize is null)
			{
				LogManager.Warn("[ScreenManager.Update] No window size");
				return;
			}

			WindowSize.X = windowSize.w;
			WindowSize.Y = windowSize.h;

			_overheadX = 0.25f * WindowSize.X;
			_overheadY = 0.25f * WindowSize.Y;
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
			var sceneManager_TypeDef = via.SceneManager.REFType;

			get_MainView_Method = sceneManager_TypeDef.GetMethod("get_MainView");

			var SceneView_TypeDef = get_MainView_Method.GetReturnType();
			SceneView_Type = SceneView_TypeDef.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

}
