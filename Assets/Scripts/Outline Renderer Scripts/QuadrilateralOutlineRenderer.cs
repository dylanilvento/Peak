using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrilateralOutlineRenderer : MonoBehaviour {
	// public Material lineMat;
	public Vector2[] spriteVertices;
	float scrollSpeed = 0.5f;

	//top, right, bottom, left
	//true means raycast hit another collider

	float raycastScale = 0.5f;
	float raycastGizmoScale = 5f;


	List<bool[]> collisionMatrices = new List <bool[]> {
		new bool[] {false, false, false, false}, //1, if this one, start vertex much be also included at end
		new bool[] {false, false, false, true}, //1
		new bool[] {false, false, true, false}, //2
		new bool[] {false, false, true, true}, //1
		new bool[] {false, true, false, false}, //2
		new bool[] {false, true, false, true}, //2
		new bool[] {false, true, true, false}, //2
		new bool[] {false, true, true, true}, //1
		new bool[] {true, false, false, false}, //1
		new bool[] {true, false, false, true}, //1
		new bool[] {true, false, true, false}, //2
		new bool[] {true, false, true, true}, //1
		new bool[] {true, true, false, false}, //1
		new bool[] {true, true, false, true}, //1
		new bool[] {true, true, true, false}, //1
		new bool[] {true, true, true, true} //0
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

	// public LineRenderer lineRenderer1, lineRenderer2;

	public LineRenderer topOutline, rightOutline, bottomOutline, leftOutline;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		boxCollider = gameObject.GetComponent<BoxCollider2D>();
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
		// SetVertices();
		// SetLineRendererVertices();

		ActivateOutlines();
		
		// for (int ii = 0; ii < spriteVertices.Length; ii++) {
		// 	lineRenderer.SetPosition(ii, (Vector3) spriteVertices[ii]);
		// }
	}
	
	// Update is called once per frame
	void Update () {

		float offset = Time.time * scrollSpeed;
        topOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		rightOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		bottomOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		leftOutline.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
		
	}

	void CheckCollision() {
		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D topHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up, raycastScale);
        
		if (topHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Top, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Top, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x/3), transform.position.y), Vector2.right, raycastScale);

		if (rightHit.collider != null) {
            // print(gameObject.name + "rightHit: " + rightHit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Right, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Right, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D bottomHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/3)), Vector2.down, raycastScale);
       
	    if (bottomHit.collider != null) {
            // print(hit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Bottom, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Bottom, false);
		}

		Debug.DrawRay(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left/raycastGizmoScale, Color.green, 10f);
		
		// origin, direction, size
		RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x/3), transform.position.y), Vector2.left, raycastScale);
        
		if (leftHit.collider != null) {
            // print(gameObject.name + " leftHit: " + leftHit.collider.gameObject.name);
			collisionChecks.Add(PolygonSide.Left, true);
        }
		else {
			collisionChecks.Add(PolygonSide.Left, false);
		}

		print(gameObject.name);
		print("top: " + collisionChecks[PolygonSide.Top]);
		print("right: " + collisionChecks[PolygonSide.Right]);
		print("bottom: " + collisionChecks[PolygonSide.Bottom]);
		print("left: " + collisionChecks[PolygonSide.Left]);
		
	}

	void ActivateOutlines () {
		if (!collisionChecks[PolygonSide.Top]) {
			topOutline.enabled = true;
		}
		else {
			topOutline.enabled = false;
		}

		if (!collisionChecks[PolygonSide.Right]) {
			rightOutline.enabled = true;
		}
		else {
			rightOutline.enabled = false;
		}

		if (!collisionChecks[PolygonSide.Bottom]) {
			bottomOutline.enabled = true;
		}
		else {
			bottomOutline.enabled = false;
		}

		if (!collisionChecks[PolygonSide.Left]) {
			leftOutline.enabled = true;
		}
		else {
			leftOutline.enabled = false;
		}
	}
	void SetVertices() {

		Vector2 upperRight = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x >= upperRight.x && vertex.y >= upperRight.y) {
				upperRight = vertex;
			}
		}
		// print("upperRight: " + upperRight);

		Vector2 lowerRight = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x >= lowerRight.x && vertex.y <= lowerRight.y) {
				lowerRight = vertex;
			}
		}

		// print("lowerRight: " + lowerRight);

		Vector2 upperLeft = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x <= upperLeft.x && vertex.y >= upperLeft.y) {
				upperLeft = vertex;
			}
		}

		// print("upperLeft: " + upperLeft);

		Vector2 lowerLeft = new Vector2(0,0);
		foreach (Vector2 vertex in spriteVertices) {
			if (vertex.x <= lowerLeft.x && vertex.y <= lowerLeft.y) {
				lowerLeft = vertex;
			}
		}

		// print("lowerLeft: " + lowerLeft);

		vertices.Add(QuadrilateralVertex.UpperRight, upperRight);
		vertices.Add(QuadrilateralVertex.LowerRight, lowerRight);
		vertices.Add(QuadrilateralVertex.LowerLeft, lowerLeft);
		vertices.Add(QuadrilateralVertex.UpperLeft, upperLeft);
		
		// lineRenderer1.positionCount = spriteVertices.Length + 1;

	}

	void SetLineRendererVertices () {
		int collisionIndex = -1;
		//create current collision matrix
		bool[] collisionMatrix = new bool[4] {
			collisionChecks[PolygonSide.Top],
			collisionChecks[PolygonSide.Right],
			collisionChecks[PolygonSide.Bottom],
			collisionChecks[PolygonSide.Left]
		};

		foreach (bool[] matrix in collisionMatrices) {
			if (ArrayComparison(collisionMatrix, matrix)) {
				collisionIndex = collisionMatrices.IndexOf(matrix);
				break;
			}
		}

		
		List<Vector3> lr1Vertices = new List<Vector3>();
		List<Vector3> lr2Vertices = new List<Vector3>();

		List<Vector3>[] vertexHolder = new List<Vector3>[2] {
			lr1Vertices,
			lr2Vertices
		};

		print("collisionIndex: " + collisionIndex);

		int vertexHolderIndex = 0;

		print("going through sides");

		if (collisionIndex == 0) {
			vertexHolder[0] = new List<Vector3> {
				vertices[QuadrilateralVertex.UpperRight],
				vertices[QuadrilateralVertex.LowerRight],
				vertices[QuadrilateralVertex.LowerLeft],
				vertices[QuadrilateralVertex.UpperLeft],
				vertices[QuadrilateralVertex.UpperRight]
			};
		}

		else {

			//this will add shared vertices TWICE
			foreach (PolygonSide side in sideRotationOrder) {
				print(side);
				print(collisionChecks[side]);
				if (collisionChecks[side] == false) {
					vertexHolder[vertexHolderIndex].Add(vertices[sides[side][0]]);
				}

				else if (collisionChecks[side] == true && vertexHolderIndex == 0) {
					vertexHolderIndex++;
				}

				else if (collisionChecks[side] == true && vertexHolderIndex >= 1) {
					break;
				}
			}
		}

		print("num of LRs: " + lrPerCollision[collisionIndex]);

		if (lrPerCollision[collisionIndex] == 1) {
			print("assigning vertices");
			// lineRenderer1.positionCount = lr1Vertices.Count;
			// lineRenderer1.SetPositions(lr1Vertices.ToArray());
			print("vertexHolder[0].Count " + vertexHolder[0].Count);

			// commented out variables at top
			// lineRenderer1.positionCount = vertexHolder[0].Count;
			// lineRenderer1.SetPositions(vertexHolder[0].ToArray());
		}

		if (lrPerCollision[collisionIndex] == 2) {
			// lineRenderer2.positionCount = lr2Vertices.Count;
			// lineRenderer2.SetPositions(lr2Vertices.ToArray());

			// commented out variables at top
			// lineRenderer2.positionCount = vertexHolder[1].Count;
			// lineRenderer2.SetPositions(vertexHolder[1].ToArray());
		}

		// boxCollider.enabled = false;


		// lineRenderer1.positionCount = 2;

		// Vector3[] lrVertices = {
		// 	(Vector3) vertices[QuadrilateralVertex.UpperRight],
		// 	(Vector3) vertices[QuadrilateralVertex.LowerRight]//,
		// 	// (Vector3) vertices[QuadrilateralVertex.LowerLeft],
		// 	// (Vector3) vertices[QuadrilateralVertex.UpperLeft],
		// 	// (Vector3) vertices[QuadrilateralVertex.UpperRight]
		// };

		// lineRenderer1.SetPositions(lrVertices);
		

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
