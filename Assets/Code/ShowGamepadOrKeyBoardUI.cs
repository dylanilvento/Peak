using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using XboxCtrlrInput;

public class ShowGamepadOrKeyBoardUI : MonoBehaviour {
	public bool isGamepadUI;
	public bool isKeyboardUI;
	// Use this for initialization
	void Start () {
		//checks to see if any controllers are plugged in
		//if there aren't and it's the gamepad UI, then the gamepad UI is turned off
		if (/*XCI.GetNumPluggedCtrlrs() == 0 &&*/ isGamepadUI) {
			gameObject.SetActive(false);
		}

		// else if (/*XCI.GetNumPluggedCtrlrs() > 0 &&*/ isKeyboardUI) {
		// 	gameObject.SetActive(false);
		// }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
