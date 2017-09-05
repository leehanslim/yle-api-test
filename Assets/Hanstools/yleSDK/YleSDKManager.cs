using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Web;
using Hanstools.Extensions;

namespace Hanstools.Yle
{
	/// <summary>
	/// YLE SDK manager for serving basic YLE-related services. Acts as a client-side interface for the YLE backend system.
	/// </summary>
	public class YleSDKManager : MonoBehaviour 
	{
		private const string programsURL = "https://external.api.yle.fi/v1/programs/items.json";
		private const string categoriesURL = "https://external.api.yle.fi/v1/programs/categories.json";

		[SerializeField]
		private string appID;

		[SerializeField]
		private string appKey;

		private static string programsEndpoint;
		private static string categoriesEndpoint;
		private static IList<CategoryData> categories;
		private static YleSDKManager monoRef;

		public static bool Initialized { get; private set; }

		void Awake()
		{
			monoRef = this;
			DontDestroyOnLoad(this);

			programsEndpoint = RequestBuilder.AddParametersToURL(programsURL + "?", new Dictionary<string, string>(){
				{ "app_id", appID },
				{ "app_key", appKey }
			});

			categoriesEndpoint = RequestBuilder.AddParametersToURL(categoriesURL + "?", new Dictionary<string, string>(){
				{ "app_id", appID },
				{ "app_key", appKey }
			});

			//Debug.Log("<color=blue>YleSDKManager | Programs Endpoint: \'" + programsEndpoint + "\'</color>");
			//Debug.Log("<color=blue>YleSDKManager | Categories Endpoint: \'" + categoriesEndpoint + "\'</color>");
		}

		/// <summary>
		/// Initializes the YLE SDK Manager. Call this before anything else.
		/// </summary>
		/// <param name="onInitComplete">On init complete.</param>
		public static void Init(System.Action<bool> onInitComplete = null)
		{
			if (Initialized) 
				return;
			
			monoRef.StartCoroutine(monoRef.InitRoutine(onInitComplete));
		}

		/// <summary>
		/// Gets the programs list specified by a couple of optional parameters.
		/// </summary>
		/// <param name="onComplete">On complete.</param>
		/// <param name="offset">Offset.</param>
		/// <param name="limit">Limit.</param>
		/// <param name="categoryKey">Category key.</param>
		/// <param name="programType">Program type.</param>
		/// <param name="mediaObjectType">Media object type.</param>
		public static void GetProgramsList(System.Action<IList<ProgramData>> onComplete, int offset=0, int limit = 10, string categoryKey=null, ProgramType programType=ProgramType.all, MediaObjectType mediaObjectType=MediaObjectType.all)
		{
			if (!Initialized)
			{
				if (onComplete != null) 
					onComplete(null);
				
				return;
			}

			string formattedURL = RequestBuilder.AddParametersToURL(programsEndpoint, new Dictionary<string, string>() {
				{"offset", offset.ToString()}, 
				{"limit", limit.ToString()} 
			});

			string categoryID = GetCategoryCode(categoryKey);
			if (!string.IsNullOrEmpty(categoryID)) 
			{
				formattedURL = RequestBuilder.AddParameterToURL(formattedURL, "category", System.Uri.EscapeDataString(categoryID));
			}

			if (programType != ProgramType.all) 
			{
				formattedURL = RequestBuilder.AddParameterToURL(formattedURL, "type", programType.ToString());
			}

			if (mediaObjectType != MediaObjectType.all) 
			{
				formattedURL = RequestBuilder.AddParameterToURL(formattedURL, "mediaobject", mediaObjectType.ToString());
			}

			RequestBuilder.Inst.NewWebRequest(formattedURL, HttpMethod.GET, delegate(WebResponse response) {
				if (!response.Success)
				{
					Debug.Log("<color=yellow>YleSDKManager.GetProgramsList | Error:" + response.GetResponseMap().GetValue<string>("error") + "</color>");
				}
				else
				{
					Debug.Log("<color=purple>YleSDKManager.GetProgramsList | Api:" + response.GetResponseMap().GetValue<string>("apiVersion") + "</color>");

					IList<ProgramData> returnVal = new List<ProgramData>();
					IDictionary<string, object>[] dataMapCollection = response.GetResponseMap().GetHashCollection("data");

					if (dataMapCollection != null)
					{
						for (int i = 0; i < dataMapCollection.Length; i++)
						{
							ProgramData pd = new ProgramData(dataMapCollection[i]);
							returnVal.Add(pd);
						}
					}

					if (onComplete != null)
						onComplete(returnVal);
				}
			});
		}

		#region Coroutines
		private IEnumerator InitRoutine(System.Action<bool> onComplete = null)
		{
			bool requestCompleted = false;
			Initialized = false;
			RequestBuilder.Inst.NewWebRequest(categoriesEndpoint, HttpMethod.GET, ((WebResponse response) => {

				if (!response.Success)
				{
					Debug.Log("<color=yellow>YleSDKManager.InitRoutine | Error:" + response.GetResponseMap().GetValue<string>("error") + "</color>");
				}
				else
				{
					Debug.Log("<color=purple>YleSDKManager.InitRoutine | Api:" + response.GetResponseMap().GetValue<string>("apiVersion") + "</color>");
					categories = new List<CategoryData>();
					IDictionary<string, object>[] dataMapCollection = response.GetResponseMap().GetHashCollection("data");

					if (dataMapCollection != null)
					{
						for (int i = 0; i < dataMapCollection.Length; i++)
						{
							CategoryData pd = new CategoryData(dataMapCollection[i]);
							categories.Add(pd);
						}
					}

					Initialized = true;
				}
				requestCompleted = true;
			}));

			while (!requestCompleted) 
				yield return null;

			yield return null;

			if (onComplete != null)
				onComplete(Initialized);
		}
		#endregion // Coroutines

		#region Helpers
		private static string GetCategoryCode(string searchKey)
		{
			if (categories == null || categories.Count <= 0)
				return null;

			CategoryData category = categories.FirstOrDefault(item => item.ID == searchKey || item.Title == searchKey);
			return category != null ? category.ID : string.Empty;
		}
		#endregion // Helpers
	}
}
