using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightScript : MonoBehaviour {

    private BossActionType eCurState = BossActionType.Idle;
    private bool dirRight = true;
    bool turning = false;
    bool jump = false;
    public float speed = 2.0f;
    public float jumpForce = 700f;
    int clickCount;
    bool facingRight = true;


    public enum BossActionType
    {
        Idle,
        Moving,
        AvoidingObstacle,
        Patrolling,
        Attacking
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            eCurState = BossActionType.Moving;

        }
        if (other.tag == "Bomb")
        {
            jump = true;
            eCurState = BossActionType.AvoidingObstacle;
        }
        if (other.tag == "Dangerous")
        {
            Debug.Log("HSK");

            Destroy(gameObject);
            Debug.Log("Dead");
        }
    }

    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(3.0f);
        dirRight = false;
        if (dirRight == false)
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        turning = false;
        StartCoroutine(MoveRight());

    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(3.0f);
        dirRight = true;
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        turning = true;
        StartCoroutine(MoveLeft());
    }

    void Update()
    {


        switch (eCurState)
        {
            case BossActionType.Idle:
                if (dirRight)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                turning = true;
                StartCoroutine(MoveLeft());

                //HandleIdleState();
                break;

            case BossActionType.Moving:
                if (dirRight == false && transform.position.x >= 4.0f)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                Debug.Log("Sweet Sweet");
                //HandleMovingState();
                break;

            case BossActionType.AvoidingObstacle:
                if (jump == true)
                {
                    transform.Translate(-Vector2.right * speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                    jump = false;
                }
                //HandleAvoidingObstacleState();
                break;

            case BossActionType.Patrolling:
                //HandlePatrollingState();
                break;

            case BossActionType.Attacking:
                //HandleAttackingState();
                break;
        }
    }

    void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);

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
}