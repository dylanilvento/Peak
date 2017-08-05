using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSelectManager : MonoBehaviour {

public GameObject levelClearBackground;

	// Use this for initialization
	void Start () {
		if (IntersceneDataHandler.levelToLevelSelectTransition) {
			StartCoroutine("ContinueFromLevel");
		}

		else {
			levelClearBackground.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ContinueFromLevel () {
		for (int ii = 0; ii < 50; ii++) {
			levelClearBackground.transform.position = new Vector2(levelClearBackground.transform.position.x - IntersceneDataHandler.globalLevelClearMovmentRate, levelClearBackground.transform.position.y);
			// continueBox.transform.position = new Vector2(continueBox.transform.position.x + globalReduction, continueBox.transform.position.y);
			yield return new WaitForSeconds(0.01f);

			IntersceneDataHandler.levelToLevelSelectTransition = false;
		}
	}
}
