using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnswerSum : MonoBehaviour {

    string urlGetAwnserSum = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/GetAnswesSum.php?cd_player=";
    public int cdPlayer;
    public string[] sumArrayString;
    public int[] sumArray;

    // Use this for initialization
    IEnumerator Start () {

        print("getPlayerLabel");
        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();
        cdPlayer = getUser.cdPlayer;

        WWW sum = new WWW(urlGetAwnserSum + cdPlayer);
        yield return sum;
        string sumData = sum.text;
        print(sumData);
        sumArrayString = sumData.Split(',');
        sumArray = Array.ConvertAll<string, int>(sumArrayString, int.Parse);


    }




    // Update is called once per frame
    void Update () {
		
	}
}
