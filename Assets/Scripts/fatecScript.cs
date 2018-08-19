using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fatecScript : MonoBehaviour {

    Animator anim;
    GameObject go;
    bool end = false;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        go = GameObject.Find("Av2lLogo");
        go.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            GetComponent<Image>().sprite = null;
        }
    }

    public void changeAnimation()
    {
        if (!end)
        {
            this.enabled = false;
            go.SetActive(true);
            end = true;
        }
        else
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
        
    }
}
