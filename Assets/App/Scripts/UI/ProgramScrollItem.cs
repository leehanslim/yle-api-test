using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;

public class ProgramScrollItem : MonoBehaviour {

	private ProgramData currentData;

	#region Event handlers
	public void HandleOnPressed()
	{
		
	}
	#endregion // Event handlers


	#region Srcoll item functions
	public void SetData(ProgramData data)
	{
		currentData = data;
	}
	#endregion // Scroll item functions


	#region Helpers
	private void UpdateDisplay()
	{
		
	}
	#endregion // Helpers
}
