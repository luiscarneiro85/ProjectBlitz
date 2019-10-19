using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H6 : MonoBehaviour {

    Transform target;
    Vector2 velocity;
    public float smoothTime;

	// Use this for initialization
	void Start () {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("Blitz").transform;

	}
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > 1.5f)
        {
            float posX = Mathf.Round(Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime) * 100) / 100;
            float posY = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime) * 100) / 100;
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
