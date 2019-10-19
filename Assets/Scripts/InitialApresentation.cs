using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialApresentation : MonoBehaviour {

    public GameObject fatecLogo;
    public GameObject a2vlLogo;
    bool end = false;

	// Use this for initialization
	void Start () {
        fatecLogo.SetActive(true);
        a2vlLogo.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (a2vlLogo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            SceneManager.LoadScene("Pesquisa");
        }

    }

    public void ChangeLogo()
    {
        if (!end)
        {
            fatecLogo.SetActive(false);
            a2vlLogo.SetActive(true);
            end = true;
        }
        else
        {
            SceneManager.LoadScene("Pesquisa");
        }
        
    }


}
