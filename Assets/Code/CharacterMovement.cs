using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour {
	Rigidbody2D rb;
	Animator anim;

	public bool movementOff = false;

	bool grounded = true;
	bool jumped = false;
	bool paused = false;
	static bool started = false;
	bool foreWorld = true;

	float lastTimeGrounded;

	GameObject collidedWith;
	SpriteRenderer sr;
	//GameObject collidedWith;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		
		if (movementOff) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
		}
		// if (!started) {
		// 	Analytics.CustomEvent("report", new Dictionary<string, object>
		// 	{
		// 		{"operatingSystem", SystemInfo.operatingSystem}
		// 	});

		// 	started = true;
		// }

		// print("started");

		grounded = true;
		jumped = false;
		paused = false;
		//anim.SetBool("Walk", true);
		//print(SystemInfo.operatingSystem);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (Time.realtimeSinceStartup - lastTimeGrounded > 0.01f) {
		if (grounded && !movementOff) {

			anim.SetBool("Walk", true);
			rb.velocity = new Vector2(2f, 1.25f);

			// rb.gravityScale = 1f;
		}
		
		// else {
		// 	//rb.velocity = new Vector2(0f, 0f);
			
		// 	//rb.gravityScale = 2f;
			
		// 	//anim.SetBool("Walk", false);
		// }
	
	}

	void OnCollisionEnter2D (Collision2D other) {
		//print(other.gameObject.name);
		/*if (other.gameObject.name.Contains("Fore Rock") || other.gameObject.name.Contains("Back Rock") && !jumped) {
			// rb.velocity = new Vector2(2f, 0f);
			//print("rock enter");
			grounded = true;
		}*/
		grounded = true;
		//lastTimeGrounded = Time.realtimeSinceStartup;

	}


	//TEST TO SEE IF GROUNDED IS CAUSE
	/*void OnCollisionStay2D (Collision2D other) {
		//print(other.gameObject.name);
		if (other.gameObject.name.Contains("Rock") || other.gameObject.name.Contains("Rock") && !jumped) {
			// rb.velocity = new Vector2(2f, 0f);
			//print("rock enter");
			grounded = true;
		}

	}*/

	void OnCollisionExit2D (Collision2D other) {
		//print(other.gameObject.name);
		
		//if (other.gameObject.name.Contains("Fore Rock") || other.gameObject.name.Contains("Back Rock")) {
			grounded = false;
		//}
	}

	void OnTriggerEnter2D (Collider2D other) {
		collidedWith = other.gameObject;

		if (collidedWith.name.Equals("Right Curtain") || collidedWith.name.Equals("Left Curtain")) {
			print("Entered curtain");
			SwitchWorlds();
			// sr.color = new Color(1f, 1f, 1f, 0f);
			//gameObject.layer = 9;
			//sr.sortingOrder = 0;
		}

		if (collidedWith.name.Contains("Boost")) {
			//print(collidedWith.name);
			//print(collidedWith.name.Contains("Back"));
			StartCoroutine("Jump");
			
		}

	}

	void OnTriggerExit2D (Collider2D other) {
		//collidedWith = other.gameObject;
		
		if (collidedWith.name.Equals("Left Curtain")) {
			if (transform.position.x >= collidedWith.transform.position.x) {
				SetBackWorld();
			}
			else {
				SetForeWorld();
			}
		}

		else if (collidedWith.name.Equals("Right Curtain")) {
			if (transform.position.x <= collidedWith.transform.position.x) {
				SetBackWorld();
			}
			else {
				SetForeWorld();
			}
		}

		/*if (collidedWith.name.Equals("Curtain Collider")) {
			print("Exited curtain");
			gameObject.layer = 8;
			collidedWith = null;
			sr.sortingOrder = 3;
		}*/

	}

	void SwitchWorlds () {
		if (!foreWorld) {
			gameObject.layer = 8;
			sr.sortingOrder = 4;
			foreWorld = true;
		}
		else {
			gameObject.layer = 9;
			sr.sortingOrder = 0;
			foreWorld = false;
		}
	}

	void SetForeWorld () {
		gameObject.layer = 8;
		sr.sortingOrder = 4;
		foreWorld = true;
		//sr.color = Color.white;
	}

	void SetBackWorld () {
		gameObject.layer = 9;
		sr.sortingOrder = 0;
		foreWorld = false;
		//sr.color = Color.red;
	}

	IEnumerator Jump () {
		print("Got to jump");
		grounded = false;
		//print("Grounded is " + grounded);

		jumped = true;
		rb.velocity = new Vector2(2f, 8f);

		//collidedWith = null;

		yield return new WaitForSeconds(0.5f); //not this
		jumped = false;

		if (collidedWith.name.Contains("Back")) {
			// print("back reached");
			gameObject.layer = 9;
			collidedWith = GameObject.Find("Curtain Collider");
		}
		else if (collidedWith.name.Contains("Fore")) {
			gameObject.layer = 8;
			collidedWith = null;
		}
		//print("rb.x: " + rb.velocity.x + ", rb.y:" + rb.velocity.y);
	}

}
