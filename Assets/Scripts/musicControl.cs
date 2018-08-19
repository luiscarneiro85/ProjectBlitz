using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicControl : MonoBehaviour {

    public AudioClip musica1;
    public AudioClip musica2;
    public AudioClip musica3;

    bool play = false;
    int aux;
    public static float soundVolume;

    public static musicControl instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        aux = menuPrincipal.globalMusic;
	}
	
	// Update is called once per frame
	void Update () {
        if (aux != menuPrincipal.globalMusic) play = false;

        GetComponent<AudioSource>().volume = menuPrincipal.globalVolume;
        if (!play)
        {
            aux = menuPrincipal.globalMusic;
            play = true;
            switch (menuPrincipal.globalMusic)
            {
                case 0:
                    GetComponent<AudioSource>().clip = musica1;
                    GetComponent<AudioSource>().Play();
                    break;
                case 1:
                    GetComponent<AudioSource>().clip = musica2;
                    GetComponent<AudioSource>().Play();
                    break;
                case 2:
                    GetComponent<AudioSource>().clip = musica3;
                    GetComponent<AudioSource>().Play();
                    break;
            }
        }
        
	}
}
