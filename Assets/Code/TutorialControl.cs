using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour {

	public List<GameObject> textWindows = new List<GameObject>();
	public List<GameObject> tutorialEndTextWindows = new List<GameObject>();
	int textBoxCount = 0;

	public CharacterMovement2 scout;

	bool[] createdBridge = new bool[2];

	public CurtainCollider curtain;

	bool canNext = false;
	// Use this for initialization
	void Start () {
		
		curtain.SwitchPausedGame();
		foreach (GameObject item in textWindows) {
			Transparency.SetTransparent(item);
		}

		foreach (GameObject item in tutorialEndTextWindows) {
			Transparency.SetTransparent(item);
		}

		if (!IntersceneDataHandler.startedTutorial) StartCoroutine("StartTutorial");
		else textBoxCount = 2;

		// print(tutorialEndTextWindows[0].name);
	}
	
	// Update is called once per frame
	void Update () {
		if ((XCI.GetButtonDown(XboxButton.A) || Input.GetKeyDown(KeyCode.Space)) && canNext && !IntersceneDataHandler.startedTutorial ) {
			print(textBoxCount);
			if (textBoxCount < 2) StartCoroutine("NextScreen", textBoxCount);//NextScreen(textBoxCount);
			else if (textBoxCount == 2) ActivateTutorial();
		}

		else if (IntersceneDataHandler.startedTutorial && textBoxCount <= 2) {
			textBoxCount++;
			ActivateTutorial();
		}

		if (((Mathf.Abs(Input.GetAxis("Horizontal")) > 0f) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && textBoxCount > 2) {
			// print("text box count: "+ textBoxCount);
			// print("Test 2");
			Transparency.SetTransparent(textWindows[2]);
			// ActivateTutorial();
		}

		if ((XCI.GetButtonDown(XboxButton.Start) || Input.GetKeyDown(KeyCode.Return))) {
			IntersceneDataHandler.startedTutorial = false;
			SceneManager.LoadScene("Level 1");
		}
	}

	IEnumerator StartTutorial () {
		yield return new WaitForSeconds(1f);

		StartCoroutine("FadeUpWindow", 0);
		// print("Test");
		// float cnt = 0f;
		

	}

	void ActivateTutorial () {
		print("this one");
		StartCoroutine("NextScreen", 2);
		curtain.SwitchPausedGame();
	}

	IEnumerator NextScreen (int index) {
		Transparency.SetTransparent(textWindows[index - 1]);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("FadeUpWindow", index);
	}

	//left = 0, right = 1;
	public void SetCreatedBridge (int index, bool val) {
		createdBridge[index] = val;

		if (createdBridge[0] && createdBridge[1]) {
			curtain.SwitchPausedGame();
			scout.SetMovementOff(false);
		}
	}

	public void TransitionToTutorialEnd () {
		StartCoroutine("EndTutorial");
	}

	IEnumerator EndTutorial () {
		yield return new WaitForSeconds(1f);

		StartCoroutine("FadeUp", tutorialEndTextWindows[0]);
		StartCoroutine("FadeUp", tutorialEndTextWindows[1]);
	}

	IEnumerator FadeUp (GameObject go) {
		float alpha = 0f;

		// if (index >= textWindows.Count) yield break;

		while (alpha < 1f) {
			// print("TEST");
			// print(textWindows[0].transform.GetChild(0).GetComponent<Image>().color.a);
			if (go.transform.childCount > 0) Transparency.SetOpacity(go, go.transform.GetChild(0).GetComponent<Text>().color.a + 0.1f);
			else Transparency.SetOpacity(go, go.GetComponent<Text>().color.a + 0.1f);
			alpha += 0.1f;
			yield return new WaitForSeconds(0.01f);
		}

		// textBoxCount++;
		
		// print(textBoxCount);
		// canNext = true;
	}

	IEnumerator FadeUpWindow (int index) {
		float alpha = 0f;

		if (index >= textWindows.Count) yield break;

		while (alpha < 1f) {
			// print("TEST");
			// print(textWindows[0].transform.GetChild(0).GetComponent<Image>().color.a);
			Transparency.SetOpacity(textWindows[index], textWindows[index].transform.GetChild(0).GetComponent<Image>().color.a + 0.1f);
			alpha += 0.1f;
			yield return new WaitForSeconds(0.01f);
		}

		textBoxCount++;
		
		print(textBoxCount);
		canNext = true;

		// if (textBoxCount >= 2) curtain.SwitchPausedGame();
	}
}
