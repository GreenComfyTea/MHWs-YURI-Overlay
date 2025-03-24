using ImGuiNET;

namespace YURI_Overlay;

internal sealed class OverlayManager
{
	private static readonly Lazy<OverlayManager> Lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => Lazy.Value;

	private LargeMonsterUiManager _largeMonsterUiManager;


	private OverlayManager() { }

	public void Initialize()
	{
		_largeMonsterUiManager = new LargeMonsterUiManager();
	}

	public void Draw()
	{
		try
		{
			ImGui.Begin("YURI Overlay",
				ImGuiWindowFlags.NoMove
				| ImGuiWindowFlags.NoBackground
				| ImGuiWindowFlags.NoCollapse
				| ImGuiWindowFlags.NoResize
				| ImGuiWindowFlags.NoTitleBar
				| ImGuiWindowFlags.NoSavedSettings
				| ImGuiWindowFlags.NoScrollbar
			);

			var backgroundDrawList = ImGui.GetBackgroundDrawList();

			_largeMonsterUiManager.Draw(backgroundDrawList);

			ImGui.End();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
