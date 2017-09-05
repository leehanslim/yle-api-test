using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hanstools.Yle;

public class YleDemo : MonoBehaviour {

	[SerializeField]
	private InputField searchInput;

	#region Event handlers
	public void HandleOnSearchBtnPressed()
	{
		YleSDKManager.GetProgramsList( (delegate(IList<ProgramData> programs) {
			Debug.Log("Retrieved \'" + (programs != null ? programs.Count : 0).ToString() + " items.");
		}), 0, 10, searchInput.text);
	}

	public void HandleOnClearBtnPressed()
	{
		searchInput.text = string.Empty;
	}
	#endregion // Event handlers
}
