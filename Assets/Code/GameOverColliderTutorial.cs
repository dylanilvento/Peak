using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverColliderTutorial : MonoBehaviour {
	GameObject target;
	LevelControl levelControl;
	
	// Use this for initialization
	void Start () {
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		target = levelControl.GetPlayer();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == target) {
			// Time.timeScale = 0f;
			// goScreen.enabled = true;
			// deathCnt++;
			// levelControl.SetFollow(false);
			IntersceneDataHandler.startedTutorial = true;
			// print(deathCnt);
			// cameraFollow.canMove = false;

			// statusScreen.SetActive(true);
			// statusScreen.transform.GetChild(0).GetComponent<Text>().text = goText;

			// goActive = true;

		}
	}
}
