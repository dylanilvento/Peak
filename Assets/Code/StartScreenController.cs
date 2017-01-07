using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XboxCtrlrInput;
using System.Collections.Generic;

public class StartScreenController : MonoBehaviour {

	Animator wardAnim, peakAnim;
	StartScreenButton a, s, k, l, space, esc;
	
	List<GameObject> escGroup = new List<GameObject>();
	List<GameObject> spaceGroup = new List<GameObject>();
	List<GameObject> asGroup = new List<GameObject>();
	List<GameObject> klGroup = new List<GameObject>();
	// GameObject[] escGroup;// = new GameObject[2];
	// GameObject[] spaceGroup;// = new GameObject[2];
	// GameObject[] asGroup;// = new GameObject[3];
	// GameObject[] klGroup;// = new GameObject[3];
	CharacterMovement charMov;
	// Use this for initialization
	void Start () {
		wardAnim = GameObject.Find("Ward Logo").GetComponent<Animator>();
		peakAnim = GameObject.Find("Peak Logo").GetComponent<Animator>();
		// a = GameObject.Find("A Button (Start Screen)").GetComponent<StartScreenButton>();
		// s = GameObject.Find("S Button (Start Screen)").GetComponent<StartScreenButton>();
		// k = GameObject.Find("K Button (Start Screen)").GetComponent<StartScreenButton>();
		// l = GameObject.Find("L Button (Start Screen)").GetComponent<StartScreenButton>();
		// space = GameObject.Find("Space Button").GetComponent<StartScreenButton>();
		// esc = GameObject.Find("Esc Button").GetComponent<StartScreenButton>();
		//charMov = GameObject.Find("Scout").GetComponent<CharacterMovement>();
		//charMov.SetPaused(true);

		for (int i = 0; i < GameObject.Find("Esc Button Group").transform.childCount; i++) {
			//print(GameObject.Find("Esc Button Group").transform.childCount);
		    escGroup.Add(GameObject.Find("Esc Button Group").transform.GetChild(i).gameObject);
		    //print(escGroup[i].name);
		}
		for (int i = 0; i < GameObject.Find("Space Button Group").transform.childCount; i++) {
		    spaceGroup.Add(GameObject.Find("Space Button Group").transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < GameObject.Find("A & S Buttons").transform.childCount; i++) {
		    asGroup.Add(GameObject.Find("A & S Buttons").transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < GameObject.Find("K & L Buttons").transform.childCount; i++) {
		    klGroup.Add(GameObject.Find("K & L Buttons").transform.GetChild(i).gameObject);
		}

		Transparency.SetTransparent(escGroup);
		Transparency.SetTransparent(spaceGroup);
		Transparency.SetTransparent(asGroup);
		Transparency.SetTransparent(klGroup);

		StartCoroutine("StartScreen");
		// StartCoroutine("SwitchButtons");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("space") || XCI.GetButtonDown(XboxButton.Start)) {
			/*GameObject.Find("Start Screen Camera").SetActive(false);
			charMov.SetPaused(false);
			SetTransparent(escGroup);
			SetTransparent(spaceGroup);
			SetTransparent(asGroup);
			SetTransparent(klGroup);
			Destroy (gameObject);*/
			Time.timeScale = 1f;
			Application.LoadLevel(1);
		}
		
	
	}

	IEnumerator StartScreen () {
		yield return new WaitForSeconds(1f);
		wardAnim.SetBool("Transition", true);
		yield return new WaitForSeconds(4f);
		Destroy (GameObject.Find("Ward Logo"));
		peakAnim.SetBool("Transition", true);
		
		yield return new WaitForSeconds(5f);
		Transparency.UpFade(asGroup);
		//TestClass.StartCoroutine("FadeUp", asGroup);
		
		yield return new WaitForSeconds(2f);
		Transparency.UpFade(klGroup);
		//TestClass.StartCoroutine("FadeUp", klGroup);
		
		yield return new WaitForSeconds(2f);
		Transparency.UpFade(escGroup);
		//TestClass.StartCoroutine("FadeUp", escGroup);
		
		yield return new WaitForSeconds(2f);
		Transparency.UpFade(spaceGroup);
		//TestClass.StartCoroutine("FadeUp", spaceGroup);
		//FadeUp(spaceGroup);
		//FadeUp(asGroup);
		//FadeUp(klGroup);

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

/*	void SetTransparent (GameObject[] goArray) {
		for (int i = 0; i < goArray.Length; i++) {
			if (goArray[i].GetComponent<Image>() != null) {
				Image currGO = goArray[i].GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}

			else if (goArray[i].GetComponent<Text>() != null) {
				Text currGO = goArray[i].GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}
		}
	}

	IEnumerator FadeUp (GameObject[] goArray) {
		while (goArray[0].GetComponent<Image>().color.a < 1.0f) {
			for (int i = 0; i < goArray.Length; i++) {
				if (goArray[i].GetComponent<Image>() != null) {
					Image currGO = goArray[i].GetComponent<Image>();
					currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, currGO.color.a + 0.1f);
				}
	
				else if (goArray[i].GetComponent<Text>() != null) {
					Text currGO = goArray[i].GetComponent<Text>();
					currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, currGO.color.a + 0.1f);
				}
			}
			yield return new WaitForSeconds(0.05f);
		}
	}*/
}
