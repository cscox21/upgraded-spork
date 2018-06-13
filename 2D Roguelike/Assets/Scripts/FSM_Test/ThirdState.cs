using UnityEngine;
using StateMachine;
using System.Collections;


public class ThirdState : State<AI_FSM>
{
    private static ThirdState _instance;

    private ThirdState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThirdState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThirdState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI_FSM _object)
    {
        Debug.Log("Entering Third State");


    }

    public override void ExitState(AI_FSM _owner)
    {
        Debug.Log("Exiting Third State");
    }

    public override void UpdateState(AI_FSM _owner)
    {

        if (_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FourthState.Instance);
        }

    }

}