using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockIdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;
    public BlockIdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        timer = manager.parameter.timer;
        //parameter.animator.Play("Idle");
        manager.parameter.animator.Play("HeroKnight_BlockIdle");
    }

    public void OnUpdate()
    {
        if (manager.target != null)
        {
            manager.TransitionState(StateType.Run);
        }
        timer += Time.deltaTime;
        manager.FlipDirection();
        if (timer >= 4)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        manager.parameter.timer = 0;
    }
}