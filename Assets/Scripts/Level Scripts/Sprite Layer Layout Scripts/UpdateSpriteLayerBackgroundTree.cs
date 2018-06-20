using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateSpriteLayerBackgroundTree : MonoBehaviour
{
	public SpriteLayerLayout spriteLayerLayout;
	public SpriteLayer layer;

	public GameObject tree, trunk;

	public int rank;

	Dictionary<SpriteLayer, int[]> spriteMap = new Dictionary<SpriteLayer, int[]>();

    void Awake() {
		
   
    }

    void Update() {
		spriteMap.Add(SpriteLayer.BackworldBackgroundObjects, new int[] {spriteLayerLayout.backworldBackgroundObjectsLowerBound, spriteLayerLayout.backworldBackgroundObjectsUpperBound});
		spriteMap.Add(SpriteLayer.ForeworldBackgroundObjects, new int[] {spriteLayerLayout.foreworldBackgroundObjectsLowerBound, spriteLayerLayout.foreworldBackgroundObjectsUpperBound});

		tree.GetComponent<SpriteRenderer>().sortingOrder = spriteMap[layer][1] - ((rank * 2) - 2);
		trunk.GetComponent<SpriteRenderer>().sortingOrder = spriteMap[layer][1] - ((rank * 2) - 3);

		spriteMap.Clear();
    }
}
