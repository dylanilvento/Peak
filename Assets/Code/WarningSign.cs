using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour {

	public GameObject warningSign;
	// SpriteRenderer sign;

	// Use this for initialization
	void Start () {
		// sign = warningSign.GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("Scout Mega Group")) {
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
