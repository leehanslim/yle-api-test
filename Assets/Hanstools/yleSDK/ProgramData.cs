using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}
}
