using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,Walk
}

[Serializable]
public class Parameter
{
    public Animator animator;
    public float timer;
}
public class FSM : MonoBehaviour
{
    public Rigidbody2D Rb;
    public LayerMask LM;
    public float CircleRadius = 5;
    public Collider2D target;
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
        states.Add(StateType.Walk, new WalkState(this));
        TransitionState(StateType.Idle);
    }

    public void Update()
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
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Player.position.x + 0.08 < transform.position.x)
        {
            Dir = -1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    void PlayerCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, CircleRadius, LM);
        target = collider;
    }
    public void Aba()
    {
        print("sdada");
    }
}