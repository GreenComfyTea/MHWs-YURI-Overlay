using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class UiUpdateDelaysCustomization
{
	public float LargeMonsterDynamic = 0.1f;
	public float LargeMonsterStatic = 0.1f;
	public float LargeMonsterTargeted = 0.1f;
	public float LargeMonsterMapPin = 0.1f;
	public float SmallMonsters = 0.2f;
	public float EndemicLife = 0.2f;
	public float DamageMeter = 0.1f;


	public bool RenderImGui(string parentName = "", UiUpdateDelaysCustomization defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-uis";

		if(ImGuiHelper.ResettableTreeNode(localization.UIs, customizationName, ref isChanged, defaultCustomization, Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.SmallMonsters}##{customizationName}", ref SmallMonsters, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.SmallMonsters);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.LargeMonstersDynamic}##{customizationName}", ref LargeMonsterDynamic, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.LargeMonsterDynamic);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.LargeMonstersStatic}##{customizationName}", ref LargeMonsterStatic, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.LargeMonsterStatic);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.LargeMonstersTargeted}##{customizationName}", ref LargeMonsterTargeted, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.LargeMonsterTargeted);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.LargeMonstersMapPin}##{customizationName}", ref LargeMonsterMapPin, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.LargeMonsterMapPin);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.EndemicLife}##{customizationName}", ref EndemicLife, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.EndemicLife);
			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.DamageMeterUI}##{customizationName}", ref DamageMeter, 0.001f, 0.001f, 10f, "%.3f", defaultCustomization?.DamageMeter);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(UiUpdateDelaysCustomization defaultCustomization = null)
	{
		if(defaultCustomization is null) return;

		SmallMonsters = defaultCustomization.SmallMonsters;
		LargeMonsterDynamic = defaultCustomization.LargeMonsterDynamic;
		LargeMonsterStatic = defaultCustomization.LargeMonsterStatic;
		LargeMonsterTargeted = defaultCustomization.LargeMonsterTargeted;
		LargeMonsterMapPin = defaultCustomization.LargeMonsterMapPin;
		EndemicLife = defaultCustomization.EndemicLife;
		DamageMeter = defaultCustomization.DamageMeter;
	}
}