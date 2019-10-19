using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectBase : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Blitz");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position;
	}

    public void EndAnimation()
    {
        Destroy(this.gameObject);
    }
}
