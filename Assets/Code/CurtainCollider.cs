using UnityEngine;
using System.Collections;
// using XboxCtrlrInput;
using Rewired;

public class CurtainCollider : MonoBehaviour {

	BoxCollider2D box;
	float sizeChange = -0.1f;
	float initialSize, smallestDist, currDist;
	GameObject rightCurtain, leftCurtain, renderCurtain;
	bool paused = false;

	public Camera camera;
	LevelControl levelControl;
	bool hasController;

	public int playerId = 0; // The Rewired player id of this character

    private Player player; // The Rewired Player
	
	void Awake () {
		player = ReInput.players.GetPlayer(playerId);
		
		
        // Subscribe to events
        ReInput.ControllerConnectedEvent += OnControllerConnected;

		
        // ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        // ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
    }

	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
        // print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		hasController = player.controllers.ContainsController<Joystick>(args.controllerId);
		// print(hasController);
    }
	// Use this for initialization
	void Start () {
		renderCurtain = GameObject.Find("Curtain Renderer");
		box = GetComponent<BoxCollider2D>();
		initialSize = box.size.x;
		rightCurtain = GameObject.Find("Right Curtain");
		leftCurtain = GameObject.Find("Left Curtain");
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		smallestDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		currDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);

		/*if (Input.GetKeyDown("escape")) {
			paused = !paused;
		}*/

		if (camera.WorldToScreenPoint(leftCurtain.transform.position).x < 0) {
			// leftCurtain.transform.position = 
		}

		if (!paused && levelControl.GetFollow()) {
			// if(Input.GetKey("a") || Input.GetAxis("Horizontal") < 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x > Screen.width + 10) {
			if(player.GetAxis("Left Curtain Movement") < 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x > Screen.width + 10) {
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
			// print(camera.WorldToScreenPoint(leftCurtain.transform.position).x < -15f);
			// if(Input.GetKey("s") || Input.GetAxis("Horizontal") > 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x < -15f) {
			if(player.GetAxis("Left Curtain Movement") > 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x < -15f) {
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

			//************ COMMENTED OUT DUE TO CTRLR ERROR
			// if(Input.GetKey("k") /*|| XCI.GetAxis(XboxAxis.RightStickX) < 0f*/ || camera.WorldToScreenPoint(rightCurtain.transform.position).x > Screen.width + 10) {
			if(player.GetAxis("Right Curtain Movement") < 0f || camera.WorldToScreenPoint(rightCurtain.transform.position).x > Screen.width + 10) {
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

			//************ COMMENTED OUT DUE TO CTRLR ERROR
			if(player.GetAxis("Right Curtain Movement") > 0f || camera.WorldToScreenPoint(rightCurtain.transform.position).x < -15f) {
			
			// if(Input.GetKey("l") || Input.GetAxis("Mouse X") > 0f || camera.WorldToScreenPoint(rightCurtain.transform.position).x < -15f) {
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

	public void SetPausedGame (bool val) {
		paused = val;
	}
}
