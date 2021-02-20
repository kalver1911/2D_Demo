using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;
    public WalkState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        timer = manager.parameter.timer;
        //parameter.animator.Play("Idle");
        manager.parameter.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        timer -= Time.deltaTime;
        manager.FlipDirection();
        Debug.Log("run");
        if (timer < 1)
        {
            manager.TransitionState(StateType.Idle);
        }
        if (manager.target != null)
        {
            manager.Rb.velocity= new Vector2(manager.Dir * 250 * Time.deltaTime, manager.Rb.velocity.y);      //正向移动(移动这里和玩家移动方式不同，玩家是根据X的速度调整方向，这里要根据方向调整X速度方向)
        }
    }

    public void OnExit()
    {
        Debug.Log("changetoidle");
        manager.parameter.timer = timer;
    }
}