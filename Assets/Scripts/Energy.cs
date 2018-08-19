using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {

    Vector3 target;
    float distance;
    float radius = 5;

    public float speed;
    float delay;
    bool isActive = false;
    GameObject player;
    RaycastHit2D hit;
    GameObject gm;


    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        delay = Random.Range(0.5f, 1.2f);
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("Manager");

    }
	
	// Update is called once per frame
	void Update () {
        target = (player.transform.position - transform.position).normalized;
        distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= radius && !isActive)
        {
            StartCoroutine(startLife());
        }

        if(isActive)
        {
            transform.Translate(target * speed * Time.deltaTime);
        }
        

	}

    IEnumerator startLife()
    {
        yield return new WaitForSeconds(delay);
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gm.SendMessage("PlayerEnergySound");
            collision.SendMessage("getEnergy", 1);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
