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

		configManager.ActiveConfigChanged += OnAnyConfigChanged;
		configManager.AnyConfigChanged += OnAnyConfigChanged;

		OnAnyConfigChanged(configManager, EventArgs.Empty);
	}

	public bool RenderImGui(string? parentName = "")
	{
		var configManager = ConfigManager.Instance;
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{localization.Config}##{parentName}"))
		{
			var isActiveConfigChanged = ImGuiHelper.Combo(localization.ActiveConfig, ref _activeConfigIndex, _configNames);
			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.ActivateConfig(_configNames[_activeConfigIndex]);
			}

			ImGui.InputText($"{localization.NewConfigName}##{parentName}", ref _configNameInput, Constants.MaxConfigNameLength);

			if(ImGui.Button($"{localization.New}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.NewConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.Duplicate}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.DuplicateConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.Rename}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.RenameConfig(_configNameInput);
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

		_configNames = configManager.Configs.Values.Select(config => config.Name).ToArray();
		_activeConfigIndex = Array.IndexOf(_configNames, configManager.ActiveConfig.Name);
	}
}