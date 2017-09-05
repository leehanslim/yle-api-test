using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Extensions;

namespace Hanstools.Yle
{
	/// <summary>
	/// Data object for holding and serving category-related information. 
	/// Uses Dictionary extension methods to mine most of the information.
	/// </summary>
	public class CategoryData
	{
		private IDictionary<string, object> dataMap;

		public CategoryData(IDictionary<string, object> dataMap)
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
					title = dataMap.GetHash("title").GetValue<string>("fi");
				}

				return title;
			}
		}
	}
}
