
using UnityEngine;
using StateMachine;


public class FirstState : State<AI_FSM>
{
    private static FirstState _instance;

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstState Instance
    {
        get
        {
            if(_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI_FSM _object)
    {
        Debug.Log("Entering First State");
    }

    public override void ExitState(AI_FSM _owner)
    {
        Debug.Log("Exiting First State");
    }

    public override void UpdateState(AI_FSM _owner)
    {
        if(_owner.switchState)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
