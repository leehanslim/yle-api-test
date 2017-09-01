using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanstools
{
	/// <summary>
	/// Simple utility for handling web requests
	/// </summary>
	public class RequestBuilder : MonoBehaviour 
	{
		private static RequestBuilder singleton;
		public static RequestBuilder Inst 
		{ 
			get
			{
				if (singleton == null)
				{
					GameObject go = new GameObject("RequestBuilder");
					singleton = go.AddComponent<RequestBuilder>();
				}

				return singleton;
			}
		}

		void Awake()
		{
			if (singleton == null) 
				singleton = this;
		}

		#region Request builder methods
		public void NewWebRequest(string url, string httpMethod, System.Action onComplete)
		{
			
		}

		private IEnumerator 
		#endregion // Request builder methods
	}
}
