using System.Diagnostics;
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
			var customization = ConfigManager.Instance.ActiveConfig.Data;

			if(!customization.LargeMonsterUI.Enabled
			   && !customization.LargeMonsterUI.Dynamic.Enabled
			   && !customization.LargeMonsterUI.Static.Enabled
			   && !customization.LargeMonsterUI.Targeted.Enabled
			   && !customization.LargeMonsterUI.MapPin.Enabled
			   && !customization.SmallMonsterUI.Enabled
			   && !customization.EndemicLifeUI.Enabled)
			{
				return PreHookResult.Continue;
			}


			var enemyCharacterPtr = args[1];

			var enemyCharacterManagedObject = ManagedObject.ToManagedObject(enemyCharacterPtr);
			if(enemyCharacterManagedObject is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoUpdateEnd] No enemy character managed object");
				return PreHookResult.Continue;
			}

			var enemyCharacter = enemyCharacterManagedObject.As<EnemyCharacter>();
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
			if(isLargeMonster
			   && customization.LargeMonsterUI.Enabled
			   && (customization.LargeMonsterUI.Dynamic.Enabled
				   || customization.LargeMonsterUI.Static.Enabled
				   || customization.LargeMonsterUI.Targeted.Enabled
				   || customization.LargeMonsterUI.MapPin.Enabled))
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
			if(isSmallMonster && customization.SmallMonsterUI.Enabled)
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
			if(isEndemicLife && customization.EndemicLifeUI.Enabled)
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

			var enemyCharacterManagedObject = ManagedObject.ToManagedObject(enemyCharacterPtr);
			if(enemyCharacterManagedObject is null)
			{
				LogManager.Warn("[MonsterManager.OnPreDoOnDestroy] No enemy character managed object");
				return PreHookResult.Continue;
			}

			var enemyCharacter = enemyCharacterManagedObject.As<EnemyCharacter>();
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

		foreach(var largeMonsterPair in LargeMonsters)
		{
			largeMonsterPair.Value.Dispose();
		}

		foreach(var smallMonsterPair in SmallMonsters)
		{
			smallMonsterPair.Value.Dispose();
		}

		foreach(var endemicLifeEntityPair in EndemicLifeEntities)
		{
			endemicLifeEntityPair.Value.Dispose();
		}

		LogManager.Info("[LargeMonster] Disposed!");
	}
}