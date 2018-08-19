using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    Transform target;
    public Vector3 direction;
    public float speed;

    public float damage = 1;
    public float delay = .3f;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Aim").transform;
        direction = (target.position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(direction * speed * Time.deltaTime);
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
        else if (collision.tag == "xp")
        {
           
        }
        else
        {
            direction = new Vector3(0, 0, 0);
            anim.SetTrigger("collision");
        }
    }

    void destroyObject()
    {
        Destroy(this.gameObject);
    }

}
