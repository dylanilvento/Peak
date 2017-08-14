using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ShowGamepadOrKeyBoardUI : MonoBehaviour {
	public bool isGamepadUI;
	public bool isKeyboardUI;
	// Use this for initialization
	void Start () {
		if (XCI.GetNumPluggedCtrlrs() > 0 && isKeyboardUI) {
			gameObject.SetActive(false);
		}

		else if (XCI.GetNumPluggedCtrlrs() == 0 && isGamepadUI) {
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
