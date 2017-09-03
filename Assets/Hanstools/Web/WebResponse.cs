using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanstools.Web
{
	/// <summary>
	/// Web response object. Easy-to-access information on response received from web.
	/// </summary>
	public class WebResponse
	{
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

		public void SetResponseMap(Dictionary<string, object> map)
		{
			this.responseMap = map;
		}

		public void SetRawMessage(string rawMessage)
		{
			this.RawMessage = rawMessage;
		}

		public Dictionary<string, object> GetResponseMap()
		{
			return this.responseMap;
		}
	}
}
