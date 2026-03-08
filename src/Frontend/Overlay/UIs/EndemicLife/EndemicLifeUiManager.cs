using Hexa.NET.ImGui;
using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class EndemicLifeUiManager : IDisposable
{
	private List<EndemicLifeEntity> _dynamicEndemicLifeEntities = [];

	private readonly List<Timer> _timers = [];

	public EndemicLifeUiManager()
	{
		this.Initialize();
	}

	~EndemicLifeUiManager()
	{
		this.Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[EndemicLifeUiManager] Initializing...");

		this.InitializeTimers();

		LogManager.Info("[EndemicLifeUiManager] Initialized!");
	}

	public void Draw(ImDrawListPtr drawList)
	{
		this.DrawDynamicUi(drawList);
	}

	public void Dispose()
	{
		LogManager.Info("[EndemicLifeUiManager] Disposing...");

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		LogManager.Info("[EndemicLifeUiManager] Disposed!");
	}

	private void InitializeTimers()
	{
		var updateDelays = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.Performance.UpdateDelays.UIs;

		foreach(var timer in this._timers)
		{
			timer.Dispose();
		}

		this._timers.Clear();

		this._timers.Add(Timers.SetInterval(this.UpdateDynamic, Utils.SecondsToMilliseconds(updateDelays.EndemicLife)));
	}

	private void UpdateDynamic()
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.EndemicLifeUI;

		if(customization.Enabled != true)
		{
			this._dynamicEndemicLifeEntities = [];

			return;
		}

		List<EndemicLifeEntity> newEndemicLifeEntities = [];

		// Filter out dead and captured

		foreach(var endemicLifeEntityPair in MonsterManager.Instance.EndemicLifeEntities)
		{
			var endemicLifeEntity = endemicLifeEntityPair.Value;

			newEndemicLifeEntities.Add(endemicLifeEntity);
		}

		// Sort by distance
		// Closest are drawn last
		newEndemicLifeEntities.Sort(EndemicLifeSorting.CompareByDistanceReversed);

		this._dynamicEndemicLifeEntities = newEndemicLifeEntities;
	}

	private void DrawDynamicUi(ImDrawListPtr drawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.EndemicLifeUI;

		if(customization.Enabled != true)
		{
			return;
		}

		foreach(var endemicLifeEntity in this._dynamicEndemicLifeEntities)
		{
			endemicLifeEntity.DynamicUi?.Draw(drawList);
		}
	}
}