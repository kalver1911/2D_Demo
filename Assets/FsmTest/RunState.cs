
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RunState : IState
{
    private FSM manager;
    private Parameter parameter;
    public RunState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        manager.parameter.animator.Play("HeroKnight_Run");
    }

    public void OnUpdate()
    {
        if (manager.target == null)
        {
            manager.TransitionState(StateType.Idle);
            manager.Rb.velocity = new Vector2(0, manager.Rb.velocity.y);
        }
        else if (manager.target != null)        //如果索敌圈有人
        {
            manager.FlipDirection();
            manager.Rb.velocity = new Vector2(manager.Dir * 250 * Time.deltaTime, manager.Rb.velocity.y);      //正向移动(移动这里和玩家移动方式不同，玩家是根据X的速度调整方向，这里要根据方向调整X速度方向)
            if (manager.Attacktarget != null)       //索敌圈有人同时攻击距离有人
            {
                manager.TransitionState(StateType.Attack);
                manager.Rb.velocity = new Vector2(0, manager.Rb.velocity.y);
            }
        }
    }

    public void OnExit()
    {
    }
}