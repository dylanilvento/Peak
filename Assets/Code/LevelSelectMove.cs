using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMove : MonoBehaviour {

	// Use this for initialization

	public GameObject path;
	public Vector3[] pathVertices;

	bool startMovement = false;
	float startTime;
	float journeyLength;
	float speed = 3f;

	int currIndex = 0;

	Vector3 startPos, endPos;

	void Start () {
		// pathVertices = new Vector2[path.GetComponent<LineRenderer>().GetPositions().Length;];

		pathVertices = new Vector3[path.GetComponent<LineRenderer>().positionCount];
		// print(path.GetComponent<LineRenderer>().positionCount);

		path.GetComponent<LineRenderer>().GetPositions(pathVertices); //pass the array that you want to put the positions into as a parameter
		transform.position = new Vector3 (pathVertices[0].x, pathVertices[0].y + 0.5f, pathVertices[0].z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow) && !startMovement && currIndex < pathVertices.Length) {
			// print("test");
			startTime = Time.time;
			startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
			endPos = new Vector3 (pathVertices[currIndex + 1].x, pathVertices[currIndex + 1].y + 0.5f, pathVertices[currIndex + 1].z);
        	journeyLength = Vector3.Distance(startPos, endPos);
			startMovement = true;
			currIndex++;
    
			
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) && !startMovement && currIndex > 0) {
			// print("test");
			startTime = Time.time;
			startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
			endPos = new Vector3 (pathVertices[currIndex - 1].x, pathVertices[currIndex - 1].y + 0.5f, pathVertices[currIndex - 1].z);
        	journeyLength = Vector3.Distance(startPos, endPos);
			startMovement = true;
			currIndex--;
    
			
		}

		else if (startMovement) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
			if (transform.position == endPos) startMovement = false;
		}
	}
}
