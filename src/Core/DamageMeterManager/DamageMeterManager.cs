using app;
using REFrameworkNET.Attributes;
using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class DamageMeterManager : IDisposable
{
	private static readonly Lazy<DamageMeterManager> Lazy = new(() => new DamageMeterManager());

	public static DamageMeterManager Instance => Lazy.Value;

	public LocalPlayer LocalPlayer;
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
		preHook.AddPre(OnPre);

		LogManager.Info("[DamageMeterManager] Initialized!");
	}

	public void Dispose()
	{
		LogManager.Info("[DamageMeterManager] Disposing...");

		LocalPlayer.Dispose();

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

	private void OnMasterPlayerChanged(object sender, EventArgs e)
	{
		InitializeLocalPlayer();
	}

	[MethodHook(typeof(MissionManager), nameof(MissionManager.addQuestAwardAmount), MethodHookType.Post)]
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

	// calcPreStockDamage(app.cEnemyStockDamage.cPreCalcDamage, app.cEnemyStockDamage.cPreStockInfo, app.HitInfo)
	[MethodHook(typeof(cEnemyStockDamage), nameof(cEnemyStockDamage.calcPreStockDamage), MethodHookType.Pre)]
	public static PreHookResult OnPreCalcPreStockDamage(Span<ulong> args)
	{
		try
		{
			LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Called {args.Length}");
			for(var i = 0; i < args.Length; i++)
			{
				LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Arg {i}: {args[i]}");
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var preCalcDamagePtr = args[i];

					var preCalcDamageManagedObject = ManagedObject.ToManagedObject(preCalcDamagePtr);
					if(preCalcDamageManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage managed object");
						continue;
					}

					var preCalcDamage = preCalcDamageManagedObject.As<cEnemyStockDamage.cPreCalcDamage>();
					if(preCalcDamage is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] preCalcDamage {i}: {preCalcDamage}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<cEnemyStockDamage.cPreCalcDamage> {i}: {exception}");
				}
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var preStockInfoPtr = args[i];

					var preStockInfoManagedObject = ManagedObject.ToManagedObject(preStockInfoPtr);
					if(preStockInfoManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info managed object");
						continue;
					}

					var preStockInfo = preStockInfoManagedObject.As<cEnemyStockDamage.cPreStockInfo>();
					if(preStockInfo is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] preStockInfo {i}: {preStockInfo}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<cEnemyStockDamage.cPreStockInfo> {i}: {exception}");
				}
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var hitInfoPtr = args[i];

					var hitInfoManagedObject = ManagedObject.ToManagedObject(hitInfoPtr);
					if(hitInfoManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info managed object");
						continue;
					}

					var hitInfo = hitInfoManagedObject.As<HitInfo>();
					if(hitInfo is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] HitInfo {i}: {hitInfo}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<HitInfo> {i}: {exception}");
				}
			}

			//var preCalcDamagePtr = args[1];

			//var preCalcDamageManagedObject = ManagedObject.ToManagedObject(preCalcDamagePtr);
			//if(preCalcDamageManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage managed object");
			//	return PreHookResult.Continue;
			//}

			//var preCalcDamage = preCalcDamageManagedObject.As<cEnemyStockDamage.cPreCalcDamage>();
			//if(preCalcDamage is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage");
			//	return PreHookResult.Continue;
			//}

			//var preStockInfoPtr = args[2];

			//var preStockInfoManagedObject = ManagedObject.ToManagedObject(preStockInfoPtr);
			//if(preStockInfoManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info managed object");
			//	return PreHookResult.Continue;
			//}

			//var preStockInfo = preStockInfoManagedObject.As<cEnemyStockDamage.cPreStockInfo>();
			//if(preStockInfo is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info");
			//	return PreHookResult.Continue;
			//}

			//var hitInfoPtr = args[3];

			//var hitInfoManagedObject = ManagedObject.ToManagedObject(hitInfoPtr);
			//if(hitInfoManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info managed object");
			//	return PreHookResult.Continue;
			//}

			//var hitInfo = hitInfoManagedObject.As<HitInfo>();
			//if(hitInfo is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info");
			//	return PreHookResult.Continue;
			//}

			//var attachOwner = hitInfo.AttackOwner;
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Attack Owner: {attachOwner}");
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Local player: {Instance.LocalPlayer.PlayerManageInfo}");
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Local player game object: {Instance.LocalPlayer.PlayerManageInfo.Character.GameObject}");


			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}

	private PreHookResult OnPre(Span<ulong> args)
	{
		try
		{
			LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Called {args.Length}");
			for(var i = 0; i < args.Length; i++)
			{
				LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Arg {i}: {args[i]}");
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var preCalcDamagePtr = args[i];

					var preCalcDamageManagedObject = ManagedObject.ToManagedObject(preCalcDamagePtr);
					if(preCalcDamageManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage managed object");
						continue;
					}

					var preCalcDamage = preCalcDamageManagedObject.As<cEnemyStockDamage.cPreCalcDamage>();
					if(preCalcDamage is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] preCalcDamage {i}: {preCalcDamage}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<cEnemyStockDamage.cPreCalcDamage> {i}: {exception}");
				}
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var preStockInfoPtr = args[i];

					var preStockInfoManagedObject = ManagedObject.ToManagedObject(preStockInfoPtr);
					if(preStockInfoManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info managed object");
						continue;
					}

					var preStockInfo = preStockInfoManagedObject.As<cEnemyStockDamage.cPreStockInfo>();
					if(preStockInfo is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] preStockInfo {i}: {preStockInfo}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<cEnemyStockDamage.cPreStockInfo> {i}: {exception}");
				}
			}

			for(var i = 0; i < args.Length; i++)
			{
				try
				{
					var hitInfoPtr = args[i];

					var hitInfoManagedObject = ManagedObject.ToManagedObject(hitInfoPtr);
					if(hitInfoManagedObject is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info managed object");
						continue;
					}

					var hitInfo = hitInfoManagedObject.As<HitInfo>();
					if(hitInfo is null)
					{
						LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info");
						continue;
					}

					LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] HitInfo {i}: {hitInfo}");
				}
				catch(Exception exception)
				{
					LogManager.Error($"[DamageMeterManager.OnPreEnemyStockDamage] As<HitInfo> {i}: {exception}");
				}
			}

			//var preCalcDamagePtr = args[1];

			//var preCalcDamageManagedObject = ManagedObject.ToManagedObject(preCalcDamagePtr);
			//if(preCalcDamageManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage managed object");
			//	return PreHookResult.Continue;
			//}

			//var preCalcDamage = preCalcDamageManagedObject.As<cEnemyStockDamage.cPreCalcDamage>();
			//if(preCalcDamage is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre calc damage");
			//	return PreHookResult.Continue;
			//}

			//var preStockInfoPtr = args[2];

			//var preStockInfoManagedObject = ManagedObject.ToManagedObject(preStockInfoPtr);
			//if(preStockInfoManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info managed object");
			//	return PreHookResult.Continue;
			//}

			//var preStockInfo = preStockInfoManagedObject.As<cEnemyStockDamage.cPreStockInfo>();
			//if(preStockInfo is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No pre stock info");
			//	return PreHookResult.Continue;
			//}

			//var hitInfoPtr = args[3];

			//var hitInfoManagedObject = ManagedObject.ToManagedObject(hitInfoPtr);
			//if(hitInfoManagedObject is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info managed object");
			//	return PreHookResult.Continue;
			//}

			//var hitInfo = hitInfoManagedObject.As<HitInfo>();
			//if(hitInfo is null)
			//{
			//	LogManager.Warn("[DamageMeterManager.OnPreEnemyStockDamage] No hit info");
			//	return PreHookResult.Continue;
			//}

			//var attachOwner = hitInfo.AttackOwner;
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Attack Owner: {attachOwner}");
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Local player: {Instance.LocalPlayer.PlayerManageInfo}");
			//LogManager.Debug($"[DamageMeterManager.OnPreEnemyStockDamage] Local player game object: {Instance.LocalPlayer.PlayerManageInfo.Character.GameObject}");


			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}
}