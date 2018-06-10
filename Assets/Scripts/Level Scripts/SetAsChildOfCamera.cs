using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsChildOfCamera : MonoBehaviour {

	GameObject camera;
	// Use this for initialization
	void Start () {

		camera = GameObject.Find("Main Camera");

		gameObject.transform.SetParent(camera.transform);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
