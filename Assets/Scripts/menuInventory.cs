using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuInventory : MonoBehaviour {

    public GameObject ui;
    public GameObject list;
    int indice = 0;
    bool locked = false;

    GameObject gm;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("Manager");
        list.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.GetComponent<GameManager>().gameState.Equals("inventory") && Input.GetButtonDown("B"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

        if(Input.GetButtonDown("Y") && gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            gm.GetComponent<GameManager>().gameState = "inventory";
            list.SetActive(true);
            ui.SetActive(false);
        }
        else if (Input.GetButtonDown("Y") && gm.GetComponent<GameManager>().gameState.Equals("inventory"))
        {
            gm.GetComponent<GameManager>().gameState = "play";
            list.SetActive(false);
            ui.SetActive(true);
        }

        if (gm.GetComponent<GameManager>().gameState.Equals("inventory"))
        {
            if (Input.GetAxis("Vertical") < 0 && !locked)
            {
                locked = true;
                indice++;
                if (indice > list.transform.childCount - 1) indice = 0;
            }
            else if (Input.GetAxis("Vertical") > 0 && !locked)
            {
                locked = true;
                indice--;
                if (indice < 0) indice = list.transform.childCount - 1;
            }

            if (Input.GetAxis("Vertical") == 0)
            {
                locked = false;
            }
            selection();
        }
            
    }

    void selection()
    {
        for (int i = 0; i < list.transform.childCount; i++)
        {
            list.transform.GetChild(i).GetComponent<Animator>().SetBool("hover", false);
        }
        list.transform.GetChild(indice).GetComponent<Animator>().SetBool("hover", true);
    }
}
