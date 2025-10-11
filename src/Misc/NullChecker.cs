using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class NullChecker
{
	public static void ValidateConfig()
	{
		LogManager.Debug("Validating default config...");

		var config = new Config();
		ConfigManager.ResetToDefault(config);

		var isValid = Validate(config);

		if(isValid)
		{
			LogManager.Debug("Default config is valid!");
		}
		else
		{
			LogManager.Error("Default config is invalid!");
		}
	}

	public static bool Validate(object obj, string path = "")
	{
		var type = obj.GetType();

		// Avoid checking primitive types, strings, and enums
		if(type.IsPrimitive || type == typeof(string) || type.IsEnum || type is { IsValueType: true, IsClass: false })
		{
			return true;
		}

		var isValid = true;

		// Handle collections (arrays, lists, etc.)
		if(obj is IEnumerable enumerable and not string) // string is IEnumerable<char>, but we treat it as a primitive
		{
			var index = 0;
			foreach(var item in enumerable)
			{
				isValid &= Validate(item, $"{path}[{index}]");
				index++;
			}

			return isValid;
		}

		// Get all public instance properties
		var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		foreach(var property in properties)
		{
			// Skip indexer properties (e.g., this[int i])
			if(property.GetIndexParameters().Length > 0)
			{
				continue;
			}

			// Get the property value
			object? propertyValue = null;

			try
			{
				propertyValue = property.GetValue(obj);
			}
			catch(TargetParameterCountException)
			{
				// This can happen if the property is an indexed property without arguments
				continue;
			}
			catch(Exception ex)
			{
				LogManager.Error($"Error getting value for property {path}.{property.Name}: {ex.Message}");
				isValid = false;
				continue;
			}


			var currentPath = string.IsNullOrEmpty(path) ? property.Name : $"{path}.{property.Name}";

			if(propertyValue is null)
			{
				LogManager.Debug($"{currentPath} is null.");
				isValid = false;
			}
			else
			{
				// Recursively check nested objects
				// Ensure it's a reference type and not a string or a value type that isn't nullable itself
				if(property.PropertyType.IsClass && property.PropertyType != typeof(string))
				{
					isValid &= Validate(propertyValue, currentPath);
				}
			}
		}

		// Optionally, you can also check fields, although properties are more common in modern C#
		var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
		foreach(var field in fields)
		{
			object? fieldValue = null;
			try
			{
				fieldValue = field.GetValue(obj);
			}
			catch(Exception ex)
			{
				LogManager.Error($"Error getting value for field {path}.{field.Name}: {ex.Message}");
				isValid = false;
				continue;
			}

			var currentPath = string.IsNullOrEmpty(path) ? field.Name : $"{path}.{field.Name}";

			if(fieldValue is null)
			{
				LogManager.Debug($"{currentPath} is null.");
				isValid = false;
			}
			else
			{
				if(field.FieldType.IsClass && field.FieldType != typeof(string))
				{
					isValid &= Validate(fieldValue, currentPath);
				}
			}
		}

		return isValid;
	}
}