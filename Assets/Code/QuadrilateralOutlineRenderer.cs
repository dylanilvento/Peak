using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrilateralOutlineRenderer : MonoBehaviour {
	// public Material lineMat;
	public Vector2[] spriteVertices;
	float scrollSpeed = 0.5f;

	//top, right, bottom, left
	bool[,] collisionMatrix = new bool[16,4] {
		{false, false, false, false}, //1, if this one, start vertex much be also included at end
		{false, false, false, true}, //1
		{false, false, true, false}, //2
		{false, false, true, true}, //1
		{false, true, false, false}, //2
		{false, true, false, true}, //2
		{false, true, true, false}, //2
		{false, true, true, true}, //1
		{true, false, false, false}, //1
		{true, false, false, true}, //1
		{true, false, true, false}, //2
		{true, false, true, true}, //1
		{true, true, false, false}, //1
		{true, true, false, true}, //1
		{true, true, true, false}, //1
		{true, true, true, true} //0
	};

	int[] lrPerCollision = new int[16] {
		1,
		1,
		2,
		1,
		2,
		2,
		2,
		1,
		1,
		1,
		2,
		1,
		1,
		1,
		1,
		0
	};

	Dictionary<QuadrilateralVertex, Vector2> vertices;
	Dictionary<PolygonSide, List<QuadrilateralVertex>> sides;

	//true means there's a collider detected
	Dictionary<PolygonSide, bool> collisionChecks;

	public LineRenderer lineRenderer1, lineRenderer2;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		vertices = new Dictionary<QuadrilateralVertex, Vector2>();
		sides = new Dictionary<PolygonSide, List<QuadrilateralVertex>>();
		collisionChecks = new Dictionary<PolygonSide, bool>();

		sides.Add(PolygonSide.Top, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperLeft, QuadrilateralVertex.UpperRight});
		sides.Add(PolygonSide.Right, new List<QuadrilateralVertex> {QuadrilateralVertex.UpperRight, QuadrilateralVertex.LowerRight});
		sides.Add(PolygonSide.Bottom, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerRight, QuadrilateralVertex.LowerLeft});
		sides.Add(PolygonSide.Left, new List<QuadrilateralVertex> {QuadrilateralVertex.LowerLeft, QuadrilateralVertex.UpperLeft});

		spriteRenderer = GetComponent<SpriteRenderer>();
		// lineRenderer = GetComponent<LineRenderer>();
		spriteVertices = spriteRenderer.sprite.vertices;

		// lineRenderer.positionCount = spriteVertices.Length;

		CheckCollision();
		SetVertices();
		
		// for (int ii = 0; ii < spriteVertices.Length; ii++) {
		// 	lineRenderer.SetPosition(ii, (Vector3) spriteVertices[ii]);
		// }
	}
	
	// Update is called once per frame
	void Update () {

		float offset = Time.time * scrollSpeed;
        lineRenderer1.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		
	}

	void CheckCollision() {
		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up/4, Color.green, 100f);
		RaycastHit2D topHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up/4, 1f);
        if (topHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Top, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Top, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right/4, Color.green, 100f);
		RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right/4, 1f);
        if (rightHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Right, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Right, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down/4, Color.green, 100f);
		RaycastHit2D bottomHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down/4, 1f);
        if (bottomHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Bottom, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Bottom, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left/4, Color.green, 100f);
		RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left/4, 1f);
        if (rightHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Left, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Left, false);
		}
		
	}
	void SetVertices() {

		Vector2 upperRight = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x >= upperRight.x && vertex.y >= upperRight.y) {
				upperRight = vertex;
			}
		}
		print("upperRight: " + upperRight);

		Vector2 lowerRight = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x >= lowerRight.x && vertex.y <= lowerRight.y) {
				lowerRight = vertex;
			}
		}

		print("lowerRight: " + lowerRight);

		Vector2 upperLeft = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x <= upperLeft.x && vertex.y >= upperLeft.y) {
				upperLeft = vertex;
			}
		}

		print("upperLeft: " + upperLeft);

		Vector2 lowerLeft = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x <= lowerLeft.x && vertex.y <= lowerLeft.y) {
				lowerLeft = vertex;
			}
		}

		print("lowerLeft: " + lowerLeft);

		vertices.Add(QuadrilateralVertex.UpperRight, upperRight);
		vertices.Add(QuadrilateralVertex.LowerRight, lowerRight);
		vertices.Add(QuadrilateralVertex.LowerLeft, lowerLeft);
		vertices.Add(QuadrilateralVertex.UpperLeft, upperLeft);
		
		// lineRenderer1.positionCount = spriteVertices.Length + 1;


		

	}

	void SetLineRendererVertices () {
		
		lineRenderer1.positionCount = 2;

		Vector3[] lrVertices = {
			(Vector3) vertices[QuadrilateralVertex.UpperRight],
			(Vector3) vertices[QuadrilateralVertex.LowerRight]//,
			// (Vector3) vertices[QuadrilateralVertex.LowerLeft],
			// (Vector3) vertices[QuadrilateralVertex.UpperLeft],
			// (Vector3) vertices[QuadrilateralVertex.UpperRight]
		};

		lineRenderer1.SetPositions(lrVertices);
		

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
}
