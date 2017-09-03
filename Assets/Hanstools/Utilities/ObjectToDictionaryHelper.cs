using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace Hanstools
{
	/* *
	 * Not mine. This one, I got from StackOverflow
	 * */
	public static class ObjectToDictionaryHelper
	{
		public static IDictionary<string, object> ToDictionary(this object source)
		{
			return source.ToDictionary<object>();
		}

		public static IDictionary<string, T> ToDictionary<T>(this object source)
		{
			if (source == null)
				ThrowExceptionWhenSourceArgumentIsNull();

			var dictionary = new Dictionary<string, T>();
			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
				AddPropertyToDictionary<T>(property, source, dictionary);
			return dictionary;
		}

		#region Data retrieval methods
		public static T GetValue<T>(this IDictionary<string, object> source, string key)
		{
			if (source == null || !source.ContainsKey(key)) 
				ThrowExceptionWhenKeyNonExistent();

			try
			{
				object returnValue = source[key];
				return (T)System.Convert.ChangeType(returnValue, typeof(T));
			}
			catch
			{
				return default(T);
			}
		}

		public static IDictionary<string, object>[] GetCollection(this IDictionary<string, object> source, string key)
		{
			if (!source.ContainsKey(key))
				ThrowExceptionWhenKeyNonExistent();

			Dictionary<string, object>[] dictArr = (Dictionary<string, object>[])System.Convert.ChangeType(source[key], typeof(Dictionary<string, object>[]));

			return dictArr;
		}

		public static IDictionary<string, object> GetArrayElementAt(this IDictionary<string, object> source, string key, int index)
		{
			if (!source.ContainsKey(key))
				ThrowExceptionWhenKeyNonExistent();
			
			Dictionary<string, object>[] dictArr = (Dictionary<string, object>[])System.Convert.ChangeType(source[key], typeof(Dictionary<string, object>[]));

			if (dictArr == null)
				ThrowExceptionConversionError();

			if (index >= dictArr.Length)
				ThrowExceptionIfIndexOutOfRange();

			return dictArr[index];
		}

		private static void ThrowExceptionWhenKeyNonExistent()
		{
			throw new System.ArgumentException("source", "Key does not exist");
		}

		private static void ThrowExceptionIfIndexOutOfRange()
		{
			throw new System.ArgumentOutOfRangeException("index", "Index out of range");
		}

		private static void ThrowExceptionConversionError()
		{
			throw new System.Exception("Object to array conversion failed");
		}
		#endregion // Data retrieval methods

		private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
		{
			object value = property.GetValue(source);
			if (IsOfType<T>(value))
				dictionary.Add(property.Name, (T)value);
		}

		private static bool IsOfType<T>(object value)
		{
			return value is T;
		}

		private static void ThrowExceptionWhenSourceArgumentIsNull()
		{
			throw new System.ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
		}
	}
}
