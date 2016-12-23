using UnityEngine;
using System.Collections;

public class CurtainFollow : MonoBehaviour {
	public GameObject target;
	Transform player;
	// Vector2 relPos;
	float yDist;
	// Camera camera;
	// Use this for initialization
	void Start () {
		//old
		// player = GameObject.Find("Scout").transform;
		
		//new
		player = target.transform;

		// camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		// relPos = player.position - transform.position;
		yDist = Mathf.Abs(player.position.y) + transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, player.position.y + yDist, transform.position.z);
		// StartCoroutine("MoveUp");
	}

	// IEnumerator MoveUp () {
	// 	while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
	// 		transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
	// 		yield return new WaitForSeconds(0.1f);
	// 	}
	// }
}
