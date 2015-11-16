﻿//Destroy bullet and enemy on collision, and spawn a particle on enemy location
using UnityEngine;
using System.Collections;

public class Squid : MonoBehaviour
{
    public GameObject DeathInkParticle;
    public int HP = 1;
    public int scoreForDefeating = 1;

    private float screenLeftBounds = -3.124f;
    private float screenRightBounds = 3.134f;

    private bool isDead = false;
    private float maxHP;

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        float leftBoundsX = screenLeftBounds + (GetComponent<CircleCollider2D>().radius * transform.localScale.x);
        float rightBoundsX = screenRightBounds - (GetComponent<CircleCollider2D>().radius * transform.localScale.x);

        if (transform.position.x < leftBoundsX)
        {
            transform.position = new Vector3(leftBoundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightBoundsX)
        {
            transform.position = new Vector3(rightBoundsX, transform.position.y, transform.position.z);
        }
    }

    private void Flee()
    {
        //Update Score
        GameController.instance.AddScore(scoreForDefeating, transform.position);
        //Spawn a particle on dead enemy
        GameObject.Instantiate(DeathInkParticle, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(20f, 40f)) * 30);
        isDead = true;
        //Destroy bullet and enemy
        DestroyObject(gameObject, 2.0f);
    }

    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if (!isDead && WhoCollidedWithMe.tag == "Bullet")
        {
            Destroy(WhoCollidedWithMe.gameObject);
            HP--;
            float hpRatio = HP / maxHP;
            if (HP <= 0)
            {
                Flee();
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, hpRatio, hpRatio, 1.0f);
            }
        }
    }
}