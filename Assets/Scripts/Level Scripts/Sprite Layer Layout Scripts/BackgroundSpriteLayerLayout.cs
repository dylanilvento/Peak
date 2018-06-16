using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpriteLayerLayout : MonoBehaviour {

	// Use this for initialization
	public SpriteLayerLayout spriteLayerLayout;
	void Start () {
		if (gameObject.layer == 8) { //fore world
			GetComponent<SpriteRenderer>().sortingOrder = spriteLayerLayout.foreworldBackground;
		}
		else if (gameObject.layer == 9) { //back world
			GetComponent<SpriteRenderer>().sortingOrder = spriteLayerLayout.backworldBackground;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
