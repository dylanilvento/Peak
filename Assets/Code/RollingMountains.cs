using UnityEngine;
using System.Collections;

public class RollingMountains : MonoBehaviour {

	GameObject camera;
	float relPosY;
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		// QualitySettings.vSyncCount = 0;
		camera = GameObject.Find("Main Camera");
		relPosY = camera.transform.position.y - transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector2 (transform.position.x + moveSpeed, transform.position.y);
		if (transform.position.y < camera.transform.position.y - relPosY) StartCoroutine("MoveUp");

	}

	IEnumerator MoveUp () {
		// while (transform.position.y < camera.transform.position.y - relPosY) {
			transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
			yield return new WaitForSeconds(0.001f);
		// }
	}
}
