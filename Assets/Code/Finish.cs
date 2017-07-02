﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using System.Diagnostics;
using System;

public class Finish : MonoBehaviour {

	//Transform player;
	//float relPos;
	// Image winScreen;

	public Animator flagAnim;

	public bool winActive = false;
	public int nextLevel;
	//GameOverCollider goCollider;
	//Camera camera;
	public GameObject statusScreen;
	GameObject target;
	public string statusTextKeyboard;
	public string statusTextCtrlr;

	LevelControl lvlControl;

	int ctrlNum;
	// Use this for initialization
	void Start () {
		
		//************ COMMENTED OUT DUE TO CTRLR ERROR
		ctrlNum = XCI.GetNumPluggedCtrlrs();
		//***************************
		lvlControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		target = lvlControl.GetPlayer();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (winActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
		// if (winActive && (Input.GetKeyDown("space") || Input.GetButtonDown("A Button") || Input.GetButtonDown("Start Button"))) {
			if (nextLevel == 0) {
				// try {
				// 	Process myProcess = new Process();
			 //        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
			 //        myProcess.StartInfo.CreateNoWindow = false;
			 //        // myProcess.StartInfo.UseShellExecute = false;
			 //        myProcess.StartInfo.FileName = "C:\\Users\\Dylan\\Desktop\\BnP\\Launcher\\Launcher.exe";
			 //        myProcess.Start();
			 //    }
			 //    catch (Exception e) {
		  //           Console.WriteLine(e.Message);
		  //       }

				// Application.Quit();
				Application.LoadLevel(0);
			}
			else {
				Time.timeScale = 1f;
				// Application.LoadLevel(Application.loadedLevel);
				Application.LoadLevel(nextLevel);
			}
			
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		

		if (other.gameObject == target) {
			// print("winning");

			other.gameObject.GetComponent<CharacterMovement2>().SetMovementOff(true);

			flagAnim.SetTrigger("Raise");
			// Time.timeScale = 0f;
			// winScreen.enabled = true;
			int deathCnt = GameOverCollider.deathCnt;
			// Analytics.CustomEvent("winGame", new Dictionary<string, object>
			// {
			// 	{"winTime", Time.timeSinceLevelLoad},
			// 	{"numDeaths", deathCnt}
			// });

			statusScreen.SetActive(true);

			// ********** COMMENTED OUT DUE TO CTRLR ERROR
			if (ctrlNum > 0) statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusTextCtrlr;
			else statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusTextKeyboard;
			//********************************

			winActive = true;

			lvlControl.SetLevelOver(true);



		}
	}
}
