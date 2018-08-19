using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> listEnemysLv1 = new List<GameObject>();
    public GameObject gameOver;
    public GameObject win;
    public string gameState = "play";

    FloorGenerator script;

    public static int enemysTotal = 0;

    public GameObject energy;
    public GameObject heal;

    public AudioClip energySound;
    public AudioClip breakSound;
    public AudioClip boxBreakSound;
    public AudioClip healSound;

    // variaveis para controle de salas

    // Use this for initialization
    void Start () {
        
        gameOver.SetActive(false);
        win.SetActive(false);
        GameObject obj = GameObject.Find("FloorGeneratorObj");
        script = obj.GetComponent<FloorGenerator>();
        
        foreach(GameObject floor in script.floor)
        {
            enemysTotal += floor.GetComponent<MapConfig>().enemysLeft;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if(enemysTotal <= 0)
        {
            gameState = "win";
        }

        if(Input.GetButtonDown("A") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("Floor_1");
        }

        if (Input.GetButtonDown("B") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

        if (gameState.Equals("win"))
        {
            win.SetActive(true);
        }

        //print(enemysTotal.ToString());

    }

    void PlayerEnergySound()
    {
        GetComponent<AudioSource>().PlayOneShot(energySound, musicControl.soundVolume);
    }

    

    void showGameOver()
    {
        if(menuPrincipal.initialLanguage == 0)
        {
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "REINICIAR O JOGO";
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "REINICIAR O JOGO";

            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "VOLTAR PARA O MENU";
            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "VOLTAR PARA O MENU";
        }
        if(menuPrincipal.initialLanguage == 1)
        {
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "RESTART THE GAME";
            gameOver.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "RESTART THE GAME";

            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = "BACK TO MENU";
            gameOver.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "BACK TO MENU";
        }
        gameState = "gameover";
        gameOver.SetActive(true);
    }

    void BoxBreakSound()
    {
        GetComponent<AudioSource>().PlayOneShot(boxBreakSound, musicControl.soundVolume);
    }

    void JarBreakSound()
    {
        GetComponent<AudioSource>().PlayOneShot(breakSound, musicControl.soundVolume);
    }

    void PlayHealSound()
    {
        GetComponent<AudioSource>().PlayOneShot(healSound, musicControl.soundVolume);
    }



}
