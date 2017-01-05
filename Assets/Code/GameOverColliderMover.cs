using UnityEngine;
using System.Collections;

public class GameOverColliderMover : MonoBehaviour {

	public GameObject goCollider;
	public float dist;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("Scout Group")) {
			goCollider.transform.position = new Vector2 (goCollider.transform.position.x, goCollider.transform.position.y + dist);

		}
	}
}
