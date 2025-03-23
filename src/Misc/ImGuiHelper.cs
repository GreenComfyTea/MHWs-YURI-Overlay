using ImGuiNET;

namespace YURI_Overlay;

internal static class ImGuiHelper
{
	public static bool Combo(string label, ref int currentItem, string[] items)
	{
		return ImGui.Combo(label, ref currentItem, items, items.Length);
	}
}
