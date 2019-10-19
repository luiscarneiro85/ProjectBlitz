using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedShoot : MonoBehaviour {

    public int id = 0;
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
            collision.GetComponent<Player>().shoot = gm.GetComponent<GameManager>().shootList[id];
            collision.GetComponent<Player>().shootID = id;
            Destroy(this.gameObject);
        }
    }

}
