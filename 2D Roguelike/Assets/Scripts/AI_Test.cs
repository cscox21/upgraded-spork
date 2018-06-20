using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Test : MonoBehaviour {

    private EnemyActionType eCurState = EnemyActionType.Idle;
    private bool dirRight = true;
    public float speed = 0.5f;
    bool turning = false;
    bool facingRight = true;
    public float sight = 7f;
    public float dodgingSight =2f;

    //BossFight script variables
    public Transform[] fireLocation;
    public GameObject projectile;
    public float fireballSpeed;

    //Jumping Variables
    public float jumpForce = 800f;
    public Rigidbody2D rb;

    public enum EnemyActionType{ Idle, Moving, AvoidingObstacle, Attacking}
    //public EnemyActionType CurrentState = EnemyActionType.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Idle());
    }

    //TODO: Create an actual Idle coroutine


    IEnumerator MoveLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        RaycastHit2D dodgingHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, dodgingSight);
        dirRight = false;
        if (dirRight == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            turning = true;
            Debug.Log("Moving Left");
        }
        if(hit.collider != null && hit.collider.tag == "Player")
        {
            //StopCoroutine(MoveLeft());
            StartCoroutine(Attacking());
        }
        if (dodgingHit.collider != null && dodgingHit.collider.tag == "ground")
        {
            StartCoroutine(Dodging());
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        RaycastHit2D dodgingHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, dodgingSight);
        dirRight = true;
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            turning = false;
            Debug.Log("Moving Right");
        }
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            //StopCoroutine(MoveRight());
            StartCoroutine(Attacking());
        }
        if(dodgingHit.collider !=null && dodgingHit.collider.tag == "ground")
        {
            StartCoroutine(Dodging());
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveLeft());
    }

    IEnumerator Attacking()
    {
        int i = 0;
        while (i < 3)
        {
            GameObject bossFireball = Instantiate(projectile, fireLocation[0].position, Quaternion.identity);

            if (!facingRight)
            {
                bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.right * fireballSpeed;
            }
            if (facingRight)
            {
                bossFireball.GetComponent<Rigidbody2D>().velocity = Vector2.left * fireballSpeed;
            }
            i++;
            Debug.Log("Attacking the player");
            yield return null;
        }
    }

    IEnumerator Dodging()
    {

        RaycastHit2D dodgingHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, dodgingSight);
        if (dodgingHit.collider != null && dodgingHit.collider.tag == "ground")
        {
            rb.AddForce(transform.up * jumpForce);
            Debug.Log("Dodge!!!");
        }
        yield return null;
    }

    void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        RaycastHit2D dodgingHit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, dodgingSight);

        switch (eCurState)
        {
            case EnemyActionType.Idle:
                if (dirRight)
                {
                    StartCoroutine(MoveRight());
                }
                break;

            case EnemyActionType.Moving:
                if (dirRight == false && transform.position.x >= 1.0f) 
                    StartCoroutine(MoveLeft());
                break;

            case EnemyActionType.Attacking: 
                if (hit.collider != null && hit.collider.tag == "Player")
                {
                    StartCoroutine(Attacking());
                }
                break;

            case EnemyActionType.AvoidingObstacle:
                if(dodgingHit.collider !=null && dodgingHit.collider.tag == "ground")
                {
                    StartCoroutine(Dodging());
                }
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
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * sight);
    }
}
