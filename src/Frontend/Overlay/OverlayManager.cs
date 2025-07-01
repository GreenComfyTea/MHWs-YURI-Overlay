using ImGuiNET;

namespace YURI_Overlay;

internal sealed class OverlayManager
{
	private static readonly Lazy<OverlayManager> Lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => Lazy.Value;

	private LargeMonsterUiManager _largeMonsterUiManager;
	private SmallMonsterUiManager _smallMonsterUiManager;
	private EndemicLifeUiManager _endemicLifeUiManager;
	//private DamageMeterUiManager _damageMeterUiManager;

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
			_smallMonsterUiManager.Draw(backgroundDrawList);
			_endemicLifeUiManager.Draw(backgroundDrawList);
			//_damageMeterUiManager.Draw(backgroundDrawList);

			ImGui.End();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}