using UnityEngine;
using System.Collections;

public class RollingMountains : MonoBehaviour {

	GameObject camera, player;
	float relPosY;
	public float moveMultiplier;
	Finish finish;

	LevelControl levelControl;

	Vector2 relPos;
	// Use this for initialization
	void Start () {
		// QualitySettings.vSyncCount = 0;
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		player = levelControl.GetPlayer();
		camera = GameObject.Find("Main Camera");
		relPosY = camera.transform.position.y - transform.position.y;
		finish = GameObject.Find("Flag Group").GetComponent<Finish>();
		
		relPos = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (!finish.winActive && levelControl.GetFollow()) {
			transform.position = new Vector2 ((player.transform.position.x - relPos.x) * moveMultiplier, transform.position.y);
			
			// transform.position = new Vector3 (transform.position.x, camera.transform.position.y - relPos.y, transform.position.z);
			
			if (transform.position.y < camera.transform.position.y - relPosY) StartCoroutine("MoveUp");
			else if (transform.position.y > camera.transform.position.y - relPosY) StartCoroutine("MoveDown");
			
		}

	}

	IEnumerator MoveUp () {
		// while (transform.position.y < camera.transform.position.y - relPosY) {
			transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
			yield return new WaitForSeconds(0.001f);
		// }
	}

	IEnumerator MoveDown () {
		// while (transform.position.y < camera.transform.position.y - relPosY) {
			transform.position = new Vector2(transform.position.x, transform.position.y - 0.01f);
			yield return new WaitForSeconds(0.001f);
		// }
	}
}
