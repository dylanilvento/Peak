using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {
	public bool freezeScout;
	public GameObject[] numbers = new GameObject[3];
	CharacterMovement charMove;
	LevelControl levelControl;

	// Use this for initialization
	void Start () {

		
		// Time.timeScale = 0f;
		levelControl = GetComponent<LevelControl>();

		charMove = levelControl.GetPlayer().GetComponent<CharacterMovement>();
		// levelControl.SetFollow(false);
		StartCoroutine("StartLevel");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator StartLevel () {
		yield return new WaitForSeconds(0.1f);
		levelControl.SetFollow(false);
		yield return new WaitForSeconds(0.75f);
		Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();;

		foreach (GameObject num in numbers) {
			// print("working");
			GameObject currNum = Instantiate(num, camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth/2, camera.pixelHeight/1.1f, 0)), Quaternion.identity);
			currNum.transform.SetParent(GameObject.Find("Canvas").transform, false);
			yield return new WaitForSeconds(0.45f);
			Destroy(currNum);
		}

		if (!freezeScout) charMove.movementOff = false; charMove.grounded = true;
		levelControl.SetFollow(true);
		// Time.timeScale = 1f;
	}
}
