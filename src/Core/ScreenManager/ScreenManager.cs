using System.Numerics;

using REFrameworkNET;

using ValueType = REFrameworkNET.ValueType;

namespace YURI_Overlay;

internal sealed class ScreenManager
{
	private static readonly Lazy<ScreenManager> Lazy = new(() => new ScreenManager());
	public static ScreenManager Instance => Lazy.Value;

	public Vector2 WindowSize = new(1920f, 1080f);

	private ManagedObject PrimaryCamera;
	private Matrix4x4 _viewProjectionMatrix = Matrix4x4.Identity;

	private Vector3 _cameraPosition = Vector3.Zero;
	private Vector3 _cameraForward = Vector3.Zero;

	private float _overheadX = 0.25f * 1920f;
	private float _overheadY = 0.25f * 1080f;

	private Type SceneView_Type;
	private Type Camera_Type;
	private Type mat4_Type;
	private Type Single_Type;
	private Type Size_Type;

	private Field m00_Field;
	private Field m01_Field;
	private Field m02_Field;
	private Field m03_Field;
	private Field m10_Field;
	private Field m11_Field;
	private Field m12_Field;
	private Field m13_Field;
	private Field m20_Field;
	private Field m21_Field;
	private Field m22_Field;
	private Field m23_Field;
	private Field m30_Field;
	private Field m31_Field;
	private Field m32_Field;
	private Field m33_Field;
	private Field w_Field;
	private Field h_Field;

	private Method get_MainView_Method;
	private Method get_PrimaryCamera_Method;
	private Method get_WindowSize_Method;
	private Method get_ViewMatrix_Method;
	private Method get_ViewProjMatrix_Method;

	private ScreenManager() { }

	public void Initialize()
	{
		LogManager.Info("[ScreenManager] Initializing...");

		InitializeTdb();

		Update();

		Timers.SetInterval(Update, 1000);

		LogManager.Info("[ScreenManager] Initialized!");
	}

	// worldPos2ScreenPos returns gibberish for some reason :(
	public (Vector2?, float) ConvertWorldPositionToScreenPosition(Vector3 worldPosition)
	{
		try
		{
			// Calculate distance
			var distance = Vector3.Distance(_cameraPosition, worldPosition);

			// Calculate vector from camera to world position
			var cameraToWorld = worldPosition - _cameraPosition;

			// Check if world position is behind the camera
			if(Vector3.Dot(_cameraForward, cameraToWorld) < 0)
			{
				return (null, distance);
			}

			var worldPosition4 = new Vector4(worldPosition, 1.0f);

			var clipSpacePosition = Vector4.Transform(worldPosition4, _viewProjectionMatrix);

			if(Math.Abs(clipSpacePosition.W) < Constants.Epsilon)
			{
				return (null, distance);
			}

			// Perform perspective division to get NDC
			var normalizedDeviceCoordinatesX = clipSpacePosition.X / clipSpacePosition.W;
			var normalizedDeviceCoordinatesY = clipSpacePosition.Y / clipSpacePosition.W;

			// Convert NDC to screen coordinates
			var screenX = (normalizedDeviceCoordinatesX + 1.0f) / 2.0f * WindowSize.X;
			var screenY = (1.0f - normalizedDeviceCoordinatesY) / 2.0f * WindowSize.Y;

			if(screenX < -_overheadX) return (null, distance);
			if(screenX > WindowSize.X + _overheadX) return (null, distance);
			if(screenY < -_overheadY) return (null, distance);
			if(screenY > WindowSize.Y + _overheadY) return (null, distance);

			return (new Vector2(screenX, screenY), distance);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return (null, 0f);
		}
	}

	public float GetWorldPositionToCameraDistance(Vector3 worldPosition)
	{
		return Vector3.Distance(_cameraPosition, worldPosition);
	}

