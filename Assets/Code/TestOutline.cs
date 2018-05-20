using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOutline : MonoBehaviour {

	public Vector2[] spriteVertices;
	// public GameObject lr;
	LineRenderer lineRenderer;

	float scrollSpeed = 0.5f;

	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		// spriteRenderer = GetComponent<SpriteRenderer>();
		// spriteVertices = spriteRenderer.sprite.vertices;
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		
	}
}
