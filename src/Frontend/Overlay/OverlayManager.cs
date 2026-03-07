using Hexa.NET.ImGui;
using System.Numerics;
using System.Runtime.InteropServices;

namespace YURI_Overlay;

internal sealed class OverlayManager : IDisposable
{
	private static readonly Lazy<OverlayManager> Lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => Lazy.Value;

	private LargeMonsterUiManager? _largeMonsterUiManager = null;
	private SmallMonsterUiManager? _smallMonsterUiManager = null;
	private EndemicLifeUiManager? _endemicLifeUiManager = null;
	//private DamageMeterUiManager? _damageMeterUiManager = null;

	private OverlayManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[OverlayManager] Initializing...");

		_largeMonsterUiManager = new LargeMonsterUiManager();
		_smallMonsterUiManager = new SmallMonsterUiManager();
		_endemicLifeUiManager = new EndemicLifeUiManager();
		//_damageMeterUiManager = new DamageMeterUiManager();

		LogManager.Info("[OverlayManager] Initialized!");
	}

	public void Draw()
	{
        try
		{
            ImGui.SetNextWindowPos(Vector2.Zero);
            ImGui.SetNextWindowSize(ImGui.GetIO().DisplaySize);

            ImGui.Begin("YURI Overlay",
				ImGuiWindowFlags.NoMove
				| ImGuiWindowFlags.NoBackground
				| ImGuiWindowFlags.NoCollapse
				| ImGuiWindowFlags.NoResize
				| ImGuiWindowFlags.NoTitleBar
				| ImGuiWindowFlags.NoSavedSettings
				| ImGuiWindowFlags.NoScrollbar
			);

            var drawList = ImGui.GetWindowDrawList();

            _largeMonsterUiManager?.Draw(drawList);
			_smallMonsterUiManager?.Draw(drawList);
			_endemicLifeUiManager?.Draw(drawList);
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

		_largeMonsterUiManager?.Dispose();
		_largeMonsterUiManager = null;

		_smallMonsterUiManager?.Dispose();
		_smallMonsterUiManager = null;

		_endemicLifeUiManager?.Dispose();
		_endemicLifeUiManager = null;

		//_damageMeterUiManager?.Dispose();
		//_damageMeterUiManager = null;

		LogManager.Info("[OverlayManager] Disposed!");
	}
}
