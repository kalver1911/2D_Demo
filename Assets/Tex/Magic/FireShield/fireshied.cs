using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireshied : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fire;
    void OnEnable()
    {
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1);
        Setfire();
        yield return new WaitForSeconds(9);
        fire.SetActive(false);
        gameObject.SetActive(false);
    }
    void Setfire()
    {
        fire.SetActive(true);
    }
}
