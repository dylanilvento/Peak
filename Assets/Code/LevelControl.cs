using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {

	bool follow = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetFollow (bool val) {
		follow = val;
	}

	public bool GetFollow () {
		return follow;
	}
}
