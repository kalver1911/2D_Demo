using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    private AnimatorStateInfo info;
    public BlockState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        timer = manager.parameter.timer;
        manager.parameter.animator.Play("HeroKnight_Block");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        timer -= Time.deltaTime;
        manager.FlipDirection();
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(StateType.BlockIdle);
        }
    }

    public void OnExit()
    {
        manager.parameter.timer = timer;
    }
}