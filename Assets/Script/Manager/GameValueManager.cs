// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Dynamic;
// using System.IO;
// using UnityEngine;

// [Serializable]
// public class Value
// {
//     public string description;
//     public float damage;
//     public float defence;
//     public float healing;
//     public string effect;
// }

// [Serializable]
// class Prop
// {
//     public string name;
//     public Value value;
// }

// [Serializable]
// class Weapon
// {
//     public string name;
//     public Value value;
// }

// class Root
// {
//     public List<Prop> prop = new List<Prop>();
//     public List<Weapon> weapon = new List<Weapon>();
// }

// public class GameValueManager
// {
//     private static GameValueManager instance;
//     public static GameValueManager Instance
//     {
//         get
//         {
//             if (instance == null)
//                 instance = new GameValueManager();
//             return instance;
//         }
//     }

//     private Root _root;
//     private static dynamic propValue = new ExpandoObject();
//     private static dynamic weaponValue = new ExpandoObject();

//     public dynamic PropValue { get { return propValue; } }
//     public dynamic WeaponValue { get { return weaponValue; } }

//     public void Init()
//     {
//         if (File.Exists(Application.dataPath + "/Resources/Json/Item.json"))
//         {
//             StreamReader sr = new StreamReader(Application.dataPath + "/Resources/Json/Item.json");
//             string JsonString = sr.ReadToEnd();
//             sr.Close();
//             _root = JsonUtility.FromJson<Root>(JsonString);
//             //通过字典来添加属性和赋值
//             Dictionary<string, object> dic = new Dictionary<string, object>();
//             dic.Clear();
//             foreach (var item in _root.prop)
//             {
//                 dic.Add(item.name, item);
//             }
//             propValue = new MyExtendsObject(dic);

//             dic.Clear();
//             foreach (var item in _root.weapon)
//             {
//                 dic.Add(item.name, item);
//             }
//             weaponValue = new MyExtendsObject(dic);
//         }
//         else
//         {
//             Debug.Log("Item.json is not Exists");
//         }
//     }
// }
