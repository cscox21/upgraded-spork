﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : MonoBehaviour {

    //Variables needed from ZombiePatrol script
    public Transform[] patrolpoints;
    int currentPoint;
    public float speed = 0.5f;
    public float timeStill = 1.2f;
    public float sight = 3f;
    Animator anim;

    //Variables needed from AI_TestTwo
    public enum EnemyActionType { Idle = 0, Patrol = 1, Attack = 2}; //Declares the states
    public EnemyActionType CurrentState = EnemyActionType.Idle; //Default state is Idle
    public float obstacleSight = 1.2f; //distance enemy is from obstacle where it will jump
    public float jumpForce = 800f; //force of enemies jump height
    public float sideJumpForce = 500f; //force that moves horizontally in air
    public Rigidbody2D rb; //referebce to the enemy's rigidbody
    bool facingRight = false; //wether enemy is facing right or now
    bool turning = false; //whether the enemy is turning
    public float fireballSpeed; //speed of the projectile
    [SerializeField]
    public GameObject projectile; //reference to the GameObject projectile
    public GameObject powerProjectile; //reference to the GameObject powerProjectile (the enemy's 2nd type of attack)
    public Transform[] fireLocation;  //reference to the location of where projectiles are instantiated from
    public float headHeight;

    float fireRate;
    float specialFireRate;
    float nextFire;
    float nextSpecialFire;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        turning = false;
        ChangeState(CurrentState);
        anim.SetBool("Walking", false);
        fireRate = 1f;
        specialFireRate = 4f;
        nextFire = Time.time;
        nextSpecialFire = Time.time;
    }

    public IEnumerator Idle()
    {

        float WaitTime = 4f;
        float ElapsedTime = 0f;

        //Loop while idling
        while (CurrentState == EnemyActionType.Idle)
        {
            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, obstacleSight);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
            Debug.Log("We are in the Idle state");
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= WaitTime)
            {
                //Once ElapsedTime hits 2 seconds, resets to 0. 
                Debug.Log("Enemy has not seen the player, switching to Move state");
                ChangeState(EnemyActionType.Patrol);
                yield break;
            }
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator Patrol()
    {
        while (true)
        {
            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * -sight, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * sight, Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, -sight);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, sight);

            //Debug.Log("Patrolling");
            anim.SetBool("Walking", true);

            if (transform.position.x == patrolpoints[currentPoint].position.x)
            {
                //Debug.Log("The X's transform position is equal to the patrol points's current point");
                currentPoint++;
                anim.SetBool("Walking", false);
                yield return new WaitForSeconds(timeStill);
                anim.SetBool("Walking", true);
            }

            if (currentPoint >= patrolpoints.Length)
            {
                Debug.Log("The current point is greater than the patrol points length, setting current point to 0");
                //anim.SetBool("Walking", false);
                currentPoint = 0;
            }

            if (hit.collider != null && hit.collider.tag == "Player")
            {
                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
                ChangeState(EnemyActionType.Attack);
                yield break;
            }

            if (rightHit.collider != null && rightHit.collider.tag == "Player")
            {
                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
                ChangeState(EnemyActionType.Attack);
                yield break;
            }


            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), speed);

            if (transform.position.x >= patrolpoints[currentPoint].position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x <= patrolpoints[currentPoint].position.x)
                transform.localScale = Vector3.one;
            yield return null;
        }
    }
    

    public IEnumerator Attack()
    {
        while (CurrentState == EnemyActionType.Attack)
        {
            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * -sight, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * headHeight, transform.right * -sight, Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, -sight);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.up * headHeight, transform.right, sight);
            Debug.Log("Attacking the player");

            //if the raycast hits the player, shoot projectiles
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                anim.SetBool("Attacking", true);
                yield return new WaitForSeconds(1f);
                Shoot();
                anim.SetBool("Attacking", false);
            }

            if (rightHit.collider != null && rightHit.collider.tag == "Player")
            {
                anim.SetBool("Attacking", true);
                yield return new WaitForSeconds(1f);
                Shoot();
                anim.SetBool("Attacking", false);
            }
            //If cannot see player or player out of range, change state to Move
            if (hit.collider == null || rightHit.collider ==null)
            {
                Debug.Log("Player is out of range");
                anim.SetBool("Attacking", false);
                anim.SetBool("Walking", true);
                ChangeState(EnemyActionType.Patrol);
                yield break;
            }
            yield return null;
        }
    }

    public void ChangeState(EnemyActionType NewState)
    {
        StopAllCoroutines();
        CurrentState = NewState;

        switch (NewState)
        {
            case EnemyActionType.Idle:
                StartCoroutine(Idle());
                break;

            case EnemyActionType.Patrol:
                StartCoroutine(Patrol());
                break;

            case EnemyActionType.Attack:
                StartCoroutine(Attack());
                break;

            //case EnemyActionType.Dodge:
                //StartCoroutine(Dodge());
                //break;
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            Instantiate(projectile, fireLocation[0].position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }

        if (Time.time > nextSpecialFire)
        {
            Instantiate(powerProjectile, fireLocation[1].position, Quaternion.identity);
            nextSpecialFire = Time.time + specialFireRate;
        }

    }

    private void FixedUpdate()
    {
        if (turning == true && !facingRight)
            Flip();
        else if (turning == false && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //void OnDrawGizmos()
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * obstacleSight);
    //}
}
