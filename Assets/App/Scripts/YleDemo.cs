using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hanstools.Yle;

public class YleDemo : MonoBehaviour 
{
	[SerializeField]
	private int initialOffset = 0;

	[SerializeField]
	private int offsetIncrease = 10;

	[SerializeField]
	private ProgramsModel programsModel;

	[SerializeField]
	private ProgramsViewController programsViewController;

	[SerializeField]
	private DetailsViewController detailsViewController;

	[SerializeField]
	private InputField searchInput;

	[SerializeField]
	private GameObject loadingPanel;

	private bool updatingData = false;

	void Awake()
	{
		programsViewController.OnScrollNearEnd += HandleOnNeedToUpdateProgramsList;
	}

	void Destroy()
	{
		programsViewController.OnScrollNearEnd -= HandleOnNeedToUpdateProgramsList;
	}

	void Start()
	{
		YleSDKManager.Init( (success) => {
			Debug.Log("Yle Demo: Yle SDK initialized: " + success.ToString());
			if (success)
			{
				DisableLoadingAnimation();
			}
			else
			{
				// QQQ - display some error message
			}
		} );
	}

	#region Event handlers
	public void HandleOnNeedToUpdateProgramsList()
	{
		if (updatingData)
			return;

		updatingData = true;

		// Whenever we update the programs list, we simply query for the next batch of programs
		initialOffset += offsetIncrease;
		UpdateProgramsListView(initialOffset, offsetIncrease, searchInput.text);
	}

	public void HandleOnSearchBtnPressed()
	{
		// A new search query should "reset" the search process, and so, will erase the current list
		programsViewController.ClearList();
		UpdateProgramsListView(initialOffset, offsetIncrease, searchInput.text);
	}
	#endregion // Event handlers


	#region Helpers
	private void UpdateProgramsListView(int offset, int limit, string key)
	{
		programsViewController.EnableLoadingAnimation();
		YleSDKManager.GetProgramsList( (delegate(IList<ProgramData> programs) {
			Debug.Log("Retrieved \'" + (programs != null ? programs.Count : 0).ToString() + " items.");
			programsModel.AddPrograms(programs);
			programsViewController.DisableLoadingAnimation();
			updatingData = false;
		}), offset, limit, key);
	}

	private void DisableLoadingAnimation()
	{
		if (loadingPanel != null) loadingPanel.SetActive(false);
	}

	private void EnableLoadingAnimation()
	{
		if (loadingPanel != null) loadingPanel.SetActive(true);
	}
	#endregion // Helpers
}
