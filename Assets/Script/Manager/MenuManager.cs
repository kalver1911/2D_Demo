using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Save
{
    //背包格子
    public List<string> squareName = new List<string>();
    public List<int> squareNumber = new List<int>();
    public List<string> squareType = new List<string>();
    //背包格子里的武器数据
    public List<int> bagWeaponMultiple = new List<int>();
    public List<int> bagWeaponDamageLevel = new List<int>();
    public List<int> bagWeaponTrackBullet = new List<int>();
    public List<float> bagWeaponBulletRange = new List<float>();
    public List<string> bagWeaponShoot1 = new List<string>();
    public List<string> bagWeaponShoot2 = new List<string>();
    public List<string> bagWeaponShoot3 = new List<string>();
    //当前装备的武器
    public string currentWeaponName;
    public int currentWeaponMultiple;
    public int currentWeaponDamageLevel;
    public int currentWeaponTrackBullet;
    public float currentWeaponBulletRange;
    public string[] currentWeaponShoot = new string[3];
}

public class MenuManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayContinues();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        foreach (var item in GameManager.Instance.square)
        {
            save.squareName.Add(item.itemName);
            save.squareNumber.Add(item.itemNumber);
            save.squareType.Add(item.itemType);

            if (item.itemType == "weapon")
            {
                WeaponProperty wp = (WeaponProperty)item;
                save.bagWeaponMultiple.Add(wp.weaponProperties.Multiple);
                save.bagWeaponDamageLevel.Add(wp.weaponProperties.DamageLevel);
                save.bagWeaponTrackBullet.Add(wp.weaponProperties.TrackBullet);
                save.bagWeaponBulletRange.Add(wp.weaponProperties.BulletRange);
                save.bagWeaponShoot1.Add(wp.weaponProperties.shoot[0]);
                save.bagWeaponShoot2.Add(wp.weaponProperties.shoot[1]);
                save.bagWeaponShoot3.Add(wp.weaponProperties.shoot[2]);
            }
        }

        //当前装备的武器
        save.currentWeaponName = GameManager.Instance.currentWeapon.itemName;
        save.currentWeaponMultiple = GameManager.Instance.currentWeapon.weaponProperties.Multiple;
        save.currentWeaponDamageLevel = GameManager.Instance.currentWeapon.weaponProperties.DamageLevel;
        save.currentWeaponTrackBullet = GameManager.Instance.currentWeapon.weaponProperties.TrackBullet;
        save.currentWeaponBulletRange = GameManager.Instance.currentWeapon.weaponProperties.BulletRange;
        save.currentWeaponShoot = GameManager.Instance.currentWeapon.weaponProperties.shoot;

        return save;
    }

    public void SaveByJSON()
    {
        Save save = CreateSaveGameObject();
        string saveString = JsonUtility.ToJson(save);
        StreamWriter swData = new StreamWriter(Application.dataPath + "/StreamingAssets/Json/Data.text");
        swData.Write(saveString);
        swData.Close();
        Debug.Log("save success");
    }

    public void LoadByJSON()
    {
        if (File.Exists(Application.dataPath + "/StreamingAssets/Json/Data.text"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets/Json/Data.text");
            string jsonString = sr.ReadToEnd();
            sr.Close();

            Save save = JsonUtility.FromJson<Save>(jsonString);

            int weaponNumber = 0;
            for (int i = 0; i < save.squareName.Count; i++)
            {
                if (save.squareType[i] == "weapon")
                {
                    WeaponProperty wp = new WeaponProperty();
                    wp.weaponProperties.Multiple = save.bagWeaponMultiple[weaponNumber];
                    wp.weaponProperties.DamageLevel = save.bagWeaponDamageLevel[weaponNumber];
                    wp.weaponProperties.TrackBullet = save.bagWeaponTrackBullet[weaponNumber];
                    wp.weaponProperties.BulletRange = save.bagWeaponBulletRange[weaponNumber];
                    wp.weaponProperties.shoot[0] = save.bagWeaponShoot1[weaponNumber];
                    wp.weaponProperties.shoot[1] = save.bagWeaponShoot2[weaponNumber];
                    wp.weaponProperties.shoot[2] = save.bagWeaponShoot3[weaponNumber];

                    GameManager.Instance.square[i] = wp;
                    weaponNumber++;
                }
                GameManager.Instance.square[i].itemName = save.squareName[i];
                GameManager.Instance.square[i].itemNumber = save.squareNumber[i];
                GameManager.Instance.square[i].itemType = save.squareType[i];


            }

            //当前装备的武器
            GameManager.Instance.currentWeapon.itemName = save.currentWeaponName;
            if (save.currentWeaponName == "null")
            {
                GameManager.Instance.currentWeapon.itemNumber = 0;
                GameManager.Instance.currentWeapon.itemType = "null";
            }
            else
            {
                GameManager.Instance.currentWeapon.itemNumber = 1;
                GameManager.Instance.currentWeapon.itemType = "weapon";
            }
            GameManager.Instance.currentWeapon.weaponProperties.Multiple = save.currentWeaponMultiple;
            GameManager.Instance.currentWeapon.weaponProperties.DamageLevel = save.currentWeaponDamageLevel;
            GameManager.Instance.currentWeapon.weaponProperties.TrackBullet = save.currentWeaponTrackBullet;
            GameManager.Instance.currentWeapon.weaponProperties.BulletRange = save.currentWeaponBulletRange;
            GameManager.Instance.currentWeapon.weaponProperties.shoot = save.currentWeaponShoot;
        }
        else
        {
            Debug.Log("Data.text not exists");
        }
    }

    public void ClickCloseButton()
    {
        gameObject.SetActive(false);
    }

    public void ClickQuitButton()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
