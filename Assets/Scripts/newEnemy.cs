using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemy : MonoBehaviour {

    RaycastHit2D ray;
    public bool horizontal;
    public float speed;
    public float distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(horizontal)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {

        }
        Debug.DrawRay(transform.position, Vector2.right, Color.red,distance);
        ray = Physics2D.Raycast((Vector2)transform.position, Vector2.right, distance, 1 << LayerMask.NameToLayer("Default"));
        if(ray.collider != null)
        {
            if(ray.collider.tag != "Player")
            {
                speed *= -1;
            }
        }
	}
}
