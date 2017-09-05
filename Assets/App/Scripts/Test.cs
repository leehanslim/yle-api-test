using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;

public class Test : MonoBehaviour 
{
	#if TEST_MODE
	[SerializeField]
	private string categoryKey;

	[SerializeField]
	private ProgramType type;

	[SerializeField]
	private MediaObjectType mediaObjectType;

	[SerializeField]
	private bool getProgramsList = false;
	#endif

	void Start()
	{
		#if TEST_MODE
		YleSDKManager.Init( (success) => {
			Debug.Log("Yle SDK initialized: " + success.ToString());
		} );
		#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
		#if TEST_MODE
		if (getProgramsList)
		{
			getProgramsList = false;

			YleSDKManager.GetProgramsList((list) => {

				if (list == null || list.Count <= 0)
				{
					Debug.Log("Nothing found!");
					return;
				}
			
				string output = string.Empty;
				for (int i = 0; i < list.Count; i++)
				{
					output += "ID=" + list[i].ID + ", Title=" + list[i].Title + ", TypeMedia=" + list[i].TypeMedia + "\n";
				}
				Debug.Log(output);
					
			}, 0, 10, categoryKey, type, mediaObjectType);
		}
		#endif
	}
}
