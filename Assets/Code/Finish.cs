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
	public bool endOfDemo = false;
	public bool winActive = false;
	public int nextLevel;

	BoxCollider2D boxCollider;
	//GameOverCollider goCollider;
	//Camera camera;
	public CurtainCollider curtain;
	public GameObject statusScreen, levelClearBox, continueBox, mainCamera;
	GameObject target, levelClearText;
	// public string statusTextKeyboard;
	// public string statusTextCtrlr;

	float levelCompleteScreenDist, globalReduction;

	LevelControl lvlControl;

	int ctrlNum;
	// Use this for initialization
	void Start () {
		// print("canvas width: " + GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width);
		// print(levelClearBox.GetComponent<RectTransform>().rect.center - GameObject.Find("Canvas").GetComponent<RectTransform>().rect.center);
		boxCollider = GetComponent<BoxCollider2D>();
		float differenceCanvasAndLevelComplete = levelClearBox.transform.position.x - GameObject.Find("Canvas").transform.position.x;
		levelClearText = levelClearBox.transform.GetChild(0).gameObject;
		print("difference = " + differenceCanvasAndLevelComplete);

		levelCompleteScreenDist = (2f / 3f) * differenceCanvasAndLevelComplete;

		//************ COMMENTED OUT DUE TO CTRLR ERROR
		ctrlNum = XCI.GetNumPluggedCtrlrs();
		//***************************
		lvlControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		target = lvlControl.GetPlayer();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (winActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
			StartCoroutine("TransitionToLevelSelect");
			


			
		}
	}

	IEnumerator TransitionToLevelSelect () {
		for (int ii = 0; ii < 60; ii++) {
			levelClearText.transform.position = new Vector2(levelClearText.transform.position.x + globalReduction, levelClearText.transform.position.y);
			continueBox.transform.position = new Vector2(continueBox.transform.position.x - globalReduction, continueBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(0.15f);

		for (int ii = 0; ii < 5; ii++) {
			levelClearBox.transform.position = new Vector2(levelClearBox.transform.position.x - globalReduction, levelClearBox.transform.position.y);
			// continueBox.transform.position = new Vector2(continueBox.transform.position.x + globalReduction, continueBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
		}
		
		if (!endOfDemo) {
			
			IntersceneDataHandler.levelToLevelSelectTransition = true;
			SceneManager.LoadScene("Level Select");
		}
		else {
			SceneManager.LoadScene("End Demo");
			IntersceneDataHandler.currentLevel = 0;
		}
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		

		if (other.gameObject == target) {
			// print("winning");
			// curtain.SwitchPausedGame();
			curtain.SetPausedGame(true);
			boxCollider.enabled = false;
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
		
		float dist = levelCompleteScreenDist;
		float reduction = levelCompleteScreenDist / 26.7f;

		print("started end, dist = " + dist);
		print(reduction);

		while(dist > 0) {
			levelClearBox.transform.position = new Vector2(levelClearBox.transform.position.x - reduction, levelClearBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
			dist -= reduction;
		}

		dist = levelCompleteScreenDist;

		while(dist > 0) {
			continueBox.transform.position = new Vector2(continueBox.transform.position.x - reduction, continueBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);
			dist -= reduction;
		}

		winActive = true;

		globalReduction = reduction;
		IntersceneDataHandler.globalLevelClearMovmentRate = reduction;
		lvlControl.SetLevelOver(true);
	}

	IEnumerator MoveCamera() {
		float dist = 1f;

		while(dist > 0) {
			// print("moving");
			mainCamera.GetComponent<CameraFollow>().enabled = false;
			mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 0.05f, mainCamera.transform.position.y, mainCamera.transform.position.z);
			yield return new WaitForSeconds(0.01f);
			dist-=0.05f;
		}
	}


}
