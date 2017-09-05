using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewIndexUtility : MonoBehaviour {

	[SerializeField]
	private ScrollRect scroll;

	[SerializeField]
	private Vector2 normalizedPosition;

	[SerializeField]
	private float verticalNormalizedPosition;

	[SerializeField]
	private Vector2 listening;
	
	// Update is called once per frame
	void Update () {
		normalizedPosition = scroll.normalizedPosition;
		verticalNormalizedPosition = scroll.verticalNormalizedPosition;
	}

	public void Listen(Vector2 v)
	{
		listening = v;
	}
}
