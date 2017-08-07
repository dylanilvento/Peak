using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Transparency : MonoBehaviour {

	//static instance of class
	static public Transparency instance;


	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetInstance () {
		//instance = this;
	}

	public static void SetOpacity (GameObject[] goArray, float opacity) {
		for (int i = 0; i < goArray.Length; i++) {
			if (goArray[i].GetComponent<Image>() != null) {
				Image currGO = goArray[i].GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (goArray[i].GetComponent<Text>() != null) {
				Text currGO = goArray[i].GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (goArray[i].GetComponent<SpriteRenderer>() != null) {
				SpriteRenderer currGO = goArray[i].GetComponent<SpriteRenderer>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}
		}
	}

	public static void SetOpacity (List<GameObject> goArray, float opacity) {
		for (int i = 0; i < goArray.Count; i++) {
			if (goArray[i].GetComponent<Image>() != null) {
				Image currGO = goArray[i].GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (goArray[i].GetComponent<Text>() != null) {
				Text currGO = goArray[i].GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (goArray[i].GetComponent<SpriteRenderer>() != null) {
				SpriteRenderer currGO = goArray[i].GetComponent<SpriteRenderer>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}
		}
	}

	public static void SetOpacity (GameObject go, float opacity) {
		// for (int i = 0; i < goArray.Length; i++) {
			if (go.transform.childCount > 0) {
				for (int ii = 0; ii < go.transform.childCount; ii++) {
					SetOpacity(go.transform.GetChild(ii).gameObject, opacity);
				}
			}

			if (go.GetComponent<Image>() != null) {
				Image currGO = go.GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (go.GetComponent<Text>() != null) {
				Text currGO = go.GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}

			else if (go.GetComponent<SpriteRenderer>() != null) {
				SpriteRenderer currGO = go.GetComponent<SpriteRenderer>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, opacity);
			}
		// }
	}

	public static void SetOpacity (SpriteRenderer[] srArray, float opacity) {
		for (int i = 0; i < srArray.Length; i++) {
			SpriteRenderer currSR = srArray[i];
			currSR.color = new Color (currSR.color.r, currSR.color.g, currSR.color.b, opacity);
		}
	}

	public static void SetOpacity (Color color, float opacity) {
		color = new Color (color.r, color.g, color.b, opacity);
	}

	public static void SetTransparent (GameObject[] goArray) {
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

	public static void SetTransparent (List<GameObject> goArray) {
		foreach (GameObject item in goArray) {
			if (item.GetComponent<Image>() != null) {
				Image currGO = item.GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}

			else if (item.GetComponent<Text>() != null) {
				Text currGO = item.GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}
		}
	}

	public static void SetTransparent (GameObject go) {
		if (go.transform.childCount > 0) {
			for (int ii = 0; ii < go.transform.childCount; ii++) {
				SetTransparent(go.transform.GetChild(ii).gameObject);
			}
		}

		else {
			if (go.GetComponent<Image>() != null) {
				Image currGO = go.GetComponent<Image>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}

			else if (go.GetComponent<Text>() != null) {
				Text currGO = go.GetComponent<Text>();
				currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, 0);
			}
		}
	}

	public static void UpFade (GameObject[] goArray) {
		//instance = this;
		//SetInstance();
		if (instance == null) {
			Debug.Log("Ain't working");
		}

		else {
			instance.StartCoroutine("FadeUp", goArray);
		}
		
	}

	public static void UpFade (List<GameObject> goArray) {
		//instance = this;
		//SetInstance();
		if (instance == null) {
			Debug.Log("Ain't working");
		}

		else {
			instance.StartCoroutine("FadeUp", goArray);
		}
		
	}

	public static void DownFade (Image img) {
		if (instance == null) {
			Debug.Log("error");
		}

		else {
			instance.StartCoroutine("FadeDown", img);
		}
	}

	// public static void DownFade (Color color) {
	// 	// if (instance == null) {
	// 	// 	Debug.Log("error");
	// 	// }

	// 	// else {
	// 		print("color works");
	// 		StartCoroutine("FadeDown", color);
	// 	// }
	// }

	IEnumerator FadeDown (Image img) {
		while(img.color.a > 0f) {
			img.color = new Color (img.color.r, img.color.g, img.color.b, img.color.a - 0.1f);
			yield return new WaitForSeconds(0.05f);
		}
	}

	static IEnumerator FadeDown (Color color) {
		
		while(color.a > 0f) {
			print("fade test");
			color = new Color (color.r, color.g, color.b, color.a - 0.1f);
			yield return new WaitForSeconds(0.05f);
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
	}

	IEnumerator FadeUp (List<GameObject> goArray) {
		while (goArray[0].GetComponent<Image>().color.a < 1.0f) {
			foreach (GameObject item in goArray) {
				if (item.GetComponent<Image>() != null) {
					Image currGO = item.GetComponent<Image>();
					currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, currGO.color.a + 0.1f);
				}
	
				else if (item.GetComponent<Text>() != null) {
					Text currGO = item.GetComponent<Text>();
					currGO.color = new Color (currGO.color.r, currGO.color.g, currGO.color.b, currGO.color.a + 0.1f);
				}
			}
			yield return new WaitForSeconds(0.05f);
		}
	}
}
