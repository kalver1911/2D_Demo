using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Walk,BlockIdle,Block,Run,Attack
}

[Serializable]
public class Parameter
{
    public Animator animator;
    public float timer;
    public bool AttackEnable=true;
    public float AttackCD=1.5f;
}
public class FSM : MonoBehaviour
{
    public Rigidbody2D Rb;
    public LayerMask LM;
    public float CircleRadius = 10;
    public float AttackCircleRadius = 2;
    public Collider2D target;
    public Collider2D Attacktarget;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public GameObject Freeze;
    public int Dir = -1;
    public Transform Player;

    public Parameter parameter;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        parameter.animator = transform.GetComponent<Animator>();
        states.Add(StateType.Idle, new IdleState(this));
        //states.Add(StateType.Walk, new WalkState(this));
        states.Add(StateType.BlockIdle, new BlockIdleState(this));
        states.Add(StateType.Block, new BlockState(this));
        states.Add(StateType.Run, new RunState(this));
        states.Add(StateType.Attack, new AttackState(this));
        TransitionState(StateType.Idle);
    }

    public void Update()
    {
        Vector2 ababa = gameObject.transform.position;
        Vector2 babab = Player.transform.position;
        print(babab-ababa);
        if (Freeze != null)
        {
            if (Freeze.activeInHierarchy == false)
            {
                PlayerCheck();
                parameter.animator.speed = 1;
                currentState.OnUpdate();
            }
            else
            {
                parameter.animator.speed = 0;
            }
        }
        else 
        {
            PlayerCheck();
            AttackCheck();
            parameter.animator.speed = 1;
            currentState.OnUpdate();
        }

    }

    public void TransitionState(StateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }
    public void FlipDirection()
    {
        if (Player.position.x - 0.1 > transform.position.x)
        {
            Dir = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Player.position.x + 0.08 < transform.position.x)
        {
            Dir = -1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    void PlayerCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, CircleRadius, LM);
        target = collider;
    }

    void AttackCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, AttackCircleRadius, LM);
        Attacktarget = collider;
    }
    private void OnDrawGizmos() //可视化地面检测长方形
    {
        Gizmos.DrawWireSphere(transform.position, CircleRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackCircleRadius);
    }
    public IEnumerator AttackColdDown()
    {
        if (parameter.AttackEnable == false)
            yield return null;
        parameter.AttackEnable = false;
        yield return new WaitForSeconds(parameter.AttackCD);
        parameter.AttackEnable = true;
    }
    public void BlockTest()
    {
        TransitionState(StateType.Block);
    }
}