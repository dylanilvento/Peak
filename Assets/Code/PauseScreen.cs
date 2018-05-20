using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// using XboxCtrlrInput;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;

public class PauseScreen : MonoBehaviour {

	bool paused = false;
	GameObject arrow;
	bool onQuit = false;
	public bool playableLevel = true;
	// GameObject[] pauseGroup = new GameObject[10];
	List<GameObject> pauseGroup = new List<GameObject>();

	CurtainCollider curtain;

	float menuDist = 23f;

	static bool demoMode = false;

	bool hasController;

	public int playerId = 0; // The Rewired player id of this character

    private Player player; // The Rewired Player
	
	void Awake () {
		player = ReInput.players.GetPlayer(playerId);
		
		
        // Subscribe to events
        ReInput.ControllerConnectedEvent += OnControllerConnected;

		
        // ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        // ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
    }

	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
        // print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		hasController = player.controllers.ContainsController<Joystick>(args.controllerId);
		// print(hasController);
    }

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

		//************ COMMENTED OUT DUE TO CTRLR ERROR

		if (player.GetButtonDown("Pause")) {

			SetPausedGame();
			if (playableLevel) curtain.SwitchPausedGame();
		}

		if (paused && !demoMode) {
			// if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0f /*|| XCI.GetDPad(XboxDPad.Down)*/) && !onQuit) {
			if ((player.GetButtonDown("Down")) && !onQuit) {
				onQuit = true;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y - menuDist);
			}
			// if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0f /*|| XCI.GetDPad(XboxDPad.Up)*/) && onQuit) {
			if ((player.GetButtonDown("Up")) && onQuit) {
				onQuit = false;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y + menuDist);
			}
			
			//************ COMMENTED OUT DUE TO CTRLR ERROR
			if ((player.GetButtonDown("Continue")) && !onQuit) {

				SetPausedGame();

				if (playableLevel) curtain.SwitchPausedGame();
			}

				//************ COMMENTED OUT DUE TO CTRLR ERROR
			if (player.GetButtonDown("Continue") && onQuit) {
			
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
			if (player.GetButtonDown("Continue") || player.GetButtonDown("B")) {
				SetPausedGame();
				if (playableLevel) curtain.SwitchPausedGame();
			}
			//reload level
			else if (player.GetButtonDown("X")) {
				Time.timeScale = 1f;
				IntersceneDataHandler.startedTutorial = false;
				IntersceneDataHandler.currentLevel = 0;
				Application.LoadLevel(Application.loadedLevel);
			}

			else if (player.GetButtonDown("Y")) {
				Time.timeScale = 1f;
				Application.LoadLevel(0);
			}

			else if (player.GetButtonDown("Right Bumper")) {
				Time.timeScale = 1f;
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

			else if (val && !demoMode && obj.name.Contains("Background2")) {
				continue;
			}
			else {
				obj.SetActive(val);
			}
			
		}
	}
}
