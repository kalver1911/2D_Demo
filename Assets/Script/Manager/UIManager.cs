using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIManager instance;
    public UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private GameObject bag;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject gameUI;

    // public GameObject[] square;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start() 
    {
        // for (int i = 0; i < square.Length; i++)
        // {
        //     square[i].transform.GetChild(0).GetComponent<Image>().sprite=Resources.Load<Sprite>("RPGicons/64X64/Elixir_1");
        //     square[i].transform.GetChild(0).GetComponent<Image>().color=new Color(1,1,1,1);
        // }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenOrCloseBag();
        }
    }

    public void OpenOrCloseBag()
    {
        if (bag.activeSelf == false)
            bag.SetActive(true);
        else
            bag.SetActive(false);
    }

    public void ClickMenuButton()
    {
        if (menu.activeSelf == false)
            menu.SetActive(true);
        else
            menu.SetActive(false);
    }
}
