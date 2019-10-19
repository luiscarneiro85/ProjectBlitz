using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuInventory : MonoBehaviour {

    public GameObject ui;
    public GameObject list;
    int indice = 0;
    int skillIndice = 0;
    bool locked = false;
    public GameObject skill;
    public GameObject auxSkill;
    GameObject gm;
    GameObject player;
    GameObject knn;
    public GameObject seletor;
    public GameObject skillInfo;
    public GameObject skillName;
    public GameObject skillCost;
    public GameObject skillPower;
    public GameObject skillTime;
    public GameObject buyConfirm;
    public GameObject xp;
    public string status = "principal";
    public bool travaParaNaoBugarcompra = true;
    bool firstBuy = true;

    // Use this for initialization
    void Start () {
        knn = GameObject.Find("KnnWatcher");
        gm = GameObject.Find("Manager");
        list.SetActive(false);
        player = GameObject.Find("Blitz");
        for(int i = 0; i < skill.transform.childCount; i++)
        {
            skill.transform.GetChild(i).GetComponent<Text>().text = gm.GetComponent<GameManager>().skillList[i].GetComponent<Transform>().name;
        }

        auxSkill = skill;

    }
	
	// Update is called once per frame
	void Update () {
        xp.GetComponent<Transform>().GetChild(0).GetComponent<Text>().text = player.GetComponent<Player>().xp.ToString();

        if (Input.GetButtonUp("A") || Input.GetButtonUp("Submit"))
        {
            travaParaNaoBugarcompra = false;
        }

        if (gm.GetComponent<GameManager>().gameState.Equals("inventory") && (Input.GetButtonDown("B") || Input.GetButtonDown("Cancel")) && status.Equals("compra"))
        {
            //SceneManager.LoadScene("MenuPrincipal");
            skill = list.GetComponent<Transform>().GetChild(0).gameObject;
            buyConfirm.SetActive(false);
            indice = 0;
            status = "principal";
        }

        //entra no inventario
        if((Input.GetButtonDown("Y") || Input.GetButtonDown("Space")) && gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            gm.GetComponent<GameManager>().gameState = "inventory";
            status = "principal";
            list.SetActive(true);
            ui.SetActive(false);
            seletor.transform.position = skill.transform.GetChild(0).transform.position;
        }
        else if ((Input.GetButtonDown("Y") || Input.GetButtonDown("Space")) && gm.GetComponent<GameManager>().gameState.Equals("inventory"))
        {
            gm.GetComponent<GameManager>().gameState = "play";
            list.SetActive(false);
            ui.SetActive(true);
            indice = 0;
            skill = list.GetComponent<Transform>().GetChild(0).gameObject;
            buyConfirm.SetActive(false);
            status = "principal";
        }

        if (gm.GetComponent<GameManager>().gameState.Equals("inventory"))
        {

            //mostra informções da skill
            if (status.Equals("principal"))
            {
                skillName.GetComponent<Text>().text = gm.GetComponent<GameManager>().skillList[indice].GetComponent<Transform>().name;
                skillInfo.GetComponent<Text>().text = gm.GetComponent<GameManager>().skillList[indice].GetComponent<Skill>().info;
                skillPower.GetComponent<Text>().text = "Poder: " + gm.GetComponent<GameManager>().skillList[indice].GetComponent<Skill>().effectPower.ToString();
                skillTime.GetComponent<Text>().text = "Tempo: " + gm.GetComponent<GameManager>().skillList[indice].GetComponent<Skill>().effectTime.ToString();
                if (gm.GetComponent<GameManager>().skillList[indice].GetComponent<Skill>().purchased == true)
                {
                    skillCost.GetComponent<Text>().text = "Comprado";
                }
                else
                {
                    skillCost.GetComponent<Text>().text = gm.GetComponent<GameManager>().skillList[indice].GetComponent<Skill>().cost.ToString();

                }
            }

            //habilita o botão equipar
            if(status.Equals("compra") && gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased)
            {
                buyConfirm.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                buyConfirm.GetComponent<Transform>().GetChild(0).gameObject.SetActive(false);
            }

            //faz a compra
            if((Input.GetButtonDown("A") || Input.GetButtonDown("Submit")) && indice == 1 && travaParaNaoBugarcompra == false)
            {
                if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == false &&
                    player.GetComponent<Player>().xp >= gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().cost)
                {
                    gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased = true;
                    player.GetComponent<Player>().xp -= gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().cost;
                    skillCost.GetComponent<Text>().text = "Comprado";
                }
            }


            //equipa skill
            if ((Input.GetButtonDown("A") || Input.GetButtonDown("Submit")) && indice == 0)
            {
                if (firstBuy)
                {
                    if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == true)
                    {
                        player.GetComponent<Player>().skillEquiped = gm.GetComponent<GameManager>().skillList[skillIndice].gameObject;
                        firstBuy = false;
                        //muda cor da skill equipada
                        for (int i = 0; i < auxSkill.transform.childCount; i++)
                        {
                            auxSkill.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                        }
                        auxSkill.transform.GetChild(skillIndice).GetComponent<Text>().color = Color.yellow;
                        player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready = true;
                        knn.GetComponent<knnRecord>().firstSkill = player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().id;
                    }
                }
                else
                {
                    if(player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().active == false)
                    {
                        if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == true && player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready)
                        {
                            player.GetComponent<Player>().skillEquiped = gm.GetComponent<GameManager>().skillList[skillIndice].gameObject;
                            //muda cor da skill equipada
                            for (int i = 0; i < auxSkill.transform.childCount; i++)
                            {
                                auxSkill.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                            }
                            auxSkill.transform.GetChild(skillIndice).GetComponent<Text>().color = Color.yellow;
                            player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready = true;
                        }
                    }
                    
                }

            }


            if (status.Equals("compra"))
            {
                if(gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == true)
                {
                    if (Input.GetAxis("Horizontal") < 0 && !locked)
                    {
                        locked = true;
                        indice++;
                        if (indice > buyConfirm.transform.childCount - 1) indice = 0;
                    }
                    else if (Input.GetAxis("Horizontal") > 0 && !locked)
                    {
                        locked = true;
                        indice--;
                        if (indice < 0) indice = buyConfirm.transform.childCount - 1;
                    }
                    if (Input.GetAxis("Horizontal") == 0)
                    {
                        locked = false;
                    }

                }
                else
                {
                    indice = 1;
                }

            }


            if (status.Equals("principal"))
            {
                if (Input.GetAxis("Vertical") < 0 && !locked)
                {
                    locked = true;
                    indice++;
                    if (indice > skill.transform.childCount - 1) indice = 0;
                }
                else if (Input.GetAxis("Vertical") > 0 && !locked)
                {
                    locked = true;
                    indice--;
                    if (indice < 0) indice = skill.transform.childCount - 1;
                }               
                if (Input.GetAxis("Vertical") == 0)
                {
                    locked = false;
                }
            }

            seletor.transform.position = skill.transform.GetChild(indice).transform.position;

            //entra no modo compra
            if ((Input.GetButtonDown("A") || Input.GetButtonDown("Submit")) && status.Equals("principal"))
            {
                status = "compra";
                buyConfirm.SetActive(true);
                skillIndice = indice;
                indice = 0;
                skill = buyConfirm;
                travaParaNaoBugarcompra = true;
            }
        }
            
    }

    public void ShowSkillInfo(int id)
    {
        indice = id;
        skillIndice = id;
        buyConfirm.SetActive(true);
    }

    public void BuySkill()
    {
        if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == false &&
    player.GetComponent<Player>().xp >= gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().cost)
        {
            gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased = true;
            player.GetComponent<Player>().xp -= gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().cost;
            skillCost.GetComponent<Text>().text = "Comprado";
            status = "compra";
        }

    }

    public void EquipSkill()
    {
        if (firstBuy)
        {
            if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == true)
            {
                player.GetComponent<Player>().skillEquiped = gm.GetComponent<GameManager>().skillList[skillIndice].gameObject;
                firstBuy = false;
                //muda cor da skill equipada
                for (int i = 0; i < auxSkill.transform.childCount; i++)
                {
                    auxSkill.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                }
                auxSkill.transform.GetChild(skillIndice).GetComponent<Text>().color = Color.yellow;
                player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready = true;
                knn.GetComponent<knnRecord>().firstSkill = player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().id;
            }
        }
        else
        {
            if (player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().active == false)
            {
                if (gm.GetComponent<GameManager>().skillList[skillIndice].GetComponent<Skill>().purchased == true && player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready)
                {
                    player.GetComponent<Player>().skillEquiped = gm.GetComponent<GameManager>().skillList[skillIndice].gameObject;
                    //muda cor da skill equipada
                    for (int i = 0; i < auxSkill.transform.childCount; i++)
                    {
                        auxSkill.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                    }
                    auxSkill.transform.GetChild(skillIndice).GetComponent<Text>().color = Color.yellow;
                    player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().ready = true;
                }
            }

        }
    }

    void selection()
    {
        for (int i = 0; i < list.transform.childCount; i++)
        {
            list.transform.GetChild(i).GetComponent<Animator>().SetBool("hover", false);
        }
        list.transform.GetChild(indice).GetComponent<Animator>().SetBool("hover", true);
    }
}
