using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class AI_FSM : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;

    public StateMachine<AI_FSM> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI_FSM>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
    }

    private void Update()
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
            switchState = !switchState;
        }

        stateMachine.Update();
    }
}
