using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrilateralOutlineRenderer : MonoBehaviour {
	// public Material lineMat;
	public Vector2[] spriteVertices;
	float scrollSpeed = 0.5f;

	Dictionary<QuadrilateralVertex, Vector2> vertices;
	Dictionary<PolygonSide, List<QuadrilateralVertex>> sides;

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
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (transform.localScale.y/3)), Vector2.up/4, 1f);
        if (hit.collider != null) {
            print(hit.collider.gameObject.name);
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
}
