using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTileOutlineRenderer : MonoBehaviour {
	// public Material lineMat;
	public Vector2[] spriteVertices;
	float scrollSpeed = 0.25f;

	//top, right, bottom, left
	//true means raycast hit another collider

	float raycastScale = 0.5f;
	float raycastGizmoScale = 5f;


	// PolygonSide[] sideRotationOrder = new PolygonSide[4] {
	// 	PolygonSide.Top,
	// 	PolygonSide.Right,
	// 	PolygonSide.Bottom,
	// 	PolygonSide.Left
	// };

	Dictionary<QuadrilateralVertex, Vector2> vertices;
	Dictionary<PolygonSide, List<QuadrilateralVertex>> sides;

	//true means there's a collider detected
	Dictionary<PolygonSide, bool> collisionChecks;

	PolygonCollider2D polygonCollider;

	// public LineRenderer lineRenderer1, lineRenderer2;

	public LineRenderer topOutline, leftOutline, hypotenuseOutline;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
		vertices = new Dictionary<QuadrilateralVertex, Vector2>();
		// sides = new Dictionary<PolygonSide, List<QuadrilateralVertex>>();
		collisionChecks = new Dictionary<PolygonSide, bool>();

		// sides.Add(PolygonSide.Top, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperLeft, QuadrilateralVertex.UpperRight});
		// sides.Add(PolygonSide.Right, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperRight, QuadrilateralVertex.LowerRight});
		// sides.Add(PolygonSide.Bottom, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerRight, QuadrilateralVertex.LowerLeft});
		// sides.Add(PolygonSide.Left, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerLeft, QuadrilateralVertex.UpperLeft});

		spriteRenderer = GetComponent<SpriteRenderer>();
		// lineRenderer = GetComponent<LineRenderer>();
		spriteVertices = spriteRenderer.sprite.vertices;

		CheckCollision();

		ActivateOutlines();
		
	}
	
	// Update is called once per frame
	void Update () {

		float offset = Time.time * scrollSpeed;
        if (topOutline != null) topOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		// if (rightOutline != null) rightOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		// if (bottomOutline != null) bottomOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		if (leftOutline != null) leftOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		if (hypotenuseOutline != null) hypotenuseOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		
	}

	void CheckCollision() {

		int layerMask = 1 << gameObject.layer;

		Vector2[] topVectors = GetTopOutlineVector();


		Debug.DrawRay(topVectors[0], topVectors[1]/raycastGizmoScale, Color.blue, 100f);
		
		// origin, direction, size
		RaycastHit2D topHit = Physics2D.Raycast(topVectors[0], topVectors[1], raycastScale, layerMask);
        
		if (topHit.collider != null && topHit.collider.gameObject.layer == gameObject.layer) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Top, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Top, false);
		}

		// Debug.DrawRay(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right/raycastGizmoScale, Color.green, 10f);
		
		// // origin, direction, size
		// RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right, raycastScale);

		// if (rightHit.collider != null && rightHit.collider.gameObject.layer == gameObject.layer) {
        //     // print(gameObject.name + "rightHit: " + rightHit.collider.gameObject.name);
		// 	collisionChecks.Add(PolygonSide.Right, true);
        // }
		// else {
		// 	collisionChecks.Add(PolygonSide.Right, false);
		// }

		// Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down/raycastGizmoScale, Color.green, 10f);
		
		// // origin, direction, size
		// RaycastHit2D bottomHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down, raycastScale);
       
	    // if (bottomHit.collider != null && bottomHit.collider.gameObject.layer == gameObject.layer) {
        //     // print(hit.collider.gameObject.name);
		// 	collisionChecks.Add(PolygonSide.Bottom, true);
        // }
		// else {
		// 	collisionChecks.Add(PolygonSide.Bottom, false);
		// }

		Vector2[] leftVectors = GetLeftOutlineVector();

		Debug.DrawRay(leftVectors[0], leftVectors[1]/raycastGizmoScale, Color.green, 100f);
		
		// origin, direction, size
		RaycastHit2D leftHit = Physics2D.Raycast(leftVectors[0], leftVectors[1], raycastScale, layerMask);
        
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

		// if (!collisionChecks[PolygonSide.Right]) {
		// 	rightOutline.enabled = true;
		// }
		// else {
		// 	// rightOutline.enabled = false;
		// 	Destroy(rightOutline);
		// }

		// if (!collisionChecks[PolygonSide.Bottom]) {
		// 	bottomOutline.enabled = true;
		// }
		// else {
		// 	// bottomOutline.enabled = false;
		// 	Destroy(bottomOutline);
		// }

		if (!collisionChecks[PolygonSide.Left]) {
			leftOutline.enabled = true;
			// print("left outline true");
		}
		else {
			// leftOutline.enabled = false;
			Destroy(leftOutline);
			// print("left outline false");
		}

		StartCoroutine("TurnOffCollider");

	}

	Vector2[] GetTopOutlineVector () {

		Vector2[] returnVectors = new Vector2[2];

		float rotationZ = transform.eulerAngles.z;

		// print("rotationZ: " + rotationZ);

		int rotationZRemainder = (int) (rotationZ / 90.0) % 4;

		// print("rotationZRemainder: " + rotationZRemainder);
		
		//top
		if (rotationZRemainder == 0) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, 1);

			return returnVectors;
		}
		//left
		else if (rotationZRemainder == 1) {
			returnVectors[0] = new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(-1, 0);

			return returnVectors;
		}
		//bottom
		else if (rotationZRemainder == 2) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, -1);

			return returnVectors;
		}
		//right
		else if (rotationZRemainder == 3) {
			returnVectors[0] = new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(1, 0);

			return returnVectors;
		}
		//right
		else if (rotationZRemainder == -1) {
			returnVectors[0] = new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(1, 0);

			return returnVectors;
		}
		//bottom
		else if (rotationZRemainder == -2) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, -1);

			return returnVectors;
		}
		//left
		else if (rotationZRemainder == -3) {
			returnVectors[0] = new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(-1, 0);

			return returnVectors;
		}
		else {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, 1);

			return returnVectors;
		}

	}

	Vector2[] GetLeftOutlineVector () {
		Vector2[] returnVectors = new Vector2[2];
		float rotationZ = transform.rotation.z;

		int rotationZRemainder = (int) (rotationZ / 90.0) % 4;
		
		//left
		if (rotationZRemainder == 0) {
			returnVectors[0] = new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(-1, 0);

			return returnVectors;
		}
		//bottom
		else if (rotationZRemainder == 1) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, -1);

			return returnVectors;
		}
		//right
		else if (rotationZRemainder == 2) {
			returnVectors[0] = new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(1, 0);

			return returnVectors;
		}
		//top
		else if (rotationZRemainder == 3) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, 1);

			return returnVectors;
		}
		//top
		else if (rotationZRemainder == -1) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, 1);

			return returnVectors;
		}
		//right
		else if (rotationZRemainder == -2) {
			returnVectors[0] = new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(1, 0);

			return returnVectors;
		}
		//bottom
		else if (rotationZRemainder == -3) {
			returnVectors[0] = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3));
			returnVectors[1] = new Vector2(0, -1);

			return returnVectors;
		}
		//left
		else {
			returnVectors[0] = new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(-1, 0);

			return returnVectors;
		}

	}

	IEnumerator TurnOffCollider () {
		yield return new WaitForSeconds(0.5f);
		polygonCollider.enabled = false;
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
