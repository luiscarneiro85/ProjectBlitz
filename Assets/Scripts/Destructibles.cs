using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour {

    Animator anim;
    public GameObject xp;
    public GameObject potion;
    public bool box;
    int aux;
    float hp = 2;

    GameObject gm;
    GameObject knn;

    public bool active = false;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("Manager");
        knn = GameObject.Find("KnnWatcher");
	}
	
	// Update is called once per frame
	void Update () {

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end") || hp <= 0)
        {
            if (!box)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sortingLayerName = "Door";
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sortingLayerName = "Door";
            }

            aux = Mathf.RoundToInt(Random.Range(1, 10));
            switch(aux)
            {
                case 1:
                    //drop potion
                    Instantiate(gm.GetComponent<GameManager>().heal, transform.position, transform.rotation);
                    break;
                case 3:
                case 4:
                case 5:
                    Instantiate(gm.GetComponent<GameManager>().energy, transform.position, transform.rotation);
                    break;
            }

            GetComponent<Destructibles>().enabled = false;
            //if (knn.GetComponent<knnRecord>().knnAtivar)
            //{
                knn.GetComponent<knnRecord>().numberOfBoxes++;
            //}
                
        }


	}

    public void LoseHp()
    {
        hp -= 1;
        anim.SetTrigger("hit");
        if (box)
        {
            gm.SendMessage("BoxBreakSound");
        }
        else
        {
            gm.SendMessage("JarBreakSound");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            hp -= 1;
            anim.SetTrigger("hit");
            if (box)
            {
                gm.SendMessage("BoxBreakSound");
            }
            else
            {
                gm.SendMessage("JarBreakSound");
            }
        }
    }

    private void OnBecameVisible()
    {
        active = true;
    }
    private void OnBecameInvisible()
    {
        active = false;
    }
}
