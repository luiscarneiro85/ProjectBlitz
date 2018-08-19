using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {

    Animator anim;
    public GameObject menu;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        menu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            menu.SetActive(true);
            GameObject gm = GameObject.Find("MenuPrincipal");
            gm.SendMessage("selection");

        }

    }

    public void setLogo()
    {
        anim.SetTrigger("start");
    }
}
