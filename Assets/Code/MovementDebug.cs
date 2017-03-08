using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDebug : MonoBehaviour {

	public GameObject[] windowObjs = new GameObject[5];

	// public bool visiblePub;
	static bool visible = false;

	// Use this for initialization
	void Start () {
		// visible = visiblePub;

		if (!visible) {
			Transparency.SetTransparent(windowObjs);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
