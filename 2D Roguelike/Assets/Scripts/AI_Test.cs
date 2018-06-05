using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Test : MonoBehaviour {

    private EnemyActionType eCurState = EnemyActionType.Idle;
    private bool dirRight = true;
    public float speed = 2.0f;
    bool turning = false;
    bool facingRight = true;


    public enum EnemyActionType{ Idle, Moving, AvoidingObstacle, Attacking}


    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(1.0f);
        dirRight = false;
        if (dirRight == false)
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        turning = true;
        Debug.Log("Moving Left");
        yield return new WaitForSeconds(1.5f); //The longer I make this the further the enemy goes, but doesnt stop him from 'twitching'

        StartCoroutine(MoveRight());
        
    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(1.0f);
        dirRight = true;
        if (dirRight)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        turning = false;
        Debug.Log("Moving Right");
        yield return new WaitForSeconds(1.5f); //The longer I make this the further the enemy goes, but doesnt stop him from 'twitching'
        
        StartCoroutine(MoveLeft());
        
    }

	void Update ()
    {
		switch(eCurState)
        {
            case EnemyActionType.Idle:
                if (dirRight)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                turning = true;
                StartCoroutine(MoveLeft());
                //HandleIdleState();  <---No idea what should be in this function
                break;

            case EnemyActionType.Moving:
                if (dirRight == false && transform.position.x >= 1.0f)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);

                //HandleMovingState(); <---No idea what should be in this function
                break;

            case EnemyActionType.Attacking: //Eventually going to add this, trying to get moving left and right correct first.
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
}
