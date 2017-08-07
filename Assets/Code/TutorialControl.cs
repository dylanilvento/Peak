using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class TutorialControl : MonoBehaviour {

	public List<GameObject> textWindows = new List<GameObject>();
	int textBoxCount = 0;

	public CurtainCollider curtain;

	bool canNext = false;
	// Use this for initialization
	void Start () {
		
		curtain.SwitchPausedGame();
		foreach (GameObject item in textWindows) {
			Transparency.SetTransparent(item);
		}

		StartCoroutine("StartTutorial");
	}
	
	// Update is called once per frame
	void Update () {
		if ((XCI.GetButtonDown(XboxButton.A) || Input.GetKeyDown(KeyCode.Space)) && canNext) {
			if (textBoxCount < 2) StartCoroutine("NextScreen", textBoxCount);//NextScreen(textBoxCount);
			else ActivateTutorial();
		}

		if (((Mathf.Abs(Input.GetAxis("Horizontal")) > 0f) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && textBoxCount > 2) {
			print("text box count: "+ textBoxCount);
			print("Test 2");
			Transparency.SetTransparent(textWindows[2]);
			// ActivateTutorial();
		}
	}

	IEnumerator StartTutorial () {
		yield return new WaitForSeconds(1f);

		StartCoroutine("FadeUpWindow", 0);
		// print("Test");
		// float cnt = 0f;
		

	}

	void ActivateTutorial () {
		print("test");
		StartCoroutine("NextScreen", 2);
		curtain.SwitchPausedGame();
	}

	IEnumerator NextScreen (int index) {
		Transparency.SetTransparent(textWindows[index - 1]);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("FadeUpWindow", index);
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
