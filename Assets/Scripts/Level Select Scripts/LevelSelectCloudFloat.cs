using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCloudFloat : MonoBehaviour {
	Vector3 startPos, endPos, nextEndPos;

	public Vector3 leftSide, rightSide;
	bool startMovement = false;

	float startTime, journeyLength, speed;
	// Use this for initialization
	void Start () {

		leftSide = new Vector3(leftSide.x, transform.position.y, leftSide.z);
		rightSide = new Vector3(rightSide.x, transform.position.y, rightSide.z);
		// leftSide = new Vector3(-8.95f, transform.position.y, 0f);
		// rightSide = new Vector3(8.95f, transform.position.y, 0f);
		startPos = transform.position;
		endPos = Random.Range(0f, 1f) > 0.5f ? leftSide: rightSide;
		nextEndPos = endPos == leftSide ? rightSide: leftSide;
		speed = Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!startMovement) {
			startTime = Time.time;
			journeyLength = Vector3.Distance(startPos, endPos);
			startMovement = true;
		}

		else if (startMovement) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
			
			if (transform.position == endPos) {
					startPos = transform.position;
					endPos = nextEndPos;
					nextEndPos = endPos == leftSide ? rightSide: leftSide;
					speed = Random.Range(0.5f, 1.5f);
					startMovement = false;
				}
		}
	}
}
