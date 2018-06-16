using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectsSpriteLayerLayout : MonoBehaviour {

	// Use this for initialization
	public SpriteLayerLayout spriteLayerLayout;
	void Start () {
		if (gameObject.layer == 8) { //fore world
			GetComponent<SpriteRenderer>().sortingOrder = Random.Range(spriteLayerLayout.foreworldBackgroundObjectsLowerBound, spriteLayerLayout.foreworldBackgroundObjectsUpperBound + 1);
		}
		else if (gameObject.layer == 9) { //back world
			GetComponent<SpriteRenderer>().sortingOrder = Random.Range(spriteLayerLayout.backworldBackgroundObjectsLowerBound, spriteLayerLayout.backworldBackgroundObjectsUpperBound + 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
