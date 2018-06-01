using UnityEngine;
//using System.Collections;

[ExecuteInEditMode]
public class SnapTiles : MonoBehaviour
{
    public float snapValue = 0.6f;

    void Update()
    {
        if(Application.isPlaying)
            return;
        float x, y;

        x = Mathf.Round(transform.position.x / snapValue) * snapValue;
        y = Mathf.Round(transform.position.y / snapValue) * snapValue;    

        transform.position = new Vector3(x, y, 0);

        //GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-y);
     }  
}