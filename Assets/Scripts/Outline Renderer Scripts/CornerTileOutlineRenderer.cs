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

	Dictionary<QuadrilateralVertex, Vector2> vertices;
	Dictionary<PolygonSide, List<QuadrilateralVertex>> sides;

	public SpriteLayerLayout spriteLayerLayout;	

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
		collisionChecks = new Dictionary<PolygonSide, bool>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (gameObject.layer == 8) { //foreworld
			spriteRenderer.sortingOrder = spriteLayerLayout.foreworldPlatforms;

			topOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			leftOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			hypotenuseOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
			// leftOutline.sortingOrder = spriteLayerLayout.foreworldOutlines;
		}

		if (gameObject.layer == 9) { //backworld
			spriteRenderer.sortingOrder = spriteLayerLayout.backworldPlatforms;

			topOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			leftOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			hypotenuseOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
			// leftOutline.sortingOrder = spriteLayerLayout.backworldOutlines;
		}

		
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


		Debug.DrawRay(topVectors[0], topVectors[1]/raycastGizmoScale, Color.red, 1000f);
		
		// origin, direction, size
		RaycastHit2D topHit = Physics2D.Raycast(topVectors[0], topVectors[1], raycastScale, layerMask);
        
		if (topHit.collider != null && topHit.collider.gameObject.layer == gameObject.layer) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Top, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Top, false);
		}

		Vector2[] leftVectors = GetLeftOutlineVector();

		Debug.DrawRay(leftVectors[0], leftVectors[1]/raycastGizmoScale, Color.black, 1000f);
		
		// origin, direction, size
		RaycastHit2D leftHit = Physics2D.Raycast(leftVectors[0], leftVectors[1], raycastScale, layerMask);
        
		if (leftHit.collider != null && leftHit.collider.gameObject.layer == gameObject.layer) {
            // print(gameObject.name + " leftHit: " + leftHit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Left, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Left, false);
		}
		
		
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
		float rotationZ = transform.eulerAngles.z;

		int rotationZRemainder = (int) (rotationZ / 90.0) % 4;


		if (gameObject.name.Equals("Backworld Plain Angled Block (a)")) {
			print("Rotation: " +rotationZ);
			print("rotationZRemainder for left vector: " + rotationZRemainder);
		}
		
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
			returnVectors[0] = new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y);
			returnVectors[1] = new Vector2(-1, 0);

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
