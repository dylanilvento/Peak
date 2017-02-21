using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target;

	Transform player;
	Vector2 relPos;
	Camera camera;
	public bool canMove = true;

	LevelControl levelControl;

	float upVal = 1.4f;
	float downVal = 4f;
	// Use this for initialization
	void Start () {
		player = target.transform;
		camera = GetComponent<Camera>();
		relPos = player.position - transform.position;
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (levelControl.GetFollow()) {
			transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
			if (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal) StartCoroutine("MoveUp");
			else if (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal) StartCoroutine("MoveDown");
		}
	}

	IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal && levelControl.GetFollow()) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator MoveDown () {
		while (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal && levelControl.GetFollow()) {
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
