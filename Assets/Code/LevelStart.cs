using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {

	public GameObject[] numbers = new GameObject[3];

	// Use this for initialization
	void Start () {

		// Time.timeScale = 0f;
		StartCoroutine("StartLevel");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator StartLevel () {
		yield return new WaitForSeconds(1f);
		Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();;

		foreach (GameObject num in numbers) {
			print("working");
			GameObject currNum = Instantiate(num, camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0)), Quaternion.identity);
			currNum.transform.SetParent(GameObject.Find("Canvas").transform, false);
			yield return new WaitForSeconds(1f);
			Destroy(currNum);
		}

		Time.timeScale = 1f;
	}
}
