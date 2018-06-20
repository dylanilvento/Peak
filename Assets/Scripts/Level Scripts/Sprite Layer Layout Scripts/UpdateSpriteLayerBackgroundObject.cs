using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateSpriteLayerBackgroundObject : MonoBehaviour
{
	public SpriteLayerLayout spriteLayerLayout;
	public SpriteLayer layer;

	public int rank;

	Dictionary<SpriteLayer, int[]> spriteMap = new Dictionary<SpriteLayer, int[]>();

    void Awake() {
		
   
    }

    void Update() {
		spriteMap.Add(SpriteLayer.BackworldBackgroundObjects, new int[] {spriteLayerLayout.backworldBackgroundObjectsLowerBound, spriteLayerLayout.backworldBackgroundObjectsUpperBound});
		spriteMap.Add(SpriteLayer.ForeworldBackgroundObjects, new int[] {spriteLayerLayout.foreworldBackgroundObjectsLowerBound, spriteLayerLayout.foreworldBackgroundObjectsUpperBound});

        if (GetComponent<SpriteRenderer>() != null) {
			GetComponent<SpriteRenderer>().sortingOrder = spriteMap[layer][1] - (rank - 1);
		}

		else if (GetComponent<Anima2D.SpriteMeshInstance>() != null) {
			GetComponent<SpriteRenderer>().sortingOrder = spriteMap[layer][1] - (rank - 1);
		}

		spriteMap.Clear();
    }
}
