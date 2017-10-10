using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using XboxCtrlrInput;
using UnityEngine.SceneManagement;
using Rewired;

public class DemoEnd : MonoBehaviour {

	bool hasController;

	public int playerId = 0; // The Rewired player id of this character

    private Player player; // The Rewired Player
	
	void Awake () {
		player = ReInput.players.GetPlayer(playerId);
		
		
        // Subscribe to events
        ReInput.ControllerConnectedEvent += OnControllerConnected;

    }

	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
        // print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		hasController = player.controllers.ContainsController<Joystick>(args.controllerId);
		// print(hasController);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// if (XCI.GetButtonDown(XboxButton.Start) || XCI.GetButtonDown(XboxButton.A)) {
		if (player.GetButtonDown("Continue") || player.GetButtonDown("Start")) {

			SceneManager.LoadScene("Start");

		}
	}
}
