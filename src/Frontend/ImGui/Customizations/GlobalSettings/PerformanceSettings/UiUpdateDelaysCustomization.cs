using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class UiUpdateDelaysCustomization
{
	public float? LargeMonsterDynamic;
	public float? LargeMonsterStatic;
	public float? LargeMonsterTargeted;
	public float? LargeMonsterMapPin;
	public float? SmallMonsters;
	public float? EndemicLife;
	public float? DamageMeter;

	public bool RenderImGui(string? parentName = "", UiUpdateDelaysCustomization? defaultCustomization = null)
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-uis";

		if(ImGuiHelper.ResettableTreeNode(localization.UIs, customizationName, ref isChanged, defaultCustomization, this.Reset))
		{
			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.SmallMonsters}##{customizationName}",
				ref this.SmallMonsters,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.SmallMonsters
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.LargeMonstersDynamic}##{customizationName}",
				ref this.LargeMonsterDynamic,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.LargeMonsterDynamic
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.LargeMonstersStatic}##{customizationName}",
				ref this.LargeMonsterStatic,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.LargeMonsterStatic
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.LargeMonstersTargeted}##{customizationName}",
				ref this.LargeMonsterTargeted,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.LargeMonsterTargeted
			);

			isChanged |= ImGuiHelper.ResettableDragFloat(
				$"{localization.LargeMonstersMapPin}##{customizationName}",
				ref this.LargeMonsterMapPin,
				0.001f,
				0.001f,
				10f,
				"%.3f",
				defaultCustomization?.LargeMonsterMapPin
			);

			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.EndemicLife}##{customizationName}", ref this.EndemicLife, 0.001f, 0.001f, 10f, "%.3f",
				defaultCustomization?.EndemicLife);

			isChanged |= ImGuiHelper.ResettableDragFloat($"{localization.DamageMeterUI}##{customizationName}", ref this.DamageMeter, 0.001f, 0.001f, 10f, "%.3f",
				defaultCustomization?.DamageMeter);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public void Reset(UiUpdateDelaysCustomization? defaultCustomization = null)
	{
		if(defaultCustomization is null)
		{
			return;
		}

		this.SmallMonsters = defaultCustomization.SmallMonsters;
		this.LargeMonsterDynamic = defaultCustomization.LargeMonsterDynamic;
		this.LargeMonsterStatic = defaultCustomization.LargeMonsterStatic;
		this.LargeMonsterTargeted = defaultCustomization.LargeMonsterTargeted;
		this.LargeMonsterMapPin = defaultCustomization.LargeMonsterMapPin;
		this.EndemicLife = defaultCustomization.EndemicLife;
		this.DamageMeter = defaultCustomization.DamageMeter;
	}
}