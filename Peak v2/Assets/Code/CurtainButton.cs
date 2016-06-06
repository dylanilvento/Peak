using UnityEngine;
using System.Collections;

public class CurtainButton : MonoBehaviour {

	public Sprite upButton, downButton;
	public string buttonName;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(buttonName)) {
			sr.sprite = downButton;
		}
		else {
			sr.sprite = upButton;
		}
	}
}
