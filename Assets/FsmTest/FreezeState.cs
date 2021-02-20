using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;
    public FreezeState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        //parameter.animator.Play("Idle");
        timer = manager.parameter.timer;
        manager.parameter.animator.Play("Idle");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        Debug.Log("idle");
        if (timer > 5)
        {
            manager.TransitionState(StateType.Walk);
        }
    }

    public void OnExit()
    {
        Debug.Log("changetorun");
        manager.parameter.timer = timer;
    }
}