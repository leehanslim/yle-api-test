using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Extensions;

namespace Hanstools.Yle
{
	/// <summary>
	/// Data object for holding and serving program-related information. 
	/// Uses Dictionary extension methods to mine most of the information.
	/// </summary>
	public class ProgramData 
	{
		private IDictionary<string, object> dataMap;

		public ProgramData(IDictionary<string, object> dataMap)
		{
			this.dataMap = dataMap;
		}

		private string id;
		public string ID
		{
			get
			{
				if (string.IsNullOrEmpty(id))
				{
					id = dataMap.GetValue<string>("id");
				}

				return id;
			}
		}

		private string title;
		public string Title
		{
			get
			{
				if (string.IsNullOrEmpty(title))
				{
					IDictionary<string, object> map = dataMap.GetHash("title");
					title = string.IsNullOrEmpty(map.GetValue<string>("fi")) ? map.GetValue<string>("und") : map.GetValue<string>("fi");
				}

				return title;
			}
		}

		private string typeMedia;
		public string TypeMedia
		{
			get
			{
				if (string.IsNullOrEmpty(typeMedia))
				{
					typeMedia = dataMap.GetValue<string>("typeMedia");
				}

				return typeMedia;
			}
		}

		private string duration;
		public string Duration
		{
			get
			{
				if (string.IsNullOrEmpty(duration))
				{
					duration = dataMap.GetValue<string>("duration");
				}
				return duration;
			}
		}

		private string typeCreative;
		public string TypeCreative
		{
			get
			{
				if (string.IsNullOrEmpty(typeCreative))
				{
					typeCreative = dataMap.GetValue<string>("typeCreative");
				}
				return typeCreative;
			}
		}

		public string description;
		public string Description
		{
			get
			{
				if (string.IsNullOrEmpty(description))
				{
					IDictionary<string, object> descriptionHash = dataMap.GetHash("description");
					if (descriptionHash.ContainsKey("fi"))
					{
						description = descriptionHash.GetValue<string>("fi");
					}
					else
					{
						description = "Could not retrieve description.";
					}
				}
				return description;
			}
		}
	}
}