	public unsafe void FrameUpdate()
	{
		try
		{
			if(PrimaryCamera == null)
			{
				//LogManager.Warn("[ScreenManager.FrameUpdate] No primary camera");
				return;
			}

			var ViewMatrix_ValueType = (ValueType) get_ViewMatrix_Method.InvokeBoxed(mat4_Type, PrimaryCamera, []);
			if(ViewMatrix_ValueType == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix");
				return;
			}

			var ViewProjMatrix_ValueType = (ValueType) get_ViewProjMatrix_Method.InvokeBoxed(mat4_Type, PrimaryCamera, []);
			if(ViewProjMatrix_ValueType == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix");
				return;
			}

			var viewMatrixPointer = (ulong) ViewMatrix_ValueType.Ptr();
			var viewProjectionMatrixPointer = (ulong) ViewProjMatrix_ValueType.Ptr();

			var viewM20 = (float?) m20_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM20 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m20");
				return;
			}

			var viewM21 = (float?) m21_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM21 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m21");
				return;
			}

			var viewM22 = (float?) m22_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM22 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m22");
				return;
			}

			var viewM30 = (float?) m30_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM30 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m30");
				return;
			}

			var viewM31 = (float?) m31_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM31 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m31");
				return;
			}

			var viewM32 = (float?) m32_Field.GetDataBoxed(Single_Type, viewMatrixPointer, true);
			if(viewM32 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view matrix m32");
				return;
			}

			// Extract camera position and forward vector from view matrix
			_cameraPosition = new Vector3((float) viewM30, (float) viewM31, (float) viewM32);
			_cameraForward = new Vector3(-(float) viewM20, -(float) viewM21, -(float) viewM22);

			var viewProjM00 = (float?) m00_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM00 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m00");
				return;
			}

			var viewProjM01 = (float?) m01_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM01 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m01");
				return;
			}

			var viewProjM02 = (float?) m02_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM02 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m02");
				return;
			}

			var viewProjM03 = (float?) m03_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM03 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m03");
				return;
			}

			var viewProjM10 = (float?) m10_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM10 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m10");
				return;
			}

			var viewProjM11 = (float?) m11_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM11 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m11");
				return;
			}

			var viewProjM12 = (float?) m12_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM12 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m12");
				return;
			}

			var viewProjM13 = (float?) m13_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM13 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m13");
				return;
			}

			var viewProjM20 = (float?) m20_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM20 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m20");
				return;
			}

			var viewProjM21 = (float?) m21_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM21 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m21");
				return;
			}

			var viewProjM22 = (float?) m22_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM22 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m22");
				return;
			}

			var viewProjM23 = (float?) m23_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM23 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m23");
				return;
			}

			var viewProjM30 = (float?) m30_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM30 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m30");
				return;
			}

			var viewProjM31 = (float?) m31_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM31 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m31");
				return;
			}

			var viewProjM32 = (float?) m32_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM32 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m32");
				return;
			}

			var viewProjM33 = (float?) m33_Field.GetDataBoxed(Single_Type, viewProjectionMatrixPointer, true);
			if(viewProjM33 == null)
			{
				LogManager.Warn("[ScreenManager.FrameUpdate] No view projection matrix m33");
				return;
			}

			_viewProjectionMatrix.M11 = (float) viewProjM00;
			_viewProjectionMatrix.M12 = (float) viewProjM01;
			_viewProjectionMatrix.M13 = (float) viewProjM02;
			_viewProjectionMatrix.M14 = (float) viewProjM03;
			_viewProjectionMatrix.M21 = (float) viewProjM10;
			_viewProjectionMatrix.M22 = (float) viewProjM11;
			_viewProjectionMatrix.M23 = (float) viewProjM12;
			_viewProjectionMatrix.M24 = (float) viewProjM13;
			_viewProjectionMatrix.M31 = (float) viewProjM20;
			_viewProjectionMatrix.M32 = (float) viewProjM21;
			_viewProjectionMatrix.M33 = (float) viewProjM22;
			_viewProjectionMatrix.M34 = (float) viewProjM23;
			_viewProjectionMatrix.M41 = (float) viewProjM30;
			_viewProjectionMatrix.M42 = (float) viewProjM31;
			_viewProjectionMatrix.M43 = (float) viewProjM32;
			_viewProjectionMatrix.M44 = (float) viewProjM33;
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
			var sceneManager_TypeDef = TDB.Get().GetType("via.SceneManager");

			get_MainView_Method = sceneManager_TypeDef.GetMethod("get_MainView");

			var SceneView_TypeDef = get_MainView_Method.GetReturnType();
			SceneView_Type = SceneView_TypeDef.GetType();

			get_PrimaryCamera_Method = SceneView_TypeDef.GetMethod("get_PrimaryCamera");
			get_WindowSize_Method = SceneView_TypeDef.GetMethod("get_WindowSize");

			var Camera_TypeDef = get_PrimaryCamera_Method.GetReturnType();
			Camera_Type = Camera_TypeDef.GetType();

			get_ViewMatrix_Method = Camera_TypeDef.GetMethod("get_WorldMatrix");
			get_ViewProjMatrix_Method = Camera_TypeDef.GetMethod("get_ViewProjMatrix");

			var mat4_TypeDef = get_ViewMatrix_Method.GetReturnType();
			mat4_Type = mat4_TypeDef.GetType();

			m00_Field = mat4_TypeDef.GetField("m00");
			m01_Field = mat4_TypeDef.GetField("m01");
			m02_Field = mat4_TypeDef.GetField("m02");
			m03_Field = mat4_TypeDef.GetField("m03");
			m10_Field = mat4_TypeDef.GetField("m10");
			m11_Field = mat4_TypeDef.GetField("m11");
			m12_Field = mat4_TypeDef.GetField("m12");
			m13_Field = mat4_TypeDef.GetField("m13");
			m20_Field = mat4_TypeDef.GetField("m20");
			m21_Field = mat4_TypeDef.GetField("m21");
			m22_Field = mat4_TypeDef.GetField("m22");
			m23_Field = mat4_TypeDef.GetField("m23");
			m30_Field = mat4_TypeDef.GetField("m30");
			m31_Field = mat4_TypeDef.GetField("m31");
			m32_Field = mat4_TypeDef.GetField("m32");
			m33_Field = mat4_TypeDef.GetField("m33");

			Single_Type = m00_Field.GetType().GetType();

			var Size_TypeDef = get_WindowSize_Method.GetReturnType();
			Size_Type = Size_TypeDef.GetType();

			w_Field = Size_TypeDef.GetField("w");
			h_Field = Size_TypeDef.GetField("h");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe void Update()
	{
		try
		{
			var sceneManager = API.GetNativeSingleton("via.SceneManager");
			if(sceneManager == null)
			{
				LogManager.Warn("[ScreenManager.Update] No scene manager");
				return;
			}

			var MainView = (ManagedObject) get_MainView_Method.InvokeBoxed(SceneView_Type, sceneManager, []);
			if(MainView == null)
			{
				LogManager.Warn("[ScreenManager.Update] No main view");
				return;
			}

			PrimaryCamera = (ManagedObject) get_PrimaryCamera_Method.InvokeBoxed(Camera_Type, MainView, []);
			if(PrimaryCamera == null)
			{
				//LogManager.Warn("[ScreenManager.Update] No primary camera");
				//return;
			}

			var _WindowSize = (ValueType) get_WindowSize_Method.InvokeBoxed(Size_Type, MainView, []);
			if(_WindowSize == null)
			{
				LogManager.Warn("[ScreenManager.Update] No window size");
				return;
			}

			var w = (float?) w_Field.GetDataBoxed(Single_Type, (ulong) _WindowSize.Ptr(), true);
			if(w == null)
			{
				LogManager.Warn("[ScreenManager.Update] No window size w");
				return;
			}

			var h = (float?) h_Field.GetDataBoxed(Single_Type, (ulong) _WindowSize.Ptr(), true);
			if(h == null)
			{
				LogManager.Warn("[ScreenManager.Update] No window size h");
				return;
			}

			WindowSize.X = (float) w;
			WindowSize.Y = (float) h;

			_overheadX = 0.25f * WindowSize.X;
			_overheadY = 0.25f * WindowSize.Y;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
