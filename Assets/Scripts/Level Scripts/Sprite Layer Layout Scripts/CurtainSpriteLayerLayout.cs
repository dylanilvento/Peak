using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainSpriteLayerLayout : MonoBehaviour {

	public SpriteLayerLayout spriteLayerLayout;
	void Start () {
		
		GetComponent<SpriteRenderer>().sortingOrder = spriteLayerLayout.curtain;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
