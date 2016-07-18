using UnityEngine;
using System.Collections;

public class Backworld : MonoBehaviour {

	GameObject collidedWith;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		//sr.color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		collidedWith = other.gameObject;
		// print("test");

		if (collidedWith.name.Equals("Curtain Collider")) {
			//sr.color = new Color(1f, 1f, 1f, 1f);
		}

	}

	void OnTriggerStay2D (Collider2D other) {
	 	collidedWith = other.gameObject;
	 	if (collidedWith != null && collidedWith.name.Equals("Curtain Collider")) {
	 		//sr.color = new Color(1f, 1f, 1f, 1f);
	 	}
	 }

	void OnTriggerExit2D (Collider2D other) {
		//collidedWith = other.gameObject;

		if (collidedWith.name.Equals("Curtain Collider")) {
			//sr.color = new Color(1f, 1f, 1f, 0f);
			collidedWith = null;
		}

	}
}
