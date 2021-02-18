using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIControl : MonoBehaviour
{
    public GameObject blood;

    private void Update()
    {
        blood.GetComponent<Slider>().value = GameManager.Instance.PercentOfProgress;
    }
}
