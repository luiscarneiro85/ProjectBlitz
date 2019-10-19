using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    Animator anim;
    GameObject knn;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        knn = GameObject.Find("KnnWatcher");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Attack")
        {
            anim.SetBool("fire",false);
           //if (knn.GetComponent<knnRecord>().knnAtivar)
            //{
                knn.GetComponent<knnRecord>().numberOfBoxes++;
            //}
        }
    }

}
