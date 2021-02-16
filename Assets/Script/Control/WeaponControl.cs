using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    public Image weaponImage;
    public Image[] slots = new Image[3];
    public WeaponProperty weaponProperty = new WeaponProperty();
    public Text weaponText;

    private void OnEnable()
    {
        GameManager.Instance.LoadWeapon(weaponProperty);
        if (weaponProperty.itemName != "null")
        {
            Debug.Log(weaponProperty.itemName);
            ChangeTexture();
        }
        DisplayInformation();
    }

    private void OnDisable()
    {
        GameManager.Instance.SaveWeapon(weaponProperty);
        Debug.Log(GameManager.Instance.currentWeapon.weaponProperties.shoot[0]
        +GameManager.Instance.currentWeapon.weaponProperties.shoot[1]
        +GameManager.Instance.currentWeapon.weaponProperties.shoot[2]
        );
        weaponImage.sprite = null;
    }

    public void ChangeTexture()
    {
        if (weaponProperty.itemName == "null")
        {
            weaponImage.sprite = null;
        }
        else
        {
            weaponImage.sprite = Resources.Load<Sprite>((string)ItemManager.Instance.GetPropertyByName(weaponProperty.itemName, "weapon", "path"));
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (weaponProperty.weaponProperties.shoot[i] == "null")
            {
                slots[i].sprite = null;
            }
            else
            {
                slots[i].sprite = Resources.Load<Sprite>((string)ItemManager.Instance.GetPropertyByName(weaponProperty.weaponProperties.shoot[i], "slot", "path"));
            }

        }
    }

    public bool EmbeddedIntoTheSlot(string slotNmae)           //嵌入槽位
    {
        for (int i = 0; i < weaponProperty.weaponProperties.shoot.Length; i++)
        {
            if (weaponProperty.weaponProperties.shoot[i] == "null")
            {
                weaponProperty.weaponProperties.shoot[i] = slotNmae;
                slots[i].sprite = Resources.Load<Sprite>((string)ItemManager.Instance.GetPropertyByName(slotNmae, "slot", "path"));
                return true;
            }
        }
        Debug.Log("武器的槽位已满，无法增加");
        return false;
    }

    public void DisplayInformation()
    {
        weaponText.text = "WeaponName:" + weaponProperty.itemName + "\n"
        + "Multiple:" + weaponProperty.weaponProperties.Multiple + "\n"
        + "DamageLevel:" + weaponProperty.weaponProperties.DamageLevel + "\n"
        + "TrackBullet:" + weaponProperty.weaponProperties.TrackBullet + "\n"
        + "BulletRange:" + weaponProperty.weaponProperties.BulletRange + "\n"
        + "Slot1:" + weaponProperty.weaponProperties.shoot[0] + "\n"
        + "Slot2:" + weaponProperty.weaponProperties.shoot[1] + "\n"
        + "Slot3:" + weaponProperty.weaponProperties.shoot[2] + "\n";
    }

    public void Exchange(WeaponProperty wp)
    {
        string name = weaponProperty.itemName;
        int number = weaponProperty.itemNumber;
        string type = weaponProperty.itemType;
        //weaponProperties
        float bulletRange = weaponProperty.weaponProperties.BulletRange;
        int damageLevel = weaponProperty.weaponProperties.DamageLevel;
        int multiple = weaponProperty.weaponProperties.Multiple;
        string[] sss = weaponProperty.weaponProperties.shoot;
        int trackBullet = weaponProperty.weaponProperties.TrackBullet;

        weaponProperty.itemName = wp.itemName;
        weaponProperty.itemNumber = wp.itemNumber;
        weaponProperty.itemType = wp.itemType;
        //weaponProperties
        weaponProperty.weaponProperties.BulletRange = wp.weaponProperties.BulletRange;
        weaponProperty.weaponProperties.DamageLevel = wp.weaponProperties.DamageLevel;
        weaponProperty.weaponProperties.Multiple = wp.weaponProperties.Multiple;
        weaponProperty.weaponProperties.shoot = wp.weaponProperties.shoot;
        weaponProperty.weaponProperties.TrackBullet = wp.weaponProperties.TrackBullet;

        wp.itemName = name;
        wp.itemNumber = number;
        wp.itemType = type;
        //weaponProperties
        wp.weaponProperties.BulletRange = bulletRange;
        wp.weaponProperties.DamageLevel = damageLevel;
        wp.weaponProperties.Multiple = multiple;
        wp.weaponProperties.shoot = sss;
        wp.weaponProperties.TrackBullet = trackBullet;
    }

    public void UninstallWeapon()
    {
        for (int i = 0; i < BagManager.Instance.squareControls.Length; i++)
        {
            if (BagManager.Instance.squareControls[i].ItemProperty.itemName == "null")
            {
                WeaponProperty wp = new WeaponProperty();
                BagManager.Instance.squareControls[i].ItemProperty = wp;
                Exchange((WeaponProperty)BagManager.Instance.squareControls[i].ItemProperty);
                DisplayInformation();
                ChangeTexture();
                BagManager.Instance.squareControls[i].ChangeTexture(BagManager.Instance.squareControls[i].ItemName, BagManager.Instance.squareControls[i].ItemType);
                Debug.Log("成功卸下装备");
                return;
            }
        }

        Debug.Log("背包格子已满，无法卸下装备");
    }

    public void UnloadAllSlots()
    {
        for (int n = 0; n < weaponProperty.weaponProperties.shoot.Length; n++)
        {
            if (weaponProperty.weaponProperties.shoot[n] == "null")         //如果这个格子是空就卸下一个
            {
                continue;
            }

            int emptySquareNumber = -1;
            for (int i = 0; i < BagManager.Instance.squareControls.Length; i++)     //遍历数组
            {
                if (emptySquareNumber == -1 && BagManager.Instance.squareControls[i].ItemName == "null")    //在遍历数组的同时，看有没有空的格子
                {
                    emptySquareNumber = i;              //有就记录第一个找到的空格
                }
                if (BagManager.Instance.squareControls[i].ItemName == weaponProperty.weaponProperties.shoot[n])         //如果在数组里找到相同名字的
                {
                    weaponProperty.weaponProperties.shoot[n] = "null";
                    BagManager.Instance.squareControls[i].ItemNumber++;             //数量++，返回
                    BagManager.Instance.squareControls[i].ChangeTexture(BagManager.Instance.squareControls[i].ItemName, BagManager.Instance.squareControls[i].ItemType);
                    break;
                }
            }
            if (emptySquareNumber == -1)                //没有空格的情况
            {
                Debug.Log("背包已满");
            }
            else            //新增加物品的情况
            {
                BagManager.Instance.squareControls[emptySquareNumber].ItemName = weaponProperty.weaponProperties.shoot[n];
                BagManager.Instance.squareControls[emptySquareNumber].ItemType = "slot";
                BagManager.Instance.squareControls[emptySquareNumber].ItemNumber++;
                weaponProperty.weaponProperties.shoot[n] = "null";
                BagManager.Instance.squareControls[emptySquareNumber].ChangeTexture(BagManager.Instance.squareControls[emptySquareNumber].ItemName, BagManager.Instance.squareControls[emptySquareNumber].ItemType);
            }
        }
        ChangeTexture();
        DisplayInformation();
    }
}
