using UnityEngine;
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
	public GameObject statusScreen, levelClearBox, continueBox, mainCamera;
	GameObject target;
	public string statusTextKeyboard;
	public string statusTextCtrlr;

	public int levelCompleteScreenDist;

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
			// if (nextLevel == 0) {
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
			// 	Application.LoadLevel(0);
			// }
			// else {
			// 	Time.timeScale = 1f;
				// Application.LoadLevel(Application.loadedLevel);
				// Application.LoadLevel(nextLevel);
				SceneManager.LoadScene("Level Select");
			// }
			
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		

		if (other.gameObject == target) {
			// print("winning");

			other.gameObject.GetComponent<CharacterMovement2>().SetMovementOff(true);
			other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

			flagAnim.SetTrigger("Raise");
			// Time.timeScale = 0f;
			// winScreen.enabled = true;
			int deathCnt = GameOverCollider.deathCnt;
			// Analytics.CustomEvent("winGame", new Dictionary<string, object>
			// {
			// 	{"winTime", Time.timeSinceLevelLoad},
			// 	{"numDeaths", deathCnt}
			// });

			// statusScreen.SetActive(true);

			// ********** COMMENTED OUT DUE TO CTRLR ERROR
			// if (ctrlNum > 0) statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusTextCtrlr;
			// else statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusTextKeyboard;
			//********************************

			StartCoroutine("ShowLevelClear");

			StartCoroutine("MoveCamera");



		}
	}

	IEnumerator ShowLevelClear() {
		float dist = (float) levelCompleteScreenDist;

		while(dist > 0) {
			levelClearBox.transform.position = new Vector2(levelClearBox.transform.position.x - 15f, levelClearBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
			dist-=15f;
		}

		dist = (float) levelCompleteScreenDist;

		while(dist > 0) {
			continueBox.transform.position = new Vector2(continueBox.transform.position.x - 15f, continueBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
			dist-=15f;
		}

		winActive = true;
		lvlControl.SetLevelOver(true);
	}

	IEnumerator MoveCamera() {
		float dist = 1f;

		while(dist > 0) {
			print("moving");
			mainCamera.GetComponent<CameraFollow>().enabled = false;
			mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 0.05f, mainCamera.transform.position.y, mainCamera.transform.position.z);
			yield return new WaitForSeconds(0.01f);
			dist-=0.05f;
		}
	}


}
