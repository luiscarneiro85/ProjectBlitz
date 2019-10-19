using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDirection : MonoBehaviour {

    Transform target;
    Vector2 direction;


	// Use this for initialization
	void Start () {
        target = GameObject.Find("Aim").transform;


    }

    // Update is called once per frame
    void Update () {
        direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        transform.up = direction;
    }
}
