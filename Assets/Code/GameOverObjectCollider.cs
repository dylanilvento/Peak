using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using Rewired;
// using XboxCtrlrInput;

public class GameOverObjectCollider : MonoBehaviour {

	// Transform player;
	float relPos;
	// Image goScreen;
	Camera camera;
	CameraFollow cameraFollow;
	

	Rigidbody2D rb;

	GameObject target;

	bool goActive = false;
	// LevelControl levelControl;
	// public static int deathCnt = 0;
	public GameObject statusScreen;

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
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		target = levelControl.GetPlayer();
		// player = GameObject.Find("Scout Group").transform;
		// relPos = player.position.x - transform.position.x;
		// goScreen = GameObject.Find("Game Over Screen").GetComponent<Image>();
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraFollow = camera.GetComponent<CameraFollow>();
		// levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		// statusScreen = GameObject.Find("Status Screen");

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		// transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		// StartCoroutine("MoveUp");

		//************ COMMENTED OUT DUE TO CTRLR ERROR
		if (goActive && (player.GetButtonDown("Continue") || player.GetButtonDown("Start"))) {
		//*************
		// if (goActive && (Input.GetKeyDown("space") || Input.GetButtonDown("A Button") || Input.GetButtonDown("Start Button"))) {
			Time.timeScale = 1f;
			Application.LoadLevel(Application.loadedLevel);
		}

		if (gameObject.name.Contains("Boulder") && GameObject.Find("Game Controller").GetComponent<LevelControl>().GetLevelOver()) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject == target) {
			// Time.timeScale = 0.0001f;

			StartCoroutine("RestartLevel");
			// goScreen.enabled = true;
			// cameraFollow.canMove = false;
			// // deathCnt++;
			// // print(deathCnt);

			// statusScreen.SetActive(true);
			// statusScreen.transform.GetChild(0).GetComponent<Text>().text = "Press A to Restart";

			// goActive = true;

		}
	}

	IEnumerator RestartLevel() {
		// print("restarting level");
		yield return new WaitForSeconds(2f);
		// Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}
}
