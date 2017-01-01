using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using System.Diagnostics;
using System;

public class Finish : MonoBehaviour {

	//Transform player;
	//float relPos;
	// Image winScreen;

	public Animator flagAnim;

	public bool winActive = false;
	public int nextLevel;
	//GameOverCollider goCollider;
	//Camera camera;
	public GameObject statusScreen;
	public string statusText;
	// Use this for initialization
	void Start () {
		//statusScreen.SetActive(false);
		//player = GameObject.Find("Scout").transform;
		//relPos = player.position.x - transform.position.x;
		//goCollider = GameObject.Find("Game Over Collider").GetComponent<GameOverCollider>();
		// winScreen = GameObject.Find("Win Screen").GetComponent<Image>();
		//camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (player.position.x - relPos, transform.position.y, transform.position.z);
		
		//StartCoroutine("MoveUp");

		if (winActive && (Input.GetKeyDown("space") || XCI.GetButton(XboxButton.A) || XCI.GetButton(XboxButton.Start))) {
			if (nextLevel == 0) {
				try {
					Process myProcess = new Process();
			        myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
			        myProcess.StartInfo.CreateNoWindow = false;
			        // myProcess.StartInfo.UseShellExecute = false;
			        myProcess.StartInfo.FileName = "C:\\Users\\Dylan\\Desktop\\BnP\\Launcher\\Launcher.exe";
			        myProcess.Start();
			    }
			    catch (Exception e) {
		            Console.WriteLine(e.Message);
		        }

				Application.Quit();
			}
			else {
				Time.timeScale = 1f;
				// Application.LoadLevel(Application.loadedLevel);
				Application.LoadLevel(nextLevel);
			}
			
		}
	}

	/*IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}*/

	void OnTriggerEnter2D (Collider2D other) {
		

		if (other.gameObject.name.Equals("Scout Group")) {
			// print("winning");

			other.gameObject.GetComponent<CharacterMovement2>().SetMovementOff(true);

			flagAnim.SetTrigger("Raise");
			// Time.timeScale = 0f;
			// winScreen.enabled = true;
			int deathCnt = GameOverCollider.deathCnt;
			// Analytics.CustomEvent("winGame", new Dictionary<string, object>
			// {
			// 	{"winTime", Time.timeSinceLevelLoad},
			// 	{"numDeaths", deathCnt}
			// });

			statusScreen.SetActive(true);
			statusScreen.transform.GetChild(0).GetComponent<Text>().text = statusText;
			winActive = true;



		}
	}
}
