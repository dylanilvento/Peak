using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class DemoEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// if (XCI.GetButtonDown(XboxButton.Start) || XCI.GetButtonDown(XboxButton.A)) {
		if (Input.GetKeyDown(KeyCode.Return)) {

			SceneManager.LoadScene("Start");

		}
	}
}
