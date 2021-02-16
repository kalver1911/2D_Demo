using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1, 1, 1) * 0.6f * Time.deltaTime;
    }
}
