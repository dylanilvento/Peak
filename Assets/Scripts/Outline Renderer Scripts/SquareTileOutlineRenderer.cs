﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTileOutlineRenderer : MonoBehaviour {
	// public Material lineMat;
	public Vector2[] spriteVertices;
	float scrollSpeed = 0.25f;

	public Sprite transitionBlock;

	float raycastScale = 0.5f;
	float raycastGizmoScale = 5f;

	public SpriteLayerLayout spriteLayerLayout;


	PolygonSide[] sideRotationOrder = new PolygonSide[4] {
		PolygonSide.Top,
		PolygonSide.Right,
		PolygonSide.Bottom,
		PolygonSide.Left
	};

	Dictionary<QuadrilateralVertex, Vector2> vertices;
	Dictionary<PolygonSide, List<QuadrilateralVertex>> sides;

	//true means there's a collider detected
	Dictionary<PolygonSide, bool> collisionChecks;

	BoxCollider2D boxCollider;

	Rigidbody2D rigidbody;

	// public LineRenderer lineRenderer1, lineRenderer2;

	public LineRenderer topOutline, rightOutline, bottomOutline, leftOutline;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {

		boxCollider = GetComponent<BoxCollider2D>();
		rigidbody = GetComponent<Rigidbody2D>();

		vertices = new Dictionary<QuadrilateralVertex, Vector2>();
		sides = new Dictionary<PolygonSide, List<QuadrilateralVertex>>();
		collisionChecks = new Dictionary<PolygonSide, bool>();

		sides.Add(PolygonSide.Top, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperLeft, QuadrilateralVertex.UpperRight});
		sides.Add(PolygonSide.Right, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperRight, QuadrilateralVertex.LowerRight});
		sides.Add(PolygonSide.Bottom, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerRight, QuadrilateralVertex.LowerLeft});
		sides.Add(PolygonSide.Left, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerLeft, QuadrilateralVertex.UpperLeft});

		spriteRenderer = GetComponent<SpriteRenderer>();

		if (gameObject.layer == 8) { //foreworld
			spriteRenderer.sortingOrder = spriteLayerLayout.foreworldPlatforms;

			topOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			rightOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			bottomOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			leftOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
		}

		if (gameObject.layer == 9) { //backworld
			spriteRenderer.sortingOrder = spriteLayerLayout.backworldPlatforms;

			topOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			rightOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			bottomOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			leftOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
		}

		// lineRenderer = GetComponent<LineRenderer>();
		spriteVertices = spriteRenderer.sprite.vertices;

		// lineRenderer.positionCount = spriteVertices.Length;

		/** THIS IS FOR TRANSITION BLOCK
			TO REENABLE TRANSITION BLOCKS, YOU MUST READD RIGIDBODY2D
			AND FREEZE ROTATION / POSITION
		 */
		// CheckMatchingTile();


		CheckCollision();
		// SetVertices();
		// SetLineRendererVertices();

		ActivateOutlines();
		
	}
	
	// Update is called once per frame
	void Update () {

		float offset = Time.time * scrollSpeed;
        if (topOutline != null) topOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		if (rightOutline != null) rightOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		if (bottomOutline != null) bottomOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		if (leftOutline != null) leftOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	
		
	}

	void CheckMatchingTile() {
		int otherCollisionLayer = gameObject.layer == 8 ? 9 : 8;

		int layerMask = 1 << otherCollisionLayer;

		ContactFilter2D contactFilter = new ContactFilter2D();

		contactFilter.layerMask = layerMask;

		RaycastHit2D[] results = new RaycastHit2D[10];

		GetComponent<Collider2D>().Cast(Vector2.zero, contactFilter, results, 1f);

		foreach (RaycastHit2D result in results) {
			// print(result.collider.gameObject.name);
			if (result.collider != null && result.collider.gameObject.layer == otherCollisionLayer) {
				GetComponent<SpriteRenderer>().sprite = transitionBlock;
				break;
			}
		}

		Destroy(rigidbody);

	}

	void CheckCollision() {
		// Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up/raycastGizmoScale, Color.green, 10f);
		
		int layerMask = 1 << gameObject.layer;

		// origin, direction, size
		RaycastHit2D topHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up, raycastScale, layerMask);
        
		if (topHit.collider != null && topHit.collider.gameObject.layer == gameObject.layer && !(topHit.collider.gameObject.tag.Equals("Player"))) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Top, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Top, false);
		}

		// Debug.DrawRay(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right, raycastScale, layerMask);

		if (rightHit.collider != null && rightHit.collider.gameObject.layer == gameObject.layer) {
            // print(gameObject.name + "rightHit: " + rightHit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Right, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Right, false);
		}

		// Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D bottomHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down, raycastScale, layerMask);
       
	    if (bottomHit.collider != null && bottomHit.collider.gameObject.layer == gameObject.layer) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Bottom, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Bottom, false);
		}

		// Debug.DrawRay(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left, raycastScale, layerMask);
        
		if (leftHit.collider != null && leftHit.collider.gameObject.layer == gameObject.layer) {
            // print(gameObject.name + " leftHit: " + leftHit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Left, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Left, false);
		}

		// if (gameObject.name.Equals("Backworld Plain Block")) {
		// 	print(gameObject.name);
		// 	print("top: " + collisionChecks[PolygonSide.Top]);
		// 	print("right: " + collisionChecks[PolygonSide.Right]);
		// 	print("bottom: " + collisionChecks[PolygonSide.Bottom]);
		// 	print("left: " + collisionChecks[PolygonSide.Left]);
		// }

		
		
	}

	void ActivateOutlines () {
		if (!collisionChecks[PolygonSide.Top]) {
			topOutline.enabled = true;
		}
		else {
			// topOutline.enabled = false;
			Destroy(topOutline);
		}

		if (!collisionChecks[PolygonSide.Right]) {
			rightOutline.enabled = true;
		}
		else {
			// rightOutline.enabled = false;
			Destroy(rightOutline);
		}

		if (!collisionChecks[PolygonSide.Bottom]) {
			bottomOutline.enabled = true;
		}
		else {
			// bottomOutline.enabled = false;
			Destroy(bottomOutline);
		}

		if (!collisionChecks[PolygonSide.Left]) {
			leftOutline.enabled = true;
		}
		else {
			// leftOutline.enabled = false;
			Destroy(leftOutline);
		}

		StartCoroutine("TurnOffCollider");

	}

	IEnumerator TurnOffCollider () {
		yield return new WaitForSeconds(0.5f);
		boxCollider.enabled = false;
	}


	bool ArrayComparison (int[] arr1, int[] arr2) {
		bool arraysAreEqual = true;
		
		if (arr1.Length != arr2.Length) {
			return false;
		}

		else {
			for (int ii = 0; ii < arr1.Length && arraysAreEqual; ii++) {
				if (arr1[ii] != arr2[ii]) {
					arraysAreEqual = false;
				}
			}
		}

		return arraysAreEqual;
	}

	bool ArrayComparison (bool[] arr1, bool[] arr2) {
		bool arraysAreEqual = true;
		
		if (arr1.Length != arr2.Length) {
			return false;
		}

		else {
			for (int ii = 0; ii < arr1.Length && arraysAreEqual; ii++) {
				if (arr1[ii] != arr2[ii]) {
					arraysAreEqual = false;
				}
			}
		}

		return arraysAreEqual;
	}
}
