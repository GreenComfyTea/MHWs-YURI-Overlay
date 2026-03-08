using System.Numerics;
using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class OverlayManager : IDisposable
{
	private static readonly Lazy<OverlayManager> Lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => Lazy.Value;

	private LargeMonsterUiManager? _largeMonsterUiManager;
	private SmallMonsterUiManager? _smallMonsterUiManager;
	private EndemicLifeUiManager? _endemicLifeUiManager;

	//private DamageMeterUiManager? _damageMeterUiManager = null;

	private OverlayManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[OverlayManager] Initializing...");

		this._largeMonsterUiManager = new LargeMonsterUiManager();
		this._smallMonsterUiManager = new SmallMonsterUiManager();
		this._endemicLifeUiManager = new EndemicLifeUiManager();
		//_damageMeterUiManager = new DamageMeterUiManager();

		LogManager.Info("[OverlayManager] Initialized!");
	}

	public void Draw()
	{
		try
		{
			ImGui.SetNextWindowPos(Vector2.Zero);
			ImGui.SetNextWindowSize(ImGui.GetIO().DisplaySize);

			ImGui.Begin(
				"YURI Overlay",
				ImGuiWindowFlags.NoMove
				| ImGuiWindowFlags.NoBackground
				| ImGuiWindowFlags.NoCollapse
				| ImGuiWindowFlags.NoResize
				| ImGuiWindowFlags.NoTitleBar
				| ImGuiWindowFlags.NoSavedSettings
				| ImGuiWindowFlags.NoScrollbar
			);

			var drawList = ImGui.GetWindowDrawList();

			this._largeMonsterUiManager?.Draw(drawList);
			this._smallMonsterUiManager?.Draw(drawList);
			this._endemicLifeUiManager?.Draw(drawList);
			//_damageMeterUiManager?.Draw(drawList);

			ImGui.End();
		}
		catch(Exception exception)
		{
			ImGui.End();
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info("[OverlayManager] Disposing...");

		this._largeMonsterUiManager?.Dispose();
		this._largeMonsterUiManager = null;

		this._smallMonsterUiManager?.Dispose();
		this._smallMonsterUiManager = null;

		this._endemicLifeUiManager?.Dispose();
		this._endemicLifeUiManager = null;

		//_damageMeterUiManager?.Dispose();
		//_damageMeterUiManager = null;

		LogManager.Info("[OverlayManager] Disposed!");
	}
}