using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    Animator anim;
    Vector2 movement;

    public GameObject energyCounter;

    public GameObject lifeBar;
    public GameObject initialMap;
    public GameObject currentMap;

    public GameObject aim;
    public GameObject weapon;
    Vector2 aimDirection;

    public GameObject shoot;

    //atributos
    float hp = 0;
    float hpMax = 10;
    public float speed;
    float shield = 0;
    float shieldMax = 0;

    public AudioClip hitSound;
    public AudioClip fireSound;

    public bool recordKnn = false;
    public bool prize = true;

    public int shootID = 0;
    public int auxShootID = 0;

    //controle
    bool isAttacking = false;

    public int xp = 0;

    GameObject gm;
    GameObject knn;

    public GameObject skillEquiped;
    public GameObject skillIcon;
    public GameObject iconShoot;
    public GameObject skillActiveInfo;

    float skillTimer = 0;
    float auxTimer = 0;

    public GameObject clone;

    public int[] arrayMostUsedSkill = new int[7];
    int mostUsedSkill;


    private void Awake()
    {
        //Assert.IsNotNull(initialMap);
        aim.GetComponent<SpriteRenderer>().enabled = false;

        knn = GameObject.Find("KnnWatcher");
    }

    // Use this for initialization
    void Start () {

        for(int i=0;i <arrayMostUsedSkill.Length; i++)
        {
            arrayMostUsedSkill[i] = 0;
        }

        if (initialMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().targetMap == null )
        {
            SceneManager.LoadScene("Floor_1");
        }

        anim = GetComponent<Animator>();

        //passa o mapa inicial para a referencia da camera
        Camera.main.GetComponent<MainCamera>().setBounds(initialMap);

        hp = hpMax;

        gm = GameObject.Find("Manager");

        shoot = gm.GetComponent<GameManager>().shootList[shootID];

        //dataBase.Create();

        skillActiveInfo.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        //atualiza o icone do tiro selecionado
        iconShoot.GetComponent<Image>().sprite = shoot.GetComponent<Shoot>().iconUI;

        if(skillEquiped != null)
        {
            skillIcon.GetComponent<Image>().sprite = skillEquiped.GetComponent<Skill>().icon;
            skillIcon.GetComponent<Image>().fillAmount = 1;
        }

        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            //pause 
            if ((Input.GetButtonDown("START") || Input.GetButtonDown("Submit")) && Time.timeScale == 0)
            {
                Time.timeScale = 1;              
            }
            else if ((Input.GetButtonDown("START") || Input.GetButtonDown("Submit")) && Time.timeScale == 1)
            {
                Time.timeScale = 0;               
            }

            // pinta barra de hp
            lifeBar.GetComponent<Image>().fillAmount = hp / hpMax;

            //atualiza quantidade de energia
            energyCounter.GetComponent<Text>().text = xp.ToString();


            if (hp > hpMax) hp = hpMax;
            if (hp <= 0)
            {
                hp = 0;
                knn.GetComponent<knnRecord>().knnAtivar = false;
                recordKnn = true;
                gm.SendMessage("showGameOver");
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                aimDirection = dir - (Vector2)transform.position;
            }
            else
            {
                aimDirection = new Vector3(Input.GetAxisRaw("HorizontalDireito"), Input.GetAxisRaw("VerticalDireito"));  
            }

            //print(aimDirection.ToString());
            aim.transform.localPosition = aimDirection;

            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            walk(movement);

            if (Input.GetAxisRaw("HorizontalDireito") != 0 || Input.GetAxisRaw("VerticalDireito") != 0 || Input.GetMouseButton(0))
            {
                if (!isAttacking) fire();
            }

            //utiliza skill
            if(Input.GetAxisRaw("RT") != 0 || Input.GetMouseButtonDown(1))
            {
                UseSkill();
            }

            //verifica status da skill para iniciar cooldown
            if(skillEquiped != null)
            {
                if (skillEquiped.GetComponent<Skill>().ready == false)
                {
                    skillTimer += Time.deltaTime;
                    auxTimer = skillTimer / skillEquiped.GetComponent<Skill>().rechargeTime;
                    skillIcon.GetComponent<Image>().fillAmount = auxTimer;
                    if (skillTimer >= skillEquiped.GetComponent<Skill>().rechargeTime)
                    {
                        skillTimer = 0;
                        skillEquiped.GetComponent<Skill>().ready = true;
                    }
                }
                else
                {
                    skillIcon.GetComponent<Image>().fillAmount = 1;
                }
            }

        }

        if (currentMap.GetComponent<MapConfig>().clear && initialMap != currentMap)
        {
            //knn.GetComponent<knnRecord>().knnAtivar = false;
            //recordKnn = true;

            if (prize && currentMap.GetComponent<MapConfig>().onTime)
            {
                int aux = Random.Range(0, (gm.GetComponent<GameManager>().icons.Count));
                Instantiate(gm.GetComponent<GameManager>().icons[aux], currentMap.GetComponent<MapConfig>().enemySpot[aux].transform.position, currentMap.GetComponent<MapConfig>().enemySpot[aux].transform.rotation);
                prize  = false;
                currentMap.GetComponent<MapConfig>().onTime = false;
            }


        }


    }

    //desliga a gracação dos parametros do knn
    public void StopRecordKnn()
    {

    }

    public void UseSkill()
    {
        //se a skill estiver equipada e pronta
        if(skillEquiped != null)
        {
            if (skillEquiped.GetComponent<Skill>().ready)
            {
                switch (skillEquiped.GetComponent<Skill>().effect)
                {
                    case "Restaurar":
                        hp += (skillEquiped.GetComponent<Skill>().effectPower);
                        skillEquiped.GetComponent<Skill>().ready = false;
                        Instantiate(skillEquiped.GetComponent<Skill>().visualEffect, transform.position, transform.rotation);
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                        break;

                    case "Drenar":
                        skillEquiped.GetComponent<Skill>().ready = false;
                        skillEquiped.GetComponent<Skill>().active = true;
                        StartCoroutine(skillTime());
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                        skillActiveInfo.SetActive(true);
                        break;

                    case "Clone":
                        Instantiate(clone, transform.position, transform.rotation);
                        this.transform.tag = "Clone";
                        skillEquiped.GetComponent<Skill>().ready = false;
                        StartCoroutine(skillTime());
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                        skillActiveInfo.SetActive(true);
                        break;

                    case "Escudo":
                        skillEquiped.GetComponent<Skill>().ready = false;
                        skillEquiped.GetComponent<Skill>().active = true;
                        Instantiate(skillEquiped.GetComponent<Skill>().visualEffect, transform.position, transform.rotation);
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                        break;

                    case "ArmadilhaDeFogo":
                        skillEquiped.GetComponent<Skill>().ready = false;
                        skillEquiped.GetComponent<Skill>().active = true;
                        StartCoroutine(skillTime());
                        Instantiate(skillEquiped.GetComponent<Skill>().fire, transform.position, transform.rotation);
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        break;

                    case "OndaDeChoque":
                        skillEquiped.GetComponent<Skill>().ready = false;
                        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Destructibles"))
                        {
                            if (obj.GetComponent<Destructibles>().active)
                            {
                                obj.GetComponent<Destructibles>().LoseHp();
                            }
                        }

                        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            if(obj.GetComponent<Enemy>().isActive)
                            {
                                obj.GetComponent<Enemy>().stun = true;
                            }

                        }
                        StartCoroutine(RemoveStun());
                        arrayMostUsedSkill[skillEquiped.GetComponent<Skill>().id]++;
                        GetComponent<AudioSource>().PlayOneShot(skillEquiped.GetComponent<Skill>().soundEffect, musicControl.soundVolume);
                        break;
                }      
            }
        }
    }

    void walk(Vector2 movement)
    {

        transform.Translate(movement * speed * Time.deltaTime);

        if (Input.GetAxisRaw("HorizontalDireito") != 0 || Input.GetAxisRaw("VerticalDireito") != 0)
        {
            anim.SetFloat("movX", aimDirection.x);
            anim.SetFloat("movY", aimDirection.y);
            anim.SetBool("walking", true);
        }
        else if (movement != Vector2.zero)
        {
            anim.SetFloat("movX", movement.x);
            anim.SetFloat("movY", movement.y);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    void fire()
    {
        StartCoroutine(attackDelay());

        if (shoot.GetComponent<Shoot>().triple)
        {
            //zera o valor guia dos tiros auxiliares
            auxShootID = 0;
            Instantiate(shoot, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(shoot, transform.position, transform.rotation);
        }

        GetComponent<AudioSource>().PlayOneShot(fireSound, musicControl.soundVolume);

        if (knn.GetComponent<knnRecord>().knnAtivar)
        {
            knn.GetComponent<knnRecord>().numberOfShoots++;

        }
            
    }

    //contagem do tempo dos efeitos das skills
    IEnumerator skillTime()
    {
        skillActiveInfo.GetComponent<Image>().sprite = skillEquiped.GetComponent<Skill>().icon;
        yield return new WaitForSeconds(skillEquiped.GetComponent<Skill>().effectTime);
        if (skillEquiped.GetComponent<Skill>().effect.Equals("Clone"))
        {
            this.transform.tag = "Player";
            Destroy(GameObject.Find("Clone(Clone)").gameObject);
        }
        skillEquiped.GetComponent<Skill>().active = false;
        skillActiveInfo.SetActive(false);

    }

    IEnumerator SkillCoolDown()
    {
        yield return new WaitForSeconds(skillEquiped.GetComponent<Skill>().rechargeTime);
        skillEquiped.GetComponent<Skill>().ready = true;
    }

    IEnumerator attackDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(shoot.GetComponent<Shoot>().delay);
        isAttacking = false;
    }

    public void takeDamage(float damage)
    {
        if(skillEquiped != null)
        {
            if (skillEquiped.GetComponent<Skill>().effect.Equals("Escudo") && skillEquiped.GetComponent<Skill>().active)
            {
                skillEquiped.GetComponent<Skill>().hp -= damage;
                if (skillEquiped.GetComponent<Skill>().hp <= 0)
                {
                    skillEquiped.GetComponent<Skill>().active = false;
                    skillEquiped.GetComponent<Skill>().hp = skillEquiped.GetComponent<Skill>().effectPower;
                    GameObject shield;
                    shield = GameObject.FindGameObjectWithTag("Shield");
                    shield.GetComponent<Animator>().SetTrigger("broken");
                }
                return;
            }
        }

        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitSound, musicControl.soundVolume);
        if (knn.GetComponent<knnRecord>().knnAtivar)
        {
            knn.GetComponent<knnRecord>().hpLost += Mathf.RoundToInt(damage);
        }
    }

    public void gainHp(float heal)
    {
        hp += heal;
        if (knn.GetComponent<knnRecord>().knnAtivar && hp != hpMax)
        {
            knn.GetComponent<knnRecord>().heal++;
        }

    }

    public void getEnergy(int energy)
    {
        xp += energy;
    }

    IEnumerator RemoveStun()
    {
        yield return new WaitForSeconds(skillEquiped.GetComponent<Skill>().effectTime);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (obj.GetComponent<Enemy>().isActive)
            {
                obj.GetComponent<Enemy>().stun = false;
            }

        }
    }
}
