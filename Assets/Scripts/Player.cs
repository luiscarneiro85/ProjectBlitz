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

    

    //controle
    bool isAttacking = false;

    int xp = 0;

    GameObject gm;
    GameObject knn;

    private void Awake()
    {
        //Assert.IsNotNull(initialMap);
        aim.GetComponent<SpriteRenderer>().enabled = false;

        knn = GameObject.Find("KnnWatcher");
    }

    // Use this for initialization
    void Start () {

       

        if(initialMap.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Warp>().targetMap == null )
        {
            SceneManager.LoadScene("Floor_1");
        }



        anim = GetComponent<Animator>();

        //passa o mapa inicial para a referencia da camera
        Camera.main.GetComponent<MainCamera>().setBounds(initialMap);

        hp = hpMax;

        gm = GameObject.Find("Manager");
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            //pause 
            if (Input.GetButtonDown("START") && Time.timeScale == 0)
            {
                Time.timeScale = 1;              
            }
            else if (Input.GetButtonDown("START") && Time.timeScale == 1)
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
                gm.SendMessage("showGameOver");
            }


            aimDirection = new Vector3(Input.GetAxisRaw("HorizontalDireito"), Input.GetAxisRaw("VerticalDireito"));
            aim.transform.localPosition = aimDirection;

            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            walk(movement);

            if (Input.GetAxisRaw("HorizontalDireito") != 0 || Input.GetAxisRaw("VerticalDireito") != 0)
            {
                if (!isAttacking) fire();
            }
        }

        if (currentMap.GetComponent<MapConfig>().clear && initialMap != currentMap)
        {
            knn.GetComponent<knnRecord>().knnAtivar = false;
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
        Instantiate(shoot, transform.position, transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(fireSound, musicControl.soundVolume);
        if (knn.GetComponent<knnRecord>().knnAtivar)
        {
            knn.GetComponent<knnRecord>().numberOfShoots++;

        }
            
    }

    IEnumerator attackDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(shoot.GetComponent<Shoot>().delay);
        isAttacking = false;
    }

    public void takeDamage(float damage)
    {
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
}
