using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

using ImGuiNET;

namespace YURI_Overlay;

internal static class Utils
{
	public static bool IsApproximatelyEqual(float a, float b)
	{
		return Math.Abs(a - b) <= Constants.Epsilon;
	}

	public static void OpenLink(string url)
	{
		try
		{
			Process.Start(url);
		}
		catch(Exception exception)
		{
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo(url) { FileName = url, UseShellExecute = true });
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
			else
			{
				LogManager.Warn(exception);
			}
		}
	}

	public static void ImGuiEndRect(float additionalSize = 0f, float rounding = -1f, float thickness = 1f)
	{
		ImGui.EndGroup();

		var actualRounding = rounding < 0f ? ImGui.GetStyle().FrameRounding : rounding;

		var itemRectMin = ImGui.GetItemRectMin();
		itemRectMin.X -= additionalSize;
		itemRectMin.Y -= additionalSize;

		var itemRectMax = ImGui.GetItemRectMax();
		itemRectMax.X += additionalSize;
		itemRectMax.Y += additionalSize;

		ImGui.GetWindowDrawList().AddRect(itemRectMin, itemRectMax, ImGui.GetColorU32(ImGuiCol.Border), actualRounding, ImDrawFlags.RoundCornersAll, thickness);
		ImGui.NewLine();
	}

	public static void EmitEvents(object sender, EventHandler eventHandler)
	{
		foreach(var subscriber in eventHandler.GetInvocationList())
		{
			try
			{
				subscriber.DynamicInvoke(sender, EventArgs.Empty);
			}
			catch(Exception exception)
			{
				LogManager.Error(exception);
			}
		}
	}

	public static string Stringify<T>(T value)
	{
		try
		{
			return JsonSerializer.Serialize(value, Constants.JsonSerializerOptionsInstance);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return string.Empty;
		}
	}

	public static MemoryStream StringToStream(string str)
	{
		MemoryStream stream = new();
		StreamWriter writer = new(stream);
		writer.Write(str);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}

	public static uint AbgrToRgba(uint argb)
	{
		var red = argb & 0x000000FF;
		var green = (argb & 0x0000FF00) >> 8;
		var blue = argb & (0x00FF0000 >> 16);
		var alpha = (argb & 0xFF000000) >> 24;

		return (red << 24) | (green << 16) | (blue << 8) | alpha;
	}

	public static uint RgbaToAbgr(uint rgba)
	{
		var red = (rgba & 0xFF000000) >> 24;
		var green = (rgba & 0x00FF0000) >> 16;
		var blue = (rgba & 0x0000FF00) >> 8;
		var alpha = rgba & 0x000000FF;
		return (alpha << 24) | (blue << 16) | (green << 8) | red;
	}

	public static uint ScaleColorOpacityAbgr(uint colorAbgr, float opacity)
	{
		var red = (colorAbgr & 0x000000FF) >> 0;
		var green = (colorAbgr & 0x0000FF00) >> 8;
		var blue = (colorAbgr & 0x00FF0000) >> 16;
		var alpha = (colorAbgr & 0xFF000000) >> 24;

		alpha = (uint) (alpha * opacity);

		return (alpha << 24) | (blue << 16) | (green << 8) | red;
	}

	public static string FormatTimer(float seconds, float maxSeconds)
	{
		var totalSeconds = (int) Math.Round(Math.Clamp(seconds, 0f, maxSeconds));

		var minutes = totalSeconds / 60;
		var remainingSeconds = totalSeconds % 60;
		return $"{minutes}:{remainingSeconds:F2}";
	}
}
