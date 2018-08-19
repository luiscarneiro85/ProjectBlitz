using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundControl : MonoBehaviour {

    Animator anim;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        setStart();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            GameObject gm = GameObject.Find("Logo");
            gm.SendMessage("setLogo");

        }
    }

    void setStart()
    {
        anim.SetTrigger("start");
    }
}
