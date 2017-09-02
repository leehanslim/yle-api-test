using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanstools.Web
{
	/// <summary>
	/// Web response object. Easy-to-access information on response received from web.
	/// </summary>
	[System.Serializable]
	public class WebResponse
	{
		[SerializeField]
		private Dictionary<string, object> responseMap;

		public System.Net.HttpStatusCode StatusCode
		{
			get;
			private set;
		}

		public bool Success
		{
			get;
			private set;
		}

		public string RawMessage
		{
			get;
			private set;
		}

		public void SetStatusCode(System.Net.HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		public void SetSuccessFlag(bool success)
		{
			this.Success = success;
		}

		public void SetMappingManually(Dictionary<string, object> map)
		{
			this.responseMap = map;
		}

		public void SetRawMessage(string rawMessage)
		{
			this.RawMessage = rawMessage;
		}

		/// <summary>
		/// Returns object specified by 'key' of type specified by 'T'. If response message is a JSON object, this traverses root level only.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Get<T>(string key)
		{
			if (responseMap == null || !responseMap.ContainsKey(key)) return default(T);

			try
			{
				object returnValue = responseMap[key];
				return (T)System.Convert.ChangeType(returnValue, typeof(T));
			}
			catch
			{
				return default(T);
			}
		}
	}
}
