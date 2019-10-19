using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEmail : MonoBehaviour {

    string cdPlayerByEmail;
    public int cdPlayerEmail;
    string email;
    string urlGetEmail = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/GetEmail.php?player_email=";

    // Use this for initialization
    public void MyCoroutine () {
        StartCoroutine(Coroutine());
    }

    // Use this for initialization
    public IEnumerator Coroutine()
    {

        GameObject user = GameObject.Find("Canvas");
        ResearchControl getEmail = user.GetComponent<ResearchControl>();
        email = getEmail.email;

        WWW cdPlayer = new WWW(urlGetEmail + email);
        yield return cdPlayer;
        cdPlayerByEmail = cdPlayer.text.ToString();
        print(cdPlayerByEmail);
        cdPlayerEmail = int.Parse(cdPlayerByEmail);
        print(cdPlayerEmail);
    }




    // Update is called once per frame
    void Update () {
		
	}
}
