using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Web;

namespace Hanstools.Yle
{
	public class YleSDKManager : MonoBehaviour 
	{
		[SerializeField]
		private string appID;

		[SerializeField]
		private string appKey;

		private const string programsURL = "https://external.api.yle.fi/v1/programs/items.json";

		private static string programsEndpoint;

		void Awake()
		{
			DontDestroyOnLoad(gameObject);
			programsEndpoint = string.Format(programsURL + "?app_id={0}&app_key={1}", appID, appKey);
			Debug.Log("<color=blue>YleSDKManager | Programs Endpoint: \'" + programsEndpoint + "\'</color>");
		}

		public static void GetProgramsList(System.Action<IList<ProgramData>> onComplete, int offset, int limit = 10)
		{
			string formattedURL = RequestBuilder.AddParametersToURL(programsEndpoint, new Dictionary<string, string>() { 
				{"offset", offset.ToString()}, 
				{"limit", limit.ToString()} 
			});

			RequestBuilder.Inst.NewWebRequest(formattedURL, HttpMethod.GET, delegate(WebResponse response) {
				Debug.Log("<color=purple>YleSDKManager.GetProgramsList | Success:" + response.Success + ", StatusCode:" + response.StatusCode.ToString() + ", RawMessage:" + response.RawMessage + "</color>");
				if (!response.Success)
				{
					Debug.Log("<color=yellow>YleSDKManager.GetProgramsList | Error:" + response.GetResponseMap().GetValue<string>("error") + "</color>");
				}
				else
				{
					Debug.Log("<color=purple>YleSDKManager.GetProgramsList | Api:" + response.GetResponseMap().GetValue<string>("apiVersion") + "</color>");

					IList<ProgramData> returnVal = new List<ProgramData>();
					IDictionary<string, object>[] dataMapCollection = response.GetResponseMap().GetCollection("data");
					for (int i = 0; i < dataMapCollection.Length; i++)
					{
						ProgramData pd = new ProgramData(dataMapCollection[i]);
						returnVal.Add(pd);
					}

					if (onComplete != null)
						onComplete(returnVal);
				}
			});
		}
	}
}
