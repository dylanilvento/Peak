using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PulseBackground : MonoBehaviour {

	public float startHeight, startWidth, endHeight, endWidth;
	RectTransform rect;

	// Use this for initialization
	void Start () {

		rect = GetComponent<RectTransform>();
		StartCoroutine("Pulse");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Pulse () {
		// bool fadeStarted = false;
		Image img = GetComponent<Image>();

		while (true) {

			// if (!fadeStarted) {
			// 	// print ()
			// 	// Transparency.DownFade(imgColor);
			// 	fadeStarted = !fadeStarted;
			// 	// print("fade started");
			// }

			// rect.rect = new Vector2(rect.sizeDelta.x + 5f, rect.sizeDelta.y);
			// rect.sizeDelta = new Vector2(rect.sizeDelta.x + 1f, rect.sizeDelta.y + 1f);
			transform.localScale = new Vector3(transform.localScale.x + 0.05f, transform.localScale.y + 0.05f, transform.localScale.z + 0.05f);
			Transparency.SetOpacity(gameObject, img.color.a - 0.05f);
			print(img.color.a);
			yield return new WaitForSeconds(0.07f);

			// if (new Vector2(rect.rect.width, rect.rect.height) == new Vector2(endWidth, endHeight)) {
			if (transform.localScale.x >= endWidth) {	
				// rect.sizeDelta = new Vector2(startWidth, startHeight);
				transform.localScale = new Vector3(startWidth, startHeight, startHeight);
				Transparency.SetOpacity(gameObject, 0.5f);
				// fadeStarted = false;
			}
		}
	}

}
