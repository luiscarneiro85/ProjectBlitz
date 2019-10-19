using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    Transform target;
    public Vector3 direction;

    public Vector3 direction1;
    public Vector3 direction2;
    Vector3 target1;
    Vector3 target2;

    public float speed;
    public int ID;
    public bool auxON;
    public float damage = 1;
    public float delay = .3f;
    public bool triple = false;
    GameObject player;
    Animator anim;

    Vector3 targetRotation;

    public GameObject auxShoot;
    public GameObject auxShoot_1;

    public Sprite iconUI;

    // Use this for initialization
    void Start () {
        //transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 45);
        //player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        target = GameObject.Find("Aim").transform;
        direction = (target.position - transform.position).normalized;
        if (auxON)
        {
            if (direction.x == 0)
            {
                target1 = new Vector3(target.position.x + .75f, target.position.y, target.position.z);
                target2 = new Vector3(target.position.x - .75f, target.position.y, target.position.z);

                direction1 = (target1 - transform.position).normalized;
                direction2 = (target2 - transform.position).normalized;
            }
            if (direction.x != 0)
            {
                target1 = new Vector3(target.position.x, target.position.y + .75f, target.position.z);
                target2 = new Vector3(target.position.x, target.position.y - .75f, target.position.z);

                direction1 = (target1 - transform.position).normalized;
                direction2 = (target2 - transform.position).normalized;
            }
        }
        if (triple)
        {
            Instantiate(auxShoot, transform.position, transform.rotation);
            //player.GetComponent<Player>().auxShootID++;
            Instantiate(auxShoot_1, transform.position, transform.rotation);
        }

         


    }
	
	// Update is called once per frame
	void Update () {
        if (auxON)
        {
            switch (ID)
            {
                case 0:
                    transform.Translate(direction1 * speed * Time.deltaTime);
                    break;
                case 1:
                    transform.Translate(direction2 * speed * Time.deltaTime);
                    break;
            }
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }



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
        else if(collision.tag == "Attack")
        {

        }
        else if (collision.tag == "Clone")
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
