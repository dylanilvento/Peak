using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersceneDataHandler : MonoBehaviour {

	static public IntersceneDataHandler instance;

	static public float globalLevelClearMovmentRate;

	static public bool levelToLevelSelectTransition = false;

	static public int currentLevel = 0;
	void Awake () {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
