﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LichPatrol : MonoBehaviour
{
    
    public Transform[] patrolpoints;
    int currentPoint;
    public Transform[] firePos;
    public float speed = 0.5f;
    public float timeStill = 1.2f;
    public float sight = 3f;
    Animator anim;

    public GameObject projectile;

    float nextBasicAttack;
    public float basicAttackRate = .25f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Patrol");
        //basicAttackRate = .3f;
        nextBasicAttack = Time.time;
        anim.SetBool("Walking", true);

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (transform.position.x == patrolpoints[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetBool("Walking", false);
                yield return new WaitForSeconds(timeStill);
                anim.SetBool("Walking", true);
            }

            if (currentPoint >= patrolpoints.Length)
            {
                currentPoint = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), speed);
            LaunchProjectiles();

            if (transform.position.x < patrolpoints[currentPoint].position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x > patrolpoints[currentPoint].position.x)
                transform.localScale = Vector3.one;
            yield return null;
        }
    }
    
    void LaunchProjectiles()
    {
        if (Time.time > nextBasicAttack)
        {
            int indexNumber = Random.Range(0, firePos.Length);
            Instantiate(projectile, firePos[indexNumber].position, Quaternion.identity);
            nextBasicAttack = Time.time + basicAttackRate;
        }
    }
    
}
