using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class MySqlDb : MonoBehaviour

{
    string urlPost = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertPlayerInfo.php";
    string urlPostKnn = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertKnnData.php";
    public int[] sumArray;


    public int cdPlayer;
    
    


    public void Start()
    {

    }


    public void Insert()
    {
        GameObject research = GameObject.Find("Canvas");
        ResearchControl researchScript = research.GetComponent<ResearchControl>();

        InsertPlayerInfo(researchScript.jogador);
   
    }


    public void InsertPlayerInfo(string[] jogador)
    {

        int playGames;

        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();
        cdPlayer = getUser.cdPlayer;



        if (jogador[5] == "SIM")
        {
            playGames = 1;
        }
        else
        {
            playGames = 0;
        }

        print("PlayerInfo cdPlayer" + cdPlayer);

        WWWForm form = new WWWForm();
        form.AddField("cdPlayerPost", cdPlayer);
        form.AddField("playerNamePost", jogador[0]);
        form.AddField("genderPost", jogador[2]);
        form.AddField("agePost", jogador[1]);
        form.AddField("educationPost", jogador[3]);
        form.AddField("cityPost", jogador[4]);
        form.AddField("playGamesPost", playGames);

        WWW www = new WWW(urlPost, form);
   
    }


    public void InsertKnnData(float[] knn, float distanceAux)
    {
        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();
        cdPlayer = getUser.cdPlayer;




        print("KnncdPlayer" + cdPlayer);

        GameObject sum = GameObject.Find("KnnWatcher");
        GetAnswerSum sumResult = sum.GetComponent<GetAnswerSum>();
        sumArray = sumResult.sumArray;

        string label = getLabel(sumArray);
        print("label " + label);


        WWWForm form = new WWWForm();
        form.AddField("cdplayerPost", cdPlayer);
        form.AddField("intOfShootsPost", (knn[0]).ToString());
        form.AddField("intOfHitsPost", (knn[1]).ToString());
        form.AddField("intOfBoxesPost", (knn[2]).ToString());
        form.AddField("hpLostPost", (knn[3]).ToString());
        form.AddField("healPost", (knn[4]).ToString());
        form.AddField("secondsPost", (knn[5]).ToString());
        form.AddField("firstSkillPost", (knn[9]).ToString());
        form.AddField("distanceOfEnemysPost", (knn[6]).ToString());
        form.AddField("collectedEnergyPost", (knn[7]).ToString());
        form.AddField("usedShootPost", (knn[8]).ToString());
        form.AddField("labelPost", label);

        WWW www = new WWW(urlPostKnn, form);

        
    }


    public string getLabel(int[] anArray)
    {
        int maxValue = anArray.Max();
        int maxIndex = anArray.ToList().IndexOf(maxValue);
        print("maxIndex " + maxIndex);

        switch (maxIndex)
        {
            case 0:
                return "Sobrevivente";
            case 1:
                return "Explorador";
            case 2:
                return "TaNaDisney";
            case 3:
                return "MexeuComaMae";
            default:
                return "";
        }

    }

}
