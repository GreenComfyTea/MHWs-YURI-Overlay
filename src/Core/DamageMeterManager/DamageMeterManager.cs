using app;
using REFrameworkNET.Attributes;
using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class DamageMeterManager : IDisposable
{
	private static readonly Lazy<DamageMeterManager> Lazy = new(() => new DamageMeterManager());

	public static DamageMeterManager Instance => Lazy.Value;

	public LocalPlayer? LocalPlayer;
	public Dictionary<EnemyCharacter, OtherPlayer> OtherPlayers = [];
	public Dictionary<EnemyCharacter, SupportHunter> SupportHunters = [];

	public TotalDamageEntity TotalDamage = new();

	public DamageMeterManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[DamageMeterManager] Initializing...");

		InitializeLocalPlayer();

		PlayerManager.Instance.MasterPlayerChanged += OnMasterPlayerChanged;

		var typeDef = TDB.Get().GetType("app.cEnemyStockDamage");
		var method = typeDef.GetMethod("calcPreStockDamage");
		var preHook = method.AddHook(false);
		//preHook.AddPre(OnPre);

		LogManager.Info("[DamageMeterManager] Initialized!");
	}

	public void Dispose()
	{
		LogManager.Info("[DamageMeterManager] Disposing...");

		LocalPlayer?.Dispose();

		foreach(var otherPlayerPair in OtherPlayers)
		{
			otherPlayerPair.Value.Dispose();
		}

		foreach(var supportHunterPair in SupportHunters)
		{
			supportHunterPair.Value.Dispose();
		}

		LogManager.Info("[DamageMeterManager] Disposed!");
	}

	public void Update()
	{
		LocalPlayer?.Update();

		foreach(var otherPlayer in OtherPlayers.Values)
		{
			//otherPlayer.Update();
		}

		foreach(var supportHunter in SupportHunters.Values)
		{
			//supportHunter.Update();
		}
	}

	private void InitializeLocalPlayer()
	{
		var playerManagerMasterPlayer = PlayerManager.Instance.MasterPlayer;

		if(playerManagerMasterPlayer is null) return;
		if(LocalPlayer is not null && LocalPlayer.PlayerManageInfo == playerManagerMasterPlayer) return;

		LocalPlayer = new LocalPlayer(playerManagerMasterPlayer);
	}

	private void OnMasterPlayerChanged(object? sender, EventArgs e)
	{
		InitializeLocalPlayer();
	}

	//[MethodHook(typeof(MissionManager), nameof(MissionManager.addQuestAwardAmount), MethodHookType.Post)]
	public static void OnPostAddQuestAwardAmount(ref ulong returnValue)
	{
		try
		{
			Instance.LocalPlayer?.UpdateAwardDamage();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public static List<ulong> processedHitInfos = [];

	[MethodHook(typeof(HitInfo), nameof(HitInfo.getActualAttackOwner), MethodHookType.Pre)]
	public static PreHookResult OnPreCopy(Span<ulong> args)
	{
		var hitInfoPtr = args[1];

		var hitInfoManagedObject = ManagedObject.ToManagedObject(hitInfoPtr);
		if(hitInfoManagedObject is null)
		{
			LogManager.Warn("[DamageMeterManager.OnPreCopy] No hitInfoManagedObject");
			return PreHookResult.Continue;
		}

		var hitInfo = hitInfoManagedObject.As<HitInfo>();

		if(processedHitInfos.Contains(hitInfoPtr))
		{
			return PreHookResult.Continue;
		}

		processedHitInfos.Add(hitInfoPtr);

		LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] hitInfo.distance: {hitInfo.AttackData._Attack}");

		return PreHookResult.Continue;
	}

	// app.cEnemyStockDamage.calcPreStockDamage(app.cEnemyStockDamage.cPreCalcDamage, app.cEnemyStockDamage.cPreStockInfo, app.HitInfo)
	[MethodHook(typeof(cEnemyStockDamage), nameof(cEnemyStockDamage.calcPreStockDamage), MethodHookType.Pre)]
	public static PreHookResult OnPreCalcPreStockDamage(Span<ulong> args)
	{
		// args[0] = Thread
		// args[1] = "this" (cEnemyStockDamage)
		// args[2] = cPreCalcDamage?
		// args[3] = cPreStockInfo?
		// args[4] = HitInfo?

		try
		{
			if(args[5] != 0) return PreHookResult.Continue;

			LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Called {args.Length}");
			for(var i = 0; i < args.Length; i++)
			{
				LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Arg {i}: {args[i]} Managed {ManagedObject.IsManagedObject(args[i])}");
			}

			var enemyStockDamagePtr = args[1];

			var enemyStockDamageManagedObject = ManagedObject.ToManagedObject(enemyStockDamagePtr);
			if(enemyStockDamageManagedObject is null)
			{
				LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No enemyStockDamageManagedObject");
				return PreHookResult.Continue;
			}

			var enemyStockDamage = enemyStockDamageManagedObject.As<cEnemyStockDamage>();

			LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] EnemyStockDamage: {enemyStockDamage}");


			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}
}