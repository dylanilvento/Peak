using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PulseBackground : MonoBehaviour {

	public float startHeight, startWidth, endHeight, endWidth;
	RectTransform rect;
	LevelControl levelControl;
	// Use this for initialization
	void Start () {

		rect = GetComponent<RectTransform>();
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Image>().color.a > 0f) StartCoroutine("Pulse");
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

			float rate = (endWidth - startWidth) / 20f;

			// rect.rect = new Vector2(rect.sizeDelta.x + 5f, rect.sizeDelta.y);
			// rect.sizeDelta = new Vector2(rect.sizeDelta.x + 1f, rect.sizeDelta.y + 1f);
			transform.localScale = new Vector3(transform.localScale.x + rate, transform.localScale.y + rate, transform.localScale.z + rate);
			Transparency.SetOpacity(gameObject, img.color.a - 0.04f);
			// print(img.color.a);
			yield return new WaitForSeconds(0.07f);

			// if (new Vector2(rect.rect.width, rect.rect.height) == new Vector2(endWidth, endHeight)) {
			if (transform.localScale.x >= endWidth) {	
				// rect.sizeDelta = new Vector2(startWidth, startHeight);
				transform.localScale = new Vector3(startWidth, startHeight, startHeight);
				Transparency.SetOpacity(gameObject, 0.75f);
				// fadeStarted = false;
			}
		}
	}

}
