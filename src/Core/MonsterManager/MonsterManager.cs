using app;
using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

internal sealed class MonsterManager : IDisposable
{
	private static readonly Lazy<MonsterManager> Lazy = new(() => new MonsterManager());

	public static MonsterManager Instance => Lazy.Value;

	public Dictionary<EnemyCharacter, LargeMonster> LargeMonsters = [];
	public Dictionary<EnemyCharacter, SmallMonster> SmallMonsters = [];
	public Dictionary<EnemyCharacter, EndemicLifeEntity> EndemicLifeEntities = [];

	private MonsterManager()
	{
	}

	public void Initialize()
	{
		LogManager.Info("[MonsterManager] Initializing...");
		LogManager.Info("[MonsterManager] Initialized!");
	}

	[MethodHook(typeof(EnemyCharacter), nameof(EnemyCharacter.doUpdateEnd), MethodHookType.Pre)]
	public static PreHookResult OnPreDoUpdateEnd(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];

			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr).As<EnemyCharacter>();
			if(enemyCharacter is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy character");
				return PreHookResult.Continue;
			}

			var context = enemyCharacter._Context;
			if(context is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context holder");
				return PreHookResult.Continue;
			}

			var enemyContext = context._Em;
			if(enemyContext is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context");
				return PreHookResult.Continue;
			}

			var isLargeMonster = enemyContext.IsBoss;
			if(isLargeMonster)
			{
				var isFound = Instance.LargeMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					var largeMonster = new LargeMonster(enemyCharacter, enemyContext);
					Instance.LargeMonsters.Add(enemyCharacter, largeMonster);
				}
				else
					Instance.LargeMonsters[enemyCharacter].Update();
			}

			var isSmallMonster = enemyContext.IsZako;
			if(isSmallMonster)
			{
				var isFound = Instance.SmallMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					var smallMonster = new SmallMonster(enemyCharacter, enemyContext);
					Instance.SmallMonsters.Add(enemyCharacter, smallMonster);
				}
				else
					Instance.SmallMonsters[enemyCharacter].Update();
			}

			var isEndemicLife = enemyContext.IsAnimal;
			if(isEndemicLife)
			{
				var isFound = Instance.EndemicLifeEntities.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					var animal = new EndemicLifeEntity(enemyCharacter, enemyContext);
					Instance.EndemicLifeEntities.Add(enemyCharacter, animal);
				}
				else
					Instance.EndemicLifeEntities[enemyCharacter].Update();
			}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}

	[MethodHook(typeof(EnemyCharacter), nameof(EnemyCharacter.doOnDestroy), MethodHookType.Pre)]
	public static PreHookResult OnPreDoOnDestroy(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];

			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr).As<EnemyCharacter>();
			if(enemyCharacter is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoOnDestroy] No enemy character");
				return PreHookResult.Continue;
			}

			var context = enemyCharacter._Context;
			if(context is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoOnDestroy] No enemy context holder");
				return PreHookResult.Continue;
			}

			var enemyContext = context._Em;
			if(enemyContext is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context");
				return PreHookResult.Continue;
			}

			var isLargeMonster = enemyContext.IsBoss;
			if(isLargeMonster)
			{
				var isFound = Instance.LargeMonsters.ContainsKey(enemyCharacter);
				if(isFound)
				{
					Instance.LargeMonsters[enemyCharacter].Dispose();
					Instance.LargeMonsters.Remove(enemyCharacter);
				}

				return PreHookResult.Continue;
			}

			var isSmallMonster = enemyContext.IsZako;
			if(isSmallMonster)
			{
				var isFound = Instance.SmallMonsters.ContainsKey(enemyCharacter);
				if(isFound)
				{
					Instance.SmallMonsters[enemyCharacter].Dispose();
					Instance.SmallMonsters.Remove(enemyCharacter);
				}

				return PreHookResult.Continue;
			}

			var isEndemicLife = enemyContext.IsAnimal;
			if(isEndemicLife)
			{
				if(Instance.EndemicLifeEntities.ContainsKey(enemyCharacter))
				{
					Instance.EndemicLifeEntities[enemyCharacter].Dispose();
					Instance.EndemicLifeEntities.Remove(enemyCharacter);
				}

				return PreHookResult.Continue;
			}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}

	public void Dispose()
	{
		LogManager.Info("[LargeMonster] Disposing...");

		foreach(var smallMonster in SmallMonsters.Values)
		{
			smallMonster.Dispose();
		}

		foreach(var largeMonster in LargeMonsters.Values)
		{
			largeMonster.Dispose();
		}

		foreach(var endemicLifeEntity in EndemicLifeEntities.Values)
		{
			endemicLifeEntity.Dispose();
		}

		LogManager.Info("[LargeMonster] Disposed!");
	}
}