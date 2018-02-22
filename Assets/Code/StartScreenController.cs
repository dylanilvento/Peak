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

	public GameObject menuArrow;
	public GameObject startMenu, resMenu;

	CharacterMovement charMov;
	// Use this for initialization
	bool hasController;

	bool onResOptions = false;

	bool startMenuActive = false;
	bool switchToMenu = false;
	int startMenuIndex = 1;

	Vector2[] resolutions = {
		new Vector2(1366, 768),
		new Vector2(1920, 1080),
		new Vector2(1280, 800),
		new Vector2(1280, 1024),
		new Vector2(1440, 900),
		new Vector2(1600, 900)
	};
	int resIndex = 0;

	public Text resText;

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
		
		Transparency.SetTransparent(startMenu);
		Transparency.SetTransparent(resMenu);
		Transparency.SetTransparent(pauseButtonGroup);
		Transparency.SetTransparent(pauseKeyboardGroup);

		StartCoroutine("StartScreen");

		Cursor.visible = false;

		// Screen.SetResolution(1920, 1080, true);
		// StartCoroutine("SwitchButtons");
	}
	
	// Update is called once per frame
	void Update () {
		
		// if (player.GetButtonDown("Start")) {
		// 	SceneManager.LoadScene("Tutorial");
		// }

		if (player.GetButtonDown("Start") && !startMenuActive && !onResOptions) {
			
			print("works");
			
			Transparency.SetOpacity(startMenu, 1f);
			switchToMenu = true;
			startMenuActive = true;
			Transparency.SetTransparent(pauseButtonGroup);
			Transparency.SetTransparent(pauseKeyboardGroup);
		}

		else if ((player.GetButtonDown("Start") || player.GetButtonDown("Continue")) && startMenuActive) {
		
		// if (Input.GetKey("space") || Input.GetButtonDown("Start Button")) {
			if (startMenuIndex == 1) {
				Time.timeScale = 1f;
				// Application.LoadLevel(1);
				SceneManager.LoadScene("Tutorial");
			}

			else if (startMenuIndex == 2) {
				Transparency.SetTransparent(startMenu);
				Transparency.SetOpacity(resMenu, 1f);
				startMenuActive = false;
				onResOptions = true;
			}

			else if (startMenuIndex == 3) {
				Application.Quit();
			}
			
		}

		// float horDir = player.GetAxis("Horizontal Movement");

		if ((player.GetButtonDown("Left") || player.GetButtonDown("Right")) && onResOptions) {
			// if (horDir > 0) {
			if (player.GetButtonDown("Right")) {
				if (resIndex >= resolutions.Length - 1) {
					resIndex = 0;
				}

				else {
					resIndex++;
				}
			}

			else if (player.GetButtonDown("Left")) {
				if (resIndex == 0) {
					resIndex = resolutions.Length - 1;
				}

				else {
					resIndex--;
				}
			}

			resText.text = resolutions[resIndex].x + " x " + resolutions[resIndex].y;
		}

		if (player.GetButtonDown("Start") && onResOptions) {
			Screen.SetResolution((int) resolutions[resIndex].x, (int) resolutions[resIndex].y, true);
			Transparency.SetTransparent(resMenu);
			Transparency.SetOpacity(startMenu, 1f);
			startMenuActive = true;
			onResOptions = false;
		}

		if (player.GetButtonDown("Up") && startMenuActive && startMenuIndex > 1) {
			menuArrow.transform.position = new Vector3(menuArrow.transform.position.x, menuArrow.transform.position.y + (Screen.height / 15), menuArrow.transform.position.z);
			startMenuIndex--;
		}

		else if (player.GetButtonDown("Down") && startMenuActive && startMenuIndex < 3) {
			menuArrow.transform.position = new Vector3(menuArrow.transform.position.x, menuArrow.transform.position.y - (Screen.height / 15), menuArrow.transform.position.z);
			startMenuIndex++;
		}

		if (player.GetButtonDown("Demo Reel Start")) {
			SceneManager.LoadScene("Demo Reel");
		}
	
	}

	IEnumerator StartScreen () {
		yield return new WaitForSeconds(1f);
		wardAnim.SetBool("Transition", true);
		yield return new WaitForSeconds(3f);
		Destroy (GameObject.Find("Ward Logo"));
		peakAnim.SetBool("Transition", true);
		
		yield return new WaitForSeconds(2f);

		if (IntersceneDataHandler.hasController || hasController) {
			while (pauseButtonGroup[0].GetComponent<Text>().color.a < 1f && !switchToMenu) {
				Transparency.SetOpacity(pauseButtonGroup, pauseButtonGroup[0].GetComponent<Text>().color.a + 0.1f);
				yield return new WaitForSeconds(0.05f);
			}
		}

		else {
			while (pauseKeyboardGroup[0].GetComponent<Text>().color.a < 1f && !switchToMenu) {
				Transparency.SetOpacity(pauseKeyboardGroup, pauseKeyboardGroup[0].GetComponent<Text>().color.a + 0.1f);
				yield return new WaitForSeconds(0.05f);
			}
		}

		if (switchToMenu) {
			Transparency.SetTransparent(pauseButtonGroup);
			Transparency.SetTransparent(pauseKeyboardGroup);
		}

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
