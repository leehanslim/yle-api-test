using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;
using UnityEngine.UI;

public class DetailsViewController : MonoBehaviour {

	[SerializeField]
	private Text idTxt;

	[SerializeField]
	private Text titleTxt;

	[SerializeField]
	private Text typeMediaTxt;

	[SerializeField]
	private Text durationTxt;

	[SerializeField]
	private Text typeCreativeTxt;

	[SerializeField]
	private Text descriptionTxt;

	[SerializeField]
	private GameObject uiContainer;

	private ProgramData currentData;

	#region Details view controller functions
	public void SetData(ProgramData currentData)
	{
		this.currentData = currentData;

		SetID(this.currentData.ID);
		SetTypeMedia(this.currentData.TypeMedia);
		SetTitle(this.currentData.Title);
		SetTypeCreative(this.currentData.TypeCreative);
		SetDuration(this.currentData.Duration);
		SetDescription(this.currentData.Description);

		uiContainer.SetActive(true);
	}
	#endregion // Details view controller functions


	#region Helpers
	private void SetID(string id)
	{
		if (idTxt != null)
			idTxt.text = id;
	}

	private void SetTitle(string title)
	{
		if (titleTxt != null)
			titleTxt.text = title;
	}

	private void SetTypeCreative(string typeCreative)
	{
		if (typeCreativeTxt != null)
			typeCreativeTxt.text = typeCreative;
	}

	private void SetTypeMedia(string typeMedia)
	{
		if (typeMediaTxt != null)
			typeMediaTxt.text = typeMedia;
	}

	private void SetDuration(string duration)
	{
		if (durationTxt != null)
			durationTxt.text = duration;
	}

	private void SetDescription(string description)
	{
		if (descriptionTxt != null)
			descriptionTxt.text = description;
	}
	#endregion // Helpers
}
