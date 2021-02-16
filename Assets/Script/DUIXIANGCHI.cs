using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUIXIANGCHI : MonoBehaviour
{
    static public DUIXIANGCHI instance;
    GameObject BulletObject;        //用于在编辑器窗口中，作为父级存放大量的物体
    GameObject ImpactObject;        //免得对象池内数量一多整个编辑器都占满

    public bool BulletlockPoolSize = true;                   //是否锁定子弹池大小
    public bool ImpactlockPoolSize = true;                   //是否锁定子弹池大小




    /* 2021/1/7 *///一列

    //预制体字典
    public Dictionary<string, GameObject> BPF;      //存放子弹预制体的字典
    public Dictionary<string, GameObject> IPF;      //存放子弹预制体的字典

    //对象池List字典
    public Dictionary<string, List<GameObject>> BP = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> IP = new Dictionary<string, List<GameObject>>();

    //对象池数据字典
    public Dictionary<string, Pool> BDATA1 = new Dictionary<string, Pool>();
    public Dictionary<string, Pool> IDATA1 = new Dictionary<string, Pool>();

    //测试
    public GameobjectSO BulletSO;               //存放预制体的Scriptableobject
    public GameobjectSO ImpactSO;


    string q = "q";
    string w = "w";
    string e = "e";         //注意，这个并不是射击序列

    ////2021/1/10 射击序列
    //public int shoottime = 0;
    //public string[] shoot = new string[3];

    void Awake()
    {
        instance = this;

        ////创建一个空物体，作为父节点存放子弹，免得编辑器窗口出现众多子弹物体
        BulletObject = new GameObject();
        BulletObject.name = ("BulletObject");   //子弹的

        ImpactObject = new GameObject();
        ImpactObject.name = ("ImpactObject");   //火花的
        //


        //
        AddDic(BulletSO, ref BPF);      //把SO里放的预制体作为GameObject放到字典(这里的SO是指Scriptableobject)
        AddDic(ImpactSO, ref IPF);      //想拿哪一个预制体按照名字直接在字典中取
        //

        //
        NewListData(BulletSO, ref BDATA1);//SO里有多少预制体就创建多少份Pool数据
        NewListData(ImpactSO, ref IDATA1);
        //

        //对象池数据初始化      //有多少类型就执行多少次！！！必须步骤
        BP.Add(q, BDATA1[q].PooledObjects);
        IP.Add(q, IDATA1[q].PooledObjects);
        BP.Add(w, BDATA1[w].PooledObjects);
        IP.Add(w, IDATA1[w].PooledObjects);
        BP.Add(e, BDATA1[e].PooledObjects);
        IP.Add(e, IDATA1[e].PooledObjects);
        //

        CreatePooledObject(q);
        CreatePooledObject(w);
        CreatePooledObject(e);

        //2021/1/10
        //shoot[0] = "q";
        //shoot[1] = "q";
        //shoot[2] = "w";
    }
    public GameObject GetPooledObject(string objKey)//实例化子弹的函数
    {
        for (int i = 0; i < BP[objKey].Count; ++i)
        {
            int temI = (BDATA1[objKey].CurrentIndex + i) % BP[objKey].Count;
            if (!BP[objKey][temI].activeInHierarchy)//判断该物体在场景中是否处于非激活状态
            {
                BDATA1[objKey].CurrentIndex = (temI + 1) % BP[objKey].Count;
                return BP[objKey][temI];
            }
        }
        if (!BulletlockPoolSize)//若对象池大小没有锁定
        {
            GameObject obj = Instantiate(BPF[objKey]);//没有可实例化的子弹时直接创建
            obj.transform.parent = BulletObject.transform;  //多出来的暂时还没找到很好的方法去放到自己对应的transform中
            BP[objKey].Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetImpactPooledObject(string objKey)
    {
        for (int i = 0; i < IP[objKey].Count; ++i)
        {
            int temI = (IDATA1[objKey].CurrentIndex + i) % IP[objKey].Count;
            if (!IP[objKey][temI].activeInHierarchy)//判断该物体在场景中是否处于非激活状态
            {
                IDATA1[objKey].CurrentIndex = (temI + 1) % IP[objKey].Count;
                return IP[objKey][temI];
            }
        }
        if (!ImpactlockPoolSize)//若对象池大小没有锁定
        {
            GameObject obj = Instantiate(IPF[objKey]);//没有可实例化的子弹时直接创建
            obj.transform.parent = ImpactObject.transform;//这里如果没这行代码，若对象池没有锁定，新创出来的obj会在编辑器里乱放
            IP[objKey].Add(obj);
            return obj;
        }
        return null;
    }
    
    void AddDic(GameobjectSO a, ref Dictionary<string, GameObject> b)       //把SO存放的Gameobjects全部放到字典中
    {
        int length = a.keys.Count;
        b = new Dictionary<string, GameObject>(length);
        for (int i = 0; i < length; i++)
        {
            b.Add(a.keys[i], a.values[i]);
        }
    }
    void NewListData(GameobjectSO a, ref Dictionary<string, Pool> b)     //SO里有多少个Gameobjects就创建多少个Pool数据，并放到字典里
    {
        int length = a.keys.Count;
        b = new Dictionary<string, Pool>(length);
        for (int i = 0; i < length; i++)
        {
            Pool obj = new Pool();
            b.Add(a.keys[i], obj);
        }
    }
    void CreatePooledObject(string a)       //子弹和爆炸火花是一套使用的，所以一起创建
    {
        GameObject BDad = new GameObject();
        BDad.name = BPF[a].ToString();
        BDad.transform.parent = BulletObject.transform;
        for (int i = 0; i < BDATA1[a].PooledAmount; ++i)
        {
            //print(BPF.ContainsKey(a));
            GameObject obj = Instantiate(BPF[a]);    //创建子弹对象
            obj.SetActive(false);                       //设置子弹无效
            BP[a].Add(obj);                     //把子弹添加到链表（对象池）中
            obj.transform.parent = BDad.transform;
        }

        GameObject IDad = new GameObject();
        IDad.name = BPF[a].ToString();
        IDad.transform.parent = ImpactObject.transform;
        for (int i = 0; i < IDATA1[a].PooledAmount; ++i)
        {
            //print(IPF[a]);
            GameObject obj = Instantiate(IPF[a]);    //创建子弹对象
            obj.SetActive(false);                       //设置子弹无效
            IP[a].Add(obj);                     //把子弹添加到链表（对象池）中
            obj.transform.parent = IDad.transform;
        }
    }

}
