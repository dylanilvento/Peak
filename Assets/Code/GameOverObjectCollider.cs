using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using XboxCtrlrInput;

public class GameOverObjectCollider : MonoBehaviour {

	Transform player;
	float relPos;
	// Image goScreen;
	Camera camera;
	CameraFollow cameraFollow;

	bool goActive = false;

	// public static int deathCnt = 0;
	public GameObject statusScreen;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Scout Group").transform;
		// relPos = player.position.x - transform.position.x;
		// goScreen = GameObject.Find("Game Over Screen").GetComponent<Image>();
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraFollow = camera.GetComponent<CameraFollow>();
		// statusScreen = GameObject.Find("Status Screen");
	}
	
	// Update is called once per frame
	void Update () {
		// transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		// StartCoroutine("MoveUp");

		if (goActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
			Time.timeScale = 1f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.name.Equals("Scout Group")) {
			Time.timeScale = 0f;
			// goScreen.enabled = true;
			cameraFollow.canMove = false;
			// deathCnt++;
			// print(deathCnt);

			statusScreen.SetActive(true);
			statusScreen.transform.GetChild(0).GetComponent<Text>().text = "Press A to Restart";

			goActive = true;

		}
	}
}
