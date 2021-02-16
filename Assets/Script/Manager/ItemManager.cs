using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Value
{
    public string description;
    public float damage;
    public float defence;
    public float healing;
    public string effect;
    public string path;
}

[Serializable]
class Prop
{
    public string name;
    public Value value;
}

[Serializable]
class Weapon
{
    public string name;
    public Value value;
}

[Serializable]
class Slot
{
    public string name;
    public Value value;
}

class Root
{
    public List<Prop> prop = new List<Prop>();
    public List<Weapon> weapon = new List<Weapon>();
    public List<Slot> slot = new List<Slot>();
}

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Root _root;
    public Dictionary<string, Dictionary<string, object>> propValue = new Dictionary<string, Dictionary<string, object>>();
    public Dictionary<string, Dictionary<string, object>> weaponValue = new Dictionary<string, Dictionary<string, object>>();
    public Dictionary<string, Dictionary<string, object>> slotValue = new Dictionary<string, Dictionary<string, object>>();
    public List<string> itemNames;
    public List<string> itemTypes;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        this.Init();
    }

    private void Init()
    {
        if (File.Exists(Application.dataPath + "/StreamingAssets/Json/Item.json"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets/Json/Item.json");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            _root = JsonUtility.FromJson<Root>(JsonString);
            foreach (var item in _root.prop)
            {
                propValue.Add(item.name, new Dictionary<string, object>());
                propValue[item.name].Add("description", item.value.description);
                propValue[item.name].Add("damage", item.value.damage);
                propValue[item.name].Add("defence", item.value.defence);
                propValue[item.name].Add("healing", item.value.healing);
                propValue[item.name].Add("effect", item.value.effect);
                propValue[item.name].Add("path", item.value.path);
                propValue[item.name].Add("type", "prop");
                itemNames.Add(item.name);
                itemTypes.Add("prop");
            }
            foreach (var item in _root.weapon)
            {
                weaponValue.Add(item.name, new Dictionary<string, object>());
                weaponValue[item.name].Add("description", item.value.description);
                weaponValue[item.name].Add("damage", item.value.damage);
                weaponValue[item.name].Add("defence", item.value.defence);
                weaponValue[item.name].Add("healing", item.value.healing);
                weaponValue[item.name].Add("effect", item.value.effect);
                weaponValue[item.name].Add("path", item.value.path);
                weaponValue[item.name].Add("type", "weapon");
                itemNames.Add(item.name);
                itemTypes.Add("weapon");
            }
            foreach (var item in _root.slot)
            {
                slotValue.Add(item.name, new Dictionary<string, object>());
                slotValue[item.name].Add("description", item.value.description);
                slotValue[item.name].Add("damage", item.value.damage);
                slotValue[item.name].Add("defence", item.value.defence);
                slotValue[item.name].Add("healing", item.value.healing);
                slotValue[item.name].Add("effect", item.value.effect);
                slotValue[item.name].Add("path", item.value.path);
                slotValue[item.name].Add("type", "slot");
                itemNames.Add(item.name);
                itemTypes.Add("slot");
            }
        }
        else
        {
            Debug.Log("Item.json is not Exists");
        }
    }

    public object GetPropertyByName(string name, string type, string property)
    {
        object obj = null;
        switch (type)
        {
            case "prop":
            obj = (string)propValue[name][property];
            break;
            case "weapon":
            obj = (string)weaponValue[name][property];
            break;
            case "slot":
            obj = (string)slotValue[name][property];
            break;
            default:
            break;
        }
        return obj;
    }
}