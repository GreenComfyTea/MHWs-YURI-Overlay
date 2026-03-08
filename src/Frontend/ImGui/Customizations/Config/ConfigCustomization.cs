using Hexa.NET.ImGui;

namespace YURI_Overlay;

internal sealed class ConfigCustomization : Customization
{
	private int _activeConfigIndex;
	private string _configNameInput = string.Empty;

	private string[] _configNames = [];

	public ConfigCustomization(bool stub)
	{
	}

	public ConfigCustomization()
	{
		var configManager = ConfigManager.Instance;

		configManager.ActiveConfigChanged += this.OnAnyConfigChanged;
		configManager.AnyConfigChanged += this.OnAnyConfigChanged;

		this.OnAnyConfigChanged(configManager, EventArgs.Empty);
	}

	public bool RenderImGui(string? parentName = "")
	{
		var configManager = ConfigManager.Instance;
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{localization.Config}##{parentName}"))
		{
			var isActiveConfigChanged = ImGuiHelper.Combo(localization.ActiveConfig, ref this._activeConfigIndex, this._configNames);

			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.ActivateConfig(this._configNames[this._activeConfigIndex]);
			}

			ImGui.InputText($"{localization.NewConfigName}##{parentName}", ref this._configNameInput, Constants.MaxConfigNameLength);

			if(ImGui.Button($"{localization.New}##{parentName}"))
			{
				if(this._configNameInput != string.Empty && !this._configNames.Contains(this._configNameInput))
				{
					isChanged = true;

					configManager.NewConfig(this._configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.Duplicate}##{parentName}"))
			{
				if(this._configNameInput != string.Empty && !this._configNames.Contains(this._configNameInput))
				{
					isChanged = true;

					configManager.DuplicateConfig(this._configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.Rename}##{parentName}"))
			{
				if(this._configNameInput != string.Empty && !this._configNames.Contains(this._configNameInput))
				{
					isChanged = true;

					configManager.RenameConfig(this._configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.Reset}##{parentName}"))
			{
				isChanged = true;

				configManager.ResetConfig();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	private void OnAnyConfigChanged(object? sender, EventArgs eventArgs)
	{
		var configManager = ConfigManager.Instance;

		this._configNames = configManager.Configs.Values.Select(config => config.Name).ToArray();
		this._activeConfigIndex = Array.IndexOf(this._configNames, configManager.ActiveConfig.Name);
	}
}