using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        if (manager.parameter.AttackEnable == false)
        {
            manager.parameter.animator.Play("HeroKnight_Idle");
        }
        else
        {
            manager.parameter.animator.Play("HeroKnight_Attack1");
            manager.StartCoroutine(manager.AttackColdDown());
        }
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        manager.FlipDirection();
    }
}