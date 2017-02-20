using UnityEngine;
using System.Collections;

public class RollingMountains : MonoBehaviour {

	GameObject camera, player;
	float relPosY;
	public float moveMultiplier;
	Finish finish;

	Vector2 relPos;
	// Use this for initialization
	void Start () {
		// QualitySettings.vSyncCount = 0;
		player = GameObject.Find("Scout Mega Group");
		camera = GameObject.Find("Main Camera");
		relPosY = camera.transform.position.y - transform.position.y;
		finish = GameObject.Find("Flag Group").GetComponent<Finish>();

		relPos = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (!finish.winActive) {
			transform.position = new Vector2 ((player.transform.position.x - relPos.x) * moveMultiplier, transform.position.y);
			// transform.position = new Vector3 (transform.position.x, camera.transform.position.y - relPosY, transform.position.z);
			if (transform.position.y < camera.transform.position.y - relPosY) StartCoroutine("MoveUp");
			// if (Mathf.Abs(camera.transform.position.y - transform.position.y) > relPosY) StartCoroutine("MoveUp");
			else if (transform.position.y > camera.transform.position.y - relPosY) StartCoroutine("MoveDown");
			// else if (Mathf.Abs(camera.transform.position.y - transform.position.y) < relPosY) StartCoroutine("MoveDown");
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
