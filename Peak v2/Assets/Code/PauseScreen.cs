using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

	bool paused = false;
	GameObject arrow;
	bool onQuit = false;
	GameObject[] pauseGroup = new GameObject[4];
	CurtainCollider curtain;
	// Use this for initialization
	void Start () {
		arrow = GameObject.Find("Pause Arrow");
		curtain = GameObject.Find("Curtain Collider").GetComponent<CurtainCollider>();
		for (int i = 0; i < 4; i++) {
			//print(GameObject.Find("Esc Button Group").transform.childCount);
		    pauseGroup[i] = gameObject.transform.GetChild(i).gameObject;
		    //print(escGroup[i].name); 
		}

		SetTransparency(0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("escape")) {
			SetPausedGame();
			curtain.SwitchPausedGame();
		}

		if (paused) {
			if (Input.GetKeyDown("down") && !onQuit) {
				onQuit = true;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y - 18f);
			}
			if (Input.GetKeyDown("up") && onQuit) {
				onQuit = false;
				arrow.transform.localPosition = new Vector2(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 18f);
			}

			if (Input.GetKeyDown("space") && !onQuit) {
				SetPausedGame();
				curtain.SwitchPausedGame();
			}

			if (Input.GetKeyDown("space") && onQuit) {
				Application.Quit();
			}
		}
	
	}

	public void SetPausedGame () {
		paused = !paused;
		if (paused) {
			Time.timeScale = 0f;
			SetTransparency(1f);
		}
		else {
			Time.timeScale = 1f;
			SetTransparency(0f);
		}
	}

	void SetTransparency (float val) {
		for (int i = 0; i < pauseGroup.Length; i++) {
			if (pauseGroup[i].GetComponent<Image>() != null) {
				Image currGO = pauseGroup[i].GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, val);
			}

			else if (pauseGroup[i].GetComponent<Text>() != null) {
				Text currGO = pauseGroup[i].GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, val);
			}
		}
	}
}
