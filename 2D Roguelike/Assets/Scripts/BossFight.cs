﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public Transform[] spots;
    public Transform[] fireLocation;
    public float speed;
    public GameObject projectile;
    public float fireballSpeed;



	// Use this for initialization
	void Start ()
    {
        StartCoroutine("Boss");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator Boss()
    {
        //First attack
        while (transform.position.x != spots[0].position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
            yield return null;
        }

        transform.localScale= new Vector2(-1, 1);
        yield return new WaitForSeconds(1f);

        int i = 0;
        while(i<6)
        {
            GameObject bossFireball = (GameObject)Instantiate(projectile, fireLocation[0].position, Quaternion.identity);
            bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;

            i++;
            yield return new WaitForSeconds(.7f);
        }

        yield return null;
    }
}
