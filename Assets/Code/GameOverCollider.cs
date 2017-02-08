using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using XboxCtrlrInput;

public class GameOverCollider : MonoBehaviour {
	public GameObject target;
	Transform player;
	float relPos;
	public float relPosY;
	Image goScreen;
	Camera camera;
	CameraFollow cameraFollow;

	int ctrlNum;

	string goText;

	CharacterMovement2 scoutMvmt;

	bool goActive = false;

	public GameObject statusScreen;

	public static int deathCnt = 0;

	// Use this for initialization
	void Start () {
		// player = GameObject.Find("Scout").transform;
		ctrlNum = XCI.GetNumPluggedCtrlrs();

		player = target.transform;

		relPos = player.position.x - transform.position.x;
		relPosY = Mathf.Abs(player.position.y - transform.position.y);
		// goScreen = GameObject.Find("Game Over Screen").GetComponent<Image>();
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraFollow = camera.GetComponent<CameraFollow>();
		scoutMvmt = target.GetComponent<CharacterMovement2>();

		if (ctrlNum > 0) goText = "Press A to Restart";
		else goText = "Press Space to Restart";
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		StartCoroutine("Move");

		if (goActive && (Input.GetKeyDown("space") || XCI.GetButtonUp(XboxButton.A) || XCI.GetButtonUp(XboxButton.Start))) {
			Time.timeScale = 1f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator Move () {
		while ((camera.WorldToScreenPoint(player.position).y > Screen.height / 2f) || ((Mathf.Abs(player.position.y - transform.position.y) > relPosY) && scoutMvmt.grounded)) {
		// while ((camera.WorldToScreenPoint(player.position).y > Screen.height / 2f) || ((Mathf.Abs(player.position.y - transform.position.y) > relPosY) && !(scoutMvmt.jumped))) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}

		while (((camera.WorldToScreenPoint(player.position).y < Screen.height / 4f)) && !(scoutMvmt.grounded)) {
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == target) {
			Time.timeScale = 0f;
			// goScreen.enabled = true;
			deathCnt++;
			// print(deathCnt);
			cameraFollow.canMove = false;

			statusScreen.SetActive(true);
			statusScreen.transform.GetChild(0).GetComponent<Text>().text = goText;

			goActive = true;

		}
	}

	public static int GetDeathCnt () {
		return deathCnt;
	}
}
