using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateSpriteLayer : MonoBehaviour
{
	public SpriteLayerLayout spriteLayerLayout;
	public SpriteLayer layer;

	Dictionary<SpriteLayer, int> spriteMap = new Dictionary<SpriteLayer, int>();

    void Awake() {
		
   
    }

    void Update() {
		spriteMap.Add(SpriteLayer.BackworldBackground, spriteLayerLayout.backworldBackground);
		spriteMap.Add(SpriteLayer.BackworldEffects, spriteLayerLayout.backworldEffects);
		spriteMap.Add(SpriteLayer.BackworldBackgroundObjects, spriteLayerLayout.backworldBackgroundObjectsUpperBound);
		spriteMap.Add(SpriteLayer.BackworldPlatforms, spriteLayerLayout.backworldPlatforms);
		spriteMap.Add(SpriteLayer.ForeworldOutlines, spriteLayerLayout.foreworldOutlines);
		spriteMap.Add(SpriteLayer.ScoutBackworldRightFoot, spriteLayerLayout.scoutBackworldRightFoot);
		spriteMap.Add(SpriteLayer.ScoutBackworldRightHand, spriteLayerLayout.scoutBackworldRightHand);
		spriteMap.Add(SpriteLayer.ScoutBackworldBody, spriteLayerLayout.scoutBackworldBody);
		spriteMap.Add(SpriteLayer.ScoutBackworldSash, spriteLayerLayout.scoutBackworldSash);
		spriteMap.Add(SpriteLayer.ScoutBackworldNeckerchief, spriteLayerLayout.scoutBackworldNeckerchief);
		spriteMap.Add(SpriteLayer.ScoutBackworldHat, spriteLayerLayout.scoutBackworldHat);
		spriteMap.Add(SpriteLayer.ScoutBackworldLeftHand, spriteLayerLayout.scoutBackworldLeftHand);
		spriteMap.Add(SpriteLayer.ScoutBackworldNeckerchiefTassleTwo, spriteLayerLayout.scoutBackworldNeckerchiefTassleTwo);
		spriteMap.Add(SpriteLayer.ScoutBackworldLeftFoot, spriteLayerLayout.scoutBackworldLeftFoot);
		spriteMap.Add(SpriteLayer.ScoutBackworldNeckerchiefTassle, spriteLayerLayout.scoutBackworldNeckerchiefTassle);
		spriteMap.Add(SpriteLayer.ScoutBackworldNeckerchiefTassleKnot, spriteLayerLayout.scoutBackworldNeckerchiefTassleKnot);
		
		spriteMap.Add(SpriteLayer.Curtain, spriteLayerLayout.curtain);

		spriteMap.Add(SpriteLayer.ForeworldBackground, spriteLayerLayout.foreworldBackground);
		spriteMap.Add(SpriteLayer.ForeworldEffects, spriteLayerLayout.foreworldEffects);
		spriteMap.Add(SpriteLayer.ForeworldBackgroundObjects, spriteLayerLayout.foreworldBackgroundObjectsUpperBound);
		spriteMap.Add(SpriteLayer.ForeworldPlatforms, spriteLayerLayout.foreworldPlatforms);
		spriteMap.Add(SpriteLayer.BackworldOutlines, spriteLayerLayout.backworldOutlines);
		spriteMap.Add(SpriteLayer.ScoutForeworldRightFoot, spriteLayerLayout.scoutForeworldRightFoot);
		spriteMap.Add(SpriteLayer.ScoutForeworldRightHand, spriteLayerLayout.scoutForeworldRightHand);
		spriteMap.Add(SpriteLayer.ScoutForeworldBody, spriteLayerLayout.scoutForeworldBody);
		spriteMap.Add(SpriteLayer.ScoutForeworldSash, spriteLayerLayout.scoutForeworldSash);
		spriteMap.Add(SpriteLayer.ScoutForeworldHat, spriteLayerLayout.scoutForeworldHat);
		spriteMap.Add(SpriteLayer.ScoutForeworldNeckerchief, spriteLayerLayout.scoutForeworldNeckerchief);
		spriteMap.Add(SpriteLayer.ScoutForeworldLeftHand, spriteLayerLayout.scoutForeworldLeftHand);
		spriteMap.Add(SpriteLayer.ScoutForeworldNeckerchiefTassleTwo, spriteLayerLayout.scoutForeworldNeckerchiefTassleTwo);
		spriteMap.Add(SpriteLayer.ScoutForeworldLeftFoot, spriteLayerLayout.scoutForeworldLeftFoot);
		spriteMap.Add(SpriteLayer.ScoutForeworldNeckerchiefTassle, spriteLayerLayout.scoutForeworldNeckerchiefTassle);
		spriteMap.Add(SpriteLayer.ScoutForeworldNeckerchiefTassleKnot, spriteLayerLayout.scoutForeworldNeckerchiefTassleKnot);

        if (GetComponent<SpriteRenderer>() != null) {
			GetComponent<SpriteRenderer>().sortingOrder = spriteMap[layer];
		}

		else if (GetComponent<Anima2D.SpriteMeshInstance>() != null) {
			GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder = spriteMap[layer];
		}

		else if (GetComponent<LineRenderer>() != null) {
			GetComponent<LineRenderer>().sortingOrder = spriteMap[layer];
		}

		spriteMap.Clear();
    }
}
