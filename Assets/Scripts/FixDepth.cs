using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDepth : MonoBehaviour {
    public bool fixEveryFrame;
    SpriteRenderer spr;
	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        spr.sortingLayerName = "Player";
        spr.sortingOrder = Mathf.RoundToInt(-transform.position.y*100);
	}
	
	// Update is called once per frame
	void Update () {
		if(fixEveryFrame)
        {
            spr.sortingOrder = Mathf.RoundToInt(-transform.position.y*100);
        }
	}
}
