﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	Transform player;
	Vector2 relPos;
	Camera camera;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Scout").transform;
		camera = GetComponent<Camera>();
		relPos = player.position - transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
		StartCoroutine("MoveUp");
	}

	IEnumerator MoveUp () {
		while (camera.WorldToScreenPoint(player.position).y > Screen.height / 1.5f) {
			transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
