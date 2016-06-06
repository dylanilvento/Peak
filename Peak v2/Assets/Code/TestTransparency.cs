using UnityEngine;
using System.Collections;

public class TestTransparency : MonoBehaviour {

	Texture2D texture;

	// Use this for initialization
	void Start () {
		texture =  GetComponent<SpriteRenderer>().sprite.texture;

		for (int y = 0; y < texture.height; y++) {
            for (int x = 0; x < 1; x++) {
                Color color = Color.clear;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
