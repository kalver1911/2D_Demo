using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsControl : MonoBehaviour
{
    public GameObject moveText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            moveText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            moveText.SetActive(false);
        }
    }
}
