using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Player;
    private Vector3 offset;
    [Range(0f, 100f)] public float force=6;
    void Awake()
    {
        offset = Player.position - transform.position;
    }

    // Update is called once per frame  Vector3.Slerp(transform.position, Player.gameObject.transform.position,10);
    void FixedUpdate()
    {
        //transform.position = Player.position - offset;
        transform.position = Vector3.Slerp(transform.position, Player.position - offset, force / 100);
    }
}
