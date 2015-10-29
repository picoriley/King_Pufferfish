﻿using UnityEngine;
using System.Collections;

public class Octopus : MonoBehaviour {

    public GameObject DeathInkParticle;
    public AudioClip inkSplatSound;

	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos (Time.time), GetComponent<Rigidbody2D>().velocity.y);
	}

	void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
	{
		if (WhoCollidedWithMe.tag == "Bullet")
		{
            //Update Score
            GameController.instance.AddScore(1);
            //Spawn a particle on dead enemy
            GameObject inkParticle = (GameObject)GameObject.Instantiate(DeathInkParticle, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            GameController.instance.PlaySound(inkSplatSound);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(20f, 40f)) * 30);
			//Destroy bullet and enemy
			Destroy(WhoCollidedWithMe.gameObject);
			DestroyObject(gameObject, 2.0f);
		}
	}
}