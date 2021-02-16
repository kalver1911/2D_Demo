using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public GameObject Before;
    public GameObject After;
    void OnEnable()
    {
        StartCoroutine(Ronghua());
    }
    IEnumerator Ronghua()
    {
        Before.SetActive(true);
        yield return new WaitForSeconds(3);
        Before.SetActive(false);
        After.SetActive(true);
        yield return new WaitUntil(() => After.activeInHierarchy == false);
        gameObject.SetActive(false);
    }
}
