using System.Reflection;

namespace YURI_Overlay;

internal partial class ConfigManager
{
	public static Config MergeConfig(Config destinationConfig, Config sourceConfig)
	{
		return Merge(destinationConfig, sourceConfig) ?? destinationConfig;
	}

	/// <summary>
	/// Merges null properties and fields from destination into source,
	/// handling deep nesting. If a nested object in 'source' is null,
	/// it will be replaced entirely by a deep copy of the corresponding object from 'destination'.
	/// If a nested object in 'source' is NOT null, its own null properties/fields
	/// will be filled from the corresponding 'destination' nested object.
	/// Strings are treated as value types for the purpose of null checks.
	/// </summary>
	/// <typeparam name="T">The type of the configuration object.</typeparam>
	/// <param name="source">The object to be updated (e.g., user-provided configuration), potentially with nulls.</param>
	/// <param name="destination">The object to take values from (e.g., default configuration) to fill nulls in source.</param>
	/// <returns>The modified source object with nulls filled, or null if both source and destination were null.</returns>
	public static T? Merge<T>(T? source, T? destination) where T : class
	{
		// If source itself is null, replace it entirely with a deep copy of destination
		if(source is null)
		{
			// If destination is also null, nothing to copy, return null
			if(destination is null) return null;

			// Create a new instance of T and deep copy destination's contents into it
			// Activator.CreateInstance returns object?, so we cast it to T and assure it's not null here.
			var newSource = (T) Activator.CreateInstance(typeof(T))!;

			// Recursively merge from destination to this newSource.
			// Since newSource is fresh, all its properties/fields will be default (often null for reference types),
			// effectively making it a deep copy of destination.
			return Merge(newSource, destination);
		}

		// If destination is null, there's nothing to fill from, so return source as is
		// Source is guaranteed not null here due to the first if block
		if(destination is null)
		{
			return source;
		}

		// Get all public instance properties
		var properties = typeof(T)
						 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
						 .Where(p => p.CanRead && p.CanWrite)
						 .ToArray();

		// Get all public instance fields
		var fields = typeof(T)
					 .GetFields(BindingFlags.Public | BindingFlags.Instance)
					 .ToArray();

		// --- Process Properties ---
		foreach(var property in properties)
		{
			// GetValue returns object?, so assign to var.
			var sourceValue = property.GetValue(source);
			var destinationValue = property.GetValue(destination);

			// Handle non-class types and strings (direct replacement if null)
			if(!property.PropertyType.IsClass || property.PropertyType == typeof(string))
			{
				if(sourceValue is null && destinationValue is not null)
				{
					property.SetValue(source, destinationValue); // Value types and strings are fine with direct assignment
				}
			}
			// Handle nested class types (recursive call for deeper merging/copying)
			else
			{
				// If the nested object in source is null, create a deep copy from destination
				if(sourceValue is null && destinationValue is not null)
				{
					// Create a new instance of the nested type. Ensure it's not null before passing to Merge.
					var newNestedInstance = Activator.CreateInstance(property.PropertyType)!;

					// The recursive Merge call will return T?, but we expect a non-null result here
					// because newNestedInstance is not null and destinationValue is not null.
					var copiedNestedObject = typeof(ConfigManager)
											 .GetMethod(nameof(Merge))
											 ?.MakeGenericMethod(property.PropertyType)
											 .Invoke(null, new object?[] { newNestedInstance, destinationValue }); // object?[] for potentially null arguments

					property.SetValue(source, copiedNestedObject);
				}
				// If the nested object in source is NOT null, recursively merge into it
				else if(sourceValue is not null && destinationValue is not null)
				{
					// Call Merge recursively for the nested object.
					// The result of Invoke here is ignored as sourceValue is modified in-place.
					typeof(ConfigManager)
						.GetMethod(nameof(Merge))
						?.MakeGenericMethod(property.PropertyType)
						.Invoke(null, new object?[] { sourceValue, destinationValue });
				}
			}
		}

		// --- Process Fields ---
		foreach(var field in fields)
		{
			var sourceValue = field.GetValue(source);
			var destinationValue = field.GetValue(destination);

			// Handle non-class types and strings (direct replacement if null)
			if(!field.FieldType.IsClass || field.FieldType == typeof(string))
			{
				if(sourceValue is null && destinationValue is not null)
				{
					field.SetValue(source, destinationValue); // Value types and strings are fine with direct assignment
				}
			}
			// Handle nested class types (recursive call for deeper merging/copying)
			else
			{
				// If the nested object in source is null, create a deep copy from destination
				if(sourceValue is null && destinationValue is not null)
				{
					var newNestedInstance = Activator.CreateInstance(field.FieldType)!;

					var copiedNestedObject = typeof(ConfigManager)
											 .GetMethod(nameof(Merge))
											 ?.MakeGenericMethod(field.FieldType)
											 .Invoke(null, new object?[] { newNestedInstance, destinationValue });

					field.SetValue(source, copiedNestedObject);
				}
				// If the nested object in source is NOT null, recursively merge into it
				else if(sourceValue is not null && destinationValue is not null)
				{
					typeof(ConfigManager)
						.GetMethod(nameof(Merge))
						?.MakeGenericMethod(field.FieldType)
						.Invoke(null, new object?[] { sourceValue, destinationValue });
				}
			}
		}

		return source; // source is guaranteed not null at this point
	}
}