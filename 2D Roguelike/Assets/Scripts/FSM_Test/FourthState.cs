using UnityEngine;
using StateMachine;


public class FourthState : State<AI_FSM>
{
    private static FourthState _instance;

    private FourthState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FourthState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FourthState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI_FSM _object)
    {
        Debug.Log("Entering Fourth State");
    }

    public override void ExitState(AI_FSM _owner)
    {
        Debug.Log("Exiting Fourth State");
    }

    public override void UpdateState(AI_FSM _owner)
    {
        if (!_owner.switchState)
        {
            _owner.stateMachine.ChangeState(FirstState.Instance);
        }

    }
}

