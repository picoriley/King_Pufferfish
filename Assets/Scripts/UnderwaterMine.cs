﻿using UnityEngine;
using System.Collections;

public class UnderwaterMine : MonoBehaviour {

	private float spawnTime;
	private GameObject[] Enemies;
	public GameObject DeathInkParticle;
	public GameObject MineExplosionParticle;
	//public GameObject MineExplosion;
	public AudioClip explosionSound;
	public AudioClip MineSpawnSound;
	public GameObject warningSign;
	public int scoreForDefeating = -100;

	public GameObject explosionFlash;
	public float explosionFlashDelay = 0.2f;

	
	// Use this for initialization
	void Start () 
	{
		spawnTime = Time.time;
		GameController.instance.PlaySound(MineSpawnSound);
		Instantiate(warningSign);
	}
	
	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sin(Time.time + spawnTime));
		if (GameController.instance.isRageMode)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
	{
		if (WhoCollidedWithMe.tag == "Bullet")
		{
			Destroy(WhoCollidedWithMe.gameObject);
			GameController.instance.PlaySound(explosionSound);

			if(Enemies == null)
			{
				Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			}

			foreach(GameObject enemy in Enemies)
			{
				GameObject inkParticle = (GameObject)GameObject.Instantiate(DeathInkParticle, new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
				enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(20f, 40f)) * 30);
			}

			foreach (GameObject enemy in Enemies)
			{
				Destroy(enemy, 2.0f);
			}

			HealthWall.hitByMine = true;

			//Update Score
			GameController.instance.AddScore(scoreForDefeating, transform.position);
			GameController.instance.ResetMultiplier();

			//commented out the animation
			//GameObject mineExplosion = (GameObject)GameObject.Instantiate(MineExplosion, new Vector3(this.transform.position.x, this.transform.position.y, -5), Quaternion.identity);
			//Destroy(mineExplosion, 3.0f);
			GameObject mineExplosionParticle = (GameObject)GameObject.Instantiate(MineExplosionParticle, new Vector3(this.transform.position.x, this.transform.position.y, -5), Quaternion.identity);
			Destroy(mineExplosionParticle, 3.0f);
			DestroyObject(gameObject, 0.1f);

			GameObject mineExplosionFlash = (GameObject)GameObject.Instantiate(explosionFlash);
			Destroy(mineExplosionFlash, explosionFlashDelay);
		}
	}
}