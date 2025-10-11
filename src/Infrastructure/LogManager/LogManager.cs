using REFrameworkNET;

namespace YURI_Overlay;

internal static class LogManager
{
	public static void Info(object value)
	{
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		API.LogInfo($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
		Console.ResetColor();
	}

	public static void Warn(object value)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		API.LogWarning($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
		Console.ResetColor();
	}

	public static void Error(object value)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		API.LogError($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
		Console.ResetColor();
	}

	public static void Debug(object value)
	{
#if DEBUG
		Console.ForegroundColor = ConsoleColor.Blue;
		API.LogInfo($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
		Console.ResetColor();
#endif
	}
}