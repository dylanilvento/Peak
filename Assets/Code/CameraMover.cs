using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	CameraFollow cam;
	GameOverCollider goCollider;
	[Range(0f, 2f)]
	public float upVal;
	[Range(0f, 5f)]
	public float downVal;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		goCollider = GameObject.Find("Game Over Collider").GetComponent<GameOverCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		print("working 1");
		if (other.gameObject.name.Equals("Scout Mega Group")) {
			print("working 2");
			cam.SetCameraVals(upVal, downVal);
			goCollider.SetGOVals(upVal, downVal);

		}
	}
}
