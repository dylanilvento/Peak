using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCastTest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		int layerMask = 1 << 9;

		ContactFilter2D contactFilter = new ContactFilter2D();

		contactFilter.layerMask = layerMask;

		RaycastHit2D[] results = new RaycastHit2D[1];

		GetComponent<Collider2D>().Cast(Vector2.zero, contactFilter, results, 1f);

		print("Printing results");

		foreach (RaycastHit2D result in results) {
			print(result.collider.gameObject.name);
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
