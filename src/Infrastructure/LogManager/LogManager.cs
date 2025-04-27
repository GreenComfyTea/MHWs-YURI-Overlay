using REFrameworkNET;

namespace YURI_Overlay;

internal static class LogManager
{
	public static void Info(object value)
	{
		API.LogInfo($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
	}

	public static void Warn(object value)
	{
		API.LogWarning($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
	}

	public static void Error(object value)
	{
		API.LogError($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
	}

	public static void Debug(object value)
	{
		API.LogInfo($"[{DateTime.Now:HH:mm:ss.fff}] [{Constants.ModName}] {value}");
	}
}