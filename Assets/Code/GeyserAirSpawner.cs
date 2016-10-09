using UnityEngine;
using System.Collections;

public class GeyserAirSpawner : MonoBehaviour {

	public GameObject air;

	// Use this for initialization
	void Start () {
		StartCoroutine("SpawnAir");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnAir () {
		while (true) {
			float t = Random.Range(0f, 0.05f);

			yield return new WaitForSeconds(t);

			Instantiate (air, new Vector2(transform.position.x + Random.Range(-0.04f, 0.095f), transform.position.y), Quaternion.identity);

		}
	}
}
