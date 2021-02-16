using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArea : MonoBehaviour
{
    public Transform Player;
    [Range(0f, 100f)] public float force = 6;
    void Awake()
    {

    }

    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, Player.position, force / 100);
    }

}
