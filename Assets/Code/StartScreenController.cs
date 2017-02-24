using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// using XboxCtrlrInput;
using System.Collections.Generic;

public class StartScreenController : MonoBehaviour {

	Animator wardAnim, peakAnim;
	StartScreenButton a, s, k, l, space, esc;

	int ctrlNum;
	
	//for xbox controller
	public List<GameObject> leftStickGroup = new List<GameObject>();
	public List<GameObject> rightStickGroup = new List<GameObject>();
	public List<GameObject> pauseGroup = new List<GameObject>();
	public List<GameObject> startGroup = new List<GameObject>();

	//for keyboard
	public List<GameObject> escGroup = new List<GameObject>();
	public List<GameObject> spaceGroup = new List<GameObject>();
	public List<GameObject> asGroup = new List<GameObject>();
	public List<GameObject> klGroup = new List<GameObject>();

	CharacterMovement charMov;
	// Use this for initialization
	void Start () {
		wardAnim = GameObject.Find("Ward Logo").GetComponent<Animator>();
		peakAnim = GameObject.Find("Peak Logo").GetComponent<Animator>();

		// ctrlNum = XCI.GetNumPluggedCtrlrs();

		Transparency.SetTransparent(leftStickGroup);
		Transparency.SetTransparent(rightStickGroup);
		Transparency.SetTransparent(pauseGroup);
		Transparency.SetTransparent(startGroup);

		Transparency.SetTransparent(escGroup);
		Transparency.SetTransparent(spaceGroup);
		Transparency.SetTransparent(asGroup);
		Transparency.SetTransparent(klGroup);

		StartCoroutine("StartScreen");

		Screen.SetResolution(1920, 1080, true);
		// StartCoroutine("SwitchButtons");
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKey("space") || XCI.GetButtonDown(XboxButton.Start)) {
		
		if (Input.GetKey("space") || Input.GetKeyDown(KeyCode.JoystickButton9)) {
			Time.timeScale = 1f;
			Application.LoadLevel(1);
		}
	
	}

	IEnumerator StartScreen () {
		yield return new WaitForSeconds(1f);
		wardAnim.SetBool("Transition", true);
		yield return new WaitForSeconds(3f);
		Destroy (GameObject.Find("Ward Logo"));
		peakAnim.SetBool("Transition", true);
		
		yield return new WaitForSeconds(4f);

		if (ctrlNum > 0) {
			print(ctrlNum);
			Transparency.UpFade(leftStickGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(rightStickGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(pauseGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(startGroup);
		}

		else {
			Transparency.UpFade(asGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(klGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(escGroup);
			
			yield return new WaitForSeconds(1f);
			Transparency.UpFade(spaceGroup);
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
