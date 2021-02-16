using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUIXIANGCHI1 : MonoBehaviour
{
    static public DUIXIANGCHI1 instance;
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
    public Dictionary<string, SkillPool> BDATA = new Dictionary<string, SkillPool>();
    public Dictionary<string, SkillPool> IDATA = new Dictionary<string, SkillPool>();

    //测试
    public GameobjectSO BulletSO;               //存放预制体的Scriptableobject
    public GameobjectSO ImpactSO;


    string eew = "eew";
    string eee = "eee";
    string wwe = "wwe";
    string qqe = "qqe";
    //前面的eew变量名可以不与SO的数据同名，后面的eew数据必须与SO同名

    ////2021/1/10 射击序列
    //public int shoottime = 0;
    //public string[] shoot = new string[3];

    void Awake()
    {
        instance = this;

        ////创建一个空物体，作为父节点存放子弹，免得编辑器窗口出现众多子弹物体
        BulletObject = new GameObject();
        BulletObject.name = ("SkillBulletObject");   //子弹的

        ImpactObject = new GameObject();
        ImpactObject.name = ("SkillImpactObject");   //火花的
        //


        //
        AddDic(BulletSO, ref BPF);      //把SO里放的预制体作为GameObject放到字典(这里的SO是指Scriptableobject)
        AddDic(ImpactSO, ref IPF);      //想拿哪一个预制体按照名字直接在字典中取
        //

        //
        NewListData(BulletSO, ref BDATA);//SO里有多少预制体就创建多少份Pool数据
        NewListData(ImpactSO, ref IDATA);
        //

        //对象池数据初始化      //有多少类型就执行多少次！！！必须步骤
        //本体添加一次，爆炸火花添加一次
        BP.Add(eew, BDATA[eew].PooledObjects);
        IP.Add(eew, IDATA[eew].PooledObjects);

        BP.Add(eee, BDATA[eee].PooledObjects);  //这个和陨石共用爆炸的特效，所以不用创多一个

        BP.Add(wwe, BDATA[wwe].PooledObjects);
        IP.Add(wwe, IDATA[wwe].PooledObjects);

        BP.Add(qqe, BDATA[qqe].PooledObjects);
        IP.Add(qqe, IDATA[qqe].PooledObjects);
        //

        CreateBulletPooledObject(eew);
        CreateImpactPooledObject(eew);

        CreateBulletPooledObject(eee);

        CreateBulletPooledObject(wwe);
        CreateImpactPooledObject(wwe);

        CreateBulletPooledObject(qqe);
        CreateImpactPooledObject(qqe);

        //2021/1/10
        //shoot[0] = "q";
        //shoot[1] = "q";
        //shoot[2] = "w";
    }
    public GameObject GetPooledObject(string objKey)//实例化子弹的函数
    {
        for (int i = 0; i < BP[objKey].Count; ++i)
        {
            int temI = (BDATA[objKey].CurrentIndex + i) % BP[objKey].Count;
            if (!BP[objKey][temI].activeInHierarchy)//判断该物体在场景中是否处于非激活状态
            {
                BDATA[objKey].CurrentIndex = (temI + 1) % BP[objKey].Count;
                print(BP[objKey][temI]);
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
            int temI = (IDATA[objKey].CurrentIndex + i) % IP[objKey].Count;
            if (!IP[objKey][temI].activeInHierarchy)//判断该物体在场景中是否处于非激活状态
            {
                IDATA[objKey].CurrentIndex = (temI + 1) % IP[objKey].Count;
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
        //SO里第N位string与第N位Gameobject写入字典
        int length = a.keys.Count;
        b = new Dictionary<string, GameObject>(length);
        for (int i = 0; i < length; i++)
        {
            b.Add(a.keys[i], a.values[i]);
        }
    }
    void NewListData(GameobjectSO a, ref Dictionary<string, SkillPool> b)     //SO里有多少个Gameobjects就创建多少个Pool数据，并放到字典里
    {
        //SO里第N位string创建与N同名的Pool
        int length = a.keys.Count;
        b = new Dictionary<string, SkillPool>(length);
        for (int i = 0; i < length; i++)
        {
            SkillPool obj = new SkillPool();
            b.Add(a.keys[i], obj);
        }
    }
    void CreateBulletPooledObject(string a)
    {
        //要改成根据预制体内脚本给出的数量而决定生成多少个Gameobject
        GameObject BDad = new GameObject();
        BDad.name = BPF[a].ToString();
        BDad.transform.parent = BulletObject.transform;
        for (int i = 0; i < BDATA[a].PooledAmount; ++i)
        {
            //print(BPF.ContainsKey(a));
            GameObject obj = Instantiate(BPF[a]);    //创建子弹对象
            obj.SetActive(false);                       //设置子弹无效
            BP[a].Add(obj);                     //把子弹添加到链表（对象池）中
            obj.transform.parent = BDad.transform;
        }
    }
    void CreateImpactPooledObject(string a)
    {
        GameObject IDad = new GameObject();
        IDad.name = BPF[a].ToString();
        IDad.transform.parent = ImpactObject.transform;
        for (int i = 0; i < IDATA[a].PooledAmount; ++i)
        {
            //print(IPF[a]);
            GameObject obj = Instantiate(IPF[a]);    //创建子弹对象
            obj.SetActive(false);                       //设置子弹无效
            IP[a].Add(obj);                     //把子弹添加到链表（对象池）中
            obj.transform.parent = IDad.transform;
        }
    }

}
