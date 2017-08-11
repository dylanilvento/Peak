using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XboxCtrlrInput;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

	bool paused = false;
	GameObject arrow;
	bool onQuit = false;
	public bool playableLevel = true;
	// GameObject[] pauseGroup = new GameObject[10];
	List<GameObject> pauseGroup = new List<GameObject>();

	CurtainCollider curtain;

	static bool demoMode = true;
	// Use this for initialization
	void Start () {
		arrow = GameObject.Find("Pause Arrow");
		if (playableLevel) curtain = GameObject.Find("Curtain Collider").GetComponent<CurtainCollider>();
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			//print(GameObject.Find("Esc Button Group").transform.childCount);
		    pauseGroup.Add(gameObject.transform.GetChild(i).gameObject);
		    //print(escGroup[i].name); 
		}

		// SetTransparency(0f, pauseGroup);
		SetPauseScreen(false);
	}
	
	// Update is called once per frame
	void Update () {

		// if (XCI.GetButtonUp(XboxButton.Start)) {
		// 	print("YES");
		// }

		//************ COMMENTED OUT DUE TO CTRLR ERROR

		if (Input.GetKeyDown("escape") || XCI.GetButtonDown(XboxButton.Start)) {

		//*********************************
		// if (Input.GetKeyDown("escape") || Input.GetButtonDown("Start Button")) {
			SetPausedGame();
			if (playableLevel) curtain.SwitchPausedGame();
		}

		if (paused && !demoMode) {
			if ((Input.GetKeyDown("down") || Input.GetAxis("Vertical") < 0f || XCI.GetDPad(XboxDPad.Down)) && !onQuit) {
				onQuit = true;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y - 18f);
			}
			if ((Input.GetKeyDown("up") || Input.GetAxis("Vertical") > 0f || XCI.GetDPad(XboxDPad.Up)) && onQuit) {
				onQuit = false;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 18f);
			}
			
			//************ COMMENTED OUT DUE TO CTRLR ERROR
			if ((Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A)) && !onQuit) {
			// if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton0)) && onQuit) {
				SetPausedGame();

				if (playableLevel) curtain.SwitchPausedGame();
			}

				//************ COMMENTED OUT DUE TO CTRLR ERROR
			if ((Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A)) && onQuit) {
			
			// if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton0)) && onQuit) {
				//USED FOR WARD GAMES LAUNCHER

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

				Application.Quit();
			}
		}

		else if (paused && demoMode) {
			if (XCI.GetButtonUp(XboxButton.A) || XCI.GetButtonUp(XboxButton.B)) {
			// if (Input.GetButtonDown("A Button") || Input.GetButtonDown("B Button")) {
				SetPausedGame();
				if (playableLevel) curtain.SwitchPausedGame();
			}
			//reload level
			else if (XCI.GetButtonUp(XboxButton.X)) {
			// else if (Input.GetButtonDown("X Button")) {
				Time.timeScale = 1f;
				IntersceneDataHandler.startedTutorial = false;
				IntersceneDataHandler.currentLevel = 0;
				Application.LoadLevel(Application.loadedLevel);
			}

			else if (XCI.GetButtonUp(XboxButton.Y)) {
			// else if (Input.GetButtonDown("Y Button")) {
				Time.timeScale = 1f;
				Application.LoadLevel(0);
			}

			else if (XCI.GetButtonUp(XboxButton.LeftBumper)) {
			// else if (Input.GetButtonDown("Left Bumper")) {
				Application.LoadLevel(4);
			}

			else if (XCI.GetButtonUp(XboxButton.RightBumper)) {
				SceneManager.LoadScene("Level Select");
			}
		}
	
	}

	public void SetPausedGame () {
		paused = !paused;
		if (paused) {
			Time.timeScale = 0f;
			// SetTransparency(1f, pauseGroup);
			SetPauseScreen(true);
		}
		else {
			Time.timeScale = 1f;
			// SetTransparency(0f, pauseGroup);
			SetPauseScreen(false);
		}
	}

	void SetTransparency (float val, GameObject[] goArray) {
		for (int i = 0; i < goArray.Length; i++) {
			// print(i + ": " + pauseGroup[i].name);
			if (val == 1 && demoMode && goArray[i].name.Equals("Quit")) {
				continue;
			}

			else if (val == 1 && !demoMode && (i >= 4)) {
				continue;
			}

			if (goArray[i].GetComponent<Image>() != null) {
				Image currGO = goArray[i].GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, val);
			}

			else if (goArray[i].GetComponent<Text>() != null) {
				Text currGO = pauseGroup[i].GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, val);
			}

			else if (goArray[i].gameObject.transform.childCount > 0) {
				 List<GameObject> children = new List<GameObject>();
				 for (int ii = 0; ii < goArray[i].gameObject.transform.childCount; ii++) {
					 children.Add(goArray[i].gameObject.transform.GetChild(ii).gameObject);
					//  SetTransparency(0f, children);
				 }

			}
		}
	}

	void SetPauseScreen (bool val) {
		foreach(GameObject obj in pauseGroup) {
			if (val && demoMode && obj.name.Equals("Quit")) {
				continue;
			}

			else if (val && !demoMode && obj.name.Contains("Restart")) {
				continue;
			}
			else {
				obj.SetActive(val);
			}
			
		}
	}
}
