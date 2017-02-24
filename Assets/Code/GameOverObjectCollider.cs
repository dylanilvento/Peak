﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
// using XboxCtrlrInput;

public class GameOverObjectCollider : MonoBehaviour {

	Transform player;
	float relPos;
	// Image goScreen;
	Camera camera;
	CameraFollow cameraFollow;

	public GameObject target;

	bool goActive = false;
	// LevelControl levelControl;
	// public static int deathCnt = 0;
	public GameObject statusScreen;

	// Use this for initialization
	void Start () {
		// player = GameObject.Find("Scout Group").transform;
		// relPos = player.position.x - transform.position.x;
		// goScreen = GameObject.Find("Game Over Screen").GetComponent<Image>();
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraFollow = camera.GetComponent<CameraFollow>();
		// levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		// statusScreen = GameObject.Find("Status Screen");
	}
	
	// Update is called once per frame
	void Update () {
		// transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		// StartCoroutine("MoveUp");

		//************ COMMENTED OUT DUE TO CTRLR ERROR
		// if (goActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
		//*************
		if (goActive && (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton9))) {
			Time.timeScale = 1f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject == target) {
			Time.timeScale = 0.0001f;

			StartCoroutine("RestartLevel");
			// goScreen.enabled = true;
			// cameraFollow.canMove = false;
			// // deathCnt++;
			// // print(deathCnt);

			// statusScreen.SetActive(true);
			// statusScreen.transform.GetChild(0).GetComponent<Text>().text = "Press A to Restart";

			// goActive = true;

		}
	}

	IEnumerator RestartLevel() {
		// print("restarting level");
		yield return new WaitForSeconds(0.0001f);
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}
}
