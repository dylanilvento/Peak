using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class PlayDemoReel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;
        movie.loop = true;
        movie.Play();
        Screen.SetResolution(1920, 1080, true);
	}
	
	// Update is called once per frame
	void Update () {
		if (XCI.GetButtonDown(XboxButton.Start)) {
			Time.timeScale = 1f;
			Application.LoadLevel(0);
		}
	}
}
