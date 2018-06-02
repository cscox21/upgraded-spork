
using UnityEngine;
using StateMachine;


public class SecondState : State<AI_FSM>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI_FSM _object)
    {
        Debug.Log("Entering Second State");
    }

    public override void ExitState(AI_FSM _owner)
    {
        Debug.Log("Exiting Second State");
    }

    public override void UpdateState(AI_FSM _owner)
    {
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }
    }
}
