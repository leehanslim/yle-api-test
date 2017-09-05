using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISimpleRotate : MonoBehaviour 
{
	[SerializeField]
	private float rotateSpeed = 50;

	[SerializeField]
	private bool reverse;

	private Transform myTransform;
	private int direction = 1;

	// Use this for initialization
	void Awake() 
	{
		myTransform = transform;
		direction = reverse ? -1 : 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		myTransform.Rotate(new Vector3(0, 0, direction * (Time.deltaTime * rotateSpeed)));
	}
}
