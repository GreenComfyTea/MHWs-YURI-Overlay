using Hexa.NET.ImGui;
using System.Numerics;
using System.Runtime.InteropServices;

namespace YURI_Overlay;

internal sealed class OverlayManager
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
		_largeMonsterUiManager = new LargeMonsterUiManager();
		_smallMonsterUiManager = new SmallMonsterUiManager();
		_endemicLifeUiManager = new EndemicLifeUiManager();
		//_damageMeterUiManager = new DamageMeterUiManager();
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
		}
		catch(Exception exception)
		{
            LogManager.Error(exception);
		}
		finally
		{
            ImGui.End();
        }
	}
}