using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    bool active = false;
    public float attackDamage = 1f;
    public float time;

    Animator anim;

	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider2D>().enabled = false;
        anim = GetComponent<Animator>();

        GetComponent<SpriteRenderer>().sortingLayerName = "Door";
    }
	
	// Update is called once per frame
	void Update () {
        if (!active)
        {
            StartCoroutine(delay());
            anim.SetTrigger("active");
        }
		
	}

    public void activeCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
    public void desactiveCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Clone")
        {
            if (collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("hit"))
            {

            }
            else
            {
                collision.SendMessage("takeDamage", attackDamage);
                collision.GetComponent<Animator>().SetTrigger("hit");
            }
        }
    }

    IEnumerator delay()
    {
        active = true;
        yield return new WaitForSeconds(time);
        active = false;
    }
}
