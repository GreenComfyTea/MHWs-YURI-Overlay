using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

internal sealed class MonsterManager : IDisposable
{
	private static readonly Lazy<MonsterManager> Lazy = new(() => new MonsterManager());

	public static MonsterManager Instance => Lazy.Value;

	public Dictionary<app.EnemyCharacter, LargeMonster> LargeMonsters = [];
	public Dictionary<app.EnemyCharacter, SmallMonster> SmallMonsters = [];
	public Dictionary<app.EnemyCharacter, EndemicLife> Animals = [];

	private MonsterManager() { }

	public void Initialize()
	{
		LogManager.Info("[MonsterManager] Initializing...");
		LogManager.Info("[MonsterManager] Initialized!");
	}

	[MethodHook(typeof(app.EnemyCharacter), nameof(app.EnemyCharacter.doUpdateEnd), MethodHookType.Pre)]
	public static PreHookResult OnPreDoUpdateEnd(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacterManagedObject = ManagedObject.ToManagedObject(enemyCharacterPtr);
			if(enemyCharacterManagedObject is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy character");
				return PreHookResult.Continue;
			}

			var enemyCharacter = enemyCharacterManagedObject.As<app.EnemyCharacter>();

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
					var largeMonster = new LargeMonster(enemyCharacter, enemyContext, enemyCharacterManagedObject);
					Instance.LargeMonsters.Add(enemyCharacter, largeMonster);
				}
				else
				{
					Instance.LargeMonsters[enemyCharacter].Update();
				}
			}

			var isSmallMonster = enemyContext.IsZako;
			if(isSmallMonster)
			{
				var isFound = Instance.SmallMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					var smallMonster = new SmallMonster(enemyCharacter, enemyContext, enemyCharacterManagedObject);
					Instance.SmallMonsters.Add(enemyCharacter, smallMonster);
				}
				else
				{
					Instance.SmallMonsters[enemyCharacter].Update();
				}
			}

			//var isAnimal = enemyContext.IsAnimal;
			//if(isAnimal)
			//{
			//	var isFound = Instance.Animals.ContainsKey(enemyCharacter);
			//	if(!isFound)
			//	{
			//		var animal = new EndemicLife(enemyCharacter, enemyContext, enemyCharacterManagedObject);
			//		Instance.Animals.Add(enemyCharacter, animal);
			//	}
			//	else
			//	{
			//		Instance.Animals[enemyCharacter].Update();
			//	}
			//}

			return PreHookResult.Continue;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return PreHookResult.Continue;
		}
	}

	[MethodHook(typeof(app.EnemyCharacter), nameof(app.EnemyCharacter.doOnDestroy), MethodHookType.Pre)]
	public static PreHookResult OnPreDoOnDestroy(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr).As<app.EnemyCharacter>();

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

			//var isAnimal = enemyContext.IsAnimal;
			//if(isAnimal)
			//{
			//	if(Instance.Animals.ContainsKey(enemyCharacter))
			//	{
			//		Instance.Animals[enemyCharacter].Dispose();
			//		Instance.Animals.Remove(enemyCharacter);
			//	}

			//	return PreHookResult.Continue;
			//}

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
		LogManager.Info($"[LargeMonster] Disposing...");

		foreach(var largeMonster in LargeMonsters.Values)
		{
			largeMonster.Dispose();
		}

		LogManager.Info($"[LargeMonster] Disposed!");
	}
}
