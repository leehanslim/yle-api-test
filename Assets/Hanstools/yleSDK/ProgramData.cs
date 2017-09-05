using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Extensions;

namespace Hanstools.Yle
{
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
	}
}
