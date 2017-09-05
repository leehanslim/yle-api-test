using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanstools.Yle;

/// <summary>
/// Simple program data model
/// </summary>
public class ProgramsModel : MonoBehaviour 
{
	public System.Action<IList<ProgramData>> OnDataSetChanged;

	private Dictionary<string,ProgramData> programsTable;

	void Awake()
	{
		programsTable = new Dictionary<string, ProgramData>();
	}

	#region Programs model functions
	public void AddPrograms(IList<ProgramData> newPrograms)
	{
		IList<ProgramData> addedPrograms = new List<ProgramData>();
		foreach(ProgramData program in newPrograms)
		{
			// Continue to next program data if this one already exists
			if (programsTable.ContainsKey(program.ID))
				continue;
		
			programsTable.Add(program.ID, program);
			addedPrograms.Add(program);
		}

		// Broadcast a change in the data to any objects listening
		if (addedPrograms != null && addedPrograms.Count > 0 && OnDataSetChanged != null)
			OnDataSetChanged(addedPrograms);
	}
	#endregion // Programs model functions
}
