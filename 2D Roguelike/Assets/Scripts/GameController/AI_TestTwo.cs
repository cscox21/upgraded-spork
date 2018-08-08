using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class AI_TestTwo : MonoBehaviour

{
    public enum EnemyActionType {Idle = 0, Move = 1, Attack = 2, Dodge = 3}; //Declares the states
    public EnemyActionType CurrentState = EnemyActionType.Idle; //Default state is Idle
    private Transform ThisTransform = null; //not sure if this is needed, could be useful later
    private Transform PlayerObject = null; //reference to the player's transform

    public float sight = 5f; //range of attack
    public float obstacleSight = 1.2f; //distance enemy is from obstacle where it will jump
    public float jumpForce = 800f; //force of enemies jump height
    public float sideJumpForce = 500f; //force that moves horizontally in air
    public Rigidbody2D rb; //referebce to the enemy's rigidbody
    public float speed; //speed of the enemy

    //variables for AI shooting projectiles
    bool facingRight = false; //wether enemy is facing right or now
    bool turning = false; //whether the enemy is turning
    public float fireballSpeed; //speed of the projectile
    [SerializeField]
    public GameObject projectile; //reference to the GameObject projectile
    public Transform[] fireLocation;  //reference to the location of where projectiles are instantiated from

    float fireRate;
    float nextFire;


    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        PlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Set starting state
        turning = false;
        ChangeState(CurrentState);
        fireRate = 1f;
        nextFire = Time.time;
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
                //Once ElapsedTime hits 2 seconds, resets to 0. 
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
        while (CurrentState == EnemyActionType.Move)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, obstacleSight);
            //turning = false;   //<----enabling this makes zombie turn around too quickly and gets stuck at first obstacle in sight. 
            Debug.Log("Moving the enemy");

            //Actual movement of the enemy
            if (facingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            if (!facingRight)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime); 
            }
                          
            //if the enemy is in attack range of the player, start the attack state
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            //if the enemy is in range of an obstacle, start the dodge state
            if (obstacleHit.collider != null && obstacleHit.collider.tag == "ground")
            {
                
                Debug.Log("We are turning the enemy's direction around");
                yield return new WaitForSeconds(.5f);
                turning = true;
                if(turning)
                {
                    yield return new WaitForSeconds(.5f);
                    ChangeState(EnemyActionType.Move);
                    turning = false;
                }
                //ChangeState(EnemyActionType.Move);  // <---unsure if needed, appears to do nothing
            }
            yield return null;
            turning = false;
            ChangeState(EnemyActionType.Move);
        }
    }

    public IEnumerator Attack()
    {
        while (CurrentState == EnemyActionType.Attack)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
            //Deal Damage here
            Debug.Log("Attacking the player");
            
            //if the raycast hits the player, shoot projectiles
            if(hit.collider !=null && hit.collider.tag == "Player")
            {
                //Shoot();

                GameObject bossFireball = Instantiate(projectile, fireLocation[0].position, Quaternion.identity);
                if (!facingRight)
                {
                    bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;
                }
                if (facingRight)
                { 
                    bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.right * fireballSpeed;
                }
                yield return new WaitForSeconds(.8f);
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

            //if the obstacleHit raycast hits an object, jump over TODO: Probably need to turn this into a flip around function 
            if (obstacleHit.collider != null && obstacleHit.collider.tag == "ground")
            {
                Debug.Log("Dodge the obstacle!!!");
                rb.AddForce(transform.up * jumpForce);
                rb.AddForce(transform.right * -sideJumpForce);
            }
            ChangeState(EnemyActionType.Move);
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

    void Shoot()
    {
        if(Time.time >nextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
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