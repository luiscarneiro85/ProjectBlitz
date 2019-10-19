using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacao : MonoBehaviour {

    GameObject target;
    Vector3 direction;

	// Use this for initialization
	void Start () {

        target = GameObject.Find("Aim");
        direction = target.transform.position - transform.position;
        transform.up = direction;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
