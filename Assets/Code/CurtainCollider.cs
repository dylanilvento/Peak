using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class CurtainCollider : MonoBehaviour {

	BoxCollider2D box;
	float sizeChange = -0.1f;
	float initialSize, smallestDist, currDist;
	GameObject rightCurtain, leftCurtain, renderCurtain;
	bool paused = false;


	// Use this for initialization
	void Start () {
		renderCurtain = GameObject.Find("Curtain Renderer");
		box = GetComponent<BoxCollider2D>();
		initialSize = box.size.x;
		rightCurtain = GameObject.Find("Right Curtain");
		leftCurtain = GameObject.Find("Left Curtain");
		smallestDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		currDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);

		/*if (Input.GetKeyDown("escape")) {
			paused = !paused;
		}*/

		if (!paused) {
			if(Input.GetKey("a") || Input.GetAxis("Horizontal") < 0f) {
				// sizeChange += 0.1f;
				// box.size.x = 1 + sizeChange;
				box.size = new Vector2 (box.size.x + 0.1f, box.size.y);
				renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x + 0.2f, renderCurtain.transform.localScale.y);
				renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x - 0.05f, renderCurtain.transform.localPosition.y);
				// transform.localPosition.x -= 0.05f;
				// transform.localPosition = new Vector2 (transform.localPosition.x - 0.05f, transform.localPosition.y);
				leftCurtain.transform.position = new Vector2 (leftCurtain.transform.position.x - 0.1f, leftCurtain.transform.position.y);
				transform.position = new Vector2 (transform.position.x - 0.05f, transform.position.y);
			}
			
			if(Input.GetKey("s") || Input.GetAxis("Horizontal") > 0f) {
				if (box.size.x > initialSize && currDist > smallestDist) {
					// sizeChange -= 0.1f;
					// box.size.x = 1 + sizeChange;
					box.size = new Vector2 (box.size.x - 0.1f, box.size.y);
					renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x - 0.2f, renderCurtain.transform.localScale.y);
					renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x + 0.05f, renderCurtain.transform.localPosition.y);
					// transform.localPosition.x += 0.05;
					leftCurtain.transform.position = new Vector2 (leftCurtain.transform.position.x + 0.1f, leftCurtain.transform.position.y);
					transform.position = new Vector2 (transform.position.x + 0.05f, transform.position.y);
				}
			}
			
			if(Input.GetKey("k") || XCI.GetAxis(XboxAxis.RightStickX) < 0f) {
				if (box.size.x > initialSize && currDist > smallestDist) {
					// sizeChange -= 0.01f;
					// box.size.x = 1 + sizeChange;
					box.size = new Vector2 (box.size.x - 0.1f, box.size.y);
					renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x - 0.2f, renderCurtain.transform.localScale.y);
					renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x - 0.05f, renderCurtain.transform.localPosition.y);
					// transform.localPosition.x -= 0.05;
					rightCurtain.transform.position = new Vector2 (rightCurtain.transform.position.x - 0.1f, rightCurtain.transform.position.y);
					transform.position = new Vector2 (transform.position.x - 0.05f, transform.position.y);
				}
				
			}
			
			if(Input.GetKey("l") || XCI.GetAxis(XboxAxis.RightStickX) > 0f) {
				// sizeChange += 0.01f;
				// box.size.x = 1 + sizeChange;
				box.size = new Vector2 (box.size.x + 0.1f, box.size.y);
				renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x + 0.2f, renderCurtain.transform.localScale.y);
				renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x + 0.05f, renderCurtain.transform.localPosition.y);
				// transform.localPosition.x += 0.05;
				rightCurtain.transform.position = new Vector2 (rightCurtain.transform.position.x + 0.1f, rightCurtain.transform.position.y);
				transform.position = new Vector2 (transform.position.x + 0.05f, transform.position.y);
			}
		}
	}

	public void SwitchPausedGame () {
		paused = !paused;
	}
}
