using UnityEngine;
using System.Collections;

//Note on the LERPs in camera movement - we use these to smooth out the movement to prevent it from being 'Jittery'
public class CameraFollow : MonoBehaviour
{
    // public GameObject target;
    Transform player;
    Vector2 relPos;
    Camera camera;
    public bool canMove = true;
    // public float yAxisLerpSpeed = 15.0f;
    // public float xAxisLerpSpeed = 5.0f;
    LevelControl levelControl;

    [Range(-5, 5)]
    public float upVal = 1.4f;
    [Range(-8, 8)]
    public float downVal = 4f;
    // Use this for initialization
    void Start()
    {
        levelControl = GameObject.Find("Game Controller").GetComponent<LevelControl>();
        player = levelControl.GetPlayer().transform;
        camera = GetComponent<Camera>();
        relPos = player.position - transform.position;


    }

    // Update is called once per frame
    
    //putting CameraFollow on update and ParallaxLayer on fixedupdate seems to fix the problem
    void Update() 
    {
        if (levelControl.GetFollow())
        {
            transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
            // if (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal) StartCoroutine("MoveUp");
            // else if (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal) StartCoroutine("MoveDown");

        }

        // else {
        // 	transform.position = new Vector3(player.position.x - relPos.x, transform.position.y, transform.position.z);
        // }


    }

    //vertical follow moves better in LateUpdate
     void LateUpdate() 
    {
        if (levelControl.GetFollow())
        {
            Vector2 vectorToPlayer = new Vector2(0f, player.transform.position.y - transform.position.y);
            float distanceToPlayer = vectorToPlayer.magnitude;

            //normalized direction, invert sign
            //below camera is positive, above camera is negative
            float direction = (vectorToPlayer.y / distanceToPlayer) * -1; 

            print("dist: " + distanceToPlayer);
            print("direction: " + direction);
            
            // if (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal) {
            if (distanceToPlayer * direction < upVal) {
                print("test");
            
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, player.position.y - relPos.y, transform.position.z), 0.5f * Time.deltaTime);
            
            }

            else if (distanceToPlayer * direction > downVal) {
                // StartCoroutine("MoveDown");

                print("downVal: " + downVal);

                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, player.position.y - relPos.y, transform.position.z), 3f * Time.deltaTime);



            }

        }

    }

    public void CallCameraShake()
    {
        StartCoroutine("CameraShake");
    }

    IEnumerator CameraShake()
    {
        levelControl.SetFollow(false);
        Vector3 originalPos = transform.position;

        // for (int ii = 0; ii < 5; ii++) {
        // 	float xPos = Random.Range(-0.5f, 0.5f), yPos = Random.Range(-0.5f, 0.5f);
        // 	transform.position = new Vector3(originalPos.x + xPos, originalPos.y + yPos, originalPos.z);
        // 	yield return new WaitForSeconds(0.1f);
        // }

        float elapsed = 0.0f, duration = 0.5f, magnitude = 2f;

        // Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            yield return null;
        }

        transform.position = originalPos;

    }

    // IEnumerator MoveUp()
    // {
    //     while (camera.WorldToScreenPoint(player.position).y > Screen.height / upVal && levelControl.GetFollow())
    //     {
    //         transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
    //         yield return new WaitForSeconds(Time.deltaTime); //changed this from fixedDeltaTime

    //     }
    // }

    // IEnumerator MoveDown()
    // {
    //     while (camera.WorldToScreenPoint(player.position).y < Screen.height / downVal && levelControl.GetFollow())
    //     {
    //         transform.position = new Vector3(transform.position.x, transform.position.y - 0.0005f, transform.position.z);
    //         yield return new WaitForSeconds(Time.deltaTime); //changed this from fixedDeltaTime
    //     }
    // }

    public void SetCameraVals(float up, float down)
    {
        upVal = up;
        downVal = down;
    }
}