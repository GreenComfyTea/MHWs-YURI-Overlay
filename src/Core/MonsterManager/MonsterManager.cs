using REFrameworkNET;

namespace YURI_Overlay;

internal sealed class MonsterManager : IDisposable
{
	private static readonly Lazy<MonsterManager> Lazy = new(() => new MonsterManager());

	public static MonsterManager Instance => Lazy.Value;

	public Dictionary<ManagedObject, LargeMonster> LargeMonsters = [];

	private Type Boolean_Type;

	private Method doUpdateEnd_Method;
	private Method doOnDestroy_Method;
	private Method get_IsBoss_Method;

	private Field _Context_Field;
	private Field _Em_Field;

	private MethodHook preDoUpdateEndHook;
	private MethodHook preDoOnDestroyHook;

	private MonsterManager() { }

	~MonsterManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		try
		{
			LogManager.Info("[MonsterManager] Initializing...");

			InitializeTdb();
			Hook();

			LogManager.Info("[MonsterManager] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info($"[MonsterManager] Disposing...");

		preDoUpdateEndHook.Dispose();

		LogManager.Info($"[MonsterManager] Disposed!");
	}

	private void InitializeTdb()
	{
		try
		{
			var EnemyCharacter_TypeDef = TDB.Get().GetType("app.EnemyCharacter");

			doUpdateEnd_Method = EnemyCharacter_TypeDef.GetMethod("doUpdateEnd");
			doOnDestroy_Method = EnemyCharacter_TypeDef.GetMethod("doOnDestroy");
			_Context_Field = EnemyCharacter_TypeDef.GetField("_Context");

			_Em_Field = _Context_Field.GetType().GetField("_Em");

			var enemyContext_TypeDef = _Em_Field.GetType();

			get_IsBoss_Method = enemyContext_TypeDef.GetMethod("get_IsBoss");

			Boolean_Type = get_IsBoss_Method.ReturnType.GetType();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void Hook()
	{
		try
		{
			LogManager.Info("[MonsterManager] Hooking...");


			preDoUpdateEndHook = doUpdateEnd_Method.AddHook(false);
			preDoUpdateEndHook.AddPre(OnPreDoUpdateEnd);

			preDoOnDestroyHook = doOnDestroy_Method.AddHook(false);
			preDoOnDestroyHook.AddPre(OnPreDoDestroy);

			LogManager.Info("[MonsterManager] Hooked!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private unsafe PreHookResult OnPreDoUpdateEnd(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr);


			var _Context = (ManagedObject) _Context_Field.GetDataBoxed(enemyCharacterPtr, false);
			if(_Context == null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context holder");
				return PreHookResult.Continue;
			}

			var _Em = (ManagedObject) _Em_Field.GetDataBoxed((ulong) _Context.Ptr(), false);
			if(_Em == null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context");
				return PreHookResult.Continue;
			}

			var isBoss = (bool?) get_IsBoss_Method.InvokeBoxed(Boolean_Type, _Em, []);
			if(isBoss == null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy boss type");
				return PreHookResult.Continue;
			}

			if(isBoss == true)
			{
				var isFound = LargeMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					LargeMonster largeMonster = new(enemyCharacter);
					LargeMonsters.Add(enemyCharacter, largeMonster);
				}
				else
				{
					LargeMonsters[enemyCharacter].Update();
				}
			}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}
	private PreHookResult OnPreDoDestroy(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr);


			var isFound = LargeMonsters.TryGetValue(enemyCharacter, out var largeMonster);
			if(isFound)
			{
				LogManager.Info($"[LargeMonster] Destroyed {largeMonster!.Name}");
				LargeMonsters.Remove(enemyCharacter);
			}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}
}
