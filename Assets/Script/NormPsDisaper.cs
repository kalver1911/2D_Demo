using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormPsDisaper : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isStopped)
        {
            gameObject.SetActive(false);
        }
    }
}
