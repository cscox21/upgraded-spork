using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Test : MonoBehaviour {

    private EnemyActionType eCurState = EnemyActionType.Idle;
    private bool dirRight = true;
    public float speed = 0.5f;
    bool turning = false;
    bool facingRight = true;
    public float sight = 3f;


    public enum EnemyActionType{ Idle, Moving, AvoidingObstacle, Attacking}

    IEnumerator MoveLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        dirRight = false;
        if (dirRight == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            turning = true;
            Debug.Log("Moving Left");
            //yield return new WaitForSeconds(1.5f); //The longer I make this the further the enemy goes, but doesnt stop him from 'twitching'
        }
        if(hit.collider != null && hit.collider.tag == "Player")
        {
            //StopCoroutine(MoveLeft());
            StartCoroutine(Attacking());
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        dirRight = true;
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            turning = false;
            Debug.Log("Moving Right");
            //yield return new WaitForSeconds(1.5f); //The longer I make this the further the enemy goes, but doesnt stop him from 'twitching'
        }
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            //StopCoroutine(MoveRight());
            StartCoroutine(Attacking());
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveLeft());
    }

    IEnumerator Attacking()
    {
        Debug.Log("Attacking the player");
        yield return null;

    }


    void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);

        switch (eCurState)
        {
            case EnemyActionType.Idle:
                if (dirRight)
                //turning = true;
                StartCoroutine(MoveRight());
                break;

            case EnemyActionType.Moving:
                if (dirRight == false && transform.position.x >= 1.0f) //Took out the 2nd half of the argument and nothing happened, not sure what the 2nd part does 
                    StartCoroutine(MoveLeft());
                break;

            case EnemyActionType.Attacking: //Eventually going to add this, trying to get moving left and right correct first.
                if (hit.collider != null && hit.collider.tag == "Player")
                {
                    Debug.Log("Hit the Player with raycast");
                    //StartCoroutine(Attacking());
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
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * -sight);
    }
}
