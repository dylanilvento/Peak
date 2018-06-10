using UnityEngine;
using System.Collections;
// using XboxCtrlrInput;
using Rewired;

public class CurtainCollider : MonoBehaviour {

	BoxCollider2D box;
	float sizeChange = -0.1f;
	float initialSize, smallestDist, currDist;
	GameObject rightCurtain, leftCurtain, renderCurtain;
	bool paused = false;

	public Camera camera;
	LevelControl levelControl;
	bool hasController;

    public int curtainJumpMaxCharges; //Max amount of charges for the curtain charges
    public int curtainJumpCharges;  //How many charges you have
    public float curtainJumpDelay; //The delay in time between when you can use a jump (in seconds)
    public bool curtainJumpRecharging; //Is jump currently being 'recharged' (To prevent multiple charges from recharging)
    public float curtainJumpLerpSpeed;
    public float curtainJumpDistance; //This is basiclly in pixels
    public float curtainJumpRechargeTime; //Recharge time for a single 'charge' 

	public int playerId = 0; // The Rewired player id of this character

    private Player player; // The Rewired Player
	
	void Awake () {
		player = ReInput.players.GetPlayer(playerId);
		
		
        // Subscribe to events
        ReInput.ControllerConnectedEvent += OnControllerConnected;

        // ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        // ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
    }

