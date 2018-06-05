using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Test : MonoBehaviour {

    private EnemyActionType eCurState = EnemyActionType.Idle;
    private bool dirRight = true;
    public float speed = 2.0f;
    bool turning = false;


    public enum EnemyActionType{ Idle, Moving, AvoidingObstacle, Patrolling}

    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(1.0f);
        dirRight = false;
        if (dirRight == false)
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        turning = false;
            StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(1.0f);
        dirRight = true;
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        turning = true;
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

                //HandleIdleState();
                break;

            case EnemyActionType.Moving:
                if (dirRight == false && transform.position.x >= 4.0f)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);

                //HandleMovingState();
                break;
        }
	}
}
