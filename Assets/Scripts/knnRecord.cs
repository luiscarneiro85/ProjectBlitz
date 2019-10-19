using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;



public class knnRecord : MonoBehaviour {

    public int numberOfShoots; //player
    public int numberOfHits; //enemys
    public int numberOfBoxes;   //destructibles
    public int hpLost;      //player
    public int heal;        //player
    public int seconds;     //mapConfig
    public int distance; // enemys
    public int firstSkill;
    public int mostUsedSkill;
    public List<float> distanceOfEnemys = new List<float>();
    public int distanceInRoom;
    public float distanceAux;

    public bool knnAtivar = false;
    float timer = 0;
    public bool activeTimer = false;
    public bool blockKnn = false;

    float[] knn = new float[10];

    GameObject obj;

    //FileMaker file = new FileMaker();
    MySqlDb insert = new MySqlDb();

    public int totalEnergy = 0;
    public int collectedEnergy = 0;


    //string conn = "URI=file:" + Application.dataPath + "/blitzDB.s3db"; //Path to database.



    // Use this for initialization
    void Start () {

        obj = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        knn[2] = numberOfBoxes;
        knn[9] = firstSkill;
        

        //salva os valores quando a porta da sala abre
        if (knnAtivar) //ligado no warp
        {
            knn[0] = numberOfShoots;
            knn[1] = numberOfHits;
            
            knn[3] = hpLost;
            knn[4] = heal;
            knn[5] = seconds;




            if (activeTimer && obj.GetComponent<Player>().currentMap.GetComponent<MapConfig>().clear == false)
            {
                timer += Time.deltaTime;
                seconds = Mathf.RoundToInt(timer);
                knn[8] = obj.GetComponent<Player>().shootID;
            }
        }
        else
        {
            if (obj.GetComponent<Player>().recordKnn && !blockKnn)
            {
                //dataBase.InsertDB("classe", numberOfShoots, numberOfHits, numberOfBoxes, hpLost, heal, seconds);

                foreach (float d in distanceOfEnemys)
                {
                    distanceAux += d;
                }
                float aux;
                aux = (distanceAux / distanceOfEnemys.Count);              
                knn[6] = Mathf.RoundToInt(aux);

                //calcula porcentagem de energia coletada
                aux = (collectedEnergy * 100f) / totalEnergy;
                if (aux > 100) aux = 100;
                knn[7] = aux;

                //int skillAux = obj.GetComponent<Player>().arrayMostUsedSkill[0];
                //for (int i = 0 ; i< obj.GetComponent<Player>().arrayMostUsedSkill.Length ; i++)


                //file.WriteFile(knn);
                
                //gravaKnnData(knn);

                //reseta os valores para nova captura
                numberOfShoots = 0;
                numberOfHits = 0;
                numberOfBoxes = 0;
                hpLost = 0;
                heal = 0;
                seconds = 0;
                timer = 0;
                distance = 0;
                distanceOfEnemys.Clear();
                distanceAux = 0;
                collectedEnergy = 0;
                totalEnergy = 0;

                insert.InsertKnnData(knn, distanceAux);

                obj.GetComponent<Player>().recordKnn = false;
                blockKnn = true;
            }

        }





    }

}