	void OnControllerConnected(ControllerStatusChangedEventArgs args) {
        // print("A controller was connected! Name = " + args.name + " Id = " + args.controllerId + " Type = " + args.controllerType);
		hasController = player.controllers.ContainsController<Joystick>(args.controllerId);
		// print(hasController);
    }
	// Use this for initialization
	void Start () {
		renderCurtain = GameObject.Find("Curtain Renderer");
		box = GetComponent<BoxCollider2D>();
		initialSize = box.size.x;
		rightCurtain = GameObject.Find("Right Curtain");
		leftCurtain = GameObject.Find("Left Curtain");
		levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
		smallestDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);
        curtainJumpMaxCharges = 3;
        curtainJumpCharges = curtainJumpMaxCharges;
        curtainJumpLerpSpeed = 0.5f;
        curtainJumpDistance = 10.0f;
        curtainJumpRecharging = false;
        curtainJumpDelay = 1.0f;
        curtainJumpRechargeTime = 6.0f;
    }
	
	// Update is called once per frame
	void Update () {
		currDist = Mathf.Abs(rightCurtain.transform.position.x) - Mathf.Abs(leftCurtain.transform.position.x);

		/*if (Input.GetKeyDown("escape")) {
			paused = !paused;
		}*/

		if (camera.WorldToScreenPoint(leftCurtain.transform.position).x < 0) {
			// leftCurtain.transform.position = 
		}

		if (!paused && levelControl.GetFollow()) {
			
			if(player.GetAxis("Left Curtain Movement") < 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x > Screen.width + 10) {
				
				box.size = new Vector2 (box.size.x + 0.1f, box.size.y);
				renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x + 0.2f, renderCurtain.transform.localScale.y);
				renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x - 0.05f, renderCurtain.transform.localPosition.y);
				
				leftCurtain.transform.position = new Vector2 (leftCurtain.transform.position.x - 0.1f, leftCurtain.transform.position.y);
				transform.position = new Vector2 (transform.position.x - 0.05f, transform.position.y);
			}
			
			if(player.GetAxis("Left Curtain Movement") > 0f || camera.WorldToScreenPoint(leftCurtain.transform.position).x < -15f) {
				if (box.size.x > initialSize) {
					
					box.size = new Vector2 (box.size.x - 0.1f, box.size.y);
					renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x - 0.2f, renderCurtain.transform.localScale.y);
					renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x + 0.05f, renderCurtain.transform.localPosition.y);
					
					leftCurtain.transform.position = new Vector2 (leftCurtain.transform.position.x + 0.1f, leftCurtain.transform.position.y);
					transform.position = new Vector2 (transform.position.x + 0.05f, transform.position.y);
				}
			}

			
			if(player.GetAxis("Right Curtain Movement") < 0f || camera.WorldToScreenPoint(rightCurtain.transform.position).x > Screen.width + 10) {
				if (box.size.x > initialSize) {
					
					box.size = new Vector2 (box.size.x - 0.1f, box.size.y);
					renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x - 0.2f, renderCurtain.transform.localScale.y);
					renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x - 0.05f, renderCurtain.transform.localPosition.y);
					
					rightCurtain.transform.position = new Vector2 (rightCurtain.transform.position.x - 0.1f, rightCurtain.transform.position.y);
					transform.position = new Vector2 (transform.position.x - 0.05f, transform.position.y);
				}
				
			}

			
			if(player.GetAxis("Right Curtain Movement") > 0f || camera.WorldToScreenPoint(rightCurtain.transform.position).x < -15f) {
			
				box.size = new Vector2 (box.size.x + 0.1f, box.size.y);
				renderCurtain.transform.localScale = new Vector2 (renderCurtain.transform.localScale.x + 0.2f, renderCurtain.transform.localScale.y);
				renderCurtain.transform.localPosition = new Vector2 (renderCurtain.transform.localPosition.x + 0.05f, renderCurtain.transform.localPosition.y);
				rightCurtain.transform.position = new Vector2 (rightCurtain.transform.position.x + 0.1f, rightCurtain.transform.position.y);
				transform.position = new Vector2 (transform.position.x + 0.05f, transform.position.y);
			}
		}
        // Curtain Jump
        if (!paused && levelControl.GetFollow())
        {
            if (player.GetButtonDown("Left Curtain Jump") && curtainJumpCharges > 0) 
            {
                // Start left curtain jump corutine which does something similar to the below until it reaches the 'endpoint' 
                StartCoroutine("CurtainJumpLeft");
            }
            
            if (player.GetButtonDown("Right Curtain Jump") && curtainJumpCharges > 0)
            {
                // Start right curtain jump corutine which does something similar to the below until it reaches the 'endpoint' 
                StartCoroutine("CurtainJumpRight");
            }

            if (curtainJumpCharges < curtainJumpMaxCharges && !curtainJumpRecharging)
            {
                //Start recharge corutine
                StartCoroutine("CurtainRecharge");
            }
        }

    }

    IEnumerator CurtainJumpLeft()
    {
        curtainJumpCharges -= 1;
        for (int i = 0; i < curtainJumpDistance; i++)
        {
            box.size = Vector2.Lerp(box.size, new Vector2(box.size.x + (0.1f * i), box.size.y), curtainJumpLerpSpeed);
            renderCurtain.transform.localScale = Vector2.Lerp(renderCurtain.transform.localScale, new Vector2(renderCurtain.transform.localScale.x + (0.2f * i), renderCurtain.transform.localScale.y), curtainJumpLerpSpeed);
            renderCurtain.transform.localPosition = Vector2.Lerp(renderCurtain.transform.localPosition, new Vector2(renderCurtain.transform.localPosition.x - (0.05f * i), renderCurtain.transform.localScale.y), curtainJumpLerpSpeed);
            leftCurtain.transform.position = Vector2.Lerp(leftCurtain.transform.position, new Vector2(leftCurtain.transform.position.x - (0.1f * i), leftCurtain.transform.position.y), curtainJumpLerpSpeed);
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - (0.05f * i), transform.position.y), curtainJumpLerpSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(curtainJumpDelay);
    }
    IEnumerator CurtainJumpRight()
    {
        curtainJumpCharges -= 1;
        for (int i = 0; i < curtainJumpDistance; i++)
        {
            box.size = Vector2.Lerp(box.size, new Vector2(box.size.x + (0.1f * i), box.size.y), curtainJumpLerpSpeed);
            renderCurtain.transform.localScale = Vector2.Lerp(renderCurtain.transform.localScale, new Vector2(renderCurtain.transform.localScale.x + (0.2f * i), renderCurtain.transform.localScale.y), curtainJumpLerpSpeed);
            renderCurtain.transform.localPosition = Vector2.Lerp(renderCurtain.transform.localPosition, new Vector2(renderCurtain.transform.localPosition.x + (0.05f * i), renderCurtain.transform.localScale.y), curtainJumpLerpSpeed);
            rightCurtain.transform.position = Vector2.Lerp(rightCurtain.transform.position, new Vector2(rightCurtain.transform.position.x + (0.1f * i), rightCurtain.transform.position.y), curtainJumpLerpSpeed);
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + (0.05f * i), transform.position.y), curtainJumpLerpSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(curtainJumpDelay);
    }
    IEnumerator CurtainRecharge()
    {
        curtainJumpRecharging = true;
        yield return new WaitForSeconds(curtainJumpRechargeTime);
        curtainJumpCharges += 1;
        if (curtainJumpCharges > curtainJumpMaxCharges)
        {
            curtainJumpCharges = curtainJumpMaxCharges;
        }
        curtainJumpRecharging = false;
    }

    public void SwitchPausedGame () {
		paused = !paused;
	}

	public void SetPausedGame (bool val) {
		paused = val;
	}
}
