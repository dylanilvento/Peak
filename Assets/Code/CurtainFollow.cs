using UnityEngine;
using System.Collections;

public class CurtainFollow : MonoBehaviour {
	// public GameObject target;
	Transform player;
	LevelControl levelControl;
	public Camera camera;
	// Vector2 relPos;
	float yDist;
	// Camera camera;
	// Use this for initialization
	void Start () {
		//old
		// player = GameObject.Find("Scout").transform;
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		//new
		player = levelControl.GetPlayer().transform;

		// camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		// relPos = player.position - transform.position;
		yDist = Mathf.Abs(player.position.y) + transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, player.position.y + yDist, transform.position.z);
		// StartCoroutine("MoveUp");
	}

	void FixedUpdate () {
		// Vector2 screenPos = camera.WorldToScreenPoint(transform.position);
		// print (screenPos);
	}

	// IEnumerator MoveUp () {
	// 	while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
	// 		transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
	// 		yield return new WaitForSeconds(0.1f);
	// 	}
	// }
}
