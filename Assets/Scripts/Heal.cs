using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    GameObject gm;

	// Use this for initialization
	void Start () {

        gm = GameObject.Find("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gm.SendMessage("PlayHealSound");
            collision.SendMessage("gainHp", 3f);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }


}
