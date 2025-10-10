using REFrameworkNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class MethodHookPatternAttribute : Attribute
{
	private static readonly List<IDisposable> ActiveHooks = [];

	public Type DeclaringType { get; }
	public string MethodSignaturePattern { get; }
	public MethodHookType HookType { get; }

	public MethodHookPatternAttribute(Type declaringType, string methodSignaturePattern, MethodHookType type)
	{
		DeclaringType = declaringType;
		MethodSignaturePattern = methodSignaturePattern;
		HookType = type;
	}

	public static void Initialize()
	{
		foreach(var method in Assembly.GetExecutingAssembly().GetTypes()
									  .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
									  .Where(method => method.GetCustomAttributes<MethodHookPatternAttribute>().Any()))
		{
			foreach(var attr in method.GetCustomAttributes<MethodHookPatternAttribute>())
			{
				TryHook(attr, method);
			}
		}
	}

	private static void TryHook(MethodHookPatternAttribute methodHookPatternAttribute, MethodInfo destination)
	{
		try
		{
			LogManager.Info($"[MethodHookPattern] Found MethodHook for {methodHookPatternAttribute.DeclaringType.Name}.{methodHookPatternAttribute.MethodSignaturePattern} in {destination.Name} in {destination.DeclaringType?.FullName}");

			if(!destination.IsStatic)
			{
				throw new ArgumentException("Destination method must be static");
			}

			// === Step 1: Get type definition from your framework ===
			var refTypeField = methodHookPatternAttribute.DeclaringType.GetField("REFType", BindingFlags.Static | BindingFlags.Public);
			if(refTypeField is null)
			{
				throw new ArgumentException($"Type {methodHookPatternAttribute.DeclaringType.Name} does not have a REFrameworkNET.TypeDefinition field");
			}

			var typeDefinition = (TypeDefinition?) refTypeField.GetValue(null);
			if(typeDefinition is null)
			{
				throw new ArgumentException($"Type {methodHookPatternAttribute.DeclaringType.Name} does not have a REFrameworkNET.TypeDefinition field");
			}

			var method = typeDefinition.Methods.Find((method) => method.Name.Contains(methodHookPatternAttribute.MethodSignaturePattern, StringComparison.Ordinal));
			if(method is null)
			{
				throw new ArgumentException("Method not found");
			}

			var hook = method.AddHook(false);
			if(hook is null)
			{
				throw new ArgumentException("Invalid method hook");
			}

			if(methodHookPatternAttribute.HookType == MethodHookType.Pre)
			{
				var preHookDelegate = Delegate.CreateDelegate(typeof(MethodHook.PreHookDelegate), destination);
				if(preHookDelegate is null)
				{
					throw new ArgumentException("Failed to create delegate");
				}

				hook.AddPre((MethodHook.PreHookDelegate) preHookDelegate);
			}
			else if(methodHookPatternAttribute.HookType == MethodHookType.Post)
			{
				var postHookDelegate = Delegate.CreateDelegate(typeof(MethodHook.PostHookDelegate), destination);
				if(postHookDelegate is null)
				{
					throw new ArgumentException("Failed to create delegate");
				}

				hook.AddPost((MethodHook.PostHookDelegate) postHookDelegate);
			}

			ActiveHooks.Add(hook);

			LogManager.Info($"[MethodHookPattern] Installed MethodHook for {methodHookPatternAttribute.DeclaringType.Name}.{methodHookPatternAttribute.MethodSignaturePattern} in {destination.Name} in {destination.DeclaringType?.FullName}");
		}
		catch(Exception exception)
		{
			LogManager.Info($"[MethodHookPattern] Found MethodHook for {methodHookPatternAttribute.DeclaringType.Name}.{methodHookPatternAttribute.MethodSignaturePattern} in {destination.Name} in {destination.DeclaringType?.FullName}: {exception.Message}");
		}
	}

	public static void Dispose()
	{
		LogManager.Info($"[MethodHookPattern] Disposing...");

		foreach(var hook in ActiveHooks)
		{
			hook.Dispose();
		}

		ActiveHooks.Clear();

		LogManager.Info($"[MethodHookPattern] Disposing!");
	}
}