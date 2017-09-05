using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;

public class ProgramsViewController : MonoBehaviour 
{
	[SerializeField]
	private ProgramsModel model;

	[SerializeField]
	private DetailsViewController detailsViewController;

	[SerializeField]
	private Transform scrollItemsParent;

	[SerializeField]
	private GameObject scrollItemPrefab;

	[SerializeField]
	private GameObject loadingPanel;

	public System.Action OnScrollNearEnd;

	// Use this for initialization
	void Awake () 
	{
		model.OnDataSetChanged += HandleOnModelUpdated;
	}

	void Destroy()
	{
		model.OnDataSetChanged -= HandleOnModelUpdated;
	}
	
	#region Event listeners
	private void HandleOnModelUpdated(IList<ProgramData> newPrograms)
	{
		if (newPrograms != null && newPrograms.Count > 0)
		{
			foreach(ProgramData programData in newPrograms)
			{
				CreateProgramButton(programData);	
			}
		}
	}
		
	public void HandleOnScrollVectorChanged(Vector2 vector)
	{
		if (vector.y < 0.05f)
		{
			// Broadcast to whoever's listening that we're nearing the end of the list
			if (OnScrollNearEnd != null)
				OnScrollNearEnd();
		}
	}
	#endregion // Event listeners


	#region Helpers
	private void CreateProgramButton(ProgramData programData)
	{
		// Create a new button object and set the necessary data
		GameObject go = Instantiate(scrollItemPrefab);
		ProgramScrollItem programScrollItem = go.GetComponent<ProgramScrollItem>();
		programScrollItem.SetData(programData);
		programScrollItem.SetDetailsDisplayReference(detailsViewController);

		// Set the transform so that this object shows up in the scroll list
		Transform goTransform = go.transform;
		goTransform.SetParent(scrollItemsParent);
	}

	public void EnableLoadingAnimation()
	{
		if (loadingPanel != null) loadingPanel.SetActive(true);
	}

	public void DisableLoadingAnimation()
	{
		if (loadingPanel != null) loadingPanel.SetActive(false);
	}

	public void ClearList()
	{
		if (scrollItemsParent.childCount <= 0)
			return;
		
		for (int i = scrollItemsParent.childCount - 1; i >= 0; i--)
		{
			Destroy(scrollItemsParent.GetChild(i).gameObject);
		}
	}
	#endregion // Helpers
}
