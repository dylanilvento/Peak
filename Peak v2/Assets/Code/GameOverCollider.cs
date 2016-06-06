using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class GameOverCollider : MonoBehaviour {

	Transform player;
	float relPos;
	Image goScreen;
	Camera camera;

	public static int deathCnt = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Scout").transform;
		relPos = player.position.x - transform.position.x;
		goScreen = GameObject.Find("Game Over Screen").GetComponent<Image>();
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		StartCoroutine("MoveUp");

		if (goScreen.enabled && Input.GetKeyDown("space")) {
			Time.timeScale = 1f;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("Scout")) {
			Time.timeScale = 0f;
			goScreen.enabled = true;
			deathCnt++;
			print(deathCnt);

		}
	}

	public static int GetDeathCnt () {
		return deathCnt;
	}
}
