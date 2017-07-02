using UnityEngine;
using System.Collections;

public class GameOverColliderMover : MonoBehaviour {

	public GameObject goCollider;
	public float dist;

	GameObject target;

	LevelControl levelControl;
	// Use this for initialization
	void Start () {
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		target = levelControl.GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject == target) {
			goCollider.transform.position = new Vector2 (goCollider.transform.position.x, goCollider.transform.position.y + dist);

		}
	}
}
