using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathSortingOrder : MonoBehaviour {
	public int sortOrder;
	// Use this for initialization
	void Start () {
		GetComponent<LineRenderer>().sortingOrder = sortOrder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
