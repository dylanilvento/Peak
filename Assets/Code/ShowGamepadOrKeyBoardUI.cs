using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
// using XboxCtrlrInput;

public class ShowGamepadOrKeyBoardUI : MonoBehaviour {
	public bool isGamepadUI;
	public bool isKeyboardUI;
	// Use this for initialization

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
		// print("haz controller? " + hasController);


		//checks to see if any controllers are plugged in
		//if there aren't and it's the gamepad UI, then the gamepad UI is turned off
		
    }
	void Start () {

		if (IntersceneDataHandler.hasController && isKeyboardUI) {
			gameObject.SetActive(false);
		}

		else if (!IntersceneDataHandler.hasController && isGamepadUI) {
			gameObject.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
