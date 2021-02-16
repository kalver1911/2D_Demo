using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropControl : MonoBehaviour
{
    public void OnClick1()
    {
        Debug.Log("propName1");
        GameManager.Instance.Add("a", "prop");
    }

    public void OnClick2()
    {
        Debug.Log("propName2");
        GameManager.Instance.Add("b", "prop");
    }

    public void OnClick3()
    {
        Debug.Log("propName3");
        GameManager.Instance.Add("c","prop");
    }
}
