using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;

public class Test : MonoBehaviour 
{
	[SerializeField]
	private bool getProgramsList;
	
	// Update is called once per frame
	void Update () 
	{
		if (getProgramsList)
		{
			getProgramsList = false;

			YleSDKManager.GetProgramsList((list) => {
			
				string output = string.Empty;
				for (int i = 0; i < list.Count; i++)
				{
					output += "ID=" + list[i].ID + "\n";
				}
				Debug.Log(output);

			}, 0, 1);
		}
	}
}
