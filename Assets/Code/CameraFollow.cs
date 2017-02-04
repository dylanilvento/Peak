using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target;

	Transform player;
	Vector2 relPos;
	Camera camera;
	public bool canMove = true;

	float upVal = 2f;
	float downVal = 4f;
	// Use this for initialization
	void Start () {
		player = target.transform;
		camera = GetComponent<Camera>();
		relPos = player.position - transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
		if (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal) StartCoroutine("MoveUp");
		else if (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal) StartCoroutine("MoveDown");
	}

	IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal && canMove) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator MoveDown () {
		while (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal && canMove) {
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
