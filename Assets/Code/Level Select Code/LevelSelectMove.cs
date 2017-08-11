using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class LevelSelectMove : MonoBehaviour {

	// Use this for initialization

	public GameObject path, circle, worldText;
	public Vector3[] pathVertices;

	bool startMovement = false;
	float startTime;
	float journeyLength;
	float speed = 6f;

	string[] levels = {"Level 1", "Level 2", "Level 3"};

	int currIndex = 0;

	Vector3 startPos, endPos;

	int upLeft = -1, upRight = -1, downLeft = -1, downRight = -1;

	void Start () {
		// pathVertices = new Vector2[path.GetComponent<LineRenderer>().GetPositions().Length;];

		pathVertices = new Vector3[path.GetComponent<LineRenderer>().positionCount];
		// print(path.GetComponent<LineRenderer>().positionCount);
		ClearRelativeNodes();
		path.GetComponent<LineRenderer>().GetPositions(pathVertices); //pass the array that you want to put the positions into as a parameter
		transform.position = new Vector3 (pathVertices[IntersceneDataHandler.currentLevel].x, pathVertices[IntersceneDataHandler.currentLevel].y + 0.5f, pathVertices[IntersceneDataHandler.currentLevel].z);
		currIndex = IntersceneDataHandler.currentLevel;
		SetRelativeNodes(IntersceneDataHandler.currentLevel + 1);
		SetRelativeNodes(IntersceneDataHandler.currentLevel - 1);
	}
	
	// Update is called once per frame
	void Update () {

		// float axisX = XCI.GetAxis(XboxAxis.LeftStickX);
        // float axisY = XCI.GetAxis(XboxAxis.LeftStickY);

		// if ((Input.GetKeyDown(KeyCode.RightArrow) || axisX > 0) && !startMovement && currIndex < pathVertices.Length) {
		// 	// print("test");
		// 	startTime = Time.time;
		// 	startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
		// 	endPos = new Vector3 (pathVertices[currIndex + 1].x, pathVertices[currIndex + 1].y + 0.5f, pathVertices[currIndex + 1].z);
        // 	journeyLength = Vector3.Distance(startPos, endPos);
		// 	startMovement = true;
		// 	currIndex++;
    
			
		// }

		// if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0 ) && !startMovement && currIndex > 0) {
		// 	// print("test");
		// 	startTime = Time.time;
		// 	startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
		// 	endPos = new Vector3 (pathVertices[currIndex - 1].x, pathVertices[currIndex - 1].y + 0.5f, pathVertices[currIndex - 1].z);
        // 	journeyLength = Vector3.Distance(startPos, endPos);
		// 	startMovement = true;
		// 	currIndex--;
    
			
		// }

		// if (Input.GetAxis("Vertical") > 0 ) {
		// 	print("test");
		// }

		if ((Input.GetAxis("Horizontal") > 0) && !startMovement) {
			if (Input.GetAxis("Vertical") >= 0 && upRight != -1) {
					// startTime = Time.time;
					// startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
					// endPos = new Vector3 (pathVertices[upRight].x, pathVertices[upRight].y + 0.5f, pathVertices[upRight].z);
					// journeyLength = Vector3.Distance(startPos, endPos);
					// startMovement = true;
					// currIndex = upRight;

				SetUpJourney(upRight);
					// currIndex--;
			}

			else if (Input.GetAxis("Vertical") <= 0 && downRight != -1) {
				SetUpJourney(downRight);
			}
		}

		else if ((Input.GetAxis("Horizontal") < 0) && !startMovement) {
			if (Input.GetAxis("Vertical") >= 0 && upLeft != -1) {
					// startTime = Time.time;
					// startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
					// endPos = new Vector3 (pathVertices[upRight].x, pathVertices[upRight].y + 0.5f, pathVertices[upRight].z);
					// journeyLength = Vector3.Distance(startPos, endPos);
					// startMovement = true;
					// currIndex = upRight;

				SetUpJourney(upLeft);
					// currIndex--;
			}

			else if (Input.GetAxis("Vertical") <= 0 && downLeft != -1) {
				SetUpJourney(downLeft);
			}
		}

		else if ((Input.GetAxis("Vertical") > 0) && !startMovement) {
			if (Input.GetAxis("Horizontal") >= 0 && upRight != -1) {
					// startTime = Time.time;
					// startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
					// endPos = new Vector3 (pathVertices[upRight].x, pathVertices[upRight].y + 0.5f, pathVertices[upRight].z);
					// journeyLength = Vector3.Distance(startPos, endPos);
					// startMovement = true;
					// currIndex = upRight;

				SetUpJourney(upRight);
					// currIndex--;
			}

			else if (Input.GetAxis("Horizontal") <= 0 && upLeft != -1) {
				SetUpJourney(upLeft);
			}
		}

		else if ((Input.GetAxis("Vertical") < 0) && !startMovement) {
			if (Input.GetAxis("Horizontal") >= 0 && downRight != -1) {
					// startTime = Time.time;
					// startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
					// endPos = new Vector3 (pathVertices[upRight].x, pathVertices[upRight].y + 0.5f, pathVertices[upRight].z);
					// journeyLength = Vector3.Distance(startPos, endPos);
					// startMovement = true;
					// currIndex = upRight;

				SetUpJourney(downRight);
					// currIndex--;
			}

			else if (Input.GetAxis("Horizontal") <= 0 && downLeft != -1) {
				SetUpJourney(downLeft);
			}
		}

		else if (startMovement) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
			if (transform.position == endPos) startMovement = false;

			//reassign indexes
			ClearRelativeNodes();
			SetRelativeNodes(currIndex + 1);
			SetRelativeNodes(currIndex - 1);
		}

		else if (!startMovement && (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.A))) {
			
			StartCoroutine("Shrink");
		}
	}

	void SetUpJourney (int index) {
		startTime = Time.time;
		startPos = new Vector3 (pathVertices[currIndex].x, pathVertices[currIndex].y + 0.5f, pathVertices[currIndex].z);
		endPos = new Vector3 (pathVertices[index].x, pathVertices[index].y + 0.5f, pathVertices[index].z);
		journeyLength = Vector3.Distance(startPos, endPos);
		startMovement = true;
		currIndex = index;
	}

	IEnumerator Shrink () {
		float scaleDiff = 1f;
		Destroy(worldText);
		while (circle.transform.localScale.x > 0) {
			circle.transform.localScale = new Vector3(circle.transform.localScale.x - scaleDiff, circle.transform.localScale.y - scaleDiff, circle.transform.localScale.z);
			yield return new WaitForSeconds(0.01f);
		}
		IntersceneDataHandler.currentLevel = currIndex;
		SceneManager.LoadScene(levels[currIndex]);
	}

	void SetRelativeNodes (int index) {
		if (index < 0 || index >= pathVertices.Length) {
			return;
		}

		if (pathVertices[index].x >= transform.position.x) {
			if (pathVertices[index].y >= transform.position.y) {
				upRight = index;
			}

			if (pathVertices[index].y < transform.position.y) {
				downRight = index;
			}
		}

		if (pathVertices[index].x < transform.position.x) {
			if (pathVertices[index].y >= transform.position.y) {
				upLeft = index;
			}

			if (pathVertices[index].y < transform.position.y) {
				downLeft = index;
			}
		}
	}

	void ClearRelativeNodes () {
		upRight = -1; upLeft = -1; downRight = -1; downLeft = -1;
	}
}
