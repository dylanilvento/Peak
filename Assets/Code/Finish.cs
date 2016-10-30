using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class Finish : MonoBehaviour {

	//Transform player;
	//float relPos;
	// Image winScreen;
	bool winActive = false;
	public int nextLevel;
	//GameOverCollider goCollider;
	//Camera camera;
	public GameObject statusScreen;
	public string statusText;
	// Use this for initialization
	void Start () {
		//player = GameObject.Find("Scout").transform;
		//relPos = player.position.x - transform.position.x;
		//goCollider = GameObject.Find("Game Over Collider").GetComponent<GameOverCollider>();
		// winScreen = GameObject.Find("Win Screen").GetComponent<Image>();
		//camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		//StartCoroutine("MoveUp");

		if (winActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
			if (nextLevel == 0) {
				Application.Quit();
			}
			else {
				Time.timeScale = 1f;
				// Application.LoadLevel(Application.loadedLevel);
				Application.LoadLevel(nextLevel);
			}
			
		}
	}

	/*IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}*/

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("Scout")) {
			print("winning");
			Time.timeScale = 0f;
			// winScreen.enabled = true;
			int deathCnt = GameOverCollider.deathCnt;
			// Analytics.CustomEvent("winGame", new Dictionary<string, object>
			// {
			// 	{"winTime", Time.timeSinceLevelLoad},
			// 	{"numDeaths", deathCnt}
			// });

			statusScreen.SetActive(true);
			statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusText;
			winActive = true;

		}
	}
}
