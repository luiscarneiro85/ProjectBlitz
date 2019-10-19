using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour {

    public GameObject fire;
    public bool ready = true;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Blitz");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    
    public void Kabum()
    {
        if (ready)
        {
            player.GetComponent<AudioSource>().GetComponent<AudioSource>().PlayOneShot(player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
            Vector3 position;
            Instantiate(fire, transform.position, transform.rotation);

            position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            Instantiate(fire, position, transform.rotation);

            StartCoroutine(Timer());
            ready = false;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().effectTime);
        foreach(GameObject fire in GameObject.FindGameObjectsWithTag("Fire"))
        {
            Destroy(fire.gameObject);
        }
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
