using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum GameState
{
    Playing,
    Pause,
    End
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("游戏状态")]
    private GameState gameState;

    private float currentProgress;          //当前血量
    private float totalProgress;            //总共血量
    private float percentOfProgress;        //血量的百分比

    // private string[] squareName = new string[32];
    // private int[] squareNumber = new int[32];
    public ItemProperty[] square = new ItemProperty[32];
    public WeaponProperty currentWeapon =new WeaponProperty();          //当前装备中的武器

    public GameObject lootPrefab;           //随机掉落物品的预制体

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.Playing;
        for (int i = 0; i < square.Length; i++)
        {
            square[i] = new ItemProperty();
            // square[i].itemName = "null";
            // square[i].itemType = "null";
            // square[i].itemNumber = 0;
        }
        square[0].itemName = "a";
        square[1].itemName = "b";
        square[2].itemName = "c";
        square[0].itemType = "prop";
        square[1].itemType = "prop";
        square[2].itemType = "prop";
        square[0].itemNumber = 2;
        square[1].itemNumber = 3;
        square[2].itemNumber = 1;

        totalProgress = 100.0f;
        currentProgress = 70.0f;
        percentOfProgress = currentProgress / totalProgress;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(lootPrefab, new Vector3(Random.Range(-2.0f, 2.0f), 1, 0), Quaternion.identity);
        }
    }

    public float CurrentProgress
    {
        get
        {
            return currentProgress;
        }
    }

    public float TotalProgress
    {
        get
        {
            return totalProgress;
        }
    }

    public float PercentOfProgress
    {
        get
        {
            return percentOfProgress;
        }
    }

    public void GetHurt(float damage)
    {
        if (damage > currentProgress)
        {
            currentProgress = 0;
        }
        else
        {
            currentProgress -= damage;
        }
        percentOfProgress = currentProgress / totalProgress;
    }

    public void LoadSquare(SquareControl[] squares)         //打开背包的时候
    {
        for (int i = 0; i < squares.Length; i++)
        {
            // squares[i].ItemName=squareName[i];
            // squares[i].ItemNumber=squareNumber[i];
            squares[i].ItemProperty = square[i];
            squares[i].gameObject.SetActive(true);
        }
    }

    public void SaveSquare(SquareControl[] squares)         //关闭背包的时候
    {
        for (int i = 0; i < squares.Length; i++)
        {
            // squareName[i]=squares[i].ItemName;
            // squareNumber[i]=squares[i].ItemNumber;
            square[i] = squares[i].ItemProperty;
        }
    }

    public void SaveWeapon(WeaponProperty wp)               //从背包里获得当前装备中的武器数据，以供游戏中使用
    {
        currentWeapon.itemName = wp.itemName;
        currentWeapon.itemType = wp.itemType;
        currentWeapon.itemNumber = wp.itemNumber;
        currentWeapon.weaponProperties.Multiple = wp.weaponProperties.Multiple;
        currentWeapon.weaponProperties.DamageLevel = wp.weaponProperties.DamageLevel;
        currentWeapon.weaponProperties.TrackBullet = wp.weaponProperties.TrackBullet;
        currentWeapon.weaponProperties.BulletRange = wp.weaponProperties.BulletRange;
        currentWeapon.weaponProperties.shoot = wp.weaponProperties.shoot;

        ExBullet.instance.MultipleBullet = currentWeapon.weaponProperties.Multiple;
        ExBullet.instance.Level = currentWeapon.weaponProperties.DamageLevel;
        ExBullet.instance.shoot = currentWeapon.weaponProperties.shoot;
    }

    public void LoadWeapon(WeaponProperty wp)               //这是读取json保存的数据
    {
        wp.itemName = currentWeapon.itemName;
        wp.itemType = currentWeapon.itemType;
        wp.itemNumber = currentWeapon.itemNumber;
        wp.weaponProperties.Multiple = currentWeapon.weaponProperties.Multiple;
        wp.weaponProperties.DamageLevel = currentWeapon.weaponProperties.DamageLevel;
        wp.weaponProperties.TrackBullet = currentWeapon.weaponProperties.TrackBullet;
        wp.weaponProperties.BulletRange = currentWeapon.weaponProperties.BulletRange;
        wp.weaponProperties.shoot = currentWeapon.weaponProperties.shoot;

        // wp = currentWeapon;
    }

    public void Add(string name, string type)
    {
        int emptySquareNumber = -1;
        for (int i = 0; i < square.Length; i++)     //遍历数组
        {
            if (emptySquareNumber == -1 && square[i].itemName == "null")    //在遍历数组的同时，看有没有空的格子
            {
                emptySquareNumber = i;              //有就记录第一个找到的空格
                if(type == "weapon")                //因为武器都不同，所以一个格子一个武器
                {
                    WeaponProperty weaponProperty = new WeaponProperty();
                    weaponProperty.weaponProperties.RandomProperties();             //武器的属性是在一开始捡起来的时候随机的
                    square[emptySquareNumber] = weaponProperty;
                    square[emptySquareNumber].itemName = name;
                    square[emptySquareNumber].itemType = type;
                    square[emptySquareNumber].itemNumber++;
                    return;
                }
            }
            if (type != "weapon" && square[i].itemName == name)         //如果在数组里找到相同名字的
            {
                square[i].itemNumber++;             //数量++，返回
                return;
            }
        }
        if (emptySquareNumber == -1)                //没有空格的情况
        {
            Debug.Log("背包已满");
        }
        else            //新增加物品的情况
        {
            square[emptySquareNumber].itemName = name;
            square[emptySquareNumber].itemType = type;
            square[emptySquareNumber].itemNumber++;
        }
    }

    public void PauseGame()
    {
        gameState = GameState.Pause;
        Time.timeScale = 0.0f;
    }

    public void PlayContinues()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1.0f;
    }
}
