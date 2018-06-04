﻿using UnityEngine;
using System.Collections;

public class GeyserAirMovement : MonoBehaviour {

	public SpriteLayerLayout spriteLayerLayout;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sortingOrder = (gameObject.layer == 8) ? spriteLayerLayout.foreworldEffects : spriteLayerLayout.backworldEffects;
		StartCoroutine("WaitAndDestroy");
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.up * 4 * Time.deltaTime);
	}

	IEnumerator WaitAndDestroy () {
		float t = Random.Range(0.3f, 0.6f);

		yield return new WaitForSeconds(t);

		Destroy(gameObject);
	}
}
