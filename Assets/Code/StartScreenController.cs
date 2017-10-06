using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using XboxCtrlrInput;
using System.Collections.Generic;
using Rewired;
public class StartScreenController : MonoBehaviour {

	Animator wardAnim, peakAnim;
	StartScreenButton a, s, k, l, space, esc;

	// int ctrlNum;
	
    // private bool start;

	//for xbox controller
	public List<GameObject> pauseButtonGroup = new List<GameObject>();
	//for keyboard
	public List<GameObject> pauseKeyboardGroup = new List<GameObject>();

	CharacterMovement charMov;
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
		IntersceneDataHandler.hasController = hasController;
		// print(hasController);
    }
	void Start () {
		wardAnim = GameObject.Find("Ward Logo").GetComponent<Animator>();
		peakAnim = GameObject.Find("Peak Logo").GetComponent<Animator>();

		Time.timeScale = 1f;
		// print(ReInput.controllers.joystickCount);
		// ctrlNum = XCI.GetNumPluggedCtrlrs();

		



		Transparency.SetTransparent(pauseButtonGroup);
		Transparency.SetTransparent(pauseKeyboardGroup);

		StartCoroutine("StartScreen");

		Cursor.visible = false;

		Screen.SetResolution(1920, 1080, true);
		// StartCoroutine("SwitchButtons");
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Return) /*|| XCI.GetButtonDown(XboxButton.Start)*/) {
		
		if (player.GetButtonDown("Start")) {
		
		// if (Input.GetKey("space") || Input.GetButtonDown("Start Button")) {
			Time.timeScale = 1f;
			// Application.LoadLevel(1);
			SceneManager.LoadScene("Tutorial");
		}
	
	}

	IEnumerator StartScreen () {
		yield return new WaitForSeconds(1f);
		wardAnim.SetBool("Transition", true);
		yield return new WaitForSeconds(3f);
		Destroy (GameObject.Find("Ward Logo"));
		peakAnim.SetBool("Transition", true);
		
		yield return new WaitForSeconds(2f);

		if (hasController) {
			while (pauseButtonGroup[0].GetComponent<Text>().color.a < 1f) {
				Transparency.SetOpacity(pauseButtonGroup, pauseButtonGroup[0].GetComponent<Text>().color.a + 0.1f);
				yield return new WaitForSeconds(0.05f);
			}
		}

		else {
			while (pauseKeyboardGroup[0].GetComponent<Text>().color.a < 1f) {
				Transparency.SetOpacity(pauseKeyboardGroup, pauseKeyboardGroup[0].GetComponent<Text>().color.a + 0.1f);
				yield return new WaitForSeconds(0.05f);
			}
		}

		
		// Transparency.UpFade(pauseButtonGroup);

		// if (ctrlNum > 0) {
		// 	print(ctrlNum);
		// 	Transparency.UpFade(leftStickGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(rightStickGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(pauseGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(startGroup);
		// }

		// else {
		// 	Transparency.UpFade(asGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(klGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(escGroup);
			
		// 	yield return new WaitForSeconds(1f);
		// 	Transparency.UpFade(spaceGroup);
		// }

	}

	IEnumerator SwitchButtons () {
		bool upTurn = true;
		while (true) {
			if (upTurn) {
				a.SetUpButton();
				s.SetDownButton();
				k.SetUpButton();
				l.SetDownButton();
				space.SetUpButton();
				esc.SetUpButton();
			}
			else {
				a.SetDownButton();
				s.SetUpButton();
				k.SetDownButton();
				l.SetUpButton();
				space.SetDownButton();
				esc.SetDownButton();
			}
			upTurn = !upTurn;
			yield return new WaitForSeconds(1f);
		}
	}
}
