using UnityEngine;
using System.Collections;

public class Foreworld : MonoBehaviour {

	GameObject collidedWith;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		sr.sortingOrder = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		collidedWith = other.gameObject;

	/*	if (collidedWith.name.Equals("Curtain Collider")) {
			sr.color = new Color(1f, 1f, 1f, 0f);
		}*/

	}

	void OnTriggerStay2D (Collider2D other) {
		/*if (collidedWith.name.Equals("Curtain Collider")) {
			sr.color = new Color(1f, 1f, 1f, 0f);
		}*/
	}

	void OnTriggerExit2D (Collider2D other) {
		//collidedWith = other.gameObject;

		/*if (collidedWith.name.Equals("Curtain Collider")) {
			sr.color = new Color(1f, 1f, 1f, 1f);
		}*/

		collidedWith = null;

	}
}
