
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        manager.parameter.animator.Play("HeroKnight_Idle");
    }

    public void OnUpdate()
    {
        if (manager.target != null && manager.Attacktarget != null)
        {
            manager.TransitionState(StateType.Attack);
        }
        else if (manager.target != null)
        {
            manager.TransitionState(StateType.Run);
        }
    }

    public void OnExit()
    {
    }
}