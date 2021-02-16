using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    private static BagManager instance;
    public static BagManager Instance
    {
        get
        {
            return instance;
        }
    }

    public SquareControl[] squareControls;
    // private int emptySquareNumber = 0;
    [SerializeField]
    private GameObject weapon;              //装备栏里的装备格子
    [SerializeField]
    private GameObject[] slots;             //装备栏里的槽位
    private SquareControl selectionItem;       //当前被选中的背包格子的脚本

    [SerializeField]
    private Text nameText;                  //选中的背包格子的名字
    [SerializeField]
    private Text descriptionText;           //选中的背包格子的解释

    public bool isExchange = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start()
    {
        // squareControls = GetComponentsInChildren<SquareControl>();
        // squareControls[3].ChangeTexture("a");
        // squareControls[0].ChangeTexture("null");
        // foreach (var item in squareControls)
        // {
        //     if(item.ItemName=="null")
        //     {
        //         emptySquareNumber++;
        //     }
        // }
    }

    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
        // if (squareControls.Length <= 0)
        //     squareControls = GetComponentsInChildren<SquareControl>();
        GameManager.Instance.LoadSquare(squareControls);
    }

    private void OnDisable()
    {
        Initialize();
        GameManager.Instance.SaveSquare(squareControls);
        GameManager.Instance.PlayContinues();
    }

    public void ShowItemText(string name, string type, SquareControl square)          //显示物品信息
    {
        switch (type)
        {
            case "prop":
                nameText.text = name;
                if (name == "null")
                    descriptionText.text = "null";
                else
                    // descriptionText.text = ItemManager.Instance.propValue[name]["description"] as string;
                    descriptionText.text = (string)ItemManager.Instance.GetPropertyByName(name, type, "description");
                break;
            case "weapon":
                nameText.text = name;
                if (name == "null")
                    descriptionText.text = "null";
                else
                {
                    WeaponProperty wp = (WeaponProperty)square.ItemProperty;
                    descriptionText.text = "Multiple:" + wp.weaponProperties.Multiple + "\n"
                                            + "DamageLevel:" + wp.weaponProperties.DamageLevel + "\n"
                                            + "TrackBullet:" + wp.weaponProperties.TrackBullet + "\n"
                                            + "BulletRange:" + wp.weaponProperties.BulletRange + "\n"
                                            + "Slot1:" + wp.weaponProperties.shoot[0] + "\n"
                                            + "Slot2:" + wp.weaponProperties.shoot[1] + "\n"
                                            + "Slot3:" + wp.weaponProperties.shoot[2] + "\n";
                }
                break;
            case "slot":
                nameText.text = name;
                if (name == "null")
                    descriptionText.text = "null";
                else
                    // descriptionText.text = ItemManager.Instance.propValue[name]["description"] as string;
                    descriptionText.text = (string)ItemManager.Instance.GetPropertyByName(name, type, "description");
                break;
            default:
                nameText.text = name;
                if (name == "null")
                    descriptionText.text = "null";
                else
                    // descriptionText.text = ItemManager.Instance.propValue[name]["description"] as string;
                    descriptionText.text = (string)ItemManager.Instance.GetPropertyByName(name, type, "description");
                break;
        }
    }

    public void SelectItem(GameObject item)     //选中背包的格子
    {
        if (selectionItem != null)
            selectionItem.Uncheck();            //先把之前选中的变为没选中
        selectionItem = item.GetComponent<SquareControl>();
        selectionItem.Checked();                //选中格子
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void Initialize()       //背包关闭时对其初始化
    {
        if (selectionItem != null)
            selectionItem.Uncheck();
        selectionItem = null;
        isExchange = false;
        nameText.text = "something";
        descriptionText.text = "something";
    }

    public void Exchange(SquareControl other)       //交换按钮
    {
        // string swapName = selectionItem.ItemName;
        // int swapNumber = selectionItem.ItemNumber;
        // selectionItem.ItemName = other.ItemName;
        // selectionItem.ItemNumber = other.ItemNumber;
        // other.ItemName = swapName;
        // other.ItemNumber = swapNumber;
        ItemProperty squareProperty = selectionItem.ItemProperty;
        selectionItem.ItemProperty = other.ItemProperty;
        other.ItemProperty = squareProperty;
        selectionItem.ChangeTexture(selectionItem.ItemName, selectionItem.ItemType);
        other.ChangeTexture(other.ItemName, other.ItemType);
        Initialize();
    }

    public void ClickUseButton()        //使用按钮
    {
        if (selectionItem == null || selectionItem.ItemName == "null")
            return;
        Debug.Log(selectionItem.ItemProperty.itemType);
        switch (selectionItem.ItemProperty.itemType)
        {
            case "prop":
                selectionItem.ItemNumber--;
                if (selectionItem.ItemNumber == 0)          //使用完后将格子清空
                {
                    selectionItem.ItemProperty = new ItemProperty();
                    selectionItem.ChangeTexture("null", "null");
                    Initialize();
                    return;
                }
                selectionItem.ChangeTexture(selectionItem.ItemProperty.itemName, selectionItem.ItemProperty.itemType);      //未用完更新一下数量文本
                break;
            case "weapon":
                WeaponProperty selectionItemItemProperty = (WeaponProperty)selectionItem.ItemProperty;         //交换

                if (weapon.GetComponent<WeaponControl>().weaponProperty.itemName == "null")      //还未装备武器
                {
                    // weapon.GetComponent<WeaponControl>().weaponProperty = (WeaponProperty)selectionItem.ItemProperty;
                    weapon.GetComponent<WeaponControl>().Exchange(selectionItemItemProperty);

                    weapon.GetComponent<WeaponControl>().ChangeTexture();
                    selectionItem.ItemProperty = new ItemProperty();
                    selectionItem.ChangeTexture(selectionItem.ItemProperty.itemName, selectionItem.ItemProperty.itemType);
                    weapon.GetComponent<WeaponControl>().DisplayInformation();
                    Initialize();
                }
                else
                {
                    // WeaponProperty swap = new WeaponProperty();
                    // swap = selectionItemItemProperty;
                    // selectionItemItemProperty = weapon.GetComponent<WeaponControl>().weaponProperty;
                    // weapon.GetComponent<WeaponControl>().weaponProperty = swap;
                    weapon.GetComponent<WeaponControl>().Exchange(selectionItemItemProperty);

                    selectionItem.ChangeTexture(selectionItem.ItemProperty.itemName, selectionItem.ItemProperty.itemType);
                    weapon.GetComponent<WeaponControl>().ChangeTexture();
                    weapon.GetComponent<WeaponControl>().DisplayInformation();
                    Initialize();
                }
                break;
            case "slot":
                if (weapon.GetComponent<WeaponControl>().weaponProperty.itemName == "null")
                {
                    Debug.Log("还未装备武器，无法使用");
                    return;
                }

                if (!weapon.GetComponent<WeaponControl>().EmbeddedIntoTheSlot(selectionItem.ItemProperty.itemName))
                {
                    Debug.Log("武器的槽位已满");
                    return;
                }
                Debug.Log("装上槽位");
                weapon.GetComponent<WeaponControl>().DisplayInformation();
                selectionItem.ItemNumber--;
                if (selectionItem.ItemNumber == 0)          //使用完后将格子清空
                {
                    selectionItem.ItemProperty = new ItemProperty();
                    selectionItem.ChangeTexture("null", "null");
                    Initialize();
                    return;
                }
                selectionItem.ChangeTexture(selectionItem.ItemProperty.itemName, selectionItem.ItemProperty.itemType);      //未用完更新一下数量文本
                break;
            default:
                break;
        }
    }

    public void ClickExchangeButton()   //交换按钮
    {
        if (selectionItem == null)
            return;
        isExchange = true;
    }

    public void ClickSellButton()       //出售按钮
    {
        if (selectionItem == null || selectionItem.ItemProperty.itemName == "null")
            return;
        selectionItem.ItemProperty = new ItemProperty();
        selectionItem.ChangeTexture(selectionItem.ItemProperty.itemName, selectionItem.ItemProperty.itemType);
        Initialize();
    }
}
