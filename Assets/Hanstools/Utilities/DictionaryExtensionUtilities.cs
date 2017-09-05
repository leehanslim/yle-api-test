using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace Hanstools.Extensions
{
	/// <summary>
	/// A bunch of dictionary extension methods that help retrieve data deserialized into objects.
	/// </summary>
	public static class DictionaryExtensionUtilities
	{
		#region Data retrieval methods
		public static T GetValue<T>(this IDictionary<string, object> source, string key)
		{
			if (source == null || !source.ContainsKey(key))
				return default(T);

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

		public static IDictionary<string, object> GetHash(this IDictionary<string, object> source, string key)
		{
			if (source == null || !source.ContainsKey(key))
				return null;

			Dictionary<string, object> dict = (Dictionary<string, object>)System.Convert.ChangeType(source[key], typeof(Dictionary<string, object>));
			return dict;
		}

		public static IDictionary<string, object>[] GetHashCollection(this IDictionary<string, object> source, string key)
		{
			if (source == null || !source.ContainsKey(key))
				return null;

			try
			{
				Dictionary<string, object>[] dictArr = (Dictionary<string, object>[])System.Convert.ChangeType(source[key], typeof(Dictionary<string, object>[]));
				return dictArr;
			}
			catch (System.Exception e)
			{
				Debug.LogWarning("Exception encountered: " + e.ToString());
				return null;
			}
		}

		public static IDictionary<string, object> GetCollectionElementAt(this IDictionary<string, object> source, string key, int index)
		{
			if (source == null || !source.ContainsKey(key))
				return null;
			
			Dictionary<string, object>[] dictArr = (Dictionary<string, object>[])System.Convert.ChangeType(source[key], typeof(Dictionary<string, object>[]));

			if (dictArr == null)
				ThrowExceptionConversionError();

			if (index >= dictArr.Length)
				ThrowExceptionIfIndexOutOfRange();

			return dictArr[index];
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
	}
}
