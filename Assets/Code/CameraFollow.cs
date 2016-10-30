using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	Transform player;
	Vector2 relPos;
	Camera camera;
	public bool canMove = true;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Scout").transform;
		camera = GetComponent<Camera>();
		relPos = player.position - transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
		if (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) StartCoroutine("MoveUp");
		else if (camera.WorldToScreenPoint(player.position).y < Screen.height / 1.75f) StartCoroutine("MoveDown");
	}

	IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f && canMove) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator MoveDown () {
		while (camera.WorldToScreenPoint(player.position).y < Screen.height / 4f && canMove) {
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
