using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP=10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HP<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
