using UnityEngine;
using System.Collections;

public class StartBoulder : MonoBehaviour {
	public GameObject boulder;
	public float velocityX;
	public float velocityY;
	Rigidbody2D boulderRB;

	// Use this for initialization
	void Start () {
		boulderRB = boulder.GetComponent<Rigidbody2D>();
		boulderRB.gravityScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("Scout Mega Group")) {
			boulderRB.gravityScale = 1f;
			boulderRB.velocity = new Vector2(velocityX, velocityY);

			
		}
	}
}
