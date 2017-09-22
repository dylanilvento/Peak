using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {

	public GameObject player;
	bool follow = true;
	bool levelOver = false;

	// public static bool demoMode;

	public bool canPulse = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLevelOver (bool val) {
		levelOver = val;
	}

	public bool GetLevelOver () {
		return levelOver;
	}

	public void SetFollow (bool val) {
		follow = val;
	}

	public bool GetFollow () {
		return follow;
	}

	public GameObject GetPlayer () {
		return player;
	}


	public bool GetCanPulse () {
		return canPulse;
	}

	public void SetCanPulse (bool val) {
		canPulse = val;
	}
}	

