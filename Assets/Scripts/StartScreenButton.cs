using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenButton : MonoBehaviour {

	public Sprite up, down;
	Image img;
	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUpButton () {
		img.sprite = up;
	}

	public void SetDownButton () {
		img.sprite = down;
	}
}
