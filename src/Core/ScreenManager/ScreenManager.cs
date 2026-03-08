using System.Numerics;
using REFrameworkNET;
using via;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class ScreenManager : IDisposable
{
	private static readonly Lazy<ScreenManager> Lazy = new(() => new ScreenManager());
	public static ScreenManager Instance => Lazy.Value;

	public Vector2 WindowSize = new(1920f, 1080f);
	public Vector3 CameraPosition = Vector3.Zero;

	private Camera? _primaryCamera;
	private Matrix4x4 _viewProjectionMatrix = Matrix4x4.Identity;

	private Vector3 _cameraForward = Vector3.Zero;

	private float _overheadX = 0.25f * 1920f;
	private float _overheadY = 0.25f * 1080f;

	private bool _isUpdatePending = true;

	private readonly List<Timer> _timers = [];

	private Type? _sceneViewType;

	private Method? _getMainViewMethod;

	private ScreenManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[ScreenManager] Initializing...");

		this.InitializeTdb();
		this.GameUpdate();
		this.InitializeTimers();

		ConfigManager.Instance.AnyConfigChanged += this.OnAnyConfigChanged;

		LogManager.Info("[ScreenManager] Initialized!");
	}

	// worldPos2ScreenPos returns gibberish for some reason :(
	public Vector2? ConvertWorldPositionToScreenPosition(Vector3 worldPosition)
	{
		try
		{
			// Calculate vector from camera to world position
			var cameraToWorld = worldPosition - this.CameraPosition;

			// Check if world position is behind the camera
			if(Vector3.Dot(cameraToWorld, -this._cameraForward) <= 0f)
			{
				return null;
			}

			var worldPosition4 = new Vector4(worldPosition, 1.0f);

			var clipSpacePosition = Vector4.Transform(worldPosition4, this._viewProjectionMatrix);

			if(Utils.IsApproximatelyEqual(clipSpacePosition.W, 0f))
			{
				return null;
			}

			// Perform perspective division to get NDC
			var normalizedDeviceCoordinatesX = clipSpacePosition.X / clipSpacePosition.W;
			var normalizedDeviceCoordinatesY = clipSpacePosition.Y / clipSpacePosition.W;

			// Convert NDC to screen coordinates
			var screenX = (normalizedDeviceCoordinatesX + 1.0f) / 2.0f * this.WindowSize.X;
			var screenY = (1.0f - normalizedDeviceCoordinatesY) / 2.0f * this.WindowSize.Y;

			if(screenX < -this._overheadX)
			{
				return null;
			}

			if(screenX > this.WindowSize.X + this._overheadX)
			{
				return null;
			}

			if(screenY < -this._overheadY)
			{
				return null;
			}

			if(screenY > this.WindowSize.Y + this._overheadY)
			{
				return null;
			}

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
		return Vector3.Distance(this.CameraPosition, worldPosition);
	}

	public void GameUpdate()
	{
		try
		{
			this.Update();

			if(this._primaryCamera is null)
				//LogManager.Warn("[ScreenManager.GameUpdate] No primary camera");
			{
				return;
			}

			var viewProjectionMatrix = this._primaryCamera.ViewProjMatrix;

			if(viewProjectionMatrix is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera view projection matrix");

				return;
			}

			this._viewProjectionMatrix.M11 = viewProjectionMatrix.m00;
			this._viewProjectionMatrix.M12 = viewProjectionMatrix.m01;
			this._viewProjectionMatrix.M13 = viewProjectionMatrix.m02;
			this._viewProjectionMatrix.M14 = viewProjectionMatrix.m03;
			this._viewProjectionMatrix.M21 = viewProjectionMatrix.m10;
			this._viewProjectionMatrix.M22 = viewProjectionMatrix.m11;
			this._viewProjectionMatrix.M23 = viewProjectionMatrix.m12;
			this._viewProjectionMatrix.M24 = viewProjectionMatrix.m13;
			this._viewProjectionMatrix.M31 = viewProjectionMatrix.m20;
			this._viewProjectionMatrix.M32 = viewProjectionMatrix.m21;
			this._viewProjectionMatrix.M33 = viewProjectionMatrix.m22;
			this._viewProjectionMatrix.M34 = viewProjectionMatrix.m23;
			this._viewProjectionMatrix.M41 = viewProjectionMatrix.m30;
			this._viewProjectionMatrix.M42 = viewProjectionMatrix.m31;
			this._viewProjectionMatrix.M43 = viewProjectionMatrix.m32;
			this._viewProjectionMatrix.M44 = viewProjectionMatrix.m33;

			var primaryCameraGameObject = this._primaryCamera.GameObject;

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

			this.CameraPosition.X = primaryCameraPosition.x;
			this.CameraPosition.Y = primaryCameraPosition.y;
			this.CameraPosition.Z = primaryCameraPosition.z;

			var primaryCameraForward = primaryCameraTransform.AxisZ;

			if(primaryCameraForward is null)
			{
				LogManager.Warn("[ScreenManager.GameUpdate] No primary camera forward");

				return;
			}

			this._cameraForward.X = primaryCameraForward.x;
			this._cameraForward.Y = primaryCameraForward.y;
			this._cameraForward.Z = primaryCameraForward.z;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info("[ScreenManager] Disposing...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		ConfigManager.Instance.AnyConfigChanged -= this.OnAnyConfigChanged;

		LogManager.Info("[ScreenManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.ScreenManager;

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

	private unsafe void Update()
	{
		try
		{
			if(!this._isUpdatePending)
			{
				return;
			}

			this._isUpdatePending = false;

			var sceneManager = API.GetNativeSingleton("via.SceneManager");

			if(sceneManager is null)
			{
				LogManager.Warn("[ScreenManager.Update] No scene manager");

				return;
			}

			var mainViewObject = (ManagedObject?) this._getMainViewMethod?.InvokeBoxed(this._sceneViewType, sceneManager, []);

			if(mainViewObject is null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view");

				return;
			}

			var mainViewPtr = (ulong) mainViewObject.Ptr();

			var mainViewManagedObject = ManagedObject.ToManagedObject(mainViewPtr);

			if(mainViewManagedObject is null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view managed object");

				return;
			}

			var mainView = mainViewManagedObject.As<SceneView>();

			if(mainView is null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view");

				return;
			}

			this._primaryCamera = mainView.PrimaryCamera;

			if(this._primaryCamera is null)
				//LogManager.Warn("[ScreenManager.Update] No primary camera");
			{
				return;
			}

			var windowSize = mainView.WindowSize;

			if(windowSize is null)
			{
				LogManager.Warn("[ScreenManager.Update] No window size");

				return;
			}

			this.WindowSize.X = windowSize.w;
			this.WindowSize.Y = windowSize.h;

			this._overheadX = 0.25f * this.WindowSize.X;
			this._overheadY = 0.25f * this.WindowSize.Y;
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
			var sceneManagerTypeDef = SceneManager.REFType;

			this._getMainViewMethod = sceneManagerTypeDef.GetMethod("get_MainView");

			var sceneViewTypeDef = this._getMainViewMethod.GetReturnType();
			this._sceneViewType = sceneViewTypeDef.GetType();
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