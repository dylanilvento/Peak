using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActivationCollider : MonoBehaviour {

	public TutorialControl tutorial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		// print("test");
		if (other.gameObject.name.Equals("Left Curtain") && gameObject.name.Equals("Left Activation Collider")) {
			tutorial.SetCreatedBridge(0, true);
		}

		else if (other.gameObject.name.Equals("Right Curtain") && gameObject.name.Equals("Right Activation Collider")) {
			tutorial.SetCreatedBridge(1, true);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		// print("test");
		if (other.gameObject.name.Equals("Left Curtain") && gameObject.name.Equals("Left Activation Collider")) {
			tutorial.SetCreatedBridge(0, false);
		}

		else if (other.gameObject.name.Equals("Right Curtain") && gameObject.name.Equals("Right Activation Collider")) {
			tutorial.SetCreatedBridge(1, false);
		}
	}
}
