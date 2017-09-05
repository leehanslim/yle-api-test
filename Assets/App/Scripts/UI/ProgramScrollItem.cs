using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hanstools.Yle;

public class ProgramScrollItem : MonoBehaviour {

	[SerializeField]
	private Text titleText;

	private ProgramData currentData;

	#region Event handlers
	public void HandleOnPressed()
	{
		// TODO - pass data to details view controller
	}
	#endregion // Event handlers


	#region Srcoll item functions
	public void SetData(ProgramData data)
	{
		currentData = data;
		UpdateDisplay();
	}
	#endregion // Scroll item functions


	#region Helpers
	private void UpdateDisplay()
	{
		titleText.text = currentData.Title;
	}
	#endregion // Helpers
}
