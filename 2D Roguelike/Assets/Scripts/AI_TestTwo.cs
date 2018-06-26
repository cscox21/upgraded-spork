using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class AI_TestTwo : MonoBehaviour

{
    public enum EnemyActionType { Idle = 0, Move = 1, Attack = 2, Dodge = 3 }; //Declares the states
    public EnemyActionType CurrentState = EnemyActionType.Idle; //Default state is Idle
    private Transform ThisTransform = null;
    private Transform PlayerObject = null;

    public float AttackDistance = 1f;
    public float sight = 5f;
    public float obstacleSight = 1.2f;
    public float jumpForce = 800f;
    public Rigidbody2D rb;
    public float speed;

    bool facingRight = false;
    bool turning = false;
    public float fireballSpeed;
    public GameObject projectile;
    public Transform[] fireLocation;


    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        PlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Set starting state
        ChangeState(CurrentState);
    }

    public IEnumerator Idle()
    {
        
        float WaitTime = 2f;
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
                //Once ElapsedTime hits 2 seconds, resets to 0. TODO:Have player Move once WaitTime is released
                Debug.Log("Enemy has not seen the player, switching to Move state");
                ChangeState(EnemyActionType.Move);
                yield break;
            }
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            if (obstacleHit.collider != null && obstacleHit.collider.tag == "ground")
            {
                ChangeState(EnemyActionType.Dodge);
                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
        RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, obstacleSight);
        while (CurrentState == EnemyActionType.Move)
        {
            Debug.Log("Moving the enemy");
            
            //if you cannot see the player, go back to idle state
            if (hit.collider == null || obstacleHit.collider ==null)
            {
                Debug.Log("Cannot see the player, back to idle state");
                yield return new WaitForSeconds(2f);
                ChangeState(EnemyActionType.Idle);
            }
            //if the player is in the attack range of the enemy, start the attack state
            if (hit.collider !=null && hit.collider.tag =="Player")
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            //if the enemy is in range of an obstacle, start the dodge state
            if (obstacleHit.collider != null && obstacleHit.collider.tag == "ground")
            {
                ChangeState(EnemyActionType.Dodge);
                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Attack()
    {
        while (CurrentState == EnemyActionType.Attack)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
            //Deal Damage here
            Debug.Log("Attacking the player");
            
            GameObject bossFireball = Instantiate(projectile, fireLocation[0].position, Quaternion.identity);
            if(hit.collider !=null && hit.collider.tag == "Player")
            {
                if (!facingRight)
                {
                    bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;
                }
                if (facingRight)
                {
                    bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.right * fireballSpeed;
                }
            }
            //If cannot see player or player out of range, change state to Move
            if (hit.collider ==null)
            {
                Debug.Log("Player is out of range");
                ChangeState(EnemyActionType.Move);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator Dodge()
    {
        while (CurrentState == EnemyActionType.Dodge)
        {
            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, obstacleSight);

            if (obstacleHit.collider != null && obstacleHit.collider.tag == "ground")
            {
                Debug.Log("Dodge the obstacle!!!");
                rb.AddForce(transform.up * jumpForce);
            }
            yield return null;
        }
    }

    //State controller 
    public void ChangeState(EnemyActionType NewState)
    {
        StopAllCoroutines();
        CurrentState = NewState;

        switch (NewState)
        {
            case EnemyActionType.Idle:
                StartCoroutine(Idle());
                break;

            case EnemyActionType.Move:
                StartCoroutine(Move());
                break;

            case EnemyActionType.Attack:
                StartCoroutine(Attack());
                break;

            case EnemyActionType.Dodge:
                StartCoroutine(Dodge());
                break;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * obstacleSight);
    }
}