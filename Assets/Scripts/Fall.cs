using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{

    float posY;
    public float forceY;
    bool inJump = false;
    bool isReady = false;
    Vector2 velocity;
    Vector2 target;
    GameObject go;

    // Use this for initialization
    void Start()
    {
        go = GameObject.Find("Chest");
        target = go.transform.position;
        starCondition();
    }

    // Update is called once per frame
    void Update()
    {
        posY = GetComponent<Transform>().position.y;

        //velocity = GetComponent<Rigidbody2D>().GetPointVelocity(new Vector2(0, 0));
        velocity = GetComponent<Rigidbody2D>().GetPointVelocity(target);

        if (posY <= target.y && !inJump)
        {
            inJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
            forceY -= 50;
            isReady = true;
        }
        else if (velocity.y < 0 && inJump)
        {
            inJump = false;

        }

        if (forceY <= 100)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            //transform.position = new Vector2(0, 0);
            transform.position = target;
        }
    }

    void starCondition()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (isReady) Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "BulletWinchester")
        {
            if (isReady) Destroy(this.gameObject);
        }
    }


}
