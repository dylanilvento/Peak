using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour {

	public GameObject warningSign;
	// SpriteRenderer sign;
	GameObject player;
	LevelControl levelControl;
	// Use this for initialization
	void Start () {
		// sign = warningSign.GetComponent<SpriteRenderer>();
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		player = levelControl.GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		print("hit warning");
		if (other.gameObject == player) {
			print("hit warning 2");
			// boulderRB.gravityScale = 1f;
			// boulderRB.velocity = new Vector2(velocityX, velocityY);

			// if (showWarning) {
			StartCoroutine("FlashWarning");
			// }
		}
	}

	IEnumerator FlashWarning () {
		float t = 4f;

		while (t > 0f) {
			Transparency.SetOpacity(warningSign, 1f);
			yield return new WaitForSeconds(0.25f);
			Transparency.SetOpacity(warningSign, 0f);
			yield return new WaitForSeconds(0.25f);
			t -= 0.5f;
		}
	}
}
