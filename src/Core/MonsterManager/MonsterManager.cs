using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

internal sealed class MonsterManager
{
	private static readonly Lazy<MonsterManager> Lazy = new(() => new MonsterManager());

	public static MonsterManager Instance => Lazy.Value;

	public Dictionary<app.EnemyCharacter, LargeMonster> LargeMonsters = [];

	private MonsterManager() { }

	public void Initialize()
	{
		LogManager.Info("[MonsterManager] Initializing...");
		LogManager.Info("[MonsterManager] Initialized!");
	}

	[MethodHook(typeof(app.EnemyCharacter), nameof(app.EnemyCharacter.doUpdateEnd), MethodHookType.Pre)]
	private PreHookResult OnPreDoUpdateEnd(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr).As<app.EnemyCharacter>();

			var context = enemyCharacter._Context;
			if(context == null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context holder");
				return PreHookResult.Continue;
			}

			var enemyContext = context._Em;
			if(enemyContext == null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy context");
				return PreHookResult.Continue;
			}

			var isBoss = enemyContext.IsBoss;

			if(isBoss)
			{
				var isFound = LargeMonsters.ContainsKey(enemyCharacter);
				if(!isFound)
				{
					LargeMonster largeMonster = new(enemyCharacter, enemyContext);
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

	[MethodHook(typeof(app.EnemyCharacter), nameof(app.EnemyCharacter.doOnDestroy), MethodHookType.Pre)]
	private PreHookResult OnPreDoDestroy(Span<ulong> args)
	{
		try
		{
			var enemyCharacterPtr = args[1];
			var enemyCharacter = ManagedObject.ToManagedObject(enemyCharacterPtr).As<app.EnemyCharacter>();


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
