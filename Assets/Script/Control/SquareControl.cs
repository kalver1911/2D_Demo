using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SquareProperty
{
    public string itemName;
    public int itemNumber;
    public string itemType;
}

/// <summary>
/// 单个背包格子挂的脚本
/// </summary>
public class SquareControl : MonoBehaviour
{
    private ItemProperty squareProperty;
    // private string itemName = "null";
    // private int itemNumber = 0;
    private string itemPath;
    private Image image;
    private Text itemNumberText;

    private Color originalColor;
    private Color selectedColor;

    public string ItemName { get { return squareProperty.itemName; } set { squareProperty.itemName = value; } }
    public int ItemNumber { get { return squareProperty.itemNumber; } set { squareProperty.itemNumber = value; } }
    public string ItemType { get { return squareProperty.itemType; } set { squareProperty.itemType = value; } }
    public ItemProperty ItemProperty { get { return squareProperty; } set { squareProperty = value; } }

    private void OnEnable()
    {
        if (ItemName == null)
            ItemName = "null";
        this.ChangeTexture(ItemName, ItemType);
        switch (ItemType)
        {
            case "prop":

            break;
            case "weapon":

            WeaponProperty weaponProperty = new WeaponProperty();
            weaponProperty = (WeaponProperty)squareProperty;


            break;
            case "slot":

            break;
            default:
            break;
        }
    }

    private void Start()
    {
        originalColor = gameObject.GetComponent<Image>().color;
        selectedColor = new Color(0.45f, 1.0f, 0.45f, 1.0f);
    }

    public void Init(string name)
    {
        if (ItemName == name)
        {
            return;
        }
        else
        {
            // this.ChangeTexture(name);
            //如果格子变了其他道具，相应的图片也应该换
        }
    }

    public void ChangeTexture(string name, string type)
    {
        if (!ItemManager.Instance)
            return;
        ItemName = name;
        ItemType = type;
        image = transform.GetChild(0).GetComponent<Image>();
        itemNumberText = transform.GetChild(1).GetComponent<Text>();
        itemNumberText.text = ItemNumber.ToString();
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (name == "null")
        {
            image.sprite = null;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            itemNumberText.text = null;
            ItemNumber = 0;
            return;
        }
        // itemPath = (string)ItemManager.Instance.propValue[name]["path"];
        itemPath = (string)ItemManager.Instance.GetPropertyByName(name, type, "path");
        Sprite sprite = Resources.Load<Sprite>(itemPath);
        image.sprite = sprite;
    }

    public void ClickSquare()           //点击格子
    {
        if (!BagManager.Instance.isExchange)
        {
            BagManager.Instance.ShowItemText(ItemName, ItemType, this);
            BagManager.Instance.SelectItem(gameObject);
        }
        else
        {
            BagManager.Instance.Exchange(this);
        }
    }

    public void Checked()           //选中
    {
        gameObject.GetComponent<Image>().color = selectedColor;
    }

    public void Uncheck()           //不选中
    {
        gameObject.GetComponent<Image>().color = originalColor;
    }
}
