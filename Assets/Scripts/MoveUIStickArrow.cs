using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIStickArrow : MonoBehaviour {
	Vector3 origin;
	// Use this for initialization
	void Start () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		 transform.Translate(Time.deltaTime * -transform.localScale.x * 50f, 0, 0);

		 if (Vector2.Distance(transform.position, origin) > 15f) {
			 transform.position = origin;
		 }
	}
}
