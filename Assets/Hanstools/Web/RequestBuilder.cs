using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hanstools.Web
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
		
			DontDestroyOnLoad(gameObject);
		}

		#region Request builder methods
		/// <summary>
		/// Starts a coroutine that processes a web request to the specified endpoint. Invokes the WebResponseDelegate object (if supplied)
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="httpMethod">Http method.</param>
		/// <param name="onComplete">On complete.</param>
		public void NewWebRequest(string url, HttpMethod httpMethod = HttpMethod.GET, WebResponseDelegate onComplete = null)
		{
			Debug.Log("<color=yellow>RequestBuilder.NewWebRequest | New web request for URL: \'" + url + "\'</color>");
			switch (httpMethod)
			{
				case HttpMethod.GET:
					StartCoroutine(GETWebRequestRoutine(url, onComplete));
				break;
				default:
					Debug.Log("<color=yellow>RequestBuilder.NewWebRequest | No Coroutine defined for \'" + httpMethod.ToString() + "\' method yet.</color>");
				break;
			}
		}

		/// <summary>
		/// Simple coroutine for handling GET requests to the server
		/// </summary>
		/// <returns>The web request routine.</returns>
		/// <param name="url">URL.</param>
		/// <param name="onComplete">On complete.</param>
		private IEnumerator GETWebRequestRoutine(string url, WebResponseDelegate onComplete = null)
		{
			yield return null;

			UnityWebRequest request = UnityWebRequest.Get(url);

			yield return request.Send();

			// Just to be safe, we make sure the request really has completed before proceeding
			while (!request.isDone) yield return null;

			WebResponse response = new WebResponse();

			if (request.isError)
			{
				response.SetSuccessFlag(false);
				response.SetResponseMap( new Dictionary<string, object>() { {"error", request.error} } );
				response.SetRawMessage(request.downloadHandler.text);
			}
			else
			{
				// Try to deserialize the response text into a simple dictionary 
				try
				{
					Dictionary<string, object> responseTable = JsonFx.Json.JsonReader.Deserialize<Dictionary<string, object>>(request.downloadHandler.text);
					response.SetResponseMap(responseTable);
					response.SetRawMessage(request.downloadHandler.text);
					response.SetSuccessFlag(true);
				}
				catch
				{
					response.SetResponseMap( new Dictionary<string, object>() { {"error", "deserialization failed"} } );
					response.SetRawMessage(request.downloadHandler.text);
					response.SetSuccessFlag(false);
				}
			}

			response.SetStatusCode(ConvertNumToCode(request.responseCode));

			if (onComplete != null)
			{
				onComplete(response);
			}
		}
			
		public static string AddParametersToURL(string baseURL, Dictionary<string, string> parameterPool)
		{
			if (baseURL == null) 
				return null;
			else
			{
				string formattedURL = baseURL;
				foreach (KeyValuePair<string, string> kvp in parameterPool)
				{
					formattedURL += string.Format("&{0}={1}", kvp.Key, kvp.Value);
				}
				return formattedURL;
			}
		}

		public static string AddParameterToURL(string baseURL, string key, string value)
		{
			if (baseURL == null)
				return null;
			else
			{
				string formattedURL = baseURL;
				formattedURL += string.Format("&{0}={1}", key, value);
				return formattedURL;
			}
		}
		#endregion // Request builder methods


		#region Helpers
		private System.Net.HttpStatusCode ConvertNumToCode(long num)
		{
			return (System.Net.HttpStatusCode)num;
		}
		#endregion // Helpers
	}
}
