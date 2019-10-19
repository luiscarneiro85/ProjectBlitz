using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CdPlayer : MonoBehaviour {

    public int cdPlayer;
    // Use this for initialization
    void Start () {

        System.Random r = new System.Random();
        cdPlayer = r.Next();

        print(cdPlayer);
   
    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}




}
