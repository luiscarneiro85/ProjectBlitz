using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTorreta : MonoBehaviour {

    GameObject player;

    Vector3 direction;
    public float speed;
    public float attackDamage;
    Animator anim;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.SendMessage("takeDamage", attackDamage);
            collision.GetComponent<Animator>().SetTrigger("hit");
            direction = new Vector3(0, 0, 0);
            anim.SetTrigger("collision");
        }
        else if(collision.tag != "Sentry")
        {
            direction = new Vector3(0, 0, 0);
            anim.SetTrigger("collision");
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void destroyObject()
    {
        Destroy(this.gameObject);
    }
}
