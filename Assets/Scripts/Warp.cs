using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public GameObject target;

    public GameObject targetMap;

    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;
    float fadeTime = 1f;
    public string info;

    FloorGenerator scriptGenerator;

    GameObject gm;
    GameObject h6;
    GameObject knn;

    //usados para setup da dungeon
    public bool isActive;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Use this for initialization
    void Start () {
        GameObject obj = GameObject.Find("FloorGeneratorObj");
        scriptGenerator = obj.GetComponent<FloorGenerator>();

        gm = GameObject.Find("Manager");
        h6 = GameObject.Find("H6");
        knn = GameObject.Find("KnnWatcher");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        GameObject map;

        if(collision.gameObject.tag == "Player")
        {
            // se nao existir um mapa de destino coloca um mapa de fimde curso
            if(targetMap == null)
            {
                map = collision.GetComponent<Player>().currentMap;

                // corrigir aqui depois para random dentro da lista
                targetMap = scriptGenerator.noWayRoom[0];
                target = scriptGenerator.noWayRoom[0].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject;

                scriptGenerator.noWayRoom[0].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().targetMap = map;
                scriptGenerator.noWayRoom[0].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().target = this.gameObject;


                collision.GetComponent<Animator>().enabled = false;
                collision.GetComponent<Player>().enabled = false;
                FadeIn();

                yield return new WaitForSeconds(fadeTime);

                collision.transform.position = target.transform.GetChild(0).transform.position;
                h6.transform.position = collision.transform.position;
                Camera.main.GetComponent<MainCamera>().setBounds(targetMap);
                collision.GetComponent<Player>().currentMap = targetMap;

                FadeOut();
                collision.GetComponent<Animator>().enabled = true;
                collision.GetComponent<Player>().enabled = true;

            }
            else
            {
                collision.GetComponent<Animator>().enabled = false;
                collision.GetComponent<Player>().enabled = false;
                FadeIn();

                yield return new WaitForSeconds(fadeTime);

                //indica pro mapa que ele esta ativo
                if (targetMap.GetComponent<MapConfig>().active == false)
                {
                    targetMap.GetComponent<MapConfig>().active = true;
                    if(targetMap.GetComponent<MapConfig>().clear == false)
                    {
                        knn.GetComponent<knnRecord>().knnAtivar = true;
                        knn.GetComponent<knnRecord>().activeTimer = true;
                    }
                }

                collision.transform.position = target.transform.GetChild(0).transform.position;
                h6.transform.position = collision.transform.position;
                Camera.main.GetComponent<MainCamera>().setBounds(targetMap);
                collision.GetComponent<Player>().currentMap = targetMap;

                FadeOut();
                collision.GetComponent<Animator>().enabled = true;
                collision.GetComponent<Player>().enabled = true;



            }
           

        }
    }

    private void OnGUI()
    {
        if (!start)
        {
            return;
        }

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            if (alpha < 0) start = false;
        }
    }

    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }

    void FadeOut()
    {
        isFadeIn = false;
    }

    
}
