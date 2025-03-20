using ImGuiNET;

namespace YURI_Overlay;

internal sealed class OverlayManager
{
	private static readonly Lazy<OverlayManager> Lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => Lazy.Value;

	private LargeMonsterUiManager _LargeMonsterUiManager;


	private OverlayManager() { }

	public void Initialize()
	{
		_LargeMonsterUiManager = new LargeMonsterUiManager();
	}

	public void Draw()
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

		_LargeMonsterUiManager.Draw(backgroundDrawList);

		ImGui.End();
	}
}
