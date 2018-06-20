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

    public bool CanSeePlayer = false;
    public float ViewAngle = 90f;
    public float AttackDistance = 1f;
    public float sight = 5f;

    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        PlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        //Set starting state
        ChangeState(CurrentState);
    }

    public IEnumerator Idle()
    {
        //Get Random point
        float WaitTime = 2f;
        float ElapsedTime = 0f;

        //Loop while idling
        while (CurrentState == EnemyActionType.Idle)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
            Debug.Log("We are in the Idle state");
            ElapsedTime += Time.deltaTime;
            if (ElapsedTime >= WaitTime)
            {
                //Once ElapsedTime hits 2 seconds, resets to 0. TODO:Have player Move once WaitTime is released
                ElapsedTime = 0f;
            }
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);
        while (CurrentState == EnemyActionType.Move)
        {
            Debug.Log("Moving the enemy");
            //ThisAgent.SetDestination(PlayerObject.position);
            //if you cannot see the player, go back to idle state
            if (hit.collider != null && hit.collider.tag != "Player")
            {
                yield return new WaitForSeconds(2f);
                if (hit.collider != null && hit.collider.tag != "Player")
                {
                    Debug.Log("Did not see player after 2 seconds, going back to idle");
                    ChangeState(EnemyActionType.Idle);
                    yield break;
                }
            }
            //if the player is in the attack range of the enemy, start the attack state
            if (Vector3.Distance(ThisTransform.position, PlayerObject.position) <= AttackDistance)
            {
                ChangeState(EnemyActionType.Attack);
                yield break;
            }
            yield return null;
        }
    }

    public IEnumerator Attack()
    {
        while (CurrentState == EnemyActionType.Attack)
        {
            //Deal Damage here
            Debug.Log("Attacking the player");

            //If cannot see player or player out of range, change state to Move
            if (!CanSeePlayer || Vector3.Distance(ThisTransform.position, PlayerObject.position) > AttackDistance)
            {
                ChangeState(EnemyActionType.Move);
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
        }
    }

    void OnTriggerStay(Collider Col)
    {
        if (!Col.CompareTag("Player"))
            return;
        CanSeePlayer = false;

        //Player transform
        Transform PlayerTransform = Col.GetComponent<Transform>();

        //Is player in sight
        Vector3 DirToPlayer = PlayerTransform.position - ThisTransform.position;

        //Get viewing Angle
        float ViewingAngle = Mathf.Abs(Vector3.Angle(ThisTransform.forward, DirToPlayer));

        if (ViewingAngle > ViewAngle)
            return;

        //Is there a direct line of signt?
        if (!Physics.Linecast(ThisTransform.position, PlayerTransform.position))
            CanSeePlayer = true;
    }
    void OnTriggerExit(Collider Col)
    {
        if (!Col.CompareTag("Player"))
            return;
        CanSeePlayer = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * sight);
    }
}