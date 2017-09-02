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
			YleSDKManager.GetProgramsList(0, 1);
		}
	}
}
