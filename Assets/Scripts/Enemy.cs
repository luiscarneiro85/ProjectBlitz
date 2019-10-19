using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy: MonoBehaviour
{

    Vector3 target;
    Vector3 initialPosition;
    Vector3 direction;

    RaycastHit2D hit;
    RaycastHit2D ray;

    GameObject player;

    public float speed;
    float step = 3;
    float speedRef;
    float hp = 0;
    public float hpMax;
    public float attackDamage;
    public float visionRadius;
    public float AttackRadius;

    float distance;

    Animator anim;

    public CircleCollider2D attackCollider;
    public bool horizontal;
    public bool ranged;
    public bool berserk;
    bool dead = false;

    public float attackDelay;
    bool isAttack = false;
    public bool isActive = false;
    public bool ready = false;
    public bool stun = false;

    public List<AudioClip> hitSound = new List<AudioClip>();

    //quantidade de energia que dropa
    public int xp;
    public GameObject energy;

    public GameObject lifeBar;

    GameObject gm;
    GameObject knn;

    public GameObject miniSlime;
    List<float> deltaDistance = new List<float>();
    bool deltaDistanceLock = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        attackCollider.enabled = false;
        hp = hpMax;
        speedRef = speed;
        gm = GameObject.Find("Manager");
        knn = GameObject.Find("KnnWatcher");


    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (isActive && ready)
            {
                //chama contagem da distance entre inimigo e jogador
                if (knn.GetComponent<knnRecord>().knnAtivar)
                {
                    if (!deltaDistanceLock)
                    {
                        StartCoroutine(DistanceCount());
                    }

                }


                lifeBar.GetComponent<Image>().fillAmount = hp / hpMax;

                if (hp > hpMax) hp = hpMax;
                if (hp <= 0 && !dead)
                {
                    dead = true;
                    die();
                }


                //define target com a posicao inicial 
                target = initialPosition;

                
                //raycast em direcao ao jogador, para verificar se ele se encontra no raio de visao do inimigo
                hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRadius, 1 << LayerMask.NameToLayer("Default"));

                //forward = transform.TransformDirection(player.transform.position - transform.position);
                Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.red);

                //verifica se ocorreu colisao do raycast, se o collider tiver tag player muda o target para o player
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (distance < visionRadius)
                        {
                            target = player.transform.position;
                        }
                    }
                }

                
                //calcula a distancia entre os objetos para determinar o comportamento
                distance = Vector3.Distance(target, transform.position);

                //calcula a direcao para tratar animacao 
                direction = (target - transform.position).normalized;

                
                // se a distancia for menor que a de ataque , ataca o target
                if (target != initialPosition && distance < AttackRadius)
                {
                    //if (!isAttack && !anim.GetCurrentAnimatorStateInfo(0).IsTag("dead"))
                    if (!isAttack && hp > 0)
                    {
                        if (!stun)
                        {
                            attack();
                        }
                        
                    }

                }
                else
                {
                    if (!isAttack)
                    {
                        if (!stun)
                        {
                            walk();
                        }
                        
                    }

                }
            }
        }
            

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    void attack()
    {
        //flip o sprite para esquerda para ajusta a animaçao
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            attackCollider.offset = new Vector2((direction.x * -1), direction.y);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            attackCollider.offset = new Vector2(direction.x, direction.y);
        }

        //posiciona o collider de ataque

        anim.SetFloat("movX", direction.x);
        anim.SetFloat("movY", direction.y);
        anim.SetTrigger("attack");
        StartCoroutine(delay());

    }

    void walk()
    { 
        //flip o sprite para esquerda para ajusta a animaçao
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }


        //caso contrario movimenta em direcao ao alvo
        transform.Translate((direction) * speed * Time.deltaTime);

        // trata a animacao doinimigo
        anim.SetFloat("movX", direction.x);
        anim.SetFloat("movY", direction.y);
        anim.SetBool("walking", true);

        if (target == initialPosition && distance < 0.02f)
        {
            transform.position = initialPosition;
            anim.SetBool("walking", false);
        }
    }

    public void takeDamage(float damage)
    {
        hp -= damage;
        int aux = Random.Range(0, 2);
        GetComponent<AudioSource>().PlayOneShot(hitSound[aux], musicControl.soundVolume);


            
    }

    void die()
    {
        //retira a animação de morte do raycast
        gameObject.layer = 2;
        anim.SetTrigger("die");
        if (knn.GetComponent<knnRecord>().knnAtivar)
        {
            float sum = 0;
            if(deltaDistance.Count == 0)
            {
                sum = .5f;
            }
            else
            {
                foreach (float d in deltaDistance)
                {
                    sum += d;
                }
                sum = sum / deltaDistance.Count;
            }
            knn.GetComponent<knnRecord>().distanceOfEnemys.Add(sum);
        }

    }

    public void activeColliderAttack()
    {
        attackCollider.enabled = true;

    }
    public void desactiveColliderAttack()
    {
        attackCollider.enabled = false;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("hit"))
            {

            }
            else
            {
                collision.SendMessage("takeDamage", attackDamage);
                collision.GetComponent<Animator>().SetTrigger("hit");
            }

        }
        else if (collision.tag == "Attack")
        {
            if (knn.GetComponent<knnRecord>().knnAtivar)
            {
                knn.GetComponent<knnRecord>().numberOfHits++;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("dead"))
            {

            }
            else
            {
                //flip o sprite para esquerda para ajusta a animaçao
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }

                if (berserk)
                {

                }
                else
                {
                    speed = 0;
                }
                anim.SetTrigger("hit");
                collision.GetComponent<Shoot>().direction = new Vector3(0, 0, 0);
                takeDamage(collision.GetComponent<Shoot>().damage);
                collision.GetComponent<Animator>().SetTrigger("collision");
                if (player.GetComponent<Player>().skillEquiped != null)
                {
                    if (player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().effect.Equals("Drenar") && player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().active)
                    {
                        //player.gameObject.SendMessage("gainHP", collision.GetComponent<Shoot>().damage);
                        player.GetComponent<Player>().gainHp(collision.GetComponent<Shoot>().damage);
                    }
                }

            }

        }
        else if (collision.tag == "Trap")
        {
            collision.GetComponent<FireTrap>().Kabum();
        }
        else if (collision.tag == "Fire")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("dead"))
            {

            }
            else
            {
                //flip o sprite para esquerda para ajusta a animaçao
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }

                if (berserk)
                {

                }
                else
                {
                    speed = 0;
                }
                anim.SetTrigger("hit");
                takeDamage(player.GetComponent<Player>().skillEquiped.GetComponent<Skill>().effectPower);
            }
        }
    }

    void resetSpeed()
    {
        speed = speedRef;
    }


    IEnumerator delay()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelay);
        if(isAttack) isAttack = false;

    }

    private void OnBecameVisible()
    {
        isActive = true;
        StartCoroutine(isReady());
    }

    private void OnBecameInvisible()
    {
        //isActive = false;
    }

    void destroyEnemy()
    {
        player.GetComponent<Player>().currentMap.GetComponent<MapConfig>().defeatEnemy();
        GameManager.enemysTotal--;
        for (int i = 0; i < xp; i++)
        {
            Instantiate(energy, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    void destroyMiniSlime()
    {
        for (int i = 0; i < xp; i++)
        {
            Instantiate(energy, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }


    public IEnumerator isReady()
    {
        yield return new WaitForSeconds(.5f);
        ready = true;
    }

    public IEnumerator createMiniSlime()
    {
        player.GetComponent<Player>().currentMap.GetComponent<MapConfig>().defeatEnemy();
        GameManager.enemysTotal--;
        Instantiate(miniSlime, transform.position, transform.rotation);
        yield return new WaitForSeconds(1.2f);
        Instantiate(miniSlime, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    //acrescenta a distancia do inimigo na lista a cada segundo
    public IEnumerator DistanceCount()
    {
        deltaDistanceLock = true;
        yield return new WaitForSeconds(1);
        deltaDistance.Add(distance);
        deltaDistanceLock = false;

    }
}
