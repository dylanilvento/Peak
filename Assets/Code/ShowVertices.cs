using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVertices : MonoBehaviour {

	// Use this for initialization

	public Vector2[] vertices;
	void Start () {
		vertices = gameObject.GetComponent<SpriteRenderer>().sprite.vertices;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
