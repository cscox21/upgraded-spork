using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class AI_FSM : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;
    public float sight = 3f;

    public StateMachine<AI_FSM> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI_FSM>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
    }

    private void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, sight);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            Debug.Log("Hit the Player with raycast");
            stateMachine.ChangeState(ThirdState.Instance);
            return;
        }

        if(hit.collider ==null)
        {
            if(Time.time > gameTimer +1)
            {
                gameTimer = Time.time;
                seconds++;
                Debug.Log(seconds);
            }

            if(seconds == 5)
            {
                seconds = 0;
                stateMachine.ChangeState(SecondState.Instance);
            }
            return;
        }



        //if (Time.time > gameTimer +1)
        //{
            //gameTimer = Time.time;
            //seconds++;
            //Debug.Log(seconds);
        //}


        //if (seconds == 9)
        //{
            //seconds = 0;
            //switchState = !switchState;
        //}



        stateMachine.Update();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * -sight);
    }

}
