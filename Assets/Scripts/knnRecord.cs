using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knnRecord : MonoBehaviour {

    public int numberOfShoots; //player
    public int numberOfHits; //enemys
    public int numberOfBoxes;   //destructibles
    public int hpLost;      //player
    public int heal;        //player
    public int seconds;     //mapConfig
    public int distanceOfEnemys;
    public int distanceInRoom;
    

    public bool knnAtivar = false;
    float timer = 0;
    public bool activeTimer = false;

    //int[ , ] knn = new int[10,5];
    int[] knn = new int[10];
    //int[] matriz = new int[10];
   // int indice = 0;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //salva os valores quando a porta da sala abre
        if (knnAtivar)
        {
            knn[0] = numberOfShoots;
            knn[1] = numberOfHits;
            knn[2] = numberOfBoxes;
            knn[3] = hpLost;
            knn[4] = heal;
            knn[5] = seconds;

            if (activeTimer)
            {
                //StartCoroutine(timerCount());
                timer += Time.deltaTime;
                seconds = Mathf.RoundToInt(timer);
                print(timer.ToString());
            }

            /*
            knn[indice,0] = numberOfShoots;
            knn[indice,1] = numberOfHits;
            knn[indice,2] = numberOfBoxes;
            knn[indice,3] = hpLost;
            knn[indice,4] = heal;
            */
        }
        else
        {
            knn[0] = 0;
            knn[1] = 0;
            knn[2] = 0;
            knn[3] = 0;
            knn[4] = 0;
            knn[5] = 0;
        }
	}

    IEnumerator timerCount()
    {
        activeTimer = false;
        while(knnAtivar)
        {
            timer += Time.deltaTime;
            seconds = Mathf.RoundToInt(timer);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
